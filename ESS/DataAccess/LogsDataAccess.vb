Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Oracle.ManagedDataAccess.Client

Public Class LogsDataAccess

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Ajoute une entrée dans la table des logs et retourne l'identifiant
    ''' généré par la base de données.
    ''' </summary>
    ''' <param name="id">Identifiant de l'utilisateur associé à l'action.</param>
    ''' <param name="message">Type ou description de l'action à enregistrer.</param>
    ''' <returns>
    ''' Identifiant du log créé ; Nothing en cas d'erreur.
    ''' </returns>
    Public Function AjoutLog(id As Integer?, message As String) As Integer
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

    ''' <summary>
    ''' Auteur: Arnaud
    ''' Récupère la liste des utilisateurs considérés comme actifs
    ''' à partir des logs de type "PING".
    ''' </summary>
    ''' <remarks>
    ''' Un utilisateur est considéré actif selon l'horodatage de son
    ''' dernier message PING enregistré dans la table des logs.
    ''' </remarks>
    ''' <returns>
    ''' Liste des identifiants des utilisateurs actifs.
    ''' </returns>
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
                        cmd.Parameters.Add("message", OracleDbType.Varchar2).Value = "PING"
                        Using reader As OracleDataReader = cmd.ExecuteReader()

                            While reader.Read()
                                Dim userId = CInt(reader("log_per_id"))
                                Dim timeNow = DateTime.UtcNow()
                                Dim ts As Date = CDate(reader("log_timeStamp"))
                                Dim tsMinus30Sec As Date = timeNow.AddSeconds(-30)
                                If ts >= tsMinus30Sec Then
                                    IdActive.Add(userId)
                                End If
                            End While
                        End Using
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return IdActive
    End Function

    Public Function SetUserOffline(userId As Integer) As Boolean
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "UPDATE ess_personne " &
                                    "SET per_chatstatut = :statut " &
                                    "WHERE per_id = :userId"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("userId", OracleDbType.Int32).Value = userId
                    cmd.Parameters.Add("statut", OracleDbType.Varchar2).Value = "Hors ligne"

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    Return rowsAffected > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
            Return False
        End Try
    End Function
End Class
