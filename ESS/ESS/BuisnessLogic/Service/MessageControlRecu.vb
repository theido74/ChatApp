Public Class MessageControlRecu
    Inherits UserControl

    Public Property EmmeteurNom As String
    Public Property Contenu As String
    Public Property TimeStamp As DateTime

    Private Sub MessageControlRecu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblEmmeteur.Text = EmmeteurNom
        lblContenu.Text = Contenu
        lblTime.Text = TimeStamp.ToString("HH:mm")
    End Sub
End Class
