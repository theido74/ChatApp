<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.panelConnection = New System.Windows.Forms.Panel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnShowPass = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.txtMDP = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.PBuser = New System.Windows.Forms.PictureBox()
        Me.lblSignUp = New System.Windows.Forms.Label()
        Me.lblQuitter = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lbluser = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.panelConnection.SuspendLayout()
        CType(Me.PBuser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Indigo
        Me.Panel1.Controls.Add(Me.panelConnection)
        Me.Panel1.Controls.Add(Me.PBuser)
        Me.Panel1.Controls.Add(Me.lblSignUp)
        Me.Panel1.Controls.Add(Me.lblQuitter)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 418)
        Me.Panel1.TabIndex = 0
        '
        'panelConnection
        '
        Me.panelConnection.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.panelConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelConnection.Controls.Add(Me.Label1)
        Me.panelConnection.Controls.Add(Me.lbluser)
        Me.panelConnection.Controls.Add(Me.lblMessage)
        Me.panelConnection.Controls.Add(Me.btnShowPass)
        Me.panelConnection.Controls.Add(Me.Button1)
        Me.panelConnection.Controls.Add(Me.txtUserName)
        Me.panelConnection.Controls.Add(Me.txtMDP)
        Me.panelConnection.Controls.Add(Me.btnLogin)
        Me.panelConnection.Location = New System.Drawing.Point(296, 157)
        Me.panelConnection.Name = "panelConnection"
        Me.panelConnection.Size = New System.Drawing.Size(208, 261)
        Me.panelConnection.TabIndex = 1
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(16, 110)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(176, 36)
        Me.lblMessage.TabIndex = 7
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnShowPass
        '
        Me.btnShowPass.Location = New System.Drawing.Point(154, 84)
        Me.btnShowPass.Name = "btnShowPass"
        Me.btnShowPass.Size = New System.Drawing.Size(38, 23)
        Me.btnShowPass.TabIndex = 6
        Me.btnShowPass.Text = "👁"
        Me.btnShowPass.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(59, 216)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Quitter"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtUserName
        '
        Me.txtUserName.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.txtUserName.Location = New System.Drawing.Point(34, 35)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(115, 20)
        Me.txtUserName.TabIndex = 1
        Me.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMDP
        '
        Me.txtMDP.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.txtMDP.Location = New System.Drawing.Point(34, 87)
        Me.txtMDP.Name = "txtMDP"
        Me.txtMDP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtMDP.Size = New System.Drawing.Size(100, 20)
        Me.txtMDP.TabIndex = 2
        Me.txtMDP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.DarkViolet
        Me.btnLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.ForeColor = System.Drawing.SystemColors.Control
        Me.btnLogin.Location = New System.Drawing.Point(34, 161)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(144, 49)
        Me.btnLogin.TabIndex = 4
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'PBuser
        '
        Me.PBuser.Image = CType(resources.GetObject("PBuser.Image"), System.Drawing.Image)
        Me.PBuser.Location = New System.Drawing.Point(356, 71)
        Me.PBuser.Name = "PBuser"
        Me.PBuser.Size = New System.Drawing.Size(90, 93)
        Me.PBuser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBuser.TabIndex = 3
        Me.PBuser.TabStop = False
        '
        'lblSignUp
        '
        Me.lblSignUp.AutoSize = True
        Me.lblSignUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSignUp.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblSignUp.Location = New System.Drawing.Point(342, 43)
        Me.lblSignUp.Name = "lblSignUp"
        Me.lblSignUp.Size = New System.Drawing.Size(124, 25)
        Me.lblSignUp.TabIndex = 2
        Me.lblSignUp.Text = "Connexion"
        '
        'lblQuitter
        '
        Me.lblQuitter.AutoSize = True
        Me.lblQuitter.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuitter.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblQuitter.Location = New System.Drawing.Point(763, 9)
        Me.lblQuitter.Name = "lblQuitter"
        Me.lblQuitter.Size = New System.Drawing.Size(25, 24)
        Me.lblQuitter.TabIndex = 1
        Me.lblQuitter.Text = "X"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(-12, 399)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(812, 187)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'lbluser
        '
        Me.lbluser.AutoSize = True
        Me.lbluser.Location = New System.Drawing.Point(31, 19)
        Me.lbluser.Name = "lbluser"
        Me.lbluser.Size = New System.Drawing.Size(87, 13)
        Me.lbluser.TabIndex = 8
        Me.lbluser.Text = "Nom d'utilisateur:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Mot de passe:"
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(800, 555)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Login"
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.panelConnection.ResumeLayout(False)
        Me.panelConnection.PerformLayout()
        CType(Me.PBuser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents panelConnection As Panel
    Friend WithEvents lblSignUp As Label
    Friend WithEvents lblQuitter As Label
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents txtMDP As TextBox
    Friend WithEvents btnLogin As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PBuser As PictureBox
    Friend WithEvents btnShowPass As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents lblMessage As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lbluser As Label
End Class
