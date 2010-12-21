<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tfObjectCh
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BCancel = New System.Windows.Forms.Button
        Me.BOk = New System.Windows.Forms.Button
        Me.PTitle = New System.Windows.Forms.Panel
        Me.BClose = New System.Windows.Forms.Button
        Me.LTitle = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PTitle.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.CheckedListBox1)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.PTitle)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(292, 380)
        Me.Panel2.TabIndex = 1
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.BackColor = System.Drawing.Color.LightGray
        Me.CheckedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckedListBox1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.CheckedListBox1.ForeColor = System.Drawing.Color.Black
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.IntegralHeight = False
        Me.CheckedListBox1.Location = New System.Drawing.Point(3, 35)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(284, 248)
        Me.CheckedListBox1.TabIndex = 3
        Me.CheckedListBox1.ThreeDCheckBoxes = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Black
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(3, 283)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(284, 92)
        Me.Panel3.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.YellowGreen
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.BCancel)
        Me.Panel1.Controls.Add(Me.BOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 62)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(284, 30)
        Me.Panel1.TabIndex = 1
        '
        'BCancel
        '
        Me.BCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BCancel.BackColor = System.Drawing.Color.Black
        Me.BCancel.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.BCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.BCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.BCancel.ForeColor = System.Drawing.Color.White
        Me.BCancel.Location = New System.Drawing.Point(204, 4)
        Me.BCancel.Name = "BCancel"
        Me.BCancel.Size = New System.Drawing.Size(75, 23)
        Me.BCancel.TabIndex = 0
        Me.BCancel.Text = "Cancel"
        Me.BCancel.UseVisualStyleBackColor = False
        '
        'BOk
        '
        Me.BOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BOk.BackColor = System.Drawing.Color.Black
        Me.BOk.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.BOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.BOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.BOk.ForeColor = System.Drawing.Color.White
        Me.BOk.Location = New System.Drawing.Point(118, 4)
        Me.BOk.Name = "BOk"
        Me.BOk.Size = New System.Drawing.Size(75, 23)
        Me.BOk.TabIndex = 0
        Me.BOk.Text = "Ok"
        Me.BOk.UseVisualStyleBackColor = False
        '
        'PTitle
        '
        Me.PTitle.BackColor = System.Drawing.Color.Black
        Me.PTitle.Controls.Add(Me.BClose)
        Me.PTitle.Controls.Add(Me.LTitle)
        Me.PTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.PTitle.Location = New System.Drawing.Point(3, 3)
        Me.PTitle.Name = "PTitle"
        Me.PTitle.Size = New System.Drawing.Size(284, 32)
        Me.PTitle.TabIndex = 1
        '
        'BClose
        '
        Me.BClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BClose.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.BClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.BClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.BClose.ForeColor = System.Drawing.Color.White
        Me.BClose.Location = New System.Drawing.Point(259, 4)
        Me.BClose.Name = "BClose"
        Me.BClose.Size = New System.Drawing.Size(20, 20)
        Me.BClose.TabIndex = 2
        Me.BClose.Text = "X"
        Me.BClose.UseVisualStyleBackColor = True
        '
        'LTitle
        '
        Me.LTitle.AutoSize = True
        Me.LTitle.BackColor = System.Drawing.Color.Transparent
        Me.LTitle.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.LTitle.ForeColor = System.Drawing.Color.Gold
        Me.LTitle.Location = New System.Drawing.Point(8, 6)
        Me.LTitle.Name = "LTitle"
        Me.LTitle.Size = New System.Drawing.Size(134, 17)
        Me.LTitle.TabIndex = 1
        Me.LTitle.Text = "Objects chooser"
        '
        'tfObjectCh
        '
        Me.AcceptButton = Me.BOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.CancelButton = Me.BClose
        Me.ClientSize = New System.Drawing.Size(292, 380)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "tfObjectCh"
        Me.Opacity = 0.95
        Me.Text = "tfObjectCh"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.PTitle.ResumeLayout(False)
        Me.PTitle.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PTitle As System.Windows.Forms.Panel
    Friend WithEvents LTitle As System.Windows.Forms.Label
    Friend WithEvents BOk As System.Windows.Forms.Button
    Friend WithEvents BCancel As System.Windows.Forms.Button
    Friend WithEvents BClose As System.Windows.Forms.Button
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
