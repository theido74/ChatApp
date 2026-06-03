Imports System.Data

Public Class CreateForum
    Private forumService As New ForumService()

    Private Sub CreateForum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialiser les champs
        txtNomForum.Text = ""
        txtDescription.Text = ""
        txtNomForum.Focus()
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement Click du bouton "Créer Forum".
    ''' Valide les entrées de l'utilisateur, crée un nouveau forum via le service, et ouvre le forum fraîchement créé.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCreerForum_Click(sender As Object, e As EventArgs) Handles btnCreerForum.Click
        ' Validation du nom
        If String.IsNullOrWhiteSpace(txtNomForum.Text) Then
            MessageBox.Show("Le nom du forum est obligatoire !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txtNomForum.Text.Length > 100 Then
            MessageBox.Show("Le nom du forum ne doit pas dépasser 100 caractères !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim nomForum As String = txtNomForum.Text
        Dim description As String = If(String.IsNullOrWhiteSpace(txtDescription.Text), "", txtDescription.Text)

        Try
            ' Créer le forum
            Dim newForumId As Integer = forumService.CreateForum(nomForum, description)

            If newForumId > 0 Then
                MessageBox.Show("Forum créé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Ouvrir le forum fraîchement créé
                Dim forumForm As New Forum With {.SelectedForumId = newForumId}
                ' Fermer la fenêtre de création
                Me.Hide()
                forumForm.ShowDialog()


            Else
                MessageBox.Show("Erreur lors de la création du forum !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As InvalidOperationException
            ' Exception pour doublon ou autre erreur métier
            MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement Click du bouton "Annuler".
    ''' Ferme le formulaire de création de forum et retourne à la page principale.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click

        Dim mainForm As New Main()
        Me.Hide()
        Main.ShowDialog()
    End Sub
End Class