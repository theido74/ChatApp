# Plan Détaillé Semaine 1 - Architecture & Infrastructure
## Mise en place et Infrastructure (4 heures)

---

## 🏗️ PARTIE 1: Architecture 3 Couches - Théorie et Pratique

### Qu'est-ce que l'architecture 3 couches ?

L'architecture 3 couches (aussi appelée **3-tier architecture**) est une **pattern** (modèle) qui divise votre application en **3 parties distinctes**, chacune avec une responsabilité spécifique. Cela rend le code **plus organisé, testable et maintenable**.

```
┌─────────────────────────────────────┐
│   COUCHE PRÉSENTATION (UI)          │
│   (Formulaires, Interfaces)         │
│   - LoginForm.vb                    │
│   - MainForm.vb                     │
│   - ForumForm.vb                    │
└─────────────────────────────────────┘
            ↓ Appelle
┌─────────────────────────────────────┐
│   COUCHE MÉTIER (Business Logic)    │
│   (Services, Logique applicative)   │
│   - AuthenticationService.vb        │
│   - ForumService.vb                 │
│   - MessageService.vb               │
│   - SessionManager.vb               │
└─────────────────────────────────────┘
            ↓ Utilise
┌─────────────────────────────────────┐
│   COUCHE DONNÉES (Data Access)      │
│   (Connexion BD, Requêtes)          │
│   - DatabaseConnection.vb           │
│   - UserDataAccess.vb               │
│   - OracleHelper.vb                 │
└─────────────────────────────────────┘
            ↓ Accède à
        [BASE DE DONNÉES]
        Oracle SQL (Locale ou serveur)
```

### 🔑 Pourquoi cette architecture ?

1. **Séparation des responsabilités**: Chaque couche a UN rôle clair
2. **Testabilité**: On peut tester chaque couche indépendamment
3. **Maintenabilité**: Modifier la BD ne casse pas l'interface
4. **Réutilisabilité**: Plusieurs interfaces peuvent utiliser les mêmes services
5. **Travail en équipe**: Dev A et Dev B peuvent travailler en parallèle sur des couches différentes

### 📋 Détail de chaque couche

#### **COUCHE 1: PRÉSENTATION (UI - User Interface)**
**Responsabilité**: Afficher les données et capturer les entrées utilisateur

**Éléments**:
- Formulaires WinForms (`.vb`)
- Contrôles (TextBox, Button, ListBox, DataGridView, etc.)
- Événements utilisateur (Click, TextChanged, etc.)

**Ce que elle NE DOIT PAS faire**:
- ❌ Pas de logique métier complexe
- ❌ Pas d'accès direct à la BD
- ❌ Pas de calculs d'authentification
- ❌ Pas de requêtes SQL

**Exemple**: Formulaire Login
```vb
' LoginForm.vb - COUCHE PRÉSENTATION
Public Class LoginForm
    Private Sub btnLogin_Click(sender As Object, e As EventArgs)
        ' Récupérer les données du formulaire
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        
        ' Appeler le SERVICE (couche métier)
        Dim authService As New AuthenticationService()
        Dim isValid As Boolean = authService.AuthenticateUser(username, password)
        
        ' Afficher le résultat
        If isValid Then
            MessageBox.Show("Connexion réussie!")
            ' Ouvrir MainForm
        Else
            MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect")
        End If
    End Sub
End Class
```

#### **COUCHE 2: MÉTIER (Business Logic / Services)**
**Responsabilité**: Contenir la logique applicative et les règles métier

**Éléments**:
- Services (AuthenticationService, ForumService, etc.)
- Managers (SessionManager)
- Classes métier (User, Message, Forum)
- Validation de règles

**Ce que elle NE DOIT PAS faire**:
- ❌ Pas d'affichage direct
- ❌ Pas de création de formulaires
- ❌ Pas de requêtes SQL directes (passer par DataAccess)

**Exemple**: Service d'authentification
```vb
' AuthenticationService.vb - COUCHE MÉTIER
Public Class AuthenticationService
    Private dbAccess As New UserDataAccess()
    
    Public Function AuthenticateUser(username As String, password As String) As Boolean
        ' Validation des données
        If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) Then
            Throw New ArgumentException("Username et password requis")
        End If
        
        If username.Length < 3 Then
            Throw New ArgumentException("Username minimum 3 caractères")
        End If
        
        ' Appeler DataAccess pour récupérer l'utilisateur
        Dim user As User = dbAccess.GetUserByUsername(username)
        
        If user Is Nothing Then
            Return False
        End If
        
        ' Hacher le mot de passe saisi et comparer avec celui en BD
        Dim hashedPassword As String = HashPassword(password)
        
        Return hashedPassword = user.PasswordHash
    End Function
    
    ' Hacher le mot de passe (SHA-256)
    Private Function HashPassword(password As String) As String
        Using hasher As New System.Security.Cryptography.SHA256Managed
            Dim hashBytes As Byte() = hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
            Return Convert.ToBase64String(hashBytes)
        End Using
    End Function
End Class
```

