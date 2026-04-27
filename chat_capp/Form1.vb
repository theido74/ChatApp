Public Class Form1
    Private Sub Form_Load() Handles Me.Load
        ' Tester la connexion au démarrage
        TestOracleConnection()
    End Sub

    Private Sub TestOracleConnection()
        Try
            MessageBox.Show("Test de connexion en cours...", "Info")

            ' Appeler la méthode statique
            If DatabaseConnection.TestConnection() Then
                MessageBox.Show("✅ Connexion Oracle réussie!", "Succès")

                ' Tester aussi la requête
                Dim userAccess As New UserDateAccess()
                ' Supposant qu'il y a au moins 1 utilisateur
                Dim e As Eleve = userAccess.GetEleveByID(3)

                If e IsNot Nothing Then
                    MessageBox.Show("✅ Utilisateur trouvé: " & e.Nom, "Succès")
                Else
                    MessageBox.Show("⚠️ Pas d'utilisateur avec ID=1", "Info")
                End If
            Else
                MessageBox.Show("❌ Connexion Oracle échouée!", "Erreur")
            End If

        Catch ex As Exception
            MessageBox.Show("Erreur: " & ex.Message, "Erreur")
        End Try
    End Sub
End Class
