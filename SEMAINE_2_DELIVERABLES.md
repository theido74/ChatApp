# 📦 Livrables Sprint 2 - Semaine 2

## 📅 Sprint 2: Authentification & Interface Login

**Durée**: 4 heures session + 20-40h travail  
**Équipe**: Dev A (Backend), Dev B (Frontend), Dev C (QA/Tests 50%)  
**Objectif**: Implémenter le login sécurisé avec interface WinForms  

---

## 📋 Documentation Créée

### 1. Plan Détaillé - `SEMAINE_2_DETAIL.md`
- Objectifs Sprint 2
- Répartition des rôles
- Tâches détaillées avec estimations (Dev A, B, C)
- Dépendances entre tâches
- Planning jour par jour
- Points critiques de sécurité
- Checklist de fin Sprint

### 2. Definition of Done - `SCRUM_ChatApp_DefinitionOfDone.md`
- Critères d'acceptation pour chaque tâche
- Tests requis (unitaires, intégration, sécurité)
- Checklist avant fin Sprint
- Métriques de qualité (70%+ coverage, 0 bugs critiques)
- Points de sécurité obligatoires

### 3. Fichiers Scrum (Format CSV - Importables dans Excel)

#### a) Product Backlog - `SCRUM_ChatApp_Sprint2_Backlog.csv`
```
ID | Fonctionnalité | Description | Priorité | Estimation | Sprint | Statut
F-001 | Authentification & Login | (détails) | Haute | 12h | Sprint 2 | En cours
F-002 | Forum public | (détails) | Haute | 12h | Sprint 3 | Planifié
...
```

#### b) User Stories - `SCRUM_ChatApp_Sprint2_UserStories.csv`
```
10 User Stories détaillées avec:
- ID (US-01 à US-10)
- Titre et description
- Développeur assigné
- Estimation en heures
- Statut
- Critères d'acceptation
- Dépendances
```

**User Stories principales:**
- US-01: Hachage SHA-256 (Dev A, 3h)
- US-02: Procédures Oracle (Dev A, 3h)
- US-03: AuthenticationService (Dev A, 3h)
- US-04: Validation des entrées (Dev A+B, 3h)
- US-05: LoginForm UI (Dev B, 3h)
- US-06: MainForm & Navigation (Dev B, 2h)
- US-07: SessionManager (Dev B, 2h)
- US-08: Tests Unitaires (Dev C, 3h)
- US-09: Tests Intégration (Dev C, 2h)
- US-10: Tests Sécurité (Dev C, 3h)

#### c) Team & Workload - `SCRUM_ChatApp_Sprint2_Team.csv`
```
Dev A (Backend/BD/Auth):
  - Disponibilité: 16h (full-time)
  - Tâches: 1.1, 1.2, 1.3, 1.4
  - Heures estimées: 11h
  - Utilisation: 68%

Dev B (Frontend/UI):
  - Disponibilité: 16h (full-time)
  - Tâches: 2.1, 2.2, 2.3, 2.4
  - Heures estimées: 9h
  - Utilisation: 56%

Dev C (QA/Tests):
  - Disponibilité: 8h (part-time 50%)
  - Tâches: 3.1, 3.2, 3.3, 3.4, 3.5
  - Heures estimées: 5h
  - Utilisation: 62%
```

#### d) Burn-Down Chart - `SCRUM_ChatApp_Sprint2_BurndownChart.csv`
```
Jour | Heures Prévues | Heures Réelles | Notes
0    | 25h            | 25h            | Démarrage
1    | 23.75h         |                |
...
20   | 0h             |                | Fin Sprint
```

**Burn rate idéal: 1.25 h/jour**

#### e) Risques & Mitigation - `SCRUM_ChatApp_Sprint2_Risques.csv`
```
6 risques identifiés avec impact/probabilité/mitigation:
- R1: Oracle BD indisponible (Critique/Moyen)
- R2: Faille sécurité injection SQL (Critique/Moyen)
- R3: Performance queries (Moyen/Bas)
- R4: Retard intégration UI-BD (Moyen/Moyen)
- R5: Surcharge Dev C (Moyen/Moyen)
- R6: Manque documentation (Bas/Moyen)
```

### 4. Vue Complète Scrum - `SCRUM_ChatApp_Sprint2_COMPLET.html`
Fichier HTML avec tous les éléments Scrum:
- Product Backlog
- User Stories Sprint 2
- Équipe & Répartition
- Risques & Mitigation
- Burn-Down Chart Data
- Clés Sprint 2

---

## 🎯 Tâches Sprint 2 (25 heures totales)

### Phase 1: Backend Authentification (Dev A - 11 heures)

**1.1 Hachage SHA-256** (3h)
- Classe `PasswordHasher.vb`
- Méthodes: `HashPassword()`, `VerifyPassword()`
- Utiliser salt aléatoire
- Tests unitaires 100% coverage

**1.2 Procédures Oracle** (3h)
- `sp_AuthenticateUser(username, password_hash, session_id)`
- `sp_ValidateSession(session_id)`
- `sp_LogoutUser(session_id)`
- Testées et documentées

**1.3 AuthenticationService** (3h)
- Classe `AuthenticationService.vb`
- Méthodes: `Authenticate()`, `ValidateSession()`, `Logout()`, `ChangePassword()`
- Intégration avec StoredProcs
- Gestion erreurs complète

