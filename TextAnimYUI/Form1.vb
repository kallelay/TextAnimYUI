Imports System.Runtime.InteropServices

Public Class Form1
    Inherits Form
   

    <DllImport("user32.dll")> _
    Public Shared Function CreateIconIndirect(ByRef icon As IconInfo) As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetIconInfo(ByVal hIcon As IntPtr, ByRef pIconInfo As IconInfo) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Public Shared Function CreateCursor(ByVal bmp As Bitmap, ByVal xHotSpot As Integer, ByVal yHotSpot As Integer) As Cursor
        Dim tmp As New IconInfo()
        GetIconInfo(bmp.GetHicon(), tmp)
        tmp.xHotspot = xHotSpot
        tmp.yHotspot = yHotSpot
        tmp.fIcon = False
        Return New Cursor(CreateIconIndirect(tmp))
    End Function
    Public Structure IconInfo
        Public fIcon As Boolean
        Public xHotspot As Integer
        Public yHotspot As Integer
        Public hbmMask As IntPtr
        Public hbmColor As IntPtr
    End Structure

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub
    Dim InitPos As Point
    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        If MouseButtons = Windows.Forms.MouseButtons.Left Then
            Me.Location += Cursor.Position - InitPos
            Form2.Location += Cursor.Position - InitPos
            W_Control.Location += Cursor.Position - InitPos

        End If
        InitPos = Cursor.Position
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()

      
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Panel6_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel6.MouseMove
        If Not Panel6.BorderStyle = BorderStyle.FixedSingle Then Panel6.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub Panel7_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel7.MouseMove
        If Not Panel7.BorderStyle = BorderStyle.FixedSingle Then Panel7.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub Panel6_MouseOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel6.MouseLeave
        If Panel6.BorderStyle = BorderStyle.FixedSingle Then Panel6.BorderStyle = BorderStyle.None
    End Sub
    Private Sub Panel7_MouseOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel7.MouseLeave
        If Panel7.BorderStyle = BorderStyle.FixedSingle Then Panel7.BorderStyle = BorderStyle.None
    End Sub

    Private Sub Panel6_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel6.Click
        If Panel6.Enabled = False Then Exit Sub
        Me.ActiveControl = sender
        SaveFrame(CurrentFrame)
        firstFrameInRow += 1
        CurrentFrame += 1

        Dim TotalSteps% = Math.Abs(Panel12.Left - Panel13.Left)
        Application.DoEvents()
        Panel17.Left = Panel15.Left + TotalSteps

        Panel17.BackgroundImage = Frames(firstFrameInRow + 3).Image
        Application.DoEvents()
        For i = 1 To TotalSteps Step 5
            Panel12.Left -= 5
            Panel13.Left -= 5
            Panel14.Left -= 5
            Panel15.Left -= 5
            Panel17.Left -= 5
            Panel17.Refresh()

        Next


        PreloadImages(Dirtype.left_)

        LoadFrame(CurrentFrame)
        EnableDisablePanels()

    End Sub
    Private Sub Panel7_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel7.Click
        If Panel7.Enabled = False Then Exit Sub
        Me.ActiveControl = sender
        SaveFrame(CurrentFrame)
        firstFrameInRow -= 1
        CurrentFrame -= 1

        Panel17.Left = -160
        If firstFrameInRow > -1 Then Panel17.BackgroundImage = Frames(firstFrameInRow).Image


        Dim TotalSteps% = Math.Abs(Panel12.Left - Panel13.Left)

        Application.DoEvents()
        For i = 1 To TotalSteps Step 5
            Panel12.Left += 5
            Panel13.Left += 5
            Panel14.Left += 5
            Panel15.Left += 5
            Panel17.Left += 5
            '  Application.DoEvents()
        Next




        PreloadImages(Dirtype.right_)

        LoadFrame(CurrentFrame)
        EnableDisablePanels()
    End Sub
    Private Sub Panel8_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub Form1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        textbox5_focus = False
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True

        If Not Saved Then
            Dim msgResp = MsgBox("Do you want to save changes?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, "Save Changes")
            Select Case msgResp
                Case MsgBoxResult.No
                    Process.GetCurrentProcess.Kill()
                    End
                Case MsgBoxResult.Yes
                    Button7_Click(sender, e)
                Case MsgBoxResult.Cancel
                    Exit Sub
            End Select

        End If
        Dim allNV = Process.GetProcessesByName("nvolt")
        For i = 0 To allNV.Count - 1
            allNV(i).Kill()
        Next


        Process.GetCurrentProcess.Kill()
        End

    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, Label1.KeyDown, Button2.KeyDown, Button9.KeyDown, Button10.KeyDown, _
    Panel6.KeyDown, Panel7.KeyDown, Panel12.KeyDown, Panel13.KeyDown, Panel14.KeyDown, Panel15.KeyDown
        'Me.ActiveControl = sender

        Dim ev As New MouseEventArgs(Windows.Forms.MouseButtons.Left, 1, 0, 0, 0)

        Select Case e.KeyCode

            Case Keys.Right

                If curFrameIdxInPlace = 3 Then Panel6_Click(Panel6, ev)
                If curFrameIdxInPlace = 2 Then Panel15_Click(Panel15, ev)
                If curFrameIdxInPlace = 1 Then Panel14_Click(Panel14, ev)
                If curFrameIdxInPlace = 0 Then Panel13_Click(Panel13, ev)

            Case Keys.Left
                If curFrameIdxInPlace = 0 Then Panel7_Click(Panel7, ev)
                If curFrameIdxInPlace = 1 Then Panel12_Click(Panel12, ev)
                If curFrameIdxInPlace = 2 Then Panel13_Click(Panel13, ev)
                If curFrameIdxInPlace = 3 Then Panel14_Click(Panel14, ev)
        End Select
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = CreateCursor(My.Resources.cursed, 0, 0)
        Panel4.Controls.Add(Panel17)
        Panel17.Top = 10

    End Sub

    Private Sub Form1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus

    End Sub

    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Init()


    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged ', ComboBox3.SelectedIndexChanged
        LoadBitmapIntoUV(Asc(CChar(ComboBox1.Text)) - 65)

    End Sub
    Dim Moving As Boolean = False
    Dim Selected As New List(Of Integer)
    Dim MousePos, LastMousePos As IrrlichtNETCP.Vector2D
    Dim DeltaMousePos As IrrlichtNETCP.Vector2D
    Dim Mat As New Matrix2x2

    Private Sub Panel16_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel16.DoubleClick
        MousePos = New IrrlichtNETCP.Vector2D((MousePosition.X - Me.Location.X - Panel16.Location.X) / Panel16.Width, _
                                                             (MousePosition.Y - Me.Location.Y - Panel16.Location.Y) / Panel16.Height)


        '   If MouseButtons = Windows.Forms.MouseButtons.Left Then
            For i = 0 To 3
            Dim v = (Selectors(i) + Selectors(i + 1)) / 2 - MousePos
                If v.Length < 0.05 Then
                    Selected.Clear()
                    Selected.Add(i)
                Selected.Add(If(i + 1 = 4, 0, i + 1))
                    Exit For
            End If

            ' Debugger.Break()
            If Selected.Count <> 2 Then Exit Sub

            Dim allX = Math.Abs(Selectors(Selected(0)).X - Selectors(Selected(1)).X)
            Dim allY = Math.Abs(Selectors(Selected(0)).Y - Selectors(Selected(1)).Y)

            'Debugger.Break()
                If allX < allY Then
                Selectors(Selected(0)).X = (Selectors(Selected(0)).X + Selectors(Selected(1)).X) / 2
                Selectors(Selected(1)).X = Selectors(Selected(0)).X

                UpdateUV()
            Else
                Selectors(Selected(0)).Y = (Selectors(Selected(0)).Y + Selectors(Selected(1)).Y) / 2
                Selectors(Selected(1)).Y = Selectors(Selected(0)).Y
                UpdateUV()
                End If

            Next


        '   End If



    End Sub
    Dim bMinX, bMinY As Single, posMinX, posMinY As Integer, bMaxX, bMaxY As Single ', posMaxX, posMaxY As Integer
    Public StaticMousePos As IrrlichtNETCP.Vector2D
    Dim Pivot As New IrrlichtNETCP.Vector2D

    Dim isCtrlShiftPressed = False
    Private Sub Panel16_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Panel16.KeyDown
        If e.Shift And e.Control Then
            isCtrlShiftPressed = True
        End If

    End Sub
    Private Sub Panel16_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Panel16.KeyUp

            isCtrlShiftPressed = True


    End Sub


    '----------- FLAG: Here go the controls ------------------- Die Kontrolle sind hierher!
    Private Sub Panel16_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel16.MouseMove
        If Loading Then Exit Sub
        Me.ActiveControl = Panel16

        If MouseButtons <> Windows.Forms.MouseButtons.None Then textbox5_focus = False : Me.ActiveControl = Label1 'update non focus when updating uv


        MousePos = New IrrlichtNETCP.Vector2D((MousePosition.X - Me.Location.X - Panel16.Location.X) / Panel16.Width, _
                                                               (MousePosition.Y - Me.Location.Y - Panel16.Location.Y) / Panel16.Height)
        DeltaMousePos = MousePos - LastMousePos

        Select Case State
            Case EditModes.Select_
                '------------------MOVING PER UV VECTOR
                Dim va = (Selectors(0) + Selectors(1) + Selectors(2) + Selectors(3)) / 4 - MousePos
                If va.Length < 0.05 Then
                    Saved = False
                    Moving = True
                    Selected.Clear()
                    Selected.Add(1)
                    Selected.Add(2)
                    Selected.Add(3)
                    Selected.Add(0)
                End If


                For i = 0 To 3
                    Dim v = (Selectors(i) + Selectors(If(i + 1 = 4, 0, i + 1))) / 2 - MousePos
                    If v.Length < 0.05 Then
                        Saved = False
                        Moving = True
                        Selected.Clear()
                        Selected.Add(i)
                        Selected.Add(If(i + 1 = 4, 0, i + 1))
                        Exit For
                    End If

                Next



                If MouseButtons = Windows.Forms.MouseButtons.Left And (Not Moving Or Selected.Count = 2) Then
                    For i = 0 To 3
                        Dim v = Selectors(i) - MousePos
                        If v.Length < 0.03 Then
                            Saved = False
                            Moving = True
                            Selected.Clear()
                            Selected.Add(i)
                            Exit For
                        End If

                    Next






                End If







                If MouseButtons = Windows.Forms.MouseButtons.Left And Moving Then
                    For i = 0 To Selected.Count - 1
                        Selectors(Selected(i)) += DeltaMousePos
                        Confirm(Selectors(Selected(i)))


                        UpdateUV()

                    Next

                End If

            Case EditModes.Rect_
                '-----------------------MOVING BY RECT
                Dim va = (Selectors(0) + Selectors(1) + Selectors(2) + Selectors(3)) / 4 - MousePos
                If va.Length < 0.05 Then
                    Saved = False
                    Moving = True
                    Selected.Clear()
                    Selected.Add(1)
                    Selected.Add(2)
                    Selected.Add(3)
                    Selected.Add(0)
                End If


                If MouseButtons = Windows.Forms.MouseButtons.Left And Not Moving Then
                    For i = 0 To 3
                        Dim v = Selectors(i) - MousePos
                        If v.Length < 0.03 Then
                            Saved = False
                            Moving = True
                            Selected.Clear()
                            Selected.Add(i)
                            Exit For
                        End If

                    Next


                    bMinX = 20000
                    bMinY = 20000
                    bMaxX = -2000
                    bMaxY = -2000
                    For i = 0 To 3
                        If Selected.Count = 0 Then Exit For
                        If Selected(0) = i Then Continue For

                        If Math.Abs(Selectors(i).X - Selectors(Selected(0)).X) < bMinX Then
                            posMinX = i
                            bMinX = Math.Abs(Selectors(i).X - Selectors(Selected(0)).X)
                        End If



                        If Math.Abs(Selectors(i).Y - Selectors(Selected(0)).Y) < bMinY Then
                            ' If Math.Abs(Selectors(i).X - Selectors(Selected(0)).X) = bMinX Then Continue For
                            posMinY = i
                            bMinY = Math.Abs(Selectors(i).Y - Selectors(Selected(0)).Y)
                        End If


                        ' If Math.Abs(Selectors(i).Y - Selectors(Selected(0)).Y) > bMaxY Then
                        'posMaxY = i
                        '  bMaxY = Math.Abs(Selectors(i).Y - Selectors(Selected(0)).Y)
                        '  End If

                    Next
                End If

                If MouseButtons = Windows.Forms.MouseButtons.Left And Moving Then 'Rectangle selection
                    If Selected.Count = 1 Then
                        Selectors(Selected(0)) += DeltaMousePos
                        Confirm(Selectors(Selected(0)))
                        Selectors(posMinX) = New IrrlichtNETCP.Vector2D(Selectors(Selected(0)).X, Selectors(posMinX).Y)
                        Selectors(posMinY) = New IrrlichtNETCP.Vector2D(Selectors(posMinY).X, Selectors(Selected(0)).Y)
                    Else                'move all
                        For i = 0 To Selected.Count - 1
                            Selectors(Selected(i)) += DeltaMousePos
                            Confirm(Selectors(Selected(i)))

                        Next
                    End If



                    UpdateUV()


                End If


            Case EditModes.Rotate_
                '-------------------------------- Rotation

                If MouseButtons = Windows.Forms.MouseButtons.None And Not Moving Then
                    StaticMousePos = New IrrlichtNETCP.Vector2D((MousePosition.X - Me.Location.X - Panel16.Location.X) / Panel16.Width, _
                                                            (MousePosition.Y - Me.Location.Y - Panel16.Location.Y) / Panel16.Height)




                End If


                If MouseButtons <> Windows.Forms.MouseButtons.None Then
                    Mat.CreateRotationMatrix(DeltaMousePos.X)
                    Pivot = New IrrlichtNETCP.Vector2D
                    For i = 0 To 3
                        Pivot += Selectors(i) / 4
                    Next

                    For i = 0 To 3
                        Selectors(i) = (Mat * (Selectors(i) - Pivot)) + Pivot
                        'Debugx(Selectors(i).X & "x" & Selectors(i).Y)
                    Next
                    Frames(CurrentFrame).Rotation += DeltaMousePos.X
                    Label14.Text = CInt(Frames(CurrentFrame).Rotation * 180 / Math.PI) & "°"
                    Select Case Label14.Text
                        Case "0°"
                            Label13.Text = "~ 0"
                        Case "30°"
                            Label13.Text = "~ π/6"
                        Case "45°"
                            Label13.Text = "~ π/4"
                        Case "60°"
                            Label13.Text = "~ π/3"
                        Case "90°"
                            Label13.Text = "~ π/2"
                        Case "120°"
                            Label13.Text = "~ 2π/3"
                        Case "135°"
                            Label13.Text = "~ 3π/4"
                        Case "150°"
                            Label13.Text = "~ 5π/6"
                        Case "180°"
                            Label13.Text = "~ π"
                        Case "360°"
                            Label13.Text = "~ 2π"
                        Case Else
                            Label13.Text = "~ " & Strings.Format((Replace(Label14.Text, "°", "") * Math.PI / 180) / (Math.PI / 6), "0.#0") & "π/6"

                    End Select

                    UpdateUV()

                End If
               

        End Select







        If MouseButtons <> Windows.Forms.MouseButtons.Left Then Moving = False

        LastMousePos = MousePos
    End Sub
    Sub Confirm(ByRef M As IrrlichtNETCP.Vector2D) 'As Boolean 
        If M.X > 1 Then M.X = 1
        If M.X < 0 Then M.X = 0
        If M.Y > 1 Then M.Y = 1
        If M.Y < 0 Then M.Y = 0

        ' If M.X > 1 Or M.X < 0 Then Return False
        '  If M.Y > 1 Or M.Y < 0 Then Return False
        '  Return True
    End Sub
    Private Sub Panel16_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel16.Paint

    End Sub

    Dim curFrameIdxInPlace = 0
    Sub Basculate(ByVal TargetPanel As Panel) 'for saving and images and etc
        If TargetPanel Is Panel12 Then curFrameIdxInPlace = 0 _
        Else If TargetPanel Is Panel13 Then curFrameIdxInPlace = 1 _
        Else If TargetPanel Is Panel14 Then curFrameIdxInPlace = 2 _
        Else If TargetPanel Is Panel15 Then curFrameIdxInPlace = 3

        Application.DoEvents()
        SaveFrame(CurrentFrame)

        Dim step_! = (TargetPanel.Left - Label2.Left) / Math.Abs(TargetPanel.Left - Label2.Left + 0.01)
        For i = 0 To Math.Abs(Label2.Left - TargetPanel.Left) Step 3
            Application.DoEvents()
            Label2.Left += step_ * 5
        Next
    End Sub
    Enum Dirtype
        right_
        left_
    End Enum
    Sub PreloadImages()

        Panel12.BackgroundImage = Frames(firstFrameInRow).Image
        If Frames.Count > 1 Then Panel13.BackgroundImage = Frames(firstFrameInRow + 1).Image
        If Frames.Count > 2 Then Panel14.BackgroundImage = Frames(firstFrameInRow + 2).Image
        If Frames.Count > 3 Then Panel15.BackgroundImage = Frames(firstFrameInRow + 3).Image


    

    End Sub
    Sub PreloadImages(ByVal Dir As Dirtype)

        If firstFrameInRow + 0 < Frames.Count Then Panel12.BackgroundImage = Frames(firstFrameInRow).Image
        If firstFrameInRow + 1 < Frames.Count Then Panel13.BackgroundImage = Frames(firstFrameInRow + 1).Image
        If firstFrameInRow + 2 < Frames.Count Then Panel14.BackgroundImage = Frames(firstFrameInRow + 2).Image
        If firstFrameInRow + 3 < Frames.Count Then Panel15.BackgroundImage = Frames(firstFrameInRow + 3).Image

        Dim TotalSteps% = Math.Abs(Panel13.Left - Panel14.Left)

        Panel17.Left = -9999



        Select Case Dir
            Case Dirtype.left_
                For i = 0 To 3
                    Panel4.Controls.Find("Panel" & 12 + i, True)(0).Left += TotalSteps
                Next
            Case Dirtype.right_
                For i = 0 To 3
                    Panel4.Controls.Find("Panel" & 12 + i, True)(0).Left -= TotalSteps
                Next
        End Select
    End Sub
    Private Sub Panel12_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel12.MouseDown
        If Panel12.Enabled = False Then Exit Sub
        Me.ActiveControl = sender
        If e.Button <> Windows.Forms.MouseButtons.None Then

            Basculate(Panel12)
            CurrentFrame = firstFrameInRow
            LoadFrame(CurrentFrame)

        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Panel18.Location = Panel4.Location + Panel12.Location + New Point(120, -80)
            TextBox1.Text = Frames(firstFrameInRow).Delay
            ComboBox2.SelectedIndex = Frames(firstFrameInRow).Type
            If Frames(firstFrameInRow).Type = AnimType.Static_ Then
                TextBox3.Enabled = False
            Else
                TextBox3.Enabled = True
            End If
            Timer3.Start()

            Panel18.Show()
        End If

    End Sub

    
    Private Sub Panel13_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel13.MouseDown
        If Panel13.Enabled = False Then Exit Sub
        Me.ActiveControl = sender
        If e.Button <> Windows.Forms.MouseButtons.None Then
            Basculate(Panel13)
            CurrentFrame = firstFrameInRow + 1
            LoadFrame(CurrentFrame)

            'PreloadImages()
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Panel18.Location = Panel4.Location + Panel13.Location + New Point(120, -80)
            TextBox1.Text = Frames(firstFrameInRow + 1).Delay
            ComboBox2.SelectedIndex = Frames(firstFrameInRow + 1).Type
            If Frames(firstFrameInRow + 1).Type = AnimType.Static_ Then
                TextBox3.Enabled = False
            Else
                TextBox3.Enabled = True
            End If
            Timer3.Start()

            Panel18.Show()
        End If

    End Sub
    Private Sub Panel14_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel14.MouseDown
        If Panel14.Enabled = False Then Exit Sub
        Me.ActiveControl = sender
        If e.Button <> Windows.Forms.MouseButtons.None Then
            Basculate(Panel14)
            CurrentFrame = firstFrameInRow + 2
            LoadFrame(CurrentFrame)

        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Panel18.Location = Panel4.Location + Panel14.Location + New Point(120, -80)
            TextBox1.Text = Frames(firstFrameInRow + 2).Delay
            ComboBox2.SelectedIndex = Frames(firstFrameInRow + 2).Type
            If Frames(firstFrameInRow + 2).Type = AnimType.Static_ Then
                TextBox3.Enabled = False
            Else
                TextBox3.Enabled = True
            End If
            Timer3.Start()

            Panel18.Show()
        End If

    End Sub
    Private Sub Panel15_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel15.MouseDown
        If Panel15.Enabled = False Then Exit Sub
        Me.ActiveControl = sender
        If e.Button <> Windows.Forms.MouseButtons.None Then
            Basculate(Panel15)
            CurrentFrame = firstFrameInRow + 3
            LoadFrame(CurrentFrame)
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Panel18.Location = Panel4.Location + Panel15.Location + New Point(-120, -80)
            TextBox1.Text = Frames(firstFrameInRow + 3).Delay
            ComboBox2.SelectedIndex = Frames(firstFrameInRow + 3).Type
            If Frames(firstFrameInRow + 3).Type = AnimType.Static_ Then
                TextBox3.Enabled = False
            Else
                TextBox3.Enabled = True
            End If
            Timer3.Start()

            Panel18.Show()
        End If
    End Sub


    Sub EnableDisablePanels()
    
        Panel12.Enabled = firstFrameInRow < Frames.Count
        Panel13.Enabled = firstFrameInRow + 1 < Frames.Count
        Panel14.Enabled = firstFrameInRow + 2 < Frames.Count
        Panel15.Enabled = firstFrameInRow + 3 < Frames.Count



        For i = 12 To 15
            If Panel4.Controls.Find("Panel" & i, True)(0).Enabled Then
                Panel4.Controls.Find("Panel" & i, True)(0).BackColor = Color.Gray
            Else
                Panel4.Controls.Find("Panel" & i, True)(0).BackColor = Color.LightGray
            End If
        Next


        If Frames.Count > firstFrameInRow + 4 Then
            Panel6.Enabled = True
            Panel6.BackgroundImage = Nothing
        Else
            Panel6.Enabled = False
            Panel6.BackgroundImage = TextAnimYUI.My.Resources.Resources.back1
        End If

        If firstFrameInRow > 0 Then
            Panel7.Enabled = True
            Panel7.BackgroundImage = Nothing
        Else
            Panel7.Enabled = False
            Panel7.BackgroundImage = TextAnimYUI.My.Resources.Resources.back1
        End If



    End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Saved = False
        If Frames.Count <> 0 Then
            Frames.Insert(CurrentFrame + 1, New Frame)
        Else
            Frames.Add(New Frame)
        End If



        EnableDisablePanels()

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Saved = False
        Frames.Remove(Frames(CurrentFrame))
        Me.Panel4.Controls.Find("Panel" & CurrentFrame - firstFrameInRow + 12, True)(0).BackgroundImage = Nothing
        CurrentFrame -= 1
        CurrentFrame = Math.Max(0, CurrentFrame)
        Basculate(Me.Panel4.Controls.Find("Panel" & CurrentFrame + 12, True)(0))
        LoadFrame(CurrentFrame)

        EnableDisablePanels()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Hmmm... I have made this a long time ago, so I think this is the main synchronizer
        'To put it simple, this shit code uses a time to start the work, to relieve a bit CPU usage
        'And a lot of repetition too, TOO MUCH
        'Thank you....

        Panel9.Visible = State = EditModes.Rotate_

        Label7.Text = "KEYFRAME: " & Strings.Format(CurrentFrame, "00") & "/" & Frames.Count
        Try
            Panel12.BackgroundImage = SaveUVintoPICTURE(firstFrameInRow)

        Catch ex As Exception

        End Try

        Try
            Panel13.BackgroundImage = SaveUVintoPICTURE(firstFrameInRow + 1)
        Catch ex As Exception
        End Try

        Try
            Panel14.BackgroundImage = SaveUVintoPICTURE(firstFrameInRow + 2)
        Catch ex As Exception
        End Try

        Try
            Panel15.BackgroundImage = SaveUVintoPICTURE(firstFrameInRow + 3)
        Catch ex As Exception
        End Try

        'update labels for each simulation
        getinfo(Label15, Panel12, 0)
        getinfo(Label16, Panel13, 1)
        getinfo(Label17, Panel14, 2)
        getinfo(Label18, Panel15, 3)


        'Update textbox5 (UV positions, if not highlighted) if textbox5 is not highlighted, otherwise, update uv from textbox5.text 
        If textbox5_focus = False Then
            TextBox5.Text = Selectors(0).X & ", " & Selectors(0).Y & "; " & Selectors(1).X & ", " & Selectors(1).Y & "; " & Selectors(2).X & ", " & Selectors(2).Y & "; " & Selectors(3).X & ", " & Selectors(3).Y
            prevTextbox5$ = TextBox5.Text
        Else
            If prevTextbox5 <> TextBox5.Text Then 'update UV from textbox5
                Try
                    Dim tmp$ = TextBox5.Text.Replace(" ", "")
                    Dim tmpspl = Split(tmp, ";")
                    Dim tmpsplspl() As String

                    If tmpspl.Length = 4 Then
                        For k = 0 To 3

                            If InStr(tmpspl(k), ",") = 0 Then
                                GoTo cleanupbadformattingorfinish
                            End If

                        Next

                        For k = 0 To 3
                            tmpsplspl = Split(tmpspl(k), ",")
                            Try

                                Selectors(k) = New IrrlichtNETCP.Vector2D(tmpsplspl(0), tmpsplspl(1))

                            Catch ex As Exception

                            End Try
                        Next
                        MapSelectors()
                        UpdateUV()

                    End If


cleanupbadformattingorfinish:
                    tmpspl = Nothing
                    tmpsplspl = Nothing
                    tmp = Nothing 'send to gc
                Catch ex As Exception

                End Try

                prevTextbox5 = TextBox5.Text
            End If


        End If


        ' Try : If Frames.Count > firstFrameInRow + 0 Then : Label15.Text = Frames(firstFrameInRow + 0).Type.ToString & "(" & Frames(firstFrameInRow + 0).Delay & "ms), F#" & (firstFrameInRow + 0) : End If : Catch ex As Exception : End Try
        ' Try : If Frames.Count > firstFrameInRow + 1 Then : Label16.Text = Frames(firstFrameInRow + 1).Type.ToString & "(" & Frames(firstFrameInRow + 1).Delay & "ms), F#" & (firstFrameInRow + 1) : End If : Catch ex As Exception : End Try
        ' Try : If Frames.Count > firstFrameInRow + 2 Then : Label17.Text = Frames(firstFrameInRow + 2).Type.ToString & "(" & Frames(firstFrameInRow + 2).Delay & "ms), F#" & (firstFrameInRow + 2) : End If : Catch ex As Exception : End Try
        ' Try : If Frames.Count > firstFrameInRow + 3 Then : Label18.Text = Frames(firstFrameInRow + 3).Type.ToString & "(" & Frames(firstFrameInRow + 3).Delay & "ms), F#" & (firstFrameInRow + 3) : End If : Catch ex As Exception : End Try
    End Sub
    Dim prevTextbox5$

    Sub getinfo(ByRef lbl As Label, ByRef Pnl As Panel, ByVal offsettofirstframeinrow As Integer)
        Try
            If Frames.Count > firstFrameInRow + offsettofirstframeinrow Then

                If Frames(firstFrameInRow + offsettofirstframeinrow).Type = AnimType.Static_ Then 'static 
                    lbl.Text = Frames(firstFrameInRow + offsettofirstframeinrow).Type.ToString & "(" & Frames(firstFrameInRow + offsettofirstframeinrow).Delay & "ms), F#" & (firstFrameInRow + +offsettofirstframeinrow)
                Else
                    lbl.Text = Frames(firstFrameInRow + offsettofirstframeinrow).Type.ToString & "(" & Frames(firstFrameInRow + offsettofirstframeinrow).Delay & "ms in " & Frames(firstFrameInRow + offsettofirstframeinrow).ImageCount & "img), F#" & (firstFrameInRow + 0)
                End If


                'Check if it's a linker frame! (delay = 0, preceeded by animation)

                If Frames(firstFrameInRow + offsettofirstframeinrow).Delay = 0 Then
                    If (firstFrameInRow + offsettofirstframeinrow - 1) > -1 AndAlso Frames(firstFrameInRow + offsettofirstframeinrow - 1).Type <> AnimType.Static_ Then
                        lbl.BackColor = Color.Honeydew
                        lbl.Text &= "-L"
                        lbl.Tag = "[LINKER FRAME]"
                    Else
                        lbl.BackColor = Color.Red
                        lbl.Text &= "[NULL FRAME]"
                        lbl.Tag = "[NULL FRAME]"
                    End If
                Else
                    lbl.Tag = ""
                    lbl.BackColor = Color.Transparent
                End If

                ToolTip1.SetToolTip(Pnl, "Key Frame: " & (firstFrameInRow + offsettofirstframeinrow) & " (uses tex " & Chr(97 + Frames(firstFrameInRow + offsettofirstframeinrow).Tex) & ")" & vbNewLine & _
                                    "Animation : " & Frames(firstFrameInRow + offsettofirstframeinrow).Type.ToString.Replace("_", "") & " " & lbl.Tag & vbNewLine & _
                         If(Frames(firstFrameInRow + offsettofirstframeinrow).Type = AnimType.Static_, "", "Total ") & "Duration: " & Frames(firstFrameInRow + offsettofirstframeinrow).Delay & "ms" & vbNewLine & _
                                    If(Frames(firstFrameInRow + offsettofirstframeinrow).Type <> AnimType.Static_, "Total Frames: " & Frames(firstFrameInRow + offsettofirstframeinrow).ImageCount & vbNewLine, "") & _
                  "Animation Speed: [place reserved]" & vbNewLine & _
                  If(Frames(firstFrameInRow + offsettofirstframeinrow).Type = AnimType.Shake_, "Shake Level in %: " & Frames(firstFrameInRow + offsettofirstframeinrow).NoiseLevel, ""))


            Else
                lbl.Text = ""
                ToolTip1.SetToolTip(Pnl, "")
                lbl.BackColor = Color.Transparent
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Button10_Click(sender, e)
    End Sub

    Private Sub Panel6_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub Panel15_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel15.Paint

    End Sub

    Private Sub Panel7_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel7.Paint

    End Sub
    Enum ImportMode
        WConsole
        Worldfile
        ' BlenderTexAnim
        YUI
    End Enum
    Dim imod As ImportMode = ImportMode.YUI


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        If Not Saved Then
            Dim msgResp = MsgBox("Do you want to save changes?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, "Save Changes")
            Select Case msgResp
                Case MsgBoxResult.No
                    Frames.Clear()
                    mFrames = Nothing
                    CurrentFrame = 0
                    firstFrameInRow = 0
                    '  PreloadImages()
                Case MsgBoxResult.Yes
                    Button7_Click(sender, e)
                Case MsgBoxResult.Cancel
                    Exit Sub
            End Select

        End If

        Loading = True
        Dim fname$ = ""
        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'get file + file ext
            fname$ = ofd.FileName

            imod = ImportMode.YUI

            If LCase(Strings.Right(fname, 3)) = "txt" Then
                imod = ImportMode.WConsole
            End If
            If LCase(Strings.Right(fname, 2)) = ".w" Then
                imod = ImportMode.Worldfile
            End If
            If LCase(Strings.Right(fname, 9)) = "framelist" Then
                imod = ImportMode.WConsole
            End If
        Else
            Exit Sub
        End If

        'Imports 
        Select Case imod
            Case ImportMode.WConsole
                LoadWConsole(fname)
            Case ImportMode.YUI
                LoadYUIfile(fname)
            Case ImportMode.Worldfile
                CurrentWorld = New WORLD(fname)
                Form2.ListBox1.Items.Clear()
                For j = 0 To CurrentWorld.AllFrames.Count - 1
                    Form2.ListBox1.Items.Add("Animation (" & j & ")")
                Next
                Form2.Location = New Point(10, 24) + Location
                Form2.Show()
                Enabled = False
                Form2.TopMost = True
                'Me.Controls.Find("Panelx", True)(0).BringToFront()

        End Select
        Loading = False
    End Sub
    Public Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click


        Dim fname$ = ""
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'get file + file ext
            fname$ = sfd.FileName

            imod = ImportMode.YUI

            If LCase(Strings.Right(fname, 3)) = "txt" Then
                imod = ImportMode.WConsole
            End If
            If LCase(Strings.Right(fname, 9)) = "framelist" Then
                imod = ImportMode.WConsole
            End If



            'Exports 
            Select Case imod
                Case ImportMode.WConsole
                    SaveWConsole(fname)
                Case ImportMode.YUI
                    SaveYUIfile(fname)
                    Saved = True

            End Select

        End If
    End Sub
    Dim Fr0 As Frame
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click ', Button20.Click

        Label6.Show()
        Label7.Hide()
        Loading = True
        Panel16.Enabled = False
        Panel4.Enabled = False
        Panel6.Enabled = False
        Panel7.Enabled = False

        Button9.Enabled = False
        Button10.Enabled = False
        LatestFrame = 0
        mStep = 0

        Timer2.Start()
        Loading = False
    End Sub
    Public LatestFrame = 0
    Public mStep = 0
    Dim random As New Random(0)
    Dim linDisp!
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        If Frames(0).Delay = 0 Then 'Never should it be here
            Debugx("Frames(" & LatestFrame & ").time = 0")
            MsgBox("Frames(" & LatestFrame & ").time = 0", MsgBoxStyle.Critical, "Error")
            Timer2.Stop()
            Exit Sub
        End If



        Label6.Text = "|> Playing (" & LatestFrame & ")"

        If Frames.Count = 0 Then
            Button5.PerformClick() 'Stop
        End If


        'different previews :-/
        Select Case Frames(LatestFrame).Type
            Case AnimType.Static_ 'STATIC
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = Frames(LatestFrame).UV(3)
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = Frames(LatestFrame).UV(0)
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = Frames(LatestFrame).UV(2)
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = Frames(LatestFrame).UV(1)

                PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + Frames(LatestFrame).Tex) & ".bmp")

                If Frames(LatestFrame).Delay <> 0 Then
                    Timer2.Interval = Frames(LatestFrame).Delay 'keep same as last!
                End If

                ' If LatestFrame >= Frames.Count - 1 Then
                'LatestFrame = -1 'rewind, attempt to recover
                ' End If

                LatestFrame += 1
                If LatestFrame >= Frames.Count Then LatestFrame = 0

            Case AnimType.Linear_ 'LINEAR
                If Frames.Count <= LatestFrame + 1 Then
                    Timer2.Stop()
                    MsgBox("Frames(" & LatestFrame + 1 & ") doesn't exist which is required for linear animation")
                    Exit Sub
                End If
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = Frames(LatestFrame).UV(3) + (Frames(LatestFrame + 1).UV(3) - Frames(LatestFrame).UV(3)) * mStep / Frames(LatestFrame).ImageCount
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = Frames(LatestFrame).UV(0) + (Frames(LatestFrame + 1).UV(0) - Frames(LatestFrame).UV(0)) * mStep / Frames(LatestFrame).ImageCount
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = Frames(LatestFrame).UV(2) + (Frames(LatestFrame + 1).UV(2) - Frames(LatestFrame).UV(2)) * mStep / Frames(LatestFrame).ImageCount
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = Frames(LatestFrame).UV(1) + (Frames(LatestFrame + 1).UV(1) - Frames(LatestFrame).UV(1)) * mStep / Frames(LatestFrame).ImageCount

                PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + Frames(LatestFrame).Tex) & ".bmp")

                If Frames(LatestFrame).Delay = 0 Then
                    MsgBox("Frames(" & LatestFrame & ").time = 0", MsgBoxStyle.Critical, "Error")
                    Timer2.Stop()
                    Exit Sub
                End If


                Timer2.Interval = Math.Max(1, Frames(LatestFrame).Delay / Frames(LatestFrame).ImageCount)
                mStep += 1
                If mStep >= Frames(LatestFrame).ImageCount Then
                    LatestFrame += 1
                    mStep = 0
                    If LatestFrame >= Frames.Count Then LatestFrame = 0
                End If



            Case AnimType.Rotation_  'ROTATION

                If Frames.Count <= LatestFrame + 1 Then
                    Timer2.Stop()
                    MsgBox("Frames(" & LatestFrame + 1 & ") doesn't exist which is required for linear animation")
                    Exit Sub
                End If


                Dim PivotU = (Frames(LatestFrame).UV(0) + Frames(LatestFrame).UV(1) + Frames(LatestFrame).UV(2) + Frames(LatestFrame).UV(3)) / 4
                ' Dim PivotV = (Frames(LatestFrame + 1).UV(0) + Frames(LatestFrame + 1).UV(1) + Frames(LatestFrame + 1).UV(2) + Frames(LatestFrame + 1).UV(3)) / 4

                ' Dim u = Frames(LatestFrame).UV(1) - PivotU
                ' Dim v = Frames(LatestFrame + 1).UV(1) - PivotV

                ' Dim Theta = Math.Abs(Math.Atan((+u.X * v.Y - v.X * u.Y) / (u.X * v.X + u.Y * v.Y)))
                ' Dim Theta = Math.Asin(u.X * v.Y - v.X * u.Y) / (Math.Sqrt(u.X ^ 2 + u.Y ^ 2) * Math.Sqrt(v.X ^ 2 + v.Y ^ 2))
                ' Dim Theta = Math.Acos(u.X * v.X + v.Y * u.Y) / (Math.Sqrt(u.X ^ 2 + u.Y ^ 2) * Math.Sqrt(v.X ^ 2 + v.Y ^ 2))


                Mat.CreateRotationMatrix((Frames(LatestFrame + 1).Rotation - Frames(LatestFrame).Rotation) / Frames(LatestFrame).ImageCount)



                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = Mat * (Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords - PivotU) + PivotU
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = Mat * (Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords - PivotU) + PivotU
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = Mat * (Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords - PivotU) + PivotU
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = Mat * (Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords - PivotU) + PivotU

                PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + Frames(LatestFrame).Tex) & ".bmp")

                If Frames(LatestFrame).Delay = 0 Then
                    MsgBox("Frames(" & LatestFrame & ").time = 0", MsgBoxStyle.Critical, "Error")
                    Timer2.Stop()
                    Exit Sub
                End If


                Timer2.Interval = Math.Max(1, Frames(LatestFrame).Delay / Frames(LatestFrame).ImageCount)
                mStep += 1
                If mStep >= Frames(LatestFrame).ImageCount Then
                    LatestFrame += 1
                    mStep = 0
                    If LatestFrame >= Frames.Count Then LatestFrame = 0
                End If



            Case AnimType.Grid_ 'Grid
                'TODO: Grid 2
                '3 0 2 1'

                '3 2 
                '0 1'
                Dim ofx! = Math.Min(Frames(LatestFrame).UV(3).X, Frames(LatestFrame).UV(0).X)
                Dim ofy! = Math.Min(Frames(LatestFrame).UV(0).Y, Frames(LatestFrame).UV(1).Y)
                Dim sx! = (Frames(LatestFrame).UV(1).X - Frames(LatestFrame).UV(0).X) / Math.Sqrt(Frames(LatestFrame).ImageCount)
                Dim sy! = (Frames(LatestFrame).UV(3).Y - Frames(LatestFrame).UV(0).Y) / Math.Sqrt(Frames(LatestFrame).ImageCount)

                Dim cy = Math.Floor(mStep / Math.Sqrt(Frames(LatestFrame).ImageCount))
                Dim cx = (mStep / Math.Sqrt(Frames(LatestFrame).ImageCount) - Math.Floor(mStep / Math.Sqrt(Frames(LatestFrame).ImageCount))) * Math.Sqrt(Frames(LatestFrame).ImageCount)



                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = New IrrlichtNETCP.Vector2D((cx + 0) * sx + ofx, (cy + 1) * sy + ofy)
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = New IrrlichtNETCP.Vector2D((cx + 0) * sx + ofx, (cy + 0) * sy + ofy)
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = New IrrlichtNETCP.Vector2D((cx + 1) * sx + ofx, (cy + 1) * sy + ofy)
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = New IrrlichtNETCP.Vector2D((cx + 1) * sx + ofx, (cy + 0) * sy + ofy)

                PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + Frames(LatestFrame).Tex) & ".bmp")

                If Frames(LatestFrame).Delay = 0 Then
                    MsgBox("Frames(" & LatestFrame & ").time = 0", MsgBoxStyle.Critical, "Error")
                    Timer2.Stop()
                    Exit Sub
                End If


                Timer2.Interval = Math.Max(1, Frames(LatestFrame).Delay / Frames(LatestFrame).ImageCount)
                mStep += 1
                If mStep >= Frames(LatestFrame).ImageCount Then
                    LatestFrame += 1
                    mStep = 0
                    If LatestFrame >= Frames.Count Then LatestFrame = 0
                End If




            Case AnimType.Shake_ 'Shake


                '0 1 
                '3 2'
                If mStep = 0 Then 'Random Seed reset at first step!
                    random = New Random(NumericUpDown1.Value)

                End If

                Dim ofx As Single, ofy As Single, dispVec As IrrlichtNETCP.Vector2D
                Try
                    ofx! = Math.Abs(Frames(LatestFrame).UV(1).X - Frames(LatestFrame).UV(0).X) * Frames(LatestFrame).NoiseLevel / 100 * (random.NextDouble() - 0.5) * 2 ' Single.Parse(TextBox6.Text)
                    ofy! = Math.Abs(Frames(LatestFrame).UV(3).Y - Frames(LatestFrame).UV(0).Y) * Frames(LatestFrame).NoiseLevel / 100 * (random.NextDouble() - 0.5) * 2 'Single.Parse(TextBox6.Text)
                    dispVec = New IrrlichtNETCP.Vector2D(ofx, ofy)
                Catch ex As Exception

                End Try




                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = Frames(LatestFrame).UV(3) + dispVec
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = Frames(LatestFrame).UV(0) + dispVec
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = Frames(LatestFrame).UV(2) + dispVec
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = Frames(LatestFrame).UV(1) + dispVec

                PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + Frames(LatestFrame).Tex) & ".bmp")

                If Frames(LatestFrame).Delay = 0 Then
                    MsgBox("Frames(" & LatestFrame & ").time = 0", MsgBoxStyle.Critical, "Error")
                    Timer2.Stop()
                    Exit Sub
                End If


                Timer2.Interval = Math.Max(1, Frames(LatestFrame).Delay / Frames(LatestFrame).ImageCount)
                mStep += 1
                If mStep >= Frames(LatestFrame).ImageCount Then
                    LatestFrame += 1
                    mStep = 0
                    If LatestFrame >= Frames.Count Then LatestFrame = 0
                End If



                ' ---------------------------------- Linear w/ wo/ Smooth (In/Out/Inout)-----------------------------------------------------------------'
            Case AnimType.Linear_
                linDisp! = Animation_Linear(mStep, Frames(LatestFrame).ImageCount)
                GoTo linear_movements

            Case AnimType.LSmooth_
                linDisp = Animation_SmoothCos(mStep, Frames(LatestFrame).ImageCount)
                GoTo linear_movements

            Case AnimType.LSmthBmerng_
                linDisp = Animation_SmoothCosBoomerang(mStep, Frames(LatestFrame).ImageCount)
                GoTo linear_movements


            Case AnimType.LEaseIn_
                linDisp = Animation_EaseInQuad(mStep, Frames(LatestFrame).ImageCount)
                GoTo linear_movements

            Case AnimType.LAccIn_
                linDisp = Animation_AccInSqrt(mStep, Frames(LatestFrame).ImageCount)
                GoTo linear_movements

            Case AnimType.LEaseOut_
                linDisp = Animation_EaseOutQuad(mStep, Frames(LatestFrame).ImageCount)
                GoTo linear_movements

            Case AnimType.LEaseInOut_
                linDisp = Animation_EaseInOutArctanAsSigmoid(mStep, Frames(LatestFrame).ImageCount)
                GoTo linear_movements

            Case AnimType.Linear_


