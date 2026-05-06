# INSERT avec SEQUENCE Oracle en VB.NET - 2 Approches

## 🎯 Prérequis Oracle

```sql
-- Créer une SEQUENCE Oracle (exemple Forum)
CREATE SEQUENCE SEQ_FORUM_ID
  START WITH 1
  INCREMENT BY 1
  NOCYCLE;

-- Ou pour Messages
CREATE SEQUENCE SEQ_MESSAGE_ID
  START WITH 1
  INCREMENT BY 1
  NOCYCLE;
```

---

## ✅ APPROCHE 1 : RETURNING clause (RECOMMANDÉE - Oracle 8i+)

### SQL Oracle
```sql
INSERT INTO Forum (ForumID, Titre, Contenu, UtilisateurID, DateCreation)
VALUES (SEQ_FORUM_ID.NEXTVAL, :p_titre, :p_contenu, :p_utilisateur_id, SYSDATE)
RETURNING ForumID INTO :p_forum_id_out;
```

### VB.NET - Code complet
```vb
Public Function InsertForumWithSequence(titre As String, contenu As String, utilisateurID As Integer) As Integer
    Dim forumID As Integer = 0
    Dim connectionString As String = "Data Source=XE;User Id=CHATAPP;Password=password123;"
    
    Using connection As New Oracle.ManagedDataAccess.Client.OracleConnection(connectionString)
        connection.Open()
        
        Using command As Oracle.ManagedDataAccess.Client.OracleCommand = connection.CreateCommand()
            command.CommandText = "INSERT INTO Forum (ForumID, Titre, Contenu, UtilisateurID, DateCreation) " & _
                                  "VALUES (SEQ_FORUM_ID.NEXTVAL, :p_titre, :p_contenu, :p_utilisateur_id, SYSDATE) " & _
                                  "RETURNING ForumID INTO :p_forum_id_out"
            
            ' Paramètres d'entrée
            command.Parameters.Add(":p_titre", titre)
            command.Parameters.Add(":p_contenu", contenu)
            command.Parameters.Add(":p_utilisateur_id", utilisateurID)
            
            ' Paramètre de sortie (récupérer l'ID généré)
            Dim outParam As Oracle.ManagedDataAccess.Client.OracleParameter = New Oracle.ManagedDataAccess.Client.OracleParameter()
            outParam.ParameterName = ":p_forum_id_out"
            outParam.OracleDbType = Oracle.ManagedDataAccess.Client.OracleDbType.Int32
            outParam.Direction = ParameterDirection.Output
            command.Parameters.Add(outParam)
            
            Try
                command.ExecuteNonQuery()
                
                ' Récupérer l'ID généré
                If outParam.Value IsNot DBNull.Value Then
                    forumID = CInt(outParam.Value)
                    Console.WriteLine($"✅ Forum créé avec ID: {forumID}")
                End If
                
            Catch ex As Exception
                Console.WriteLine($"❌ Erreur INSERT: {ex.Message}")
            End Try
        End Using
    End Using
    
    Return forumID
End Function
```

### Utilisation
```vb
' Dans ton code métier
Dim repo As New ForumDataAccess()
Dim nouveauForumID As Integer = repo.InsertForumWithSequence("Mon super forum", "Bienvenue!", 5)

' L'objet Forum peut être créé avec le nouvel ID
Dim monForum As New Forum With {
    .ForumID = nouveauForumID,
    .Titre = "Mon super forum",
    .Contenu = "Bienvenue!",
    .UtilisateurID = 5,
    .DateCreation = Date.Now
}
```

---

## ✅ APPROCHE 2 : SELECT CURRVAL (Alternative si RETURNING pas dispo)

### SQL Oracle
```sql
-- 2 requêtes: INSERT puis SELECT
INSERT INTO Forum (ForumID, Titre, Contenu, UtilisateurID, DateCreation)
VALUES (SEQ_FORUM_ID.NEXTVAL, :p_titre, :p_contenu, :p_utilisateur_id, SYSDATE);

-- Puis récupérer le dernier ID généré
SELECT SEQ_FORUM_ID.CURRVAL FROM DUAL;
```

