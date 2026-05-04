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
                            Console.WriteLine("ID" & e.UserID)
                            e.UserName = reader("per_username").ToString()
                            Console.WriteLine("ID" & e.UserName)

                            e.Nom = reader("per_nom").ToString()
                            Console.WriteLine("per_nom" & e.Nom)

                            e.Prenom = reader("per_prenom").ToString()
                            Console.WriteLine("per_prenom" & e.Prenom)

                            e.DateDeNaissance = CDate(reader("per_dateNaissance"))
                            Console.WriteLine("ID" & e.DateDeNaissance)

                            e.Email = reader("per_email").ToString()
                            Console.WriteLine("per_dateNaissance" & e.Email)

                            e.MdpHashed = reader("per_mdpHashed").ToString()
                            Console.WriteLine("per_mdpHashed" & e.MdpHashed)

                            e.DateCreation = CDate(reader("per_dateCreation"))
                            Console.WriteLine("per_dateCreation" & e.DateCreation)

                            e.IsActive = CBool(reader("per_isActive"))
                            Console.WriteLine("per_isActive" & e.IsActive)

                            e.ChatStatut = reader("per_chatStatut").ToString()
                            Console.WriteLine("per_chatStatus" & e.ChatStatut)

                            If Not IsDBNull(reader("ele_niveau")) Then
                                e.Niveau = reader("ele_niveau").ToString()
                                Console.WriteLine("ID" & e.Niveau)

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
End Class
