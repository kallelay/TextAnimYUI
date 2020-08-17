Public Class W_Control

    Public FORM_W_LOST_FOCUS As Boolean = True
    Dim InitPos As Point
    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        If MouseButtons = Windows.Forms.MouseButtons.Left Then
            Me.Location += Cursor.Position - InitPos
            Form2.Location += Cursor.Position - InitPos

        End If
        InitPos = Cursor.Position
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form1.Enabled = True

        Form1.TimerForAnim.Stop()
        Hide()
        LoadFrame(CurrentFrame)



        'activate first frame (if possible)
        Dim ev As New MouseEventArgs(Windows.Forms.MouseButtons.Left, 1, 0, 0, 0)
        Form1.Panel12_Click(Form1.Panel12, ev)

    End Sub
    Public myData$
    Dim Th As Threading.Thread

    Private Sub W_Control_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        FORM_W_LOST_FOCUS = False
    End Sub
    Private Sub W_Control_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Location = Form1.Location + New Point(-100, 20)
        Th = New Threading.Thread(AddressOf StartThread)
        ListBox1.Items.Clear()
        myData = Main.TextBox1.Text
        ' Threading.Thread.SetData(mySlot, Main.TextBox1.Text)
        th.Start()

    End Sub
    Sub StartThread()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf StartThread))
            Exit Sub
        End If
        ListBox1.Items.Clear()
        CurrentWorld = New WORLD(myData)

        For j = 0 To CurrentWorld.AllFrames.Count - 1
            ListBox1.Items.Add("[" & j & "] Animation (" & j & ")")
        Next

    End Sub

    Private Sub ListBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.Click



    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Form1.TimerForAnim.Stop()
        Form1.LatestFrame = 0
        Form1.TimerForAnim.Start()

    End Sub

    Private Sub W_Control_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate

        FORM_W_LOST_FOCUS = True
    End Sub

    Private Sub W_Control_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Form1.Button5.PerformClick() 'stop animations
        Form1.Enabled = False
        'stop animations


    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        CurrentWorld.AllFrames.Remove(CurrentWorld.AllFrames.Item(ListBox1.SelectedIndex))
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)

        ListBox1.Items.Clear()
        For j = 0 To CurrentWorld.AllFrames.Count - 1
            ListBox1.Items.Add("[" & j & "] Animation (" & j & ")")
        Next

        'savebutton.PerformClick()
        SaveWorld()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If ListBox1.SelectedIndex <= 0 Then Exit Sub
        Dim anim = CurrentWorld.AllFrames(ListBox1.SelectedIndex)
        CurrentWorld.AllFrames(ListBox1.SelectedIndex) = CurrentWorld.AllFrames(ListBox1.SelectedIndex - 1)
        CurrentWorld.AllFrames(ListBox1.SelectedIndex - 1) = anim

        Dim myN = ListBox1.SelectedItem
        ListBox1.Items(ListBox1.SelectedIndex) = ListBox1.Items(ListBox1.SelectedIndex - 1)
        ListBox1.Items(ListBox1.SelectedIndex - 1) = myN

        'savebutton.PerformClick()
        SaveWorld()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If ListBox1.SelectedIndex >= ListBox1.Items.Count - 1 Then Exit Sub
        Dim anim = CurrentWorld.AllFrames(ListBox1.SelectedIndex)
        CurrentWorld.AllFrames(ListBox1.SelectedIndex) = CurrentWorld.AllFrames(ListBox1.SelectedIndex + 1)
        CurrentWorld.AllFrames(ListBox1.SelectedIndex + 1) = anim

        Dim myN = ListBox1.SelectedItem
        ListBox1.Items(ListBox1.SelectedIndex) = ListBox1.Items(ListBox1.SelectedIndex + 1)
        ListBox1.Items(ListBox1.SelectedIndex + 1) = myN

        '   savebutton.PerformClick()
        SaveWorld()
    End Sub
    Sub SaveWorld()
        Try
            If IO.File.Exists(Main.TextBox1.Text & "_backup") = False Then
                IO.File.Copy(Main.TextBox1.Text, Main.TextBox1.Text & "_backup")
                Debugx("Made a backup at " & Main.TextBox1.Text & "_backup")
            End If
        Catch ex As Exception
            Debugx("Something went wrong in making a backupfile")
        End Try

        'save to current world
        Debugx("saving world file...")
        CurrentWorld.Save(Main.TextBox1.Text)

        'refresh
        Debugx("Refreshing input")
        StartThread()
    End Sub
    Private Sub savebutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles savebutton.Click
        SaveWorld()

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        texanimWExport.Show()
    End Sub


    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click ', Button9.Click
        'add new animation
        If Not cooked() Then
            MsgBox("- Darling... Dinner isn't ready yet!" & vbNewLine & "- Cook it Cook it!", MsgBoxStyle.Critical)
            Debugx("Attempted to add/remove animation, which is not baked/cooked yet. Please cook all animations before attempting")
            Exit Sub
        End If
        CurrentWorld.AllFrames(ListBox1.SelectedIndex) = Frames

        '   savebutton.PerformClick()
        '   StartThread() 'reset
        SaveWorld()


    End Sub

    Private Sub Button9_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If Not cooked() Then
            MsgBox("- Darling... Dinner isn't ready yet!" & vbNewLine & "- Cook it Cook it!", MsgBoxStyle.Critical)
            Debugx("Attempted to add/remove animation, which is not baked/cooked yet. Please cook all animations before attempting")
            Exit Sub
        End If
        CurrentWorld.AllFrames.Add(Frames)
        SaveWorld()

        '  StartThread() 'reset
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Debugx("Loading Animation # " & ListBox1.SelectedIndex)
        Debugx("- Total Frames: " & CurrentWorld.AllFrames(ListBox1.SelectedIndex).Count)
        Frames.Clear()

        Frames.AddRange(CurrentWorld.AllFrames(ListBox1.SelectedIndex))

        Form1.EnableDisablePanels()
        LoadFrame(0)
        CurrentFrame = 0
        firstFrameInRow = 0
        Form1.PreloadImages()
        Form1.Enabled = True
        Form1.EnableDisablePanels()
        Form1.TimerForAnim.Stop()
        Me.Hide()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Button10.PerformClick()
        YUIAutoCook_TimeMachine.TimeMachine_ReturnRecipe()
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub
End Class