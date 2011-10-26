<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tfTransform
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.pMain = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'pMain
        '
        Me.pMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pMain.Location = New System.Drawing.Point(0, 0)
        Me.pMain.Name = "pMain"
        Me.pMain.Size = New System.Drawing.Size(214, 396)
        Me.pMain.TabIndex = 0
        '
        'tfTransform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(214, 396)
        Me.Controls.Add(Me.pMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "tfTransform"
        Me.Text = "tfTransform"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pMain As System.Windows.Forms.Panel
End Class
