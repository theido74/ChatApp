Imports System.Timers


Public Class LogService

    Private dbAccess As New LogsDataAccess()
    Public Function AjoutLog(id As Integer, message As String) As Integer
        If id < 0 Then
            Return Nothing
        End If

        Try
            Return dbAccess.AjoutLog(id, message)
        Catch ex As Exception
            MessageBox.Show("Erreur Fonction Creation Log")
            Return Nothing
        End Try
    End Function

    Public Function PingDB(id As Integer, message As String) As Integer
        AjoutLog(id, message)
        Console.WriteLine("PING DB Exécuté à " & DateTime.Now)
    End Function


    Public Function isActive() As List(Of Integer)
        Dim lstActive As New List(Of Integer)
        lstActive = dbAccess.isActive()
        Return lstActive
    End Function
    'Private WithEvents timer As New Timers.Timer(30000) ' 30 000 ms = 30 sec

End Class
