Imports System.Windows.Forms

Public Class dfVideo
    Public w As Integer = 320
    Public h As Integer = 240
    Public fps As Integer = 24
    Public vhan As AviClasses.cVideoHandler = Nothing
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim vhs As New AviClasses.cVideoHandlers
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Try
            Me.w = Me.TBWidth.Text
            Me.h = Me.TBHeight.Text
            Me.fps = Me.TBFPS.Text
            Me.vhan = vhs.Handler(Me.CBCodec.SelectedIndex + 1)
        Catch ex As Exception
            MsgBox(ex.Message)
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Try
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dfVideo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim vh As AviClasses.cVideoHandler
        Dim vhs As New AviClasses.cVideoHandlers
        Dim i As Integer
        Me.CBCodec.Items.Clear()
        For i = 1 To vhs.HandlerCount
            Me.CBCodec.Items.Add(vhs.Handler(i).Name + " (" + vhs.Handler(i).Description + ")")
        Next
        Me.CBCodec.SelectedIndex = 0
    End Sub
End Class