#### **COUCHE 3: DONNÉES (Data Access Layer)**
**Responsabilité**: Gérer la connexion à la BD et les requêtes

**Éléments**:
- Classes d'accès aux données (UserDataAccess, MessageDataAccess, etc.)
- Connexion à Oracle
- Requêtes SQL
- Procédures stockées
- Mapping des résultats vers les objets

**Ce que elle NE DOIT PAS faire**:
- ❌ Pas de logique métier
- ❌ Pas d'affichage
- ❌ Pas de validation de règles métier

**Exemple**: Accès aux données utilisateurs
```vb
' UserDataAccess.vb - COUCHE DONNÉES
Public Class UserDataAccess
    Private connectionString As String = "Data Source=ORACLE_SERVER;User ID=username;Password=password;"
    
    Public Function GetUserByUsername(username As String) As User
        Dim user As New User()
        
        ' Utiliser "Using" pour s'assurer que la connexion est fermée
        Using connection As New OracleConnection(connectionString)
            connection.Open()
            
            ' Utiliser des PARAMÈTRES pour éviter l'injection SQL
            Dim command As New OracleCommand("SELECT * FROM Users WHERE Username = :username", connection)
            command.Parameters.AddWithValue(":username", username)
            
            Using reader As OracleDataReader = command.ExecuteReader()
                If reader.Read() Then
                    user.UserID = reader("UserID")
                    user.Username = reader("Username")
                    user.PasswordHash = reader("PasswordHash")
                    user.Email = reader("Email")
                    user.CreatedDate = reader("CreatedDate")
                    Return user
                End If
            End Using
        End Using
        
        Return Nothing
    End Function
End Class
```

### 📊 Flux de données - Exemple complet

```
1. UTILISATEUR tape "John" et "password123" dans LoginForm
                    ↓
2. FORMULAIRE (Présentation)
   - Récupère les valeurs
   - Appelle authService.AuthenticateUser("John", "password123")
                    ↓
3. SERVICE (Métier)
   - Valide les données
   - Hache le mot de passe
   - Appelle dbAccess.GetUserByUsername("John")
                    ↓
4. DATA ACCESS (Données)
   - Ouvre connexion Oracle
   - Exécute: SELECT * FROM Users WHERE Username = :username
   - Retourne objet User
                    ↓
5. SERVICE (Métier)
   - Compare les mots de passe
   - Retourne true/false
                    ↓
6. FORMULAIRE (Présentation)
   - Affiche "Connexion réussie!" ou "Erreur"
   - Ouvre MainForm si succès
```

### ✅ Avantages pratiques pour VOTRE PROJET

| Avantage | Impact pour vous |
|----------|------------------|
| **Dev A et Dev B peuvent travailler ensemble** | Dev A crée les DataAccess, Dev B crée les Formulaires |
| **Facile à tester** | Tester le Service sans avoir une BD réelle |
| **Facile à changer de BD** | Si vous passez de Oracle à SQL Server, changez juste DataAccess |
| **Code réutilisable** | ForumService utilisé par 2 formulaires différents |
| **Déboguer plus facilement** | Erreur dans Forum ? Allez voir ForumService/ForumDataAccess |

---

## 📅 SEMAINE 1 - TÂCHES DÉTAILLÉES

Durée totale: **4 heures** | Répartition: **Dev A (2.5h) + Dev B (1.5h)**

### TÂCHE 1: Créer le schéma de base de données Oracle
**Durée**: 2 heures  
**Responsable**: Développeur A  
**Dépendance**: Aucune (commencer par là!)

#### Qu'est-ce qu'il faut faire ?

Créer **6 tables** dans Oracle avec toutes les colonnes, types de données, contraintes et clés étrangères.

#### Explication de chaque table

