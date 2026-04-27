Imports System.Data
Imports Oracle.ManagedDataAccess.Client

Public Class UserDateAccess
    Public Function GetEleveByUsername(username As String) As Eleve
        Dim e As New Eleve()
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT * from Eleve WHERE per_nom = :username"
                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            e.UserID = CInt(reader("per_id"))
                            e.Nom = reader("per_nom").ToString()
                            e.Prenom = reader("per_prenom").ToString()
                            e.DateDeNaissance = CDate(reader("dateDeNaissance"))
                            e.Email = reader("per_email").ToString()
                            e.MdpHashed = reader("per_motDePasse").ToString()
                            e.DateCreation = CDate(reader("dateCreation"))
                            e.IsActive = CBool(reader("isActive"))
                            e.ChatStatut = reader("chatStatus").ToString()
                            e.Niveau = reader("ele_niveau").ToString()
                            e.NbPoints = CInt(reader("ele_nbPoints"))
                            e.Classe = reader("ele_classe").ToString()
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
            MessageBox.Show("Erreur DB: " & ex.Message)
        End Try
        Return Nothing
    End Function
End Class
