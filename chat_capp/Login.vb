Imports System.Drawing.Drawing2D

Public Class Login


    Private logger As New LogService()
    Private authService As New AuthenticationService()

    Private clientValidator As New ClientValidator()
    Private Const MESSAGEFAILED As String = "LOG ERROR"

    Public ConnectedUserID As Integer = 0




    Private placeholderUsername As String = "Entrez votre Username"
    Private placeholderPassword As String = "Entrez votre mot de passe"

    Private Sub HandleEnter(tb As TextBox, placeholder As String, isPassword As Boolean)
        If tb.Text = placeholder Then
            tb.Text = ""
            tb.ForeColor = Color.White
            If isPassword Then tb.UseSystemPasswordChar = True
        End If
    End Sub
    Private Sub InitTextBox(tb As TextBox, placeholder As String)
        tb.BackColor = Color.Black
        tb.ForeColor = Color.Gray
        tb.BorderStyle = BorderStyle.None
        tb.Text = placeholder
    End Sub
    Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
        HandleEnter(txtUsername, placeholderUsername, False)
    End Sub

    Private Sub txtMDP_Enter(sender As Object, e As EventArgs) Handles txtMDP.Enter
        HandleEnter(txtMDP, placeholderPassword, True)

    End Sub

    Private Sub txtMDP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMDP.KeyPress
        ' Vérifier si la touche pressée est Enter (code ASCII 13)
        If e.KeyChar = Chr(13) Then
            ' Empêcher le comportement par défaut (bip sonore)
            e.Handled = True
            ' Lancer la connexion
            PerformLogin()
        End If
    End Sub
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitTextBox(txtUsername, placeholderUsername)
        InitTextBox(txtMDP, placeholderPassword)
        txtMDP.UseSystemPasswordChar = False

    End Sub

    Private Sub lblCreeCompteClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblCreeCompte.LinkClicked
        Dim inscriptionForm As New Inscription()
        inscriptionForm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub BtnConnexion_Click(sender As Object, e As EventArgs) Handles btnConnexion.Click
        PerformLogin()
    End Sub
    Public Sub AfficherErreur(message As String)
        If Me.lblMessage IsNot Nothing Then
            lblMessage.ForeColor = Color.FromArgb(220, 53, 69)
            lblMessage.Text = message
        Else
            MessageBox.Show(message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub AfficherSucces(message As String)
        If Me.lblMessage IsNot Nothing Then
            lblMessage.ForeColor = Color.FromArgb(40, 167, 69)
            lblMessage.Text = message
        Else
            MessageBox.Show(message, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub PerformLogin()
        Try
            ' ===== ÉTAPE 1 : Récupérer les valeurs des champs =====
            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtMDP.Text

            ' ===== ÉTAPE 2 : Valider côté client (avant envoi) =====
            If Not clientValidator.ValidateUsername(username) Then
                AfficherErreur("Nom d'utilisateur invalide (4-20 caractères)")
                clientValidator.GetValidationMessage(username, password)

                txtMDP.Clear()
                Return
            End If

            If Not clientValidator.ValidatePassword(password) Then

                AfficherErreur("Mot de passe invalide (8-50 caractères minimum)")

                clientValidator.GetValidationMessage(username, password)
                txtMDP.Clear()
                Return
            End If

            ' ===== ÉTAPE 3 : Appeler le service d'authentification =====
            ' Vérifier les identifiants auprès du service
            Dim isAuthenticated As Boolean = authService.Authenticate(username, password)

            ' ===== ÉTAPE 4 : Si authentification réussie =====
            If isAuthenticated Then
                ' Récupérer les informations complètes de l'utilisateur
                Dim user As User = authService.GetEleveByUsername(username)

                ' Vérifier que l'utilisateur a été trouvé
                If user Is Nothing Then
                    AfficherErreur("Erreur : Utilisateur introuvable")
                    Return
                End If

                ' Afficher le message de succès en vert
                AfficherSucces("Connexion réussie ! Redirection en cours...")

                ' Stocker l'ID utilisateur connecté
                ConnectedUserID = user.UserID

                ' Créer et ouvrir le formulaire principal avec l'ID utilisateur
                Dim mainForm As New Main()
                ' TODO: Passer l'ID utilisateur au formulaire principal si nécessaire
                ' mainForm.SetConnectedUserId(user.UserID)

                ' Attendre un court instant (pour voir le message de succès)
                System.Threading.Thread.Sleep(500)

                ' Masquer le formulaire de connexion
                Me.Hide()
                ' Afficher le formulaire principal
                mainForm.ShowDialog()

                ' Si le formulaire principal se ferme, fermer aussi la connexion
                Me.Close()
            End If

        Catch ex As UnauthorizedAccessException
            ' Erreur d'authentification (identifiants incorrects)
            logger.AjoutLog(999, MESSAGEFAILED)

            AfficherErreur(ex.Message)

            ' Vider le champ password pour sécurité
            txtMDP.Clear()

        Catch ex As NotImplementedException
            ' Service pas encore implémenté
            AfficherErreur("Service d'authentification en cours d'implémentation...")

        Catch ex As Exception
            ' Erreur générique
            AfficherErreur("Erreur lors de la connexion : " & ex.Message)
            ' Vider le champ password
            txtMDP.Clear()

        End Try
    End Sub
End Class