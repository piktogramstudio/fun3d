Public Class ClassTextWriter
    Inherits IO.StringWriter
    Dim rtb As WebBrowser
    Public Sub New(ByVal rtb As WebBrowser)
        Me.rtb = rtb
    End Sub
    Public Overrides Sub WriteLine(ByVal value As String)
        Me.rtb.DocumentText += value + "<br />"
    End Sub
    Public Overrides Sub Write(ByVal value As String)
        Me.rtb.Text += value
    End Sub
End Class
