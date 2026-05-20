Public Class Form1
    Private passwordHasher As New PasswordHasher()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tester la connexion au démarrage
        ' TestOracleConnection()
        TestLabelTemp()
    End Sub

    Private Sub TestLabelTemp()
        'Dim CurrentUserId As Integer? = CurrentUser.User.UserID
        lblUnread.Text = ""
        Dim mesSer As MessageService = New MessageService
        Dim chats As List(Of Chat) = mesSer.GetConversationNameWithUnreadMessagesById(11)
        If chats IsNot Nothing Then
            For Each cha As Chat In chats
                Dim line As String = cha.ContactNom
                lblUnread.Text &= line & Environment.NewLine
            Next
        End If
        'lblUnread.ForeColor = Color.Red
        'lblUnread.Text = "●"
        'lblUnread.Visible = True
    End Sub

    Private Sub GetForums()
        Dim forAcc As ForumDataAccess = New ForumDataAccess
        Dim forums As List(Of Forums) = forAcc.GetAllForums
        For Each forum As Forums In forums
            Console.WriteLine("ID: " & forum.ForumId)
            Console.WriteLine("Nom: " & forum.NomForum)
            Console.WriteLine("Description: " & forum.Description)
            Console.WriteLine("DateCreation: " & forum.DateCreation)
            Console.WriteLine("Actif: " & forum.EstActif)
            Console.WriteLine("-----------------------------")
        Next
    End Sub

    Private Sub GetMessages()
        Dim mesAcc As messageDataAccess = New messageDataAccess()
        Dim userMessages As List(Of Message) = mesAcc.GetMessageByForumId(2)
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
                Dim id As Integer = message.CreatePrivateMessage(4, 1, "TEST SALUT LOGS")
                Dim logger As New LogService()

                If id > 0 Then
                    MessageBox.Show("✅ Utilisateur trouvé: " & id, "Succès")
                    logger.PingDB(4)
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
