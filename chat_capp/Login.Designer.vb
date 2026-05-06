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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
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
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.BlueViolet
        Me.Button1.Location = New System.Drawing.Point(124, 341)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(304, 50)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "SE CONNECTER"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LinkLabel1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkColor = System.Drawing.Color.BlueViolet
        Me.LinkLabel1.Location = New System.Drawing.Point(346, 502)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(106, 19)
        Me.LinkLabel1.TabIndex = 4
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Crée un compte"
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(562, 543)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button1)
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
    Friend WithEvents Button1 As Button
    Friend WithEvents LinkLabel1 As LinkLabel
End Class
