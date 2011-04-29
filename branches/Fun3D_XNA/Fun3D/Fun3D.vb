Public Class Fun3D

    Private Sub ConsoleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsoleToolStripMenuItem.Click
        Me.WBConsole.Visible = Me.ConsoleToolStripMenuItem.Checked
        Me.Splitter1.Visible = Me.ConsoleToolStripMenuItem.Checked
    End Sub

    Private Sub Fun3D_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cout As New ClassTextWriter(Me.WBConsole)
        Console.SetOut(cout)
        Console.WriteLine("Fun3D(XNA) by Bojan Mitrovic 2011, Fun3D 2008-2011")
    End Sub
End Class