<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tfOsobine
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
        Me.PropertyGridUV = New System.Windows.Forms.PropertyGrid
        Me.SuspendLayout()
        '
        'PropertyGridUV
        '
        Me.PropertyGridUV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGridUV.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGridUV.Name = "PropertyGridUV"
        Me.PropertyGridUV.Size = New System.Drawing.Size(267, 384)
        Me.PropertyGridUV.TabIndex = 5
        '
        'tfOsobine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(267, 384)
        Me.Controls.Add(Me.PropertyGridUV)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "tfOsobine"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "tfOsobine"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PropertyGridUV As System.Windows.Forms.PropertyGrid
End Class
