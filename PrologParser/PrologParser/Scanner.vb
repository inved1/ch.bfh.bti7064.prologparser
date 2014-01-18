Imports System.Text.RegularExpressions

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

        'cghange color
        colorMe(New System.Text.RegularExpressions.Regex("[#][#][F][U][N][_][S][T][A][R][T][#][#]"),
                New System.Text.RegularExpressions.Regex("[#][#][F][U][N][_][E][N][D][#][#]"),
                Color.Blue, New List(Of String)(New String() {"##FUN_START##", "##FUN_END##"}))
        colorMe(New System.Text.RegularExpressions.Regex("[#][#][K][O][N][S][T][_][S][T][A][R][T][#][#]"),
                New System.Text.RegularExpressions.Regex("[#][#][K][O][N][S][T][_][E][N][D][#][#]"),
                Color.Red, New List(Of String)(New String() {"##KONST_START##", "##KONST_END##"}))
        colorMe(New System.Text.RegularExpressions.Regex("[#][#][L][I][S][T][_][S][T][A][R][T][#][#]"),
             New System.Text.RegularExpressions.Regex("[#][#][L][I][S][T][_][E][N][D][#][#]"),
             Color.Violet, New List(Of String)(New String() {"##LIST_START##", "##LIST_END##"}))

        colorMe(New System.Text.RegularExpressions.Regex("[#][#][K][E][Y][W][O][R][D][_][S][T][A][R][T][#][#]"),
             New System.Text.RegularExpressions.Regex("[#][#][K][E][Y][W][O][R][D][_][E][N][D][#][#]"),
            Color.Gold, New List(Of String)(New String() {"##KEYWORD_START##", "##KEYWORD_END##"}))
        colorMe(New System.Text.RegularExpressions.Regex("[#][#][C][O][M][P][A][R][E][R][_][S][T][A][R][T][#][#]"),
       New System.Text.RegularExpressions.Regex("[#][#][C][O][M][P][A][R][E][R][_][E][N][D][#][#]"),
      Color.Gold, New List(Of String)(New String() {"##COMPARER_START##", "##COMPARER_END##"}))

        colorMe(New System.Text.RegularExpressions.Regex("[#][#][A][S][S][I][G][N][M][E][N][T][#][#]"),
              ":-", "##ASSIGNMENT##".Length,
              Color.Green, New List(Of String)(New String() {"##ASSIGNMENT##"}))




    End Sub

    Private Sub colorMe(regexstart As Regex, replaceString As String, len As Integer, color As Color, patternsToRemove As List(Of String))
        Dim lpoints As New List(Of Integer)


        For Each m As Match In regexstart.Matches(Me.rtf_result.Text)
            lpoints.Add(m.Index)
        Next


        For Each i As Integer In lpoints

            Me.rtf_result.SelectionStart = i
            Me.rtf_result.SelectionLength = len
            If Me.rtf_result.SelectionColor.Name = "Black" Then
                Me.rtf_result.SelectionColor = color
            End If
        Next


        For Each s As String In patternsToRemove
            Me.rtf_result.Rtf = Me.rtf_result.Rtf.Replace(s, replaceString)
        Next
    End Sub
    Private Sub colorMe(regexstart As Regex, regexend As Regex, color As Color, keywordsToRemove As List(Of String))
        Dim lpoints As New List(Of Integer)


        For Each m As Match In regexstart.Matches(Me.rtf_result.Text)
            lpoints.Add(m.Index)
        Next
        Dim lpoints2 As New List(Of Integer)

        For Each m As Match In regexend.Matches(Me.rtf_result.Text)
            lpoints2.Add(m.Index)
        Next

        For Each i As Integer In lpoints
            Dim j As Integer = lpoints2.Item(lpoints.IndexOf(i))
            Me.rtf_result.SelectionStart = i
            Me.rtf_result.SelectionLength = j - i
            If Me.rtf_result.SelectionColor.Name = "Black" Then
                Me.rtf_result.SelectionColor = color
            End If
        Next


        For Each s As String In keywordsToRemove
            Me.rtf_result.Rtf = Me.rtf_result.Rtf.Replace(s, "")
        Next
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