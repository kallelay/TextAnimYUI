<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class texanimWExport
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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.renderWindowPanel = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ListBox4 = New System.Windows.Forms.ListBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.renderWindowPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.White
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(12, 23)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(166, 379)
        Me.ListBox1.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Mesh list"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(45, 348)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(190, 30)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "Quadify mesh only..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(493, 272)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(190, 30)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "Assign New Animation and save"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(496, 311)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(190, 30)
        Me.Button3.TabIndex = 19
        Me.Button3.Text = "Assign Selected Animation and save"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(223, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Visible = False
        '
        'renderWindowPanel
        '
        Me.renderWindowPanel.BackColor = System.Drawing.Color.White
        Me.renderWindowPanel.Controls.Add(Me.Button1)
        Me.renderWindowPanel.Controls.Add(Me.Button2)
        Me.renderWindowPanel.Controls.Add(Me.Button4)
        Me.renderWindowPanel.Controls.Add(Me.Button3)
        Me.renderWindowPanel.Location = New System.Drawing.Point(344, 24)
        Me.renderWindowPanel.Name = "renderWindowPanel"
        Me.renderWindowPanel.Size = New System.Drawing.Size(711, 382)
        Me.renderWindowPanel.TabIndex = 0
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(303, 293)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(154, 24)
        Me.Button4.TabIndex = 20
        Me.Button4.Text = "&Preview"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(347, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "camstatus:"
        '
        'ListBox2
        '
        Me.ListBox2.BackColor = System.Drawing.Color.White
        Me.ListBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(187, 39)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(154, 106)
        Me.ListBox2.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(187, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Animation:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(184, 151)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Polygons:"
        '
        'ListBox3
        '
        Me.ListBox3.BackColor = System.Drawing.Color.White
        Me.ListBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.Location = New System.Drawing.Point(184, 168)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.ListBox3.Size = New System.Drawing.Size(154, 158)
        Me.ListBox3.TabIndex = 27
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(187, 333)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Orientation:"
        '
        'ListBox4
        '
        Me.ListBox4.BackColor = System.Drawing.Color.White
        Me.ListBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox4.FormattingEnabled = True
        Me.ListBox4.Items.AddRange(New Object() {"0,1,2,3", "0,1,3,2", "0,2,1,3", "0,2,3,1", "0,3,1,2", "0,3,2,1", "1,0,2,3", "1,0,3,2", "1,2,0,3", "1,2,3,0", "1,3,0,2", "1,3,2,0", "2,1,3,0", "2,1,0,3", "2,0,1,3", "2,0,3,1", "2,3,1,0", "2,3,0,1", "3,1,2,0", "3,1,0,2", "3,0,1,2", "3,0,2,1", "3,2,0,1", "3,2,1,0", "0,1,2,0"})
        Me.ListBox4.Location = New System.Drawing.Point(187, 349)
        Me.ListBox4.Name = "ListBox4"
        Me.ListBox4.Size = New System.Drawing.Size(154, 54)
        Me.ListBox4.TabIndex = 29
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(242, 151)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(23, 22)
        Me.Button5.TabIndex = 21
        Me.Button5.Text = "A"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(271, 151)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(23, 22)
        Me.Button6.TabIndex = 31
        Me.Button6.Text = "N"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'texanimWExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1067, 418)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ListBox4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.renderWindowPanel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "texanimWExport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TexAnim W Export"
        Me.TopMost = True
        Me.renderWindowPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents renderWindowPanel As System.Windows.Forms.Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ListBox4 As ListBox
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
End Class
