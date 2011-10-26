Public Class tfTransform
    Public uc As New ucTransform()
    Private Sub tfTransform_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.pMain.Controls.Clear()
        Me.uc.Dock = DockStyle.Fill
        Me.uc.AutoScroll = True
        Me.pMain.Controls.Add(Me.uc)
    End Sub
End Class