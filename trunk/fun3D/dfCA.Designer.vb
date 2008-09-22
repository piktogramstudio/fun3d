<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dfCA
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.NUDLive = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.NUDMax = New System.Windows.Forms.NumericUpDown
        Me.Button1 = New System.Windows.Forms.Button
        Me.NUDMin = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.NUDL = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.NUDR = New System.Windows.Forms.NumericUpDown
        Me.NUDC = New System.Windows.Forms.NumericUpDown
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.NUDLive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NUDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(137, 65)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.NUDLive)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.NUDMax)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.NUDMin)
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.NUDL)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.NUDR)
        Me.Panel1.Controls.Add(Me.NUDC)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(5, 291)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(286, 97)
        Me.Panel1.TabIndex = 1
        '
        'NUDLive
        '
        Me.NUDLive.Location = New System.Drawing.Point(127, 32)
        Me.NUDLive.Maximum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.NUDLive.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NUDLive.Name = "NUDLive"
        Me.NUDLive.Size = New System.Drawing.Size(36, 20)
        Me.NUDLive.TabIndex = 4
        Me.NUDLive.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "RULE"
        '
        'NUDMax
        '
        Me.NUDMax.Location = New System.Drawing.Point(85, 32)
        Me.NUDMax.Maximum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.NUDMax.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NUDMax.Name = "NUDMax"
        Me.NUDMax.Size = New System.Drawing.Size(36, 20)
        Me.NUDMax.TabIndex = 3
        Me.NUDMax.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(6, 68)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(46, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Clear"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'NUDMin
        '
        Me.NUDMin.Location = New System.Drawing.Point(43, 32)
        Me.NUDMin.Maximum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.NUDMin.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NUDMin.Name = "NUDMin"
        Me.NUDMin.Size = New System.Drawing.Size(36, 20)
        Me.NUDMin.TabIndex = 3
        Me.NUDMin.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(192, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Levels"
        '
        'NUDL
        '
        Me.NUDL.Location = New System.Drawing.Point(236, 6)
        Me.NUDL.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NUDL.Name = "NUDL"
        Me.NUDL.Size = New System.Drawing.Size(41, 20)
        Me.NUDL.TabIndex = 0
        Me.NUDL.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Rows"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(90, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Collumns"
        '
        'NUDR
        '
        Me.NUDR.Location = New System.Drawing.Point(140, 6)
        Me.NUDR.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NUDR.Name = "NUDR"
        Me.NUDR.Size = New System.Drawing.Size(41, 20)
        Me.NUDR.TabIndex = 0
        Me.NUDR.Value = New Decimal(New Integer() {6, 0, 0, 0})
        '
        'NUDC
        '
        Me.NUDC.Location = New System.Drawing.Point(43, 6)
        Me.NUDC.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NUDC.Name = "NUDC"
        Me.NUDC.Size = New System.Drawing.Size(41, 20)
        Me.NUDC.TabIndex = 0
        Me.NUDC.Value = New Decimal(New Integer() {6, 0, 0, 0})
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Cross
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(286, 286)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'dfCA
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(296, 393)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dfCA"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "dfCA"
        Me.TopMost = True
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.NUDLive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NUDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents NUDL As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDC As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDR As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NUDLive As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents NUDMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
