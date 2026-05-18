Imports System.Data

Public Class ForumService
    Private dbAccess As New ForumDataAccess()
    Private logger As New LogService()
    Private Const FORUM_CREE As String = "Forum Créé"

    ' Récupérer tous les forums actifs
    Public Function GetAllForums() As List(Of Forums)
        Try
            Return dbAccess.GetAllForums()
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

    ' Récupérer un forum par ID
    Public Function GetForumById(forumId As Integer) As Forums
        Try
            Return dbAccess.GetForumById(forumId)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

    ' Créer un nouveau forum
    Public Function CreateForum(name As String, description As String) As Integer
        ' Validation
        If String.IsNullOrWhiteSpace(name) Then
            Throw New ArgumentException("Le nom du forum est obligatoire")
        End If
        If name.Length > 100 Then
            Throw New ArgumentException("Le nom du forum ne doit pas dépasser 100 caractères")
        End If

        Try
            ' Log l'action
            logger.AjoutLog(CurrentUser.User.UserID, FORUM_CREE)

            ' Créer le forum
            Dim newForumId As Integer = dbAccess.CreateForum(name, description)

            Return newForumId

        Catch ex As InvalidOperationException
            ' Doublon détecté
            Throw New InvalidOperationException(ex.Message)
        Catch ex As ArgumentException
            ' Erreur de validation
            Throw New ArgumentException(ex.Message)
        Catch ex As Exception
            Throw New Exception("Erreur lors de la création du forum: " & ex.Message)
        End Try
    End Function
End Class