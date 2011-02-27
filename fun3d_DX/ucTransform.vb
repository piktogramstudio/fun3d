Public Class ucTransform
    Public Property transformObject As New ClassU()
    Private Sub ucTransform_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim b As New Binding("Value", Me.transformObject.transform, "rx")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TrackBar1.DataBindings.Add(b)
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Me.transformObject.tgeom = Me.transformObject.transform.getTransformedGeometry(Me.transformObject.geom)
        cf3D.Invalidate()
    End Sub
End Class
