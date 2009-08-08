Imports System.Windows.Forms
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Math
Public Class dfPredefined
    Public Scena As New ClassScena
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dfPredefined_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        cfShow3d.Close()
    End Sub

    Private Sub dfPredefined_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ListBoxSamples.Items.Clear()
        Dim f As String
        For Each f In My.Computer.FileSystem.GetFiles(My.Application.Info.DirectoryPath + "\samples", FileIO.SearchOption.SearchTopLevelOnly, "*.f3d")
            Dim naziv() As String = f.Split("\")
            Me.ListBoxSamples.Items.Add(naziv(naziv.Length - 1).Split(".")(0))
        Next
        cfShow3d.Dock = DockStyle.Fill
        cfShow3d.TopLevel = False
        cfShow3d.Parent = Me
        cfShow3d.Show()
        cfShow3d.BringToFront()
    End Sub

    Private Sub ListBoxSamples_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxSamples.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Me.Scena = New ClassScena
        Try
            Dim mem As New IO.FileStream(My.Application.Info.DirectoryPath + "\samples\" + Me.ListBoxSamples.SelectedItem.ToString + ".f3dx", IO.FileMode.Open)
            Dim b As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            Me.Scena = b.Deserialize(mem)
            Me.Scena.afterPaste(cfShow3d.device)
            mem.Close()
            cfShow3d.Invalidate()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
End Class
