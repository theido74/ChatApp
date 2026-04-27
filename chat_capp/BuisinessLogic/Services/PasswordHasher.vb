Imports System.Security.Cryptography
Imports System.Text

Public Class PasswordHasher
    Private Const SALT_SIZE As Integer = 16
    Private Const HASH_ITERATIONS As Integer = 1000

    'Hache un mot de passe avec un salt aléatoire (PBKDF2-SHA256)
    'Paramètre de la fonction = mot de passe en clair
    'Retourne String (base64)

    Public Shared Function HashMotdePasse(motDePasse As String) As String
        'Génération d'un salt aléatoire
        Dim saltBytes As Byte() = New Byte(SALT_SIZE - 1) {}
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(saltBytes)
        End Using
        ' Utiliser PBKDF2 (meilleur que simple SHA-256)
        Using pbkdf2 As New Rfc2898DeriveBytes(motDePasse, saltBytes, HASH_ITERATIONS, HashAlgorithmName.SHA256)
            Dim hashBytes As Byte() = pbkdf2.GetBytes(32) ' 32 bytes = 256 bits

            ' Combiner salt + hash et encoder en base64
            Dim combinedBytes As Byte() = New Byte(saltBytes.Length + hashBytes.Length - 1) {}
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length)
            Buffer.BlockCopy(hashBytes, 0, combinedBytes, saltBytes.Length, hashBytes.Length)

            Return Convert.ToBase64String(combinedBytes)
        End Using
    End Function

    Public Shared Function VerifierMotDePasse(motdepasse As String, hash As String) As Boolean
        Try
            'Décoder le hash BASE64
            Dim hashByte As Byte() = Convert.FromBase64String(hash)

            'Extraire le SALT
            Dim saltbytes As Byte() = New Byte(SALT_SIZE - 1) {}
            Buffer.BlockCopy(hashByte, 0, saltbytes, 0, SALT_SIZE)

            'Extraire le hash Stocké
            Dim storedHashBytes As Byte() = New Byte(hashByte.Length - SALT_SIZE - 1) {}
            Buffer.BlockCopy(hashByte, SALT_SIZE, storedHashBytes, 0, storedHashBytes.Length)



        Catch ex As Exception

        End Try
    End Function


End Class
