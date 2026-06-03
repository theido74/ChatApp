Imports System.Drawing.Drawing2D

Public Class Login


    Private logger As New LogService()
    Private authService As New AuthenticationService()
    Private clientValidator As New ClientValidator()
    Private Const MESSAGEFAILED As String = "LOG ERROR"

    Public ConnectedUserID As Integer = 0




    Private placeholderUsername As String = "Entrez votre Username"
    Private placeholderPassword As String = "Entrez votre mot de passe"

    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement Enter pour les TextBox de username et password.
    ''' Si le texte actuel est le placeholder, il est effacé et la couleur du texte est changée.
    ''' </summary>
    ''' <param name="tb">Le TextBox concerné</param>
    ''' <param name="placeholder">Le texte placeholder à vérifier</param>
    ''' <param name="isPassword">Indique si le TextBox est pour un mot de passe</param>
    Private Sub HandleEnter(tb As TextBox, placeholder As String, isPassword As Boolean)
        If tb.Text = placeholder Then
            tb.Text = ""
            tb.ForeColor = Color.White
            If isPassword Then tb.UseSystemPasswordChar = True
        End If
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Initialise un TextBox avec les propriétés de style et le texte placeholder.
    ''' </summary>
    ''' <param name="tb">Le TextBox à initialiser</param>
    ''' <param name="placeholder">Le texte placeholder à afficher</param>
    Private Sub InitTextBox(tb As TextBox, placeholder As String)
        tb.BackColor = Color.Black
        tb.ForeColor = Color.Gray
        tb.BorderStyle = BorderStyle.None
        tb.Text = placeholder
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement Enter pour le TextBox de username.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
        HandleEnter(txtUsername, placeholderUsername, False)
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement Enter pour le TextBox de mot de passe.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub txtMDP_Enter(sender As Object, e As EventArgs) Handles txtMDP.Enter
        HandleEnter(txtMDP, placeholderPassword, True)

    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement KeyPress pour le TextBox de mot de passe.
    ''' Si l'utilisateur appuie sur la touche Enter, cela déclenche la tentative de connexion.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement LinkClicked pour le LinkLabel de création de compte.
    ''' Ouvre le formulaire d'inscription et masque le formulaire de connexion.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lblCreeCompteClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblCreeCompte.LinkClicked
        Dim inscriptionForm As New Inscription()
        Me.Hide()
        inscriptionForm.ShowDialog()

    End Sub

    Private Sub BtnConnexion_Click(sender As Object, e As EventArgs) Handles btnConnexion.Click
        PerformLogin()
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Affiche un message d'erreur dans le label de message s'il existe, sinon affiche une MessageBox.
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub AfficherErreur(message As String)
        If Me.lblMessage IsNot Nothing Then
            lblMessage.ForeColor = Color.FromArgb(220, 53, 69)
            lblMessage.Text = message
        Else
            MessageBox.Show(message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ''' <summary>
    ''' Author: Ayman
    ''' Affiche un message de succès dans le label de message s'il existe, sinon affiche une MessageBox.
    ''' </summary>
    ''' <param name="message"></param>
    Private Sub AfficherSucces(message As String)
        If Me.lblMessage IsNot Nothing Then
            lblMessage.ForeColor = Color.FromArgb(40, 167, 69)
            lblMessage.Text = message
        Else
            MessageBox.Show(message, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    ''' <summary>
    ''' Author: Ayman
    ''' Effectue la tentative de connexion en validant les entrées, en appelant le service d'authentification et en gérant les différentes exceptions possibles.
    ''' </summary>
    Private Sub PerformLogin()
        Try

            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtMDP.Text


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

            Dim isAuthenticated As Boolean = authService.Authenticate(username, password)


            If isAuthenticated Then

                Dim user As User = authService.GetEleveByUsername(username)

                If user Is Nothing Then
                    AfficherErreur("Erreur : Utilisateur introuvable")
                    Return
                End If

                AfficherSucces("Connexion réussie ! Redirection en cours...")


                ConnectedUserID = user.UserID
                CurrentUser.User = user  'STOCKER L'UTILISATEUR

                Dim mainForm As New Main()

                ' Attendre un court instant (pour voir le message de succès)
                System.Threading.Thread.Sleep(500)

                Me.Hide()

                mainForm.ShowDialog()
                Me.Close() ' Si le formulaire principal se ferme, fermer aussi la connexion
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

    ''' <summary>
    ''' Author: Ayman
    ''' Gère l'événement Click pour le bouton de voir/masquer le mot de passe.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnVoirMDP_Click(sender As Object, e As EventArgs) Handles btnVoirMDP.Click
        If (txtMDP.UseSystemPasswordChar) Then
            txtMDP.UseSystemPasswordChar = False
        Else
            txtMDP.UseSystemPasswordChar = True

        End If
    End Sub
End Class