# 📋 Plan Détaillé Semaine 2 - Authentification & Interface Login

## Vue d'ensemble du projet
- **Projet**: ChatApp - Application VB.NET & Oracle SQL
- **Objectif Semaine 2**: Implémenter le login sécurisé et créer la première interface utilisateur
- **Équipe**: 3 développeurs (2 full-time + 1 part-time QA/Tests)
- **Durée**: 4 heures de session + 20-40h de travail total
- **Statut Prérequis**: Semaine 1 complétée (BD + Architecture 3 couches)

---

## 🎯 Objectifs Semaine 2

1. ✅ Implémenter le hachage sécurisé des mots de passe (SHA-256)
2. ✅ Créer les procédures stockées d'authentification Oracle
3. ✅ Construire l'interface Login (LoginForm)
4. ✅ Implémenter la gestion des sessions utilisateur
5. ✅ Créer le MainForm (menu de navigation)
6. ✅ Tester tous les scénarios de connexion/déconnexion

---

## 👥 Répartition des rôles

### Développeur A (Full-time) - Backend & Authentification
**Responsabilités**:
- Implémenter le hachage SHA-256 des mots de passe
- Créer les procédures stockées Oracle
- Implémenter AuthenticationService
- Validation côté service des entrées

### Développeur B (Full-time) - Frontend & Interface
**Responsabilités**:
- Créer les formulaires WinForms (LoginForm, MainForm)
- Implémenter la validation côté client
- Créer le menu de navigation
- Gérer l'expérience utilisateur

### Développeur C (Part-time, 50%) - QA/Tests
**Responsabilités**:
- Tests unitaires pour AuthenticationService
- Tests d'intégration login/BD
- Tests de sécurité (injection SQL, brute-force)
- Documentation des test cases
- Gestion de la qualité

---

## 📊 Tâches Détaillées par Développeur

### PHASE 1: Backend - Authentification (Dev A)

#### Tâche 1.1: Implémenter le hachage SHA-256
- Créer la classe `PasswordHasher.vb` dans la couche Métier
- Implémenter:
  - `HashPassword(password As String) -> String` (avec salt)
  - `VerifyPassword(password As String, hash As String) -> Boolean`
- Utiliser `System.Security.Cryptography`
- **Livrables**: Classe testée avec unit tests
- **Dépendances**: Aucune
- **Durée estimée**: 2-3h

**Code Exemple**:
```vb
' PasswordHasher.vb - COUCHE MÉTIER
Imports System.Security.Cryptography
Imports System.Text

Public Class PasswordHasher
    ' Taille du salt (16 bytes = 128 bits)
    Private Const SALT_SIZE As Integer = 16
    ' Nombre d'itérations du PBKDF2
    Private Const HASH_ITERATIONS As Integer = 10000
    
    ''' <summary>
    ''' Hache un mot de passe avec un salt aléatoire (PBKDF2-SHA256)
    ''' </summary>
    ''' <param name="password">Mot de passe en clair</param>
    ''' <returns>String contenant salt + hash (base64)</returns>
    Public Shared Function HashPassword(password As String) As String
        ' Générer un salt aléatoire
        Dim saltBytes As Byte() = New Byte(SALT_SIZE - 1) {}
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(saltBytes)
        End Using
        
        ' Utiliser PBKDF2 (meilleur que simple SHA-256)
        Using pbkdf2 As New Rfc2898DeriveBytes(password, saltBytes, HASH_ITERATIONS, HashAlgorithmName.SHA256)
            Dim hashBytes As Byte() = pbkdf2.GetBytes(32) ' 32 bytes = 256 bits
            
            ' Combiner salt + hash et encoder en base64
            Dim combinedBytes As Byte() = New Byte(saltBytes.Length + hashBytes.Length - 1) {}
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length)
            Buffer.BlockCopy(hashBytes, 0, combinedBytes, saltBytes.Length, hashBytes.Length)
            
            Return Convert.ToBase64String(combinedBytes)
        End Using
    End Function
    
    ''' <summary>
    ''' Vérifie qu'un mot de passe correspond à son hash
    ''' </summary>
    ''' <param name="password">Mot de passe à vérifier</param>
    ''' <param name="hash">Hash stocké en BD</param>
    ''' <returns>True si le password correspond, False sinon</returns>
    Public Shared Function VerifyPassword(password As String, hash As String) As Boolean
        Try
            ' Décoder le hash base64
            Dim hashBytes As Byte() = Convert.FromBase64String(hash)
            
            ' Extraire le salt (premiers SALT_SIZE bytes)
            Dim saltBytes As Byte() = New Byte(SALT_SIZE - 1) {}
            Buffer.BlockCopy(hashBytes, 0, saltBytes, 0, SALT_SIZE)
            
            ' Extraire le hash stocké (reste des bytes)
            Dim storedHashBytes As Byte() = New Byte(hashBytes.Length - SALT_SIZE - 1) {}
            Buffer.BlockCopy(hashBytes, SALT_SIZE, storedHashBytes, 0, storedHashBytes.Length)
            
            ' Hacher le password saisi avec le MÊME salt
            Using pbkdf2 As New Rfc2898DeriveBytes(password, saltBytes, HASH_ITERATIONS, HashAlgorithmName.SHA256)
                Dim computedHashBytes As Byte() = pbkdf2.GetBytes(32)
                
                ' Comparer les deux hashs byte-by-byte
                Return CompareHashes(storedHashBytes, computedHashBytes)
            End Using
        Catch ex As Exception
            ' Si erreur de décodage, le password est invalide
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Comparaison sécurisée de deux hashs (résiste aux timing attacks)
    ''' </summary>
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
```

**Tests Unitaires**:
```vb
' PasswordHasherTests.vb
<TestFixture>
Public Class PasswordHasherTests
    <Test>
    Public Sub HashPassword_ReturnsDifferentHashEachTime()
        ' Arrange
        Dim password As String = "MySecurePassword123!"
        
        ' Act
        Dim hash1 As String = PasswordHasher.HashPassword(password)
        Dim hash2 As String = PasswordHasher.HashPassword(password)
        
        ' Assert - Les hashs doivent être différents (salts différents)
        Assert.AreNotEqual(hash1, hash2, "Chaque hash doit avoir un salt unique")
    End Sub
    
    <Test>
    Public Sub VerifyPassword_WithCorrectPassword_ReturnsTrue()
        ' Arrange
        Dim password As String = "MySecurePassword123!"
        Dim hash As String = PasswordHasher.HashPassword(password)
        
        ' Act
        Dim result As Boolean = PasswordHasher.VerifyPassword(password, hash)
        
        ' Assert
        Assert.IsTrue(result, "Le bon password doit être accepté")
    End Sub
    
    <Test>
    Public Sub VerifyPassword_WithWrongPassword_ReturnsFalse()
        ' Arrange
        Dim correctPassword As String = "MySecurePassword123!"
        Dim wrongPassword As String = "WrongPassword456!"
        Dim hash As String = PasswordHasher.HashPassword(correctPassword)
        
        ' Act
        Dim result As Boolean = PasswordHasher.VerifyPassword(wrongPassword, hash)
        
        ' Assert
        Assert.IsFalse(result, "Un mauvais password doit être rejeté")
    End Sub
End Class
```

#### Tâche 1.2: Créer les procédures stockées Oracle
- Procédure `sp_AuthenticateUser(p_username, p_password_hash, p_session_id)`
  - Valider l'utilisateur
  - Créer une session active
  - Retourner UserID + SessionID
- Procédure `sp_ValidateSession(p_session_id)` 
  - Vérifier si la session est active
  - Mettre à jour LastActivityTime
  - Retourner UserID ou NULL
- Procédure `sp_LogoutUser(p_session_id)`
  - Marquer la session comme inactive
  - Loger la déconnexion
- **Livrables**: Procédures testées dans Oracle
- **Dépendances**: Tables BD de Semaine 1 (Users, UserSessions)
- **Durée estimée**: 2-3h

