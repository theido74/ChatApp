Public Class MessageService

    Private dbAccess As New messageDataAccess()
    'Private userDataAccess As New UserDateAccess()
    Private logger As New LogService()
    Private Const MESSAGE As String = "Message Envoyé"


    ' Vérifier si un chat 1:1 possède des messages non lu par l'utilisateur [DAMIEN]
    Public Function ChatHasUnreadMessages(senderId As Integer, Optional receiverId As Integer? = -1) As Integer
        If receiverId = -1 Then
            receiverId = CurrentUser.User.UserID
        End If

        Try
            Return dbAccess.ChatHasUnreadMessages(senderId, receiverId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors du comptage")
            Return Nothing
        End Try
    End Function


    ' Retrouver les conversation qui possèdent des messages non lu [DAMIEN]
    Public Function GetChatById(Optional receiverId As Integer? = -1) As List(Of Chat)
        If receiverId = -1 Then
            receiverId = CurrentUser.User.UserID
        End If

        Try
            Return dbAccess.GetChatById(receiverId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors du comptage")
            Return Nothing
        End Try
    End Function


    ' Compter le nombre de messages non lu de l'utilisateur connecté [DAMIEN]
    Public Function GetNbUnreadMessagesById(Optional receiverId As Integer? = -1) As Integer
        If receiverId = -1 Then
            receiverId = CurrentUser.User.UserID
        End If

        Try
            Return dbAccess.CountUnreadMessagesByReceiverId(receiverId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors du comptage")
            Return Nothing
        End Try
    End Function


    ' Marquer les messages d'une conversation comme lu [DAMIEN]
    Public Function MarkAsReadById(sender As Integer, Optional receiver As Integer? = -1) As Integer
        If receiver = -1 Then
            receiver = CurrentUser.User.UserID
        End If

        Try
            Return dbAccess.MarkAsReadByChat(sender, receiver)
        Catch ex As Exception
            MessageBox.Show("Erreur lors du comptage")
            Return Nothing
        End Try
    End Function


    ' Message PRIVÉ : entre deux utilisateurs
    Public Function CreatePrivateMessage(idEnvoyeur As Integer, idReceveur As Integer, contenu As String) As Integer
        If String.IsNullOrEmpty(contenu) Then
            Return Nothing
        End If

        Try
            logger.AjoutLog(idEnvoyeur, MESSAGE)
            Return dbAccess.CreateMessage(idEnvoyeur, idReceveur, contenu)
        Catch ex As Exception
            MessageBox.Show("Erreur lors de l'envoi du message privé")
            Return Nothing
        End Try
    End Function

    ' Message PUBLIC : au forum
    Public Function CreateForumMessage(idEnvoyeur As Integer, forumId As Integer, contenu As String) As Integer
        If String.IsNullOrEmpty(contenu) Then
            Return Nothing
        End If
        ' Poser question a Arneaud pour logique BD pour l'id passer en parametre de createMessage : idReceveur ou forumId ? (pour l'instant on met forumId dans les deux champs)
        Try
            logger.AjoutLog(idEnvoyeur, MESSAGE)
            Return dbAccess.CreateMessage(idEnvoyeur, forumId, contenu, forumId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors de l'envoi du message au forum")
            Return Nothing
        End Try
    End Function

    Public Function GetMessagesByForumId(forumId As Integer) As List(Of Message)
        Try
            Return dbAccess.GetMessageByForumId(forumId)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetAllUsers() As List(Of Eleve)
        Dim userAccess As New UserDateAccess()
        Return userAccess.GetAllUsers()
    End Function

    Public Function GetRecentConversations(currentUserId As Integer) As List(Of Message)
        Try
            Return dbAccess.GetRecentConversations(currentUserId)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetPrivateConversation(currentUserId As Integer, otherUserId As Integer) As List(Of Message)
        Try
            MarkAsReadById(otherUserId) ' NEW
            Return dbAccess.GetPrivateConversation(currentUserId, otherUserId)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

End Class





