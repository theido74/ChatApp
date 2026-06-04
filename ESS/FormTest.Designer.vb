<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTest
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvDiscussion = New System.Windows.Forms.DataGridView()
        Me.ColUserId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTimeStamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNewMess = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cbbOtherUser = New System.Windows.Forms.ComboBox()
        Me.lblFilDiscussion = New System.Windows.Forms.Label()
        Me.lblAutreContact = New System.Windows.Forms.Label()
        Me.lblSelectedUser = New System.Windows.Forms.Label()
        CType(Me.dgvDiscussion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDiscussion
        '
        Me.dgvDiscussion.AllowUserToAddRows = False
        Me.dgvDiscussion.AllowUserToDeleteRows = False
        Me.dgvDiscussion.AllowUserToResizeColumns = False
        Me.dgvDiscussion.AllowUserToResizeRows = False
        Me.dgvDiscussion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvDiscussion.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColUserId, Me.ColName, Me.ColTimeStamp, Me.ColNewMess, Me.ColStatus})
        Me.dgvDiscussion.Location = New System.Drawing.Point(12, 54)
        Me.dgvDiscussion.MultiSelect = False
        Me.dgvDiscussion.Name = "dgvDiscussion"
        Me.dgvDiscussion.ReadOnly = True
        Me.dgvDiscussion.RowHeadersVisible = False
        Me.dgvDiscussion.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvDiscussion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDiscussion.Size = New System.Drawing.Size(425, 276)
        Me.dgvDiscussion.TabIndex = 1
        '
        'ColUserId
        '
        Me.ColUserId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColUserId.Frozen = True
        Me.ColUserId.HeaderText = "UserId"
        Me.ColUserId.Name = "ColUserId"
        Me.ColUserId.ReadOnly = True
        Me.ColUserId.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColUserId.Visible = False
        '
        'ColName
        '
        Me.ColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ColName.Frozen = True
        Me.ColName.HeaderText = "Nom"
        Me.ColName.Name = "ColName"
        Me.ColName.ReadOnly = True
        Me.ColName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColName.Width = 54
        '
        'ColTimeStamp
        '
        Me.ColTimeStamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ColTimeStamp.Frozen = True
        Me.ColTimeStamp.HeaderText = "Dernier mess."
        Me.ColTimeStamp.Name = "ColTimeStamp"
        Me.ColTimeStamp.ReadOnly = True
        Me.ColTimeStamp.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColTimeStamp.Width = 96
        '
        'ColNewMess
        '
        Me.ColNewMess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColNewMess.HeaderText = "Message(s)"
        Me.ColNewMess.Name = "ColNewMess"
        Me.ColNewMess.ReadOnly = True
        Me.ColNewMess.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'ColStatus
        '
        Me.ColStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ColStatus.HeaderText = "Statut"
        Me.ColStatus.Name = "ColStatus"
        Me.ColStatus.ReadOnly = True
        Me.ColStatus.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColStatus.Width = 60
        '
        'cbbOtherUser
        '
        Me.cbbOtherUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbbOtherUser.FormattingEnabled = True
        Me.cbbOtherUser.Location = New System.Drawing.Point(443, 54)
        Me.cbbOtherUser.Name = "cbbOtherUser"
        Me.cbbOtherUser.Size = New System.Drawing.Size(121, 21)
        Me.cbbOtherUser.TabIndex = 2
        '
        'lblFilDiscussion
        '
        Me.lblFilDiscussion.AutoSize = True
        Me.lblFilDiscussion.Location = New System.Drawing.Point(12, 38)
        Me.lblFilDiscussion.Name = "lblFilDiscussion"
        Me.lblFilDiscussion.Size = New System.Drawing.Size(84, 13)
        Me.lblFilDiscussion.TabIndex = 3
        Me.lblFilDiscussion.Text = "Fil de discussion"
        '
        'lblAutreContact
        '
        Me.lblAutreContact.AutoSize = True
        Me.lblAutreContact.Location = New System.Drawing.Point(440, 38)
        Me.lblAutreContact.Name = "lblAutreContact"
        Me.lblAutreContact.Size = New System.Drawing.Size(66, 13)
        Me.lblAutreContact.TabIndex = 4
        Me.lblAutreContact.Text = "Autre élèves"
        '
        'lblSelectedUser
        '
        Me.lblSelectedUser.AutoSize = True
        Me.lblSelectedUser.Location = New System.Drawing.Point(12, 9)
        Me.lblSelectedUser.Name = "lblSelectedUser"
        Me.lblSelectedUser.Size = New System.Drawing.Size(130, 13)
        Me.lblSelectedUser.TabIndex = 5
        Me.lblSelectedUser.Text = "UserId sélectionné : None"
        '
        'FormTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 342)
        Me.Controls.Add(Me.lblSelectedUser)
        Me.Controls.Add(Me.lblAutreContact)
        Me.Controls.Add(Me.lblFilDiscussion)
        Me.Controls.Add(Me.cbbOtherUser)
        Me.Controls.Add(Me.dgvDiscussion)
        Me.Name = "FormTest"
        Me.Text = "Form1"
        CType(Me.dgvDiscussion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvDiscussion As DataGridView
    Friend WithEvents lblFilDiscussion As Label
    Friend WithEvents lblAutreContact As Label
    Friend WithEvents lblSelectedUser As Label
    Friend WithEvents ColUserId As DataGridViewTextBoxColumn
    Friend WithEvents ColName As DataGridViewTextBoxColumn
    Friend WithEvents ColTimeStamp As DataGridViewTextBoxColumn
    Friend WithEvents ColNewMess As DataGridViewTextBoxColumn
    Friend WithEvents ColStatus As DataGridViewTextBoxColumn
    Protected Friend WithEvents cbbOtherUser As ComboBox
End Class
