# Definition of Done - Sprint 2 ChatApp

## ✅ Pour chaque tâche

- [ ] Code écrit suivant les standards du projet (architecture 3 couches respectée)
- [ ] Code commenté (méthodes complexes uniquement - pas de commentaire trivial)
- [ ] Tests unitaires écrits (minimum 70% de couverture de code)
- [ ] Tous les tests passent localement (0 failures)
- [ ] Code reviewé par au moins 1 pair (via Git PR + approbation)
- [ ] Pas d'erreurs non gérées (try-catch obligatoire sur chaque action sensible)
- [ ] Commits Git descriptifs avec co-author trailer
  ```
  git commit -m "Description tâche" --trailer "Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"
  ```

## ✅ Avant fin Sprint 2

- [ ] Tous les user stories marqués "Done"
- [ ] Tests d'intégration réussis (Login -> BD -> Session)
- [ ] Tests de sécurité validés (injection SQL, brute-force, sessions)
- [ ] Documentation Sprint 2 complétée (SEMAINE_2_DETAIL.md)
- [ ] Excel Scrum mis à jour (burndown chart, status tous les stories)
- [ ] Pas de dettes techniques critiques
- [ ] Code prêt pour la Semaine 3 (no breaking changes from Semaine 1)

## 🧪 Tests requis (par tâche)

### US-01 & US-08: PasswordHasher Tests
```
- [x] Hachage correct d'un mot de passe
- [x] Vérification correcte (password valide)
- [x] Rejet du mauvais password
- [x] Salt unique à chaque hachage
- [x] Gestion des cas d'erreur (null, vide)
```

### US-03 & US-09: AuthenticationService & Intégration
```
- [x] Authentification avec credentials valides -> SessionID créée
- [x] Rejet credentials invalides -> Error message
- [x] Gestion des sessions actives
- [x] Validation logout -> session marquée inactive
- [x] Erreurs BD gérées (timeout, connexion fermée)
- [x] Flux complet: Login -> BD -> Session
```

### US-04 & US-10: Validation & Tests Sécurité
```
- [x] Injection SQL: ' OR '1'='1 -> Rejectée
- [x] Injection SQL: ; DROP TABLE Users; -> Rejectée
- [x] Brute-force: 10 tentatives rapides -> Délai ou blocage
- [x] Session hijacking: SessionID expiré -> Rejetée
- [x] Password jamais en clair dans logs
```

### US-05 & US-06: LoginForm & MainForm UI
```
- [x] Formulaire Login loads without errors
- [x] Enter key dans Password -> Valide le login
- [x] Validation client avant envoi au service
- [x] Messages d'erreur conviviaux (pas techniques)
- [x] MainForm appears après login réussi
- [x] Menu items clickable (no crash)
- [x] Déconnexion revient à LoginForm
```

## 🛡️ Sécurité obligatoire

- ✅ Tous les mots de passe hachés (SHA-256 + salt)
- ✅ Aucun password en clair nulle part (logs, mémoire, etc.)
- ✅ Paramètres SQL utilisés dans toutes les requêtes
- ✅ SessionID validée avant chaque action
- ✅ Timeout 1h d'inactivité
- ✅ Validation longueur entrées (4-20 pour username, 8-50 pour password)

## 📊 Métriques de qualité

- **Code Coverage**: Minimum 70% pour AuthenticationService + PasswordHasher
- **Tests Passed**: 100% (0 failing tests)
- **Security Tests**: 100% (tous les scénarios de sécurité testés)
- **Code Review**: 1+ approbation avant merge
- **Documentation**: 100% des tâches documentées

## 📝 Checklist finale

- [ ] Plan.md et Excel Scrum mis à jour
- [ ] SEMAINE_2_DETAIL.md complétée (délivrables + tâches)
- [ ] Tous les commits avec co-author
- [ ] Pas de TODOs ou FIXMEs oubliés dans le code
- [ ] README.md à jour avec nouvelles dépendances
- [ ] Prêt pour code review et fusion sur main branch

---

**Status**: Sprint 2 début
**Last Updated**: 2026-04-24
**Next Review**: Fin session (Jour 4)
