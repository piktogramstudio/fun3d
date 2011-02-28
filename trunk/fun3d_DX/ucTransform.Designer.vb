<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucTransform
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TBarRX = New System.Windows.Forms.TrackBar()
        Me.LRX = New System.Windows.Forms.Label()
        Me.LRY = New System.Windows.Forms.Label()
        Me.TBarRY = New System.Windows.Forms.TrackBar()
        Me.TBarRZ = New System.Windows.Forms.TrackBar()
        Me.LRZ = New System.Windows.Forms.Label()
        Me.TBRX = New System.Windows.Forms.TextBox()
        Me.TBRY = New System.Windows.Forms.TextBox()
        Me.TBRZ = New System.Windows.Forms.TextBox()
        Me.FLPRX = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.LTX = New System.Windows.Forms.Label()
        Me.TBTX = New System.Windows.Forms.TextBox()
        Me.LTY = New System.Windows.Forms.Label()
        Me.TBTY = New System.Windows.Forms.TextBox()
        Me.LTZ = New System.Windows.Forms.Label()
        Me.TBTZ = New System.Windows.Forms.TextBox()
        Me.LSX = New System.Windows.Forms.Label()
        Me.TBSX = New System.Windows.Forms.TextBox()
        Me.LSY = New System.Windows.Forms.Label()
        Me.TBSY = New System.Windows.Forms.TextBox()
        Me.LSZ = New System.Windows.Forms.Label()
        Me.TBSZ = New System.Windows.Forms.TextBox()
        CType(Me.TBarRX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TBarRY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TBarRZ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FLPRX.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.FlowLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TBarRX
        '
        Me.TBarRX.Dock = System.Windows.Forms.DockStyle.Top
        Me.TBarRX.LargeChange = 15
        Me.TBarRX.Location = New System.Drawing.Point(5, 35)
        Me.TBarRX.Maximum = 360
        Me.TBarRX.Name = "TBarRX"
        Me.TBarRX.Size = New System.Drawing.Size(204, 45)
        Me.TBarRX.TabIndex = 0
        Me.TBarRX.TickFrequency = 30
        '
        'LRX
        '
        Me.LRX.Location = New System.Drawing.Point(3, 0)
        Me.LRX.Name = "LRX"
        Me.LRX.Size = New System.Drawing.Size(117, 23)
        Me.LRX.TabIndex = 1
        Me.LRX.Text = "Rotation around X axis:"
        Me.LRX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LRY
        '
        Me.LRY.Location = New System.Drawing.Point(3, 0)
        Me.LRY.Name = "LRY"
        Me.LRY.Size = New System.Drawing.Size(117, 23)
        Me.LRY.TabIndex = 2
        Me.LRY.Text = "Rotation around Y axis:"
        Me.LRY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TBarRY
        '
        Me.TBarRY.Dock = System.Windows.Forms.DockStyle.Top
        Me.TBarRY.LargeChange = 15
        Me.TBarRY.Location = New System.Drawing.Point(5, 110)
        Me.TBarRY.Maximum = 360
        Me.TBarRY.Name = "TBarRY"
        Me.TBarRY.Size = New System.Drawing.Size(204, 45)
        Me.TBarRY.TabIndex = 3
        Me.TBarRY.TickFrequency = 30
        '
        'TBarRZ
        '
        Me.TBarRZ.Dock = System.Windows.Forms.DockStyle.Top
        Me.TBarRZ.LargeChange = 15
        Me.TBarRZ.Location = New System.Drawing.Point(5, 185)
        Me.TBarRZ.Maximum = 360
        Me.TBarRZ.Name = "TBarRZ"
        Me.TBarRZ.Size = New System.Drawing.Size(204, 45)
        Me.TBarRZ.TabIndex = 5
        Me.TBarRZ.TickFrequency = 30
        '
        'LRZ
        '
        Me.LRZ.Location = New System.Drawing.Point(3, 0)
        Me.LRZ.Name = "LRZ"
        Me.LRZ.Size = New System.Drawing.Size(117, 23)
        Me.LRZ.TabIndex = 4
        Me.LRZ.Text = "Rotation around Z axis:"
        Me.LRZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TBRX
        '
        Me.TBRX.Location = New System.Drawing.Point(126, 3)
        Me.TBRX.Name = "TBRX"
        Me.TBRX.Size = New System.Drawing.Size(62, 20)
        Me.TBRX.TabIndex = 6
        '
        'TBRY
        '
        Me.TBRY.Location = New System.Drawing.Point(126, 3)
        Me.TBRY.Name = "TBRY"
        Me.TBRY.Size = New System.Drawing.Size(62, 20)
        Me.TBRY.TabIndex = 7
        '
        'TBRZ
        '
        Me.TBRZ.Location = New System.Drawing.Point(126, 3)
        Me.TBRZ.Name = "TBRZ"
        Me.TBRZ.Size = New System.Drawing.Size(62, 20)
        Me.TBRZ.TabIndex = 8
        '
        'FLPRX
        '
        Me.FLPRX.Controls.Add(Me.LRX)
        Me.FLPRX.Controls.Add(Me.TBRX)
        Me.FLPRX.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLPRX.Location = New System.Drawing.Point(5, 5)
        Me.FLPRX.Name = "FLPRX"
        Me.FLPRX.Size = New System.Drawing.Size(204, 30)
        Me.FLPRX.TabIndex = 9
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.LRY)
        Me.FlowLayoutPanel1.Controls.Add(Me.TBRY)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(5, 80)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(204, 30)
        Me.FlowLayoutPanel1.TabIndex = 10
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.LRZ)
        Me.FlowLayoutPanel2.Controls.Add(Me.TBRZ)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(5, 155)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(204, 30)
        Me.FlowLayoutPanel2.TabIndex = 11
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.Controls.Add(Me.LTX)
        Me.FlowLayoutPanel3.Controls.Add(Me.TBTX)
        Me.FlowLayoutPanel3.Controls.Add(Me.LTY)
        Me.FlowLayoutPanel3.Controls.Add(Me.TBTY)
        Me.FlowLayoutPanel3.Controls.Add(Me.LTZ)
        Me.FlowLayoutPanel3.Controls.Add(Me.TBTZ)
        Me.FlowLayoutPanel3.Controls.Add(Me.LSX)
        Me.FlowLayoutPanel3.Controls.Add(Me.TBSX)
        Me.FlowLayoutPanel3.Controls.Add(Me.LSY)
        Me.FlowLayoutPanel3.Controls.Add(Me.TBSY)
        Me.FlowLayoutPanel3.Controls.Add(Me.LSZ)
        Me.FlowLayoutPanel3.Controls.Add(Me.TBSZ)
        Me.FlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(5, 230)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(204, 162)
        Me.FlowLayoutPanel3.TabIndex = 12
        '
        'LTX
        '
        Me.LTX.Location = New System.Drawing.Point(3, 0)
        Me.LTX.Name = "LTX"
        Me.LTX.Size = New System.Drawing.Size(117, 23)
        Me.LTX.TabIndex = 0
        Me.LTX.Text = "Translate along X axis:"
        Me.LTX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TBTX
        '
        Me.TBTX.Location = New System.Drawing.Point(126, 3)
        Me.TBTX.Name = "TBTX"
        Me.TBTX.Size = New System.Drawing.Size(65, 20)
        Me.TBTX.TabIndex = 1
        '
        'LTY
        '
        Me.LTY.Location = New System.Drawing.Point(3, 26)
        Me.LTY.Name = "LTY"
        Me.LTY.Size = New System.Drawing.Size(117, 23)
        Me.LTY.TabIndex = 2
        Me.LTY.Text = "Translate along Y axis:"
        Me.LTY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TBTY
        '
        Me.TBTY.Location = New System.Drawing.Point(126, 29)
        Me.TBTY.Name = "TBTY"
        Me.TBTY.Size = New System.Drawing.Size(65, 20)
        Me.TBTY.TabIndex = 3
        '
        'LTZ
        '
        Me.LTZ.Location = New System.Drawing.Point(3, 52)
        Me.LTZ.Name = "LTZ"
        Me.LTZ.Size = New System.Drawing.Size(117, 23)
        Me.LTZ.TabIndex = 4
        Me.LTZ.Text = "Translate along Z axis:"
        Me.LTZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TBTZ
        '
        Me.TBTZ.Location = New System.Drawing.Point(126, 55)
        Me.TBTZ.Name = "TBTZ"
        Me.TBTZ.Size = New System.Drawing.Size(65, 20)
        Me.TBTZ.TabIndex = 5
        '
        'LSX
        '
        Me.LSX.Location = New System.Drawing.Point(3, 78)
        Me.LSX.Name = "LSX"
        Me.LSX.Size = New System.Drawing.Size(117, 23)
        Me.LSX.TabIndex = 6
        Me.LSX.Text = "Scale along X axis:"
        Me.LSX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TBSX
        '
        Me.TBSX.Location = New System.Drawing.Point(126, 81)
        Me.TBSX.Name = "TBSX"
        Me.TBSX.Size = New System.Drawing.Size(65, 20)
        Me.TBSX.TabIndex = 7
        '
        'LSY
        '
        Me.LSY.Location = New System.Drawing.Point(3, 104)
        Me.LSY.Name = "LSY"
        Me.LSY.Size = New System.Drawing.Size(117, 23)
        Me.LSY.TabIndex = 8
        Me.LSY.Text = "Scale along Y axis:"
        Me.LSY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TBSY
        '
        Me.TBSY.Location = New System.Drawing.Point(126, 107)
        Me.TBSY.Name = "TBSY"
        Me.TBSY.Size = New System.Drawing.Size(65, 20)
        Me.TBSY.TabIndex = 9
        '
        'LSZ
        '
        Me.LSZ.Location = New System.Drawing.Point(3, 130)
        Me.LSZ.Name = "LSZ"
        Me.LSZ.Size = New System.Drawing.Size(117, 23)
        Me.LSZ.TabIndex = 10
        Me.LSZ.Text = "Scale along Z axis:"
        Me.LSZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TBSZ
        '
        Me.TBSZ.Location = New System.Drawing.Point(126, 133)
        Me.TBSZ.Name = "TBSZ"
        Me.TBSZ.Size = New System.Drawing.Size(65, 20)
        Me.TBSZ.TabIndex = 11
        '
        'ucTransform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.FlowLayoutPanel3)
        Me.Controls.Add(Me.TBarRZ)
        Me.Controls.Add(Me.FlowLayoutPanel2)
        Me.Controls.Add(Me.TBarRY)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.TBarRX)
        Me.Controls.Add(Me.FLPRX)
        Me.Name = "ucTransform"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Size = New System.Drawing.Size(214, 397)
        CType(Me.TBarRX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TBarRY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TBarRZ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FLPRX.ResumeLayout(False)
        Me.FLPRX.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.FlowLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TBarRX As System.Windows.Forms.TrackBar
    Friend WithEvents LRX As System.Windows.Forms.Label
    Friend WithEvents LRY As System.Windows.Forms.Label
    Friend WithEvents TBarRY As System.Windows.Forms.TrackBar
    Friend WithEvents TBarRZ As System.Windows.Forms.TrackBar
    Friend WithEvents LRZ As System.Windows.Forms.Label
    Friend WithEvents TBRX As System.Windows.Forms.TextBox
    Friend WithEvents TBRY As System.Windows.Forms.TextBox
    Friend WithEvents TBRZ As System.Windows.Forms.TextBox
    Friend WithEvents FLPRX As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel3 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents LTX As System.Windows.Forms.Label
    Friend WithEvents TBTX As System.Windows.Forms.TextBox
    Friend WithEvents LTY As System.Windows.Forms.Label
    Friend WithEvents TBTY As System.Windows.Forms.TextBox
    Friend WithEvents LTZ As System.Windows.Forms.Label
    Friend WithEvents TBTZ As System.Windows.Forms.TextBox
    Friend WithEvents LSX As System.Windows.Forms.Label
    Friend WithEvents TBSX As System.Windows.Forms.TextBox
    Friend WithEvents LSY As System.Windows.Forms.Label
    Friend WithEvents TBSY As System.Windows.Forms.TextBox
    Friend WithEvents LSZ As System.Windows.Forms.Label
    Friend WithEvents TBSZ As System.Windows.Forms.TextBox

End Class
