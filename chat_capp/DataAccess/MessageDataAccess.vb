'Imports System.Data
'Imports System.Windows
'Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Oracle.ManagedDataAccess.Client

Public Class messageDataAccess

    Public Function CreateMessage(idEnvoyeur As Integer, idReceveur As Integer, contenu As String, Optional forum As Integer? = Nothing) As Integer
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

                        If forum.HasValue Then
                            cmd.Parameters.Add("forumId", OracleDbType.Int32).Value = forum.Value
                        Else
                            cmd.Parameters.Add("forumId", OracleDbType.Int32).Value = DBNull.Value
                        End If

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

    Public Function GetMessageByRecipientId(id As Integer) As List(Of Message)
        Dim messages As New List(Of Message)()

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection() ' Using -> à la fin la variable "conn" se ferme; GetConnection() -> retourne une connexion oracle configurée.
                conn.Open() ' Rend la connexion active.

                Dim sql As String = "SELECT mes_id, mes_per_id_emm, mes_per_id_rec, mes_for_id, mes_contenu, mes_timestamp, mes_estprive, mes_estsupprime " &
                    "FROM ess_message " &
                    "WHERE mes_estsupprime = 0 AND mes_per_id_rec = :id " &
                    "ORDER BY mes_for_id ASC, mes_per_id_emm ASC, mes_timestamp DESC"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("id", OracleDbType.Int32).Value = id
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim forumId As Integer = -1
                            If Not IsDBNull(reader("mes_for_id")) Then
                                forumId = CInt(reader("mes_for_id"))
                            End If

                            Dim message As New Message With {
                                .MessageId = CInt(reader("mes_id")),
                                .EmmeteurId = CInt(reader("mes_per_id_emm")),
                                .ReceveurId = CInt(reader("mes_per_id_rec")),
                                .ForumId = forumId,
                                .Contenu = reader("mes_contenu").ToString(),
                                .TimeStamp = CDate(reader("mes_timestamp")),
                                .EstPrive = CBool(reader("mes_estprive")),
                                .EstSupprime = CBool(reader("mes_estsupprime"))
                            }
                            messages.Add(message)
                        End While
                    End Using
                End Using
            End Using
            Return messages

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return Nothing
    End Function
End Class
