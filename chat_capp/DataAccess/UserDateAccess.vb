Imports System.Data
Imports Oracle.ManagedDataAccess.Client

Public Class UserDateAccess

    Public Function GetEleveByUsername(username As String) As Eleve
        Dim e As New Eleve()
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT p.per_id, p.per_username, p.per_nom, p.per_prenom, " & _ 'PUREMENT DE LA FRIME!!!!
                        "p.per_dateNaissance, p.per_email, p.per_mdpHashed, " &
                        "p.per_dateCreation, p.per_isActive, p.per_chatStatut, " &
                        "e.ele_niveau, e.ele_nbPoints, e.ele_classe " &
                        "FROM ess_personne p " &
                        "JOIN ess_eleve e ON p.per_id = e.ele_per_id " &
                        "WHERE p.per_username = :username"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username

                    Using reader As OracleDataReader = cmd.ExecuteReader()

                        If reader.Read() Then
                            e.UserID = CInt(reader("per_id"))
                            e.UserName = reader("per_username").ToString()

                            e.Nom = reader("per_nom").ToString()

                            e.Prenom = reader("per_prenom").ToString()

                            e.DateDeNaissance = CDate(reader("per_dateNaissance"))

                            e.Email = reader("per_email").ToString()

                            e.MdpHashed = reader("per_mdpHashed").ToString()

                            e.DateCreation = CDate(reader("per_dateCreation"))

                            e.IsActive = CBool(reader("per_isActive"))

                            e.ChatStatut = reader("per_chatStatut").ToString()

                            If Not IsDBNull(reader("ele_niveau")) Then
                                e.Niveau = CInt(reader("ele_niveau"))

                            End If
                            If Not IsDBNull(reader("ele_nbPoints")) Then
                                e.NbPoints = CInt(reader("ele_nbPoints"))
                            End If
                            If Not IsDBNull(reader("ele_classe")) Then
                                e.Classe = reader("ele_classe").ToString()   'VALEUR QUI VIENNENT D'UNE AUTRE TABLE, A TESTER SI NULL AVANT; EVITE ERREUR NO SUCH TABLE
                            End If

                            Return e
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function GetEleveByID(userID As Integer) As Eleve
        Dim e As New Eleve()
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT * FROM ESS_PERSONNE where per_id = :userID"
                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("userID", OracleDbType.Int32).Value = userID

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            e.UserID = userID
                            e.Nom = reader("per_nom").ToString()
                            e.Prenom = reader("per_prenom").ToString()
                        End If
                        Return e
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur DB")
        End Try
        Return Nothing
    End Function

    Public Function CreateEleve(username As String, nom As String, prenom As String, dateDeNaissance As DateTime, email As String, mdp As String, niveau As Integer, nbPoint As Integer, classe As String) As Integer
        Dim newId As Integer = 0

        Dim sqlPerson As String = "INSERT INTO ess_personne (per_id, per_username, per_nom, per_prenom, per_dateNaissance, per_email, per_mdpHashed, per_dateCreation, per_isActive, per_chatStatut) " &
                              "VALUES (seq_personne.NEXTVAL, :username, :nom, :prenom, :dateDeNaissance, :email, :mdpHashed, :dateCreation, :isActive, :chatStatut) " &
                              "RETURNING per_id INTO :newId"

        Dim sqlEleve As String = "INSERT INTO ess_eleve (ele_per_id, ele_niveau, ele_nbPoints, ele_classe) " &
                             "VALUES (:per_id, :niveau, :nbPoints, :classe)"


        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()
                Using tx = conn.BeginTransaction()
                    Try
                        ' Insert dans ess_personne et récupérer le nouvel ID
                        Using cmd As New OracleCommand(sqlPerson, conn)
                            cmd.Transaction = tx
                            cmd.BindByName = True


                            cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username
                            cmd.Parameters.Add("nom", OracleDbType.Varchar2).Value = nom
                            cmd.Parameters.Add("prenom", OracleDbType.Varchar2).Value = prenom
                            cmd.Parameters.Add("dateDeNaissance", OracleDbType.Date).Value = dateDeNaissance
                            cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = email
                            cmd.Parameters.Add("mdpHashed", OracleDbType.Varchar2).Value = mdp
                            cmd.Parameters.Add("dateCreation", OracleDbType.Date).Value = DateTime.Now
                            cmd.Parameters.Add("isActive", OracleDbType.Int16).Value = 0
                            cmd.Parameters.Add("chatStatut", OracleDbType.Varchar2).Value = String.Empty

                            Dim prmNewId = cmd.Parameters.Add("newId", OracleDbType.Int32)
                            prmNewId.Direction = ParameterDirection.Output

                            cmd.ExecuteNonQuery()

                            newId = Convert.ToInt32(prmNewId.Value.ToString())
                        End Using

                        ' Insert dans ess_eleve lié à per_id
                        Using cmd2 As New OracleCommand(sqlEleve, conn)
                            cmd2.Transaction = tx
                            cmd2.BindByName = True

                            cmd2.Parameters.Add("per_id", OracleDbType.Int32).Value = newId
                            cmd2.Parameters.Add("niveau", OracleDbType.Int16).Value = niveau
                            cmd2.Parameters.Add("nbPoints", OracleDbType.Int32).Value = nbPoint
                            cmd2.Parameters.Add("classe", OracleDbType.Varchar2).Value = classe

                            cmd2.ExecuteNonQuery()
                        End Using

                        tx.Commit()
                        Return newId

                    Catch oex As OracleException
                        Try
                            tx.Rollback()
                        Catch
                        End Try
                        System.Diagnostics.Debug.WriteLine($"[ORA] Number={oex.Number} Message={oex.Message}")
                        System.Diagnostics.Debug.WriteLine(oex.ToString())
                        Throw
                    Catch ex As Exception
                        Try
                            tx.Rollback()
                        Catch
                        End Try
                        System.Diagnostics.Debug.WriteLine(ex.ToString())
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ' Remonter l'exception pour que l'appelant gère/logge
            Throw
        End Try
    End Function
End Class
