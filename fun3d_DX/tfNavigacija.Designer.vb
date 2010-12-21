<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tfNavigacija
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.NUDzcam = New System.Windows.Forms.NumericUpDown
        Me.NUDycam = New System.Windows.Forms.NumericUpDown
        Me.NUDugaoZ = New System.Windows.Forms.NumericUpDown
        Me.NUDugaoY = New System.Windows.Forms.NumericUpDown
        Me.NUDugaoX = New System.Windows.Forms.NumericUpDown
        Me.NUDxcam = New System.Windows.Forms.NumericUpDown
        CType(Me.NUDzcam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDycam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDugaoZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDugaoY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDugaoX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDxcam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NUDzcam
        '
        Me.NUDzcam.DecimalPlaces = 1
        Me.NUDzcam.Location = New System.Drawing.Point(111, 3)
        Me.NUDzcam.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NUDzcam.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
        Me.NUDzcam.Name = "NUDzcam"
        Me.NUDzcam.Size = New System.Drawing.Size(48, 20)
        Me.NUDzcam.TabIndex = 5
        Me.NUDzcam.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'NUDycam
        '
        Me.NUDycam.DecimalPlaces = 1
        Me.NUDycam.Location = New System.Drawing.Point(57, 3)
        Me.NUDycam.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NUDycam.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
        Me.NUDycam.Name = "NUDycam"
        Me.NUDycam.Size = New System.Drawing.Size(48, 20)
        Me.NUDycam.TabIndex = 6
        '
        'NUDugaoZ
        '
        Me.NUDugaoZ.DecimalPlaces = 1
        Me.NUDugaoZ.Location = New System.Drawing.Point(282, 3)
        Me.NUDugaoZ.Maximum = New Decimal(New Integer() {359, 0, 0, 0})
        Me.NUDugaoZ.Minimum = New Decimal(New Integer() {359, 0, 0, -2147483648})
        Me.NUDugaoZ.Name = "NUDugaoZ"
        Me.NUDugaoZ.Size = New System.Drawing.Size(48, 20)
        Me.NUDugaoZ.TabIndex = 7
        '
        'NUDugaoY
        '
        Me.NUDugaoY.DecimalPlaces = 1
        Me.NUDugaoY.Location = New System.Drawing.Point(228, 3)
        Me.NUDugaoY.Maximum = New Decimal(New Integer() {359, 0, 0, 0})
        Me.NUDugaoY.Minimum = New Decimal(New Integer() {359, 0, 0, -2147483648})
        Me.NUDugaoY.Name = "NUDugaoY"
        Me.NUDugaoY.Size = New System.Drawing.Size(48, 20)
        Me.NUDugaoY.TabIndex = 2
        '
        'NUDugaoX
        '
        Me.NUDugaoX.DecimalPlaces = 1
        Me.NUDugaoX.Location = New System.Drawing.Point(174, 3)
        Me.NUDugaoX.Maximum = New Decimal(New Integer() {359, 0, 0, 0})
        Me.NUDugaoX.Minimum = New Decimal(New Integer() {359, 0, 0, -2147483648})
        Me.NUDugaoX.Name = "NUDugaoX"
        Me.NUDugaoX.Size = New System.Drawing.Size(48, 20)
        Me.NUDugaoX.TabIndex = 3
        '
        'NUDxcam
        '
        Me.NUDxcam.DecimalPlaces = 1
        Me.NUDxcam.Location = New System.Drawing.Point(3, 3)
        Me.NUDxcam.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NUDxcam.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
        Me.NUDxcam.Name = "NUDxcam"
        Me.NUDxcam.Size = New System.Drawing.Size(48, 20)
        Me.NUDxcam.TabIndex = 4
        '
        'tfNavigacija
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(334, 29)
        Me.Controls.Add(Me.NUDzcam)
        Me.Controls.Add(Me.NUDycam)
        Me.Controls.Add(Me.NUDugaoZ)
        Me.Controls.Add(Me.NUDugaoY)
        Me.Controls.Add(Me.NUDugaoX)
        Me.Controls.Add(Me.NUDxcam)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "tfNavigacija"
        Me.ShowInTaskbar = False
        Me.Text = "Navigate"
        CType(Me.NUDzcam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDycam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDugaoZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDugaoY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDugaoX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDxcam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NUDzcam As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDycam As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDugaoZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDugaoY As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDugaoX As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDxcam As System.Windows.Forms.NumericUpDown
End Class