linear_movements:

                If Frames.Count <= LatestFrame + 1 Then
                    Timer2.Stop()
                    MsgBox("Frames(" & LatestFrame + 1 & ") doesn't exist which is required for linear animation")
                    Exit Sub
                End If

                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = Frames(LatestFrame).UV(3) + (Frames(LatestFrame + 1).UV(3) - Frames(LatestFrame).UV(3)) * linDisp
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = Frames(LatestFrame).UV(0) + (Frames(LatestFrame + 1).UV(0) - Frames(LatestFrame).UV(0)) * linDisp
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = Frames(LatestFrame).UV(2) + (Frames(LatestFrame + 1).UV(2) - Frames(LatestFrame).UV(2)) * linDisp
                Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = Frames(LatestFrame).UV(1) + (Frames(LatestFrame + 1).UV(1) - Frames(LatestFrame).UV(1)) * linDisp

                PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + Frames(LatestFrame).Tex) & ".bmp")

                If Frames(LatestFrame).Delay = 0 Then
                    MsgBox("Frames(" & LatestFrame & ").time = 0", MsgBoxStyle.Critical, "Error")
                    Timer2.Stop()
                    Exit Sub
                End If


                Timer2.Interval = Math.Max(1, Frames(LatestFrame).Delay / Frames(LatestFrame).ImageCount)
                mStep += 1
                If mStep >= Frames(LatestFrame).ImageCount Then
                    LatestFrame += 1
                    mStep = 0
                    If LatestFrame >= Frames.Count Then LatestFrame = 0
                End If





        End Select


    


     


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Panel16.Enabled = True
        Panel4.Enabled = True
        ' Panel6.Enabled = True
        'Panel7.Enabled = True
        EnableDisablePanels()
        Button9.Enabled = True
        Button10.Enabled = True

        Timer2.Stop()
        LoadFrame(CurrentFrame)
        UpdateUV()
        PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + Frames(CurrentFrame).Tex) & ".bmp")
        Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = Frames(CurrentFrame).UV(3)
        Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = Frames(CurrentFrame).UV(0)
        Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = Frames(CurrentFrame).UV(2)
        Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = Frames(CurrentFrame).UV(1)




        EnableDisablePanels()
    End Sub
    Public LastPlay = False
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Label6.Hide()
        Label7.Show()
        Timer2.Stop()
     
        Panel16.Enabled = True
        Panel4.Enabled = True
        ' Panel6.Enabled = True
        'Panel7.Enabled = True
        EnableDisablePanels()
        Button9.Enabled = True
        Button10.Enabled = True

        LoadFrame(CurrentFrame)
        PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + Frames(CurrentFrame).Tex) & ".bmp")


    End Sub

    Private Sub Panel13_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel13.Paint

    End Sub

    Private Sub Panel12_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel12.Paint

    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Timer3.Interval = 100
        If MouseButtons <> Windows.Forms.MouseButtons.None Then
            If Cursor.Position.X > Me.Location.X + Panel18.Location.X And _
