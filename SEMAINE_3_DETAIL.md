# 📋 Plan Détaillé Semaine 3 - Forum Public & Messages Privés

## Vue d'ensemble du projet
- **Projet**: ChatApp - Application VB.NET & Oracle SQL
- **Objectif Semaine 3**: Implémenter le forum public et les messages privés
- **Équipe**: 3 développeurs (2 full-time + 1 part-time QA/Tests)
- **Durée**: 4 heures de session + 25-35h de travail total
- **Statut Prérequis**: Semaine 2 complétée (Authentification + Login)

---

## 🎯 Objectifs Semaine 3

1. ✅ Créer la structure des tables Oracle pour Forum et Messages
2. ✅ Implémenter les procédures stockées pour Forum
3. ✅ Implémenter les procédures stockées pour Messages Privés
4. ✅ Créer ForumService (backend)
5. ✅ Créer PrivateMessageService (backend)
6. ✅ Créer interfaces UI pour Forum
7. ✅ Créer interfaces UI pour Messages Privés
8. ✅ Tests complets (unitaires + intégration)

---

## 👥 Répartition des rôles

### Développeur A (Full-time) - Backend & BD
**Responsabilités**:
- Créer les tables Oracle (Forums, ForumMessages, Messages)
- Implémenter procédures stockées Forum
- Implémenter procédures stockées Messages Privés
- Créer ForumService.vb
- Créer PrivateMessageService.vb
- **Estimation**: 10h total

### Développeur B (Full-time) - Frontend & UI
**Responsabilités**:
- Créer ForumListForm.vb
- Créer ForumThreadForm.vb
- Créer PostMessageForm.vb
- Créer InboxForm.vb
- Créer ChatForm.vb
- Créer SendMessageForm.vb
- Intégrer au MainForm
- **Estimation**: 10h total

### Développeur C (Part-time, 50%) - QA/Tests
**Responsabilités**:
- Tests unitaires ForumService
- Tests unitaires PrivateMessageService
- Tests d'intégration Forum (BD → UI)
- Tests d'intégration Messages
- Documentation TEST_CASES_SEMAINE3.md
- **Estimation**: 5h total

---

## 📊 Tâches Détaillées par Développeur

### PHASE 1: Backend - Structures BD & Procédures (Dev A - 5h)

#### Tâche 1.1: Créer les tables Oracle
- Créer `Forums` table
  - ForumID (PK), ForumName, Description, CreatedDate, IsActive
- Créer `ForumMessages` table (pour les messages du forum)
  - MessageID (PK), ForumID (FK), SenderID (FK), Content, CreatedDate, LastModified
- Créer `Messages` table (messages privés)
  - MessageID (PK), SenderID (FK), RecipientID (FK), Content, CreatedDate, IsRead
- **Livrables**: Tables créées et testées en Oracle
- **Dépendances**: Table Users (Semaine 1)
- **Durée estimée**: 1h

