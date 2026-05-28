Imports System.Data
Imports System.Linq
Public Class TrouverForum
    Private forumService As New ForumService()
    Private allForums As List(Of Forums)

    Private Sub TrouverForum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChargerTousLesForums()
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Charge tous les forums depuis la base de données et les affiche dans le DataGridView.
    ''' </summary>
    Private Sub ChargerTousLesForums()
        Try
            allForums = forumService.GetAllForums()
            RemplirDataGridView(allForums)
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des forums: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Remplit le DataGridView avec la liste des forums.
    ''' Prend une liste de forums en paramètre et ajoute chaque forum comme une ligne dans le DataGridView.
    ''' </summary>
    Private Sub RemplirDataGridView(forums As List(Of Forums))
        dgvForums.DataSource = Nothing
        dgvForums.Rows.Clear()

        If forums IsNot Nothing AndAlso forums.Count > 0 Then
            For Each forum In forums
                dgvForums.Rows.Add(forum.NomForum, forum.Description, forum.ForumId)
            Next
        End If
    End Sub


    ''' <summary>
    ''' Author: Ayman
    ''' Ouvre le formulaire de création de forum lorsque l'utilisateur clique sur le bouton "Créer un Forum".
    ''' </summary>

    Private Sub btnCreerForum_Click(sender As Object, e As EventArgs) Handles btnCreerForum.Click
        Dim createForumForm As New CreateForum()
        Me.Hide()
        createForumForm.ShowDialog()

    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Ouvre le formulaire du forum sélectionné lorsque l'utilisateur clique sur une cellule du DataGridView.
    ''' </summary>

    Private Sub dgvForums_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvForums.CellContentClick
        If e.RowIndex < 0 Then
            Return
        End If
        Dim forumId As Integer = CInt(dgvForums.Rows(e.RowIndex).Cells(2).Value)
        Dim forumForm As New Forum With {.SelectedForumId = forumId}
        Me.Hide()
        forumForm.ShowDialog()
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Ferme le formulaire actuel et ouvre le formulaire principal lorsque l'utilisateur clique sur le bouton.
    ''' </summary>

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click

        Me.Close()


    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Filtre les forums en temps réel selon le texte saisi dans la TextBox.
    ''' </summary>
    Private Sub txtForum_TextChanged(sender As Object, e As EventArgs) Handles txtForum.TextChanged


        Dim recherche As String = txtForum.Text.Trim().ToLower()

        If String.IsNullOrEmpty(recherche) Then
            ' Si la recherche est vide, afficher tous les forums
            RemplirDataGridView(allForums)
        Else
            ' Filtrer sur le nom ET la description avec une "Boucle for each" version (LINQ)'
            'Function(f) est une expression qui représente une fonction anonyme utilisée pour filtrer les forums.
            '.Contains verifie caractere par caractere si la chaine de recherche est presente 
            'dans le nom ou la description du forum, et retourne true ou false
            Dim forumsFiltres As List(Of Forums) = allForums.Where(
            Function(f) f.NomForum.ToLower().Contains(recherche) OrElse
                        f.Description.ToLower().Contains(recherche)
        ).ToList() 'Exécute la requête et retourne une liste filtrée

            RemplirDataGridView(forumsFiltres)
        End If
    End Sub

End Class