##### 1. **Table Users** (DÉJÀ CRÉÉE par prof - juste vérifier)
```sql
-- Cette table existe déjà, mais voici sa structure
CREATE TABLE Users (
    UserID NUMBER PRIMARY KEY,  -- Identifiant unique
    Username VARCHAR2(50) NOT NULL UNIQUE,  -- Nom d'utilisateur (pas de doublons)
    PasswordHash VARCHAR2(255) NOT NULL,  -- Mot de passe HASHÉ (jamais le vrai)
    Email VARCHAR2(100) NOT NULL UNIQUE,
    FirstName VARCHAR2(50),
    LastName VARCHAR2(50),
    CreatedDate DATE DEFAULT SYSDATE,  -- Date actuelle par défaut
    IsActive NUMBER DEFAULT 1  -- 1=actif, 0=inactif
);
```

**Explications**:
- `NOT NULL`: Le champ ne peut pas être vide
- `UNIQUE`: Deux utilisateurs ne peuvent pas avoir le même username
- `PRIMARY KEY`: Identifie de façon unique chaque ligne
- `DEFAULT SYSDATE`: La date du jour si pas spécifiée
- `PasswordHash` en VARCHAR2(255): Contient le mot de passe HASHÉ (jamais le mot de passe en clair!)

##### 2. **Table UserSessions** (Gestion des sessions)
```sql
CREATE TABLE UserSessions (
    SessionID VARCHAR2(50) PRIMARY KEY,  -- ID de session unique (UUID)
    UserID NUMBER NOT NULL,
    LoginTime DATE DEFAULT SYSDATE,  -- Quand l'utilisateur s'est connecté
    LastActivityTime DATE DEFAULT SYSDATE,  -- Quand il a fait quelque chose
    IsActive NUMBER DEFAULT 1,  -- 1=session valide, 0=session expirée
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);

-- Index pour recherche rapide par UserID
CREATE INDEX idx_sessions_userid ON UserSessions(UserID);
```

**Explications**:
- `SessionID`: Chaque fois qu'un utilisateur se connecte, il reçoit une SessionID unique
- `FOREIGN KEY`: Lie cette table à Users (un UserID doit exister dans Users)
- `ON DELETE CASCADE`: Si un User est supprimé, toutes ses sessions sont supprimées
- `INDEX`: Accélère les recherches par UserID (très utilisé)

**Cas d'usage**:
```
Utilisateur "John" se connecte
→ Créer une ligne: SessionID='abc123', UserID=1, LoginTime=21/04/2026 08:40
→ Chaque action: UpdateLastActivityTime('abc123') = SYSDATE
→ Déconnexion: Mettre IsActive=0
```

##### 3. **Table Messages** (Forum + Messages privés)
```sql
CREATE TABLE Messages (
    MessageID NUMBER PRIMARY KEY,  -- ID unique pour chaque message
    SenderID NUMBER NOT NULL,  -- Qui envoie le message
    RecipientID NUMBER,  -- NULL si c'est un message forum, UserID si message privé
    ForumID NUMBER,  -- NULL si message privé, ForumID si message forum
    Content VARCHAR2(4000),  -- Contenu du message (max 4000 caractères)
    Timestamp DATE DEFAULT SYSDATE,  -- Quand le message a été envoyé
    IsPrivate NUMBER DEFAULT 0,  -- 1=privé, 0=forum
    IsDeleted NUMBER DEFAULT 0,  -- Soft delete (pour l'historique)
    FOREIGN KEY (SenderID) REFERENCES Users(UserID),
    FOREIGN KEY (RecipientID) REFERENCES Users(UserID),
    FOREIGN KEY (ForumID) REFERENCES Forums(ForumID)
);

CREATE INDEX idx_messages_senderid ON Messages(SenderID);
CREATE INDEX idx_messages_recipientid ON Messages(RecipientID);
CREATE INDEX idx_messages_forumid ON Messages(ForumID);
CREATE INDEX idx_messages_timestamp ON Messages(Timestamp);  -- Pour trier par date
```

**Explications**:
- `RecipientID NULL`: Message forum (tout le monde le voit)
- `RecipientID=2`: Message privé pour UserID=2
- `IsPrivate`: Flag pour distinguer rapidement forum/privé
- `IsDeleted`: Soft delete (ne pas vraiment supprimer, juste marquer comme supprimé)
  - Avantage: On peut retrouver les anciens messages, garder l'historique
  - Inconvénient: Faut toujours filtrer `WHERE IsDeleted=0`

##### 4. **Table Forums** (Catégories de forums)
```sql
CREATE TABLE Forums (
    ForumID NUMBER PRIMARY KEY,  -- ID du forum
    ForumName VARCHAR2(100) NOT NULL,  -- Ex: "Général", "Questions VB.NET", etc.
    Description VARCHAR2(500),  -- Description du forum
    CreatedDate DATE DEFAULT SYSDATE,  -- Quand le forum a été créé
    IsActive NUMBER DEFAULT 1
);
```

