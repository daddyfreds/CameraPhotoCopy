Public Class frmProgress

    Public Sub New(ByVal i_sText As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        tProgress.Value = 0
    End Sub

    Public Property Value() As Int32
        Get
            Return tProgress.Value
        End Get
        Set(ByVal value As Int32)
            tProgress.Value = value
        End Set
    End Property

    Public WriteOnly Property Message() As String
        Set(ByVal value As String)
            txtMessage.Text = value
        End Set
    End Property

    Public WriteOnly Property Title() As String
        Set(ByVal value As String)
            Text = value
        End Set
    End Property

End Class