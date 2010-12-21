Imports System.Windows.Forms

Public Class dfCA
    Public CA As New ClassCA(cf3D.device, 6, 6, 3)
    Dim saveCA As New ClassCA(cf3D.device)
    Dim paintC As Boolean = True
    Dim rlist As New List(Of Rectangle)
    Dim matrixRefresh As Boolean = True
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        With Me.CA
            .nOfLevels = Me.saveCA.nOfLevels
            .matrice = Me.saveCA.matrice
            .maxBC = Me.saveCA.maxBC
            .minBC = Me.saveCA.minBC
            .toLive = Me.saveCA.toLive
            .xFields = Me.saveCA.xFields
            .yFields = Me.saveCA.yFields
        End With
        Me.CA.createLevels()
        Me.CA.refreshBuffer(cf3D.device)
        CA.refreshBuffer(cf3D.device)
        cf3D.Refresh()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dfCA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.matrixRefresh = False
        NUDC.Value = CA.yFields
        NUDR.Value = CA.xFields
        NUDL.Value = CA.nOfLevels
        Me.NUDMax.Value = CA.maxBC
        Me.NUDMin.Value = CA.minBC
        Me.NUDLive.Value = CA.toLive


        With Me.saveCA
            .xFields = Me.CA.xFields
            .yFields = Me.CA.yFields
            .nOfLevels = Me.CA.nOfLevels
            .matrice = Me.CA.matrice
            .maxBC = Me.CA.maxBC
            .minBC = Me.CA.minBC
            .toLive = Me.CA.toLive
        End With
        Me.matrixRefresh = True
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim r As Rectangle
            For Each r In rlist
                If r.Contains(e.Location) Then
                    Me.paintC = Not CA.matrice(0)(rlist.IndexOf(r)) And 1
                    CA.matrice(0)(rlist.IndexOf(r)) = Not CA.matrice(0)(rlist.IndexOf(r)) And 1
                    'CA.refreshBuffer(cf3D.device)
                    'cf3D.Refresh()
                End If
            Next
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim r As Rectangle
            For Each r In rlist
                If r.Contains(e.Location) Then
                    If paintC Then
                        CA.matrice(0)(rlist.IndexOf(r)) = 1
                        'CA.refreshBuffer(cf3D.device)
                        'cf3D.Refresh()
                    Else
                        CA.matrice(0)(rlist.IndexOf(r)) = 0
                        'CA.refreshBuffer(cf3D.device)
                        'cf3D.Refresh()
                    End If
                End If
            Next
            Me.PictureBox1.Refresh()
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        CA.createLevels()
        CA.refreshBuffer(cf3D.device)
        cf3D.Refresh()
        Me.PictureBox1.Refresh()
    End Sub


    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        rlist.Clear()
        Dim h, w As Single
        h = (e.ClipRectangle.Height - 4) / CA.yFields
        w = (e.ClipRectangle.Width - 4) / CA.xFields
        Dim t, u As Integer
        u = CA.matrice(0).Count
        For t = 0 To u - 1
            rlist.Add(New Rectangle((t - Int(t / CA.xFields) * CA.xFields) * w + 2, Int(t / CA.xFields) * h + 2, w - 2, h - 2))
            If CA.matrice(0)(t) > 0 Then
                e.Graphics.FillRectangle(Brushes.Black, rlist(t))
            Else
                e.Graphics.DrawRectangle(Pens.Black, rlist(t))
            End If
        Next
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NUDR.ValueChanged, NUDC.ValueChanged, NUDL.ValueChanged
        If sender Is NUDC And Me.matrixRefresh Then
            CA.brojPoljaPoYosi = NUDC.Value
            CA.nOfLevels = NUDL.Value
        ElseIf sender Is NUDR And Me.matrixRefresh Then
            CA.brojPoljaPoXosi = NUDR.Value
            CA.nOfLevels = NUDL.Value
        ElseIf sender Is NUDL And Me.matrixRefresh Then
            CA.nOfLevels = NUDL.Value
            CA.createLevels()
            CA.refreshBuffer(cf3D.device)
            cf3D.Refresh()
        Else
            CA.createLevels()
            CA.refreshBuffer(cf3D.device)
            cf3D.Refresh()
        End If
        If tfOsobine.Visible Then
            tfOsobine.PropertyGridUV.SelectedObject = mf.Scena.SelectedObject
            tfOsobine.PropertyGridUV.Invalidate()
        End If
        cf3D.Refresh()
        Me.PictureBox1.Refresh()
    End Sub

    Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NUDLive.ValueChanged, NUDMin.ValueChanged, NUDMax.ValueChanged
        If Me.matrixRefresh Then
            CA.toLive = Me.NUDLive.Value
            CA.minBC = Me.NUDMin.Value
            CA.maxBC = Me.NUDMax.Value
            CA.createLevels()
            CA.refreshBuffer(cf3D.device)
            cf3D.Refresh()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CA.generisiPraznuMatricu()
        CA.createLevels()
        CA.refreshBuffer(cf3D.device)
        cf3D.Refresh()
        Me.PictureBox1.Refresh()
    End Sub

    Private Sub dfCA_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.PictureBox1.Refresh()
    End Sub

    Private Sub dfCA_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        Me.Opacity = 0.8
    End Sub

    Private Sub dfCA_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Me.Opacity = 1
        mf.xdfca = Me.Left
        mf.ydfca = Me.Top
    End Sub

    Private Sub BISO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BISO.Click
        CA.createISOMesh(cf3D.device)
        CA.drawISO = Not CA.drawISO
        cf3D.Refresh()
    End Sub
End Class
