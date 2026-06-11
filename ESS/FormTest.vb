Public Class FormTest
    Private passwordHasher As New PasswordHasher()

    Dim mesServ As MessageService = New MessageService
    Dim useServ As UserService = New UserService


    Private Sub FormTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetObjectProperties()
        ChargerDonnées(11)
        dgvDiscussion.ClearSelection()
    End Sub


    ''' <summary>
    ''' Configurer certaines propriétés d'objets, en l'occurence de la combo box.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    Private Sub SetObjectProperties()
        cbbOtherUser.DisplayMember = "Value"
        cbbOtherUser.ValueMember = "Key"
        cbbOtherUser.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub


    ''' <summary>
    ''' Tester la fonction permettant de récupérer le fil de discussion et d'afficher les données.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    Private Sub ChargerDonnées(currentUserId As Integer)
        Dim currentUser As Eleve = useServ.GetEleveById(currentUserId)
        lblCurrentUser.Text = "ID : " & currentUser.UserID.ToString() & " " & currentUser.UserName

        Dim chats As List(Of Chat) = mesServ.GetChatById(currentUserId)
        Dim eleves As List(Of Eleve) = useServ.GetAllEleve(currentUserId)

        cbbOtherUser.Items.Clear()
        For Each eleve As Eleve In eleves
            If eleve.UserID <> currentUserId Then
                Dim dejaDansChat As Boolean = False
                For Each chat As Chat In chats
                    If chat.ContactId = eleve.UserID Then
                        dejaDansChat = True
                        Exit For
                    End If
                Next

                If Not dejaDansChat Then
                    cbbOtherUser.Items.Add(
                        New KeyValuePair(Of Integer, String)(
                            eleve.UserID,
                            eleve.UserName
                        )
                    )
                End If
            End If
        Next

        dgvDiscussion.Rows.Clear()
        For Each chat As Chat In chats
            dgvDiscussion.Rows.Add(
                chat.ContactId,
                chat.ContactNom.ToString(),
                chat.DateDernierMessage.ToString(),
                chat.NbOfUnreadMessages,
                chat.Statut
            )
        Next
    End Sub


    ''' <summary>
    ''' Afficher l'id donné en paramètre dans lblIdSelected
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="selectedId"></param>
    Private Sub actualiserLblIdSelected(selectedId As Integer)
        lblIdSelected.Text = "ID sélectionnée : " & selectedId.ToString()
    End Sub


    ''' <summary>
    ''' Récupère les index de la sélection dans l'événement pour aller chercher la valeur de la collone id puis actualise le label qui indique la sélection
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dgvDiscussion_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDiscussion.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim userId As Integer = Convert.ToInt32(dgvDiscussion.Rows(e.RowIndex).Cells("ColUserId").Value)
        actualiserLblIdSelected(userId)

        cbbOtherUser.SelectedIndex = -1
        cbbOtherUser.Text = "-"
    End Sub


    ''' <summary>
    ''' Actualiser le label Id sélectionné
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cbbOtherUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbOtherUser.SelectedIndexChanged
        If cbbOtherUser.SelectedIndex < 0 Then Exit Sub

        Dim kvp As KeyValuePair(Of Integer, String) = CType(cbbOtherUser.SelectedItem, KeyValuePair(Of Integer, String))
        Dim userId As Integer = kvp.Key
        actualiserLblIdSelected(userId)

        dgvDiscussion.ClearSelection()
    End Sub


    Private Sub TestConnectionOracle()
        Try
            MessageBox.Show("Test de connexion en cours...", "Info")

            If DatabaseConnection.TestConnection() Then
                MessageBox.Show("✅ Connexion Oracle réussie!", "Succès")

                Dim message As New MessageService()
                Dim id As Integer = message.CreatePrivateMessage(4, 1, "TEST SALUT LOGS")
                Dim logger As New LogService()

                If id > 0 Then
                    MessageBox.Show("✅ Utilisateur trouvé: " & id, "Succès")
                    logger.PingDB(4)
                Else
                    MessageBox.Show("⚠️ Pas d'utilisateur avec ", "Info")
                End If
            Else
                MessageBox.Show("❌ Connexion Oracle échouée!", "Erreur")
            End If

        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message, "Erreur")
        End Try
    End Sub

End Class
