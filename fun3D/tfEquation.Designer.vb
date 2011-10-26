<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tfEquation
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
        Me.UcEquation1 = New fun3D.ucEquation()
        Me.SuspendLayout()
        '
        'UcEquation1
        '
        Me.UcEquation1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcEquation1.Location = New System.Drawing.Point(0, 0)
        Me.UcEquation1.Name = "UcEquation1"
        Me.UcEquation1.Size = New System.Drawing.Size(369, 361)
        Me.UcEquation1.TabIndex = 0
        '
        'tfEquation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(369, 361)
        Me.Controls.Add(Me.UcEquation1)
        Me.Name = "tfEquation"
        Me.Text = "tfEquation"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcEquation1 As fun3D.ucEquation
End Class