**Explications**:
- Chaque Forum est une **catégorie** de discussion
- Exemple de données:
  ```
  ForumID=1, ForumName='Général', Description='Discussions générales'
  ForumID=2, ForumName='VB.NET', Description='Questions sur VB.NET'
  ForumID=3, ForumName='Base de données', Description='Oracle, SQL'
  ```

##### 5. **Table UserActivity** (Pings pour détection en ligne)
```sql
CREATE TABLE UserActivity (
    ActivityID NUMBER PRIMARY KEY,
    UserID NUMBER NOT NULL,
    LastPingTime DATE DEFAULT SYSDATE,  -- Dernière activité
    Status VARCHAR2(20) DEFAULT 'online',  -- 'online' ou 'offline'
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
);

CREATE INDEX idx_activity_userid ON UserActivity(UserID);
CREATE INDEX idx_activity_status ON UserActivity(Status);  -- Pour chercher les utilisateurs en ligne
```

**Explications**:
- **Ping**: Quand l'utilisateur fait quelque chose, on met à jour LastPingTime
- Logique pour déterminer en ligne:
  ```
  Si LastPingTime < SYSDATE - 30 minutes → Status = 'offline'
  Sinon → Status = 'online'
  ```
- C'est une table séparée car on ne veut pas modifier Users à chaque action

##### 6. **Table Logs** (Audit et logging)
```sql
CREATE TABLE Logs (
    LogID NUMBER PRIMARY KEY,
    UserID NUMBER,  -- NULL si action système
    Action VARCHAR2(50),  -- 'LOGIN', 'SEND_MESSAGE', 'DELETE_MESSAGE', 'ERROR', etc.
    Timestamp DATE DEFAULT SYSDATE,
    Details VARCHAR2(1000),  -- Détails supplémentaires
    ErrorMessage VARCHAR2(1000),  -- Si c'est une erreur
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL
);

CREATE INDEX idx_logs_userid ON Logs(UserID);
CREATE INDEX idx_logs_action ON Logs(Action);
CREATE INDEX idx_logs_timestamp ON Logs(Timestamp);
```

**Explications**:
- **Audit trail**: Tracer TOUT ce qui se passe (sécurité)
- `Action='LOGIN'`: Quelqu'un s'est connecté
- `Action='SEND_MESSAGE'`: Un message a été envoyé
- `Action='ERROR'`: Une erreur s'est produite (garder la trace)
- `ON DELETE SET NULL`: Si un user est supprimé, on garde le log mais UserID devient NULL

#### 📝 Instructions pour Dev A

1. **Ouvrir Oracle SQL Developer (ou SQL*Plus)**
2. **Copier-coller les 6 CREATE TABLE** ci-dessus
3. **Exécuter chaque requête** (Ctrl+Enter)
4. **Vérifier que tout est créé**:
   ```sql
   DESC Users;
   DESC UserSessions;
   DESC Messages;
   DESC Forums;
   DESC UserActivity;
   DESC Logs;
   ```
5. **Créer quelques données de test** dans Forums:
   ```sql
   INSERT INTO Forums VALUES (1, 'Général', 'Discussions générales', SYSDATE, 1);
   INSERT INTO Forums VALUES (2, 'VB.NET', 'Questions sur VB.NET', SYSDATE, 1);
   INSERT INTO Forums VALUES (3, 'Base de données', 'Oracle et SQL', SYSDATE, 1);
   COMMIT;
   ```
6. **Vérifier**:
   ```sql
   SELECT * FROM Forums;
   ```

---

### TÂCHE 2: Créer les procédures stockées Oracle
**Durée**: 1 heure  
**Responsable**: Développeur A  
**Dépendence**: Tâche 1 (tables créées)

#### Qu'est-ce qu'une procédure stockée ?

Une **procédure stockée** est un **bloc de code SQL/PL-SQL** enregistré dans la BD et qu'on peut appeler depuis l'application.

**Avantages**:
- ✅ Code réutilisable (appelé plusieurs fois)
- ✅ Plus rapide (code compila dans la BD)
- ✅ Sécurité (logique métier en BD, pas en code)
- ✅ Évite dupliquer les requêtes

**Inconvénient**:
- ❌ Harder à déboguer

#### Procédures à créer pour Semaine 1

