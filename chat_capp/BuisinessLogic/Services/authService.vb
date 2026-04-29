Public Class AuthenticationService
    Private dbAccess As New UserDateAccess()
    Private passwordHasher As New PasswordHasher()
    
    ''' Authentifie un utilisateur avec username et password

    Public Function Authenticate(username As String, password As String) As Boolean

        ' Trim des entrées POUR EVITER FICHIER SQL
        username = username.Trim()
        password = password.Trim()
        
        Try
            ' Récupérer l'eleve de la BD
            Dim e As Eleve() = dbAccess.GetEleveByUsername(username)
            If e Is Nothing Then
                Throw New UnauthorizedAccessException("Utilisateur non trouvé")
            End If
            
            ' Vérifier le password
            If Not passwordHasher.VerifierMotDePasse(password, e.MdpHashed) Then
                LogError("Authenticate", $"Echec de connexion {username}")
                Throw New UnauthorizedAccessException("Nom d'utilisateur ou mot de passe incorrect")
            End If
            
            ' Login réussi
            LogInfo("Authenticate", $"User {username} (ID {user.UserID}) logged")
            Return True
            
        Catch ex As UnauthorizedAccessException
            Throw
        Catch ex As Exception
            LogError("Authenticate", ex)
            Throw New Exception("Erreur lors du logging")
        End Try
    End Function
    
    ''' Récupère un utilisateur par son username

    Public Function GetEleveByUsername(username As String) As Eleve
        If String.IsNullOrEmpty(username) Then
            Return Nothing
        End If
        
        Try
            Return dbAccess.GetEleveByUsername(username)
        Catch ex As Exception
            LogError("GetEleveByUsername", ex)
            Return Nothing
        End Try
    End Function
    
    ''' Change le mot de passe d'un utilisateur

    ' Public Function ChangePassword(userId As Integer, oldPassword As String, newPassword As String) As Boolean
    '     ' Validation
    '     If String.IsNullOrEmpty(oldPassword) OrElse String.IsNullOrEmpty(newPassword) Then
    '         Throw New ArgumentException("Passwords requis")
    '     End If
        
    '     If oldPassword = newPassword Then
    '         Throw New ArgumentException("Nouveau password doit être différent")
    '     End If
        
    '     Try
    '         ' Récupérer l'utilisateur
    '         Dim user As User = dbAccess.GetUserByID(userId)
            
    '         ' Vérifier l'ancien password
    '         If Not passwordHasher.VerifyPassword(oldPassword, user.PasswordHash) Then
    '             Throw New UnauthorizedAccessException("Ancien password incorrect")
    '         End If
            
    '         ' Hacher le nouveau password
    '         Dim newHash As String = passwordHasher.HashPassword(newPassword)
            
    '         ' Mettre à jour
    '         dbAccess.UpdateUserPassword(userId, newHash)
            
    '         ' Logger
    '         LogInfo("ChangePassword", $"User {userId} changed password")
            
    '         Return True
    '     Catch ex As Exception
    '         LogError("ChangePassword", ex)
    '         Return False
    '     End Try
    ' End Function
    
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
