
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

            Return ""
        End If

        If username.Length < 4 Then
            Return ""
        End If

        If username.Length > 20 Then
            Return ""
        End If

        If String.IsNullOrEmpty(password) Then
            Return ""
        End If

        If password.Length < 8 Then
            Return ""
        End If

        If password.Length > 50 Then
            Return ""
        End If

        Return ""
    End Function

End Class
