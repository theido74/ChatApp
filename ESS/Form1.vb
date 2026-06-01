Public Class Form1
    Private passwordHasher As New PasswordHasher()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' TestOracleConnection()
        ' TestGetMessages()
        lblTest.Text = TestGetFilDeDiscussion()
    End Sub

    ''' <summary>
    ''' Tester la fonction permettant de récupérer les discussion et la classe Chat en retournant un texte récupéré en les utilisant.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <returns></returns>
    Private Function TestGetFilDeDiscussion() As String
        Dim mesServ As MessageService = New MessageService
        Dim chats As List(Of Chat) = mesServ.GetChatById(1)
        Dim separation = " - "
        Dim text = ""
        Dim line = ""
        For Each chat As Chat In chats
            line = chat.ContactNom & separation & "Id" & chat.ContactId.ToString()
            If chat.NbOfUnreadMessages > 0 Then
                line = line & separation & chat.NbOfUnreadMessages.ToString() & " Message(s)"
            End If
            If chat.Statut IsNot Nothing And chat.Statut <> "" Then
                line = line & separation & chat.Statut
            End If
            line = line & Environment.NewLine
            text = text & line
        Next
        Return text
    End Function

    Private Sub TestGetForums()
        Dim forumDataAccess = New ForumDataAccess
        Dim forums As List(Of Forums) = forumDataAccess.GetAllForums
        For Each forum As Forums In forums
            Console.WriteLine("ID: " & forum.ForumId)
            Console.WriteLine("Nom: " & forum.NomForum)
            Console.WriteLine("Description: ")
            Console.WriteLine("DateCreation: ")
            Console.WriteLine("Supprimé: ")
            Console.WriteLine("-----------------------------")
        Next
    End Sub

    Private Sub TestGetMessages()
        Dim messageDataAccess As New messageDataAccess()
        Dim userMessages As List(Of Message) = messageDataAccess.GetMessageByForumId(2)
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
