# 📋 Guide Complet - Fichiers Scrum Semaine 2

## 🎯 Vue d'ensemble

Suite à votre demande pour la **Semaine 2 du projet ChatApp** avec **l'arrivée d'un 3ème développeur part-time**, tous les éléments Scrum ont été préparés:

- ✅ **Plan détaillé** de la Semaine 2 (Authentification & Login)
- ✅ **Fichiers Scrum complets** (Product Backlog, User Stories, Team, Risks, Burn-down)
- ✅ **Équipe**: 2 devs full-time + 1 QA part-time (50%)
- ✅ **Estimation**: 25 heures de travail sur 40 heures disponibles
- ✅ **Capacité**: Bonne utilisation avec buffer de 15 heures pour imprévus

---

## 📁 Fichiers Disponibles

### 1️⃣ **SEMAINE_2_DETAIL.md** (Principal)
**Contenu**: Plan complet et détaillé de la Semaine 2
- Objectifs
- Répartition des 3 rôles (Dev A, B, C)
- Tâches détaillées par phase (Backend → Frontend → Tests)
- Dépendances entre tâches
- Planning jour par jour
- Points critiques de sécurité
- Checklist fin Sprint

**Quand l'utiliser**: Référence principale pour l'implémentation

---

### 2️⃣ **SEMAINE_2_DELIVERABLES.md**
**Contenu**: Résumé exécutif des livrables
- Vue d'ensemble des 3 phases
- Fichiers Scrum expliqués
- Tâches (25 heures total)
- Critères sécurité obligatoires
- Métriques de qualité
- Checklist fin Sprint

**Quand l'utiliser**: Pour une vue d'ensemble rapide

---

### 3️⃣ **SCRUM_ChatApp_Sprint2_Backlog.csv**
**Contenu**: Product Backlog (10 fonctionnalités)
```
ID | Fonctionnalité | Description | Priorité | Estimation | Sprint | Statut
```

**Utilisation**:
- Importer dans Excel
- Colonne "Statut" à mettre à jour pendant le Sprint
- Référence pour Sprint 3, 4, 5

**User Stories inclus**:
- F-001: Authentification & Login (Sprint 2) ← 👈 FOCUS SEMAINE 2
- F-002 à F-010: Forum, Messages, Détection, Logging, etc.

---

### 4️⃣ **SCRUM_ChatApp_Sprint2_UserStories.csv**
**Contenu**: 10 User Stories détaillées pour Sprint 2

**Colonnes**:
- ID (US-01 à US-10)
- Titre
- Description
- Développeur assigné
- Estimation (heures)
- Statut
- Critères d'acceptation
- Dépendances

**Stories principales**:
| # | Titre | Dev | h | Dépend |
|---|-------|-----|---|--------|
| US-01 | Hachage SHA-256 | Dev A | 3 | - |
| US-02 | Procédures Oracle | Dev A | 3 | - |
| US-03 | AuthenticationService | Dev A | 3 | US-01,02 |
| US-04 | Validation entrées | Dev A+B | 3 | - |
| US-05 | LoginForm UI | Dev B | 3 | US-03 |
| US-06 | MainForm & Nav | Dev B | 2 | US-05 |
| US-07 | SessionManager | Dev B | 2 | US-03 |
| US-08 | Tests Unitaires | Dev C | 3 | US-01,03 |
| US-09 | Tests Intégration | Dev C | 2 | US-02,03,05 |
| US-10 | Tests Sécurité | Dev C | 3 | US-04,05 |

---

### 5️⃣ **SCRUM_ChatApp_Sprint2_Team.csv**
**Contenu**: Répartition de charge par développeur

