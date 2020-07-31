Public Class Main

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Process.GetCurrentProcess.Kill()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Dim InitPos As Point
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = 137
        Me.StartPosition = FormStartPosition.CenterScreen

        'Our directory, which contains user pref 
        If IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData) = False Then
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "Path")
        End If


        If IO.File.Exists(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "Path") <> False Then
            TextBox1.Text = IO.File.ReadAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "Path")
        End If
    End Sub

    Private Sub Main_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += Cursor.Position - InitPos
        End If
        InitPos = Cursor.Position
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Button1.Enabled = False
        If IO.File.Exists(TextBox1.Text) = False Then
            TextBox1.ForeColor = Color.Maroon
            Exit Sub
        End If
        If Strings.Right(TextBox1.Text, 2) <> ".w" Then
            TextBox1.ForeColor = Color.Maroon
            Exit Sub
        End If

        TextBox1.ForeColor = Color.Green
        Button1.Enabled = True

    End Sub
    Sub Clean_For_Textures()

    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ofdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = ofdw.FileName
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Save world infos (for preview etc)
        WorldFile = TextBox1.Text
        WorldPath = Replace(TextBox1.Text, Split(TextBox1.Text, "\").Last, "")
        WorldName = Mid(Split(TextBox1.Text, "\").Last, 1, Len(Split(TextBox1.Text, "\").Last) - 2)

        'Save into file
        IO.File.WriteAllText(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "Path", TextBox1.Text)


        'unload this form and load form1
        Form1.Show()
        Me.Hide()

    End Sub
End Class
