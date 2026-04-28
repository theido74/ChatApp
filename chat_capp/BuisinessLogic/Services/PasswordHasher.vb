Imports System.Security.Cryptography
Imports System.Text

Public Class PasswordHasher
	'Salt = Empêche deux utilisateurs avec le même mot de passe d’avoir le même hash
    Private Const SALT_SIZE As Integer = 16
    'Itérations = hash(hash(hash( - Ajoute de la complexité au Hash pour plus de sécurité 
    Private Const HASH_ITERATIONS As Integer = 1000

    'Hache un mot de passe avec un salt aléatoire (PBKDF2-SHA256) PBKDF2 = S'occupe du Hash + salt + itérations
    'Paramètre de la fonction = mot de passe en clair
    'Retourne String (base64)
    'Base64 pratique pour le stockage car uniquement du texte pas de risque d'erreur

    Public Shared Function HashMotdePasse(motDePasse As String) As String
        'Génération d'un salt aléatoire / Byte() = tableau de byte, liste de nombre de 0 à 255 - Type de variable
        Dim saltBytes As Byte() = New Byte(SALT_SIZE - 1) {}
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(saltBytes)
        End Using
        ' Utiliser PBKDF2 (meilleur que simple SHA-256)
        Using pbkdf2 As New Rfc2898DeriveBytes(motDePasse, saltBytes, HASH_ITERATIONS, HashAlgorithmName.SHA256)
            Dim hashBytes As Byte() = pbkdf2.GetBytes(32) ' 32 bytes = 256 bits

            ' Combiner salt + hash et encoder en base64 / {} = instancier le tableau, avec -1 car VB.net créer toujours un tableau n+1
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

            'Extraire le SALT le premier octet du tableau
            Dim saltbytes As Byte() = New Byte(SALT_SIZE - 1) {}
            Buffer.BlockCopy(hashByte, 0, saltbytes, 0, SALT_SIZE)

            'Extraire le hash Stocké reste du tableau après extraction du salt
            Dim storedHashBytes As Byte() = New Byte(hashByte.Length - SALT_SIZE - 1) {}
            Buffer.BlockCopy(hashByte, SALT_SIZE, storedHashBytes, 0, storedHashBytes.Length)


            'Hacher le password saisi avec le MÊME salt
            Using pbkdf2 As New Rfc2898DeriveBytes(motdepasse, saltbytes, HASH_ITERATIONS, HashAlgorithmName.SHA256)
                Dim computedHashBytes As Byte() = pbkdf2.GetBytes(32)
                'Comparer les deux hashs byte-by-byte
                Return CompareHashes(storedHashBytes, computedHashBytes)
            End Using
        Catch ex As Exception
            ' Si erreur de décodage, le password est invalide
            Return False
        End Try
    End Function
    Private Shared Function CompareHashes(hash1 As Byte(), hash2 As Byte()) As Boolean
        If hash1.Length <> hash2.Length Then
            Return False
        End If

        Dim result As Integer = 0
        For i As Integer = 0 To hash1.Length - 1
            ' XOR compares sans court-circuiter
            result = result Or (hash1(i) Xor hash2(i))
        Next

        Return result = 0
    End Function


End Class