**Code SQL Oracle**:
```sql
-- ============================================
-- Procédure 1: sp_AuthenticateUser
-- Valide l'utilisateur et crée une session
-- ============================================
CREATE OR REPLACE PROCEDURE sp_AuthenticateUser(
    p_username IN VARCHAR2,
    p_password_hash IN VARCHAR2,
    p_session_id OUT VARCHAR2,
    p_user_id OUT NUMBER,
    p_result OUT VARCHAR2
)
IS
    v_stored_hash VARCHAR2(255);
    v_user_id NUMBER;
BEGIN
    p_result := 'ERROR';
    p_user_id := NULL;
    p_session_id := NULL;
    
    -- 1. Chercher l'utilisateur par username
    SELECT UserID, PasswordHash 
    INTO v_user_id, v_stored_hash
    FROM Users 
    WHERE Username = p_username AND IsActive = 1;
    
    -- 2. Vérifier que le password correspond
    IF v_stored_hash != p_password_hash THEN
        -- Log tentative échouée
        INSERT INTO Logs(UserID, Action, Timestamp, Details)
        VALUES(NULL, 'LOGIN_FAILED', SYSDATE, 'Username: ' || p_username);
        COMMIT;
        RETURN;
    END IF;
    
    -- 3. Créer une nouvelle session
    p_session_id := LOWER(SYS_GUID()); -- UUID unique
    
    INSERT INTO UserSessions(SessionID, UserID, LoginTime, LastActivityTime, IsActive)
    VALUES(p_session_id, v_user_id, SYSDATE, SYSDATE, 1);
    
    -- 4. Log succès
    INSERT INTO Logs(UserID, Action, Timestamp, Details)
    VALUES(v_user_id, 'LOGIN_SUCCESS', SYSDATE, 'SessionID: ' || p_session_id);
    
    p_user_id := v_user_id;
    p_result := 'SUCCESS';
    COMMIT;
    
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        -- Utilisateur pas trouvé
        p_result := 'USER_NOT_FOUND';
    WHEN OTHERS THEN
        p_result := 'ERROR: ' || SQLERRM;
        ROLLBACK;
END sp_AuthenticateUser;
/

-- ============================================
-- Procédure 2: sp_ValidateSession
-- Vérifie qu'une session est valide (pas expirée)
-- ============================================
CREATE OR REPLACE PROCEDURE sp_ValidateSession(
    p_session_id IN VARCHAR2,
    p_user_id OUT NUMBER,
    p_is_valid OUT NUMBER
)
IS
    v_last_activity DATE;
    v_hours_ago NUMBER;
BEGIN
    p_user_id := NULL;
    p_is_valid := 0;
    
    -- Chercher la session
    SELECT UserID, LastActivityTime
    INTO p_user_id, v_last_activity
    FROM UserSessions
    WHERE SessionID = p_session_id AND IsActive = 1;
    
    -- Calculer temps écoulé depuis dernière activité
    v_hours_ago := (SYSDATE - v_last_activity) * 24;
    
    -- Vérifier timeout (1 heure = 3600 secondes)
    IF v_hours_ago > 1 THEN
        -- Session expirée
        UPDATE UserSessions
        SET IsActive = 0
        WHERE SessionID = p_session_id;
        COMMIT;
        p_is_valid := 0;
    ELSE
        -- Mettre à jour LastActivityTime
        UPDATE UserSessions
        SET LastActivityTime = SYSDATE
        WHERE SessionID = p_session_id;
        COMMIT;
        p_is_valid := 1;
    END IF;
    
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        -- Session pas trouvée ou inactive
        p_user_id := NULL;
        p_is_valid := 0;
    WHEN OTHERS THEN
        p_user_id := NULL;
        p_is_valid := 0;
END sp_ValidateSession;
/

-- ============================================
-- Procédure 3: sp_LogoutUser
-- Marque une session comme inactive
-- ============================================
CREATE OR REPLACE PROCEDURE sp_LogoutUser(
    p_session_id IN VARCHAR2,
    p_result OUT VARCHAR2
)
IS
    v_user_id NUMBER;
BEGIN
    p_result := 'ERROR';
    
    -- Trouver l'utilisateur
    SELECT UserID INTO v_user_id
    FROM UserSessions
    WHERE SessionID = p_session_id AND IsActive = 1;
    
    -- Désactiver la session
    UPDATE UserSessions
    SET IsActive = 0
    WHERE SessionID = p_session_id;
    
    -- Logger la déconnexion
    INSERT INTO Logs(UserID, Action, Timestamp, Details)
    VALUES(v_user_id, 'LOGOUT', SYSDATE, 'SessionID: ' || p_session_id);
    
    COMMIT;
    p_result := 'SUCCESS';
    
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        p_result := 'SESSION_NOT_FOUND';
    WHEN OTHERS THEN
        p_result := 'ERROR: ' || SQLERRM;
        ROLLBACK;
END sp_LogoutUser;
/
```

#### Tâche 1.3: Implémenter AuthenticationService
- Créer `AuthenticationService.vb` dans la couche Métier
- Méthodes:
  - `Authenticate(username As String, password As String) -> SessionInfo`
  - `ValidateSession(sessionId As String) -> Boolean`
  - `Logout(sessionId As String) -> Boolean`
  - `ChangePassword(userId As String, oldPassword As String, newPassword As String) -> Boolean`
- Appeler les procédures stockées via DataAccess
- Gestion des erreurs complète
- **Livrables**: Service intégré avec authentification fonctionnelle
- **Dépendances**: Tâche 1.1 (PasswordHasher), Tâche 1.2 (Stored Procs)
- **Durée estimée**: 3h

**Code Exemple**:
```vb
' AuthenticationService.vb - COUCHE MÉTIER
Public Class AuthenticationService
    Private dbAccess As New UserDataAccess()
    Private passwordHasher As New PasswordHasher()
    
    ''' <summary>
    ''' Authentifie un utilisateur et crée une session
    ''' </summary>
    Public Function Authenticate(username As String, password As String) As SessionInfo
        ' Validation des entrées
        Dim inputValidator As New InputValidator()
        If Not inputValidator.ValidateUsername(username) Then
            Throw New ArgumentException("Username invalide (4-20 caractères)")
        End If
        
        If Not inputValidator.ValidatePassword(password) Then
            Throw New ArgumentException("Password invalide (8-50 caractères)")
        End If
        
        ' Hacher le password saisi
        Dim passwordHash As String = passwordHasher.HashPassword(password)
        
        Try
            ' Appeler la procédure stockée
            Dim sessionInfo As New SessionInfo()
            dbAccess.CallAuthenticateUserProcedure(username, passwordHash, sessionInfo)
            
            If sessionInfo.Result = "SUCCESS" Then
                ' Créer objet session
                Dim session As New SessionInfo With {
                    .UserID = sessionInfo.UserID,
                    .SessionID = sessionInfo.SessionID,
                    .LoginTime = Now,
                    .IsValid = True
                }
                Return session
            Else
                ' Login échoué
                Throw New UnauthorizedAccessException("Nom d'utilisateur ou mot de passe incorrect")
            End If
        Catch ex As Exception
            ' Logger l'erreur sans révéler les détails
            LogError("Authenticate", ex)
            Throw New Exception("Erreur lors de l'authentification")
        End Try
    End Function
    
    ''' <summary>
    ''' Valide qu'une session est toujours active
    ''' </summary>
    Public Function ValidateSession(sessionId As String) As Boolean
        If String.IsNullOrEmpty(sessionId) Then
            Return False
        End If
        
        Try
            Dim isValid As Integer = dbAccess.CallValidateSessionProcedure(sessionId)
            Return isValid = 1
        Catch ex As Exception
            LogError("ValidateSession", ex)
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Déconnecte un utilisateur
    ''' </summary>
    Public Function Logout(sessionId As String) As Boolean
        If String.IsNullOrEmpty(sessionId) Then
            Return False
        End If
        
        Try
            Dim result As String = dbAccess.CallLogoutUserProcedure(sessionId)
            Return result = "SUCCESS"
        Catch ex As Exception
            LogError("Logout", ex)
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Change le mot de passe d'un utilisateur
    ''' </summary>
    Public Function ChangePassword(userId As Integer, oldPassword As String, newPassword As String) As Boolean
        ' Validation
        If String.IsNullOrEmpty(oldPassword) OrElse String.IsNullOrEmpty(newPassword) Then
            Throw New ArgumentException("Passwords requis")
        End If
        
        If oldPassword = newPassword Then
            Throw New ArgumentException("Nouveau password doit être différent")
        End If
        
        Try
            ' Récupérer l'utilisateur
            Dim user As User = dbAccess.GetUserByID(userId)
            
            ' Vérifier l'ancien password
            If Not passwordHasher.VerifyPassword(oldPassword, user.PasswordHash) Then
                Throw New UnauthorizedAccessException("Ancien password incorrect")
            End If
            
            ' Hacher le nouveau password
            Dim newHash As String = passwordHasher.HashPassword(newPassword)
            
            ' Mettre à jour
            dbAccess.UpdateUserPassword(userId, newHash)
            
            ' Logger
            LogInfo("ChangePassword", $"User {userId} changed password")
            
            Return True
        Catch ex As Exception
            LogError("ChangePassword", ex)
            Return False
        End Try
    End Function
    
    Private Sub LogError(method As String, ex As Exception)
        ' À implémenter avec la couche de logging
        System.Diagnostics.Debug.WriteLine($"[ERROR] {method}: {ex.Message}")
    End Sub
    
    Private Sub LogInfo(method As String, message As String)
        System.Diagnostics.Debug.WriteLine($"[INFO] {method}: {message}")
    End Sub
End Class

''' <summary>
''' Classe pour transporter les infos de session
''' </summary>
Public Class SessionInfo
    Public Property UserID As Integer
    Public Property SessionID As String
    Public Property LoginTime As DateTime
    Public Property IsValid As Boolean
    Public Property Result As String ' SUCCESS, USER_NOT_FOUND, ERROR
End Class
```

#### Tâche 1.4: Valider les entrées (anti-injection SQL)
- Créer classe `InputValidator.vb`
- Valider:
  - Longueur username (4-20 caractères)
  - Longueur password (8-50 caractères)
  - Caractères autorisés (alphanumériques + caractères spéciaux sûrs)
  - Absence de commandes SQL malveillantes
- **Livrables**: Validateur réutilisable et testé
- **Dépendances**: Aucune
- **Durée estimée**: 2h

