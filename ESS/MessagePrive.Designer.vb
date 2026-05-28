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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MessagePrive))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblContactName = New System.Windows.Forms.Label()
        Me.flpMessagesPrives = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnEnvoyer = New System.Windows.Forms.Button()
        Me.txtMessagePrive = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblPresentation = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Username = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblContactName
        '
        Me.lblContactName.AutoSize = True
        Me.lblContactName.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblContactName.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactName.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblContactName.Location = New System.Drawing.Point(361, 93)
        Me.lblContactName.Name = "lblContactName"
        Me.lblContactName.Size = New System.Drawing.Size(93, 16)
        Me.lblContactName.TabIndex = 0
        Me.lblContactName.Text = "ContactName"
        '
        'flpMessagesPrives
        '
        Me.flpMessagesPrives.AutoScroll = True
        Me.flpMessagesPrives.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.flpMessagesPrives.Location = New System.Drawing.Point(348, 122)
        Me.flpMessagesPrives.Name = "flpMessagesPrives"
        Me.flpMessagesPrives.Size = New System.Drawing.Size(680, 374)
        Me.flpMessagesPrives.TabIndex = 5
        '
        'btnEnvoyer
        '
        Me.btnEnvoyer.BackgroundImage = CType(resources.GetObject("btnEnvoyer.BackgroundImage"), System.Drawing.Image)
        Me.btnEnvoyer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnvoyer.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnEnvoyer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnvoyer.Location = New System.Drawing.Point(936, 524)
        Me.btnEnvoyer.Name = "btnEnvoyer"
        Me.btnEnvoyer.Size = New System.Drawing.Size(76, 29)
        Me.btnEnvoyer.TabIndex = 6
        Me.btnEnvoyer.UseVisualStyleBackColor = True
        '
        'txtMessagePrive
        '
        Me.txtMessagePrive.Location = New System.Drawing.Point(348, 529)
        Me.txtMessagePrive.Multiline = True
        Me.txtMessagePrive.Name = "txtMessagePrive"
        Me.txtMessagePrive.Size = New System.Drawing.Size(534, 20)
        Me.txtMessagePrive.TabIndex = 7
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblUsername.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblUsername.Location = New System.Drawing.Point(404, 28)
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
        Me.lblPresentation.Location = New System.Drawing.Point(84, 65)
        Me.lblPresentation.Name = "lblPresentation"
        Me.lblPresentation.Size = New System.Drawing.Size(133, 16)
        Me.lblPresentation.TabIndex = 11
        Me.lblPresentation.Text = "Choisir un contact :"
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.ColumnHeadersVisible = False
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Username, Me.Status})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DataGridView1.Location = New System.Drawing.Point(87, 84)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(228, 421)
        Me.DataGridView1.TabIndex = 14
        '
        'Username
        '
        Me.Username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Username.HeaderText = "Username"
        Me.Username.Name = "Username"
        Me.Username.ReadOnly = True
        Me.Username.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Username.Width = 21
        '
        'Status
        '
        Me.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'btnAnnuler
        '
        Me.btnAnnuler.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnnuler.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnnuler.ForeColor = System.Drawing.Color.BlueViolet
        Me.btnAnnuler.Location = New System.Drawing.Point(140, 526)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(116, 23)
        Me.btnAnnuler.TabIndex = 15
        Me.btnAnnuler.Text = "Retour"
        Me.btnAnnuler.UseVisualStyleBackColor = False
        '
        'MessagePrive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1120, 615)
        Me.Controls.Add(Me.btnAnnuler)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.lblPresentation)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtMessagePrive)
        Me.Controls.Add(Me.btnEnvoyer)
        Me.Controls.Add(Me.flpMessagesPrives)
        Me.Controls.Add(Me.lblContactName)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MessagePrive"
        Me.Text = "MessagePrivéForm"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblContactName As Label
    Friend WithEvents flpMessagesPrives As FlowLayoutPanel
    Friend WithEvents btnEnvoyer As Button
    Friend WithEvents txtMessagePrive As TextBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblPresentation As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnAnnuler As Button
    Friend WithEvents Username As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
End Class
