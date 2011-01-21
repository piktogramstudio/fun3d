Public Class tfObjectCh
    Dim ex, ey As Single
    Public objectsList As New List(Of Object)
    Public checkedObjList As New List(Of Object)
    Private Sub BClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BClose.Click
        Me.Close()
    End Sub

    Private Sub PTitle_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PTitle.MouseDown
        Me.ex = e.X
        Me.ey = e.Y
    End Sub

    
    Private Sub PTitle_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PTitle.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = Control.MousePosition - New Size(CInt(ex), CInt(ey))
        End If
    End Sub

    Private Sub tfObjectCh_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            Me.CheckedListBox1.Items.Clear()
            Dim o As Object
            For Each o In Me.objectsList
                Try
                    Me.CheckedListBox1.Items.Add(o, False)
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub

    Private Sub BOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK

        Me.Close()
    End Sub

    Private Sub BCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BCancel.Click
        Me.Close()
    End Sub

    Private Sub LTitle_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LTitle.MouseDown
        Me.ex = e.X
        Me.ey = e.Y
    End Sub

    Private Sub LTitle_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LTitle.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location = Control.MousePosition - New Size(CInt(ex), CInt(ey))
        End If
    End Sub
End Class