**Code SQL Oracle**:
```sql
-- ============================================
-- Table 1: Forums
-- ============================================
CREATE TABLE Forums (
    ForumID NUMBER PRIMARY KEY,
    ForumName VARCHAR2(100) NOT NULL UNIQUE,
    Description VARCHAR2(500),
    CreatedDate DATE DEFAULT SYSDATE,
    IsActive NUMBER DEFAULT 1
);

CREATE SEQUENCE forums_seq START WITH 1 INCREMENT BY 1;

-- ============================================
-- Table 2: ForumMessages (messages du forum)
-- ============================================
CREATE TABLE ForumMessages (
    MessageID NUMBER PRIMARY KEY,
    ForumID NUMBER NOT NULL REFERENCES Forums(ForumID) ON DELETE CASCADE,
    SenderID NUMBER NOT NULL REFERENCES Users(UserID) ON DELETE CASCADE,
    Content VARCHAR2(4000) NOT NULL,
    CreatedDate DATE DEFAULT SYSDATE,
    LastModified DATE DEFAULT SYSDATE
);

CREATE SEQUENCE forum_messages_seq START WITH 1 INCREMENT BY 1;

-- ============================================
-- Table 3: Messages (messages privés)
-- ============================================
CREATE TABLE Messages (
    MessageID NUMBER PRIMARY KEY,
    SenderID NUMBER NOT NULL REFERENCES Users(UserID) ON DELETE CASCADE,
    RecipientID NUMBER NOT NULL REFERENCES Users(UserID) ON DELETE CASCADE,
    Content VARCHAR2(4000) NOT NULL,
    CreatedDate DATE DEFAULT SYSDATE,
    IsRead NUMBER DEFAULT 0
);

CREATE SEQUENCE messages_seq START WITH 1 INCREMENT BY 1;

-- Index pour performance
CREATE INDEX idx_forum_messages_forum_id ON ForumMessages(ForumID);
CREATE INDEX idx_forum_messages_sender_id ON ForumMessages(SenderID);
CREATE INDEX idx_messages_recipient_id ON Messages(RecipientID);
CREATE INDEX idx_messages_sender_id ON Messages(SenderID);
```

#### Tâche 1.2: Procédures stockées Forum
- Procédure `sp_CreateForum(p_name, p_description)`
- Procédure `sp_GetAllForums()` (retourner liste)
- Procédure `sp_PostForumMessage(p_forum_id, p_sender_id, p_content)`
- Procédure `sp_GetForumMessages(p_forum_id, p_limit)`
- Procédure `sp_DeleteForumMessage(p_message_id, p_sender_id)`
- **Livrables**: Procédures testées
- **Dépendances**: Tâche 1.1
- **Durée estimée**: 2h

**Code SQL Oracle**:
```sql
-- ============================================
-- Procédure: sp_CreateForum
-- ============================================
CREATE OR REPLACE PROCEDURE sp_CreateForum(
    p_name IN VARCHAR2,
    p_description IN VARCHAR2,
    p_forum_id OUT NUMBER,
    p_result OUT VARCHAR2
)
IS
BEGIN
    p_result := 'ERROR';
    
    -- Vérifier que le forum n'existe pas déjà
    SELECT COUNT(*) INTO p_forum_id
    FROM Forums WHERE ForumName = p_name;
    
    IF p_forum_id > 0 THEN
        p_result := 'FORUM_EXISTS';
        RETURN;
    END IF;
    
    -- Créer le forum
    SELECT forums_seq.NEXTVAL INTO p_forum_id FROM DUAL;
    
    INSERT INTO Forums(ForumID, ForumName, Description, IsActive)
    VALUES(p_forum_id, p_name, p_description, 1);
    
    COMMIT;
    p_result := 'SUCCESS';
    
EXCEPTION
    WHEN OTHERS THEN
        p_result := 'ERROR: ' || SQLERRM;
        ROLLBACK;
        p_forum_id := NULL;
END sp_CreateForum;
/

-- ============================================
-- Procédure: sp_GetForumMessages
-- ============================================
CREATE OR REPLACE PROCEDURE sp_GetForumMessages(
    p_forum_id IN NUMBER,
    p_limit IN NUMBER DEFAULT 50
)
IS
BEGIN
    SELECT m.MessageID, m.SenderID, u.Username, m.Content, m.CreatedDate
    FROM ForumMessages m
    JOIN Users u ON m.SenderID = u.UserID
    WHERE m.ForumID = p_forum_id
    ORDER BY m.CreatedDate DESC
    FETCH FIRST p_limit ROWS ONLY;
    
EXCEPTION
    WHEN OTHERS THEN
        NULL;
END sp_GetForumMessages;
/

-- ============================================
-- Procédure: sp_PostForumMessage
-- ============================================
CREATE OR REPLACE PROCEDURE sp_PostForumMessage(
    p_forum_id IN NUMBER,
    p_sender_id IN NUMBER,
    p_content IN VARCHAR2,
    p_message_id OUT NUMBER,
    p_result OUT VARCHAR2
)
IS
    v_forum_exists NUMBER;
BEGIN
    p_result := 'ERROR';
    p_message_id := NULL;
    
    -- Vérifier que le forum existe
    SELECT COUNT(*) INTO v_forum_exists
    FROM Forums WHERE ForumID = p_forum_id AND IsActive = 1;
    
    IF v_forum_exists = 0 THEN
        p_result := 'FORUM_NOT_FOUND';
        RETURN;
    END IF;
    
    -- Créer le message
    SELECT forum_messages_seq.NEXTVAL INTO p_message_id FROM DUAL;
    
    INSERT INTO ForumMessages(MessageID, ForumID, SenderID, Content)
    VALUES(p_message_id, p_forum_id, p_sender_id, p_content);
    
    -- Log
    INSERT INTO Logs(UserID, Action, Timestamp, Details)
    VALUES(p_sender_id, 'POST_FORUM_MESSAGE', SYSDATE, 'ForumID: ' || p_forum_id);
    
    COMMIT;
    p_result := 'SUCCESS';
    
EXCEPTION
    WHEN OTHERS THEN
        p_result := 'ERROR: ' || SQLERRM;
        ROLLBACK;
        p_message_id := NULL;
END sp_PostForumMessage;
/
```

