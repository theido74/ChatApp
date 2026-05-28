<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateForum
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateForum))
        Me.txtNomForum = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.btnCreerForum = New System.Windows.Forms.Button()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtNomForum
        '
        Me.txtNomForum.BackColor = System.Drawing.SystemColors.WindowText
        Me.txtNomForum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNomForum.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNomForum.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtNomForum.Location = New System.Drawing.Point(292, 165)
        Me.txtNomForum.Multiline = True
        Me.txtNomForum.Name = "txtNomForum"
        Me.txtNomForum.Size = New System.Drawing.Size(420, 27)
        Me.txtNomForum.TabIndex = 0
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.SystemColors.WindowText
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.BlueViolet
        Me.txtDescription.Location = New System.Drawing.Point(292, 303)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(487, 115)
        Me.txtDescription.TabIndex = 1
        '
        'btnCreerForum
        '
        Me.btnCreerForum.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCreerForum.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCreerForum.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreerForum.ForeColor = System.Drawing.Color.BlueViolet
        Me.btnCreerForum.Location = New System.Drawing.Point(663, 498)
        Me.btnCreerForum.Name = "btnCreerForum"
        Me.btnCreerForum.Size = New System.Drawing.Size(116, 23)
        Me.btnCreerForum.TabIndex = 2
        Me.btnCreerForum.Text = "Créer le forum"
        Me.btnCreerForum.UseVisualStyleBackColor = False
        '
        'btnAnnuler
        '
        Me.btnAnnuler.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAnnuler.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnnuler.ForeColor = System.Drawing.Color.BlueViolet
        Me.btnAnnuler.Location = New System.Drawing.Point(488, 498)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(116, 23)
        Me.btnAnnuler.TabIndex = 3
        Me.btnAnnuler.Text = "Annuler"
        Me.btnAnnuler.UseVisualStyleBackColor = False
        '
        'CreateForum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(893, 592)
        Me.Controls.Add(Me.btnAnnuler)
        Me.Controls.Add(Me.btnCreerForum)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.txtNomForum)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "CreateForum"
        Me.Text = "CreateForum"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtNomForum As TextBox
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents btnCreerForum As Button
    Friend WithEvents btnAnnuler As Button
End Class
