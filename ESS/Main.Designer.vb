<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblUsername2 = New System.Windows.Forms.Label()
        Me.lblClasse = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ForumToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CréerUnForumToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrouverUnForumToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MessagesPrivésToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblNotification = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblUsername.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblUsername.Location = New System.Drawing.Point(339, 376)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(0, 22)
        Me.lblUsername.TabIndex = 6
        '
        'lblUsername2
        '
        Me.lblUsername2.AutoSize = True
        Me.lblUsername2.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUsername2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblUsername2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername2.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblUsername2.Location = New System.Drawing.Point(55, 553)
        Me.lblUsername2.Name = "lblUsername2"
        Me.lblUsername2.Size = New System.Drawing.Size(0, 22)
        Me.lblUsername2.TabIndex = 7
        '
        'lblClasse
        '
        Me.lblClasse.AutoSize = True
        Me.lblClasse.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblClasse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblClasse.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClasse.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblClasse.Location = New System.Drawing.Point(339, 448)
        Me.lblClasse.Name = "lblClasse"
        Me.lblClasse.Size = New System.Drawing.Size(0, 22)
        Me.lblClasse.TabIndex = 8
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ForumToolStripMenuItem, Me.MessagesPrivésToolStripMenuItem, Me.QuitterToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(755, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ForumToolStripMenuItem
        '
        Me.ForumToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.CréerUnForumToolStripMenuItem, Me.TrouverUnForumToolStripMenuItem1})
        Me.ForumToolStripMenuItem.Name = "ForumToolStripMenuItem"
        Me.ForumToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.ForumToolStripMenuItem.Text = "Forum"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(166, 6)
        '
        'CréerUnForumToolStripMenuItem
        '
        Me.CréerUnForumToolStripMenuItem.Name = "CréerUnForumToolStripMenuItem"
        Me.CréerUnForumToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.CréerUnForumToolStripMenuItem.Text = "Créer un Forum"
        '
        'TrouverUnForumToolStripMenuItem1
        '
        Me.TrouverUnForumToolStripMenuItem1.Name = "TrouverUnForumToolStripMenuItem1"
        Me.TrouverUnForumToolStripMenuItem1.Size = New System.Drawing.Size(169, 22)
        Me.TrouverUnForumToolStripMenuItem1.Text = "Trouver un Forum"
        '
        'MessagesPrivésToolStripMenuItem
        '
        Me.MessagesPrivésToolStripMenuItem.Name = "MessagesPrivésToolStripMenuItem"
        Me.MessagesPrivésToolStripMenuItem.Size = New System.Drawing.Size(104, 20)
        Me.MessagesPrivésToolStripMenuItem.Text = "Messages Privés"
        '
        'QuitterToolStripMenuItem
        '
        Me.QuitterToolStripMenuItem.Name = "QuitterToolStripMenuItem"
        Me.QuitterToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
        Me.QuitterToolStripMenuItem.Text = "Quitter"
        '
        'lblNotification
        '
        Me.lblNotification.AutoSize = True
        Me.lblNotification.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblNotification.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotification.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblNotification.Location = New System.Drawing.Point(290, 524)
        Me.lblNotification.Name = "lblNotification"
        Me.lblNotification.Size = New System.Drawing.Size(11, 16)
        Me.lblNotification.TabIndex = 10
        Me.lblNotification.Text = "-"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(755, 584)
        Me.Controls.Add(Me.lblNotification)
        Me.Controls.Add(Me.lblClasse)
        Me.Controls.Add(Me.lblUsername2)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Main"
        Me.Text = "Main"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblUsername2 As Label
    Friend WithEvents lblClasse As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ForumToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MessagesPrivésToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents CréerUnForumToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TrouverUnForumToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents lblNotification As Label
End Class
