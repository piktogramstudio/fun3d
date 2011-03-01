<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucEquation
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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PParameters = New System.Windows.Forms.Panel()
        Me.DGWParameters = New System.Windows.Forms.DataGridView()
        Me.ParameterName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Min = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Max = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SliderStep = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAddParam = New System.Windows.Forms.Button()
        Me.PParameters.SuspendLayout()
        CType(Me.DGWParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(5, 5)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(155, 136)
        Me.ListBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(5, 141)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.Label1.Size = New System.Drawing.Size(60, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Parameters"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TextBox1.Location = New System.Drawing.Point(5, 164)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(400, 129)
        Me.TextBox1.TabIndex = 2
        '
        'PParameters
        '
        Me.PParameters.Controls.Add(Me.DGWParameters)
        Me.PParameters.Controls.Add(Me.BAddParam)
        Me.PParameters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PParameters.Location = New System.Drawing.Point(160, 5)
        Me.PParameters.Name = "PParameters"
        Me.PParameters.Size = New System.Drawing.Size(245, 136)
        Me.PParameters.TabIndex = 3
        '
        'DGWParameters
        '
        Me.DGWParameters.AllowUserToAddRows = False
        Me.DGWParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGWParameters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ParameterName, Me.Value, Me.Min, Me.Max, Me.SliderStep})
        Me.DGWParameters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGWParameters.Location = New System.Drawing.Point(0, 0)
        Me.DGWParameters.Name = "DGWParameters"
        Me.DGWParameters.Size = New System.Drawing.Size(245, 113)
        Me.DGWParameters.TabIndex = 0
        '
        'ParameterName
        '
        Me.ParameterName.HeaderText = "Name"
        Me.ParameterName.Name = "ParameterName"
        Me.ParameterName.Width = 60
        '
        'Value
        '
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        Me.Value.Width = 30
        '
        'Min
        '
        Me.Min.HeaderText = "Min"
        Me.Min.Name = "Min"
        Me.Min.Width = 30
        '
        'Max
        '
        Me.Max.HeaderText = "Max"
        Me.Max.Name = "Max"
        Me.Max.Width = 30
        '
        'SliderStep
        '
        Me.SliderStep.HeaderText = "Step"
        Me.SliderStep.Name = "SliderStep"
        Me.SliderStep.Width = 30
        '
        'BAddParam
        '
        Me.BAddParam.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BAddParam.Location = New System.Drawing.Point(0, 113)
        Me.BAddParam.Name = "BAddParam"
        Me.BAddParam.Size = New System.Drawing.Size(245, 23)
        Me.BAddParam.TabIndex = 1
        Me.BAddParam.Text = "Add parameter"
        Me.BAddParam.UseVisualStyleBackColor = True
        '
        'ucEquation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.PParameters)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "ucEquation"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Size = New System.Drawing.Size(410, 298)
        Me.PParameters.ResumeLayout(False)
        CType(Me.DGWParameters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents PParameters As System.Windows.Forms.Panel
    Friend WithEvents DGWParameters As System.Windows.Forms.DataGridView
    Friend WithEvents ParameterName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Min As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Max As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SliderStep As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAddParam As System.Windows.Forms.Button

End Class
