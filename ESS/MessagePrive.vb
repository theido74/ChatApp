Imports System.Data

Public Class MessagePrive
    Private messageService As New MessageService()
    Private userService As New UserService()
    Private selectedContactId As Integer = -1
    Private logger As New LogService()
    Private timer As New System.Windows.Forms.Timer

    Private Sub MessagePrive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.User IsNot Nothing Then
            lblUsername.Text = "#" & CurrentUser.User.UserName
        End If
        RemplirDataGridView()
        logger.PingDB(CurrentUser.User.UserID)
        RefreshOnlineUser()

        ' Démarrer le timer pour rafraîchir tous les 30 secondes
        timer.Interval = 30000
        AddHandler timer.Tick, AddressOf TimerTick
        timer.Start()
    End Sub

    Private Sub TimerTick(sender As Object, e As EventArgs)
        Try
            logger.PingDB(CurrentUser.User.UserID)
            RefreshOnlineUser()
            RemplirDataGridView()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RefreshOnlineUser()
        Try
            Dim lstUser = logger.isActive()

        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Charge la conversation privée entre l'utilisateur actuel et un autre utilisateur spécifié par son ID.
    ''' Il prend en paramètre l'ID de l'autre utilisateur, récupère les messages de la conversation privée depuis la base de données,
    ''' et les affiche dans le FlowLayoutPanel. Les messages envoyés par l'utilisateur actuel sont affichés avec un contrôle différent de ceux reçus.
    ''' </summary>

    Private Sub ChargerConversation(otherUserId As Integer)
        Dim messages As List(Of Message) = messageService.GetPrivateConversation(CurrentUser.User.UserID, otherUserId)

        flpMessagesPrives.Controls.Clear()

        If messages IsNot Nothing Then
            For Each msg In messages
                Dim ctrl As Control

                If msg.EmmeteurId = CurrentUser.User.UserID Then
                    ' Message ENVOYÉ
                    Dim ctrlEnvoye As New MessageControlEnvoye With {
                        .EmmeteurNom = GetEmmeteurName(msg.EmmeteurId),
                        .Contenu = msg.Contenu,
                        .TimeStamp = msg.TimeStamp
                    }
                    ctrl = ctrlEnvoye
                Else
                    ' Message REÇU
                    Dim ctrlRecu As New MessageControlRecu With {
                        .EmmeteurNom = GetEmmeteurName(msg.EmmeteurId),
                        .Contenu = msg.Contenu,
                        .TimeStamp = msg.TimeStamp
                    }
                    ctrl = ctrlRecu
                End If

                flpMessagesPrives.Controls.Add(ctrl)
            Next
            ' NOUVEAU
            If flpMessagesPrives.Controls.Count > 0 Then
                Dim lastCtrl As Control = flpMessagesPrives.Controls(flpMessagesPrives.Controls.Count - 1)
                flpMessagesPrives.ScrollControlIntoView(lastCtrl)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Récupère le nom d'utilisateur (pseudo) d'un utilisateur à partir de son ID.
    ''' Il prend en paramètre l'ID de l'utilisateur et utilise le service utilisateur pour obtenir son nom d'utilisateur,
    ''' qui est ensuite utilisé pour afficher les messages dans la conversation privée.
    ''' Et retourne le nom d'utilisateur correspondant à l'ID fourni.
    ''' </summary>

    Private Function GetEmmeteurName(userId As Integer) As String
        Return userService.GetUsernameById(userId)
    End Function

    ''' <summary>
    ''' Author: Ayman
    ''' Envoie un message privé à l'utilisateur sélectionné.
    ''' Il vérifie d'abord si un utilisateur est sélectionné et si le message n'est pas vide.
    ''' Ensuite, il utilise le service de messagerie pour créer le message privé et rafraîchit la conversation.
    ''' </summary>

    Private Sub btnEnvoyer_Click(sender As Object, e As EventArgs) Handles btnEnvoyer.Click
        If selectedContactId = -1 Then
            MessageBox.Show("Sélectionnez un utilisateur d'abord !")
            Return
        End If

        If String.IsNullOrEmpty(txtMessagePrive.Text) Then
            MessageBox.Show("Écrivez un message !")
            Return
        End If

        messageService.CreatePrivateMessage(CurrentUser.User.UserID, selectedContactId, txtMessagePrive.Text)
        txtMessagePrive.Clear()

        ' Rafraîchir
        ChargerConversation(selectedContactId)

    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Ferme le formulaire actuel et ouvre le formulaire principal lorsque l'utilisateur clique sur le bouton "Annuler".
    ''' </summary>

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click
        timer.Stop()
        timer.Dispose()
        Me.Close()
    End Sub
    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        ' Vérifier que c'est bien la colonne ChatStatut (index 1)
        'Select Case marche comme un if else if, mais plus adapté pour comparer une même variable à plusieurs valeurs différentes
        If e.ColumnIndex = 1 AndAlso e.Value IsNot Nothing Then
            If e.Value.ToString() = "En ligne" Then
                e.CellStyle.ForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Red
            End If
        End If
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement de clic sur une cellule du DataGridView pour afficher la conversation privée avec l'utilisateur sélectionné.
    ''' Lorsque l'utilisateur clique sur une cellule, cette méthode vérifie si la ligne est valide, puis récupère l'ID de l'utilisateur sélectionné à partir de la propriété Tag de la ligne.
    ''' Ensuite, elle charge la conversation privée correspondante et met à jour le label pour afficher le nom de l'utilisateur avec lequel la conversation est en cours.
    ''' </summary>
    Private Sub RemplirDataGridView()

        DataGridView1.AllowUserToAddRows = False

        Dim users As List(Of Eleve) = userService.GetAllEleve()

        DataGridView1.Rows.Clear()

        If users IsNot Nothing AndAlso users.Count > 0 Then

            For Each eleve In users

                Dim statut As String = "X"

                If eleve.ChatStatut IsNot Nothing Then
                    statut = eleve.ChatStatut.ToString()
                End If

                Dim index As Integer = DataGridView1.Rows.Add(
                eleve.UserName,
                statut)

                DataGridView1.Rows(index).Tag = eleve.UserID

            Next

        End If

    End Sub

    ''' <summary>
    ''' Permet de récupérer le fil des discussions [non utilisée]
    ''' </summary>
    ''' <auteur> Damien </auteur>
    Private Sub RemplirDataGridView2()

        DataGridView1.AllowUserToAddRows = False
        Dim chats As List(Of Chat) = messageService.GetChatById()
        DataGridView1.Rows.Clear()

        If chats IsNot Nothing AndAlso chats.Count > 0 Then

            For Each chat As Chat In chats
                Dim col1 As String = chat.UserId & "-" & chat.ContactNom & "[" & chat.DateDernierMessage.ToString() & "]"
                Dim col2 As String = ""
                If chat.NbOfUnreadMessages > 0 Then
                    col2 += " " & chat.NbOfUnreadMessages.ToString() & " nouveau m."
                End If
                If chat.Statut IsNot Nothing AndAlso chat.Statut <> "" Then
                    col2 += " <" & chat.Statut & ">"
                End If

                Dim index As Integer = DataGridView1.Rows.Add(col1, col2)
                DataGridView1.Rows(index).Tag = chat.UserId
            Next

        End If

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then
            Return
        End If

        selectedContactId = CInt(DataGridView1.Rows(e.RowIndex).Tag)
        ChargerConversation(selectedContactId)

        Dim contactName As String =
        DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString()

        lblContactName.Text = "Conversation avec " & contactName

    End Sub

    Private Sub btnEffacer_Click(sender As Object, e As EventArgs) Handles btnEffacer.Click
        messageService.DeleteConversation(selectedContactId)
        ' Rafraîchir l'interface
        ChargerConversation(selectedContactId)
    End Sub
End Class