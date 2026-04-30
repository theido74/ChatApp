Public Class AuthenticationService
    Private dbAccess As UserDateAccess
    Private passwordHasher As PasswordHasher

    '''Authentifie un utilisateur avec username et password

    Public Function Authenticate(username As String, password As String) As Boolean

        ' Trim des entrées POUR EVITER FICHIER SQL
        username = If(username, "").Trim()
        password = If(password, "").Trim()

        Try
            ' Récupérer l'eleve de la BD
            Dim u As User = dbAccess.GetEleveByUsername(username)
            If u Is Nothing Then
                Throw New UnauthorizedAccessException("Utilisateur non trouvé")
            End If

            ' Vérifier le password
            If Not passwordHasher.VerifierMotDePasse(password, u.MdpHashed) Then
                LogError("Authenticate", $"Echec de connexion {username}")
                Throw New UnauthorizedAccessException("Nom d'utilisateur ou mot de passe incorrect")
            End If

            ' Login réussi
            LogInfo("Authenticate", $"User {username} (ID {u.UserID}) logged")
            Return True

        Catch ex As UnauthorizedAccessException
            Throw
        Catch ex As Exception
            LogError("Authenticate", ex)
            Throw New Exception("Erreur lors du logging")
        End Try
    End Function

    ' Optionnel : wrapper pour compatibilité si d'autres parties utilisent GetEleveByUsername
    Public Function GetEleveByUsername(username As String) As Eleve
        Dim u = dbAccess.GetEleveByUsername(username)
        If u Is Nothing Then
            Return Nothing
        End If
        Dim e As New Eleve With {
            .UserID = u.UserID,
            .Nom = u.Nom,
            .Prenom = u.Prenom,
            .Email = u.Email,
            .MdpHashed = u.MdpHashed
        }
        Return e
    End Function

    Private Sub LogError(method As String, ex As Exception)
        System.Diagnostics.Debug.WriteLine($"[ERROR] {method}: {ex.Message}")
    End Sub

    Private Sub LogError(method As String, message As String)
        System.Diagnostics.Debug.WriteLine($"[ERROR] {method}: {message}")
    End Sub

    Private Sub LogInfo(method As String, message As String)
        System.Diagnostics.Debug.WriteLine($"[INFO] {method}: {message}")
    End Sub
End Class