### VB.NET - Code complet
```vb
Public Function InsertForumWithCurrval(titre As String, contenu As String, utilisateurID As Integer) As Integer
    Dim forumID As Integer = 0
    Dim connectionString As String = "Data Source=XE;User Id=CHATAPP;Password=password123;"
    
    Using connection As New Oracle.ManagedDataAccess.Client.OracleConnection(connectionString)
        connection.Open()
        
        ' ÉTAPE 1: INSERT
        Using cmdInsert As Oracle.ManagedDataAccess.Client.OracleCommand = connection.CreateCommand()
            cmdInsert.CommandText = "INSERT INTO Forum (ForumID, Titre, Contenu, UtilisateurID, DateCreation) " & _
                                    "VALUES (SEQ_FORUM_ID.NEXTVAL, :p_titre, :p_contenu, :p_utilisateur_id, SYSDATE)"
            
            cmdInsert.Parameters.Add(":p_titre", titre)
            cmdInsert.Parameters.Add(":p_contenu", contenu)
            cmdInsert.Parameters.Add(":p_utilisateur_id", utilisateurID)
            
            Try
                cmdInsert.ExecuteNonQuery()
                Console.WriteLine("✅ INSERT réussi")
            Catch ex As Exception
                Console.WriteLine($"❌ Erreur INSERT: {ex.Message}")
                Return 0
            End Try
        End Using
        
        ' ÉTAPE 2: Récupérer l'ID généré
        Using cmdSelect As Oracle.ManagedDataAccess.Client.OracleCommand = connection.CreateCommand()
            cmdSelect.CommandText = "SELECT SEQ_FORUM_ID.CURRVAL FROM DUAL"
            
            Try
                Dim result = cmdSelect.ExecuteScalar()
                If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                    forumID = CInt(result)
                    Console.WriteLine($"✅ ID récupéré: {forumID}")
                End If
            Catch ex As Exception
                Console.WriteLine($"❌ Erreur SELECT CURRVAL: {ex.Message}")
            End Try
        End Using
    End Using
    
    Return forumID
End Function
```

---

## 🔄 APPROCHE 3 : Pattern DataAccess Complet (PRODUCTION)

### Classe Forum Model
```vb
Public Class Forum
    Public Property ForumID As Integer
    Public Property Titre As String
    Public Property Contenu As String
    Public Property UtilisateurID As Integer
    Public Property DateCreation As DateTime
    
    Public Sub New()
    End Sub
    
    Public Sub New(titre As String, contenu As String, utilisateurID As Integer)
        Me.Titre = titre
        Me.Contenu = contenu
        Me.UtilisateurID = utilisateurID
        Me.DateCreation = Date.Now
    End Sub
End Class
```

### DataAccess - Insert avec Retour
```vb
Public Class ForumDataAccess
    Private Const CONNECTION_STRING As String = "Data Source=XE;User Id=CHATAPP;Password=password123;"
    
    ''' <summary>
    ''' Insère un nouveau forum et retourne l'objet complété avec l'ID généré
    ''' </summary>
    Public Function Insert(forum As Forum) As Forum
        Dim forumID As Integer = 0
        
        Using connection As New Oracle.ManagedDataAccess.Client.OracleConnection(CONNECTION_STRING)
            connection.Open()
            
            Using command As Oracle.ManagedDataAccess.Client.OracleCommand = connection.CreateCommand()
                command.CommandText = "INSERT INTO Forum (ForumID, Titre, Contenu, UtilisateurID, DateCreation) " & _
                                      "VALUES (SEQ_FORUM_ID.NEXTVAL, :titre, :contenu, :utilisateur_id, SYSDATE) " & _
                                      "RETURNING ForumID INTO :forum_id_out"
                
                ' Paramètres d'entrée
                command.Parameters.Add(":titre", forum.Titre)
                command.Parameters.Add(":contenu", forum.Contenu)
                command.Parameters.Add(":utilisateur_id", forum.UtilisateurID)
                
                ' Paramètre de sortie
                Dim outParam As New Oracle.ManagedDataAccess.Client.OracleParameter()
                outParam.ParameterName = ":forum_id_out"
                outParam.OracleDbType = Oracle.ManagedDataAccess.Client.OracleDbType.Int32
                outParam.Direction = ParameterDirection.Output
                command.Parameters.Add(outParam)
                
                Try
                    command.ExecuteNonQuery()
                    
                    If outParam.Value IsNot DBNull.Value Then
                        forumID = CInt(outParam.Value)
                        forum.ForumID = forumID
                    End If
                    
                Catch ex As Exception
                    Throw New Exception($"Erreur insertion forum: {ex.Message}", ex)
                End Try
            End Using
        End Using
        
        Return forum  ' L'objet contient maintenant l'ID généré!
    End Function
    
    ''' <summary>
    ''' Insère plusieurs forums et retourne la liste avec les IDs
    ''' </summary>
    Public Function InsertBatch(forums As List(Of Forum)) As List(Of Forum)
        Dim resultat As New List(Of Forum)
        
        Using connection As New Oracle.ManagedDataAccess.Client.OracleConnection(CONNECTION_STRING)
            connection.Open()
            
            For Each forum In forums
                Using command As Oracle.ManagedDataAccess.Client.OracleCommand = connection.CreateCommand()
                    command.CommandText = "INSERT INTO Forum (ForumID, Titre, Contenu, UtilisateurID, DateCreation) " & _
                                          "VALUES (SEQ_FORUM_ID.NEXTVAL, :titre, :contenu, :utilisateur_id, SYSDATE) " & _
                                          "RETURNING ForumID INTO :forum_id_out"
                    
                    command.Parameters.Add(":titre", forum.Titre)
                    command.Parameters.Add(":contenu", forum.Contenu)
                    command.Parameters.Add(":utilisateur_id", forum.UtilisateurID)
                    
                    Dim outParam As New Oracle.ManagedDataAccess.Client.OracleParameter()
                    outParam.ParameterName = ":forum_id_out"
                    outParam.OracleDbType = Oracle.ManagedDataAccess.Client.OracleDbType.Int32
                    outParam.Direction = ParameterDirection.Output
                    command.Parameters.Add(outParam)
                    
                    Try
                        command.ExecuteNonQuery()
                        
                        If outParam.Value IsNot DBNull.Value Then
                            forum.ForumID = CInt(outParam.Value)
                        End If
                        
                        resultat.Add(forum)
                        
                    Catch ex As Exception
                        Console.WriteLine($"⚠️ Erreur insertion forum '{forum.Titre}': {ex.Message}")
                    End Try
                End Using
            Next
        End Using
        
        Return resultat
    End Function
End Class
```

