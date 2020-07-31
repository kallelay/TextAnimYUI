Public Class Form2

    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        If ListBox1.SelectedIndex = -1 Then Exit Sub

        Debugx("Loading Animation # " & ListBox1.SelectedIndex)
        Frames.Clear()
        Frames.AddRange(CurrentWorld.AllFrames(ListBox1.SelectedIndex))
        LoadFrame(0)
        CurrentFrame = 0
        firstFrameInRow = 0
        Form1.PreloadImages()
        Form1.Enabled = True
        Form1.EnableDisablePanels()
        Form1.TimerForAnim.Stop()
        Me.Hide()

    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Form1.LatestFrame = 0
        Form1.TimerForAnim.Start()

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged

    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Panelx_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panelx.Paint

    End Sub

    Private Sub Form2_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ListBox2.SelectedIndex = ListBox2.FindString("3,0,2,1")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form1.Enabled = True
        Form1.TimerForAnim.Stop()
        Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListBox1_DoubleClick(ListBox1, Nothing)

    End Sub
End Class