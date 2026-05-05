Partial Public Class Login


    Private authService As New AuthenticationService()

    Private clientValidator As New ClientValidator()

    Public ConnectedUserID As Integer = 0

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Appeler la méthode de connexion
        PerformLogin()
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

    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Fermer complètement l'application
        Application.Exit()
    End Sub

    Private Sub lblQuitter_Click(sender As Object, e As EventArgs) Handles lblQuitter.Click
        ' Fermer le formulaire
        Me.Close()
    End Sub

    ' ========== LOGIQUE DE CONNEXION ==========

    ''' <summary>
    ''' Logique principale de connexion
    ''' Valide les entrées et appelle le service d'authentification
    ''' </summary>
    Private Sub PerformLogin()
        Try
            ' ===== ÉTAPE 1 : Récupérer les valeurs des champs =====
            Dim username As String = txtUserName.Text.Trim()
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

    ' ========== MÉTHODES UTILITAIRES ==========

    ''' <summary>
    ''' Affiche un message d'erreur en rouge
    ''' </summary>
    ''' <param name="message">Le message d'erreur à afficher</param>
    Public Sub AfficherErreur(message As String)
        If Me.lblMessage IsNot Nothing Then
            lblMessage.ForeColor = Color.FromArgb(220, 53, 69)
            lblMessage.Text = message
        Else
            MessageBox.Show(message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ''' <summary>
    ''' Affiche un message de succès en vert
    ''' </summary>
    ''' <param name="message">Le message de succès à afficher</param>
    Private Sub AfficherSucces(message As String)
        If Me.lblMessage IsNot Nothing Then
            lblMessage.ForeColor = Color.FromArgb(40, 167, 69)
            lblMessage.Text = message
        Else
            MessageBox.Show(message, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnShowPass_Click(sender As Object, e As EventArgs) Handles btnShowPass.Click
        ' Toggle password visibility
        If txtMDP.PasswordChar = ChrW(0) Then
            txtMDP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            btnShowPass.Text = "Show"
        Else
            txtMDP.PasswordChar = ChrW(0)
            btnShowPass.Text = "Hide"
        End If
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUserName.Text = String.Empty
        txtMDP.Text = String.Empty
        txtUserName.Focus()
        ' ToolTip for show/hide password
        Try
            Dim tt As New ToolTip()
            tt.SetToolTip(btnShowPass, "Afficher/Masquer le mot de passe")
        Catch
        End Try
    End Sub

    Private Sub RoundControl(ctrl As Control, radius As Integer)
        Try
            Dim gp As New Drawing2D.GraphicsPath()
            gp.AddArc(0, 0, radius, radius, 180, 90)
            gp.AddArc(ctrl.Width - radius, 0, radius, radius, 270, 90)
            gp.AddArc(ctrl.Width - radius, ctrl.Height - radius, radius, radius, 0, 90)
            gp.AddArc(0, ctrl.Height - radius, radius, radius, 90, 90)
            gp.CloseFigure()
            ctrl.Region = New Region(gp)
        Catch
            ' ignore if control not yet fully initialized
        End Try
    End Sub

    ''' <summary>
    ''' À l'affichage, arrondir légèrement les boutons
    ''' </summary>
    Private Sub Login_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        RoundControl(btnLogin, 8)
        RoundControl(Button1, 8)
    End Sub



    Private Sub Login_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub




    Private Sub Login_Load_3(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
