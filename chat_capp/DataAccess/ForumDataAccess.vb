Imports Oracle.ManagedDataAccess.Client

Public Class ForumDataAccess

    Public Function GetAllForums() As List(Of Forum)
        Dim forums As New List(Of Forum)()

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection() ' Using -> à la fin la variable "conn" se ferme; GetConnection() -> retourne une connexion oracle configurée.
                conn.Open() ' Rend la connexion active.

                Dim sql As String = "SELECT for_id, for_nom, for_description, for_dateCreation, for_estActif " &
                                    "FROM ess_forum " &
                                    "WHERE for_estActif = 1 " &
                                    "ORDER BY for_dateCreation DESC"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim forum As New Forum With {
                                .ForumId = CInt(reader("for_id")),
                                .NomForum = reader("for_nom").ToString(),
                                .Description = reader("for_description").ToString(),
                                .DateCreation = CDate(reader("for_dateCreation")),
                                .EstActif = CBool(reader("for_estActif"))
                            }
                            forums.Add(forum)
                        End While
                    End Using
                End Using
            End Using
            Return forums

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function GetForumById(id As Integer) As Forum

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection() ' Using -> à la fin la variable "conn" se ferme; GetConnection() -> retourne une connexion oracle configurée.
                conn.Open() ' Rend la connexion active.

                Dim sql As String = "SELECT for_id, for_nom, for_description, for_dateCreation, for_estActif " &
                                    "FROM ess_forum " &
                                    "WHERE for_estActif = 1 AND for_id = :id"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("id", OracleDbType.Int32).Value = id
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        reader.Read()
                        Dim forum As New Forum With {
                            .ForumId = CInt(reader("for_id")),
                            .NomForum = reader("for_nom").ToString(),
                            .Description = reader("for_description").ToString(),
                            .DateCreation = CDate(reader("for_dateCreation")),
                            .EstActif = CBool(reader("for_estActif"))
                        }
                        Return forum
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function CreateForum(name As String, description As String) As Integer ' Retourne l'id.
        ' Validation
        If String.IsNullOrWhiteSpace(name) Then
            Throw New ArgumentException("Forum nom obligatoire")
        End If
        If name.Length > 100 Then
            Throw New ArgumentException("Forum nom max 100 caractères")
        End If

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                ' Vérifier l'unicité du nom (prévention doublon)
                Dim checkSql As String = "SELECT COUNT(*) FROM ess_forum WHERE for_nom = :name"
                Using checkCmd As New OracleCommand(checkSql, conn)
                    checkCmd.Parameters.Add("name", OracleDbType.Varchar2).Value = name
                    Dim count = CInt(checkCmd.ExecuteScalar()) ' Récupère le résultat de COUNT(*) (une seule valeur : le nombre total de correspondances)
                    If count > 0 Then
                        Throw New InvalidOperationException($"Forum '{name}' existe déjà")
                    End If
                End Using

                ' Insérer le nouveau forum
                Dim insertSql As String = "INSERT INTO ess_forum(for_id, for_nom, for_description, for_dateCreation, for_estActif) " &
                                         "VALUES(seq_forum.NEXTVAL, :name, :description, SYSDATE, 1) " &
                                         "RETURNING for_id INTO :newId"

                Dim newForumID As Integer

                Using insertCmd As New OracleCommand(insertSql, conn)
                    insertCmd.BindByName = True
                    insertCmd.Parameters.Add("name", OracleDbType.Varchar2).Value = name

                    Dim pDesc As New OracleParameter("description", OracleDbType.Varchar2)
                    If String.IsNullOrEmpty(description) Then
                        pDesc.Value = DBNull.Value
                    Else
                        pDesc.Value = description
                    End If
                    insertCmd.Parameters.Add(pDesc)

                    Dim pNewId As OracleParameter = New OracleParameter("newId", OracleDbType.Int32)
                    pNewId.Direction = ParameterDirection.Output
                    insertCmd.Parameters.Add(pNewId)

                    insertCmd.ExecuteNonQuery()

                    newForumID = Convert.ToInt32(pNewId.Value.ToString())
                End Using

                Return newForumID
            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return Nothing
    End Function

End Class
