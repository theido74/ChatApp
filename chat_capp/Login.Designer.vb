<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtMDP = New System.Windows.Forms.TextBox()
        Me.btnVoirMDP = New System.Windows.Forms.Button()
        Me.btnConnexion = New System.Windows.Forms.Button()
        Me.lblCreeCompte = New System.Windows.Forms.LinkLabel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(151, 218)
        Me.txtUsername.Multiline = True
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(140, 20)
        Me.txtUsername.TabIndex = 0
        '
        'txtMDP
        '
        Me.txtMDP.Location = New System.Drawing.Point(151, 293)
        Me.txtMDP.Multiline = True
        Me.txtMDP.Name = "txtMDP"
        Me.txtMDP.Size = New System.Drawing.Size(238, 20)
        Me.txtMDP.TabIndex = 1
        '
        'btnVoirMDP
        '
        Me.btnVoirMDP.BackgroundImage = CType(resources.GetObject("btnVoirMDP.BackgroundImage"), System.Drawing.Image)
        Me.btnVoirMDP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVoirMDP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVoirMDP.Location = New System.Drawing.Point(404, 292)
        Me.btnVoirMDP.Name = "btnVoirMDP"
        Me.btnVoirMDP.Size = New System.Drawing.Size(24, 21)
        Me.btnVoirMDP.TabIndex = 2
        Me.btnVoirMDP.Text = "Button1"
        Me.btnVoirMDP.UseVisualStyleBackColor = True
        '
        'btnConnexion
        '
        Me.btnConnexion.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnConnexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConnexion.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConnexion.ForeColor = System.Drawing.Color.BlueViolet
        Me.btnConnexion.Location = New System.Drawing.Point(124, 341)
        Me.btnConnexion.Name = "btnConnexion"
        Me.btnConnexion.Size = New System.Drawing.Size(304, 50)
        Me.btnConnexion.TabIndex = 3
        Me.btnConnexion.Text = "SE CONNECTER"
        Me.btnConnexion.UseVisualStyleBackColor = False
        '
        'lblCreeCompte
        '
        Me.lblCreeCompte.AutoSize = True
        Me.lblCreeCompte.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCreeCompte.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreeCompte.LinkColor = System.Drawing.Color.BlueViolet
        Me.lblCreeCompte.Location = New System.Drawing.Point(346, 502)
        Me.lblCreeCompte.Name = "lblCreeCompte"
        Me.lblCreeCompte.Size = New System.Drawing.Size(106, 19)
        Me.lblCreeCompte.TabIndex = 4
        Me.lblCreeCompte.TabStop = True
        Me.lblCreeCompte.Text = "Crée un compte"
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblMessage.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.IndianRed
        Me.lblMessage.Location = New System.Drawing.Point(148, 415)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(0, 18)
        Me.lblMessage.TabIndex = 5
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(562, 543)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lblCreeCompte)
        Me.Controls.Add(Me.btnConnexion)
        Me.Controls.Add(Me.btnVoirMDP)
        Me.Controls.Add(Me.txtMDP)
        Me.Controls.Add(Me.txtUsername)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Login"
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtMDP As TextBox
    Friend WithEvents btnVoirMDP As Button
    Friend WithEvents btnConnexion As Button
    Friend WithEvents lblCreeCompte As LinkLabel
    Friend WithEvents lblMessage As Label
End Class
