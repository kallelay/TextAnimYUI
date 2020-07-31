<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.Panelx = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Panelx.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panelx
        '
        Me.Panelx.Controls.Add(Me.Button2)
        Me.Panelx.Controls.Add(Me.Label2)
        Me.Panelx.Controls.Add(Me.Button1)
        Me.Panelx.Controls.Add(Me.ListBox2)
        Me.Panelx.Controls.Add(Me.Label1)
        Me.Panelx.Controls.Add(Me.ListBox1)
        Me.Panelx.Location = New System.Drawing.Point(2, 12)
        Me.Panelx.Name = "Panelx"
        Me.Panelx.Size = New System.Drawing.Size(383, 336)
        Me.Panelx.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 245)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Advanced, don't touch:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(264, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 19)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Hide"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox2.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ListBox2.HorizontalExtent = 20
        Me.ListBox2.ItemHeight = 19
        Me.ListBox2.Items.AddRange(New Object() {"0,1,2,3", "0,1,3,2", "0,2,1,3", "0,2,3,1", "0,3,1,2", "0,3,2,1", "1,0,2,3", "1,0,3,2", "1,2,0,3", "1,2,3,0", "1,3,2,0", "1,3,0,2", "2,0,1,3", "2,0,3,1", "2,1,0,3", "2,1,3,0", "2,3,1,0", "2,3,0,1", "3,0,1,2", "3,0,2,1", "3,1,0,2", "3,1,2,0", "3,2,0,1", "3,2,1,0"})
        Me.ListBox2.Location = New System.Drawing.Point(30, 261)
        Me.ListBox2.MinimumSize = New System.Drawing.Size(25, 10)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(336, 59)
        Me.ListBox2.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select Animation:"
        '
        'ListBox1
        '
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ListBox1.HorizontalExtent = 20
        Me.ListBox1.ItemHeight = 19
        Me.ListBox1.Location = New System.Drawing.Point(30, 63)
        Me.ListBox1.MinimumSize = New System.Drawing.Size(25, 10)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(336, 154)
        Me.ListBox1.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(264, 223)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 18)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "OK"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 351)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panelx)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Panelx.ResumeLayout(False)
        Me.Panelx.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panelx As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents ListBox1 As System.Windows.Forms.ListBox
    Public WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