**Team Setup**:
```
Dev A: Backend / BD / Auth
  Disponibilité: 16h (full-time)
  Tâches: 1.1 (PasswordHasher), 1.2 (StoredProcs), 1.3 (AuthService), 1.4 (InputValidator)
  Total: 11h (68% utilisation)

Dev B: Frontend / UI / Navigation
  Disponibilité: 16h (full-time)
  Tâches: 2.1 (LoginForm), 2.2 (MainForm), 2.3 (ClientValidator), 2.4 (SessionManager)
  Total: 9h (56% utilisation)

Dev C: QA / Tests / Documentation (50% part-time)
  Disponibilité: 8h (part-time)
  Tâches: 3.1 (Tests PasswordHasher), 3.2 (Tests AuthService), 3.3 (Tests Intégration), 3.4 (Tests Sécurité), 3.5 (Doc Tests)
  Total: 5h (62% utilisation)
```

**Utilisation**: Suivi de charge, réallocation si besoin

---

### 6️⃣ **SCRUM_ChatApp_Sprint2_BurndownChart.csv**
**Contenu**: Données Burn-Down Chart (20 jours de Sprint)

**Colonnes**:
- Jour (0-20)
- Heures Restantes Prévues (25h → 0h linéairement)
- Heures Restantes Réelles (à remplir pendant Sprint)
- Notes

**Burn Rate Idéal**: 1.25 h/jour (25h ÷ 20 jours)

**Utilisation**:
1. Importer dans Excel
2. Ajouter un graphique XY (Scatter)
3. Remplir colonne "Réelles" pendant Sprint
4. Comparer avec la courbe prévue

---

### 7️⃣ **SCRUM_ChatApp_Sprint2_Risques.csv**
**Contenu**: Identification des 6 risques du Sprint

**Risques**:
| ID | Risque | Impact | Probabilité | Mitigation | Owner |
|----|----|--------|-------------|-----------|-------|
| R1 | Oracle BD indisponible | Critique | Moyen | Tester local + mock BD | Dev A |
| R2 | Injection SQL | Critique | Moyen | Tests sécurité + code review | Dev C |
| R3 | Performance queries | Moyen | Bas | Indexes dès le départ | Dev A |
| R4 | Retard intégration UI-BD | Moyen | Moyen | Interfaces bien définies | Dev B+A |
| R5 | Surcharge Dev C | Moyen | Moyen | Tests automatisés | Dev C+A |
| R6 | Manque documentation | Bas | Moyen | Doc simultanée au code | Tous |

**Utilisation**: Suivi des risques, escalade si probabilité augmente

---

### 8️⃣ **SCRUM_ChatApp_Sprint2_COMPLET.html**
**Contenu**: Vue complète Scrum en HTML

**Sections**:
1. Product Backlog complet
2. User Stories Sprint 2
3. Team & Équipe
4. Risques & Mitigation
5. Burn-Down Chart data

**Utilisation**:
- Ouvrir dans un navigateur
- Imprimer pour affichage physique
- Partager comme rapport

---

### 9️⃣ **SCRUM_ChatApp_DefinitionOfDone.md**
**Contenu**: Critères d'acceptation pour chaque tâche

**Pour chaque tâche**:
- ✓ Code suivant standards
- ✓ Tests unitaires (70%+ coverage)
- ✓ Code review (1+ approbation)
- ✓ Pas d'erreurs non gérées
- ✓ Commits descriptifs
- ✓ Documentation

**Avant fin Sprint**:
- ✓ Tous US = "Done"
- ✓ Tests intégration passent
- ✓ Tests sécurité validés
- ✓ Excel Scrum à jour
- ✓ Documentation finalisée

---

## 🚀 Comment Utiliser Ces Fichiers

