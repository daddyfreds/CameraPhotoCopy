Namespace BridgeUtilities

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

        Public Function ToXML() As String
            Dim sbFileLIst As New System.Text.StringBuilder
            sbFileLIst.AppendLine("<?xml version='1.0' encoding='UTF-8' standalone='yes' ?>")
            sbFileLIst.AppendLine("<arbitrary_collection version='1'>")
            For Each oFile As File In Files
                sbFileLIst.AppendLine($"  <file uri=""{oFile.uri}"" />")
            Next
            sbFileLIst.AppendLine("</arbitrary_collection>")
            Return sbFileLIst.ToString
        End Function

    End Class



End Namespace
