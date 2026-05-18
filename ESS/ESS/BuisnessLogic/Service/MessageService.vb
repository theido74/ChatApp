Public Class MessageService

    Private dbAccess As New messageDataAccess()
    Private userDataAccess As New UserDateAccess()
    Private logger As New LogService()
    Private Const MESSAGE As String = "Message Envoyé"



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
            Return dbAccess.GetPrivateConversation(currentUserId, otherUserId)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

End Class





