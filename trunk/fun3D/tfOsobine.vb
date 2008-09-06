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
        Catch ex As Exception

        End Try
    End Sub
End Class