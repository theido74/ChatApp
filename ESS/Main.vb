Public Class Main
    Private timer As New System.Windows.Forms.Timer
    Private logger As New LogService()


    ''' <summary>
    ''' Auteur: Ayman
    ''' Lors du chargement du formulaire principal, cette méthode initialise les informations de l'utilisateur connecté,
    ''' enregistre une entrée de "PING" dans les logs pour indiquer que l'utilisateur est actif, met à jour son statut en ligne
    ''' dans la base de données, rafraîchit la liste des utilisateurs en ligne et démarre un timer pour continuer à envoyer des "PING"
    ''' périodiquement. Elle vérifie également si l'utilisateur a des messages non lus et affiche une notification en conséquence.
    ''' </summary>
    ''' <param name="sender">L'objet qui a déclenché l'événement.</param>
    ''' <param name="e">Les arguments de l'événement de chargement du formulaire.</param>
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.User IsNot Nothing Then
            lblUsername.Text = CurrentUser.User.UserName
            lblUsername2.Text = CurrentUser.User.UserName
            lblClasse.Text = CurrentUser.User.Classe
        End If

        logger.PingDB(CurrentUser.User.UserID)

        Dim userService As New UserService()
        userService.SetOnline(CurrentUser.User.UserID)


        timer.Interval = 30000
        AddHandler timer.Tick, AddressOf TimerTick
        timer.Start()

        lblNotification.Visible = False
        CheckMessages()
    End Sub

    ''' <summary>
    ''' Auteur: Ayman
    ''' Arrête le timer, enregistre la déconnexion de l'utilisateur dans les logs
    ''' et met à jour son statut en ligne dans la base de données lors de la fermeture du formulaire principal.
    ''' </summary>
    ''' <param name="sender">L'objet qui a déclenché l'événement.</param>
    ''' <param name="e">Les arguments de l'événement de fermeture du formulaire.</param>
    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            timer.Stop()
            logger.Logout(CurrentUser.User.UserID)

            Dim userService As New UserService()
            userService.Logout(CurrentUser.User.UserID)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TimerTick(sender As Object, e As EventArgs)
        Try
            logger.PingDB(CurrentUser.User.UserID)

        Catch ex As Exception

        End Try
    End Sub


    ''' <summary>
    ''' Vérifier si l'utilisateur à des messages non lu
    ''' </summary>
    ''' <auteur> Damien </auteur>
    Private Sub CheckMessages()
        Dim messageServ As New MessageService
        Dim nbUnreadMessages As Integer = messageServ.GetNbUnreadMessagesById()
        If nbUnreadMessages > 0 Then
            lblNotification.Text = "Vous avez " & nbUnreadMessages.ToString() & " message(s) non lu(s)."
        Else
            lblNotification.Text = "Pas de nouveau message"
        End If
        lblNotification.Visible = True
    End Sub

    Private Sub QuitterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitterToolStripMenuItem.Click
        logger.SetUserOffline(CurrentUser.User.UserID)
        Dim loginForm As New Login()
        Me.Hide()
        loginForm.ShowDialog()


    End Sub

    Private Sub MessagesPrivésToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MessagesPrivésToolStripMenuItem.Click
        Me.Hide()
        Dim MessaagePriveForm As New MessagePrive()
        MessaagePriveForm.ShowDialog()
        Me.Show()
    End Sub

    Private Sub CréerUnForumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CréerUnForumToolStripMenuItem.Click
        Me.Hide()

        Dim createForumForm As New CreateForum()
        createForumForm.ShowDialog()
        Me.Show()
    End Sub

    Private Sub TrouverUnForumToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TrouverUnForumToolStripMenuItem1.Click
        Me.Hide()
        Dim trouverForumForm As New TrouverForum()

        trouverForumForm.ShowDialog()
        Me.Show()
    End Sub


End Class