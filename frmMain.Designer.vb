<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnBrowseSource = New System.Windows.Forms.Button()
        Me.btnBrowseDestination = New System.Windows.Forms.Button()
        Me.txtDestinationPath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grdCopyPhotos = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblSourceFolders = New System.Windows.Forms.Label()
        CType(Me.grdCopyPhotos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Location = New System.Drawing.Point(809, 432)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(53, 23)
        Me.btnImport.TabIndex = 2
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnBrowseSource
        '
        Me.btnBrowseSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseSource.Location = New System.Drawing.Point(657, 1)
        Me.btnBrowseSource.Name = "btnBrowseSource"
        Me.btnBrowseSource.Size = New System.Drawing.Size(53, 23)
        Me.btnBrowseSource.TabIndex = 5
        Me.btnBrowseSource.Text = "Browse"
        Me.btnBrowseSource.UseVisualStyleBackColor = True
        '
        'btnBrowseDestination
        '
        Me.btnBrowseDestination.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseDestination.Location = New System.Drawing.Point(809, 401)
        Me.btnBrowseDestination.Name = "btnBrowseDestination"
        Me.btnBrowseDestination.Size = New System.Drawing.Size(53, 23)
        Me.btnBrowseDestination.TabIndex = 7
        Me.btnBrowseDestination.Text = "Browse"
        Me.btnBrowseDestination.UseVisualStyleBackColor = True
        '
        'txtDestinationPath
        '
        Me.txtDestinationPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDestinationPath.Location = New System.Drawing.Point(109, 402)
        Me.txtDestinationPath.Name = "txtDestinationPath"
        Me.txtDestinationPath.Size = New System.Drawing.Size(694, 20)
        Me.txtDestinationPath.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Folders to Import:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 405)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Album Folder:"
        '
        'grdCopyPhotos
        '
        Me.grdCopyPhotos.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.grdCopyPhotos.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.grdCopyPhotos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCopyPhotos.ColumnInfo = resources.GetString("grdCopyPhotos.ColumnInfo")
        Me.grdCopyPhotos.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdCopyPhotos.ExtendLastCol = True
        Me.grdCopyPhotos.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never
        Me.grdCopyPhotos.Location = New System.Drawing.Point(109, 30)
        Me.grdCopyPhotos.Name = "grdCopyPhotos"
        Me.grdCopyPhotos.Rows.DefaultSize = 17
        Me.grdCopyPhotos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdCopyPhotos.Size = New System.Drawing.Size(694, 366)
        Me.grdCopyPhotos.StyleInfo = resources.GetString("grdCopyPhotos.StyleInfo")
        Me.grdCopyPhotos.TabIndex = 45
        Me.grdCopyPhotos.Text = "C1FlexGrid2"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(716, 1)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(87, 23)
        Me.btnRefresh.TabIndex = 46
        Me.btnRefresh.Text = "Refresh Grid"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblSourceFolders
        '
        Me.lblSourceFolders.AutoSize = True
        Me.lblSourceFolders.Location = New System.Drawing.Point(109, 6)
        Me.lblSourceFolders.Name = "lblSourceFolders"
        Me.lblSourceFolders.Size = New System.Drawing.Size(39, 13)
        Me.lblSourceFolders.TabIndex = 47
        Me.lblSourceFolders.Text = "Label3"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 467)
        Me.Controls.Add(Me.lblSourceFolders)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.grdCopyPhotos)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBrowseDestination)
        Me.Controls.Add(Me.txtDestinationPath)
        Me.Controls.Add(Me.btnBrowseSource)
        Me.Controls.Add(Me.btnImport)
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Camera Photo Copy"
        CType(Me.grdCopyPhotos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnBrowseSource As System.Windows.Forms.Button
    Friend WithEvents btnBrowseDestination As System.Windows.Forms.Button
    Friend WithEvents txtDestinationPath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdCopyPhotos As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents lblSourceFolders As System.Windows.Forms.Label

End Class
