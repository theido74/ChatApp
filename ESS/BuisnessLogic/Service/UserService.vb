Public Class UserService
    Private dbAccess As New UserDateAccess()
    Private logger As New LogService()



    ''' <summary>
    ''' Crée un nouvel élève dans le système.
    ''' </summary>
    ''' Auteur: Arnaud
    ''' <param name="username">Nom d'utilisateur unique de l'élève.</param>
    ''' <param name="nom">Nom de famille de l'élève.</param>
    ''' <param name="prenom">Prénom de l'élève.</param>
    ''' <param name="dateDeNaissance">Date de naissance de l'élève.</param>
    ''' <param name="email">Adresse e-mail de l'élève.</param>
    ''' <param name="mdp">Mot de passe de l'élève.</param>
    ''' <param name="niveau">Niveau actuel de l'élève.</param>
    ''' <param name="nbPoint">Nombre de points de l'élève.</param>
    ''' <param name="classe">Classe de l'élève.</param>
    ''' <returns>
    ''' Identifiant de l'élève créé ; Nothing si la création échoue.
    ''' </returns>
    Public Function CreateEleve(username As String, nom As String, prenom As String, dateDeNaissance As DateTime, email As String, mdp As String, niveau As Integer, nbPoint As Integer, classe As String) As Integer
        If String.IsNullOrEmpty(username) Then
            Return Nothing
        End If

        Try
            Return dbAccess.CreateEleve(username, nom, prenom, dateDeNaissance, email, mdp, niveau, nbPoint, classe)
        Catch ex As Exception
            MessageBox.Show("Erreur Fonction CreateEleve")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Vérifie si un utilisateur est actuellement connecté.
    ''' </summary>
    ''' <param name="id">Identifiant de l'utilisateur.</param>
    ''' <returns>
    ''' True si l'utilisateur est connecté ; False sinon.
    ''' </returns>
    Public Function CheckIsActive(id As Integer) As Boolean
        Dim lstActive As New List(Of Integer)
        lstActive = logger.isActive()
        If lstActive.Contains(id) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Récupère les informations d'un élève à partir de son nom d'utilisateur.
    ''' </summary>
    ''' <param name="username">Nom d'utilisateur recherché.</param>
    ''' <returns>
    ''' L'objet Eleve correspondant ; Nothing si aucun élève n'est trouvé
    ''' ou en cas d'erreur.
    ''' </returns>
    Public Function GetEleveByUsername(username As String) As Eleve
        Try
            Return dbAccess.GetEleveByUsername(username)
        Catch ex As Exception
            MessageBox.Show("Erreur Fonction GetEleveByUsername")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Récupère la liste de tous les élèves enregistrés.
    ''' </summary>
    ''' <returns>
    ''' Liste des élèves ; Nothing en cas d'erreur.
    ''' </returns>
    Public Function GetAllEleve() As List(Of Eleve)
        Try
            Return dbAccess.GetAllUsers()
        Catch ex As Exception
            MessageBox.Show("Erreur Fonction GetallEleve")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Récupère le nom d'utilisateur associé à un identifiant.
    ''' </summary>
    ''' <param name="userId">Identifiant de l'utilisateur.</param>
    ''' <returns>
    ''' Nom d'utilisateur correspondant ; Nothing en cas d'erreur.
    ''' </returns>
    ''' 
    Public Function GetUsernameById(userId As Integer) As String
        Try
            Return dbAccess.GetUsernameById(userId)
        Catch ex As Exception
            MessageBox.Show("Erreur Fonction GetUsernameById")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Auteur: Arnaud
    ''' Récupère les informations d'un élève à partir de son identifiant.
    ''' </summary>
    ''' <param name="userId">Identifiant de l'élève.</param>
    ''' <returns>
    ''' L'objet Eleve correspondant ; Nothing si aucun élève n'est trouvé
    ''' ou en cas d'erreur.
    ''' </returns>
    Public Function GetEleveById(userId As Integer) As Eleve
        Try
            Return dbAccess.GetEleveByID(userId)
        Catch ex As Exception
            MessageBox.Show("Erreur Fonction GetEleveById")
            Return Nothing
        End Try
    End Function

    Public Function Logout(userId As Integer) As Boolean
        Try
            Return logger.SetUserOffline(userId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors de la déconnexion")
            Return False
        End Try
    End Function

    Public Function SetOnline(userId As Integer) As Boolean
        Try
            Return logger.SetUserOnline(userId)
        Catch ex As Exception
            MessageBox.Show("Erreur lors de la mise en ligne")
            Return False
        End Try
    End Function
End Class