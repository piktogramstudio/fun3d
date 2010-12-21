<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tfListProperties
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
        Me.TBLP = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'TBLP
        '
        Me.TBLP.AcceptsReturn = True
        Me.TBLP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TBLP.Location = New System.Drawing.Point(0, 0)
        Me.TBLP.Multiline = True
        Me.TBLP.Name = "TBLP"
        Me.TBLP.Size = New System.Drawing.Size(685, 475)
        Me.TBLP.TabIndex = 0
        '
        'tfListProperties
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 475)
        Me.Controls.Add(Me.TBLP)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "tfListProperties"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "tfListProperties"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TBLP As System.Windows.Forms.TextBox
End Class
