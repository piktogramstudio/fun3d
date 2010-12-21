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
        Me.LSelectedObjName = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'PropertyGridUV
        '
        Me.PropertyGridUV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGridUV.HelpBackColor = System.Drawing.Color.Gainsboro
        Me.PropertyGridUV.LineColor = System.Drawing.Color.Gainsboro
        Me.PropertyGridUV.Location = New System.Drawing.Point(0, 23)
        Me.PropertyGridUV.Name = "PropertyGridUV"
        Me.PropertyGridUV.Size = New System.Drawing.Size(267, 361)
        Me.PropertyGridUV.TabIndex = 5
        Me.PropertyGridUV.ToolbarVisible = False
        '
        'LSelectedObjName
        '
        Me.LSelectedObjName.BackColor = System.Drawing.Color.Gainsboro
        Me.LSelectedObjName.Dock = System.Windows.Forms.DockStyle.Top
        Me.LSelectedObjName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.LSelectedObjName.Location = New System.Drawing.Point(0, 0)
        Me.LSelectedObjName.Name = "LSelectedObjName"
        Me.LSelectedObjName.Size = New System.Drawing.Size(267, 23)
        Me.LSelectedObjName.TabIndex = 6
        Me.LSelectedObjName.Text = "Selected object"
        Me.LSelectedObjName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tfOsobine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(267, 384)
        Me.Controls.Add(Me.PropertyGridUV)
        Me.Controls.Add(Me.LSelectedObjName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "tfOsobine"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Properties"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PropertyGridUV As System.Windows.Forms.PropertyGrid
    Friend WithEvents LSelectedObjName As System.Windows.Forms.Label
End Class
