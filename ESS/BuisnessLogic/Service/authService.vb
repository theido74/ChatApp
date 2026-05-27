Public Class AuthenticationService
    Private dbAccess As New UserDateAccess()
    Private passwordHasher As New PasswordHasher()
    Private logger As New LogService()
    Private Const MESSAGEOK As String = "LOGGED"
    Private Const MESSAGENOTOK As String = "LOGERROR"


    Public Function Authenticate(username As String, password As String) As Boolean

        username = username.Trim()
        password = password.Trim()

        Try
            Dim e As Eleve = dbAccess.GetEleveByUsername(username)
            If e Is Nothing Then
                Throw New UnauthorizedAccessException("Utilisateur non trouvé")
            End If

            If Not PasswordHasher.VerifierMotDePasse(password, e.MdpHashed) Then
                logger.AjoutLog(e.UserID, MESSAGENOTOK)
                LogError("Authenticate", $"Echec de connexion {username}")
                Throw New UnauthorizedAccessException("Nom d'utilisateur ou mot de passe incorrect")
            End If

            LogInfo("Authenticate", $"User {username} (ID {e.UserID}) logged")
            e.IsActive = True
            logger.AjoutLog(e.UserID, MESSAGEOK)
            Return True

        Catch ex As UnauthorizedAccessException
            Throw


        Catch ex As Exception
            LogError("Authenticate", ex)
            Throw 'New Exception("Erreur lors du logging")
        End Try

    End Function


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