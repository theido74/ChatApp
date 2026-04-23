Imports Oracle.DataAccess.Client
Imports Oracle.ManagedDataAccess.Client

Public Class DatabaseConnection
    ' ⚠️ À ADAPTER avec vos paramètres Oracle !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    Private Shared _connectionString As String =
        "Data Source=localhost:1521/xe;" &
        "User Id=YourUsername;" &
        "Password=YourPassword;"

    Public Shared Function GetConnection() As OracleConnection
        Return New OracleConnection(_connectionString)
    End Function

    ' Vérifier la connexion
    Public Shared Function TestConnection() As Boolean
        Try
            Using conn As OracleConnection = GetConnection()
                conn.Open()
                ' Si aucune exception, c'est bon
                conn.Close()
                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur de connexion: " & ex.Message)
            Return False
        End Try
    End Function
End Class