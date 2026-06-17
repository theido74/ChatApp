Public Class MessageService

    Private dbAccess As New messageDataAccess()
    Private logger As New LogService()
    Private Const MESSAGE As String = "Message Envoyé"
    Private Const MESSAGENOTOK As String = "Message Non Envoyé"

    ''' <summary>
    ''' Vérifier si un chat 1:1 possède des messages non lu par l'utilisateur.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="senderId"></param>
    ''' <param name="receiverId"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Retrouver les conversation qui possèdent des messages non lu
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="receiverId"></param>
    ''' <returns></returns>
    Public Function GetChatById(Optional receiverId As Integer? = -1) As List(Of Chat)
        If receiverId = -1 Then
            receiverId = CurrentUser.User.UserID
        End If

        Try
            Return dbAccess.GetChatByIdBis(receiverId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors du comptage")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Compter le nombre de messages non lu de l'utilisateur connecté
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="receiverId"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Marquer les messages d'une conversation comme lu.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="sender"></param>
    ''' <param name="receiver"></param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' Retourne une conversation à partir de l'id du contact.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="contactId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Function GetMessagesByContactId(contactId As Integer, Optional userId As Integer? = -1) As List(Of Message)
        If userId = -1 Then
            userId = CurrentUser.User.UserID
        End If

        Try
            Return dbAccess.GetMessageByContactId(contactId, userId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors du comptage")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Authors : Ayman
    ''' Crée un message privé entre deux utilisateurs.
    ''' </summary>
    ''' <param name="idEnvoyeur">ID de l'utilisateur envoyant le message</param>
    ''' <param name="idReceveur">ID de l'utilisateur recevant le message</param>
    ''' <param name="contenu">Contenu du message</param>
    ''' <returns>ID du message créé ou Nothing en cas d'erreur</returns>
    Public Function CreatePrivateMessage(idEnvoyeur As Integer, idReceveur As Integer, contenu As String) As Integer
        If String.IsNullOrEmpty(contenu) Then
            Return Nothing
        End If

        Try
            logger.AjoutLog(idEnvoyeur, MESSAGE)
            Return dbAccess.CreateMessage(idEnvoyeur, idReceveur, contenu)
        Catch ex As Exception
            logger.AjoutLog(idEnvoyeur, MESSAGENOTOK)
            MessageBox.Show("Erreur lors de l'envoi du message privé")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Authors : Ayman
    ''' Crée un message dans un forum spécifique. Le champ idReceveur est laissé à 0 ou NULL pour indiquer qu'il s'agit d'un message de forum.
    ''' </summary>
    ''' <param name="idEnvoyeur">ID de l'utilisateur envoyant le message</param>
    ''' <param name="forumId">ID du forum où le message sera publié</param>
    ''' <param name="contenu">Contenu du message</param>
    ''' <returns>ID du message créé ou Nothing en cas d'erreur</returns>
    Public Function CreateForumMessage(idEnvoyeur As Integer, forumId As Integer, contenu As String) As Integer
        If String.IsNullOrEmpty(contenu) Then
            Return Nothing
        End If

        Try
            logger.AjoutLog(idEnvoyeur, MESSAGE)
            ' Passer 0 comme idReceveur placeholder - la fonction CreateMessage gérera NULL pour les messages de forum
            Return dbAccess.CreateMessage(idEnvoyeur, 0, contenu, forumId)
        Catch ex As Exception
            logger.AjoutLog(idEnvoyeur, MESSAGENOTOK)
            MessageBox.Show("Erreur lors de l'envoi du message au forum")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Authors : Ayman
    ''' Récupère tous les messages d'un forum spécifique à partir de son ID. Les messages de forum ont idReceveur à NULL ou 0, et sont filtrés par forumId.
    ''' </summary>
    ''' <param name="forumId">ID du forum dont les messages doivent être récupérés</param>
    ''' <returns>Liste des messages du forum ou Nothing en cas d'erreur</returns>
    Public Function GetMessagesByForumId(forumId As Integer) As List(Of Message)
        Try
            Return dbAccess.GetMessageByForumId(forumId)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Authors : Ayman
    ''' Récupère la liste de tous les utilisateurs (élèves) pour permettre à l'utilisateur de sélectionner un destinataire pour les messages privés.
    ''' </summary>
    ''' <returns>Liste de tous les utilisateurs ou Nothing en cas d'erreur</returns>
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

    ''' <summary>
    ''' Authors : Ayman
    ''' Récupère tous les messages d'une conversation privée entre deux utilisateurs 
    ''' à partir de leurs IDs. Les messages sont triés par date d'envoi.
    ''' </summary>
    ''' <param name="currentUserId"></param>
    ''' <param name="otherUserId"></param>
    ''' <returns></returns>
    Public Function GetPrivateConversation(currentUserId As Integer, otherUserId As Integer) As List(Of Message)
        Try
            MarkAsReadById(otherUserId) ' NEW
            Return dbAccess.GetPrivateConversation(currentUserId, otherUserId)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Authors : Ayman
    ''' Supprime tous les messages d'une conversation privée entre deux utilisateurs 
    ''' à partir de leurs IDs. Cette action est irréversible et supprimera tous les messages échangés entre les deux utilisateurs.
    ''' </summary>
    ''' <param name="senderId">L'ID de l'utilisateur qui envoie la demande de suppression.</param>
    ''' <param name="receiverId">L'ID de l'autre utilisateur de la conversation.</param>
    ''' <returns>True si la suppression a réussi, False sinon.</returns>
    Public Function DeleteConversation(senderId As Integer, Optional receiverId As Integer? = -1) As Boolean
        If receiverId = -1 Then
            receiverId = CurrentUser.User.UserID
        End If

        Try
            Return dbAccess.DeleteMessageById(senderId, receiverId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors de la suppression de la conversation")
            Return False
        End Try
    End Function

End Class







