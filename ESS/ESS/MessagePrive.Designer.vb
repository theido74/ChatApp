<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessagePrive
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MessagePrive))
        Me.lblContactName = New System.Windows.Forms.Label()
        Me.dgvConversationsRecentes = New System.Windows.Forms.DataGridView()
        Me.flpMessagesPrives = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnEnvoyer = New System.Windows.Forms.Button()
        Me.txtMessagePrive = New System.Windows.Forms.TextBox()
        Me.lstUtilisateurs = New System.Windows.Forms.ListBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblPresentation = New System.Windows.Forms.Label()
        CType(Me.dgvConversationsRecentes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblContactName
        '
        Me.lblContactName.AutoSize = True
        Me.lblContactName.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblContactName.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactName.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblContactName.Location = New System.Drawing.Point(276, 85)
        Me.lblContactName.Name = "lblContactName"
        Me.lblContactName.Size = New System.Drawing.Size(93, 16)
        Me.lblContactName.TabIndex = 0
        Me.lblContactName.Text = "ContactName"
        '
        'dgvConversationsRecentes
        '
        Me.dgvConversationsRecentes.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgvConversationsRecentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.MenuText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvConversationsRecentes.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvConversationsRecentes.Location = New System.Drawing.Point(63, 99)
        Me.dgvConversationsRecentes.Name = "dgvConversationsRecentes"
        Me.dgvConversationsRecentes.Size = New System.Drawing.Size(186, 389)
        Me.dgvConversationsRecentes.TabIndex = 1
        '
        'flpMessagesPrives
        '
        Me.flpMessagesPrives.AutoScroll = True
        Me.flpMessagesPrives.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.flpMessagesPrives.Location = New System.Drawing.Point(279, 104)
        Me.flpMessagesPrives.Name = "flpMessagesPrives"
        Me.flpMessagesPrives.Size = New System.Drawing.Size(536, 335)
        Me.flpMessagesPrives.TabIndex = 5
        '
        'btnEnvoyer
        '
        Me.btnEnvoyer.BackgroundImage = CType(resources.GetObject("btnEnvoyer.BackgroundImage"), System.Drawing.Image)
        Me.btnEnvoyer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnvoyer.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnEnvoyer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnvoyer.Location = New System.Drawing.Point(739, 463)
        Me.btnEnvoyer.Name = "btnEnvoyer"
        Me.btnEnvoyer.Size = New System.Drawing.Size(76, 29)
        Me.btnEnvoyer.TabIndex = 6
        Me.btnEnvoyer.UseVisualStyleBackColor = True
        '
        'txtMessagePrive
        '
        Me.txtMessagePrive.Location = New System.Drawing.Point(279, 468)
        Me.txtMessagePrive.Multiline = True
        Me.txtMessagePrive.Name = "txtMessagePrive"
        Me.txtMessagePrive.Size = New System.Drawing.Size(383, 20)
        Me.txtMessagePrive.TabIndex = 7
        '
        'lstUtilisateurs
        '
        Me.lstUtilisateurs.FormattingEnabled = True
        Me.lstUtilisateurs.Location = New System.Drawing.Point(63, 63)
        Me.lstUtilisateurs.Name = "lstUtilisateurs"
        Me.lstUtilisateurs.Size = New System.Drawing.Size(186, 30)
        Me.lstUtilisateurs.TabIndex = 8
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblUsername.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblUsername.Location = New System.Drawing.Point(326, 19)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(0, 22)
        Me.lblUsername.TabIndex = 9
        '
        'lblPresentation
        '
        Me.lblPresentation.AutoSize = True
        Me.lblPresentation.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPresentation.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPresentation.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblPresentation.Location = New System.Drawing.Point(60, 34)
        Me.lblPresentation.Name = "lblPresentation"
        Me.lblPresentation.Size = New System.Drawing.Size(133, 16)
        Me.lblPresentation.TabIndex = 11
        Me.lblPresentation.Text = "Choisir un contact :"
        '
        'MessagePrive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(890, 546)
        Me.Controls.Add(Me.lblPresentation)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.lstUtilisateurs)
        Me.Controls.Add(Me.txtMessagePrive)
        Me.Controls.Add(Me.btnEnvoyer)
        Me.Controls.Add(Me.flpMessagesPrives)
        Me.Controls.Add(Me.dgvConversationsRecentes)
        Me.Controls.Add(Me.lblContactName)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "MessagePrive"
        Me.Text = "MessagePrivéForm"
        CType(Me.dgvConversationsRecentes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblContactName As Label
    Friend WithEvents dgvConversationsRecentes As DataGridView
    Friend WithEvents flpMessagesPrives As FlowLayoutPanel
    Friend WithEvents btnEnvoyer As Button
    Friend WithEvents txtMessagePrive As TextBox
    Friend WithEvents lstUtilisateurs As ListBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblPresentation As Label
End Class
