Public Class Inscription


    Private authService As New AuthenticationService()
    Private clientValidator As New ClientValidator()
    Private UserService As New UserService()
    Private passwordHash As New PasswordHasher()





    ' Message d'erreur succinct
    Private Sub AfficherErreur(msg As String)
            MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

        ' Message de succès succinct
        Private Sub AfficherSucces(msg As String)
            MessageBox.Show(msg, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub


    Private Sub btnInscription_Click(sender As Object, e As EventArgs) Handles btnInscription.Click
            ' Récupérer valeurs
            Dim nom = txtNom.Text.Trim()
            Dim prenom = txtPrenom.Text.Trim()
            Dim username = txtUsername.Text.Trim()
            Dim password = txtMDP.Text
        Dim confirmPassword = txtConfirmMDP.Text.Trim()
        Dim dateDeNaissance = New Date(1989 - 20 - 7)
        Dim ageValue As Integer = 0
        Dim email = txtEmail.Text.Trim()
        Dim niveau = CInt(txtNiveau.Text.Trim())
        Dim classe = "ESIG1"

        ' Vérifications basiques de présence
        If String.IsNullOrEmpty(nom) OrElse String.IsNullOrEmpty(prenom) Then
                AfficherErreur("Le nom et le prénom sont requis.")
                Return
            End If

            If String.IsNullOrEmpty(username) Then
                AfficherErreur("Le nom d'utilisateur est requis.")
                Return
            End If

            If String.IsNullOrEmpty(password) Then
                AfficherErreur("Le mot de passe est requis.")
                Return
            End If

        ' Vérifier âge (si présent)
        If Not String.IsNullOrWhiteSpace(txtAge.Text) Then
            If Not Integer.TryParse(txtAge.Text.Trim(), ageValue) Then
                AfficherErreur("L'âge doit être un nombre entier.")
                Return
            End If
            If ageValue < 5 OrElse ageValue > 120 Then
                AfficherErreur("L'âge semble invalide.")
                Return
            End If
        End If

        ' Vérifier niveau (si présent)
        If String.IsNullOrWhiteSpace(CStr(niveau)) Then
            AfficherErreur("Le niveau est requis.")
            Return
        End If

        ' Vérifier que les services sont disponibles (runtime)
        If clientValidator Is Nothing OrElse authService Is Nothing Then
                AfficherErreur("Erreur interne : services non initialisés (mode designer ?).")
                Return
            End If

            ' Réutiliser les règles du login pour username et password
            If Not clientValidator.ValidateUsername(username) Then
                AfficherErreur("Nom d'utilisateur invalide (respecter les règles).")
                Return
            End If

            If Not clientValidator.ValidatePassword(password) Then
                AfficherErreur("Mot de passe invalide (respecter les règles).")
                Return
            End If

            ' Vérifier confirmation du mot de passe
            If Not password.Equals(confirmPassword) Then
                AfficherErreur("Les mots de passe ne correspondent pas.")
                Return
            End If

            ' Vérifier unicité du username via le service existant
            Try
                Dim existingUser = authService.GetEleveByUsername(username)
                If existingUser IsNot Nothing Then
                    AfficherErreur("Nom d'utilisateur déjà utilisé. Choisissez-en un autre.")
                    Return
                End If
            Catch ex As Exception
                AfficherErreur("Impossible de vérifier l'unicité du nom d'utilisateur : " & ex.Message)
                Return
            End Try


        UserService.CreateEleve(username, nom, prenom, dateDeNaissance, email, passwordHash.HashMotdePasse(password), niveau, 0, classe)

        AfficherSucces("Inscription prête. Les données sont stockées en mémoire pour insertion en base.")
        ' Option : appeler directement le service d'enregistrement ici si disponible,
        ' sinon rediriger vers le Login et insérer depuis là en récupérant PendingRegistration.
        Dim loginform As New Login()
        loginform.ShowDialog()
            Me.Close()
        End Sub

        Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click
            Me.Close()
        End Sub



End Class
