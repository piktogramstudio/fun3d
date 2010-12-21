Public Class ClassTextWriter
    Inherits IO.StringWriter
    Dim rtb As RichTextBox
    Public Sub New(ByVal rtb As RichTextBox)
        Me.rtb = rtb
    End Sub
    Public Overrides Sub WriteLine(ByVal value As String)
        Me.rtb.Text += value + vbCrLf
        Me.rtb.Select(Me.rtb.Text.Length, 0)
        Me.rtb.ScrollToCaret()
    End Sub
    Public Overrides Sub Write(ByVal value As String)
        Me.rtb.Text += value
        Me.rtb.Select(Me.rtb.Text.Length, 0)
        Me.rtb.ScrollToCaret()
    End Sub
End Class
