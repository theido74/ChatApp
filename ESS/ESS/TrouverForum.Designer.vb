<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TrouverForum
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TrouverForum))
        Me.btnCreerForum = New System.Windows.Forms.Button()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        Me.txtForum = New System.Windows.Forms.TextBox()
        Me.dgvForums = New System.Windows.Forms.DataGridView()
        CType(Me.dgvForums, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCreerForum
        '
        Me.btnCreerForum.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCreerForum.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCreerForum.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreerForum.ForeColor = System.Drawing.Color.BlueViolet
        Me.btnCreerForum.Location = New System.Drawing.Point(112, 306)
        Me.btnCreerForum.Name = "btnCreerForum"
        Me.btnCreerForum.Size = New System.Drawing.Size(111, 22)
        Me.btnCreerForum.TabIndex = 3
        Me.btnCreerForum.Text = "Créer le forum"
        Me.btnCreerForum.UseVisualStyleBackColor = False
        '
        'btnAnnuler
        '
        Me.btnAnnuler.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnnuler.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnnuler.ForeColor = System.Drawing.Color.BlueViolet
        Me.btnAnnuler.Location = New System.Drawing.Point(89, 521)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(116, 23)
        Me.btnAnnuler.TabIndex = 4
        Me.btnAnnuler.Text = "Annuler"
        Me.btnAnnuler.UseVisualStyleBackColor = False
        '
        'txtForum
        '
        Me.txtForum.BackColor = System.Drawing.SystemColors.MenuText
        Me.txtForum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtForum.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForum.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtForum.Location = New System.Drawing.Point(299, 97)
        Me.txtForum.Name = "txtForum"
        Me.txtForum.Size = New System.Drawing.Size(374, 14)
        Me.txtForum.TabIndex = 5
        Me.txtForum.Text = "Rechercher un forum"
        '
        'dgvForums
        '
        Me.dgvForums.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgvForums.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.MenuText
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.BlueViolet
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvForums.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvForums.Location = New System.Drawing.Point(262, 138)
        Me.dgvForums.Name = "dgvForums"
        Me.dgvForums.Size = New System.Drawing.Size(562, 394)
        Me.dgvForums.TabIndex = 6
        '
        'TrouverForum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(901, 581)
        Me.Controls.Add(Me.dgvForums)
        Me.Controls.Add(Me.txtForum)
        Me.Controls.Add(Me.btnAnnuler)
        Me.Controls.Add(Me.btnCreerForum)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "TrouverForum"
        Me.Text = "TrouverForum"
        CType(Me.dgvForums, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCreerForum As Button
    Friend WithEvents btnAnnuler As Button
    Friend WithEvents txtForum As TextBox
    Friend WithEvents dgvForums As DataGridView
End Class
