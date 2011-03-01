Public Class ucParameters
    Public params As List(Of ClassParametri)
    Public context As Object
    Public WithEvents b As Binding
    Private Sub ucParameters_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim p As ClassParametri
        Dim l As Label
        Dim nud As NumericUpDown
        Me.FLPParams.Controls.Clear()
        For Each p In params
            l = New Label()
            l.AutoSize = True
            l.Text = p.Name
            Me.FLPParams.Controls.Add(l)
            nud = New NumericUpDown
            nud.Minimum = p.sliderMinimum
            nud.Maximum = p.sliderMaximum
            nud.Increment = p.sliderStep
            nud.Value = p.value
            nud.Width = 50
            b = New Binding("Value", p, "value")
            b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            nud.DataBindings.Add(b)
            Me.FLPParams.Controls.Add(nud)
        Next
    End Sub

    Private Sub b_Format(ByVal sender As Object, ByVal e As System.Windows.Forms.ConvertEventArgs) Handles b.Format
        Me.context.refreshbuffer()
        cf3D.Invalidate()
    End Sub
End Class