##### 1. **UpdateUserActivity** - Ping utilisateur
```sql
CREATE OR REPLACE PROCEDURE UpdateUserActivity(p_UserID IN NUMBER)
AS
BEGIN
    -- Vérifier si la ligne existe
    IF EXISTS (SELECT 1 FROM UserActivity WHERE UserID = p_UserID) THEN
        -- Mettre à jour le ping
        UPDATE UserActivity 
        SET LastPingTime = SYSDATE, Status = 'online'
        WHERE UserID = p_UserID;
    ELSE
        -- Créer une nouvelle ligne
        INSERT INTO UserActivity (ActivityID, UserID, LastPingTime, Status)
        VALUES (seq_activity.NEXTVAL, p_UserID, SYSDATE, 'online');
    END IF;
    
    COMMIT;
END UpdateUserActivity;
/
```

**Explication**:
- `p_UserID`: Paramètre IN (valeur reçue)
- `EXISTS`: Vérifier si la ligne existe
- `SYSDATE`: Date/heure actuelle
- `NEXTVAL`: Récupérer la prochaine valeur de la séquence (ID auto-incrémentant)

**Usage depuis VB.NET**:
```vb
' Chaque 30 secondes, ping la BD
Dim dbAccess As New UserDataAccess()
dbAccess.UpdateUserActivity(currentUserID)
```

##### 2. **GetOnlineUsers** - Lister les utilisateurs en ligne
```sql
CREATE OR REPLACE PROCEDURE GetOnlineUsers(p_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN p_cursor FOR
    SELECT u.UserID, u.Username, a.LastPingTime, a.Status
    FROM Users u
    INNER JOIN UserActivity a ON u.UserID = a.UserID
    WHERE a.Status = 'online'
    AND a.LastPingTime > SYSDATE - 1/48  -- Moins de 30 minutes
    ORDER BY u.Username;
END GetOnlineUsers;
/
```

**Explication**:
- `SYS_REFCURSOR`: Pointeur vers un resultset (liste de résultats)
- `INNER JOIN`: Joindre Users avec UserActivity
- `SYSDATE - 1/48`: SYSDATE moins 30 minutes (1 jour = 48 × 30 min)

**Usage depuis VB.NET**:
```vb
Dim users As List(Of User) = dbAccess.GetOnlineUsers()
' Affiche "John, Marie, Pierre"
```

##### 3. **CreateUserSession** - Créer une session après login
```sql
CREATE OR REPLACE PROCEDURE CreateUserSession(
    p_UserID IN NUMBER,
    p_SessionID IN VARCHAR2,
    p_result OUT VARCHAR2
)
AS
BEGIN
    -- Vérifier que l'utilisateur existe
    IF NOT EXISTS (SELECT 1 FROM Users WHERE UserID = p_UserID AND IsActive = 1) THEN
        p_result := 'USER_NOT_FOUND';
        RETURN;
    END IF;
    
    -- Insérer la nouvelle session
    INSERT INTO UserSessions (SessionID, UserID, LoginTime, LastActivityTime, IsActive)
    VALUES (p_SessionID, p_UserID, SYSDATE, SYSDATE, 1);
    
    -- Créer/mettre à jour UserActivity
    BEGIN
        UPDATE UserActivity
        SET LastPingTime = SYSDATE, Status = 'online'
        WHERE UserID = p_UserID;
    EXCEPTION WHEN NO_DATA_FOUND THEN
        INSERT INTO UserActivity (ActivityID, UserID, LastPingTime, Status)
        VALUES (seq_activity.NEXTVAL, p_UserID, SYSDATE, 'online');
    END;
    
    COMMIT;
    p_result := 'SUCCESS';
EXCEPTION WHEN OTHERS THEN
    p_result := 'ERROR: ' || SQLERRM;
    ROLLBACK;
END CreateUserSession;
/
```

**Explication**:
- Vérifier que l'utilisateur existe et est actif
- Si oui, créer la session
- Si non, retourner une erreur
- `EXCEPTION WHEN OTHERS`: Attraper les erreurs

##### 4. **ValidateSession** - Vérifier si une session est valide
```sql
CREATE OR REPLACE FUNCTION ValidateSession(p_SessionID IN VARCHAR2)
RETURN NUMBER
AS
    v_isValid NUMBER := 0;
BEGIN
    -- Vérifier que la session existe, est active, et pas expirée
    SELECT COUNT(*)
    INTO v_isValid
    FROM UserSessions
    WHERE SessionID = p_SessionID
    AND IsActive = 1
    AND LastActivityTime > SYSDATE - 1/24;  -- Moins d'une heure
    
    RETURN v_isValid;
EXCEPTION WHEN OTHERS THEN
    RETURN 0;
END ValidateSession;
/
```

**Explication**:
- `FUNCTION`: Retourne une valeur (0=invalid, 1=valid)
- `RETURN NUMBER`: Type de retour
- Vérifier 3 choses: existe, active, pas expirée (1h)

