Imports System.IO
Module Main

    Public Enum eCopyPhotoColumns
        Selected = 0
        FilePath
        ModifiedDate
        MovedToPath
        DeletedSource
        Count = DeletedSource
    End Enum

    Public Function MakeFormat(ByVal i_s As Int32, Optional ByVal i_eFormat As LeadingZeroFormat = LeadingZeroFormat.Two) As String
        Select Case i_eFormat
            Case LeadingZeroFormat.Three
                Return Format(i_s, "000")
            Case LeadingZeroFormat.Two
                Return Format(i_s, "00")
            Case Else
                Throw New InvalidDataException("No format specified.")
        End Select
    End Function

    Public Function GetDateFolderName(ByVal i_oDate As DateTime) As String
        Dim s As String = i_oDate.Year.ToString & "\" & GetMonthWord(i_oDate.Month, True) & "\" & MakeFormat(i_oDate.Month) & "_" & MakeFormat(i_oDate.Day) & "_" & i_oDate.Year.ToString & "\" & i_oDate.Year.ToString & "_" & GetMonthWord(i_oDate.Month) & "_" & MakeFormat(i_oDate.Day)
        Return s
    End Function

    Public Function GetMonthWord(ByVal i_iMonth As Int32, Optional ByVal i_bPrefixNumber As Boolean = False) As String
        Select Case i_iMonth
            Case "1" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "January"
            Case "2" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "February"
            Case "3" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "March"
            Case "4" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "April"
            Case "5" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "May"
            Case "6" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "June"
            Case "7" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "July"
            Case "8" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "August"
            Case "9" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "September"
            Case "10" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "October"
            Case "11" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "November"
            Case "12" : Return IIf(i_bPrefixNumber, "(" & Format(i_iMonth, "00") & ") ", "") & "December"
            Case Else : Throw New Exception(i_iMonth.ToString & " not valid.")
        End Select
    End Function

    Public Enum LeadingZeroFormat
        Two
        Three
    End Enum

    Public Function MakeFolderIfNeeded(ByVal i_sPath As String) As String
        If Not Directory.Exists(i_sPath) Then Directory.CreateDirectory(i_sPath)
        Return i_sPath
    End Function

    Public Function Quote(ByVal i_s As String) As String
        Return """" & i_s & """"
    End Function

End Module
