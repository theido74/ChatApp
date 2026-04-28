Imports Microsoft.VisualStudio.TestTools.UnitTesting
Public Class PasswordHasherTests
    Public Sub HashPassword_ReturnsDifferentHashEachTime()
        Dim password As String = "MySecurePassword123!"

        Dim hash1 As String = PasswordHasher.HashMotdePasse(password)
        Dim hash2 As String = PasswordHasher.HashMotdePasse(password)

        ' Assert - Les hashs doivent être différents (salts différents)
        Assert.AreNotEqual(hash1, hash2, "Chaque hash doit avoir un salt unique")
    End Sub

    Public Sub VerifyPassword_WithCorrectPassword_ReturnsTrue()
        Dim password As String = "MySecurePassword123!"
        Dim hash As String = PasswordHasher.HashMotdePasse(password)

        Dim result As Boolean = PasswordHasher.VerifierMotDePasse(password, hash)

        Assert.IsTrue(result, "Le bon password doit être accepté")
    End Sub

    Public Sub VerifyPassword_WithWrongPassword_ReturnsFalse()
        Dim correctPassword As String = "MySecurePassword123!"
        Dim wrongPassword As String = "WrongPassword456!"
        Dim hash As String = PasswordHasher.HashMotdePasse(correctPassword)


        Dim result As Boolean = PasswordHasher.VerifierMotDePasse(wrongPassword, hash)

        Assert.IsFalse(result, "Un mauvais password doit être rejeté")
    End Sub
End Class

