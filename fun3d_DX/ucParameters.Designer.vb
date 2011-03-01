<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucParameters
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
        Me.FLPParams = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'FLPParams
        '
        Me.FLPParams.AutoScroll = True
        Me.FLPParams.BackColor = System.Drawing.Color.White
        Me.FLPParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FLPParams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLPParams.Location = New System.Drawing.Point(0, 0)
        Me.FLPParams.Name = "FLPParams"
        Me.FLPParams.Padding = New System.Windows.Forms.Padding(5)
        Me.FLPParams.Size = New System.Drawing.Size(108, 103)
        Me.FLPParams.TabIndex = 0
        '
        'ucParameters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.FLPParams)
        Me.Name = "ucParameters"
        Me.Size = New System.Drawing.Size(108, 103)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FLPParams As System.Windows.Forms.FlowLayoutPanel

End Class
