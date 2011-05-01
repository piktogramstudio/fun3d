Public Class ClassTextWriter
    Inherits IO.StringWriter
    Dim rtb As WebBrowser
    Public Sub New(ByVal rtb As WebBrowser)
        Me.rtb = rtb
    End Sub
    Public Overrides Sub WriteLine(ByVal value As String)
        Me.rtb.DocumentText += "<span style=""font-size:x-small"">" + value + "</span><br />"
    End Sub
    Public Overrides Sub Write(ByVal value As String)
        Me.rtb.Text += "<span style=""font-size:x-small"">" + value + "</span>"
    End Sub
End Class
