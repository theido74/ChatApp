Imports Oracle.DataAccess.Client
Imports Oracle.ManagedDataAccess.Client
Public Class UserDateAccess
    Public Function GetUserByUsername(username As String) As User
        Dim user As New User()
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.open()

                Dim cmd As New OracleCommand("SELECT * from Eleve WHERE per_nom = :username", conn)
                cmd.Parameters.Add(":username", username)

                Using reader As OracleDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        user.UserID = CInt(reader("per_id"))
                        user.Nom = reader("per_nom").ToString()
                        user.Prenom = reader("per_prenom").ToString()
                        user.DateDeNaissance = CDate(reader("dateDeNaissance"))
                        user.email = reader("per_email").ToString()
                        user.MdpHashed = reader("per_motDePasse").ToString()
                        user.DateCreation = CDate(reader("dateCreation"))
                        user.IsActive = CBool(reader("isActive"))
                        user.ChatStatut = reader("chatStatus").ToString()








                    End If
                End Using

            End Using
        Catch ex As Exception

        End Try
    End Function
End Class