**1.4 InputValidator** (2h)
- Classe `InputValidator.vb`
- Validation username/password
- Anti-injection SQL
- Tests sécurité

### Phase 2: Frontend Interface (Dev B - 9 heures)

**2.1 LoginForm** (3h)
- Formulaire WinForms
- TextBox username/password
- Boutons connexion/quitter
- Validation côté client
- Enter key support

**2.2 MainForm** (2h)
- Menu principal navigable
- MenuStrip: Forum, Messages, Profil, Déconnexion
- Afficher utilisateur connecté
- Barre de statut

**2.3 ClientValidator** (2h)
- Validation côté client avant envoi
- Messages d'erreur conviviaux
- Classe réutilisable

**2.4 SessionManager** (2h)
- Classe `SessionManager.vb` (Singleton)
- Propriétés: CurrentUserID, CurrentSessionID, IsLoggedIn
- Timeout 1h d'inactivité
- Gestion sessions actives

### Phase 3: QA/Tests (Dev C - 5 heures)

**3.1 Tests Unitaires PasswordHasher** (2h)
- Test class NUnit/MSTest
- 100% coverage
- Cas erreur (null, vide)
- Salt unique validation

**3.2 Tests Unitaires AuthService** (3h)
- Tests authentification valide
- Tests rejet credentials
- Tests gestion sessions
- Mocks BD

**3.3 Tests Intégration** (3h)
- Flux complet: Login → BD → Session
- Tests timeout
- Logout validation
- Erreurs BD gérées

**3.4 Tests Sécurité** (2h)
- Injection SQL: `' OR '1'='1`
- Brute-force: tentatives rapides
- Session hijacking
- Password en logs (négatif)

**3.5 Documentation Tests** (2h)
- Document `TEST_CASES_SEMAINE2.md`
- Scénarios Happy path + erreurs
- Résultats attendus
- Evidence de tests

---

## 🔒 Critères de Sécurité Obligatoires

✅ **Authentification**
- Mots de passe hachés (SHA-256 + salt)
- Aucun password en clair nulle part
- Vérification avec `VerifyPassword()` uniquement

✅ **Injection SQL**
- Paramètres (@) dans TOUTES les requêtes
- Validation input (longueur + caractères)
- Tests avec payloads malveillants

✅ **Sessions**
- SessionID unique et aléatoire
- Timeout 1h d'inactivité
- Validation avant chaque action
- Déconnexion sécurisée

✅ **Messages d'erreur**
- "Nom d'utilisateur ou mot de passe incorrect" (générique)
- Pas de révélation d'info (user exists? format?)

✅ **Logging**
- Tentatives connexion échouées loggées
- Jamais de password dans logs
- Timestamp + username + action

---

## 📊 Métriques de Qualité

| Métrique | Cible | Vérification |
|----------|-------|------------|
| Code Coverage | 70%+ | Tests PasswordHasher + AuthService |
| Tests Passed | 100% | 0 failing tests |
| Security Tests | 100% | Injection SQL, brute-force |
| Code Review | 1+ approval | Avant merge |
| Documentation | 100% | Toutes tâches documentées |

---

## ✅ Checklist Fin Sprint 2

**Avant la démo**:
- [ ] Tous les US marqués "Done"
- [ ] Tests d'intégration réussis
- [ ] Tests de sécurité validés
- [ ] Code review complète
- [ ] Documentation finalisée
- [ ] Excel Scrum à jour
- [ ] Aucun bug critique
- [ ] Prêt pour Semaine 3

**Git & Code**:
- [ ] Commits descriptifs (co-author trailer)
- [ ] Pas de TODOs oubliés
- [ ] Pas d'erreurs non gérées
- [ ] Standards architecturaux respectés
- [ ] README.md à jour

---

## 📥 Comment utiliser les fichiers

### Importer dans Excel
1. Ouvrir Excel
2. Données > Depuis un fichier texte
3. Sélectionner `SCRUM_ChatApp_Sprint2_*.csv`
4. Format délimité (virgules)
5. Fusionner tous les CSV dans un workbook

### Visualiser le HTML
- Ouvrir `SCRUM_ChatApp_Sprint2_COMPLET.html` dans un navigateur
- Vue complète de tous les éléments Scrum
- Imprimable pour affichage physique

### Tracker les tâches
- Utiliser plan.md (session folder) pour détails
- CSV pour suivi quantitatif
- HTML pour présentations

---

## 📚 Ressources Complémentaires

- `SEMAINE_1_DETAIL.md` - Architecture 3 couches (contexte)
- `PLAN_DEVELOPPEMENT.md` - Plan 5 semaines complet
- `SCRUM_ChatApp_DefinitionOfDone.md` - Critères acceptation
- `TEST_CASES_SEMAINE2.md` - Scénarios détaillés (à créer par Dev C)

---

## 🎬 Prochaines Étapes (Semaine 3)

- Forum public (CRUD complet)
- Procédures Forum
- Interface ForumForm
- Pagination messages
- Tests forum complets

---

**Sprint 2 Créé**: 2026-04-24  
**Équipe**: Dev A + Dev B + Dev C (50%)  
**Statut**: 📋 Plan & Estimation terminés, prêt pour implémentation  
**Burn Rate**: 1.25 h/jour (Idéal)  
**Total Capacity**: 40 heures (16+16+8)  
**Total Planned**: 25 heures (62.5% utilisation)
