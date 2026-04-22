# ChatApp - Projet VB.NET & Oracle SQL

Application de chat avec forum et messages privés pour le projet d'études en informatique de gestion.

## 📋 Description

ChatApp est une application de chat robuste et sécurisée développée en **VB.NET (Windows Forms)** avec backend **Oracle SQL**.

### ✨ Fonctionnalités principales
- ✅ **Authentification sécurisée** - Login avec hachage des mots de passe (SHA-256)
- ✅ **Forum** - Conversations de groupe par catégories
- ✅ **Messages privés** - Conversations 1-à-1 entre utilisateurs
- ✅ **Détection d'utilisateurs en ligne** - Pings SQL toutes les 30 secondes
- ✅ **Recherche de messages** - Rechercher dans l'historique
- ✅ **Logging complet** - Audit de toutes les actions et erreurs

## 🏗️ Architecture

L'application suit une **architecture 3 couches**:

```
Présentation (WinForms UI)
        ↓
Métier (Services & Business Logic)
        ↓
Données (Oracle SQL & DataAccess)
```

Voir [SEMAINE_1_DETAIL.md](./SEMAINE_1_DETAIL.md) pour une explication complète de l'architecture.

## 📚 Documentation

- **[PLAN_DEVELOPPEMENT.md](./PLAN_DEVELOPPEMENT.md)** - Plan global sur 5 semaines
- **[SEMAINE_1_DETAIL.md](./SEMAINE_1_DETAIL.md)** - Guide détaillé pour la Semaine 1 (architecture 3 couches, BD, code de base)

## 🛠️ Stack Technique

| Composant | Technologie |
|-----------|------------|
| Langage | VB.NET |
| Interface | Windows Forms (WinForms) |
| Base de données | Oracle SQL |
| Authentification | Sessions utilisateur |
| Sécurité | Hachage SHA-256, Paramètres SQL |

## 📦 Prérequis

- Visual Studio 2019+ avec VB.NET
- Oracle Database (version 11g+) ou Oracle Express
- Oracle.ManagedDataAccess (NuGet package)

## 🚀 Démarrage rapide

### Installation BD
1. Lancer Oracle SQL Developer ou SQL*Plus
2. Exécuter les scripts `CREATE TABLE` du fichier [SEMAINE_1_DETAIL.md](./SEMAINE_1_DETAIL.md)
3. Créer les procédures stockées

### Installation Application
1. Ouvrir `ChatApp.sln` dans Visual Studio
2. Installer NuGet: `Install-Package Oracle.ManagedDataAccess`
3. Adapter les paramètres de connexion Oracle dans `DataAccess/DatabaseConnection.vb`
4. Compiler et exécuter

## 👥 Équipe

- 2 développeurs étudiants en première année informatique de gestion
- **Durée**: 5 semaines (5 sessions de 4 heures = 20 heures total)

## 📅 Planning

| Semaine | Objectifs |
|---------|-----------|
| **1** | Infrastructure BD + Structure VB.NET |
| **2** | Authentification + Interface Login |
| **3** | Forum public |
| **4** | Messages privés + Détection en ligne |
| **5** | Logging, Tests, Documentation |

## 🔒 Sécurité

- ✅ Mots de passe hachés (SHA-256 avec salt)
- ✅ Protection contre l'injection SQL (paramètres)
- ✅ Gestion sécurisée des sessions (1h timeout)
- ✅ Audit complet (table Logs)
- ✅ Validation des entrées

## 📝 License

Projet académique - Libre d'utilisation

---

**Bonne chance ! 🚀**
