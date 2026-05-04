Public Class Form1
    Private passwordHasher As New PasswordHasher()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tester la connexion au démarrage
        TestOracleConnection()

        ' Exécuter le test manuellement (débogage uniquement) et afficher toute erreur
        Try
            Dim passwordTest As New PasswordHasherTests()
            passwordTest.HashPassword_ReturnsDifferentHashEachTime()
            MessageBox.Show("Test manuel exécuté avec succès", "Info")
        Catch ex As Exception
            MessageBox.Show("Erreur lors du test manuel: " & ex.Message, "Erreur")
        End Try
    End Sub

    Private Sub TestOracleConnection()
        Try
            MessageBox.Show("Test de connexion en cours...", "Info")

            If DatabaseConnection.TestConnection() Then
                MessageBox.Show("✅ Connexion Oracle réussie!", "Succès")

                Dim message As New messageDataAccess()
                Dim id As Integer = message.CreateMessage(1, 2, "TEST SALUT")

                If id > 0 Then
                    MessageBox.Show("✅ Utilisateur trouvé: " & id, "Succès")
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

