
Imports System.Text.RegularExpressions
Public Class Analyser




    Dim myTXTin As String
    Dim myTXTout As String
    Dim myTXTBOXin As RichTextBox
    Dim myTXTBOXout As RichTextBox
    Dim mySettings As Settings

    Public Sub New(ByVal oSettings As Settings)
        Me.mySettings = oSettings
    End Sub
    Public Function analyseText(ByVal txtIN As String, ByRef txtboxIN As RichTextBox, ByVal txtOUT As String, ByRef txtboxOUT As RichTextBox) As Boolean
        Me.myTXTin = txtIN
        Me.myTXTout = txtOUT
        Me.myTXTBOXin = txtboxIN
        Me.myTXTBOXout = txtboxOUT

        removeComments()

        'rules here - I dont know what to identify first, what last. so I'll start with this rules:
        ' 1. identify assignemts (and variables therefore)
        ' identify also brackets and therefore functions
        ' 2. identify . and therefore end-of-statements
        ' 3. identify structs and lists - therefore functions (structs)
        ' 4. identify keywords 
        ' 5. identify comparers
        ' 6. identify strings   

        identifyAssignments()
        identifyBrackets()


        Me.myTXTBOXout.Text = myTXTout

        analyseText = True
    End Function

    Private Function identifyBrackets()
        'find first bracket, everything before is a function
        Dim pattern As String = Me.mySettings.regex.Select("description =  'beginnfunction'")(0).Item(0).ToString.Trim

        Dim regex As New Regex(pattern)
        Dim pos As New SortedDictionary(Of Integer, String)

        For Each m As Match In regex.Matches(myTXTout)
            pos.Add(m.Index, m.Value)
        Next

        For Each entry As KeyValuePair(Of Integer, String) In pos.Reverse
            myTXTout = myTXTout.Insert(entry.Key, "##FUN_START##")
            myTXTout = myTXTout.Insert(entry.Key + "##FUN_START##".Length + entry.Value.Length - 1, "##FUN_END##")
        Next

    End Function

    Private Function identifyAssignments()
        Dim pattern As String = Me.mySettings.regex.Select("description =  'zuweisung'")(0).Item(0).ToString.Trim
        Dim replacement As String = "##ASSIGNMENT##"
        Dim regex As New Regex(pattern, RegexOptions.IgnorePatternWhitespace)

        myTXTout = regex.Replace(myTXTout, replacement)



        ' now search for string, followed by ##assignment## -> should be variable
        Dim patternString As String = Me.mySettings.regex.Select("description =  'Konstante'")(0).Item(0).ToString.Trim
        '[_a-z][_a-zA-Z0-9]*(\W)##ASSIGNMENT##(\W)

        patternString = patternString + "(\W)##ASSIGNMENT##(\W)"
        identifyConst(patternString)

        patternString = Me.mySettings.regex.Select("description =  'Konstante mit funktion'")(0).Item(0).ToString.Trim
        patternString = patternString + "(\W)##ASSIGNMENT##(\W)"
        identifyConst(patternString)

        patternString = Me.mySettings.regex.Select("description =  'Variable'")(0).Item(0).ToString.Trim
        patternString = patternString + "(\W)##ASSIGNMENT##(\W)"
        identifyConst(patternString)

        patternString = Me.mySettings.regex.Select("description =  'Variable mit funktion'")(0).Item(0).ToString.Trim
        patternString = patternString + "(\W)##ASSIGNMENT##(\W)"
        identifyConst(patternString)


    End Function

    Private Function identifyConst(pattern As String)
        Dim regex2 As New Regex(pattern)

        Dim pos As New SortedDictionary(Of Integer, String)
        For Each m As Match In regex2.Matches(myTXTout)
            Debug.Print(m.Value)
            pos.Add(m.Index, m.Value)

        Next


        'reverse, easyier to replace 
        For Each entry As KeyValuePair(Of Integer, String) In pos.Reverse
            myTXTout = myTXTout.Insert(entry.Key, "##KONST_START##")
            myTXTout = myTXTout.Insert(entry.Key + "##KONST_START##".Length + entry.Value.Replace("##ASSIGNMENT##", "").Trim.Length, "##KONST_END##")

        Next

    End Function

    Private Function removeComments()
        Dim pattern As String = Me.mySettings.regex.Select("description =  'comments c-style'")(0).Item(0).ToString.Trim


        For Each s As String In myTXTin.Split(vbLf)
            If s.Length > 0 Then
                If Not (s.Length > 0 And s.Substring(0, 1) = "%") Then

                    Dim count As Integer = Regex.Match(s, pattern).Length
                    If count < 1 Then
                        myTXTout = myTXTout + s + vbLf
                    End If

                End If
            End If
        Next


    End Function


End Class
