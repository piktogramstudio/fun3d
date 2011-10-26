Public Class dfImage

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If CDbl(Me.TBWidth.Text) < 1 Or CDbl(Me.TBWidth.Text) > 4000 Then
                MsgBox("Width must be between 1 and 4000")
            ElseIf CDbl(Me.TBHeight.Text) < 1 Or CDbl(Me.TBHeight.Text) > 4000 Then
                MsgBox("Height must be between 1 and 4000")
            Else
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dfImage_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        Me.TBHeight.Text = cf3D.Height.ToString
        Me.TBWidth.Text = cf3D.Width.ToString
    End Sub
End Class