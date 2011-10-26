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
            l.AutoSize = False
            l.Width = 50
            l.TextAlign = ContentAlignment.MiddleLeft
            l.Text = p.Name
            Me.FLPParams.Controls.Add(l)
            nud = New NumericUpDown
            nud.Minimum = CDec(p.sliderMinimum)
            nud.Maximum = CDec(p.sliderMaximum)
            nud.Increment = CDec(p.sliderStep)
            nud.Value = CDec(p.value)
            nud.Width = 45
            nud.DecimalPlaces = 2
            b = New Binding("Value", p, "value")
            b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            nud.DataBindings.Add(b)
            AddHandler b.Format, AddressOf b_Format
            Me.FLPParams.Controls.Add(nud)
        Next
    End Sub

    Private Sub b_Format(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.context.refreshbuffer()
        cf3D.Invalidate()
    End Sub
End Class
