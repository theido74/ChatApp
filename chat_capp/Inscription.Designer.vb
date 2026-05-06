<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inscription
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Inscription))
        Me.txtNom = New System.Windows.Forms.TextBox()
        Me.txtPrenom = New System.Windows.Forms.TextBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtMDP = New System.Windows.Forms.TextBox()
        Me.txtConfirmMDP = New System.Windows.Forms.TextBox()
        Me.txtDateNaissance = New System.Windows.Forms.TextBox()
        Me.txtNiveau = New System.Windows.Forms.TextBox()
        Me.txtClasse = New System.Windows.Forms.TextBox()
        Me.btnInscription = New System.Windows.Forms.Button()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        Me.btnVoirMDP = New System.Windows.Forms.Button()
        Me.btnVoirConfirmMDP = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtNom
        '
        Me.txtNom.Location = New System.Drawing.Point(152, 219)
        Me.txtNom.Multiline = True
        Me.txtNom.Name = "txtNom"
        Me.txtNom.Size = New System.Drawing.Size(131, 20)
        Me.txtNom.TabIndex = 0
        '
        'txtPrenom
        '
        Me.txtPrenom.Location = New System.Drawing.Point(348, 219)
        Me.txtPrenom.Multiline = True
        Me.txtPrenom.Name = "txtPrenom"
        Me.txtPrenom.Size = New System.Drawing.Size(131, 20)
        Me.txtPrenom.TabIndex = 1
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(152, 290)
        Me.txtUsername.Multiline = True
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(131, 20)
        Me.txtUsername.TabIndex = 2
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(348, 290)
        Me.txtEmail.Multiline = True
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(131, 20)
        Me.txtEmail.TabIndex = 3
        '
        'txtMDP
        '
        Me.txtMDP.Location = New System.Drawing.Point(152, 357)
        Me.txtMDP.Multiline = True
        Me.txtMDP.Name = "txtMDP"
        Me.txtMDP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtMDP.Size = New System.Drawing.Size(115, 20)
        Me.txtMDP.TabIndex = 4
        '
        'txtConfirmMDP
        '
        Me.txtConfirmMDP.Location = New System.Drawing.Point(348, 357)
        Me.txtConfirmMDP.Multiline = True
        Me.txtConfirmMDP.Name = "txtConfirmMDP"
        Me.txtConfirmMDP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmMDP.Size = New System.Drawing.Size(131, 20)
        Me.txtConfirmMDP.TabIndex = 5
        '
        'txtDateNaissance
        '
        Me.txtDateNaissance.Location = New System.Drawing.Point(152, 428)
        Me.txtDateNaissance.Multiline = True
        Me.txtDateNaissance.Name = "txtDateNaissance"
        Me.txtDateNaissance.Size = New System.Drawing.Size(139, 20)
        Me.txtDateNaissance.TabIndex = 6
        '
        'txtNiveau
        '
        Me.txtNiveau.Location = New System.Drawing.Point(348, 428)
        Me.txtNiveau.Multiline = True
        Me.txtNiveau.Name = "txtNiveau"
        Me.txtNiveau.Size = New System.Drawing.Size(151, 20)
        Me.txtNiveau.TabIndex = 7
        '
        'txtClasse
        '
        Me.txtClasse.Location = New System.Drawing.Point(152, 495)
        Me.txtClasse.Multiline = True
        Me.txtClasse.Name = "txtClasse"
        Me.txtClasse.Size = New System.Drawing.Size(131, 20)
        Me.txtClasse.TabIndex = 8
        '
        'btnInscription
        '
        Me.btnInscription.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnInscription.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInscription.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInscription.ForeColor = System.Drawing.Color.BlueViolet
        Me.btnInscription.Location = New System.Drawing.Point(126, 538)
        Me.btnInscription.Name = "btnInscription"
        Me.btnInscription.Size = New System.Drawing.Size(141, 34)
        Me.btnInscription.TabIndex = 9
        Me.btnInscription.Text = "INSCRIPTION"
        Me.btnInscription.UseVisualStyleBackColor = False
        '
        'btnAnnuler
        '
        Me.btnAnnuler.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnnuler.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnnuler.ForeColor = System.Drawing.Color.BlueViolet
        Me.btnAnnuler.Location = New System.Drawing.Point(358, 538)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(141, 34)
        Me.btnAnnuler.TabIndex = 11
        Me.btnAnnuler.Text = "ANNULER"
        Me.btnAnnuler.UseVisualStyleBackColor = False
        '
        'btnVoirMDP
        '
        Me.btnVoirMDP.BackgroundImage = CType(resources.GetObject("btnVoirMDP.BackgroundImage"), System.Drawing.Image)
        Me.btnVoirMDP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVoirMDP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVoirMDP.Location = New System.Drawing.Point(273, 357)
        Me.btnVoirMDP.Name = "btnVoirMDP"
        Me.btnVoirMDP.Size = New System.Drawing.Size(24, 21)
        Me.btnVoirMDP.TabIndex = 12
        Me.btnVoirMDP.Text = "Button1"
        Me.btnVoirMDP.UseVisualStyleBackColor = True
        '
        'btnVoirConfirmMDP
        '
        Me.btnVoirConfirmMDP.BackgroundImage = CType(resources.GetObject("btnVoirConfirmMDP.BackgroundImage"), System.Drawing.Image)
        Me.btnVoirConfirmMDP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVoirConfirmMDP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVoirConfirmMDP.Location = New System.Drawing.Point(475, 357)
        Me.btnVoirConfirmMDP.Name = "btnVoirConfirmMDP"
        Me.btnVoirConfirmMDP.Size = New System.Drawing.Size(24, 21)
        Me.btnVoirConfirmMDP.TabIndex = 13
        Me.btnVoirConfirmMDP.Text = "Button1"
        Me.btnVoirConfirmMDP.UseVisualStyleBackColor = True
        '
        'Inscription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(630, 596)
        Me.Controls.Add(Me.btnVoirConfirmMDP)
        Me.Controls.Add(Me.btnVoirMDP)
        Me.Controls.Add(Me.btnAnnuler)
        Me.Controls.Add(Me.btnInscription)
        Me.Controls.Add(Me.txtClasse)
        Me.Controls.Add(Me.txtNiveau)
        Me.Controls.Add(Me.txtDateNaissance)
        Me.Controls.Add(Me.txtConfirmMDP)
        Me.Controls.Add(Me.txtMDP)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.txtPrenom)
        Me.Controls.Add(Me.txtNom)
        Me.Name = "Inscription"
        Me.Text = "Inscription"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtNom As TextBox
    Friend WithEvents txtPrenom As TextBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtMDP As TextBox
    Friend WithEvents txtConfirmMDP As TextBox
    Friend WithEvents txtDateNaissance As TextBox
    Friend WithEvents txtNiveau As TextBox
    Friend WithEvents txtClasse As TextBox
    Friend WithEvents btnInscription As Button
    Friend WithEvents btnAnnuler As Button
    Friend WithEvents btnVoirMDP As Button
    Friend WithEvents btnVoirConfirmMDP As Button
End Class
