
Public Class ClientValidator

    Public Function ValidateUsername(username As String) As Boolean
        If String.IsNullOrWhiteSpace(username) Then

            Return False
        End If

        If username.Length < 4 OrElse username.Length > 20 Then
            Return False
        End If

        Return True
    End Function


    Public Function ValidatePassword(password As String) As Boolean
        If String.IsNullOrEmpty(password) Then
            Return False
        End If

        If password.Length < 8 OrElse password.Length > 50 Then
            Return False
        End If

        Return True
    End Function

    Public Function GetValidationMessage(username As String, password As String) As String
        If String.IsNullOrWhiteSpace(username) Then
            Login.AfficherErreur("Veuillez entrer un nom d'utilisateur")

            Return ""
        End If

        If username.Length < 4 Then
            Login.AfficherErreur("Le nom d'utilisateur doit contenir au moins 4 caractères")
            Return ""
        End If

        If username.Length > 20 Then
            Login.AfficherErreur("Le nom d'utilisateur ne doit pas dépasser 20 caractères")
            Return ""
        End If

        If String.IsNullOrEmpty(password) Then
            Login.AfficherErreur("Veuillez entrer un mot de passe")
            Return ""
        End If

        If password.Length < 8 Then
            Login.AfficherErreur("Le mot de passe doit contenir au moins 8 caractères")
            Return ""
        End If

        If password.Length > 50 Then
            Login.AfficherErreur("Le mot de passe ne doit pas dépasser 50 caractères")
            Return ""
        End If

        Return ""
    End Function

End Class