#### Tâche 1.3: Procédures stockées Messages Privés
- Procédure `sp_SendPrivateMessage(p_sender_id, p_recipient_id, p_content)`
- Procédure `sp_GetPrivateMessages(p_user_id, p_from_user_id, p_limit)`
- Procédure `sp_MarkMessageAsRead(p_message_id)`
- Procédure `sp_GetUnreadMessageCount(p_user_id)`
- Procédure `sp_DeletePrivateMessage(p_message_id, p_user_id)`
- **Livrables**: Procédures testées
- **Dépendances**: Tâche 1.1
- **Durée estimée**: 2h

**Code SQL Oracle**:
```sql
-- ============================================
-- Procédure: sp_SendPrivateMessage
-- ============================================
CREATE OR REPLACE PROCEDURE sp_SendPrivateMessage(
    p_sender_id IN NUMBER,
    p_recipient_id IN NUMBER,
    p_content IN VARCHAR2,
    p_message_id OUT NUMBER,
    p_result OUT VARCHAR2
)
IS
    v_recipient_exists NUMBER;
BEGIN
    p_result := 'ERROR';
    p_message_id := NULL;
    
    -- Vérifier que le destinataire existe
    SELECT COUNT(*) INTO v_recipient_exists
    FROM Users WHERE UserID = p_recipient_id AND IsActive = 1;
    
    IF v_recipient_exists = 0 THEN
        p_result := 'RECIPIENT_NOT_FOUND';
        RETURN;
    END IF;
    
    -- Créer le message
    SELECT messages_seq.NEXTVAL INTO p_message_id FROM DUAL;
    
    INSERT INTO Messages(MessageID, SenderID, RecipientID, Content, IsRead)
    VALUES(p_message_id, p_sender_id, p_recipient_id, p_content, 0);
    
    COMMIT;
    p_result := 'SUCCESS';
    
EXCEPTION
    WHEN OTHERS THEN
        p_result := 'ERROR: ' || SQLERRM;
        ROLLBACK;
        p_message_id := NULL;
END sp_SendPrivateMessage;
/

-- ============================================
-- Procédure: sp_GetUnreadMessageCount
-- ============================================
CREATE OR REPLACE PROCEDURE sp_GetUnreadMessageCount(
    p_user_id IN NUMBER,
    p_count OUT NUMBER
)
IS
BEGIN
    SELECT COUNT(*) INTO p_count
    FROM Messages
    WHERE RecipientID = p_user_id AND IsRead = 0;
    
EXCEPTION
    WHEN OTHERS THEN
        p_count := 0;
END sp_GetUnreadMessageCount;
/

-- ============================================
-- Procédure: sp_MarkMessageAsRead
-- ============================================
CREATE OR REPLACE PROCEDURE sp_MarkMessageAsRead(
    p_message_id IN NUMBER,
    p_result OUT VARCHAR2
)
IS
BEGIN
    p_result := 'ERROR';
    
    UPDATE Messages
    SET IsRead = 1
    WHERE MessageID = p_message_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        COMMIT;
        p_result := 'SUCCESS';
    ELSE
        p_result := 'MESSAGE_NOT_FOUND';
    END IF;
    
EXCEPTION
    WHEN OTHERS THEN
        p_result := 'ERROR: ' || SQLERRM;
        ROLLBACK;
END sp_MarkMessageAsRead;
/
```

