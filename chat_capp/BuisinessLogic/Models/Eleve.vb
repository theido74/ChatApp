Public Class Eleve
    Inherits User

    Public Property Niveau As String
    Public Property NbPoints As Integer

    Public Sub New(niveau As String, nbPoints As Integer)
        Me.Niveau = niveau
        Me.NbPoints = nbPoints
    End Sub

    Public Sub AfficherDetails()
        Console.WriteLine("Niveau: " & Niveau)
        Console.WriteLine("Nombre de points: " & NbPoints)
    End Sub

End Class

