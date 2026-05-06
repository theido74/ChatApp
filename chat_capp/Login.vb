Imports System.Drawing.Drawing2D

Public Class Login

    Private placeholderUsername As String = "Entrez votre Username"
    Private placeholderPassword As String = "Entrez votre mot de passe"

    Private Sub HandleEnter(tb As TextBox, placeholder As String, isPassword As Boolean)
        If tb.Text = placeholder Then
            tb.Text = ""
            tb.ForeColor = Color.White
            If isPassword Then tb.UseSystemPasswordChar = True
        End If
    End Sub
    Private Sub InitTextBox(tb As TextBox, placeholder As String)
        tb.BackColor = Color.Black
        tb.ForeColor = Color.Gray
        tb.BorderStyle = BorderStyle.None
        tb.Text = placeholder
    End Sub
    Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
        HandleEnter(txtUsername, placeholderUsername, False)
    End Sub

    Private Sub txtMDP_Enter(sender As Object, e As EventArgs) Handles txtMDP.Enter
        HandleEnter(txtMDP, placeholderPassword, True)

    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitTextBox(txtUsername, placeholderUsername)
        InitTextBox(txtMDP, placeholderPassword)
        txtMDP.UseSystemPasswordChar = False

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim inscriptionForm As New Inscription()
        inscriptionForm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim main = New Main()
        main.ShowDialog()
        Me.Close()
    End Sub




End Class