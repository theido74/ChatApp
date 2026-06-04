Public Class FormTest
    Private passwordHasher As New PasswordHasher()

    Dim mesServ As MessageService = New MessageService
    Dim useServ As UserService = New UserService

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbbOtherUser.DisplayMember = "Value"
        cbbOtherUser.ValueMember = "Key"

        dgvDiscussion.ClearSelection()
        TestFilDeDiscussion()
    End Sub

    ''' <summary>
    ''' Tester la fonction permettant de récupérer les discussion et la classe Chat en retournant un texte récupéré en les utilisant.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    Private Sub TestFilDeDiscussion()
        Dim userSelected As Integer = 1
        Dim chats As List(Of Chat) = mesServ.GetChatById(userSelected)

        Dim eleves As List(Of Eleve) = useServ.GetAllEleve(userSelected)
        eleves.RemoveAll(Function(e) chats.Any(Function(c) c.ContactId = e.UserID))

        For Each chat As Chat In chats
            dgvDiscussion.Rows.Add(
                chat.UserId,
                chat.ContactNom.ToString(),
                "[" & chat.DateDernierMessage.ToShortTimeString() & "]",
                chat.NbOfUnreadMessages,
                chat.Statut
            )
        Next

        cbbOtherUser.Items.Clear()
        For Each eleve As Eleve In eleves
            cbbOtherUser.Items.Add(
                New KeyValuePair(Of Integer, String)(
                    eleve.UserID,
                    eleve.UserName
                )
            )
        Next
    End Sub

    ''' <summary>
    ''' Récupère les index de la sélection dans l'événement pour aller chercher la valeur de la collone id puis actualise le label qui indique la sélection
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dgvDiscussion_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDiscussion.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        Dim userId As Integer = CInt(dgvDiscussion.Rows(e.RowIndex).Cells("UserId").Value)
        lblSelectedUser.Text = "Selected User : " & userId.ToString()

        cbbOtherUser.SelectedIndex = -1
        cbbOtherUser.Text = ""
    End Sub

    Private Sub cbbOtherUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbOtherUser.SelectedIndexChanged
        If cbbOtherUser.SelectedIndex < 0 Then Exit Sub

        Dim kvp As KeyValuePair(Of Integer, String) =
        CType(cbbOtherUser.SelectedItem, KeyValuePair(Of Integer, String))

        Dim userId As Integer = kvp.Key
        lblSelectedUser.Text = "Selected User : " & userId.ToString()

        dgvDiscussion.ClearSelection()
    End Sub

    Private Sub TestOracleConnection()
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
