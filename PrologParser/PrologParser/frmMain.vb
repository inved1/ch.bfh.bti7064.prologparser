Public Class frmMain

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dim f As New frmOptions
        f.MdiParent = Me
        f.Show()

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim f As New AboutBox1
        f.MdiParent = Me
        f.Show()

    End Sub

    Private Sub PrologParserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrologParserToolStripMenuItem.Click
        Dim f As New Scanner
        f.MdiParent = Me
        f.Show()

    End Sub
End Class
