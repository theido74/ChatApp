Public Class Main
    Private timer As New System.Windows.Forms.Timer
    Private logger As New LogService()


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.User IsNot Nothing Then
            lblUsername.Text = CurrentUser.User.UserName
            lblUsername2.Text = CurrentUser.User.UserName
            lblClasse.Text = CurrentUser.User.Classe
        End If

        logger.PingDB(CurrentUser.User.UserID)
        RefreshOnlineUser()
        timer.Interval = 30000
        AddHandler timer.Tick, AddressOf TimerTick
        timer.Start()

    End Sub

    Private Sub TimerTick(sender As Object, e As EventArgs)
        Try
            logger.PingDB(CurrentUser.User.UserID)
            RefreshOnlineUser()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RefreshOnlineUser()
        Try
            Dim lstUser = logger.isActive()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ForumTestToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim forumForm As New Forum With {.SelectedForumId = 1}  ' Ou un ID dynamique
        forumForm.ShowDialog()
    End Sub

    Private Sub QuitterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitterToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub MessagesPrivésToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MessagesPrivésToolStripMenuItem.Click
        Dim MessaagePriveForm As New MessagePrive()
        MessaagePriveForm.ShowDialog()
        Me.Hide()
    End Sub

    Private Sub CréerUnForumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CréerUnForumToolStripMenuItem.Click
        Dim createForumForm As New CreateForum()
        createForumForm.ShowDialog()
        Me.Hide()
    End Sub

    Private Sub TrouverUnForumToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TrouverUnForumToolStripMenuItem1.Click
        Dim trouverForumForm As New TrouverForum()
        trouverForumForm.ShowDialog()
        Me.Hide()
    End Sub
End Class