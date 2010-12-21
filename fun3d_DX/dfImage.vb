Public Class dfImage

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If Me.TBWidth.Text < 1 Or Me.TBWidth.Text > 4000 Then
                MsgBox("Width must be between 1 and 4000")
            ElseIf Me.TBHeight.Text < 1 Or Me.TBHeight.Text > 4000 Then
                MsgBox("Height must be between 1 and 4000")
            Else
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            console.writeline(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class