#### 📊 Créer les séquences (auto-increment)

Avant les procédures, créer les séquences:
```sql
CREATE SEQUENCE seq_message START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE seq_activity START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE seq_log START WITH 1 INCREMENT BY 1;
```

#### 📝 Instructions pour Dev A

1. **Créer les séquences** (copier-coller les CREATE SEQUENCE)
2. **Créer les 4 procédures/fonctions** (copier-coller les CREATE OR REPLACE)
3. **Tester chaque procédure**:
   ```sql
   -- Tester UpdateUserActivity
   EXEC UpdateUserActivity(1);
   SELECT * FROM UserActivity;
   
   -- Tester GetOnlineUsers
   VAR cursor REFCURSOR;
   EXEC GetOnlineUsers(:cursor);
   PRINT cursor;
   ```

---

### TÂCHE 3: Configurer le projet VB.NET et architecture 3 couches
**Durée**: 1.5 heures  
**Responsable**: Développeur B  
**Dépendance**: Aucune (travailler en parallèle)

#### Étape 1: Créer le projet VB.NET

1. **Ouvrir Visual Studio**
2. **Créer un nouveau projet**:
   - Type: `Windows Forms App (.NET Framework)` (ou .NET Core)
   - Nom: `ChatApp`
   - Localisation: `/home/leprechaun/Documents/CAPP_APP`
3. **Cliquer sur Create**

La structure ressemble à:
```
ChatApp/
├── ChatApp.sln
├── ChatApp.vbproj
└── Form1.vb
```

#### Étape 2: Créer la structure 3 couches

**Dans l'Explorateur de solutions**, créer ces dossiers:

```
ChatApp/
├── Presentation/        ← COUCHE 1 (Formulaires)
│   ├── LoginForm.vb
│   ├── MainForm.vb
│   └── ForumForm.vb
├── BusinessLogic/       ← COUCHE 2 (Services)
│   ├── Services/
│   │   ├── AuthenticationService.vb
│   │   └── SessionManager.vb
│   └── Models/
│       ├── User.vb
│       ├── Message.vb
│       ├── Forum.vb
│       └── Session.vb
└── DataAccess/          ← COUCHE 3 (BD)
    ├── DatabaseConnection.vb
    ├── UserDataAccess.vb
    └── OracleHelper.vb
```

**Comment créer les dossiers**:
1. **Clic-droit** sur le projet
2. **Ajouter > Dossier**
3. Taper le nom

#### Étape 3: Créer les classes métier (Models)

##### Class User.vb
```vb
' BusinessLogic/Models/User.vb
Public Class User
    Public Property UserID As Integer
    Public Property Username As String
    Public Property PasswordHash As String
    Public Property Email As String
    Public Property FirstName As String
    Public Property LastName As String
    Public Property CreatedDate As DateTime
    Public Property IsActive As Boolean
End Class
```

##### Class Message.vb
```vb
' BusinessLogic/Models/Message.vb
Public Class Message
    Public Property MessageID As Integer
    Public Property SenderID As Integer
    Public Property RecipientID As Integer?  ' ? = Nullable
    Public Property ForumID As Integer?
    Public Property Content As String
    Public Property Timestamp As DateTime
    Public Property IsPrivate As Boolean
    Public Property IsDeleted As Boolean
End Class
```

##### Class Forum.vb
```vb
' BusinessLogic/Models/Forum.vb
Public Class Forum
    Public Property ForumID As Integer
    Public Property ForumName As String
    Public Property Description As String
    Public Property CreatedDate As DateTime
    Public Property IsActive As Boolean
End Class
```

##### Class Session.vb
```vb
' BusinessLogic/Models/Session.vb
Public Class Session
    Public Property SessionID As String
    Public Property UserID As Integer
    Public Property LoginTime As DateTime
    Public Property LastActivityTime As DateTime
    Public Property IsActive As Boolean
    Public Property CurrentUser As User
End Class
```

#### Étape 4: Créer la classe de connexion Oracle

```vb
' DataAccess/DatabaseConnection.vb
Imports Oracle.DataAccess.Client

Public Class DatabaseConnection
    ' ⚠️ À ADAPTER avec vos paramètres Oracle
    Private Shared _connectionString As String = _
        "Data Source=localhost:1521/xe;" & _
        "User Id=YourUsername;" & _
        "Password=YourPassword;"
    
    Public Shared Function GetConnection() As OracleConnection
        Return New OracleConnection(_connectionString)
    End Function
    
    ' Vérifier la connexion
    Public Shared Function TestConnection() As Boolean
        Try
            Using conn As OracleConnection = GetConnection()
                conn.Open()
                ' Si aucune exception, c'est bon
                conn.Close()
                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur de connexion: " & ex.Message)
            Return False
        End Try
    End Function
End Class
```

