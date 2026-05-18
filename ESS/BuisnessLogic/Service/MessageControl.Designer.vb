<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MessageControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblEmmeteur = New System.Windows.Forms.Label()
        Me.lblContenu = New System.Windows.Forms.Label()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblEmmeteur
        '
        Me.lblEmmeteur.AutoSize = True
        Me.lblEmmeteur.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmmeteur.ForeColor = System.Drawing.Color.BlueViolet
        Me.lblEmmeteur.Location = New System.Drawing.Point(10, 10)
        Me.lblEmmeteur.Name = "lblEmmeteur"
        Me.lblEmmeteur.Size = New System.Drawing.Size(75, 16)
        Me.lblEmmeteur.TabIndex = 0
        Me.lblEmmeteur.Text = "Utilisateur"
        '
        'lblContenu
        '
        Me.lblContenu.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContenu.ForeColor = System.Drawing.Color.White
        Me.lblContenu.Location = New System.Drawing.Point(10, 35)
        Me.lblContenu.Name = "lblContenu"
        Me.lblContenu.Size = New System.Drawing.Size(410, 50)
        Me.lblContenu.TabIndex = 1
        Me.lblContenu.Text = "Contenu du message"
        Me.lblContenu.AutoSize = False
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.ForeColor = System.Drawing.Color.Gray
        Me.lblTime.Location = New System.Drawing.Point(10, 90)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(38, 14)
        Me.lblTime.TabIndex = 2
        Me.lblTime.Text = "14:30"
        '
        'MessageControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.lblContenu)
        Me.Controls.Add(Me.lblEmmeteur)
        Me.Name = "MessageControl"
        Me.Size = New System.Drawing.Size(430, 115)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblEmmeteur As Label
    Friend WithEvents lblContenu As Label
    Friend WithEvents lblTime As Label

End Class
