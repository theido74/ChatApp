# Plan de Développement - Projet Chat VB.NET & Oracle SQL
## Durée: 5 semaines (5 sessions de 4 heures = 20 heures total)
**Équipe**: 2 développeurs étudiants en première année

---

## 1. Vue d'ensemble du projet

**Objectif**: Créer une application de chat robuste et sécurisée en VB.NET (Windows Forms) avec backend Oracle SQL.

**Fonctionnalités principales**:
- ✅ Login/Authentification sécurisée
- ✅ Forum (conversations de groupe)
- ✅ Messages privés (conversations 1-à-1)
- ✅ Détection des utilisateurs en ligne (via ping SQL toutes les 30 sec)
- ✅ Recherche de messages
- ✅ Logging des actions (audit)

**Stack technique**:
- Langage: VB.NET
- Interface: Windows Forms (WinForms)
- Base de données: Oracle SQL
- Authentification: Sessions utilisateur
- Architecture: 3 couches (Présentation / Métier / Données)

**Considérations de sécurité**:
- Hachage des mots de passe (SHA-256 ou bcrypt)
- Validation des entrées (anti-injection SQL)
- Gestion sécurisée des sessions
- Logs d'authentification

---

## 2. Structure de la base de données

**Tables existantes**:
- `Users` (déjà créée par le professeur)

**Tables à créer**:
1. **UserSessions** - Gestion des sessions actives
   - SessionID (PK), UserID (FK), LoginTime, LastActivityTime, IsActive

2. **Messages** - Tous les messages (forum + privés)
   - MessageID (PK), SenderID (FK), RecipientID (FK nullable), Content, Timestamp, IsPrivate

3. **Forums** - Catégories de forums
   - ForumID (PK), ForumName, Description, CreatedDate

4. **ForumMessages** - Messages du forum (optionnel, ou lier à Messages)
   - ForumMessageID (PK), ForumID (FK), MessageID (FK), ThreadID

5. **UserActivity** - Pings pour détection en ligne
   - ActivityID (PK), UserID (FK), LastPingTime, Status (online/offline)

6. **Logs** - Audit et logging
   - LogID (PK), UserID (FK nullable), Action, Timestamp, Details

---

## 3. Plan sur 5 semaines

### **Semaine 1 (Session 1): Mise en place et Infrastructure (4h)**

**Objectifs**:
- Créer la structure de la BD Oracle complète
- Configurer le projet VB.NET
- Mettre en place l'architecture 3 couches
- Commencer l'authentification

**Tâches (par développeur)**:

*Développeur A - Base de données*:
- Créer toutes les tables Oracle (Users, UserSessions, Messages, Forums, UserActivity, Logs)
- Ajouter les constraints, clés étrangères, et indexes
- Créer les procédures stockées pour les pings (détection en ligne)
- Tester la connexion Oracle depuis VB.NET

*Développeur B - Structure VB.NET*:
- Créer le projet WinForms VB.NET
- Implémenter la couche Données (DataAccess) avec classe Oracle
- Créer les classes métier (User, Message, Forum, Session)
- Implémenter la classe SessionManager pour gérer les sessions actives

**Livrables**:
- ✅ Schéma BD complet et validé
- ✅ Projet VB.NET structuré (3 couches)
- ✅ Connexion Oracle fonctionnelle

---

### **Semaine 2 (Session 2): Authentification et Interface Login (4h)**

**Objectifs**:
- Implémenter le login sécurisé
- Créer la première interface utilisateur
- Gérer les sessions

**Tâches (par développeur)**:

*Développeur A - Authentification*:
- Implémenter le hachage des mots de passe (SHA-256)
- Créer la procédure stockée d'authentification
- Implémenter la classe AuthenticationService
- Valider les entrées (longueur, caractères spéciaux)

*Développeur B - Interface*:
- Créer le formulaire Login (LoginForm)
- Implémenter la validation côté client
- Créer le formulaire principal (MainForm) après connexion
- Ajouter le menu de navigation (Forum / Messages privés / Profil)

**Livrables**:
- ✅ Login fonctionnel et sécurisé
- ✅ Gestion des sessions
- ✅ Interface utilisateur de base

---

### **Semaine 3 (Session 3): Forum et Messages Publics (4h)**

**Objectifs**:
- Implémenter l'affichage et la création de messages du forum
- Gérer les threads/discussions
- Pagination des messages

**Tâches (par développeur)**:

*Développeur A - Requêtes Forum*:
- Créer les procédures stockées:
  - GetForums()
  - GetForumMessages(ForumID, PageNum)
  - InsertMessage(UserID, Content, ForumID)
  - DeleteMessage(MessageID, UserID)
- Implémenter la classe ForumService

*Développeur B - Interface Forum*:
- Créer ForumForm avec liste des forums
- Créer le formulaire d'affichage des messages
- Implémenter le formulaire de création de message
- Ajouter les contrôles ListBox/DataGridView pour les messages

**Livrables**:
- ✅ Forum fonctionnel avec CRUD complet
- ✅ Affichage paginé des messages

