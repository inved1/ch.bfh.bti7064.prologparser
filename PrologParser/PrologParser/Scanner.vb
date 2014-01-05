Public Class Scanner

    Dim mySettings As Settings
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
        Dim oAn As New Analyser(Me.mySettings)
        Me.rtf_result.Text = ""
        oAn.analyseText(Me.rtf_source.Text, Me.rtf_source, Me.rtf_result.Text, Me.rtf_result)


    End Sub

    Private Sub Scanner_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim tbregex As New DataTable
        tbregex.Columns.Add("regex")
        tbregex.Columns.Add("description")
        Dim tbkeywords As New DataTable
        tbkeywords.Columns.Add("keyword")
        tbkeywords.Columns.Add("description")
        Dim tbvarious As New DataTable
        tbvarious.Columns.Add("various")
        tbvarious.Columns.Add("description")

        For Each l As String In My.Computer.FileSystem.ReadAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.regexFile)).Split(Environment.NewLine)
            tbregex.Rows.Add(Split(l, ";"))
        Next
        For Each l As String In My.Computer.FileSystem.ReadAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.keywordsFile)).Split(Environment.NewLine)
            tbkeywords.Rows.Add(Split(l, ";"))
        Next

        For Each l As String In My.Computer.FileSystem.ReadAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.variousFile)).Split(Environment.NewLine)
            tbvarious.Rows.Add(Split(l, ";"))
        Next

        Me.mySettings = New Settings(tbkeywords, tbregex, tbvarious)

    End Sub
End Class