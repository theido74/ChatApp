Public Class Forum
    Private messageService As New MessageService()
    Private forumId As Integer
    Private userDataAccess As New UserDateAccess()
    Private forumService As New ForumService()

    Private Function GetEmmeteurName(userId As Integer) As String
        Return userDataAccess.GetUsernameById(userId)
    End Function

    ' Property pour recevoir le forumId
    Public Property SelectedForumId As Integer
        Get
            Return forumId
        End Get
        Set(value As Integer)
            forumId = value
        End Set
    End Property


    Private Sub Forum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CurrentUser.User IsNot Nothing Then
            lblUsername.Text = "#" & CurrentUser.User.UserName
        End If

        AfficherNomForum()

        ChargerMessages()
    End Sub

    Private Sub ChargerMessages()
        ' Récupérer tous les messages du forum
        Dim messages As List(Of Message) = messageService.GetMessagesByForumId(forumId)

        flpFenetreMessage.Controls.Clear()

        For Each msg In messages
            Dim ctrl As Control

            If msg.EmmeteurId = CurrentUser.User.UserID Then
                ' Message ENVOYÉ
                Dim ctrlEnvoye As New MessageControlEnvoye With {
                    .EmmeteurNom = GetEmmeteurName(msg.EmmeteurId),
                    .Contenu = msg.Contenu,
                    .TimeStamp = msg.TimeStamp
                }
                ctrl = ctrlEnvoye
            Else
                ' Message REÇU
                Dim ctrlRecu As New MessageControlRecu With {
                    .EmmeteurNom = GetEmmeteurName(msg.EmmeteurId),
                    .Contenu = msg.Contenu,
                    .TimeStamp = msg.TimeStamp
                }
                ctrl = ctrlRecu
            End If

            flpFenetreMessage.Controls.Add(ctrl)
        Next
    End Sub


    Private Sub btnEnvoyer_Click(sender As Object, e As EventArgs) Handles btnEnvoyer.Click
        If String.IsNullOrEmpty(txtMessge.Text) Then
            MessageBox.Show("Écrivez un message !")
            Return
        End If

        messageService.CreateForumMessage(CurrentUser.User.UserID, forumId, txtMessge.Text)
        txtMessge.Clear()
        ChargerMessages()
    End Sub
    ' Afficher le nom du forum
    Private Sub AfficherNomForum()
        Dim forum As Forums = ForumService.GetForumById(forumId)
        If forum IsNot Nothing Then
            lblNomForum.Text = forum.NomForum
        End If
    End Sub

    Private Sub dgvUtilisateursForum_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUtilisateursForum.CellClick

    End Sub

    Private Sub flpFenetreMessage_Paint(sender As Object, e As PaintEventArgs) Handles flpFenetreMessage.Paint

    End Sub
End Class

