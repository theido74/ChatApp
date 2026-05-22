Public Class Forum
    Private messageService As New MessageService()
    Private forumId As Integer
    Private userDataAccess As New UserService()
    Private forumService As New ForumService()

    Private Function GetEmmeteurName(userId As Integer) As String
        Return userDataAccess.GetUsernameById(userId)
    End Function


    ''' <summary>
    ''' Author: Ayman
    ''' Propriété pour stocker et accéder à l'ID du forum sélectionné. Cette propriété est utilisée pour identifier quel forum doit être affiché et géré dans ce formulaire.
    ''' Property pour recevoir le forumId depuis le formulaire TrouverForum ou CreateForum, et l'utiliser pour charger les messages et les informations du forum correspondant.
    ''' </summary>

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
        ChargerEleves()
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Charge la liste des élèves (utilisateurs) depuis la base de données et les affiche dans le DataGridView.
    ''' </summary>
    Private Sub ChargerEleves()
        Dim eleves As List(Of Eleve) = userDataAccess.GetAllEleve()
        dgvUtilisateursForum.AllowUserToAddRows = False
        dgvUtilisateursForum.Rows.Clear()

        If eleves IsNot Nothing AndAlso eleves.Count > 0 Then
            For Each eleve In eleves
                Dim index As Integer = dgvUtilisateursForum.Rows.Add(
                eleve.UserName,
                eleve.ChatStatut
            )
                ' UserID stocké invisiblement dans le Tag
                dgvUtilisateursForum.Rows(index).Tag = eleve.UserID
            Next
        End If
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement de formatage des cellules du DataGridView pour appliquer une coloration conditionnelle en fonction du statut de chat de chaque utilisateur.
    ''' </summary>
    Private Sub dgvUtilisateursForum_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvUtilisateursForum.CellFormatting
        ' Vérifier que c'est bien la colonne ChatStatut (index 1)
        'Select Case marche comme un if else if, mais plus adapté pour comparer une même variable à plusieurs valeurs différentes
        If e.ColumnIndex = 1 AndAlso e.Value IsNot Nothing Then
            Select Case e.Value.ToString()
                Case "En ligne"
                    e.CellStyle.ForeColor = Color.Green
                Case "Hors ligne"
                    e.CellStyle.ForeColor = Color.Red
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Charge les messages du forum depuis la base de données et les affiche dans le FlowLayoutPanel.
    ''' Il récupère tous les messages du forum en utilisant le service de messagerie, puis crée des contrôles pour chaque message.
    ''' Les messages envoyés par l'utilisateur actuel sont affichés avec un contrôle différent de ceux reçus.
    ''' </summary>
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

    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement de clic sur le bouton "Envoyer" pour envoyer un message dans le forum.
    ''' Il vérifie d'abord que le champ de message n'est pas vide, puis utilise le service de messagerie pour créer un nouveau message dans le forum.
    ''' Après l'envoi, il efface le champ de message et recharge les messages du forum pour afficher le nouveau message envoyé.
    ''' </summary>


    Private Sub btnEnvoyer_Click(sender As Object, e As EventArgs) Handles btnEnvoyer.Click
        If String.IsNullOrEmpty(txtMessge.Text) Then
            MessageBox.Show("Écrivez un message !")
            Return
        End If

        messageService.CreateForumMessage(CurrentUser.User.UserID, forumId, txtMessge.Text)
        txtMessge.Clear()
        ChargerMessages()
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Récupère le nom du forum à partir de la base de données en utilisant le service de forum et l'affiche dans le label lblNomForum.
    ''' </summary>
    Private Sub AfficherNomForum()
        Dim forum As Forums = ForumService.GetForumById(forumId)
        If forum IsNot Nothing Then
            lblNomForum.Text = forum.NomForum
        End If
    End Sub

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click

        Me.Close()


    End Sub
End Class