---

### PHASE 2: Backend - Services Métier (Dev A - 2-3h)

#### Tâche 1.4: Implémenter ForumService
- Créer `ForumService.vb` dans la couche Métier
- Méthodes:
  - `GetAllForums() -> List(Of Forum)`
  - `CreateForum(name As String, description As String) -> Integer`
  - `PostMessage(forumId As Integer, userId As Integer, content As String) -> Boolean`
  - `GetForumMessages(forumId As Integer) -> List(Of Message)`
  - `DeleteMessage(messageId As Integer, userId As Integer) -> Boolean`
- **Livrables**: Service intégré et testable
- **Dépendances**: Tâche 1.2, 1.3
- **Durée estimée**: 2h

**Code Exemple**:
```vb
' ForumService.vb - COUCHE MÉTIER
Public Class ForumService
    Private dbAccess As New ForumDataAccess()
    
    ''' <summary>
    ''' Récupère tous les forums disponibles
    ''' </summary>
    Public Function GetAllForums() As List(Of Forum)
        Try
            Return dbAccess.GetAllForums()
        Catch ex As Exception
            LogError("GetAllForums", ex)
            Return New List(Of Forum)()
        End Try
    End Function
    
    ''' <summary>
    ''' Crée un nouveau forum
    ''' </summary>
    Public Function CreateForum(name As String, description As String) As Integer
        If String.IsNullOrWhiteSpace(name) OrElse name.Length > 100 Then
            Throw New ArgumentException("Forum name doit être entre 1-100 caractères")
        End If
        
        Try
            Return dbAccess.CreateForum(name, description)
        Catch ex As Exception
            LogError("CreateForum", ex)
            Throw
        End Try
    End Function
    
    ''' <summary>
    ''' Poste un message dans un forum
    ''' </summary>
    Public Function PostMessage(forumId As Integer, userId As Integer, content As String) As Boolean
        If String.IsNullOrWhiteSpace(content) OrElse content.Length > 4000 Then
            Throw New ArgumentException("Contenu invalide (1-4000 caractères)")
        End If
        
        Try
            Return dbAccess.PostForumMessage(forumId, userId, content)
        Catch ex As Exception
            LogError("PostMessage", ex)
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Récupère les messages d'un forum
    ''' </summary>
    Public Function GetForumMessages(forumId As Integer) As List(Of Message)
        Try
            Return dbAccess.GetForumMessages(forumId)
        Catch ex As Exception
            LogError("GetForumMessages", ex)
            Return New List(Of Message)()
        End Try
    End Function
    
    Private Sub LogError(method As String, ex As Exception)
        System.Diagnostics.Debug.WriteLine($"[ERROR] ForumService.{method}: {ex.Message}")
    End Sub
End Class

''' <summary>
''' Classe Forum
''' </summary>
Public Class Forum
    Public Property ForumID As Integer
    Public Property ForumName As String
    Public Property Description As String
    Public Property CreatedDate As DateTime
    Public Property IsActive As Boolean
End Class

''' <summary>
''' Classe Message (générique pour forum et privé)
''' </summary>
Public Class Message
    Public Property MessageID As Integer
    Public Property SenderID As Integer
    Public Property SenderName As String
    Public Property Content As String
    Public Property CreatedDate As DateTime
    Public Property IsRead As Boolean
    Public Property RecipientID As Integer ' Pour messages privés
End Class
```

