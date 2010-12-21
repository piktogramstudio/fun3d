Public Class tfNavigacija
    Public valNUD As Boolean = True
    Private Sub NUDxcam_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NUDxcam.ValueChanged, NUDzcam.ValueChanged, NUDycam.ValueChanged, NUDugaoX.ValueChanged, NUDugaoZ.ValueChanged, NUDugaoY.ValueChanged
        If valNUD Then
            cf3D.xcam = Me.NUDxcam.Value
            cf3D.ycam = Me.NUDycam.Value
            cf3D.zcam = Me.NUDzcam.Value
            cf3D.angleX = Me.NUDugaoX.Value
            cf3D.angleY = Me.NUDugaoY.Value
            cf3D.angleZ = Me.NUDugaoZ.Value
            cf3D.Invalidate()
        End If
    End Sub

    Private Sub tfNavigacija_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        mf.NavigationToolStripMenuItem.Checked = False
    End Sub

    Private Sub tfNavigacija_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        mf.NavigationToolStripMenuItem.Checked = Me.Visible
    End Sub
End Class