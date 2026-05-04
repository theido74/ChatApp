<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inscription
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Inscription))
        Me.pnlInscription = New System.Windows.Forms.Panel()
        Me.txtNiveau = New System.Windows.Forms.TextBox()
        Me.lblNiveau = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAge = New System.Windows.Forms.TextBox()
        Me.lblAge = New System.Windows.Forms.Label()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        Me.btnInscription = New System.Windows.Forms.Button()
        Me.txtConfirmMDP = New System.Windows.Forms.TextBox()
        Me.txtMDP = New System.Windows.Forms.TextBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPrenom = New System.Windows.Forms.TextBox()
        Me.txtNom = New System.Windows.Forms.TextBox()
        Me.lblConfirmerMDP = New System.Windows.Forms.Label()
        Me.lblMDP = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblPrenom = New System.Windows.Forms.Label()
        Me.lblNom = New System.Windows.Forms.Label()
        Me.lblSignup = New System.Windows.Forms.Label()
        Me.pnlInscription.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlInscription
        '
        Me.pnlInscription.BackColor = System.Drawing.Color.DarkOrchid
        Me.pnlInscription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInscription.Controls.Add(Me.txtNiveau)
        Me.pnlInscription.Controls.Add(Me.lblNiveau)
        Me.pnlInscription.Controls.Add(Me.txtEmail)
        Me.pnlInscription.Controls.Add(Me.Label1)
        Me.pnlInscription.Controls.Add(Me.txtAge)
        Me.pnlInscription.Controls.Add(Me.lblAge)
        Me.pnlInscription.Controls.Add(Me.btnAnnuler)
        Me.pnlInscription.Controls.Add(Me.btnInscription)
        Me.pnlInscription.Controls.Add(Me.txtConfirmMDP)
        Me.pnlInscription.Controls.Add(Me.txtMDP)
        Me.pnlInscription.Controls.Add(Me.txtUsername)
        Me.pnlInscription.Controls.Add(Me.txtPrenom)
        Me.pnlInscription.Controls.Add(Me.txtNom)
        Me.pnlInscription.Controls.Add(Me.lblConfirmerMDP)
        Me.pnlInscription.Controls.Add(Me.lblMDP)
        Me.pnlInscription.Controls.Add(Me.lblUsername)
        Me.pnlInscription.Controls.Add(Me.lblPrenom)
        Me.pnlInscription.Controls.Add(Me.lblNom)
        Me.pnlInscription.Controls.Add(Me.lblSignup)
        Me.pnlInscription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pnlInscription.Location = New System.Drawing.Point(246, 45)
        Me.pnlInscription.Name = "pnlInscription"
        Me.pnlInscription.Size = New System.Drawing.Size(308, 361)
        Me.pnlInscription.TabIndex = 2
        '
        'txtNiveau
        '
        Me.txtNiveau.Location = New System.Drawing.Point(174, 192)
        Me.txtNiveau.Name = "txtNiveau"
        Me.txtNiveau.Size = New System.Drawing.Size(120, 20)
        Me.txtNiveau.TabIndex = 20
        '
        'lblNiveau
        '
        Me.lblNiveau.AutoSize = True
        Me.lblNiveau.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNiveau.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblNiveau.Location = New System.Drawing.Point(170, 169)
        Me.lblNiveau.Name = "lblNiveau"
        Me.lblNiveau.Size = New System.Drawing.Size(59, 20)
        Me.lblNiveau.TabIndex = 19
        Me.lblNiveau.Text = "Niveau :"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(174, 146)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(120, 20)
        Me.txtEmail.TabIndex = 18
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(170, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 20)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Email :"
        '
        'txtAge
        '
        Me.txtAge.Location = New System.Drawing.Point(174, 100)
        Me.txtAge.Name = "txtAge"
        Me.txtAge.Size = New System.Drawing.Size(120, 20)
        Me.txtAge.TabIndex = 16
        '
        'lblAge
        '
        Me.lblAge.AutoSize = True
        Me.lblAge.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAge.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblAge.Location = New System.Drawing.Point(170, 77)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(41, 20)
        Me.lblAge.TabIndex = 14
        Me.lblAge.Text = "Age :"
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnnuler.Location = New System.Drawing.Point(230, 335)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(75, 23)
        Me.btnAnnuler.TabIndex = 13
        Me.btnAnnuler.Text = "Annuler"
        Me.btnAnnuler.UseVisualStyleBackColor = True
        '
        'btnInscription
        '
        Me.btnInscription.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInscription.Location = New System.Drawing.Point(3, 333)
        Me.btnInscription.Name = "btnInscription"
        Me.btnInscription.Size = New System.Drawing.Size(75, 23)
        Me.btnInscription.TabIndex = 12
        Me.btnInscription.Text = "S'inscrire"
        Me.btnInscription.UseVisualStyleBackColor = True
        '
        'txtConfirmMDP
        '
        Me.txtConfirmMDP.Location = New System.Drawing.Point(16, 284)
        Me.txtConfirmMDP.Name = "txtConfirmMDP"
        Me.txtConfirmMDP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmMDP.Size = New System.Drawing.Size(162, 20)
        Me.txtConfirmMDP.TabIndex = 10
        '
        'txtMDP
        '
        Me.txtMDP.Location = New System.Drawing.Point(16, 238)
        Me.txtMDP.Name = "txtMDP"
        Me.txtMDP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtMDP.Size = New System.Drawing.Size(162, 20)
        Me.txtMDP.TabIndex = 9
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(16, 192)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(120, 20)
        Me.txtUsername.TabIndex = 8
        '
        'txtPrenom
        '
        Me.txtPrenom.Location = New System.Drawing.Point(16, 146)
        Me.txtPrenom.Name = "txtPrenom"
        Me.txtPrenom.Size = New System.Drawing.Size(120, 20)
        Me.txtPrenom.TabIndex = 7
        '
        'txtNom
        '
        Me.txtNom.Location = New System.Drawing.Point(16, 100)
        Me.txtNom.Name = "txtNom"
        Me.txtNom.Size = New System.Drawing.Size(120, 20)
        Me.txtNom.TabIndex = 6
        '
        'lblConfirmerMDP
        '
        Me.lblConfirmerMDP.AutoSize = True
        Me.lblConfirmerMDP.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfirmerMDP.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblConfirmerMDP.Location = New System.Drawing.Point(12, 261)
        Me.lblConfirmerMDP.Name = "lblConfirmerMDP"
        Me.lblConfirmerMDP.Size = New System.Drawing.Size(166, 20)
        Me.lblConfirmerMDP.TabIndex = 5
        Me.lblConfirmerMDP.Text = "Confirmer mot de passe :"
        '
        'lblMDP
        '
        Me.lblMDP.AutoSize = True
        Me.lblMDP.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDP.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblMDP.Location = New System.Drawing.Point(12, 215)
        Me.lblMDP.Name = "lblMDP"
        Me.lblMDP.Size = New System.Drawing.Size(99, 20)
        Me.lblMDP.TabIndex = 4
        Me.lblMDP.Text = "Mot de passe :"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblUsername.Location = New System.Drawing.Point(12, 169)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(79, 20)
        Me.lblUsername.TabIndex = 3
        Me.lblUsername.Text = "Username :"
        '
        'lblPrenom
        '
        Me.lblPrenom.AutoSize = True
        Me.lblPrenom.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrenom.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblPrenom.Location = New System.Drawing.Point(12, 123)
        Me.lblPrenom.Name = "lblPrenom"
        Me.lblPrenom.Size = New System.Drawing.Size(70, 20)
        Me.lblPrenom.TabIndex = 2
        Me.lblPrenom.Text = "Prenom : "
        '
        'lblNom
        '
        Me.lblNom.AutoSize = True
        Me.lblNom.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNom.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblNom.Location = New System.Drawing.Point(12, 77)
        Me.lblNom.Name = "lblNom"
        Me.lblNom.Size = New System.Drawing.Size(46, 20)
        Me.lblNom.TabIndex = 1
        Me.lblNom.Text = "Nom :"
        '
        'lblSignup
        '
        Me.lblSignup.AutoSize = True
        Me.lblSignup.Font = New System.Drawing.Font("Arial Narrow", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSignup.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblSignup.Location = New System.Drawing.Point(107, 18)
        Me.lblSignup.Name = "lblSignup"
        Me.lblSignup.Size = New System.Drawing.Size(104, 25)
        Me.lblSignup.TabIndex = 0
        Me.lblSignup.Text = "Inscription"
        '
        'Inscription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlInscription)
        Me.Name = "Inscription"
        Me.Text = "Inscription"
        Me.pnlInscription.ResumeLayout(False)
        Me.pnlInscription.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlInscription As Panel
    Friend WithEvents txtNiveau As TextBox
    Friend WithEvents lblNiveau As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAge As TextBox
    Friend WithEvents lblAge As Label
    Friend WithEvents btnAnnuler As Button
    Friend WithEvents btnInscription As Button
    Friend WithEvents txtConfirmMDP As TextBox
    Friend WithEvents txtMDP As TextBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPrenom As TextBox
    Friend WithEvents txtNom As TextBox
    Friend WithEvents lblConfirmerMDP As Label
    Friend WithEvents lblMDP As Label
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblPrenom As Label
    Friend WithEvents lblNom As Label
    Friend WithEvents lblSignup As Label
End Class