#### Tâche 1.5: Implémenter PrivateMessageService
- Créer `PrivateMessageService.vb`
- Méthodes:
  - `SendMessage(userId As Integer, recipientId As Integer, content As String) -> Boolean`
  - `GetConversation(userId As Integer, withUserId As Integer) -> List(Of Message)`
  - `GetInbox(userId As Integer) -> List(Of User)` (liste des correspondants)
  - `MarkAsRead(messageId As Integer) -> Boolean`
  - `GetUnreadCount(userId As Integer) -> Integer`
- **Livrables**: Service testable
- **Dépendances**: Tâche 1.2, 1.3
- **Durée estimée**: 1-2h

**Code Exemple**:
```vb
' PrivateMessageService.vb - COUCHE MÉTIER
Public Class PrivateMessageService
    Private dbAccess As New MessageDataAccess()
    
    ''' <summary>
    ''' Envoie un message privé
    ''' </summary>
    Public Function SendMessage(userId As Integer, recipientId As Integer, content As String) As Boolean
        If userId = recipientId Then
            Throw New ArgumentException("Impossible d'envoyer un message à soi-même")
        End If
        
        If String.IsNullOrWhiteSpace(content) OrElse content.Length > 4000 Then
            Throw New ArgumentException("Contenu invalide (1-4000 caractères)")
        End If
        
        Try
            Return dbAccess.SendPrivateMessage(userId, recipientId, content)
        Catch ex As Exception
            LogError("SendMessage", ex)
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Récupère la conversation entre deux utilisateurs
    ''' </summary>
    Public Function GetConversation(userId As Integer, withUserId As Integer) As List(Of Message)
        Try
            Dim messages As List(Of Message) = dbAccess.GetPrivateMessages(userId, withUserId)
            
            ' Marquer les messages reçus comme lus
            For Each msg In messages
                If msg.RecipientID = userId AndAlso Not msg.IsRead Then
                    dbAccess.MarkMessageAsRead(msg.MessageID)
                End If
            End Sub
            
            Return messages
        Catch ex As Exception
            LogError("GetConversation", ex)
            Return New List(Of Message)()
        End Try
    End Function
    
    ''' <summary>
    ''' Récupère le nombre de messages non lus
    ''' </summary>
    Public Function GetUnreadCount(userId As Integer) As Integer
        Try
            Return dbAccess.GetUnreadMessageCount(userId)
        Catch ex As Exception
            LogError("GetUnreadCount", ex)
            Return 0
        End Try
    End Function
    
    Private Sub LogError(method As String, ex As Exception)
        System.Diagnostics.Debug.WriteLine($"[ERROR] PrivateMessageService.{method}: {ex.Message}")
    End Sub
End Class
```

---

### PHASE 3: Frontend - Forum UI (Dev B - 5h)

#### Tâche 2.1: ForumListForm - Afficher liste forums
- Créer `ForumListForm.vb`
- Contrôles:
  - ListBox ou DataGridView pour afficher forums
  - Button "Accéder" (ouvrir le forum)
  - Button "Créer Forum" (si admin - future feature)
  - Label pour description du forum sélectionné
- Événements: Double-click ouvre ForumThreadForm
- **Livrables**: Formulaire navigable
- **Dépendances**: Tâche 1.4 (ForumService)
- **Durée estimée**: 1.5h