---

## 📝 Utilisation Complète

```vb
' Créer un nouveau forum
Dim monForum As New Forum("C# Advanced Tips", "Partagez vos meilleurs astuces C#", 5)

' Insérer et récupérer l'ID automatiquement
Dim dataAccess As New ForumDataAccess()
Dim forumInséré = dataAccess.Insert(monForum)

' L'objet contient maintenant l'ID!
Console.WriteLine($"Forum créé: ID={forumInséré.ForumID}, Titre={forumInséré.Titre}")
' Output: Forum créé: ID=42, Titre=C# Advanced Tips

' Batch insert
Dim forums As New List(Of Forum) From {
    New Forum("VB.NET Tutoriaux", "Apprendre VB.NET", 3),
    New Forum("Oracle SQL", "Questions SQL", 7),
    New Forum("Debugging", "Résoudre les bugs", 2)
}

Dim forumsInérés = dataAccess.InsertBatch(forums)

For Each f In forumsInérés
    Console.WriteLine($"ID={f.ForumID}, Titre={f.Titre}")
Next
```

---

## ⚡ Comparaison des 3 Approches

| Aspect | RETURNING | CURRVAL | Batch |
|--------|-----------|---------|-------|
| **Performance** | ⭐⭐⭐⭐⭐ (1 aller-retour) | ⭐⭐⭐⭐ (2 aller-retours) | ⭐⭐⭐⭐⭐ (N+1 optimisé) |
| **Sécurité** | ✅ SQL Injection safe (parameterized) | ✅ SQL Injection safe | ✅ SQL Injection safe |
| **Compatibilité Oracle** | Oracle 8i+ | Oracle 7+ | Oracle 8i+ |
| **Complexité Code** | Simple | Très simple | Moyenne |
| **Transactions** | ✅ Atomique (1 cmd) | ⚠️ 2 commands (plus risqué) | ✅ Atomique |
| **Préconisation** | 🏆 RECOMMANDÉE | Alternative legacy | Production batch |

---

## 🔐 Sécurité & Bonnes Pratiques

### ✅ BON - Parameterized (injection-safe)
```vb
command.Parameters.Add(":titre", titre)  ' Paramètre sécurisé
```

### ❌ MAUVAIS - String Concatenation (DANGER!)
```vb
command.CommandText = $"INSERT INTO Forum VALUES (SEQ_FORUM_ID.NEXTVAL, '{titre}')"  ' Vulnérable!
```

### ✅ BON - Exception Handling
```vb
Try
    command.ExecuteNonQuery()
Catch ex As Oracle.ManagedDataAccess.Client.OracleException
    If ex.Number = 1 Then
        ' Violation de contrainte unique
    ElseIf ex.Number = 2291 Then
        ' Foreign key constraint violation
    End If
Catch ex As Exception
    Console.WriteLine($"Erreur: {ex.Message}")
End Try
```

---

## 📌 Résumé

**Pour INSERT avec SEQUENCE Oracle en VB.NET:**

1. ✅ **Préféré**: `RETURNING ... INTO` (1 requête, atomique, performant)
2. ✅ **Alternative**: `SELECT CURRVAL` (2 requêtes, mais simple)
3. ✅ **Production**: Pattern DataAccess wrapper (réutilisable, testable)

**Code clé à retenir:**
```vb
command.CommandText = "INSERT INTO Table VALUES (...) RETURNING ID INTO :id_out"
outParam.Direction = ParameterDirection.Output
command.ExecuteNonQuery()
Dim generatedID = outParam.Value
```
