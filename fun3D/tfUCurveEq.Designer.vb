<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tfUCurveEq
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tbXt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbYt = New System.Windows.Forms.TextBox()
        Me.tbZt = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgwParameters = New System.Windows.Forms.DataGridView()
        Me.tbMaxt = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbMint = New System.Windows.Forms.TextBox()
        Me.bsU = New System.Windows.Forms.BindingSource(Me.components)
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SliderMinimumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SliderMaximumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SliderStepDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.plus = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.minus = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.dgwParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bsU, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbXt
        '
        Me.tbXt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbXt.Location = New System.Drawing.Point(68, 61)
        Me.tbXt.Margin = New System.Windows.Forms.Padding(4)
        Me.tbXt.Multiline = True
        Me.tbXt.Name = "tbXt"
        Me.tbXt.Size = New System.Drawing.Size(452, 39)
        Me.tbXt.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 63)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "X(t) = "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 108)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Y(t) = "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 153)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 17)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Z(t) = "
        '
        'tbYt
        '
        Me.tbYt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbYt.Location = New System.Drawing.Point(68, 106)
        Me.tbYt.Margin = New System.Windows.Forms.Padding(4)
        Me.tbYt.Multiline = True
        Me.tbYt.Name = "tbYt"
        Me.tbYt.Size = New System.Drawing.Size(452, 39)
        Me.tbYt.TabIndex = 0
        '
        'tbZt
        '
        Me.tbZt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbZt.Location = New System.Drawing.Point(68, 151)
        Me.tbZt.Margin = New System.Windows.Forms.Padding(4)
        Me.tbZt.Multiline = True
        Me.tbZt.Name = "tbZt"
        Me.tbZt.Size = New System.Drawing.Size(452, 39)
        Me.tbZt.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dgwParameters)
        Me.Panel1.Controls.Add(Me.tbMaxt)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.tbMint)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.tbZt)
        Me.Panel1.Controls.Add(Me.tbYt)
        Me.Panel1.Controls.Add(Me.tbXt)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(546, 337)
        Me.Panel1.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = Global.fun3D.My.Resources.Resources.chart_curve_edit_icon
        Me.Button1.Location = New System.Drawing.Point(4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(40, 40)
        Me.Button1.TabIndex = 2
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(0, 201)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(544, 25)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Parameters:"
        '
        'dgwParameters
        '
        Me.dgwParameters.AutoGenerateColumns = False
        Me.dgwParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwParameters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameDataGridViewTextBoxColumn, Me.ValueDataGridViewTextBoxColumn, Me.SliderMinimumDataGridViewTextBoxColumn, Me.SliderMaximumDataGridViewTextBoxColumn, Me.SliderStepDataGridViewTextBoxColumn, Me.plus, Me.minus})
        Me.dgwParameters.DataSource = Me.bsU
        Me.dgwParameters.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgwParameters.Location = New System.Drawing.Point(0, 226)
        Me.dgwParameters.Name = "dgwParameters"
        Me.dgwParameters.Size = New System.Drawing.Size(544, 109)
        Me.dgwParameters.TabIndex = 5
        '
        'tbMaxt
        '
        Me.tbMaxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbMaxt.Location = New System.Drawing.Point(209, 21)
        Me.tbMaxt.Name = "tbMaxt"
        Me.tbMaxt.Size = New System.Drawing.Size(85, 23)
        Me.tbMaxt.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(159, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 17)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "<  t  <"
        '
        'tbMint
        '
        Me.tbMint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbMint.Location = New System.Drawing.Point(68, 21)
        Me.tbMint.Name = "tbMint"
        Me.tbMint.Size = New System.Drawing.Size(85, 23)
        Me.tbMint.TabIndex = 4
        '
        'bsU
        '
        Me.bsU.DataMember = "Parameters"
        Me.bsU.DataSource = GetType(fun3D.ClassU)
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        '
        'ValueDataGridViewTextBoxColumn
        '
        Me.ValueDataGridViewTextBoxColumn.DataPropertyName = "value"
        Me.ValueDataGridViewTextBoxColumn.HeaderText = "value"
        Me.ValueDataGridViewTextBoxColumn.Name = "ValueDataGridViewTextBoxColumn"
        Me.ValueDataGridViewTextBoxColumn.Width = 60
        '
        'SliderMinimumDataGridViewTextBoxColumn
        '
        Me.SliderMinimumDataGridViewTextBoxColumn.DataPropertyName = "sliderMinimum"
        Me.SliderMinimumDataGridViewTextBoxColumn.HeaderText = "Min"
        Me.SliderMinimumDataGridViewTextBoxColumn.Name = "SliderMinimumDataGridViewTextBoxColumn"
        Me.SliderMinimumDataGridViewTextBoxColumn.Width = 60
        '
        'SliderMaximumDataGridViewTextBoxColumn
        '
        Me.SliderMaximumDataGridViewTextBoxColumn.DataPropertyName = "sliderMaximum"
        Me.SliderMaximumDataGridViewTextBoxColumn.HeaderText = "Max"
        Me.SliderMaximumDataGridViewTextBoxColumn.Name = "SliderMaximumDataGridViewTextBoxColumn"
        Me.SliderMaximumDataGridViewTextBoxColumn.Width = 60
        '
        'SliderStepDataGridViewTextBoxColumn
        '
        Me.SliderStepDataGridViewTextBoxColumn.DataPropertyName = "sliderStep"
        Me.SliderStepDataGridViewTextBoxColumn.HeaderText = "Step"
        Me.SliderStepDataGridViewTextBoxColumn.Name = "SliderStepDataGridViewTextBoxColumn"
        Me.SliderStepDataGridViewTextBoxColumn.Width = 60
        '
        'plus
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle1.NullValue = "+"
        Me.plus.DefaultCellStyle = DataGridViewCellStyle1
        Me.plus.HeaderText = "plus"
        Me.plus.Name = "plus"
        Me.plus.Text = "+"
        Me.plus.Width = 60
        '
        'minus
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        DataGridViewCellStyle2.NullValue = "-"
        Me.minus.DefaultCellStyle = DataGridViewCellStyle2
        Me.minus.HeaderText = "minus"
        Me.minus.Name = "minus"
        Me.minus.Text = "-"
        Me.minus.Width = 60
        '
        'tfUCurveEq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 337)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "tfUCurveEq"
        Me.Opacity = 0.95R
        Me.Text = "tfUCurveEq"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgwParameters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bsU, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbXt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbYt As System.Windows.Forms.TextBox
    Friend WithEvents tbZt As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents tbMaxt As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbMint As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dgwParameters As System.Windows.Forms.DataGridView
    Friend WithEvents bsU As System.Windows.Forms.BindingSource
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SliderMinimumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SliderMaximumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SliderStepDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents plus As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents minus As System.Windows.Forms.DataGridViewButtonColumn
End Class
