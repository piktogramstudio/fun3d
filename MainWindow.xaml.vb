Class MainWindow 

    Private Sub ButtonOpen_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles ButtonOpen.Click
        Dim ofd As New Microsoft.Win32.OpenFileDialog
        ofd.Filter = "Fun3D file (*.f3d)|*.f3d|All files (*.*)|*.*"
        If ofd.ShowDialog() = True Then

        End If
    End Sub
End Class
