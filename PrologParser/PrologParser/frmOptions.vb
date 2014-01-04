Public Class frmOptions


    

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        saveData()
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub frmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init()
        loadData()

    End Sub

    Private Sub init()
        Me.dgv_regex.Columns.Add("regex", "regex")
        Me.dgv_regex.Columns.Add("description", "description")
        Me.dgv_keywords.Columns.Add("keyword", "keyword")
        Me.dgv_keywords.Columns.Add("description", "description")
        Me.dgv_various.Columns.Add("keyword", "keyword")
        Me.dgv_various.Columns.Add("description", "description")

    End Sub

    Private Sub saveData()

        Me.dgv_keywords.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.dgv_regex.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.dgv_various.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText

        Me.dgv_regex.SelectAll()

        IO.File.WriteAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.regexFile), Me.dgv_regex.GetClipboardContent().GetText().TrimEnd.Replace(vbTab, ";"))

        Me.dgv_keywords.SelectAll()
        IO.File.WriteAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.keywordsFile), Me.dgv_keywords.GetClipboardContent().GetText().TrimEnd.Replace(vbTab, ";"))

        Me.dgv_various.SelectAll()
        IO.File.WriteAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.variousFile), Me.dgv_various.GetClipboardContent().GetText().TrimEnd.Replace(vbTab, ";"))

    End Sub

    Private Sub loadData()
        Me.dgv_keywords.Rows.Clear()
        Me.dgv_regex.Rows.Clear()
        Me.dgv_various.Rows.Clear()

        For Each l As String In My.Computer.FileSystem.ReadAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.regexFile)).Split(Environment.NewLine)
            Me.dgv_regex.Rows.Add(Split(l, ";"))
        Next
        For Each l As String In My.Computer.FileSystem.ReadAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.keywordsFile)).Split(Environment.NewLine)
            Me.dgv_keywords.Rows.Add(Split(l, ";"))
        Next

        For Each l As String In My.Computer.FileSystem.ReadAllText(IO.Path.Combine(My.Computer.FileSystem.CurrentDirectory, My.Settings.variousFile)).Split(Environment.NewLine)
            Me.dgv_various.Rows.Add(Split(l, ";"))
        Next
        Me.dgv_keywords.Sort(Me.dgv_keywords.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        Me.dgv_regex.Sort(Me.dgv_regex.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        Me.dgv_various.Sort(Me.dgv_various.Columns(0), System.ComponentModel.ListSortDirection.Ascending)



    End Sub

End Class