### Phase 1: Lancement Sprint (Jour 1)
1. **Lire** SEMAINE_2_DETAIL.md (vue d'ensemble)
2. **Importer** les CSV dans Excel (create workbook)
3. **Afficher** SCRUM_ChatApp_Sprint2_COMPLET.html
4. **Assigner** les tâches aux devs
5. **Valider** les estimations

### Phase 2: Exécution Sprint (Jour 2-20)
1. **Dev A**: Commencer Tâche 1.1 (PasswordHasher)
2. **Dev B**: Commencer Tâche 2.3 (ClientValidator) + 2.1
3. **Dev C**: Commencer Tâche 3.1 (Tests) après Day 1
4. **Chacun**: Mettre à jour statut des US dans Excel quotidiennement
5. **Dev C**: Remplir Burn-Down Chart (colonne "Réelles") chaque jour

### Phase 3: Clôture Sprint (Jour 19-20)
1. **Tous**: Finaliser code + tests
2. **Dev B+A**: Code review croisée
3. **Dev C**: Documentation + rapports tests
4. **Tous**: Vérifier Definition of Done
5. **Lead**: Update Excel Scrum final

---

## 📊 Métriques Clés

| Métrique | Valeur | Status |
|----------|--------|--------|
| **Total Heures** | 25h | ✓ Realiste |
| **Capacité Équipe** | 40h | ✓ Buffer 15h |
| **Utilisation Moy** | 62.5% | ✓ Bon |
| **Dev A** | 68% | ✓ Optimal |
| **Dev B** | 56% | ✓ Confortable |
| **Dev C** | 62% | ✓ Part-time OK |
| **Burn Rate** | 1.25h/jour | ✓ Réaliste |

---

## 🔒 Sécurité Intégrée

Tous les fichiers Scrum incluent les critères de sécurité:

✅ **Hachage de passwords**:
- SHA-256 + Salt
- Aucun password en clair
- Tests de vérification

✅ **Anti-injection SQL**:
- Paramètres (@) obligatoires
- Validation input
- Tests avec payloads malveillants

✅ **Gestion des sessions**:
- SessionID unique
- Timeout 1h
- Validation avant action

✅ **Logging sécurisé**:
- Jamais de password dans logs
- Tentatives échouées tracées
- Timestamp + username

---

## 📚 Fichiers Complémentaires (Existants)

- **PLAN_DEVELOPPEMENT.md** - Plan 5 semaines complet
- **SEMAINE_1_DETAIL.md** - Architecture 3 couches (contexte)
- **README.md** - Overview du projet

---

## ❓ Questions Fréquentes

**Q: Le 3ème dev part-time, il fait quoi?**
A: Tests QA/Sécurité (30% des heures) → Tâches 3.1-3.5. Commence après Day 1.

**Q: Et si on ne finit pas les 25h?**
A: Relâcher US moins critiques (ex: 3.5 Documentation peut être réduit).

**Q: Excel Scrum, comment l'importer?**
A: Ouvrir Excel → Données → Depuis fichier texte → Sélectionner CSV → Format délimité virgules.

**Q: Burn-Down Chart, comment le faire?**
A: Importer CSV → Insérer graphique XY → Ajouter série "Réelles" pendant Sprint.

**Q: Dev C surcharge?**
A: Tests 5h seulement → Peut aider Dev B/A si besoin (buffer 3h).

---

## 📞 Contacts & Support

- **Lead Dev**: Consulter SEMAINE_2_DETAIL.md
- **Questions Scrum**: Relire SEMAINE_2_DELIVERABLES.md
- **Risques**: Vérifier SCRUM_ChatApp_Sprint2_Risques.csv

---

## 🎬 Next Steps

1. **Aujourd'hui**: Lire ce guide + SEMAINE_2_DETAIL.md
2. **Demain**: Importer CSV dans Excel + assigner tâches
3. **Session 1**: Dev A/B commencent implementation
4. **Entre sessions**: Dev C prépare tests
5. **Session 2**: Intégration + Code review
6. **Fin Sprint**: Démo + Review Sprint 3

---

**Créé**: 2026-04-24  
**Version**: Sprint 2 - Complet & Prêt  
**Statut**: ✅ Documentation finalisée, prêt pour implémentation  
**Total Fichiers**: 9 (Markdown + CSV + HTML)  
**Couverture**: Product Backlog, Team, Tasks, Risks, Tests, Quality