---

### **Semaine 4 (Session 4): Messages Privés et Détection en Ligne (4h)**

**Objectifs**:
- Implémenter les messages privés
- Ajouter la détection en ligne (ping toutes les 30 sec)
- Implémenter la recherche de messages

**Tâches (par développeur)**:

*Développeur A - Messages Privés + Détection*:
- Créer les procédures stockées:
  - GetPrivateConversations(UserID)
  - GetPrivateMessages(UserID1, UserID2, PageNum)
  - InsertPrivateMessage(SenderID, RecipientID, Content)
  - UpdateUserActivity(UserID) - ping
  - GetOnlineUsers()
- Implémenter la classe MessageService
- Implémenter la classe ActivityTracker (timer pour pings)

*Développeur B - Interface Messages Privés*:
- Créer PrivateMessagesForm (liste des conversations)
- Créer ConversationForm (chat avec un utilisateur)
- Implémenter la recherche de messages (SearchForm)
- Ajouter le timer pour les pings (30 sec)
- Afficher les utilisateurs en ligne

**Livrables**:
- ✅ Messages privés fonctionnels
- ✅ Détection en ligne automatique
- ✅ Recherche de messages

---

### **Semaine 5 (Session 5): Logging, Optimisations, Tests et Documentation (4h)**

**Objectifs**:
- Implémenter le logging complet
- Optimiser les performances
- Tester l'application
- Sécuriser et documenter

**Tâches (par développeur)**:

*Développeur A - Logging et Optimisations*:
- Créer la classe LoggingService
- Implémenter l'enregistrement de toutes les actions (login, messages, erreurs)
- Ajouter les indexes Oracle pour les requêtes critiques
- Optimiser les requêtes lentes (EXPLAIN PLAN)
- Gestion des erreurs (try-catch global)

*Développeur B - Tests et Interface*:
- Tester tous les scénarios (login, forum, messages, recherche)
- Tester les cas d'erreur (BD indisponible, entrées invalides)
- Ajouter des messages d'erreur conviviales
- Créer une page d'administration simple (Logs, Utilisateurs actifs)
- Rédiger la documentation utilisateur

**Livrables**:
- ✅ Application complète et testée
- ✅ Logs complets et sécurité validée
- ✅ Documentation

---

## 4. Répartition des tâches par semaine

| Semaine | Développeur A | Développeur B |
|---------|---------------|---------------|
| 1 | BD + Procédures | Architecture + Classes métier |
| 2 | Authentification + Hachage | Interface Login + Navigation |
| 3 | Forum StoredProcs | Forum UI |
| 4 | Messages Privés + Ping | Messages Privés UI + Recherche |
| 5 | Logging + Optimisations | Tests + Documentation |

---

## 5. Points critiques de sécurité

- ✅ **Mots de passe**: Hachage SHA-256 (ou bcrypt avec salt)
- ✅ **Injection SQL**: Utiliser les paramètres (@param) dans toutes les requêtes
- ✅ **Sessions**: Vérifier SessionID valide avant chaque action
- ✅ **Données sensibles**: Jamais logger les mots de passe
- ✅ **Timeouts**: Sessions expirées après 1h d'inactivité

---

## 6. Checklist par étape

**Avant Semaine 1**:
- [ ] Accès à Oracle SQL configuré
- [ ] VB.NET 2019+ installé
- [ ] Outils: SQL Developer ou SQL*Plus

**Avant Semaine 2**:
- [ ] BD complètement créée et testée
- [ ] Connexion Oracle depuis VB.NET confirmée
- [ ] Architecture 3 couches validée

**Avant Semaine 3**:
- [ ] Login fonctionnel et sécurisé
- [ ] MainForm naviguée sans erreurs

**Avant Semaine 4**:
- [ ] Forum complet (afficher, créer, modifier, supprimer)
- [ ] Pas de bugs critiques

**Avant Semaine 5**:
- [ ] Messages privés et détection en ligne fonctionnels
- [ ] Recherche de messages testée

**Avant soumission**:
- [ ] 100% des fonctionnalités testées
- [ ] Documentation complète
- [ ] Logs actifs
- [ ] Code commenté (méthodes complexes)

---

## 7. Ressources et conseils

**Documentation**:
- Oracle SQL: https://docs.oracle.com/
- VB.NET WinForms: https://learn.microsoft.com/
- Sécurité: OWASP Top 10

**Astuces VB.NET**:
- Utiliser `Using` pour les connexions BD (auto-dispose)
- Implémenter `IDisposable` pour les ressources
- Timer control pour les pings (System.Windows.Forms.Timer)

**Astuces Oracle**:
- Ajouter des indexes sur UserID, Timestamp
- Utiliser la pagination pour éviter les gros resultsets
- Créer des views pour les requêtes complexes

**Gestion de version** (optionnel):
- Git local ou cloud (GitHub, GitLab)
- Un branch par fonctionnalité

---

**Bonne chance ! Vous avez une excellente feuille de route. Posez des questions si besoin ! 🚀**