Cursor.Position.Y > Me.Location.Y + Panel18.Location.Y And _
                Cursor.Position.X < Me.Location.X + Panel18.Location.X + Panel18.Width And _
Cursor.Position.Y < Me.Location.Y + Panel18.Location.Y + Panel18.Height Then
            Else
                Timer3.Interval = 1000
                Timer3.Stop()
                Panel18.Hide()
            End If
        End If
    End Sub
    Function cSingle(ByVal str$) As Single 'Für Verrückten, die wollen irgendwie beiden Decimalseparator nutzen, Vielleicht ¨ware es eine güte Idee, die UI Globalisierung Parameter um Global US parameter zu setzen
        Try
            If InStr(12.5, ",") > 0 Then
                str = Replace(str, ".", ",")
            Else
                str = Replace(str, ",", ".")

            End If

            Return CSng(str)
        Catch ex As Exception

        End Try

    End Function
    'Delay textbox
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            Frames(CurrentFrame).Delay = cSingle(TextBox1.Text)

        Catch ex As Exception

        End Try
    End Sub
    'Noise level text box
    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        If Frames.Count = 0 Or Frames.Count < CurrentFrame Then Exit Sub
        Try
            Frames(CurrentFrame).NoiseLevel = cSingle(TextBox6.Text)

        Catch ex As Exception

        End Try
    End Sub
    'Image count text box
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If Frames.Count = 0 Then Exit Sub

        Frames(CurrentFrame).ImageCount = cSingle(TextBox3.Text)

    End Sub

 
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        If CurrentFrame - 1 < 0 Then
            Beep()
            Exit Sub
        End If

        Frames(CurrentFrame).Delay = Frames(CurrentFrame - 1).Delay
        Frames(CurrentFrame).Image = Frames(CurrentFrame - 1).Image
        Frames(CurrentFrame).UV = Frames(CurrentFrame - 1).UV.Clone
        Frames(CurrentFrame).Tex = Frames(CurrentFrame - 1).Tex
        Frames(CurrentFrame).Type = Frames(CurrentFrame - 1).Type
        Frames(CurrentFrame).cloned = False
        LoadFrame(CurrentFrame)

        Panel18.Hide()

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If CurrentFrame - 1 < 0 Then
            Beep()
            Exit Sub
        End If

        Frames(CurrentFrame).Delay = Frames(CurrentFrame - 1).Delay
        Frames(CurrentFrame).Image = Frames(CurrentFrame - 1).Image
        Frames(CurrentFrame).ImageCount = Frames(CurrentFrame - 1).ImageCount
        Frames(CurrentFrame).Rotation = Frames(CurrentFrame - 1).Rotation
        Frames(CurrentFrame).UV = Frames(CurrentFrame - 1).UV
        Frames(CurrentFrame).Tex = Frames(CurrentFrame - 1).Tex
        Frames(CurrentFrame).Type = Frames(CurrentFrame - 1).Type
        Frames(CurrentFrame).cloned = True
        LoadFrame(CurrentFrame)


        Panel18.Hide()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Frames(CurrentFrame).UV(0) = New IrrlichtNETCP.Vector2D(0, 0)
        Frames(CurrentFrame).UV(1) = New IrrlichtNETCP.Vector2D(1, 0)
        Frames(CurrentFrame).UV(2) = New IrrlichtNETCP.Vector2D(1, 1)
        Frames(CurrentFrame).UV(3) = New IrrlichtNETCP.Vector2D(0, 1)
        LoadFrame(CurrentFrame)
        Frames(CurrentFrame).cloned = False
        Panel18.Hide()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        If ComboBox2.SelectedIndex = 0 Then
            Beep()
            Exit Sub
        End If

        Panel8.Location = Panel18.Location
        Panel8.Show()
        Panel18.Hide()
        TextBox2.Text = TextBox1.Text
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked Then State = EditModes.Select_
    End Sub

    'Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If RadioButton1.Checked Then State = EditModes.Move_

    ' End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then State = EditModes.Rotate_
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then State = EditModes.Rect_
    End Sub

    Private Sub pl_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Label6.Visible Then
            Label6.Hide()
        Else
            Label6.Show()
        End If
    End Sub

    Private Sub TimerForAnim_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerForAnim.Tick


        If Form2.Visible Then

            If Form2.ListBox1.SelectedIndex = -1 Then Exit Sub
            If CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex).Count < LatestFrame Then LatestFrame = 0
            Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex)(LatestFrame).UV(Split(Form2.ListBox2.SelectedItem, ",")(0))
            Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex)(LatestFrame).UV(Split(Form2.ListBox2.SelectedItem, ",")(1))
            Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex)(LatestFrame).UV(Split(Form2.ListBox2.SelectedItem, ",")(2))
            Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex)(LatestFrame).UV(Split(Form2.ListBox2.SelectedItem, ",")(3))



            PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex)(LatestFrame).Tex) & ".bmp")




            'Error, timer = 0
            If CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex)(LatestFrame).Delay = 0 Then
                MsgBox("Frames(" & LatestFrame & ").time = 0", MsgBoxStyle.Critical, "Error")
                TimerForAnim.Stop()
                Exit Sub
            End If


            TimerForAnim.Interval = CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex)(LatestFrame).Delay '* 1000
            LatestFrame += 1
            If LatestFrame >= CurrentWorld.AllFrames(Form2.ListBox1.SelectedIndex).Count Then LatestFrame = 0
        ElseIf W_Control.Visible Then

            If W_Control.ListBox1.SelectedIndex = -1 Then Exit Sub
            If CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex).Count < LatestFrame Then LatestFrame = 0
            Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex)(LatestFrame).UV(3)
            Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex)(LatestFrame).UV(0)
            Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex)(LatestFrame).UV(2)
            Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex)(LatestFrame).UV(1)



            PScn.GetMaterial(0).Texture1 = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex)(LatestFrame).Tex) & ".bmp")




            'Error, timer = 0
            If CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex)(LatestFrame).Delay = 0 Then
                MsgBox("Frames(" & LatestFrame & ").time = 0", MsgBoxStyle.Critical, "Error")
                TimerForAnim.Stop()
                Exit Sub
            End If


            TimerForAnim.Interval = CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex)(LatestFrame).Delay '* 1000
            LatestFrame += 1
            If LatestFrame >= CurrentWorld.AllFrames(W_Control.ListBox1.SelectedIndex).Count Then LatestFrame = 0



        End If

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Frames(CurrentFrame).Type = ComboBox2.SelectedIndex

        If Frames(CurrentFrame).Type = AnimType.Static_ Then
            TextBox3.Enabled = False
        Else
            TextBox3.Enabled = True
        End If



        If Frames(CurrentFrame).Type = AnimType.Shake_ Then
            TextBox6.Enabled = True
        Else
            TextBox6.Enabled = False
        End If
    End Sub


    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text = "" Then Exit Sub
        Label11.Text = TextBox2.Text / TextBox3.Text & " ms/image"
        If CInt(TextBox2.Text / TextBox3.Text) > 0 Then
            Label11.ForeColor = Color.Green
        Else
            Label11.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click

        Panel8.Hide()
        If Frames.Count <= CurrentFrame + 1 And (Frames(CurrentFrame).Type <> AnimType.Shake_ And Frames(CurrentFrame).Type <> AnimType.Grid_) Then
            Timer2.Stop()
            MsgBox("Recipe is incomplete, you forgot the next frame! >_>", MsgBoxStyle.Critical)
            Exit Sub
        End If
        cook()

    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Panel18.Location = Panel8.Location
        Panel18.Show()
        Panel8.Hide()

    End Sub

    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Panel18.Hide()

        Dim f = InputBox("With which frame?", "select friend-frame") '<> ""
        If f <> "" Then
            If f < 0 Or f >= Frames.Count Then
                MsgBox("Out of bounds!", MsgBoxStyle.Critical)
                Exit Sub
            End If
            Dim tFrame = Frames(CurrentFrame)
            tFrame.UV = Frames(CurrentFrame).UV.Clone

            Frames(CurrentFrame) = Frames(f)
            Frames(CurrentFrame).UV = Frames(f).UV.Clone

            Frames(f) = tFrame
            Frames(f).UV = tFrame.UV.Clone

            LoadFrame(CurrentFrame)

        End If
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim center As New IrrlichtNETCP.Vector2D
        'Center
        For i = 0 To 3
            center += Selectors(i) / 4
        Next

        For i = 0 To 3

            Selectors(i) = Selectors(i) + New IrrlichtNETCP.Vector2D(2 * (center.X - Selectors(i).X), 0)
         
        Next
        UpdateUV()

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim center As New IrrlichtNETCP.Vector2D
        'Center
        For i = 0 To 3
            center += Selectors(i) / 4
        Next

        For i = 0 To 3

            Selectors(i) = Selectors(i) + New IrrlichtNETCP.Vector2D(0, 2 * (center.Y - Selectors(i).Y))

        Next
        UpdateUV()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        SelectPolies.Show()
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        '  If Not cooked() Then
        'MsgBox("But you haven't cooked the animations :-/....", MsgBoxStyle.Exclamation, "hmmm....")
        '  Exit Sub
        '   End If
        W_Control.Show()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MapSelectors()
        UpdateUV()

        For i = 0 To Frames.Count - 1
            Dim u = New IrrlichtNETCP.Vector2D(Frames(i).UV(0).X, Frames(i).UV(0).Y)
            Frames(i).UV(0) = New IrrlichtNETCP.Vector2D(Frames(i).UV(1).X, Frames(i).UV(1).Y)
            Frames(i).UV(1) = New IrrlichtNETCP.Vector2D(Frames(i).UV(2).X, Frames(i).UV(2).Y)
            Frames(i).UV(2) = New IrrlichtNETCP.Vector2D(Frames(i).UV(3).X, Frames(i).UV(3).Y)
            Frames(i).UV(3) = New IrrlichtNETCP.Vector2D(u.X, u.Y)

            u = New IrrlichtNETCP.Vector2D(Selectors(0).X, Selectors(0).Y)
            Selectors(0) = New IrrlichtNETCP.Vector2D(Selectors(1).X, Selectors(1).Y)
            Selectors(1) = New IrrlichtNETCP.Vector2D(Selectors(2).X, Selectors(2).Y)
            Selectors(2) = New IrrlichtNETCP.Vector2D(Selectors(3).X, Selectors(3).Y)
            Selectors(3) = New IrrlichtNETCP.Vector2D(u.X, u.Y)




        Next

    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Dim textbox5_focus As Boolean = False
    Private Sub TextBox5_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox5.GotFocus
        textbox5_focus = True
    End Sub

    Private Sub TextBox5_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox5.LostFocus
        textbox5_focus = False
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        'Blue / Red / Green / Yellow / White / Black

        Select Case ComboBox3.SelectedIndex
            Case 0 : SelectedColor = IrrlichtNETCP.Color.Blue
            Case 1 : SelectedColor = IrrlichtNETCP.Color.Red
            Case 2 : SelectedColor = IrrlichtNETCP.Color.Green
            Case 3 : SelectedColor = IrrlichtNETCP.Color.Yellow
            Case 4 : SelectedColor = IrrlichtNETCP.Color.White
            Case 5 : SelectedColor = IrrlichtNETCP.Color.Black

        End Select
        For i = 0 To 3
            Sel(i).GetMaterial(0).EmissiveColor = SelectedColor

        Next i
        MapSelectors()

    End Sub
End Class
