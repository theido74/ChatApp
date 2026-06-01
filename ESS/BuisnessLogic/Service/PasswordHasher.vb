Imports System.Security.Cryptography
Imports System.Text

Public Class PasswordHasher
    Private Const SALT_SIZE As Integer = 16
    Private Const HASH_ITERATIONS As Integer = 1000

    'Hache un mot de passe avec un salt aléatoire (PBKDF2-SHA256) PBKDF2 = S'occupe du Hash + salt + itérations
    'Paramètre de la fonction = mot de passe en clair
    'Retourne String (base64)
    'Base64 pratique pour le stockage car uniquement du texte pas de risque d'erreur

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Hache un mot de passe avec un salt aléatoire (PBKDF2-SHA256) PBKDF2 = S'occupe du Hash + salt + itérations
    ''' Paramètre de la fonction = mot de passe en clair
    ''' Retourne String (base64)
    ''' Base64 pratique pour le stockage car uniquement du texte pas de risque d'erreur
    ''' </summary>
    ''' <param name="motDePasse"></param>
    ''' <returns>String HashedMotdePasse</returns>
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

    ''' <summary>
    ''' Vérifie qu'un mot de passe en clair correspond à un hash PBKDF2 stocké.
    ''' Le hash fourni doit être encodé en Base64 et contenir le SALT suivi du hash.
    ''' </summary>
    ''' <param name="motdepasse">Mot de passe saisi par l'utilisateur.</param>
    ''' <param name="hash">Hash stocké en Base64 (SALT + hash PBKDF2).</param>
    ''' <returns>
    ''' True si le mot de passe correspond au hash stocké ;
    ''' False si la vérification échoue ou en cas d'erreur.
    ''' </returns>
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


            Using pbkdf2 As New Rfc2898DeriveBytes(motdepasse, saltbytes, HASH_ITERATIONS, HashAlgorithmName.SHA256)
                Dim computedHashBytes As Byte() = pbkdf2.GetBytes(32)
                Return CompareHashes(storedHashBytes, computedHashBytes)
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Compare deux tableaux d'octets représentant des hash cryptographiques.
    ''' La comparaison est effectuée en temps constant afin de limiter les
    ''' attaques par analyse temporelle (timing attacks).
    ''' </summary>
    ''' <param name="hash1">Premier hash à comparer.</param>
    ''' <param name="hash2">Second hash à comparer.</param>
    ''' <returns>
    ''' True si les deux hash sont identiques ;
    ''' False dans le cas contraire.
    ''' </returns>
    Private Shared Function CompareHashes(hash1 As Byte(), hash2 As Byte()) As Boolean
        If hash1.Length <> hash2.Length Then
            Return False
        End If

        Dim result As Integer = 0
        For i As Integer = 0 To hash1.Length - 1
            result = result Or (hash1(i) Xor hash2(i))
        Next

        Return result = 0
    End Function

End Class

