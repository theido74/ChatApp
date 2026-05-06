Public Class MessageService

    Private dbAccess As New messageDataAccess()
    Private logger As New LogService()
    Private Const MESSAGE As String = "Message Envoyé"

    Public Function CreateMessage(idEnvoyeur As Integer, idReceveur As Integer, contenu As String) As Integer
        If idReceveur < 0 Then
            Return Nothing
        End If

        Try
            logger.AjoutLog(idEnvoyeur, MESSAGE)
            Return dbAccess.CreateMessage(idEnvoyeur, idReceveur, MESSAGE)
        Catch ex As Exception
            MessageBox.Show("Erreur Fonction Creation Log")
            Return Nothing
        End Try
    End Function
End Class

