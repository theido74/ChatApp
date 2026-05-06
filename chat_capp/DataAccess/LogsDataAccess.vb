Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Oracle.ManagedDataAccess.Client

Public Class LogsDataAccess

    Public Function AjoutLog(id As Integer?, message As String) As Integer 'A TESTER AVEC CONSTANTE
        Dim newId As Integer = 0
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()
                Using tx = conn.BeginTransaction()

                    Dim sql As String = "INSERT INTO ESS_LOGS(log_id,log_per_id,log_action,log_timestamp,log_details)" &
                                        " VALUES(seq_logs.NEXTVAL, :id, :message, SYSDATE, 'userId: ' || :id)" &
                                        " RETURNING log_id INTO :newId"

                    Using cmd As New OracleCommand(sql, conn)
                        cmd.BindByName = True
                        cmd.CommandType = CommandType.Text
                        cmd.Parameters.Add("id", OracleDbType.Int16).Value = id
                        cmd.Parameters.Add("message", OracleDbType.Varchar2).Value = message
                        Dim prmNewId = cmd.Parameters.Add("newId", OracleDbType.Int32)
                        prmNewId.Direction = ParameterDirection.Output
                        cmd.ExecuteNonQuery()
                        newId = Convert.ToInt32(prmNewId.Value.ToString())
                        tx.Commit()
                    End Using
                End Using
            End Using

            Return newId
        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function isActive() As List(Of Integer)
        Dim IdActive As New List(Of Integer)
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()
                Using tx = conn.BeginTransaction()

                    Dim sql As String = "SELECT log_per_id, log_timeStamp, log_action " &
                    "FROM ess_logs " &
                    "WHERE log_action = :message"

                    Using cmd As New OracleCommand(sql, conn)
                        cmd.BindByName = True
                        cmd.CommandType = CommandType.Text
                        cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = "PING"
                        Using reader As OracleDataReader = cmd.ExecuteReader()

                            If reader.Read() Then
                                Dim userId = CInt(reader("log_per_id"))
                                Dim ts As Date = CDate(reader("log_timeStamp"))
                                Dim tsMinus30Sec As Date = ts.AddSeconds(-30)
                                If ts < tsMinus30Sec Then
                                    IdActive.Add(userId)
                                End If
                            End If

                        End Using

                    End Using

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return IdActive
    End Function
End Class
