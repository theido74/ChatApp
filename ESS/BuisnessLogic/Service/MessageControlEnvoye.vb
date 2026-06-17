Public Class MessageControlEnvoye
    Inherits UserControl


    Public Property EmmeteurNom As String
    Public Property Contenu As String
    Public Property TimeStamp As DateTime
    ''' <summary>
    ''' Auteur : Ayman
    ''' Cette méthode est appelée lorsque le contrôle est chargé. 
    ''' Elle initialise les labels du contrôle avec les valeurs des propriétés EmmeteurNom,
    ''' Contenu et TimeStamp.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MessageControlEnvoye_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblEmmeteur.Text = EmmeteurNom
        lblContenu.Text = Contenu
        lblTime.Text = TimeStamp.ToString("HH:mm")
    End Sub
End Class