#### Tâche 2.2: ForumThreadForm - Afficher messages d'un forum
- Créer `ForumThreadForm.vb`
- Contrôles:
  - TextBox affichant le nom du forum
  - ListBox/DataGridView messages avec colonnnes: Auteur, Date, Contenu
  - Button "Répondre" (ouvre PostMessageForm)
  - Button "Rafraîchir"
  - Button "Retour aux forums"
- Auto-refresh toutes les 30 secondes (timer)
- **Livrables**: Affichage messages + refresh automatique
- **Dépendances**: Tâche 1.4
- **Durée estimée**: 2h

#### Tâche 2.3: PostMessageForm - Composer message
- Créer `PostMessageForm.vb`
- Contrôles:
  - RichTextBox pour rédiger le message
  - Button "Envoyer"
  - Button "Annuler"
  - Label pour validation
- Validation: Contenu non-vide, max 4000 caractères
- **Livrables**: Formulaire de composition
- **Dépendances**: Tâche 1.4
- **Durée estimée**: 1h

#### Tâche 2.4: Intégrer Forum au MainForm
- Ajouter menu "Forum" -> affiche ForumListForm
- Placer les formulaires Forum en ordre logique
- **Livrables**: Navigation complète
- **Dépendances**: Tâches 2.1-2.3
- **Durée estimée**: 0.5h

---

### PHASE 4: Frontend - Messages Privés UI (Dev B - 5h)

#### Tâche 3.1: InboxForm - Lister conversations
- Créer `InboxForm.vb`
- Contrôles:
  - DataGridView avec colonnes: Utilisateur, Dernier message, Date, Non-lus
  - Button "Ouvrir conversation"
  - Button "Nouveau message"
  - Label compteur "X messages non-lus"
  - Button "Rafraîchir"
- Auto-refresh toutes les 15 secondes
- **Livrables**: Inbox navigable
- **Dépendances**: Tâche 1.5 (PrivateMessageService)
- **Durée estimée**: 1.5h

#### Tâche 3.2: ChatForm - Conversation 1-à-1
- Créer `ChatForm.vb`
- Contrôles:
  - Panel avec les messages (affichage conversation)
  - TextBox pour rédiger réponse
  - Button "Envoyer"
  - Button "Retour Inbox"
  - Label avec le nom de l'utilisateur
- Style: Messages simples avec timestamp et auteur
- Auto-refresh toutes les 5 secondes
- **Livrables**: Chat fonctionnel
- **Dépendances**: Tâche 1.5
- **Durée estimée**: 2h

#### Tâche 3.3: SendMessageForm - Nouveau message
- Créer `SendMessageForm.vb`
- Contrôles:
  - ComboBox pour sélectionner destinataire (liste des users)
  - RichTextBox pour le contenu
  - Button "Envoyer"
  - Button "Annuler"
- **Livrables**: Formulaire composition
- **Dépendances**: Tâche 1.5
- **Durée estimée**: 1h

#### Tâche 3.4: Intégrer Messages au MainForm
- Ajouter menu "Messages Privés" -> affiche InboxForm
- Notification badge pour messages non-lus
- **Livrables**: Navigation complète messages
- **Dépendances**: Tâches 3.1-3.3
- **Durée estimée**: 0.5h

---

### PHASE 5: Tests (Dev C - 5h)

#### Tâche 4.1: Tests unitaires ForumService
- 8+ test cases:
  - CreateForum (succès, doublon)
  - PostMessage (succès, contenu invalide, forum inexistant)
  - GetAllForums (liste non-vide)
  - **Durée estimée**: 1h

#### Tâche 4.2: Tests unitaires PrivateMessageService
- 8+ test cases:
  - SendMessage (succès, autosend, contenu invalide)
  - GetConversation (messages ordonnés)
  - GetUnreadCount
  - MarkAsRead
  - **Durée estimée**: 1h

