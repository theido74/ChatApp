<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Forum
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Forum))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblNomForum = New System.Windows.Forms.Label()
        Me.txtMessge = New System.Windows.Forms.TextBox()
        Me.btnEnvoyer = New System.Windows.Forms.Button()
        Me.flpFenetreMessage = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.lblUtilisateurs = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.dgvUtilisateursForum = New System.Windows.Forms.DataGridView()
        CType(Me.dgvUtilisateursForum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblNomForum
        '
        Me.lblNomForum.AutoSize = True
        Me.lblNomForum.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNomForum.Font = New System.Drawing.Font("Arial Black", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNomForum.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblNomForum.Location = New System.Drawing.Point(326, 23)
        Me.lblNomForum.Name = "lblNomForum"
        Me.lblNomForum.Size = New System.Drawing.Size(138, 30)
        Me.lblNomForum.TabIndex = 0
        Me.lblNomForum.Text = "ForumNom"
        '
        'txtMessge
        '
        Me.txtMessge.Location = New System.Drawing.Point(293, 515)
        Me.txtMessge.Multiline = True
        Me.txtMessge.Name = "txtMessge"
        Me.txtMessge.Size = New System.Drawing.Size(383, 20)
        Me.txtMessge.TabIndex = 1
        '
        'btnEnvoyer
        '
        Me.btnEnvoyer.BackgroundImage = CType(resources.GetObject("btnEnvoyer.BackgroundImage"), System.Drawing.Image)
        Me.btnEnvoyer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnvoyer.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnEnvoyer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnvoyer.Location = New System.Drawing.Point(768, 508)
        Me.btnEnvoyer.Name = "btnEnvoyer"
        Me.btnEnvoyer.Size = New System.Drawing.Size(76, 32)
        Me.btnEnvoyer.TabIndex = 2
        Me.btnEnvoyer.UseVisualStyleBackColor = True
        '
        'flpFenetreMessage
        '
        Me.flpFenetreMessage.AutoScroll = True
        Me.flpFenetreMessage.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.flpFenetreMessage.Location = New System.Drawing.Point(289, 91)
        Me.flpFenetreMessage.Name = "flpFenetreMessage"
        Me.flpFenetreMessage.Size = New System.Drawing.Size(554, 408)
        Me.flpFenetreMessage.TabIndex = 4
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Location = New System.Drawing.Point(698, 522)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(0, 13)
        Me.lblTime.TabIndex = 5
        '
        'lblUtilisateurs
        '
        Me.lblUtilisateurs.AutoSize = True
        Me.lblUtilisateurs.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUtilisateurs.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUtilisateurs.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblUtilisateurs.Location = New System.Drawing.Point(68, 72)
        Me.lblUtilisateurs.Name = "lblUtilisateurs"
        Me.lblUtilisateurs.Size = New System.Drawing.Size(126, 19)
        Me.lblUtilisateurs.TabIndex = 10
        Me.lblUtilisateurs.Text = "Liste des Utilisateurs :"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUsername.Font = New System.Drawing.Font("Bahnschrift Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblUsername.Location = New System.Drawing.Point(68, 91)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(111, 19)
        Me.lblUsername.TabIndex = 3
        Me.lblUsername.Text = "#CurrentUsername"
        '
        'dgvUtilisateursForum
        '
        Me.dgvUtilisateursForum.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgvUtilisateursForum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUtilisateursForum.ColumnHeadersVisible = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.MenuText
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUtilisateursForum.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvUtilisateursForum.Location = New System.Drawing.Point(66, 113)
        Me.dgvUtilisateursForum.Name = "dgvUtilisateursForum"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUtilisateursForum.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvUtilisateursForum.Size = New System.Drawing.Size(194, 389)
        Me.dgvUtilisateursForum.TabIndex = 11
        '
        'Forum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(923, 598)
        Me.Controls.Add(Me.dgvUtilisateursForum)
        Me.Controls.Add(Me.lblUtilisateurs)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.flpFenetreMessage)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.btnEnvoyer)
        Me.Controls.Add(Me.txtMessge)
        Me.Controls.Add(Me.lblNomForum)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Forum"
        Me.Text = "Forum"
        CType(Me.dgvUtilisateursForum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblNomForum As Label
    Friend WithEvents txtMessge As TextBox
    Friend WithEvents btnEnvoyer As Button
    Friend WithEvents flpFenetreMessage As FlowLayoutPanel
    Friend WithEvents lblTime As Label
    Friend WithEvents lblUtilisateurs As Label
    Friend WithEvents lblUsername As Label
    Friend WithEvents dgvUtilisateursForum As DataGridView
End Class
