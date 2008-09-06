Imports System.Windows.Forms

Public Class dfUVLista

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dfUVLista_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UV As ClassUV
        Dim U As ClassU
        Me.ListBox1.Items.Clear()
        For Each UV In mf.Scena.UVList
            Me.ListBox1.Items.Add(UV.Name)
        Next
        For Each U In mf.Scena.UList
            Me.ListBox1.Items.Add(U.Name)
        Next
    End Sub
End Class