#### Tâche 4.3: Tests intégration Forum
- 5+ test cases:
  - Créer forum → vérifier en BD
  - Poster message → vérifier apparaît en UI
  - Auto-refresh messages
  - **Durée estimée**: 1.5h

#### Tâche 4.4: Tests intégration Messages Privés
- 5+ test cases:
  - Envoyer message → apparaît Inbox
  - Marquer lu → disparaît des non-lus
  - Chat refresh automatique
  - **Durée estimée**: 1h

#### Tâche 4.5: Documentation TEST_CASES_SEMAINE3.md
- Documenter tous les scénarios
- Couverture de code: 70% minimum
- **Durée estimée**: 0.5h

---

## 📅 Planning Semaine 3 (Exemple - 20 jours de travail)

| Jour | Dev A - Backend | Dev B - Frontend | Dev C - Tests |
|------|-----------------|------------------|---------------|
| J1-2 | Tâche 1.1 (Tables BD) | Tâche 2.1 (ForumListForm) | Setup tests |
| J3-4 | Tâche 1.2 (Proc Forum) | Tâche 2.2 (ForumThreadForm) | Tests 4.1 |
| J5 | Tâche 1.3 (Proc Messages) | Tâche 2.3 (PostMessageForm) | Tests 4.2 |
| J6-7 | Tâche 1.4 (ForumService) | Tâche 2.4 (Intégration Forum) | Tests 4.3 |
| J8-9 | Tâche 1.5 (PrivateMessageService) | Tâche 3.1-3.2 (Chat) | Tests 4.3-4.4 |
| J10+ | Integration BD + Bug fixes | Tâche 3.3-3.4 (SendMessage) | Tests 4.4-4.5 |

---

## 🔒 Points de Sécurité

1. **Validation contenu**: Vérifier longueur + pas de HTML dangereux
2. **Parameterized queries**: Tous les appels BD
3. **Autorisation**: Seul l'auteur peut supprimer/modifier messages
4. **Rate limiting**: Limiter envoi de messages (future feature)
5. **Logging**: Logger tous les posts forum + messages privés

---

## ⚠️ Dépendances & Risques

### Dépendances externes:
- ✅ Semaine 2 complétée (Authentification)
- ✅ Tables Users + Logs (Semaine 1)

### Risques identifiés:
1. **Performance**: Trop d'utilisateurs = refresh lent → Solution: pagination + indexes
2. **Concurrence**: Deux utilisateurs postent simultanément → Solution: test de charge
3. **UI freeze**: Auto-refresh bloque UI → Solution: threads asynchrones
4. **Spam**: Utilisateurs postent trop rapidement → Solution: add rate-limiting (S4)

---

## 📋 Checklist Fin Sprint 3

- [ ] Toutes les tables créées et testées en BD
- [ ] Procédures stockées testées
- [ ] ForumService testé à 100%
- [ ] PrivateMessageService testé à 100%
- [ ] ForumListForm + ForumThreadForm + PostMessageForm navigables
- [ ] InboxForm + ChatForm + SendMessageForm navigables
- [ ] Tests unitaires passent à 100%
- [ ] Tests intégration passent à 100%
- [ ] Couverture de code ≥ 70%
- [ ] Aucun bug critique
- [ ] Documentation complète
- [ ] MainForm intègre Forum + Messages

---

## 📚 Documents Livrés

1. **SEMAINE_3_DETAIL.md** - Ce document (plan complet)
2. **TEST_CASES_SEMAINE3.md** - Documentation de tous les tests
3. **Code source**:
   - Tables SQL Oracle
   - Procédures stockées
   - ForumService.vb + PrivateMessageService.vb
   - 6 formulaires WinForms
   - Tests unitaires + intégration
4. **Scrum artifacts**:
   - SCRUM_ChatApp_Sprint3_UserStories.csv
   - SCRUM_ChatApp_Sprint3_BurndownChart.csv
   - SCRUM_ChatApp_Sprint3_Team.csv
