Imports System.Drawing.Drawing2D
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text




Public Class Inscription



    Private authService As New AuthenticationService()
    Private clientValidator As New ClientValidator()
    Private UserService As New UserService()
    Private passwordHash As New PasswordHasher()


    Private placeholderNom As String = "Entrez votre nom"
    Private placeholderPrenom As String = "Entrez votre prénom"
    Private placeholderAge As String = "Ex: 30/05/2006"
    Private placeholderPseudo As String = "Entrez votre pseudo"
    Private placeholderEmail As String = "Entrez votre email"
    Private placeholderPassword As String = "Entrez votre Password"
    Private placeholderConfirmPassword As String = "Confirmez votre Password"
    Private placeholderClasse As String = "Entrez votre niveau"
    Private placeholderNiveau As String = "Entrez votre niveau"

    Private Sub AfficherErreur(msg As String)
        MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub


    Private Sub AfficherSucces(msg As String)
        MessageBox.Show(msg, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Inscription_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitTextBox(txtNom, placeholderNom)
        InitTextBox(txtPrenom, placeholderPrenom)
        InitTextBox(txtClasse, placeholderClasse)
        InitTextBox(txtUsername, placeholderPseudo)
        InitTextBox(txtEmail, placeholderEmail)
        InitTextBox(txtMDP, placeholderPassword)
        InitTextBox(txtConfirmMDP, placeholderConfirmPassword)
        InitTextBox(txtNiveau, placeholderNiveau)
        InitTextBox(txtDateNaissance, placeholderAge)


        txtMDP.UseSystemPasswordChar = False
        txtConfirmMDP.UseSystemPasswordChar = False
    End Sub



    Private Sub InitTextBox(tb As TextBox, placeholder As String)
        tb.BackColor = Color.Black
        tb.ForeColor = Color.Gray
        tb.BorderStyle = BorderStyle.None
        tb.Text = placeholder
    End Sub

    Private Sub HandleEnter(tb As TextBox, placeholder As String, isPassword As Boolean)
        If tb.Text = placeholder Then
            tb.Text = ""
            tb.ForeColor = Color.White
            If isPassword Then tb.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub HandleLeave(tb As TextBox, placeholder As String, isPassword As Boolean)
        If tb.Text = "" Then
            tb.Text = placeholder
            tb.ForeColor = Color.Gray
            If isPassword Then tb.UseSystemPasswordChar = False
        End If
    End Sub
    Private Sub txtNom_Enter(sender As Object, e As EventArgs) Handles txtNom.Enter
        HandleEnter(txtNom, placeholderNom, False)
    End Sub
    Private Sub txtNom_Leave(sender As Object, e As EventArgs) Handles txtNom.Leave
        HandleLeave(txtNom, placeholderNom, False)
    End Sub

    Private Sub txtPrenom_Enter(sender As Object, e As EventArgs) Handles txtPrenom.Enter
        HandleEnter(txtPrenom, placeholderPrenom, False)
    End Sub
    Private Sub txtPrenom_Leave(sender As Object, e As EventArgs) Handles txtPrenom.Leave
        HandleLeave(txtPrenom, placeholderPrenom, False)
    End Sub

    Private Sub txtClasse_Enter(sender As Object, e As EventArgs) Handles txtClasse.Enter
        HandleEnter(txtClasse, placeholderClasse, False)
    End Sub
    Private Sub txtClasse_Leave(sender As Object, e As EventArgs) Handles txtClasse.Leave
        HandleLeave(txtClasse, placeholderClasse, False)
    End Sub

    Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
        HandleEnter(txtUsername, placeholderPseudo, False)
    End Sub
    Private Sub txtUsername_Leave(sender As Object, e As EventArgs) Handles txtUsername.Leave
        HandleLeave(txtUsername, placeholderPseudo, False)
    End Sub

    Private Sub txtEmail_Enter(sender As Object, e As EventArgs) Handles txtEmail.Enter
        HandleEnter(txtEmail, placeholderEmail, False)
    End Sub
    Private Sub txtEmail_Leave(sender As Object, e As EventArgs) Handles txtEmail.Leave
        HandleLeave(txtEmail, placeholderEmail, False)
    End Sub

    Private Sub txtMDP_Enter(sender As Object, e As EventArgs) Handles txtMDP.Enter
        HandleEnter(txtMDP, placeholderPassword, True)

    End Sub
    Private Sub txtMDP_Leave(sender As Object, e As EventArgs) Handles txtMDP.Leave
        HandleLeave(txtMDP, placeholderPassword, True)

    End Sub

    Private Sub txtConfirmMDP_Enter(sender As Object, e As EventArgs) Handles txtConfirmMDP.Enter
        HandleEnter(txtConfirmMDP, placeholderConfirmPassword, True)

    End Sub
    Private Sub txtConfirmMDP_Leave(sender As Object, e As EventArgs) Handles txtConfirmMDP.Leave
        HandleLeave(txtConfirmMDP, placeholderConfirmPassword, True)

    End Sub

    Private Sub txtNiveau_Enter(sender As Object, e As EventArgs) Handles txtNiveau.Enter
        HandleLeave(txtNiveau, placeholderNiveau, False)
    End Sub
    Private Sub txtNiveau_Leave(sender As Object, e As EventArgs) Handles txtNiveau.Leave
        HandleLeave(txtNiveau, placeholderNiveau, False)
    End Sub
    Private Sub txtDateNaissance_Enter(sender As Object, e As EventArgs) Handles txtDateNaissance.Enter
        HandleEnter(txtDateNaissance, placeholderAge, False)
    End Sub
    Private Sub txtDateNaissance_Leave(sender As Object, e As EventArgs) Handles txtDateNaissance.Leave
        HandleLeave(txtDateNaissance, placeholderAge, False)
    End Sub

    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) Handles btnAnnuler.Click
        Me.Close()
    End Sub

    Private Sub btnVoirMDP_Click(sender As Object, e As EventArgs) Handles btnVoirMDP.Click
        If (txtMDP.UseSystemPasswordChar) Then
            txtMDP.UseSystemPasswordChar = False
        Else
            txtMDP.UseSystemPasswordChar = True

        End If

    End Sub

    Private Sub btnVoirConfirmMDP_Click(sender As Object, e As EventArgs) Handles btnVoirConfirmMDP.Click
        If (txtConfirmMDP.UseSystemPasswordChar) Then
            txtConfirmMDP.UseSystemPasswordChar = False
        Else
            txtConfirmMDP.UseSystemPasswordChar = True

        End If
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
        If Not String.IsNullOrWhiteSpace(txtDateNaissance.Text) Then
            If Not Integer.TryParse(txtDateNaissance.Text.Trim(), ageValue) Then
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

End Class
