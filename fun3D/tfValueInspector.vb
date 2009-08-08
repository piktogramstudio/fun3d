Public Class tfValueInspector
    Private Sub refreshCombo()
        Dim UV As ClassUV
        Dim U As ClassU
        Me.ComboBoxPropertyObject.Items.Clear()
        For Each UV In mf.Scena.UVList
            Me.ComboBoxPropertyObject.Items.Add(UV.Name)
        Next
        For Each U In mf.Scena.UList
            Me.ComboBoxPropertyObject.Items.Add(U.Name)
        Next
    End Sub

    Private Sub tfValueInspector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.refreshCombo()
    End Sub

    Private Sub ComboBoxPropertyObject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxPropertyObject.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim UV As ClassUV = mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex)
            ' U min
            Me.TrackBarU.Increment = UV.sliderStepUmin
            Me.TrackBarU.Minimum = UV.sliderMinimumUmin
            Me.TrackBarU.Maximum = UV.sliderMaximumUmin
            Me.TrackBarU.Value = UV.minimalnoU
            Me.TextBoxUmax.Text = UV.sliderMaximumUmin
            Me.TextBoxUmin.Text = UV.sliderMinimumUmin
            Me.TextBoxUstep.Text = UV.sliderStepUmin
            ' V min
            Me.TrackBarV.Increment = UV.sliderStepVmin
            Me.TrackBarV.Minimum = UV.sliderMinimumVmin
            Me.TrackBarV.Maximum = UV.sliderMaximumVmin
            Me.TrackBarV.Value = UV.minimalnoV
            Me.TextBoxVmax.Text = UV.sliderMaximumVmin
            Me.TextBoxVmin.Text = UV.sliderMinimumVmin
            Me.TextBoxVstep.Text = UV.sliderStepVmin
            ' U max
            Me.TrackBarUm.Increment = UV.sliderStepUmax
            Me.TrackBarUm.Minimum = UV.sliderMinimumUmax
            Me.TrackBarUm.Maximum = UV.sliderMaximumUmax
            Me.TrackBarUm.Value = UV.maksimalnoU
            Me.TextBoxUmaxm.Text = UV.sliderMaximumUmax
            Me.TextBoxUminm.Text = UV.sliderMinimumUmax
            Me.TextBoxUstepm.Text = UV.sliderStepUmax
            ' V max
            Me.TrackBarVm.Increment = UV.sliderStepVmax
            Me.TrackBarVm.Minimum = UV.sliderMinimumVmax
            Me.TrackBarVm.Maximum = UV.sliderMaximumVmax
            Me.TrackBarVm.Value = UV.maksimalnoV
            Me.TextBoxVmaxm.Text = UV.sliderMaximumVmax
            Me.TextBoxVminm.Text = UV.sliderMinimumVmax
            Me.TextBoxVstepm.Text = UV.sliderStepVmax
            ' kontrole za parametre
            Me.Panel1.Controls.Clear()
            Dim p As ClassParametri
            Dim tp As Integer = Me.TextBoxUmin.Top
            Dim i As Integer = 0
            For Each p In UV.prm
                Dim t1 As TextBox
                t1 = New TextBox
                t1.Name = "minvT" + i.ToString
                t1.Top = tp
                t1.Left = Me.TextBoxUmin.Left
                t1.Width = Me.TextBoxUmin.Width
                t1.Height = Me.TextBoxUmin.Height
                t1.Text = p.sliderMinimum
                AddHandler t1.TextChanged, AddressOf Me.valueChanged
                Me.Panel1.Controls.Add(t1)
                t1 = New TextBox
                t1.Name = "maxvT" + i.ToString
                t1.Top = tp
                t1.Left = Me.TextBoxUmax.Left
                t1.Width = Me.TextBoxUmax.Width
                t1.Height = Me.TextBoxUmax.Height
                t1.Text = p.sliderMaximum
                AddHandler t1.TextChanged, AddressOf Me.valueChanged
                Me.Panel1.Controls.Add(t1)
                t1 = New TextBox
                t1.Name = "stepT" + i.ToString
                t1.Top = tp - 25
                t1.Left = Me.TextBoxUstep.Left
                t1.Width = Me.TextBoxUstep.Width
                t1.Height = Me.TextBoxUstep.Height
                t1.Text = p.sliderStep
                AddHandler t1.TextChanged, AddressOf Me.valueChanged
                Me.Panel1.Controls.Add(t1)
                Dim tr1 As NumericUpDown
                tr1 = New NumericUpDown
                tr1.Name = "vB" + i.ToString
                tr1.Top = tp
                tr1.Left = Me.TrackBarU.Left
                tr1.Width = Me.TrackBarU.Width
                tr1.Height = Me.TrackBarU.Height
                tr1.Minimum = p.sliderMinimum
                tr1.Maximum = p.sliderMaximum
                tr1.Increment = p.sliderStep
                tr1.Value = p.value
                tr1.DecimalPlaces = Me.TrackBarU.DecimalPlaces
                AddHandler tr1.ValueChanged, AddressOf Me.trackSkroll
                Me.Panel1.Controls.Add(tr1)
                Dim l1 As Label
                l1 = New Label
                l1.Name = "L" + i.ToString
                l1.Top = tp - 25
                l1.Left = Me.Label1.Left
                l1.Width = Me.Label1.Width
                l1.Height = Me.Label1.Height
                l1.Text = p.Name
                Me.Panel1.Controls.Add(l1)
                i += 1
                tp += 50
            Next
        Catch ex As Exception
            Try
                Dim U As ClassU = mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count)
                ' U min
                Me.TrackBarU.Increment = U.sliderStepUmin
                Me.TrackBarU.Minimum = U.sliderMinimumUmin
                Me.TrackBarU.Maximum = U.sliderMaximumUmin
                Me.TrackBarU.Value = U.minimalnoU
                Me.TextBoxUmax.Text = U.sliderMaximumUmin
                Me.TextBoxUmin.Text = U.sliderMinimumUmin
                Me.TextBoxUstep.Text = U.sliderStepUmin
                ' U max
                Me.TrackBarUm.Increment = U.sliderStepUmax
                Me.TrackBarUm.Minimum = U.sliderMinimumUmax
                Me.TrackBarUm.Maximum = U.sliderMaximumUmax
                Me.TrackBarUm.Value = U.maksimalnoU
                Me.TextBoxUmaxm.Text = U.sliderMaximumUmax
                Me.TextBoxUminm.Text = U.sliderMinimumUmax
                Me.TextBoxUstepm.Text = U.sliderStepUmax
                ' kontrole za parametre
                Me.Panel1.Controls.Clear()
                Dim p As ClassParametri
                Dim tp As Integer = Me.TextBoxUmin.Top
                Dim i As Integer = 0
                For Each p In U.prm
                    Dim t1 As TextBox
                    t1 = New TextBox
                    t1.Name = "minvT" + i.ToString
                    t1.Top = tp
                    t1.Left = Me.TextBoxUmin.Left
                    t1.Width = Me.TextBoxUmin.Width
                    t1.Height = Me.TextBoxUmin.Height
                    t1.Text = p.sliderMinimum
                    AddHandler t1.TextChanged, AddressOf Me.valueChanged
                    Me.Panel1.Controls.Add(t1)
                    t1 = New TextBox
                    t1.Name = "maxvT" + i.ToString
                    t1.Top = tp
                    t1.Left = Me.TextBoxUmax.Left
                    t1.Width = Me.TextBoxUmax.Width
                    t1.Height = Me.TextBoxUmax.Height
                    t1.Text = p.sliderMaximum
                    AddHandler t1.TextChanged, AddressOf Me.valueChanged
                    Me.Panel1.Controls.Add(t1)
                    t1 = New TextBox
                    t1.Name = "stepT" + i.ToString
                    t1.Top = tp - 25
                    t1.Left = Me.TextBoxUstep.Left
                    t1.Width = Me.TextBoxUstep.Width
                    t1.Height = Me.TextBoxUstep.Height
                    t1.Text = p.sliderStep
                    AddHandler t1.TextChanged, AddressOf Me.valueChanged
                    Me.Panel1.Controls.Add(t1)
                    Dim tr1 As NumericUpDown
                    tr1 = New NumericUpDown
                    tr1.Name = "vB" + i.ToString
                    tr1.Top = tp
                    tr1.Left = Me.TrackBarU.Left
                    tr1.Width = Me.TrackBarU.Width
                    tr1.Height = Me.TrackBarU.Height
                    tr1.Minimum = p.sliderMinimum
                    tr1.Maximum = p.sliderMaximum
                    tr1.Increment = p.sliderStep
                    tr1.Value = p.value
                    tr1.DecimalPlaces = Me.TrackBarU.DecimalPlaces
                    AddHandler tr1.ValueChanged, AddressOf Me.trackSkroll
                    Me.Panel1.Controls.Add(tr1)
                    Dim l1 As Label
                    l1 = New Label
                    l1.Name = "L" + i.ToString
                    l1.Top = tp - 25
                    l1.Left = Me.Label1.Left
                    l1.Width = Me.Label1.Width
                    l1.Height = Me.Label1.Height
                    l1.Text = p.Name
                    Me.Panel1.Controls.Add(l1)
                    i += 1
                    tp += 50
                Next
            Catch ex1 As Exception

            End Try
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TrackBarU_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBarU.ValueChanged
        Try
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).minimalnoU = Me.TrackBarU.Value
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).refreshBuffer()
            cf3D.Refresh()
        Catch ex As Exception
            Try
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).minimalnoU = Me.TrackBarU.Value
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).refreshBuffer()
                cf3D.Refresh()
            Catch ex1 As Exception

            End Try
        End Try
    End Sub
    Private Sub valueChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim tb As TextBox = sender
        Dim spam() As String = tb.Name.Split("T")
        Try
            Select Case spam(0)
                Case "minv"
                    mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).prm(Val(spam(1))).sliderMinimum = tb.Text
                    Dim tb1 As NumericUpDown = Me.Panel1.Controls("vB" + spam(1))
                    tb1.Minimum = tb.Text
                Case "maxv"
                    mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).prm(Val(spam(1))).sliderMaximum = tb.Text
                    Dim tb1 As NumericUpDown = Me.Panel1.Controls("vB" + spam(1))
                    tb1.Maximum = tb.Text
                Case "step"
                    mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).prm(Val(spam(1))).sliderStep = tb.Text
                    Dim tb1 As NumericUpDown = Me.Panel1.Controls("vB" + spam(1))
                    tb1.Increment = tb.Text
            End Select

        Catch ex As Exception
            Try
                Select Case spam(0)
                    Case "minv"
                        mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).prm(Val(spam(1))).sliderMinimum = tb.Text
                        Dim tb1 As NumericUpDown = Me.Panel1.Controls("vB" + spam(1))
                        tb1.Minimum = tb.Text
                    Case "maxv"
                        mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).prm(Val(spam(1))).sliderMaximum = tb.Text
                        Dim tb1 As NumericUpDown = Me.Panel1.Controls("vB" + spam(1))
                        tb1.Maximum = tb.Text
                    Case "step"
                        mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).prm(Val(spam(1))).sliderStep = tb.Text
                        Dim tb1 As NumericUpDown = Me.Panel1.Controls("vB" + spam(1))
                        tb1.Increment = tb.Text
                End Select
            Catch ex1 As Exception

            End Try
        End Try
    End Sub
    Private Sub trackSkroll(ByVal sender As Object, ByVal e As EventArgs)
        Dim tb As NumericUpDown = sender
        Dim spam() As String = tb.Name.Split("B")
        Try
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).prm(Val(spam(1))).value = tb.Value
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).refreshBuffer()
            cf3D.Refresh()
        Catch ex As Exception
            Try
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).prm(Val(spam(1))).value = tb.Value
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).refreshBuffer()
                cf3D.Refresh()
            Catch ex1 As Exception

            End Try
        End Try
    End Sub

    Private Sub TrackBarUm_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBarUm.ValueChanged
        Try
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).maksimalnoU = Me.TrackBarUm.Value
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).refreshBuffer()
            cf3D.Refresh()
        Catch ex As Exception
            Try
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).maksimalnoU = Me.TrackBarUm.Value
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).refreshBuffer()
                cf3D.Refresh()
            Catch ex1 As Exception

            End Try
        End Try
    End Sub

    Private Sub TrackBarV_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBarV.ValueChanged
        Try
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).minimalnoV = Me.TrackBarV.Value
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).refreshBuffer()
            cf3D.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TrackBarVm_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBarVm.ValueChanged
        Try
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).maksimalnoV = Me.TrackBarVm.Value
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).refreshBuffer()
            cf3D.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBoxUmin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxUmin.TextChanged
        Try
            Me.TrackBarU.Minimum = Me.TextBoxUmin.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderMinimumUmin = Me.TextBoxUmin.Text
        Catch ex As Exception
            Try
                Me.TrackBarU.Minimum = Me.TextBoxUmin.Text
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).sliderMinimumUmin = Me.TextBoxUmin.Text
            Catch ex1 As Exception

            End Try
        End Try
    End Sub

    Private Sub TextBoxUmax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxUmax.TextChanged
        Try
            Me.TrackBarU.Maximum = Me.TextBoxUmax.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderMaximumUmin = Me.TextBoxUmax.Text
        Catch ex As Exception
            Try
                Me.TrackBarU.Maximum = Me.TextBoxUmax.Text
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).sliderMaximumUmin = Me.TextBoxUmax.Text
            Catch ex1 As Exception

            End Try
        End Try
    End Sub

    Private Sub TextBoxUstep_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxUstep.TextChanged
        Try
            Me.TrackBarU.Increment = Me.TextBoxUstep.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderStepUmin = Me.TextBoxUstep.Text
        Catch ex As Exception
            Try
                Me.TrackBarU.Increment = Me.TextBoxUstep.Text
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).sliderStepUmin = Me.TextBoxUstep.Text
            Catch ex1 As Exception

            End Try
        End Try
    End Sub

    Private Sub TextBoxUminm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxUminm.TextChanged
        Try
            Me.TrackBarUm.Minimum = Me.TextBoxUminm.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderMinimumUmax = Me.TextBoxUminm.Text
        Catch ex As Exception
            Try
                Me.TrackBarUm.Minimum = Me.TextBoxUminm.Text
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).sliderMinimumUmax = Me.TextBoxUminm.Text
            Catch ex1 As Exception

            End Try
        End Try
    End Sub

    Private Sub TextBoxUmaxm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxUmaxm.TextChanged
        Try
            Me.TrackBarUm.Maximum = Me.TextBoxUmaxm.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderMaximumUmax = Me.TextBoxUmaxm.Text
        Catch ex As Exception
            Try
                Me.TrackBarUm.Maximum = Me.TextBoxUmaxm.Text
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).sliderMaximumUmax = Me.TextBoxUmaxm.Text
            Catch ex1 As Exception

            End Try
        End Try
    End Sub

    Private Sub TextBoxUstepm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxUstepm.TextChanged
        Try
            Me.TrackBarUm.Increment = Me.TextBoxUstepm.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderStepUmax = Me.TextBoxUstepm.Text
        Catch ex As Exception
            Try
                Me.TrackBarUm.Increment = Me.TextBoxUstepm.Text
                mf.Scena.UList(Me.ComboBoxPropertyObject.SelectedIndex - mf.Scena.UVList.Count).sliderStepUmax = Me.TextBoxUstepm.Text
            Catch ex1 As Exception

            End Try
        End Try
    End Sub

    Private Sub TextBoxVmin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxVmin.TextChanged
        Try
            Me.TrackBarV.Minimum = Me.TextBoxVmin.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderMinimumVmin = Me.TextBoxVmin.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBoxVmax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxVmax.TextChanged
        Try
            Me.TrackBarV.Maximum = Me.TextBoxVmax.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderMaximumVmin = Me.TextBoxVmax.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBoxVstep_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxVstep.TextChanged
        Try
            Me.TrackBarV.Increment = Me.TextBoxVstep.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderStepVmin = Me.TextBoxVstep.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBoxVminm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxVminm.TextChanged
        Try
            Me.TrackBarVm.Minimum = Me.TextBoxVminm.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderMinimumVmax = Me.TextBoxVminm.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBoxVmaxm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxVmaxm.TextChanged
        Try
            Me.TrackBarVm.Maximum = Me.TextBoxVmaxm.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderMaximumVmax = Me.TextBoxVmaxm.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBoxVstepm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxVstepm.TextChanged
        Try
            Me.TrackBarVm.Increment = Me.TextBoxVstepm.Text
            mf.Scena.UVList(Me.ComboBoxPropertyObject.SelectedIndex).sliderStepVmax = Me.TextBoxVstepm.Text
        Catch ex As Exception

        End Try
    End Sub
End Class