**Code Exemple**:
```vb
' InputValidator.vb - COUCHE MÉTIER (Validation serveur)
Public Class InputValidator
    
    ''' <summary>
    ''' Valide qu'un username respecte les règles
    ''' </summary>
    Public Function ValidateUsername(username As String) As Boolean
        If String.IsNullOrWhiteSpace(username) Then
            Return False
        End If
        
        ' Longueur 4-20 caractères
        If username.Length < 4 OrElse username.Length > 20 Then
            Return False
        End If
        
        ' Alphanumériques + underscore seulement
        Dim pattern As String = "^[a-zA-Z0-9_]+$"
        Return System.Text.RegularExpressions.Regex.IsMatch(username, pattern)
    End Function
    
    ''' <summary>
    ''' Valide qu'un password respecte les règles
    ''' </summary>
    Public Function ValidatePassword(password As String) As Boolean
        If String.IsNullOrEmpty(password) Then
            Return False
        End If
        
        ' Longueur 8-50 caractères
        If password.Length < 8 OrElse password.Length > 50 Then
            Return False
        End If
        
        ' Vérifier au moins:
        ' - 1 majuscule
        ' - 1 minuscule
        ' - 1 chiffre
        ' - 1 caractère spécial sûr
        
        Dim hasUpper As Boolean = System.Text.RegularExpressions.Regex.IsMatch(password, "[A-Z]")
        Dim hasLower As Boolean = System.Text.RegularExpressions.Regex.IsMatch(password, "[a-z]")
        Dim hasDigit As Boolean = System.Text.RegularExpressions.Regex.IsMatch(password, "[0-9]")
        Dim hasSpecial As Boolean = System.Text.RegularExpressions.Regex.IsMatch(password, "[@#$%]")
        
        Return hasUpper AndAlso hasLower AndAlso hasDigit AndAlso hasSpecial
    End Function
    
    ''' <summary>
    ''' Détecte les tentatives d'injection SQL
    ''' </summary>
    Public Function IsSQLInjectionAttempt(input As String) As Boolean
        If String.IsNullOrEmpty(input) Then
            Return False
        End If
        
        ' Patterns dangereux
        Dim sqlPatterns() As String = {
            "' OR '1'='1",
            "'; DROP TABLE",
            "' OR 1=1",
            "' UNION SELECT",
            "' AND 1=1",
            "--",
            "/*",
            "*/",
            "xp_",
            "sp_"
        }
        
        Dim upperInput As String = input.ToUpper()
        For Each pattern In sqlPatterns
            If upperInput.Contains(pattern.ToUpper()) Then
                Return True
            End If
        Next
        
        Return False
    End Function
    
    ''' <summary>
    ''' Nettoie et échappe les entrées dangeriques
    ''' </summary>
    Public Function SanitizeInput(input As String) As String
        If String.IsNullOrEmpty(input) Then
            Return ""
        End If
        
        ' Supprimer espaces avant/après
        Dim result As String = input.Trim()
        
        ' Remplacer les apostrophes par des espaces (pas de suppression complète)
        result = result.Replace("'", " ")
        
        ' Limiter la longueur
        If result.Length > 100 Then
            result = result.Substring(0, 100)
        End If
        
        Return result
    End Function
    
    ''' <summary>
    ''' Valide un email
    ''' </summary>
    Public Function ValidateEmail(email As String) As Boolean
        If String.IsNullOrEmpty(email) Then
            Return False
        End If
        
        Try
            Dim addr As New System.Net.Mail.MailAddress(email)
            Return True
        Catch
            Return False
        End Try
    End Function
End Class

' TESTS UNITAIRES - InputValidatorTests.vb
<TestFixture>
Public Class InputValidatorTests
    Private validator As InputValidator
    
    <SetUp>
    Public Sub Setup()
        validator = New InputValidator()
    End Sub
    
    <Test>
    Public Sub ValidateUsername_ValidName_ReturnsTrue()
        ' Arrange
        Dim username As String = "john_doe"
        
        ' Act
        Dim result As Boolean = validator.ValidateUsername(username)
        
        ' Assert
        Assert.IsTrue(result)
    End Sub
    
    <Test>
    Public Sub ValidateUsername_TooShort_ReturnsFalse()
        Dim result As Boolean = validator.ValidateUsername("abc")
        Assert.IsFalse(result)
    End Sub
    
    <Test>
    Public Sub ValidateUsername_WithSpecialChars_ReturnsFalse()
        Dim result As Boolean = validator.ValidateUsername("john@doe")
        Assert.IsFalse(result)
    End Sub
    
    <Test>
    Public Sub IsSQLInjectionAttempt_MaliciousInput_ReturnsTrue()
        Dim result As Boolean = validator.IsSQLInjectionAttempt("' OR '1'='1")
        Assert.IsTrue(result)
    End Sub
    
    <Test>
    Public Sub IsSQLInjectionAttempt_ValidInput_ReturnsFalse()
        Dim result As Boolean = validator.IsSQLInjectionAttempt("mypassword123")
        Assert.IsFalse(result)
    End Sub
End Class
```

---

### PHASE 2: Frontend - Interface Login (Dev B)

#### Tâche 2.1: Créer le formulaire LoginForm
- Créer `LoginForm.vb` (formulaire WinForms)
- Contrôles:
  - TextBox pour Username
  - TextBox (PasswordChar = "*") pour Password
  - Button "Se connecter"
  - Button "Quitter"
  - Label pour les erreurs/messages
  - Link "Mot de passe oublié ?" (désactivé pour Semaine 2)
- Événements:
  - Click sur "Se connecter": Valider + Appeler AuthenticationService
  - Enter dans Password: Activer connexion (Enter key)
  - "Quitter": Fermer l'application
- Validation côté client avant envoi
- **Livrables**: Formulaire fonctionnel avec validation
- **Dépendances**: Tâche 1.3 (AuthenticationService)
- **Durée estimée**: 2-3h

**Code Exemple**:
```vb
' LoginForm.vb - COUCHE PRÉSENTATION (Windows Forms)
Public Class LoginForm
    Inherits Form
    
    ' Contrôles UI
    Private WithEvents btnLogin As New Button
    Private WithEvents btnQuit As New Button
    Private WithEvents txtUsername As New TextBox
    Private WithEvents txtPassword As New TextBox
    Private WithEvents lblMessage As New Label
    Private lblUsernameLabel As New Label
    Private lblPasswordLabel As New Label
    
    ' Services
    Private authService As New AuthenticationService()
    Private clientValidator As New ClientValidator()
    
    Public Sub New()
        InitializeComponent()
    End Sub
    
    Private Sub InitializeComponent()
        ' Configuration du formulaire
        Me.Text = "ChatApp - Connexion"
        Me.Width = 400
        Me.Height = 300
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        
        ' Label Username
        lblUsernameLabel.Text = "Nom d'utilisateur:"
        lblUsernameLabel.Location = New Point(20, 20)
        lblUsernameLabel.AutoSize = True
        Me.Controls.Add(lblUsernameLabel)
        
        ' TextBox Username
        txtUsername.Location = New Point(20, 40)
        txtUsername.Width = 340
        txtUsername.Height = 25
        Me.Controls.Add(txtUsername)
        
        ' Label Password
        lblPasswordLabel.Text = "Mot de passe:"
        lblPasswordLabel.Location = New Point(20, 75)
        lblPasswordLabel.AutoSize = True
        Me.Controls.Add(lblPasswordLabel)
        
        ' TextBox Password
        txtPassword.Location = New Point(20, 95)
        txtPassword.Width = 340
        txtPassword.Height = 25
        txtPassword.PasswordChar = "*"c
        Me.Controls.Add(txtPassword)
        
        ' Label Message (erreurs)
        lblMessage.Location = New Point(20, 130)
        lblMessage.Width = 340
        lblMessage.Height = 40
        lblMessage.ForeColor = Color.Red
        lblMessage.AutoSize = False
        Me.Controls.Add(lblMessage)
        
        ' Button Se Connecter
        btnLogin.Text = "Se connecter"
        btnLogin.Location = New Point(150, 180)
        btnLogin.Width = 100
        Me.Controls.Add(btnLogin)
        
        ' Button Quitter
        btnQuit.Text = "Quitter"
        btnQuit.Location = New Point(260, 180)
        btnQuit.Width = 100
        Me.Controls.Add(btnQuit)
    End Sub
    
    ''' <summary>
    ''' Événement Login (clic bouton ou Enter en password)
    ''' </summary>
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        PerformLogin()
    End Sub
    
    ''' <summary>
    ''' Permettre Enter dans password pour lancer login
    ''' </summary>
    Private Sub TxtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Chr(13) Then ' Touche Enter
            e.Handled = True
            PerformLogin()
        End If
    End Sub
    
    ''' <summary>
    ''' Logique de login
    ''' </summary>
    Private Sub PerformLogin()
        ' Récupérer les valeurs
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        
        ' Valider côté client d'abord
        If Not clientValidator.ValidateUsername(username) Then
            lblMessage.Text = "Nom d'utilisateur invalide (4-20 caractères)"
            Return
        End If
        
        If Not clientValidator.ValidatePassword(password) Then
            lblMessage.Text = "Mot de passe invalide (8-50 caractères)"
            Return
        End If
        
        ' Appeler le service d'authentification
        Try
            Dim sessionInfo As SessionInfo = authService.Authenticate(username, password)
            
            If sessionInfo.IsValid Then
                ' Login réussi - ouvrir MainForm
                lblMessage.Text = "Connexion réussie..."
                
                ' Créer la MainForm avec la session
                Dim mainForm As New MainForm(sessionInfo)
                Me.Hide()
                mainForm.Show()
            End If
        Catch ex As UnauthorizedAccessException
            lblMessage.Text = ex.Message
            txtPassword.Clear()
        Catch ex As Exception
            lblMessage.Text = "Erreur lors de la connexion"
            ' Ne pas révéler les détails à l'utilisateur
        End Try
    End Sub
    
    ''' <summary>
    ''' Événement Quitter
    ''' </summary>
    Private Sub BtnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Application.Exit()
    End Sub
End Class
```

