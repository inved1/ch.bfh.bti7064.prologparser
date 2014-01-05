Public Class Settings


    Dim myTBkeywords As DataTable
    Dim myTBregex As DataTable
    Dim myTBvarious As DataTable
    Sub New(ByVal tbkeywords As DataTable, ByVal tbregex As DataTable, ByVal tbvarious As DataTable)
        Me.myTBkeywords = tbkeywords
        Me.myTBregex = tbregex
        Me.myTBvarious = tbvarious


    End Sub

    Public ReadOnly Property keywords As DataTable

        Get
            Return Me.myTBkeywords
        End Get
    End Property

    Public ReadOnly Property regex As DataTable
        Get
            Return Me.myTBregex
        End Get
    End Property

    Public ReadOnly Property various As DataTable
        Get
            Return Me.myTBvarious
        End Get
    End Property

End Class
