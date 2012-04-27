Public Class tfUCurveEq
    Dim sizeSwitch As Boolean = True
    Public u As ClassU
    Private Sub tfUCurveEq_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            Dim b As Binding
            b = New Binding("Text", mf.Scena.SelectedObject, "funX")
            b.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            Me.tbXt.DataBindings.Clear()
            Me.tbXt.DataBindings.Add(b)
            b = New Binding("Text", mf.Scena.SelectedObject, "funY")
            b.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            Me.tbYt.DataBindings.Clear()
            Me.tbYt.DataBindings.Add(b)
            b = New Binding("Text", mf.Scena.SelectedObject, "funZ")
            b.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            b.FormattingEnabled = True
            Me.tbZt.DataBindings.Clear()
            Me.tbZt.DataBindings.Add(b)
            b = New Binding("Text", mf.Scena.SelectedObject, "Umin")
            b.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            b.FormattingEnabled = True
            Me.tbMint.DataBindings.Clear()
            Me.tbMint.DataBindings.Add(b)
            b = New Binding("Text", mf.Scena.SelectedObject, "Umax")
            b.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            b.FormattingEnabled = True
            Me.tbMaxt.DataBindings.Clear()
            Me.tbMaxt.DataBindings.Add(b)
            AddHandler b.BindingComplete, AddressOf bc
            u = CType(mf.Scena.SelectedObject, ClassU)
            bsU.DataSource = u
            AddHandler bsU.CurrentItemChanged, AddressOf bc
        End If
    End Sub

    Public Sub bc(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            CType(mf.Scena.SelectedObject, ClassU).refreshBuffer()
            cf3D.Refresh()
            mf.addUndoData("Property value changed")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If sizeSwitch Then
            Me.Width = 50
            Me.Height = 50
            Me.Opacity = 0.3
        Else
            Me.Width = 546
            Me.Height = 337
            Me.Opacity = 0.9
        End If
        sizeSwitch = Not sizeSwitch
    End Sub

    Private Sub dgwParameters_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgwParameters.CellContentClick
        If e.RowIndex > -1 And Not Me.dgwParameters.Rows(e.RowIndex).IsNewRow Then
            Select Case Me.dgwParameters.Columns(e.ColumnIndex).Name
                Case plus.Name
                    Me.dgwParameters.Rows(e.RowIndex).Cells(ValueDataGridViewTextBoxColumn.Name).Value = CSng(Me.dgwParameters.Rows(e.RowIndex).Cells(ValueDataGridViewTextBoxColumn.Name).Value) + CSng(Me.dgwParameters.Rows(e.RowIndex).Cells(SliderStepDataGridViewTextBoxColumn.Name).Value)
                Case minus.Name
                    Me.dgwParameters.Rows(e.RowIndex).Cells(ValueDataGridViewTextBoxColumn.Name).Value = CSng(Me.dgwParameters.Rows(e.RowIndex).Cells(ValueDataGridViewTextBoxColumn.Name).Value) - CSng(Me.dgwParameters.Rows(e.RowIndex).Cells(SliderStepDataGridViewTextBoxColumn.Name).Value)
            End Select
        End If
    End Sub
End Class