**⚠️ Paramètres à adapter**:
- `localhost:1521/xe`: Adresse et port Oracle
- `YourUsername`: Votre user Oracle
- `YourPassword`: Votre password Oracle

#### Étape 5: Créer SessionManager

```vb
' BusinessLogic/Services/SessionManager.vb
Public Class SessionManager
    Private Shared _currentSession As Session
    
    Public Shared Function GetCurrentSession() As Session
        Return _currentSession
    End Function
    
    Public Shared Sub SetCurrentSession(session As Session)
        _currentSession = session
    End Sub
    
    Public Shared Sub ClearSession()
        _currentSession = Nothing
    End Sub
    
    Public Shared Function IsSessionActive() As Boolean
        If _currentSession Is Nothing Then Return False
        
        ' Vérifier que la session n'a pas expiré (1 heure)
        If DateTime.Now.Subtract(_currentSession.LastActivityTime).TotalMinutes > 60 Then
            ClearSession()
            Return False
        End If
        
        Return _currentSession.IsActive
    End Function
End Class
```

#### Étape 6: Créer User Data Access

```vb
' DataAccess/UserDataAccess.vb
Imports Oracle.DataAccess.Client

Public Class UserDataAccess
    ' Récupérer un utilisateur par son username
    Public Function GetUserByUsername(username As String) As User
        Dim user As New User()
        
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()
                
                ' ⚠️ IMPORTANT: Utiliser les paramètres pour éviter l'injection SQL
                Dim cmd As New OracleCommand("SELECT * FROM Users WHERE Username = :username", conn)
                cmd.Parameters.AddWithValue(":username", username)
                
                Using reader As OracleDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        user.UserID = CInt(reader("UserID"))
                        user.Username = reader("Username").ToString()
                        user.PasswordHash = reader("PasswordHash").ToString()
                        user.Email = reader("Email").ToString()
                        user.FirstName = If(reader("FirstName") IsNot DBNull.Value, reader("FirstName").ToString(), "")
                        user.LastName = If(reader("LastName") IsNot DBNull.Value, reader("LastName").ToString(), "")
                        user.CreatedDate = CDate(reader("CreatedDate"))
                        user.IsActive = CBool(reader("IsActive"))
                        Return user
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
            Return Nothing
        End Try
        
        Return Nothing
    End Function
    
    ' Récupérer un utilisateur par son ID
    Public Function GetUserByID(userID As Integer) As User
        Dim user As New User()
        
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()
                
                Dim cmd As New OracleCommand("SELECT * FROM Users WHERE UserID = :userID", conn)
                cmd.Parameters.AddWithValue(":userID", userID)
                
                Using reader As OracleDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        user.UserID = userID
                        user.Username = reader("Username").ToString()
                        user.Email = reader("Email").ToString()
                        user.FirstName = If(reader("FirstName") IsNot DBNull.Value, reader("FirstName").ToString(), "")
                        Return user
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        
        Return Nothing
    End Function
End Class
```

