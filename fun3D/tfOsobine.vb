Public Class tfOsobine
    Public WithEvents scn As ClassScena = mf.Scena
    Private Sub tfOsobine_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.PropertyGridUV.SelectedObject = mf.Scena.SelectedObject
        Me.Refresh()
    End Sub
    Private Sub scn_SelectionChanged() Handles scn.SelectionChanged
        Try
            Me.PropertyGridUV.SelectedObject = mf.Scena.SelectedObject
            Me.Refresh()
            Me.LSelectedObjName.Text = mf.Scena.SelectedObject.Name
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tfOsobine_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        Me.Opacity = 0.8
    End Sub

    Private Sub tfOsobine_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Me.Opacity = 1
    End Sub

    Private Sub PropertyGridUV_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGridUV.PropertyValueChanged
        Try
            mf.Scena.SelectedObject.refreshBuffer()
            cf3D.renderToTexture(mf.Scena.SelectedObject)
        Catch ex As Exception

        End Try
        cf3D.Refresh()
        mf.addUndoData("Property value changed")
    End Sub
End Class