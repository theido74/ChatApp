Imports System.Data

Public Class ForumService
    Private dbAccess As New ForumDataAccess()
    Private logger As New LogService()
    Private Const FORUM_CREE As String = "Forum Créé"

    ''' <summary>
    ''' Auteur: Ayman
    ''' Récupère tous les forums disponibles.
    ''' </summary>
    ''' <returns>Liste de tous les forums disponibles ou Nothing en cas d'erreur</returns>
    Public Function GetAllForums() As List(Of Forums)
        Try
            Return dbAccess.GetAllForums()
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Auteur: Ayman
    ''' Récupère un forum par son identifiant.
    ''' </summary>
    ''' <param name="forumId">ID du forum à récupérer</param>
    ''' <returns>Le forum correspondant à l'ID ou Nothing en cas d'erreur</returns>
    Public Function GetForumById(forumId As Integer) As Forums
        Try
            Return dbAccess.GetForumById(forumId)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Auteur: Ayman
    ''' Crée un nouveau forum avec le nom et la description fournis.
    ''' </summary>
    ''' <param name="name">Nom du forum à créer</param>
    ''' <param name="description">Description du forum à créer</param>
    ''' <returns>ID du forum créé ou Nothing en cas d'erreur</returns>
    Public Function CreateForum(name As String, description As String) As Integer
        ' Validation
        If String.IsNullOrWhiteSpace(name) Then
            Throw New ArgumentException("Le nom du forum est obligatoire")
        End If
        If name.Length > 100 Then
            Throw New ArgumentException("Le nom du forum ne doit pas dépasser 100 caractères")
        End If

        Try

            logger.AjoutLog(CurrentUser.User.UserID, FORUM_CREE)


            Dim newForumId As Integer = dbAccess.CreateForum(name, description)

            Return newForumId

        Catch ex As InvalidOperationException

            Throw New InvalidOperationException(ex.Message)
        Catch ex As ArgumentException

            Throw New ArgumentException(ex.Message)
        Catch ex As Exception
            Throw New Exception("Erreur lors de la création du forum: " & ex.Message)
        End Try
    End Function
End Class