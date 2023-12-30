Imports System.IO, System.IO.Path
Public Class frmMain

    Private Delegate Sub UpdateProgressDelegate(ByVal i_iValue As Int32, ByVal i_sMessage As String, ByVal i_sTitle As String)
    Private Delegate Sub CloseProgressFormDelegate()

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

            Dim sImportFolders() As String = Split(My.Settings.ImportFolders, "|")
            For Each sFolder As String In sImportFolders
                Dim sINIFiles() As String = IO.Directory.GetFiles(sFolder, ".picasa.ini", SearchOption.AllDirectories)
                For Each sFile As String In sINIFiles
                    IO.File.Delete(sFile)
                Next
                Dim sFiles() As String = IO.Directory.GetFiles(sFolder, "*.*", SearchOption.AllDirectories)

                If sFiles.Length = 0 Then
                    IO.Directory.Delete(sFolder, True)
                    IO.Directory.CreateDirectory(sFolder)
                End If
            Next
        Catch ex As Exception
            MsgBox("Failed to clean up import folder(s): " & ex.Message)
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Text = "Copy Photo v" & My.Application.Info.Version.ToString

            FillGrid(My.Settings.ImportFolders)

            txtDestinationPath.Text = My.Settings.LocalDestinationFolder
            Me.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillGrid(ByVal i_sImportFolderPath As String)
        lblSourceFolders.Text = i_sImportFolderPath
        Dim sImportFolders() As String = Split(i_sImportFolderPath, "|")
        Dim sImportTypes() As String = Split(My.Settings.FileTypesToImport, "|")
        grdCopyPhotos.Rows.Count = 1
        Dim bOldPhotos As Boolean = False
        For Each sFolder As String In sImportFolders
            For Each sType As String In sImportTypes
                Dim sFiles() As String = IO.Directory.GetFiles(sFolder, sType, SearchOption.AllDirectories)
                For Each sFile As String In sFiles
                    grdCopyPhotos.AddItem(True) 'Check it
                    grdCopyPhotos.Item(grdCopyPhotos.Rows.Count - 1, eCopyPhotoColumns.FilePath) = sFile
                    grdCopyPhotos.Item(grdCopyPhotos.Rows.Count - 1, eCopyPhotoColumns.ModifiedDate) = IO.File.GetLastWriteTime(sFile).ToShortDateString
                    If Now.Subtract(IO.File.GetLastWriteTime(sFile)).TotalDays > My.Settings.PhotoTooOldInDays Then
                        grdCopyPhotos.SetCellStyle(grdCopyPhotos.Rows.Count - 1, eCopyPhotoColumns.ModifiedDate, grdCopyPhotos.Styles.Highlight)
                        bOldPhotos = True
                    End If
                Next
            Next
        Next

        If bOldPhotos Then MsgBox("There are some old photos in this batch.  I've highlighted their dates.")
        btnImport.Enabled = grdCopyPhotos.Rows.Count > 1
    End Sub

    Private Sub WriteLog()
        Dim sFilePath As String = My.Settings.LogFolder & "\" & Replace(Now.TimeOfDay.ToString, ":", ";") & ".txt"
        Dim sLog As String = Nothing
        For r As Int32 = 0 To grdCopyPhotos.Rows.Count - 1
            sLog += vbCrLf
            For c As Int32 = 0 To eCopyPhotoColumns.Count
                sLog += vbTab
                sLog += CStr(grdCopyPhotos.Item(r, c))
            Next
        Next
        If Not IO.Directory.Exists(IO.Path.GetDirectoryName(sFilePath)) Then IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(sFilePath))
        IO.File.WriteAllText(sFilePath, sLog)
    End Sub

    Private Class clsCopyPhotos
        Private m_fProgress As frmProgress
        Private sFiles() As Object
        Private m_sPCTargetPath As String
        Public FileDestinations() As String
        Public FileDeletions() As String
        Public Sub New(ByVal i_fProgress As frmProgress, ByRef i_sFiles() As Object, ByVal i_sPCTargetPath As String)
            m_fProgress = i_fProgress
            sFiles = i_sFiles
            m_sPCTargetPath = i_sPCTargetPath
            ReDim FileDeletions(UBound(i_sFiles))
            ReDim FileDestinations(UBound(i_sFiles))
        End Sub

        Public Event CopyPhotosDone(ByVal sender As clsCopyPhotos)

        Public Sub CloseProgressForm()
            m_fProgress.Close()
        End Sub

        Public Sub UpdateProgressBarValue(ByVal i_iValue As Int32, ByVal i_sMessage As String, ByVal i_sTitle As String)
            m_fProgress.Value = i_iValue
            m_fProgress.Message = i_sMessage
            m_fProgress.Title = i_sTitle
        End Sub

        Private Function GetValidFileNameIndex(ByVal i_sListOfExistingFiles() As String) As String
            Dim iHighestIndex As Int32 = i_sListOfExistingFiles.Length + 1
            For Each sFilename As String In i_sListOfExistingFiles
                Dim iFileNameIndex As Int32
                Dim vFileName() As String = Split(Path.GetFileNameWithoutExtension(sFilename), "_")
                If IsNumeric(vFileName(UBound(vFileName))) Then
                    iFileNameIndex = CInt(vFileName(UBound(vFileName)))
                    iHighestIndex = Math.Max(iHighestIndex, iFileNameIndex)
                End If
            Next

            Return MakeFormat(iHighestIndex + 1, LeadingZeroFormat.Three)

        End Function

        Public Sub CopyPhotos()
            Dim cdlg As New CloseProgressFormDelegate(AddressOf CloseProgressForm)
            Dim udlg As New UpdateProgressDelegate(AddressOf UpdateProgressBarValue)

            Try
                Dim bError As Boolean = False

                For i As Int32 = 0 To sFiles.GetUpperBound(0)
                    Dim sDestinationPath As String = Nothing
                    If IO.File.Exists(sFiles(i)) Then
                        Try
                            Dim oCreate As DateTime = File.GetCreationTime(sFiles(i))
                            Dim oModified As DateTime = File.GetLastWriteTime(sFiles(i))
                            Dim oPictureDate As DateTime

                            If oCreate > oModified Then
                                oPictureDate = oModified
                            Else
                                oPictureDate = oCreate
                            End If

                            Dim sNewFileNameWithoutExt As String = Combine(m_sPCTargetPath, GetDateFolderName(oPictureDate))
                            Dim DestinationDirectory As String = MakeFolderIfNeeded(GetDirectoryName(sNewFileNameWithoutExt))
                            Dim sExistingFiles() As String = Directory.GetFiles(DestinationDirectory)

                            Dim sID As String = GetValidFileNameIndex(sExistingFiles)

                            sDestinationPath = Combine(DestinationDirectory, sNewFileNameWithoutExt & "_" & sID & GetExtension(sFiles(i)))

                            If File.Exists(sDestinationPath) Then Throw New Exception(sDestinationPath & " already exists.")

                            FileDestinations(i) = sDestinationPath

                            File.Copy(sFiles(i), sDestinationPath, True)

                            Dim ar() As Object = New Object() {CInt(((i + 1) / sFiles.Length) * 100), Quote(Path.GetFileName(sFiles(i))) & " ==> " & Quote(Path.GetFileName(sDestinationPath)), sDestinationPath}
                            m_fProgress.Invoke(udlg, ar)
                        Catch ex As Exception
                            FileDestinations(i) = ex.Message
                            Throw New Exception("There was a problem moving the files. " & vbCrLf & vbCrLf & sFiles(i) & "===>" & sDestinationPath & vbCrLf & vbCrLf & ex.Message)
                        End Try
                    Else
                        FileDestinations(i) = "NOT IMPORTED"
                    End If
                Next

                m_fProgress.Invoke(cdlg)


                Dim oResult As DialogResult = MsgBox("Done.  Would you like to delete the files from the source?", MsgBoxStyle.YesNo, "Success!")
                Select Case oResult
                    Case Windows.Forms.DialogResult.Yes
                        For i As Int32 = 0 To UBound(sFiles)
                            Try
                                If IO.File.Exists(sFiles(i)) Then
                                    File.Delete(sFiles(i))
                                    FileDeletions(i) = "DELETED"
                                End If
                            Catch ex As Exception
                                bError = True
                            End Try
                        Next
                End Select

                If bError Then
                    MsgBox("Failed to delete files from the source.  Try to do it manually.  I'll open the folder for you.")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                RaiseEvent CopyPhotosDone(Me)
            End Try
        End Sub
    End Class

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        btnImport.Enabled = False

        Dim lstFiles As New ArrayList
        For i As Int32 = 1 To grdCopyPhotos.Rows.Count - 1
            Dim sPath As String = Nothing
            If CBool(grdCopyPhotos.Item(i, eCopyPhotoColumns.Selected)) = True Then
                sPath = grdCopyPhotos.Item(i, eCopyPhotoColumns.FilePath)
            End If
            lstFiles.Add(sPath)
        Next

        If lstFiles.Count = 0 Then MsgBox("Didn't check any folders") : Exit Sub

        Dim sFiles() As Object = lstFiles.ToArray
        Dim sDestinations(UBound(sFiles)) As String

        Me.WindowState = FormWindowState.Maximized

        Dim fProgress As frmProgress = New frmProgress("Importing Files")
        Dim oCopyPhotos As New clsCopyPhotos(fProgress, sFiles, txtDestinationPath.Text)
        Dim oThread As New Threading.Thread(AddressOf oCopyPhotos.CopyPhotos)
        oThread.Start()
        fProgress.ShowDialog()
        AddHandler oCopyPhotos.CopyPhotosDone, AddressOf EndCopyPhotos
    End Sub

    Private Sub EndCopyPhotos(ByVal oCopyPhotos As clsCopyPhotos)
        For i As Int32 = 0 To UBound(oCopyPhotos.FileDestinations)
            grdCopyPhotos.Item(i + 1, eCopyPhotoColumns.MovedToPath) = oCopyPhotos.FileDestinations(i)
            grdCopyPhotos.Item(i + 1, eCopyPhotoColumns.DeletedSource) = oCopyPhotos.FileDeletions(i)
        Next
        WriteLog()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Enum StatusType
        Head = 0
        Item
        None
    End Enum

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseSource.Click
        Dim oFolderDialog As New FolderBrowserDialog
        oFolderDialog.SelectedPath = My.Settings.ImportFolders
        Select Case oFolderDialog.ShowDialog()
            Case Windows.Forms.DialogResult.Cancel
            Case Else
                If oFolderDialog.SelectedPath <> "" Then
                    FillGrid(oFolderDialog.SelectedPath)
                End If
        End Select
    End Sub

    Private Sub btnBrowseDestination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDestination.Click
        Dim oFolderDialog As New FolderBrowserDialog
        Select Case oFolderDialog.ShowDialog()
            Case Windows.Forms.DialogResult.Cancel
            Case Else
                If oFolderDialog.SelectedPath <> "" Then
                    txtDestinationPath.Text = oFolderDialog.SelectedPath
                End If
        End Select
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        FillGrid(lblSourceFolders.Text)
    End Sub

    Private Sub btnMakeAlbums_Click(sender As Object, e As EventArgs) Handles btnMakeAlbums.Click
        Try
            Dim oDialog As New FolderBrowserDialog()
            Select Case oDialog.ShowDialog
                Case DialogResult.OK
                    If String.IsNullOrWhiteSpace(oDialog.SelectedPath) Then Throw New Exception("No folder selected")
                    If Not IO.Directory.Exists(oDialog.SelectedPath) Then Throw New Exception("No folder exists")
                    Dim sFolders() As String = IO.Directory.GetDirectories(oDialog.SelectedPath)
                    Dim lstCollections As New List(Of Bridge.Collection)
                    For Each sFolder As String In sFolders
                        Dim oCollection As New Bridge.Collection
                        For Each sFile As String In IO.Directory.GetFiles(sFolder, "*.*")
                            Dim sResult() As String = Bridge.utilities.FindPictureInLibrary(sFile, My.Settings.LocalDestinationFolder)
                            'todo: validation to make sure it has exactly 1, if not , report error
                            oCollection.Files.Add(New Bridge.Collection.File() With {.Path = sResult(0)})
                        Next
                    Next
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

End Class

