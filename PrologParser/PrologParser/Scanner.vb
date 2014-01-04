Public Class Scanner

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim o As New OpenFileDialog

        o.InitialDirectory = IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory)
        If o.ShowDialog = Windows.Forms.DialogResult.OK Then

            Me.rtf_source.Text = ""

            Dim sr As IO.StreamReader = IO.File.OpenText(o.FileName)
            Do While sr.Peek >= 0
                Me.rtf_source.Text = Me.rtf_source.Text + vbCrLf + sr.ReadLine()
            Loop



        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
End Class