#### Tâche 2.2: Créer le MainForm (écran après connexion)
- Créer `MainForm.vb` (formulaire principal)
- Structure:
  - MenuStrip avec options:
    - Forum
    - Messages Privés
    - Profil / Paramètres
    - Déconnexion
    - Quitter
  - Barre de statut (utilisateur connecté, heure)
  - Panneau d'accueil (bienvenue)
- Afficher le nom de l'utilisateur connecté
- Bouton Déconnexion (revenir à LoginForm)
- **Livrables**: MainForm navigable (pas d'implémentation du forum/messages pour Semaine 2)
- **Dépendances**: Tâche 2.1 (LoginForm), Tâche 1.3 (AuthenticationService)
- **Durée estimée**: 2h

**Code Exemple**:
```vb
' MainForm.vb - COUCHE PRÉSENTATION
Public Class MainForm
    Inherits Form
    
    Private sessionInfo As SessionInfo
    Private authService As New AuthenticationService()
    
    ' Contrôles
    Private WithEvents menuStrip As New MenuStrip
    Private WithEvents statusStrip As New StatusStrip
    Private lblWelcome As New Label
    Private WithEvents lblTime As New Label
    Private timer As New Timer()
    
    Public Sub New(session As SessionInfo)
        sessionInfo = session
        InitializeComponent()
    End Sub
    
    Private Sub InitializeComponent()
        ' Configuration du formulaire
        Me.Text = "ChatApp - Menu Principal"
        Me.Width = 800
        Me.Height = 600
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.Sizable
        
        ' === MENU STRIP ===
        Dim menuForum As New ToolStripMenuItem("&Forum")
        menuForum.Click += AddressOf MenuForum_Click
        
        Dim menuMessages As New ToolStripMenuItem("&Messages Privés")
        menuMessages.Click += AddressOf MenuMessages_Click
        
        Dim menuProfile As New ToolStripMenuItem("&Profil")
        menuProfile.Click += AddressOf MenuProfile_Click
        
        Dim menuLogout As New ToolStripMenuItem("&Déconnexion")
        menuLogout.Click += AddressOf MenuLogout_Click
        
        Dim menuQuit As New ToolStripMenuItem("&Quitter")
        menuQuit.Click += AddressOf MenuQuit_Click
        
        menuStrip.Items.AddRange({menuForum, menuMessages, menuProfile, menuLogout, menuQuit})
        Me.MainMenuStrip = menuStrip
        Me.Controls.Add(menuStrip)
        
        ' === STATUS STRIP ===
        Dim statusLabel As New ToolStripStatusLabel(
            $"Connecté: {sessionInfo.UserID} | SessionID: {sessionInfo.SessionID.Substring(0, 8)}...")
        lblTime = New ToolStripStatusLabel()
        lblTime.Text = Now.ToString("HH:mm:ss")
        
        statusStrip.Items.AddRange({statusLabel, New ToolStripStatusLabel() With {.Spring = True}, lblTime})
        Me.Controls.Add(statusStrip)
        
        ' === PANEL ACCUEIL ===
        lblWelcome = New Label With {
            .Text = $"Bienvenue, Utilisateur #{sessionInfo.UserID}!" & vbCrLf & 
                    $"Vous êtes connecté depuis: {sessionInfo.LoginTime:G}",
            .Location = New Point(50, 50),
            .Width = 300,
            .Height = 100,
            .Font = New Font("Arial", 12)
        }
        Me.Controls.Add(lblWelcome)
        
        ' === TIMER POUR HEURE ===
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Interval = 1000
        timer.Start()
        
        ' Valider la session au démarrage
        ValidateSessionOnLoad()
    End Sub
    
    Private Sub ValidateSessionOnLoad()
        If Not authService.ValidateSession(sessionInfo.SessionID) Then
            MessageBox.Show("Session expirée. Reconnectez-vous.", "Erreur", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ReturnToLogin()
        End If
    End Sub
    
    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        lblTime.Text = Now.ToString("HH:mm:ss")
        
        ' Valider la session toutes les minutes
        If Now.Minute Mod 5 = 0 Then
            If Not authService.ValidateSession(sessionInfo.SessionID) Then
                MessageBox.Show("Session expirée.", "Avertissement")
                ReturnToLogin()
            End If
        End If
    End Sub
    
    Private Sub MenuForum_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Forum - Semaine 3", "À venir")
    End Sub
    
    Private Sub MenuMessages_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Messages Privés - Semaine 3", "À venir")
    End Sub
    
    Private Sub MenuProfile_Click(sender As Object, e As EventArgs)
        MessageBox.Show($"Profil de l'utilisateur #{sessionInfo.UserID}", "Profil")
    End Sub
    
    Private Sub MenuLogout_Click(sender As Object, e As EventArgs)
        If authService.Logout(sessionInfo.SessionID) Then
            timer.Stop()
            ReturnToLogin()
        Else
            MessageBox.Show("Erreur lors de la déconnexion", "Erreur")
        End If
    End Sub
    
    Private Sub MenuQuit_Click(sender As Object, e As EventArgs)
        ' Logout avant de quitter
        authService.Logout(sessionInfo.SessionID)
        timer.Stop()
        Application.Exit()
    End Sub
    
    Private Sub ReturnToLogin()
        Me.Close()
        Dim loginForm As New LoginForm()
        loginForm.Show()
    End Sub
End Class
```

#### Tâche 2.3: Implémenter la validation côté client
- Créer `ClientValidator.vb`
- Valider avant l'envoi au service:
  - Username non vide
  - Password non vide
  - Longueurs respectées
  - Afficher messages d'erreur conviviales
- **Livrables**: Classe réutilisable, intégrée à LoginForm
- **Dépendances**: Aucune
- **Durée estimée**: 1-2h

**Code Exemple**:
```vb
' ClientValidator.vb - COUCHE PRÉSENTATION (Validation côté client)
Public Class ClientValidator
    
    ''' <summary>
    ''' Valide un username côté client avant envoi au serveur
    ''' </summary>
    Public Function ValidateUsername(username As String) As Boolean
        ' Vérifier non-vide
        If String.IsNullOrWhiteSpace(username) Then
            Return False
        End If
        
        ' Vérifier longueur
        If username.Length < 4 OrElse username.Length > 20 Then
            Return False
        End If
        
        Return True
    End Function
    
    ''' <summary>
    ''' Valide un password côté client avant envoi
    ''' </summary>
    Public Function ValidatePassword(password As String) As Boolean
        If String.IsNullOrEmpty(password) Then
            Return False
        End If
        
        ' Longueur minimale 8 caractères
        If password.Length < 8 OrElse password.Length > 50 Then
            Return False
        End If
        
        Return True
    End Function
    
    ''' <summary>
    ''' Retourne un message d'erreur détaillé
    ''' </summary>
    Public Function GetValidationMessage(username As String, password As String) As String
        If String.IsNullOrWhiteSpace(username) Then
            Return "Veuillez entrer un nom d'utilisateur"
        End If
        
        If username.Length < 4 Then
            Return "Le nom d'utilisateur doit contenir au moins 4 caractères"
        End If
        
        If username.Length > 20 Then
            Return "Le nom d'utilisateur ne doit pas dépasser 20 caractères"
        End If
        
        If String.IsNullOrEmpty(password) Then
            Return "Veuillez entrer un mot de passe"
        End If
        
        If password.Length < 8 Then
            Return "Le mot de passe doit contenir au moins 8 caractères"
        End If
        
        If password.Length > 50 Then
            Return "Le mot de passe ne doit pas dépasser 50 caractères"
        End If
        
        Return ""
    End Function
End Class
```

#### Tâche 2.4: Implémenter SessionManager (gestion de session)
- Créer `SessionManager.vb` dans la couche Métier
- Propriétés:
  - CurrentUserID
  - CurrentSessionID
  - CurrentUsername
  - IsLoggedIn
  - SessionStartTime
- Méthodes:
  - StartSession(userId, sessionId, username)
  - EndSession()
  - GetCurrentUser() -> User object
  - CheckSessionTimeout() (1h timeout)
- Singleton pattern (une instance globale)
- **Livrables**: SessionManager intégré et testé
- **Dépendances**: Tâche 1.3 (AuthenticationService)
- **Durée estimée**: 2h

**Code Exemple**:
```vb
' SessionManager.vb - COUCHE MÉTIER (Singleton)
Public Class SessionManager
    ' Instance unique (Singleton)
    Private Shared _instance As SessionManager = Nothing
    Private Shared _lockObj As New Object()
    
    ' Propriétés de session
    Private _currentUserID As Integer = 0
    Private _currentSessionID As String = ""
    Private _currentUsername As String = ""
    Private _sessionStartTime As DateTime = Nothing
    Private _isLoggedIn As Boolean = False
    
    ' Constructeur privé (Singleton)
    Private Sub New()
    End Sub
    
    ''' <summary>
    ''' Récupère l'instance unique de SessionManager
    ''' </summary>
    Public Shared Function GetInstance() As SessionManager
        If _instance Is Nothing Then
            SyncLock _lockObj
                If _instance Is Nothing Then
                    _instance = New SessionManager()
                End If
            End SyncLock
        End If
        Return _instance
    End Function
    
    ''' <summary>
    ''' Démarre une nouvelle session utilisateur
    ''' </summary>
    Public Sub StartSession(userId As Integer, sessionId As String, username As String)
        _currentUserID = userId
        _currentSessionID = sessionId
        _currentUsername = username
        _sessionStartTime = Now
        _isLoggedIn = True
    End Sub
    
    ''' <summary>
    ''' Termine la session actuelle
    ''' </summary>
    Public Sub EndSession()
        _currentUserID = 0
        _currentSessionID = ""
        _currentUsername = ""
        _sessionStartTime = Nothing
        _isLoggedIn = False
    End Sub
    
    ''' <summary>
    ''' Propriétés publiques en lecture seule
    ''' </summary>
    Public ReadOnly Property CurrentUserID As Integer
        Get
            Return _currentUserID
        End Get
    End Property
    
    Public ReadOnly Property CurrentSessionID As String
        Get
            Return _currentSessionID
        End Get
    End Property
    
    Public ReadOnly Property CurrentUsername As String
        Get
            Return _currentUsername
        End Get
    End Property
    
    Public ReadOnly Property IsLoggedIn As Boolean
        Get
            Return _isLoggedIn
        End Get
    End Property
    
    Public ReadOnly Property SessionStartTime As DateTime
        Get
            Return _sessionStartTime
        End Get
    End Property
    
    ''' <summary>
    ''' Vérifie si la session n'a pas expiré (1 heure)
    ''' </summary>
    Public Function CheckSessionTimeout() As Boolean
        If Not _isLoggedIn Then
            Return False
        End If
        
        Dim elapsedTime As TimeSpan = Now - _sessionStartTime
        
        If elapsedTime.TotalHours >= 1 Then
            ' Expirée
            EndSession()
            Return False
        End If
        
        Return True
    End Function
    
    ''' <summary>
    ''' Récupère les infos de session actuelles
    ''' </summary>
    Public Function GetSessionInfo() As SessionInfo
        If Not _isLoggedIn Then
            Return Nothing
        End If
        
        Return New SessionInfo With {
            .UserID = _currentUserID,
            .SessionID = _currentSessionID,
            .LoginTime = _sessionStartTime,
            .IsValid = CheckSessionTimeout()
        }
    End Function
    
    ''' <summary>
    ''' Affiche l'état de la session (pour debug)
    ''' </summary>
    Public Overrides Function ToString() As String
        If Not _isLoggedIn Then
            Return "Aucune session active"
        End If
        
        Return $"SessionManager - UserID: {_currentUserID}, Username: {_currentUsername}, " & 
               $"SessionID: {_currentSessionID.Substring(0, 8)}..., StartTime: {_sessionStartTime:G}"
    End Function
End Class

' UTILISATION
' Dim sessionMgr As SessionManager = SessionManager.GetInstance()
' sessionMgr.StartSession(userId, sessionId, username)
' If sessionMgr.IsLoggedIn Then
'     Debug.WriteLine(sessionMgr.ToString())
' End If
' sessionMgr.EndSession()
```

---

### PHASE 3: QA/Tests (Dev C - Part-time)

#### Tâche 3.1: Tests unitaires - PasswordHasher
- Tester:
  - Hachage correct d'un mot de passe
  - Vérification correcte (password valide)
  - Rejet du mauvais password
  - Salt unique à chaque hachage
- Créer projet Test avec NUnit ou MSTest
- Couvrir les cas d'erreur (null, vide)
- **Livrables**: Test class avec 100% de couverture pour PasswordHasher
- **Dépendances**: Tâche 1.1 (PasswordHasher)
- **Durée estimée**: 1-2h

**Code Exemple**:
```vb
' PasswordHasherTests.vb - TESTS UNITAIRES
<TestFixture>
Public Class PasswordHasherTests
    Private passwordHasher As PasswordHasher
    
    <SetUp>
    Public Sub Setup()
        passwordHasher = New PasswordHasher()
    End Sub
    
    <Test>
    Public Sub HashPassword_ValidPassword_ReturnsHash()
        ' Arrange
        Dim password As String = "MySecurePass@123"
        
        ' Act
        Dim hash As String = passwordHasher.HashPassword(password)
        
        ' Assert
        Assert.IsNotNull(hash)
        Assert.IsNotEmpty(hash)
        Assert.AreNotEqual(password, hash) ' Hash != plaintext
    End Sub
    
    <Test>
    Public Sub HashPassword_SamePasswordTwice_DifferentHashes()
        ' Arrange
        Dim password As String = "MySecurePass@123"
        
        ' Act
        Dim hash1 As String = passwordHasher.HashPassword(password)
        Dim hash2 As String = passwordHasher.HashPassword(password)
        
        ' Assert - Les hashes doivent être différents (salt différent)
        Assert.AreNotEqual(hash1, hash2)
    End Sub
    
    <Test>
    Public Sub VerifyPassword_CorrectPassword_ReturnsTrue()
        ' Arrange
        Dim password As String = "MySecurePass@123"
        Dim hash As String = passwordHasher.HashPassword(password)
        
        ' Act
        Dim result As Boolean = passwordHasher.VerifyPassword(password, hash)
        
        ' Assert
        Assert.IsTrue(result)
    End Sub
    
    <Test>
    Public Sub VerifyPassword_WrongPassword_ReturnsFalse()
        ' Arrange
        Dim correctPassword As String = "MySecurePass@123"
        Dim wrongPassword As String = "WrongPassword@456"
        Dim hash As String = passwordHasher.HashPassword(correctPassword)
        
        ' Act
        Dim result As Boolean = passwordHasher.VerifyPassword(wrongPassword, hash)
        
        ' Assert
        Assert.IsFalse(result)
    End Sub
    
    <Test>
    Public Sub VerifyPassword_EmptyPassword_ReturnsFalse()
        ' Arrange
        Dim password As String = "MySecurePass@123"
        Dim hash As String = passwordHasher.HashPassword(password)
        
        ' Act
        Dim result As Boolean = passwordHasher.VerifyPassword("", hash)
        
        ' Assert
        Assert.IsFalse(result)
    End Sub
    
    <Test>
    Public Sub HashPassword_NullPassword_ThrowsException()
        ' Act & Assert
        Assert.Throws(Of ArgumentNullException)(
            Sub() passwordHasher.HashPassword(Nothing)
        )
    End Sub
    
    <Test>
    Public Sub VerifyPassword_NullHash_ThrowsException()
        ' Act & Assert
        Assert.Throws(Of ArgumentNullException)(
            Sub() passwordHasher.VerifyPassword("password", Nothing)
        )
    End Sub
End Class
```

#### Tâche 3.2: Tests unitaires - AuthenticationService
- Tester:
  - Authentification avec credentials valides
  - Rejet credentials invalides
  - Gestion des sessions
  - Validation logout
  - Gestion des erreurs BD (déjà accessible)
- Utiliser mocks pour la BD
- **Livrables**: Test class complète pour AuthenticationService
- **Dépendances**: Tâche 1.3 (AuthenticationService)
- **Durée estimée**: 2-3h

**Code Exemple**:
```vb
' AuthenticationServiceTests.vb - TESTS UNITAIRES
<TestFixture>
Public Class AuthenticationServiceTests
    Private authService As AuthenticationService
    Private mockDbAccess As Mock(Of UserDataAccess)
    Private mockPasswordHasher As Mock(Of PasswordHasher)
    
    <SetUp>
    Public Sub Setup()
        ' Créer les mocks
        mockDbAccess = New Mock(Of UserDataAccess)
        mockPasswordHasher = New Mock(Of PasswordHasher)
        
        ' Initialiser le service
        authService = New AuthenticationService()
    End Sub
    
    <Test>
    Public Sub Authenticate_ValidCredentials_ReturnsSessionInfo()
        ' Arrange
        Dim username As String = "john_doe"
        Dim password As String = "MySecurePass@123"
        Dim expectedSessionID As String = Guid.NewGuid().ToString()
        
        ' Mock le hashage et la BD
        mockPasswordHasher.Setup(Function(ph) ph.HashPassword(password)) _
            .Returns("hashedvalue")
        
        mockDbAccess.Setup(Sub(da) da.CallAuthenticateUserProcedure(username, "hashedvalue", It.IsAny(Of SessionInfo))) _
            .Callback(Of String, String, SessionInfo)(
                Sub(un, ph, si)
                    si.UserID = 1
                    si.SessionID = expectedSessionID
                    si.Result = "SUCCESS"
                End Sub
            )
        
        ' Act
        Dim result As SessionInfo = authService.Authenticate(username, password)
        
        ' Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(1, result.UserID)
        Assert.AreEqual(expectedSessionID, result.SessionID)
        Assert.IsTrue(result.IsValid)
    End Sub
    
    <Test>
    Public Sub Authenticate_InvalidCredentials_ThrowsException()
        ' Arrange
        Dim username As String = "john_doe"
        Dim password As String = "WrongPassword@123"
        
        mockDbAccess.Setup(Sub(da) da.CallAuthenticateUserProcedure(It.IsAny(Of String), It.IsAny(Of String), It.IsAny(Of SessionInfo))) _
            .Callback(Of String, String, SessionInfo)(
                Sub(un, ph, si)
                    si.Result = "INVALID_CREDENTIALS"
                End Sub
            )
        
        ' Act & Assert
        Assert.Throws(Of UnauthorizedAccessException)(
            Sub() authService.Authenticate(username, password)
        )
    End Sub
    
    <Test>
    Public Sub Authenticate_InvalidUsername_ThrowsException()
        ' Arrange
        Dim invalidUsername As String = "ab"  ' Trop court
        Dim password As String = "MySecurePass@123"
        
        ' Act & Assert
        Assert.Throws(Of ArgumentException)(
            Sub() authService.Authenticate(invalidUsername, password)
        )
    End Sub
    
    <Test>
    Public Sub ValidateSession_ActiveSession_ReturnsTrue()
        ' Arrange
        Dim sessionID As String = Guid.NewGuid().ToString()
        mockDbAccess.Setup(Function(da) da.CallValidateSessionProcedure(sessionID)) _
            .Returns(1)
        
        ' Act
        Dim result As Boolean = authService.ValidateSession(sessionID)
        
        ' Assert
        Assert.IsTrue(result)
    End Sub
    
    <Test>
    Public Sub ValidateSession_ExpiredSession_ReturnsFalse()
        ' Arrange
        Dim sessionID As String = Guid.NewGuid().ToString()
        mockDbAccess.Setup(Function(da) da.CallValidateSessionProcedure(sessionID)) _
            .Returns(0)
        
        ' Act
        Dim result As Boolean = authService.ValidateSession(sessionID)
        
        ' Assert
        Assert.IsFalse(result)
    End Sub
    
    <Test>
    Public Sub Logout_ValidSessionID_ReturnsTrue()
        ' Arrange
        Dim sessionID As String = Guid.NewGuid().ToString()
        mockDbAccess.Setup(Function(da) da.CallLogoutUserProcedure(sessionID)) _
            .Returns("SUCCESS")
        
        ' Act
        Dim result As Boolean = authService.Logout(sessionID)
        
        ' Assert
        Assert.IsTrue(result)
    End Sub
End Class
```

#### Tâche 3.3: Tests d'intégration - Login/BD
- Tester le flux complet:
  - Login → BD → Session créée
  - Logout → BD → Session marquée inactive
  - Connexion à une BD de test Oracle
- Tester les timeouts
- **Livrables**: Integration tests validant le flux complet
- **Dépendances**: Tâches 1.2, 1.3, 2.1
- **Durée estimée**: 3h

**Code Exemple**:
```vb
' IntegrationTests.vb - TESTS D'INTÉGRATION
<TestFixture>
Public Class IntegrationTests
    Private authService As AuthenticationService
    Private dbAccess As UserDataAccess
    Private testUsername As String = "test_user_integration"
    Private testPassword As String = "TestPass@123"
    
    <SetUp>
    Public Sub Setup()
        authService = New AuthenticationService()
        dbAccess = New UserDataAccess()
        
        ' Créer un utilisateur de test dans la BD (si nécessaire)
        CreateTestUser()
    End Sub
    
    <TearDown>
    Public Sub TearDown()
        ' Nettoyer après les tests
        CleanupTestUser()
    End Sub
    
    <Test>
    Public Sub FullLoginFlow_FromLoginToMainForm_Success()
        ' Arrange
        Dim username As String = testUsername
        Dim password As String = testPassword
        
        ' Act - Authentification
        Dim sessionInfo As SessionInfo = authService.Authenticate(username, password)
        
        ' Assert - Vérifier session créée
        Assert.IsNotNull(sessionInfo)
        Assert.IsTrue(sessionInfo.IsValid)
        Assert.Greater(sessionInfo.UserID, 0)
        Assert.IsNotEmpty(sessionInfo.SessionID)
        
        ' Act - Valider la session depuis la BD
        Dim isValid As Boolean = authService.ValidateSession(sessionInfo.SessionID)
        
        ' Assert
        Assert.IsTrue(isValid)
    End Sub
    
    <Test>
    Public Sub FullLogoutFlow_FromMainFormToLogin_Success()
        ' Arrange
        Dim username As String = testUsername
        Dim password As String = testPassword
        
        ' Act - Login
        Dim sessionInfo As SessionInfo = authService.Authenticate(username, password)
        
        ' Vérifier que la session est valide
        Assert.IsTrue(authService.ValidateSession(sessionInfo.SessionID))
        
        ' Act - Logout
        Dim logoutResult As Boolean = authService.Logout(sessionInfo.SessionID)
        
        ' Assert
        Assert.IsTrue(logoutResult)
        Assert.IsFalse(authService.ValidateSession(sessionInfo.SessionID))
    End Sub
    
    <Test>
    Public Sub SessionTimeout_AfterOneHour_InvalidateSession()
        ' Arrange
        Dim username As String = testUsername
        Dim password As String = testPassword
        
        ' Act - Login
        Dim sessionInfo As SessionInfo = authService.Authenticate(username, password)
        Dim sessionID As String = sessionInfo.SessionID
        
        ' Vérifier session active
        Assert.IsTrue(authService.ValidateSession(sessionID))
        
        ' Simuler une inactivité d'une heure (en BD directement)
        ' UPDATE UserSessions SET LastActivityTime = LastActivityTime - INTERVAL '1' HOUR WHERE SessionID = 'xxx'
        dbAccess.SimulateSessionTimeout(sessionID)
        
        ' Act - Vérifier timeout
        Dim isValid As Boolean = authService.ValidateSession(sessionID)
        
        ' Assert
        Assert.IsFalse(isValid)
    End Sub
    
    <Test>
    Public Sub MultipleLogins_SameLUser_MultipleSessionsCreated()
        ' Arrange
        Dim username As String = testUsername
        Dim password As String = testPassword
        
        ' Act - Premier login
        Dim session1 As SessionInfo = authService.Authenticate(username, password)
        
        ' Act - Deuxième login (même utilisateur)
        Dim session2 As SessionInfo = authService.Authenticate(username, password)
        
        ' Assert - Deux SessionID différents
        Assert.AreNotEqual(session1.SessionID, session2.SessionID)
        Assert.AreEqual(session1.UserID, session2.UserID)
        
        ' Les deux sessions doivent être valides
        Assert.IsTrue(authService.ValidateSession(session1.SessionID))
        Assert.IsTrue(authService.ValidateSession(session2.SessionID))
    End Sub
    
    <Test>
    Public Sub LoginWithWrongPassword_SessionNotCreated()
        ' Arrange
        Dim username As String = testUsername
        Dim wrongPassword As String = "WrongPassword@999"
        
        ' Act & Assert
        Assert.Throws(Of UnauthorizedAccessException)(
            Sub() authService.Authenticate(username, wrongPassword)
        )
        
        ' Vérifier aucune session ne s'est créée en BD
        Dim sessionCount As Integer = dbAccess.GetActiveSessionCount(username)
        ' Le count avant et après devrait être le même (ou 0 si aucune session existante)
    End Sub
    
    ' ===== MÉTHODES HELPER =====
    Private Sub CreateTestUser()
        ' Créer un utilisateur de test dans la BD
        Try
            Dim phaser As New PasswordHasher()
            Dim hashedPassword As String = phaser.HashPassword(testPassword)
            dbAccess.CreateUser(testUsername, hashedPassword, "test@example.com")
        Catch ex As Exception
            ' Utilisateur existe déjà, c'est OK
        End Try
    End Sub
    
    Private Sub CleanupTestUser()
        ' Supprimer les données de test
        Try
            dbAccess.DeleteUserAndSessions(testUsername)
        Catch ex As Exception
            ' Ignorer les erreurs de cleanup
        End Try
    End Sub
End Class
```

#### Tâche 3.4: Tests de sécurité
- Tester les vulnerabilités:
  - Injection SQL: `' OR '1'='1` dans username/password
  - Brute-force: Tentatives multiples avec délai de réponse
  - Session hijacking: Utiliser un sessionID expiré
  - Vérifier pas de password en clair dans logs
- Documenter les scénarios de test
- **Livrables**: Document de test cases de sécurité + rapports
- **Dépendances**: Toutes les tâches d'authentification
- **Durée estimée**: 2-3h

**Code Exemple**:
```vb
' SecurityTests.vb - TESTS DE SÉCURITÉ
<TestFixture>
Public Class SecurityTests
    Private authService As AuthenticationService
    Private inputValidator As InputValidator
    
    <SetUp>
    Public Sub Setup()
        authService = New AuthenticationService()
        inputValidator = New InputValidator()
    End Sub
    
    <Test>
    Public Sub SQLInjection_SingleQuoteInUsername_Blocked()
        ' Arrange
        Dim maliciousUsername As String = "' OR '1'='1"
        Dim password As String = "ValidPassword@123"
        
        ' Act
        Dim isSQLAttempt As Boolean = inputValidator.IsSQLInjectionAttempt(maliciousUsername)
        
        ' Assert
        Assert.IsTrue(isSQLAttempt, "SQL injection attempt should be detected")
    End Sub
    
    <Test>
    Public Sub SQLInjection_DropTableInPassword_Blocked()
        ' Arrange
        Dim username As String = "john_doe"
        Dim maliciousPassword As String = "'; DROP TABLE Users; --"
        
        ' Act
        Dim isSQLAttempt As Boolean = inputValidator.IsSQLInjectionAttempt(maliciousPassword)
        
        ' Assert
        Assert.IsTrue(isSQLAttempt)
    End Sub
    
    <Test>
    Public Sub SQLInjection_UnionSelectInUsername_Blocked()
        ' Arrange
        Dim maliciousUsername As String = "admin' UNION SELECT * FROM Users--"
        
        ' Act
        Dim isSQLAttempt As Boolean = inputValidator.IsSQLInjectionAttempt(maliciousUsername)
        
        ' Assert
        Assert.IsTrue(isSQLAttempt)
    End Sub
    
    <Test>
    Public Sub BruteForce_MultipleFailedAttempts_ShouldBlock()
        ' Arrange
        Dim username As String = "john_doe"
        Dim wrongPassword As String = "WrongPass@123"
        Dim failureCount As Integer = 0
        Dim maxAttempts As Integer = 5
        
        ' Act - Simuler 5 tentatives échouées
        For i As Integer = 1 To maxAttempts
            Try
                authService.Authenticate(username, wrongPassword)
                failureCount += 1
            Catch ex As UnauthorizedAccessException
                failureCount += 1
                ' Attendre un peu entre les tentatives (rate limiting)
                Threading.Thread.Sleep(100)
            End Try
        End Sub
        
        ' Assert - Après X tentatives, l'accès devrait être bloqué
        ' (À implémenter dans AuthenticationService)
        Assert.GreaterOrEqual(failureCount, maxAttempts)
    End Sub
    
    <Test>
    Public Sub SessionHijacking_FakeSessionID_Rejected()
        ' Arrange
        Dim fakeSessionID As String = Guid.NewGuid().ToString()
        
        ' Act
        Dim isValid As Boolean = authService.ValidateSession(fakeSessionID)
        
        ' Assert
        Assert.IsFalse(isValid, "Fake session ID should not validate")
    End Sub
    
    <Test>
    Public Sub SessionExpiry_ExpiredSessionID_Rejected()
        ' Arrange
        Dim username As String = "john_doe"
        Dim password As String = "ValidPassword@123"
        
        ' Act - Créer une session
        Dim sessionInfo As SessionInfo = authService.Authenticate(username, password)
        
        ' Attendre quelques secondes (simuler inactivité)
        Threading.Thread.Sleep(2000)
        
        ' Vérifier que la session est toujours valide (pas 1h écoulée)
        Dim isStillValid As Boolean = authService.ValidateSession(sessionInfo.SessionID)
        
        ' Assert
        Assert.IsTrue(isStillValid, "Fresh session should still be valid")
        
        ' Logout pour terminer proprement
        authService.Logout(sessionInfo.SessionID)
    End Sub
    
    <Test>
    Public Sub PasswordNotInLogs_CheckLoggingDoesntExposeSensitiveData()
        ' Arrange
        Dim username As String = "john_doe"
        Dim password As String = "SecretPassword@123"
        
        ' Capturer les logs avant
        Dim logBefore As List(Of String) = CaptureApplicationLogs()
        
        ' Act - Essayer de login avec mauvais password
        Try
            authService.Authenticate(username, "WrongPassword@123")
        Catch ex As Exception
            ' Expected
        End Try
        
        ' Capturer les logs après
        Dim logAfter As List(Of String) = CaptureApplicationLogs()
        
        ' Assert - Vérifier que le password n'est pas dans les logs
        Dim combinedLogs As String = String.Join(" ", logAfter)
        Assert.IsFalse(combinedLogs.Contains(password), "Password should not be in logs")
        Assert.IsFalse(combinedLogs.Contains("SecretPassword"), "Password part should not be in logs")
    End Sub
    
    <Test>
    Public Sub PasswordValidation_WeakPassword_Rejected()
        ' Arrange - Passwords trop faibles
        Dim weakPasswords() As String = {
            "short",              ' Trop court
            "nouppercase@123",   ' Pas de majuscule
            "NoLowerCase@123",   ' Pas de minuscule
            "NoSpecial123",      ' Pas de caractère spécial
            "No1234567890"       ' Pas de caractère spécial
        }
        
        ' Act & Assert
        For Each weakPassword In weakPasswords
            Dim isValid As Boolean = inputValidator.ValidatePassword(weakPassword)
            Assert.IsFalse(isValid, $"Password '{weakPassword}' should be rejected")
        End Sub
    End Sub
    
    ' ===== HELPER METHOD =====
    Private Function CaptureApplicationLogs() As List(Of String)
        ' À implémenter selon la stratégie de logging
        Return New List(Of String)()
    End Function
End Class
```

#### Tâche 3.5: Documentation des test cases
- Créer un fichier `TEST_CASES_SEMAINE2.md`
- Documenter tous les scénarios:
  - Happy path (connexion réussie)
  - Erreurs de validation
  - Erreurs de base de données
  - Tests de sécurité
- Chaque test case incluant: ID, description, données d'entrée, résultat attendu
- **Livrables**: Documentation complète des tests
- **Dépendances**: Tâches 3.1-3.4
- **Durée estimée**: 1-2h

**Structure du fichier TEST_CASES_SEMAINE2.md**:
```markdown
# TEST CASES SEMAINE 2 - ChatApp

## 1. TESTS UNITAIRES PASSWORDHASHER

### TC-1.1.1: Hachage correct d'un mot de passe
- **Objectif**: Vérifier que le hachage produit un résultat valide
- **Données d'entrée**: `password = "MySecurePass@123"`
- **Étapes**:
  1. Appeler PasswordHasher.HashPassword(password)
  2. Vérifier le résultat n'est pas null
  3. Vérifier le résultat n'est pas égal au plaintext
- **Résultat attendu**: ✅ Hash valide retourné, différent du plaintext

### TC-1.1.2: Salt unique à chaque hachage
- **Objectif**: Vérifier que chaque hachage produit un résultat différent (même password)
- **Données d'entrée**: `password = "MySecurePass@123"`
- **Étapes**:
  1. Appeler HashPassword(password) deux fois
  2. Comparer les deux résultats
- **Résultat attendu**: ✅ Les deux hashes sont différents

### TC-1.1.3: Vérification password correct
- **Objectif**: Vérifier qu'un password correct est accepté
- **Données d'entrée**: `password = "MySecurePass@123"`
- **Étapes**:
  1. Hacher le password
  2. Appeler VerifyPassword(password, hash)
  3. Vérifier le résultat
- **Résultat attendu**: ✅ VerifyPassword retourne true

### TC-1.1.4: Rejet d'un mauvais password
- **Objectif**: Vérifier qu'un password incorrect est rejeté
- **Données d'entrée**: 
  - correct: "MySecurePass@123"
  - incorrect: "WrongPassword@456"
- **Étapes**:
  1. Hacher le correct password
  2. Appeler VerifyPassword(incorrect, hash)
- **Résultat attendu**: ✅ VerifyPassword retourne false

## 2. TESTS AUTHENTIFICATION

### TC-2.1.1: Login avec credentials valides
- **Objectif**: Authentifier un utilisateur avec username/password corrects
- **Données d'entrée**: 
  - username: "john_doe"
  - password: "ValidPass@123"
- **Étapes**:
  1. Appeler AuthenticationService.Authenticate(username, password)
  2. Vérifier SessionInfo retourné
  3. Vérifier que UserID et SessionID ne sont pas null
- **Résultat attendu**: ✅ SessionInfo valide avec UserID et SessionID

### TC-2.1.2: Login avec username inexistant
- **Objectif**: Rejeter un login avec username inexistant
- **Données d'entrée**: 
  - username: "nonexistent_user"
  - password: "AnyPassword@123"
- **Étapes**:
  1. Appeler Authenticate(username, password)
  2. Attendre une exception
- **Résultat attendu**: ✅ UnauthorizedAccessException levée

### TC-2.1.3: Login avec password incorrect
- **Objectif**: Rejeter un login avec password incorrect
- **Données d'entrée**: 
  - username: "john_doe"
  - password: "WrongPassword@123"
- **Étapes**:
  1. Appeler Authenticate(username, password)
  2. Attendre une exception
- **Résultat attendu**: ✅ UnauthorizedAccessException levée

## 3. TESTS INJECTION SQL

### TC-3.1.1: Injection ' OR '1'='1' dans username
- **Objectif**: Détecter et bloquer injection SQL basique
- **Données d'entrée**: `username = "' OR '1'='1'"`
- **Étapes**:
  1. Appeler InputValidator.IsSQLInjectionAttempt(username)
  2. Vérifier le résultat
- **Résultat attendu**: ✅ Retourne true (injection détectée)

### TC-3.1.2: Injection DROP TABLE dans password
- **Objectif**: Détecter injection DROP TABLE
- **Données d'entrée**: `password = "'; DROP TABLE Users; --"`
- **Étapes**:
  1. Appeler IsSQLInjectionAttempt(password)
  2. Vérifier le résultat
- **Résultat attendu**: ✅ Retourne true (injection détectée)

### TC-3.2.1: Validation username longueur minima
- **Objectif**: Rejeter username trop court
- **Données d'entrée**: `username = "ab"`
- **Étapes**:
  1. Appeler InputValidator.ValidateUsername(username)
  2. Vérifier le résultat
- **Résultat attendu**: ✅ Retourne false

### TC-3.2.2: Validation username longueur maxima
- **Objectif**: Rejeter username trop long
- **Données d'entrée**: `username = "a" * 25`  (25 caractères)
- **Étapes**:
  1. Appeler ValidateUsername(username)
- **Résultat attendu**: ✅ Retourne false

## 4. TESTS SESSION

### TC-4.1.1: Validation session active
- **Objectif**: Valider qu'une session récem créée est active
- **Données d'entrée**: SessionID d'une session fraîche
- **Étapes**:
  1. Créer une session via Authenticate
  2. Appeler AuthenticationService.ValidateSession(sessionID)
  3. Vérifier le résultat
- **Résultat attendu**: ✅ Retourne true

### TC-4.2.1: Logout valide
- **Objectif**: Terminer une session
- **Données d'entrée**: SessionID d'une session active
- **Étapes**:
  1. Créer une session
  2. Appeler AuthenticationService.Logout(sessionID)
  3. Essayer de valider la session à nouveau
- **Résultat attendu**: ✅ Logout réussit, ValidateSession retourne false

## 5. TESTS UI - LOGINFORM

### TC-5.1.1: Login button click avec credentials valides
- **Objectif**: Vérifier que le login form appelle le service correctement
- **Données d'entrée**: 
  - Username TextBox: "john_doe"
  - Password TextBox: "ValidPass@123"
- **Étapes**:
  1. Cliquer bouton "Se connecter"
  2. Vérifier appel à AuthenticationService.Authenticate
  3. Vérifier MainForm s'ouvre
- **Résultat attendu**: ✅ MainForm ouvre, LoginForm cachée

### TC-5.1.2: Enter key dans password field
- **Objectif**: Vérifier que Enter lance le login
- **Données d'entrée**: Credentials valides
- **Étapes**:
  1. Remplir username et password
  2. Appuyer sur Enter dans le password field
  3. Vérifier que PerformLogin s'exécute
- **Résultat attendu**: ✅ Login déclenché

### TC-5.1.3: Message d'erreur pour username vide
- **Objectif**: Afficher message d'erreur convivial
- **Données d'entrée**: 
  - Username: "" (vide)
  - Password: "ValidPass@123"
- **Étapes**:
  1. Cliquer "Se connecter"
  2. Vérifier le message d'erreur
- **Résultat attendu**: ✅ Message: "Veuillez entrer un nom d'utilisateur"

## 6. TESTS UI - MAINFORM

### TC-6.1.1: Affichage bienvenue utilisateur
- **Objectif**: Vérifier que MainForm affiche l'ID utilisateur
- **Données d'entrée**: SessionInfo avec UserID = 42
- **Étapes**:
  1. Ouvrir MainForm avec SessionInfo
  2. Vérifier le label "Bienvenue"
- **Résultat attendu**: ✅ Label affiche "Bienvenue, Utilisateur #42"

### TC-6.1.2: Heure mise à jour toutes les secondes
- **Objectif**: Vérifier que l'horloge est à jour
- **Étapes**:
  1. Ouvrir MainForm
  2. Attendre 2 secondes
  3. Vérifier que l'heure a changé
- **Résultat attendu**: ✅ Heure mise à jour correctement

### TC-6.2.1: Déconnexion via menu
- **Objectif**: Vérifier que Déconnexion logout et revient à LoginForm
- **Étapes**:
  1. Ouvrir MainForm
  2. Cliquer menu "Déconnexion"
  3. Vérifier LoginForm s'affiche
- **Résultat attendu**: ✅ LoginForm affichée, MainForm fermée

## 7. TESTS INTÉGRATION

### TC-7.1.1: Flux complet Login → MainForm → Logout
- **Objectif**: Tester le flux utilisateur complet
- **Étapes**:
  1. Ouvrir LoginForm
  2. Entrer credentials
  3. Vérifier MainForm ouvre
  4. Vérifier que les données utilisateur s'affichent
  5. Cliquer Déconnexion
  6. Vérifier LoginForm réapparaît
- **Résultat attendu**: ✅ Flux complet sans erreurs

### TC-7.2.1: Multiple logins du même utilisateur
- **Objectif**: Vérifier que plusieurs sessions peuvent être créées
- **Étapes**:
  1. Login avec user1
  2. Ouvrir une 2ème instance d'application
  3. Login avec user1 à nouveau
  4. Vérifier que les deux sessions sont actives
- **Résultat attendu**: ✅ Deux SessionID différents, les deux valides

## 8. RAPPORT DE COUVERTURE

### Couverture de code cible: 70% minimum

#### Couverture par module:
- PasswordHasher: 100%
- AuthenticationService: 85%
- InputValidator: 90%
- SessionManager: 75%
- LoginForm: 70%
- MainForm: 70%

### Résumé exécution:
- Total test cases: 30+
- Passés: [À remplir]
- Échoués: [À remplir]
- Bloquants: [À remplir]
```

---

## 🔄 Dépendances entre tâches

```
1.1 (PasswordHasher) ──┐
                       ├──> 1.3 (AuthenticationService) ──┐
1.2 (StoredProcs) ────┘                                   ├──> 2.1 (LoginForm)
                                                          ├──> 2.2 (MainForm)
1.4 (InputValidator)      (Parallèle à 1.1-1.2)           ├──> 2.4 (SessionManager)
                                                          │
2.3 (ClientValidator)     (Parallèle)                     └──> 3.2 (Tests AuthService)

3.1 (Tests PasswordHasher) - Parallèle, dépend de 1.1
3.3 (Tests Intégration)    - Après 1.2, 1.3, 2.1
3.4 (Tests Sécurité)       - Après 1.4, 2.1
3.5 (Documentation Tests)  - Dernière tâche
```

---

## ⏱️ Planning Semaine 2 (Exemple de répartition)

### Jour 1 (Session)
- **Dev A**: Tâche 1.1 (PasswordHasher) - 2h
- **Dev B**: Tâche 2.3 (ClientValidator) + Début Tâche 2.1 (LoginForm) - 2h
- **Dev C**: Tâche 3.1 (Tests PasswordHasher) - 1h
- **Reunion fin de jour**: Synchronisation, prochains steps

### Jour 2-3 (Entre sessions)
- **Dev A**: Tâche 1.2 (StoredProcs Oracle) 2-3h + Tâche 1.3 (AuthenticationService) 1-2h
- **Dev B**: Fin Tâche 2.1 (LoginForm) 1-2h + Tâche 2.2 (MainForm) 2h + Tâche 2.4 (SessionManager) 1-2h
- **Dev C**: Tâche 3.2 (Tests AuthenticationService) 2-3h + Tâche 3.3 (Tests Intégration) 1-2h

### Jour 4 (Session 2)
- **Dev A**: Revue code AuthenticationService, tests avec Dev C
- **Dev B**: Intégration LoginForm + MainForm + SessionManager
- **Dev C**: Tâche 3.4 (Tests Sécurité) + Tâche 3.5 (Documentation)
- **Reunion fin de session**: Code review, validations finales

---

## 📦 Livrables Semaine 2

### Code
- ✅ `PasswordHasher.vb` - Classe de hachage sécurisé
- ✅ `AuthenticationService.vb` - Service d'authentification
- ✅ `SessionManager.vb` - Gestion des sessions (Singleton)
- ✅ `InputValidator.vb` - Validation des entrées
- ✅ `ClientValidator.vb` - Validation côté client
- ✅ `LoginForm.vb` - Interface de connexion
- ✅ `MainForm.vb` - Écran principal (menu navigable)
- ✅ Procédures stockées Oracle (sp_AuthenticateUser, sp_ValidateSession, sp_LogoutUser)

### Tests
- ✅ `PasswordHasherTests.vb` - Tests unitaires du hachage
- ✅ `AuthenticationServiceTests.vb` - Tests du service
- ✅ `IntegrationTests.vb` - Tests du flux complet
- ✅ `SecurityTests.vb` - Tests de sécurité
- ✅ `TEST_CASES_SEMAINE2.md` - Documentation des tests

### Documentation
- ✅ Ce plan détaillé (SEMAINE_2_DETAIL.md)
- ✅ `TEST_CASES_SEMAINE2.md` - Scénarios de test
- ✅ Commentaires de code pour classes complexes
- ✅ Fichiers Scrum (CSV + HTML avec backlog, user stories, burn-down chart)

---

## 🔒 Points critiques de sécurité

1. **Hachage de mots de passe**
   - ❌ NE JAMAIS stocker les mots de passe en clair
   - ✅ Utiliser SHA-256 + salt aléatoire
   - ✅ Vérifier avec `VerifyPassword()`, jamais avec `==`

2. **Injection SQL**
   - ✅ Utiliser les paramètres (@param) dans toutes les requêtes
   - ✅ Valider les entrées longueur/caractères
   - ✅ Tester avec `' OR '1'='1`, `; DROP TABLE Users;`

3. **Sessions**
   - ✅ SessionID unique et aléatoire
   - ✅ Timeout après 1h d'inactivité
   - ✅ Vérifier SessionID avant chaque action
   - ❌ Ne jamais accepter un SessionID qui n'existe pas

4. **Logging**
   - ✅ Logger les tentatives de connexion échouées
   - ✅ Logger les erreurs (sauf mot de passe!)
   - ❌ Ne JAMAIS logger le mot de passe
   - ✅ Inclure timestamp, username, IP (si disponible)

5. **Messages d'erreur**
   - ✅ Afficher "Nom d'utilisateur ou mot de passe incorrect" (générique)
   - ❌ Ne pas dire "cet utilisateur n'existe pas" (fuites d'info)

---

## ✅ Checklist avant fin Semaine 2

- [ ] Login fonctionnel avec hachage SHA-256
- [ ] Gestion des sessions en place
- [ ] MainForm navigable (pas de crash)
- [ ] Tous les tests passent (unitaires + intégration)
- [ ] Tests de sécurité réussis (no injection SQL)
- [ ] Code review effectuée par tous
- [ ] Pas d'erreurs non gérées (try-catch)
- [ ] Documentation des tests complète
- [ ] Procédures Oracle testées et documentées
- [ ] Excel Scrum à jour

---

## 🚀 Prochaines étapes (Semaine 3)

- Forum public (créer, lire, modifier, supprimer les messages)
- Procédures stockées pour le forum
- Interface Forum (ForumForm)
- Pagination des messages

---

## 📝 Notes importantes

1. **Travail en parallèle**: Dev A et Dev B doivent travailler en parallèle. Dev C commence plus tard (après Day 1).
2. **Commits Git**: Faire des commits réguliers (1 commit par tâche complétée)
3. **Code review**: Chaque code doit être revu par un autre dev avant merge
4. **Communication**: Synchros quotidiennes, signaler rapidement les bloquants
5. **Qualité > Vitesse**: Mieux vaut du code robuste et sécurisé que du code rapide
6. **Tests premiers**: Écrire les tests avant le code si possible (TDD)
