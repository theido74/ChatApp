Public Class Form1
    Private passwordHasher As New PasswordHasher()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tester la connexion au démarrage
        ' TestOracleConnection()
        GetMessages()
    End Sub

    Private Sub GetForums()
        Dim forumDataAccess = New ForumDataAccess
        Dim forums As List(Of Forum) = forumDataAccess.GetAllForums
        For Each forum As Forum In forums
            Console.WriteLine("ID: " & forum.ForumId)
            Console.WriteLine("Nom: " & forum.NomForum)
            Console.WriteLine("Description: ")
            Console.WriteLine("DateCreation: ")
            Console.WriteLine("Supprimé: ")
            Console.WriteLine("-----------------------------")
        Next
    End Sub

    Private Sub GetMessages()
        Dim messageDataAccess As New messageDataAccess()
        Dim userMessages As List(Of Message) = messageDataAccess.GetMessageByRecipientId(2)
        For Each msg As Message In userMessages
            Console.WriteLine("ID: " & msg.MessageId)
            Console.WriteLine("Expéditeur: " & msg.EmmeteurId)
            Console.WriteLine("Destinataire: " & msg.ReceveurId)
            Console.WriteLine("Forum: " & msg.ForumId)
            Console.WriteLine("Contenu: " & msg.Contenu)
            Console.WriteLine("Date: " & msg.TimeStamp)
            Console.WriteLine("Privé: " & msg.EstPrive)
            Console.WriteLine("Supprimé: " & msg.EstSupprime)
            Console.WriteLine("-----------------------------")
        Next
    End Sub

    Private Sub TestOracleConnection()
        Try
            MessageBox.Show("Test de connexion en cours...", "Info")

            If DatabaseConnection.TestConnection() Then
                MessageBox.Show("✅ Connexion Oracle réussie!", "Succès")

                Dim message As New MessageService()
                Dim id As Integer = message.CreateMessage(4, 1, "TEST SALUT LOGS")
                Dim logger As New LogService()

                If id > 0 Then
                    MessageBox.Show("✅ Utilisateur trouvé: " & id, "Succès")
                    logger.PingDB(4, "PING")
                Else
                    MessageBox.Show("⚠️ Pas d'utilisateur avec ", "Info")
                End If
            Else
                MessageBox.Show("❌ Connexion Oracle échouée!", "Erreur")
            End If

        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message, "Erreur")
        End Try
    End Sub
End Class

