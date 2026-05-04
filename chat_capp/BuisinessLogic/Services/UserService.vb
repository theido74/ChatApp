Public Class UserService
    Private dbAccess As New UserDateAccess()

    Public Function CreateEleve(username As String, nom As String, prenom As String, dateDeNaissance As DateTime, email As String, mdp As String, niveau As Integer, nbPoint As Integer, classe As String) As Integer
        If String.IsNullOrEmpty(username) Then
            Return Nothing
        End If

        Try
            Return dbAccess.CreateEleve(username, nom, prenom, dateDeNaissance, email, mdp, niveau, nbPoint, classe)
        Catch ex As Exception
            MessageBox.Show("Erreur Fonction CreateEleve")
            Return Nothing
        End Try
    End Function
End Class
