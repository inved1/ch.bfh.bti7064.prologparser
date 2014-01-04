Public Class Analyser


    Dim myTXTin As String
    Dim myTXTout As String
    Dim myTXTBOXin As RichTextBox
    Dim myTXTBOXout As RichTextBox

    Public Function analyseText(ByVal txtIN As String, txtboxIN As RichTextBox, txtOUT As String, txtboxOUT As RichTextBox) As Boolean
        Me.myTXTin = txtIN
        Me.myTXTout = txtOUT
        Me.myTXTBOXin = txtboxIN
        Me.myTXTBOXout = txtboxOUT

        removeComments()





        analyseText = True
    End Function

    Private Function removeComments()
        For Each s As String In myTXTin.Split(vbCrLf)
            If Not (s.Length > 0 And s.Substring(0, 1) = "%") Then
                myTXTout = myTXTout + s
            End If
        Next


    End Function


End Class
