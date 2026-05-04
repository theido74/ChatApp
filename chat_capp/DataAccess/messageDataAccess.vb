Imports System.Data
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Oracle.ManagedDataAccess.Client

Public Class messageDataAccess
    Public Function CreateMessage(idEnvoyeur As Integer, idReceveur As Integer, contenu As String) As Integer
        Dim newId As Integer = 0

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()
                Using tx = conn.BeginTransaction()

                    Dim sql As String = "INSERT INTO ess_message(mes_id,mes_per_id_emm,mes_per_id_rec,mes_for_id,mes_contenu,mes_timeStamp,mes_estPrive,mes_estSupprime) " &
                                    "VALUES(seq_message.NEXTVAL, :idEnvoyeur, :idReceveur, :forumId, :contenu, :timeStamp, :estPrive ,:supprime) " &
                                    "RETURNING mes_id INTO :newId"

                    Using cmd As New OracleCommand(sql, conn)
                        cmd.Transaction = tx
                        cmd.BindByName = True

                        cmd.Parameters.Add("idEnvoyeur", OracleDbType.Int32).Value = idEnvoyeur
                        cmd.Parameters.Add("idReceveur", OracleDbType.Int32).Value = idReceveur
                        cmd.Parameters.Add("forumId", OracleDbType.Int32).Value = 22
                        cmd.Parameters.Add("contenu", OracleDbType.Varchar2).Value = contenu
                        cmd.Parameters.Add("timeStamp", OracleDbType.Date).Value = DateTime.Now
                        cmd.Parameters.Add("estPrive", OracleDbType.Int16).Value = 0
                        cmd.Parameters.Add("supprime", OracleDbType.Int16).Value = 0

                        Dim prmNewId = cmd.Parameters.Add("newId", OracleDbType.Int32)
                        prmNewId.Direction = ParameterDirection.Output

                        cmd.ExecuteNonQuery()

                        newId = Convert.ToInt32(prmNewId.Value.ToString())

                        tx.Commit()

                        Return newId
                    End Using
                    tx.Rollback()
                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)

            Return -1

        End Try
    End Function
End Class
