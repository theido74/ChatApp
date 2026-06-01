Imports Oracle.ManagedDataAccess.Client

Public Class ForumDataAccess

    ''' <summary>
    ''' Retourne une liste de tous les forums.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <returns></returns>
    Public Function GetAllForums() As List(Of Forums)
        Dim forums As New List(Of Forums)()

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT for_id, for_nom, for_description, for_dateCreation, for_estActif " &
                                    "FROM ess_forum " &
                                    "WHERE for_estActif = 1 " &
                                    "ORDER BY for_dateCreation DESC"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim forum As New Forums With {
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
        Return forums
    End Function

    ''' <summary>
    ''' Retourne le forum dont l'id est spécifié dans le paramètre s'il existe.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetForumById(id As Integer) As Forums

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT for_id, for_nom, for_description, for_dateCreation, for_estActif " &
                                    "FROM ess_forum " &
                                    "WHERE for_estActif = 1 AND for_id = :id"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = id
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        reader.Read()
                        Dim forum As New Forums With {
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

    ''' <summary>
    ''' Insère un forum dans la bdd après avoir vérifier que le forum à insérer respècte certaines conditions.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="name"></param>
    ''' <param name="description"></param>
    ''' <returns></returns>
    Public Function CreateForum(name As String, description As String) As Integer
        If String.IsNullOrWhiteSpace(name) Then
            Throw New ArgumentException("Forum nom obligatoire")
        End If
        If name.Length > 100 Then
            Throw New ArgumentException("Forum nom max 100 caractères")
        End If

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim checkSql As String = "SELECT COUNT(*) FROM ess_forum WHERE for_nom = :name"
                Using checkCmd As New OracleCommand(checkSql, conn)
                    checkCmd.Parameters.Add("name", OracleDbType.Varchar2).Value = name
                    Dim count = CInt(checkCmd.ExecuteScalar())
                    If count > 0 Then
                        Throw New InvalidOperationException($"Forum '{name}' existe déjà")
                    End If
                End Using

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

    ''' <summary>
    ''' Passer le paramètre estActif de forum à 0.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function DisableForumById(id As Integer) As Boolean
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "UPDATE ess_forum " &
                                    "SET for_estActif = 0 " &
                                    "WHERE for_id = :id"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("id", OracleDbType.Int32).Value = id
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    ' rowsAffected contiendra le nombre de lignes modifiées
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
