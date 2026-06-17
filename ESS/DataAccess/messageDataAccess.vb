Imports Oracle.ManagedDataAccess.Client

Public Class messageDataAccess

    ''' <summary>
    ''' Permet d'insérer des messages dans la base de donnée.
    ''' </summary>
    ''' <auteur> Arnaud </auteur>
    ''' <param name="idEnvoyeur"></param>
    ''' <param name="idReceveur"></param>
    ''' <param name="contenu"></param>
    ''' <param name="forum"></param>
    ''' <returns></returns>
    Public Function CreateMessage(idEnvoyeur As Integer, idReceveur As Integer, contenu As String, Optional forum As Integer? = Nothing) As Integer
        Dim newId As Integer = 0

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()
                Using tx = conn.BeginTransaction()

                    Dim sql As String = "INSERT INTO ess_message(mes_id,mes_per_id_emm,mes_per_id_rec,mes_for_id,mes_contenu,mes_timeStamp,mes_estLu,mes_estPrive,mes_estSupprime) " &
                                        "VALUES(seq_message.NEXTVAL, :idEnvoyeur, :idReceveur, :forumId, :contenu, :timeStamp, :estLu, :estPrive ,:supprime) " &
                                        "RETURNING mes_id INTO :newId"

                    Using cmd As New OracleCommand(sql, conn)
                        cmd.Transaction = tx
                        cmd.BindByName = True
                        Dim isPrivate = 1

                        cmd.Parameters.Add("idEnvoyeur", OracleDbType.Int32).Value = idEnvoyeur

                        ' Pour les messages de forum, idReceveur est NULL
                        If forum.HasValue Then
                            cmd.Parameters.Add("idReceveur", OracleDbType.Int32).Value = DBNull.Value
                        Else
                            cmd.Parameters.Add("idReceveur", OracleDbType.Int32).Value = idReceveur
                        End If

                        If forum.HasValue Then
                            cmd.Parameters.Add("forumId", OracleDbType.Int32).Value = forum.Value
                            isPrivate = 0
                        Else
                            cmd.Parameters.Add("forumId", OracleDbType.Int32).Value = DBNull.Value
                        End If

                        cmd.Parameters.Add("contenu", OracleDbType.Varchar2).Value = contenu
                        cmd.Parameters.Add("timeStamp", OracleDbType.Date).Value = DateTime.Now
                        cmd.Parameters.Add("estLu", OracleDbType.Int16).Value = 0
                        cmd.Parameters.Add("estPrive", OracleDbType.Int16).Value = isPrivate
                        cmd.Parameters.Add("supprime", OracleDbType.Int16).Value = 0

                        Dim prmNewId = cmd.Parameters.Add("newId", OracleDbType.Int32)
                        prmNewId.Direction = ParameterDirection.Output

                        cmd.ExecuteNonQuery()

                        newId = Convert.ToInt32(prmNewId.Value.ToString())

                        tx.Commit()

                        Return newId
                    End Using
                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)

            Return -1

        End Try
    End Function

    ''' <summary>
    ''' Permet de récupérer les messges d'un chat.
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="userId"></param>
    ''' <param name="contactId"></param>
    ''' <returns></returns>
    Public Function GetMessageByContactId(contactId As Integer, userId As Integer) As List(Of Message)
        Dim messages As New List(Of Message)()

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT mes_id, mes_per_id_emm, mes_per_id_rec, mes_for_id, mes_contenu, mes_timestamp, mes_estlu, mes_estprive, mes_estsupprime " &
                                    "FROM ess_message " &
                                    "WHERE mes_estsupprime = 0 AND ((mes_per_id_rec = :userId AND mes_per_id_emm = :contactId) OR (mes_per_id_rec = :contactId AND mes_per_id_emm = :userId))" &
                                    "ORDER BY mes_timestamp ASC"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("userId", OracleDbType.Int32).Value = userId
                    cmd.Parameters.Add("contactId", OracleDbType.Int32).Value = contactId
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim forumId As Integer = -1
                            If Not IsDBNull(reader("mes_for_id")) Then
                                forumId = CInt(reader("mes_for_id"))
                            End If
                            Dim ReceveurId As Integer = -1
                            If Not IsDBNull(reader("mes_per_id_rec")) Then
                                ReceveurId = CInt(reader("mes_per_id_rec"))
                            End If

                            Dim message As New Message With {
                                .MessageId = CInt(reader("mes_id")),
                                .EmmeteurId = CInt(reader("mes_per_id_emm")),
                                .ReceveurId = ReceveurId,
                                .ForumId = forumId,
                                .Contenu = reader("mes_contenu").ToString(),
                                .TimeStamp = CDate(reader("mes_timestamp")),
                                .EstLu = CBool(reader("mes_estlu")),
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
        Return messages
    End Function


    ''' <summary>
    ''' Permet de récupérer les message à partir de l'id du forum
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="forumIdPar"></param>
    ''' <returns></returns>
    Public Function GetMessageByForumId(forumIdPar As Integer) As List(Of Message)
        Dim messages As New List(Of Message)()

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT mes_id, mes_per_id_emm, mes_per_id_rec, mes_for_id, mes_contenu, mes_timestamp, mes_estlu, mes_estprive, mes_estsupprime " &
                                    "FROM ess_message " &
                                    "WHERE mes_estsupprime = 0 AND mes_for_id = :forum " &
                                    "ORDER BY mes_timestamp ASC"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.Parameters.Add("forum", OracleDbType.Int32).Value = forumIdPar
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim forumId As Integer = -1
                            If Not IsDBNull(reader("mes_for_id")) Then
                                forumId = CInt(reader("mes_for_id"))
                            End If
                            Dim ReceveurId As Integer = -1
                            If Not IsDBNull(reader("mes_per_id_rec")) Then
                                ReceveurId = CInt(reader("mes_per_id_rec"))
                            End If

                            Dim message As New Message With {
                                .MessageId = CInt(reader("mes_id")),
                                .EmmeteurId = CInt(reader("mes_per_id_emm")),
                                .ReceveurId = ReceveurId,
                                .ForumId = forumId,
                                .Contenu = reader("mes_contenu").ToString(),
                                .TimeStamp = CDate(reader("mes_timestamp")),
                                .EstLu = CBool(reader("mes_estlu")),
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
        Return messages
    End Function


    ''' <summary>
    ''' Permet la supression logique d'un message (set mes_estSupprime à 1) à partir de l'id de ce dernier
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="senderId"></param>
    ''' <param name="receiverId"></param>
    ''' <returns></returns>
    Public Function DeleteMessageById(senderId As Integer, receiverId As Integer) As Boolean
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "UPDATE ess_message " &
                                    "SET mes_estsupprime = 1 " &
                                    "WHERE (mes_per_id_emm = :sender AND mes_per_id_rec = :receiver) OR " &
                                    "(mes_per_id_emm = :receiver AND mes_per_id_rec = :sender)"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("sender", OracleDbType.Int32).Value = senderId
                    cmd.Parameters.Add("receiver", OracleDbType.Int32).Value = receiverId
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    Return rowsAffected > 0
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
            Return False
        End Try
    End Function


    ''' <summary>
    ''' Permet de compter le nombre de message non lu d'un contact à partir de l'id du destinataire
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="receiverId"></param>
    ''' <returns></returns>
    Public Function CountUnreadMessagesByReceiverId(receiverId As Integer) As Integer
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT COUNT(*) " &
                                    "FROM ess_message " &
                                    "WHERE mes_estsupprime = 0 AND mes_estlu = 0 AND mes_per_id_rec = :receiver "

                Using checkCmd As New OracleCommand(sql, conn)
                    checkCmd.Parameters.Add("receiver", OracleDbType.Int32).Value = receiverId
                    Dim count = CInt(checkCmd.ExecuteScalar())
                    Return count
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try
        Return 0
    End Function


    ''' <summary>
    ''' Permet d'initier des classe Chat pour l'affichage d'un fil de conversation
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="receiverId"></param>
    ''' <returns></returns>
    Public Function GetChatByIdBis(receiverId As Integer) As List(Of Chat)
        Dim chatLst As New List(Of Chat)

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT ess_message.mes_per_id_emm, " &
                                    "ess_personne.per_nom, " &
                                    "MAX(ess_message.mes_timestamp) AS mes_timestamp, " &
                                    "SUM(CASE WHEN ess_message.mes_estlu = 0 THEN 1 ELSE 0 END) AS unread_count, " &
                                    "ess_personne.per_chatstatut " &
                                    "FROM ess_message " &
                                    "JOIN ess_personne ON ess_personne.per_id = ess_message.mes_per_id_emm " &
                                    "WHERE ess_message.mes_estsupprime = 0 " &
                                    "AND ess_message.mes_per_id_rec = :receiver " &
                                    "GROUP BY ess_message.mes_per_id_emm, " &
                                    "ess_message.mes_per_id_rec, " &
                                    "ess_personne.per_nom, " &
                                    "ess_personne.per_chatstatut " &
                                    "ORDER BY MAX(ess_message.mes_timestamp) DESC"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("receiver", OracleDbType.Int32).Value = receiverId

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim cha As New Chat With {
                            .UserId = receiverId,
                            .ContactId = CInt(reader("mes_per_id_emm")),
                            .ContactNom = reader("per_nom").ToString(),
                            .DateDernierMessage = CDate(reader("mes_timestamp")),
                            .NbOfUnreadMessages = CInt(reader("unread_count")),
                            .Statut = reader("per_chatstatut").ToString()
                            }
                            chatLst.Add(cha)
                        End While
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
        End Try

        Return chatLst
    End Function


    ''' <summary>
    ''' Permet de passer l'attribu "estLu" d'un message à 1
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="senderId"></param>
    ''' <param name="reveiverId"></param>
    ''' <returns></returns>
    Public Function MarkAsReadByChat(senderId As Integer, reveiverId As Integer) As Boolean
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "UPDATE ess_message " &
                                    "SET mes_estlu = 1 " &
                                    "WHERE mes_per_id_emm = :sender AND mes_per_id_rec = :receiver AND mes_estlu = 0"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("sender", OracleDbType.Int32).Value = senderId
                    cmd.Parameters.Add("receiver", OracleDbType.Int32).Value = reveiverId
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    Return rowsAffected > 0
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
            Return False
        End Try
    End Function


    ''' <summary>
    ''' Permet de savoir si un chat possède des messages non lu
    ''' </summary>
    ''' <auteur> Damien </auteur>
    ''' <param name="senderId"></param>
    ''' <param name="reveiverId"></param>
    ''' <returns></returns>
    Public Function ChatHasUnreadMessages(senderId As Integer, reveiverId As Integer) As Boolean
        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT COUNT(*) " &
                                    "FROM ess_message " &
                                    "WHERE mes_per_id_emm = :sender AND mes_per_id_rec = :receiver AND mes_estlu = 0"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("sender", OracleDbType.Int32).Value = senderId
                    cmd.Parameters.Add("receiver", OracleDbType.Int32).Value = reveiverId
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Dim rowsAffected As Integer = CInt(cmd.ExecuteScalar())
                    Return rowsAffected > 0
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Erreur BD: " & ex.Message)
            Return False
        End Try
    End Function


    ''' <summary>
    ''' Auteur : Ayman
    ''' Permet de récupérer le dernier message de chaque conversation privée d'un utilisateur 
    ''' à partir de son id.
    ''' </summary>
    ''' <param name="currentUserId"></param>
    ''' <returns>La liste des derniers messages de chaque conversation privée de l'utilisateur.</returns>
    Public Function GetRecentConversations(currentUserId As Integer) As List(Of Message)
        Dim messages As New List(Of Message)()

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                ' Récupérer le dernier message de chaque conversation
                Dim sql As String = "SELECT DISTINCT " &
                    "FIRST_VALUE(mes_id) OVER (PARTITION BY CASE WHEN mes_per_id_emm = :userId THEN mes_per_id_rec ELSE mes_per_id_emm END ORDER BY mes_timestamp DESC) as mes_id, " &
                    "FIRST_VALUE(mes_per_id_emm) OVER (PARTITION BY CASE WHEN mes_per_id_emm = :userId THEN mes_per_id_rec ELSE mes_per_id_emm END ORDER BY mes_timestamp DESC) as mes_per_id_emm, " &
                    "FIRST_VALUE(mes_per_id_rec) OVER (PARTITION BY CASE WHEN mes_per_id_emm = :userId THEN mes_per_id_rec ELSE mes_per_id_emm END ORDER BY mes_timestamp DESC) as mes_per_id_rec, " &
                    "FIRST_VALUE(mes_contenu) OVER (PARTITION BY CASE WHEN mes_per_id_emm = :userId THEN mes_per_id_rec ELSE mes_per_id_emm END ORDER BY mes_timestamp DESC) as mes_contenu, " &
                    "FIRST_VALUE(mes_timestamp) OVER (PARTITION BY CASE WHEN mes_per_id_emm = :userId THEN mes_per_id_rec ELSE mes_per_id_emm END ORDER BY mes_timestamp DESC) as mes_timestamp " &
                    "FROM ess_message " &
                    "WHERE mes_estSupprime = 0 AND mes_estPrive = 1 AND (mes_per_id_emm = :userId OR mes_per_id_rec = :userId) " &
                    "ORDER BY mes_timestamp DESC"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("userId", OracleDbType.Int32).Value = currentUserId
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim message As New Message With {
                                .MessageId = CInt(reader("mes_id")),
                                .EmmeteurId = CInt(reader("mes_per_id_emm")),
                                .ReceveurId = CInt(reader("mes_per_id_rec")),
                                .Contenu = reader("mes_contenu").ToString(),
                                .TimeStamp = CDate(reader("mes_timestamp")),
                                .EstPrive = True
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

    ''' <summary>
    ''' Auteur : Ayman
    ''' Permet de récupérer tous les messages d'une conversation privée entre deux utilisateurs 
    ''' à partir de leurs id.
    ''' </summary>
    ''' <param name="currentUserId">L'ID de l'utilisateur actuel.</param>
    ''' <param name="otherUserId">L'ID de l'autre utilisateur.</param>
    ''' <returns>La liste des messages de la conversation privée entre les deux utilisateurs.</returns>
    Public Function GetPrivateConversation(currentUserId As Integer, otherUserId As Integer) As List(Of Message)
        Dim messages As New List(Of Message)()

        Try
            Using conn As OracleConnection = DatabaseConnection.GetConnection()
                conn.Open()

                Dim sql As String = "SELECT mes_id, mes_per_id_emm, mes_per_id_rec, mes_contenu, mes_timestamp, mes_estlu, mes_estprive, mes_estsupprime " &
                                    "FROM ess_message " &
                                    "WHERE mes_estsupprime = 0 AND mes_estPrive = 1 AND " &
                                    "((mes_per_id_emm = :currentUserId AND mes_per_id_rec = :otherUserId) OR " &
                                    "(mes_per_id_emm = :otherUserId AND mes_per_id_rec = :currentUserId)) " &
                                    "ORDER BY mes_timestamp ASC"

                Using cmd As New OracleCommand(sql, conn)
                    cmd.BindByName = True
                    cmd.Parameters.Add("currentUserId", OracleDbType.Int32).Value = currentUserId
                    cmd.Parameters.Add("otherUserId", OracleDbType.Int32).Value = otherUserId
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30

                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim message As New Message With {
                                .MessageId = CInt(reader("mes_id")),
                                .EmmeteurId = CInt(reader("mes_per_id_emm")),
                                .ReceveurId = CInt(reader("mes_per_id_rec")),
                                .Contenu = reader("mes_contenu").ToString(),
                                .TimeStamp = CDate(reader("mes_timestamp")),
                                .EstLu = CBool(reader("mes_estlu")),
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