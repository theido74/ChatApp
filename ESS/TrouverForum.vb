Imports System.Data

Public Class TrouverForum
    Private forumService As New ForumService()
    Private allForums As List(Of Forums)

    Private Sub TrouverForum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Charger tous les forums au démarrage
        ChargerTousLesForums()
    End Sub

    ' ===== CHARGER TOUS LES FORUMS =====
    Private Sub ChargerTousLesForums()
        Try
            allForums = forumService.GetAllForums()
            RemplirDataGridView(allForums)
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des forums: " & ex.Message)
        End Try
    End Sub

    ' ===== REMPLIR LA DATAGRIDVIEW =====
    Private Sub RemplirDataGridView(forums As List(Of Forums))
        dgvForums.DataSource = Nothing
        dgvForums.Rows.Clear()

        If forums IsNot Nothing AndAlso forums.Count > 0 Then
            For Each forum In forums
                dgvForums.Rows.Add(forum.ForumId, forum.NomForum, forum.Description, forum.DateCreation.ToString("dd/MM/yyyy"))
            Next
        End If
    End Sub

    ' ===== RECHERCHE EN TEMPS RÉEL =====
    Private Sub txtForum_TextChanged(sender As Object, e As EventArgs) Handles txtForum.TextChanged
        If String.IsNullOrEmpty(txtForum.Text) Then
            ' Si la recherche est vide, afficher tous les forums
            RemplirDataGridView(allForums)
        Else
            ' Filtrer les forums selon la recherche
            Dim recherche As String = txtForum.Text.ToLower()
            Dim forumsFiltrés As List(Of Forums) = allForums.Where(Function(f) f.NomForum.ToLower().Contains(recherche)).ToList()
            RemplirDataGridView(forumsFiltrés)
        End If
    End Sub

    ' ===== OUVRIR UN FORUM =====
    Private Sub dgvForums_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvForums.CellDoubleClick
        If e.RowIndex < 0 Then
            Return
        End If

        ' Récupérer l'ID du forum (colonne 0)
        Dim forumId As Integer = CInt(dgvForums.Rows(e.RowIndex).Cells(0).Value)

        ' Ouvrir le forum
        Dim forumForm As New Forum With {.SelectedForumId = forumId}
        forumForm.ShowDialog()
    End Sub

    ' ===== BOUTON CRÉER FORUM =====
    Private Sub btnCreerForum_Click(sender As Object, e As EventArgs) Handles btnCreerForum.Click
        Dim createForumForm As New CreateForum()
        createForumForm.ShowDialog()

        ' Rafraîchir après création
        ChargerTousLesForums()
    End Sub
End Class