Public Class dfAbout
    Private Sub LinkLabel4_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        System.Diagnostics.Process.Start("mailto:mitrovic.bojan@gmail.com")
    End Sub

    Private Sub LinkLabel5_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        System.Diagnostics.Process.Start("http://code.google.com/p/fun3d")
    End Sub
 
    Private Sub LinkLabel6_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        System.Diagnostics.Process.Start("http://creativecommons.org/licenses/by/3.0/")
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        System.Diagnostics.Process.Start("http://creativecommons.org/licenses/by/3.0/")
    End Sub
End Class