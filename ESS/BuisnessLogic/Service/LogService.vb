Imports System.Timers


Public Class LogService

    Private dbAccess As New LogsDataAccess()

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Ajout un ligne dans la table ESS_LOGS grâce dbAccess.Ajoutlog
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="message"></param>
    ''' <returns>Integer Id</returns>
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

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Ajout ligne dans la table ESS_LOGS
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>Integer Id</returns>
    Public Function PingDB(id As Integer) As Integer
        Dim newID = AjoutLog(id, "PING")
        Return newID

    End Function

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Ajout ligne dans la table ESS_LOGS
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function Logout(id As Integer) As Integer
        Dim newID = AjoutLog(id, "LOGOUT")

        Return newID
    End Function

    Public Function SetUserOffline(id As Integer) As Boolean
        Return dbAccess.SetUserOffline(id)
    End Function

    Public Function SetUserOnline(id As Integer) As Boolean
        Return dbAccess.SetUserOnline(id)
    End Function


    ''' <summary>
    ''' Auteur: Arnaud
    ''' Retourne la liste des Id's des utilisateur qui ont un ping dans ESS_LOGS de moins de 30 sec.
    ''' </summary>
    ''' <returns></returns>
    Public Function isActive() As List(Of Integer)
        Dim lstActive As New List(Of Integer)
        lstActive = dbAccess.isActive()
        Return lstActive
    End Function



End Class