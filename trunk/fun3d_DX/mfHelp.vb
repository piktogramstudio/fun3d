Public Class mfHelp

    Private Sub mfHelp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WebBrowser1.Url = New Uri(My.Application.Info.DirectoryPath + "/help/index.html")
    End Sub
End Class