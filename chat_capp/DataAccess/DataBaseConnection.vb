Imports Oracle.ManagedDataAccess.Client
Imports System.Configuration

Public Class DatabaseConnection
    Private Shared ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("OracleConnectionString")?.ConnectionString

    Public Shared Function GetConnection() As OracleConnection
        If String.IsNullOrEmpty(_connectionString) Then
            Throw New InvalidOperationException("Chaîne de connexion 'OracleConnectionString' introuvable dans App.config.")
        End If
        Return New OracleConnection(_connectionString)
    End Function

    Public Shared Function TestConnection() As Boolean
        Try
            Using conn As OracleConnection = GetConnection()
                conn.Open()
                conn.Close()
                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur de connexion: " & ex.Message)
            Return False
        End Try
    End Function
End Class