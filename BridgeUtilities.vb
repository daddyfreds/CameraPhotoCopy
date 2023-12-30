Imports C1.Win.C1FlexGrid

Namespace Bridge

    Public Class Collection
        Public Property Files As List(Of File)
        Public Property Name As String
        Public Property Folder As String
        Public ReadOnly Property Path As String
            Get
                Return $"{IO.Path.Combine(Folder, Name)}.filelist"
            End Get
        End Property
        Public Class File
            Public Property Path As String
            Public ReadOnly Property uri() As String
                Get
                    Return $"bridge:fs:file:///{Replace(Path, "\", "/")}"
                End Get
            End Property
        End Class

        Private Function ToXML() As String
            Dim sbFileLIst As New System.Text.StringBuilder
            sbFileLIst.AppendLine("<?xml version='1.0' encoding='UTF-8' standalone='yes' ?>")
            sbFileLIst.AppendLine("<arbitrary_collection version='1'>")
            For Each oFile As File In Files
                sbFileLIst.AppendLine($"  <file uri=""{oFile.uri}"" />")
            Next
            sbFileLIst.AppendLine("</arbitrary_collection>")
            Return sbFileLIst.ToString
        End Function

        Private Sub Save()
            Try
                Dim xml As New Xml.XmlDocument()
                xml.LoadXml(ToXML)
                xml.Save(Path)
            Catch ex As Exception
                MsgBox($"Failed to save collection ""{Path}"" : {ex.Message}")
            End Try
        End Sub


    End Class

    Public Class utilities
        Public Shared Function FindPictureInLibrary(i_sPicturePath As String, i_sLibraryFolder As String) As String()
            Return IO.Directory.GetFiles(i_sLibraryFolder, IO.Path.GetFileName(i_sPicturePath), IO.SearchOption.AllDirectories)
        End Function
    End Class

End Namespace