**Points clés**:
- `Using`: S'assure que la connexion est fermée même en cas d'erreur
- `:username`: Paramètre (sûr contre l'injection SQL)
- `AddWithValue`: Ajouter la valeur du paramètre
- `If(... IsNot DBNull.Value, ...)`: Gérer les valeurs NULL

#### 📝 Instructions pour Dev B

1. **Créer le projet VB.NET** dans Visual Studio
2. **Créer les dossiers**: Presentation, BusinessLogic, DataAccess
3. **Créer les 4 fichiers Models** (User, Message, Forum, Session)
4. **Créer DatabaseConnection.vb** (adapter les paramètres Oracle)
5. **Créer SessionManager.vb**
6. **Créer UserDataAccess.vb**
7. **Compiler** (Ctrl+Shift+B) pour vérifier qu'il n'y a pas d'erreurs

---

### TÂCHE 4: Tester la connexion Oracle depuis VB.NET
**Durée**: 0.5 heure  
**Responsable**: Dev A + Dev B ensemble  
**Dépendance**: Tâche 3 (code VB.NET)

#### Étape 1: Ajouter la référence Oracle

1. **Clic-droit sur le projet** → **Gérer les packages NuGet**
2. **Chercher**: `Oracle.ManagedDataAccess`
3. **Installer** (version stable)

Ou en ligne de commande (Package Manager):
```powershell
Install-Package Oracle.ManagedDataAccess
```

#### Étape 2: Créer un formulaire de test

```vb
' Presentation/TestConnectionForm.vb
Public Class TestConnectionForm
    Private Sub Form_Load() Handles Me.Load
        ' Tester la connexion au démarrage
        TestOracleConnection()
    End Sub
    
    Private Sub TestOracleConnection()
        Try
            MessageBox.Show("Test de connexion en cours...", "Info")
            
            ' Appeler la méthode statique
            If DatabaseConnection.TestConnection() Then
                MessageBox.Show("✅ Connexion Oracle réussie!", "Succès")
                
                ' Tester aussi la requête
                Dim userAccess As New UserDataAccess()
                ' Supposant qu'il y a au moins 1 utilisateur
                Dim user As User = userAccess.GetUserByID(1)
                
                If user IsNot Nothing Then
                    MessageBox.Show("✅ Utilisateur trouvé: " & user.Username, "Succès")
                Else
                    MessageBox.Show("⚠️ Pas d'utilisateur avec ID=1", "Info")
                End If
            Else
                MessageBox.Show("❌ Connexion Oracle échouée!", "Erreur")
            End If
            
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message, "Erreur")
        End Try
    End Sub
End Class
```

#### Étape 3: Exécuter le test

1. **Définir ce formulaire comme démarrage** (clic-droit → Set as Start Form)
2. **Appuyer sur F5** pour exécuter
3. **Vérifier les messages**:
   - ✅ "Connexion Oracle réussie!"
   - ✅ "Utilisateur trouvé"

#### 🎯 Si ça ne marche pas

| Erreur | Cause | Solution |
|--------|-------|----------|
| `Cannot connect to Oracle` | Oracle pas démarré | Démarrer Oracle (ou vérifier le serveur) |
| `Invalid username/password` | Mauvais paramètres | Vérifier connectionString |
| `No such user` | Pas d'utilisateur ID=1 | Créer un utilisateur test en Oracle |
| `Assembly not found` | NuGet pas installé | Installer Oracle.ManagedDataAccess via NuGet |

---

## 📋 Checklist Semaine 1

**Dev A - Base de données**:
- [ ] 6 tables créées (Users, UserSessions, Messages, Forums, UserActivity, Logs)
- [ ] Indexes créés sur UserID, Timestamp, Status
- [ ] Séquences créées (seq_message, seq_activity, seq_log)
- [ ] 4 procédures/fonctions créées (UpdateUserActivity, GetOnlineUsers, CreateUserSession, ValidateSession)
- [ ] Données de test dans Forums (3 forums)
- [ ] ✅ Test: Requête `SELECT * FROM Forums` retourne 3 lignes

**Dev B - Structure VB.NET**:
- [ ] Projet VB.NET créé avec structure 3 couches
- [ ] Dossiers créés (Presentation, BusinessLogic, DataAccess)
- [ ] 4 classes Models créées (User, Message, Forum, Session)
- [ ] DatabaseConnection.vb et SessionManager.vb créées
- [ ] UserDataAccess.vb créée
- [ ] Oracle.ManagedDataAccess installé via NuGet
- [ ] Projet compile sans erreur (Ctrl+Shift+B)

**Dev A + Dev B**:
- [ ] ✅ Test de connexion Oracle réussit
- [ ] ✅ Formulaire de test affiche "Connexion réussie!"
- [ ] ✅ Utilisateur test récupéré de la BD

---

## 🎓 Concepts clés à retenir

1. **Architecture 3 couches = Organisation du code**
   - Chaque couche a une responsabilité unique
   - Les couches communiquent de bas en haut

2. **Développement en parallèle**
   - Dev A crée la BD (couche 3)
   - Dev B crée l'interface (couche 1) et modèles (couche 2)
   - Aucune dépendance entre eux!

3. **Paramètres SQL = Sécurité**
   - JAMAIS concaténer les variables dans les requêtes
   - Toujours utiliser `:paramName` avec `AddWithValue`

4. **Using = Bonne gestion des ressources**
   - Garantit que la connexion est fermée
   - Même en cas d'erreur

---

## 📚 Ressources

- [Oracle SQL Documentation](https://docs.oracle.com/database/oracle19c/index.html)
- [VB.NET Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/visual-basic/)
- [Oracle Data Provider for .NET](https://www.oracle.com/database/technologies/appdev/dotnet/)

---

**À la fin de Semaine 1, vous aurez une FONDATION SOLIDE pour construire le reste de l'application! 🚀**
