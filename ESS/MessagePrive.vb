Imports System.Data

Public Class MessagePrive
    Private messageService As New MessageService()
    Private userService As New UserService()
    Private userDataAccess As New UserDateAccess()
    Private selectedContactId As Integer = -1

    Private Sub MessagePrive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Afficher l'utilisateur connecté
        If CurrentUser.User IsNot Nothing Then
            lblUsername.Text = "#" & CurrentUser.User.UserName
        End If

        ' Charger la ListBox avec tous les utilisateurs
        ChargerListBoxUtilisateurs()

        ' Charger la DataGridView avec les conversations récentes
        ChargerConversationsRecentes()

        ' Rendre l'élément contenant les message scrollable uniquement sur l'axe des Y
        flpMessagesPrives.AutoScroll = True
        flpMessagesPrives.FlowDirection = FlowDirection.TopDown
        flpMessagesPrives.WrapContents = False
        flpMessagesPrives.HorizontalScroll.Enabled = False
        flpMessagesPrives.HorizontalScroll.Visible = False

    End Sub

    ' ===== LISTBOX =====
    Private Sub ChargerListBoxUtilisateurs()
        Try
            Dim users As List(Of Eleve) = messageService.GetAllUsers()
            lstUtilisateurs.Items.Clear()

            If users IsNot Nothing Then
                For Each user In users
                    lstUtilisateurs.Items.Add(user.UserName)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des utilisateurs: " & ex.Message)
        End Try
    End Sub

    Private Sub lstUtilisateurs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUtilisateurs.SelectedIndexChanged
        If lstUtilisateurs.SelectedIndex = -1 Then
            Return
        End If

        ' Désélectionner la DataGridView
        dgvConversationsRecentes.ClearSelection()

        ' Récupérer l'ID de l'utilisateur sélectionné
        Dim selectedUsername As String = lstUtilisateurs.SelectedItem.ToString()
        Dim selectedUser As Eleve = userDataAccess.GetEleveByUsername(selectedUsername)


        If selectedUser IsNot Nothing Then
            selectedContactId = selectedUser.UserID
            lblContactName.Text = "Conversation avec " & selectedUser.UserName
            ChargerConversation(selectedContactId)
        End If
    End Sub

    ' ===== DATAGRIDVIEW =====
    Private Sub ChargerConversationsRecentes()
        Try
            Dim conversations As List(Of Message) = messageService.GetRecentConversations(CurrentUser.User.UserID)

            dgvConversationsRecentes.DataSource = Nothing
            dgvConversationsRecentes.Rows.Clear()

            If conversations IsNot Nothing Then
                For Each conv In conversations
                    ' Déterminer l'autre utilisateur (celui qui n'est pas moi)
                    Dim otherUserId As Integer
                    If conv.EmmeteurId = CurrentUser.User.UserID Then
                        otherUserId = conv.ReceveurId
                    Else
                        otherUserId = conv.EmmeteurId
                    End If

                    ' Récupérer les infos de l'autre utilisateur
                    Dim otherUser As Eleve = userDataAccess.GetEleveByID(otherUserId)

                    If otherUser IsNot Nothing Then
                        Dim lastMessage As String = conv.Contenu
                        If lastMessage.Length > 30 Then
                            lastMessage = lastMessage.Substring(0, 30) & "..."
                        End If

                        dgvConversationsRecentes.Rows.Add(otherUser.UserName, otherUser.Classe, lastMessage)
                        ' Stocker l'ID dans un Tag (pour récupérer au clic)
                        dgvConversationsRecentes.Rows(dgvConversationsRecentes.Rows.Count - 1).Tag = otherUserId
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des conversations: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvConversationsRecentes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConversationsRecentes.CellClick
        If e.RowIndex < 0 Then
            Return
        End If

        ' Désélectionner la ListBox
        lstUtilisateurs.SelectedIndex = -1

        ' Récupérer l'ID de l'utilisateur (stocké dans Tag)
        selectedContactId = CInt(dgvConversationsRecentes.Rows(e.RowIndex).Tag)

        ' Afficher le nom du contact
        Dim contactName As String = dgvConversationsRecentes.Rows(e.RowIndex).Cells(0).Value.ToString()
        lblContactName.Text = "Conversation avec " & contactName

        ' Charger la conversation
        ChargerConversation(selectedContactId)
    End Sub

    ' ===== AFFICHAGE MESSAGES =====
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
                ctrl.Width = flpMessagesPrives.ClientSize.Width - 20 ' Contôler la taille

                flpMessagesPrives.Controls.Add(ctrl)
            Next
        End If
    End Sub

    Private Function GetEmmeteurName(userId As Integer) As String
        Return userDataAccess.GetUsernameById(userId)
    End Function

    ' ===== ENVOYER MESSAGE =====
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
        ChargerConversationsRecentes()
    End Sub

End Class