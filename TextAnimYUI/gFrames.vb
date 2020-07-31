Imports IrrlichtNETCP
Module gFrames


    Public Class Frame
        Public ImageCount As Integer = 16
        Public Rotation As Single 'required, better than doing problems....
        Public Image As Drawing.Image = Nothing
        Public Tex As Short
        Public Type As AnimType = AnimType.Static_
        Public Delay! = 30
        Public UV(4) As Vector2D
        Public Cloned As Boolean = False

        Sub New()
            UV(0) = New Vector2D(0, 0)
            UV(1) = New Vector2D(1, 0)
            UV(2) = New Vector2D(1, 1)
            UV(3) = New Vector2D(0, 1)
        End Sub
    End Class
    Public Frames As New List(Of Frame)
    Public CurrentFrame = 0          'frame in editor
    Public firstFrameInRow = 0        'new name: first frame in list
    '  Public CurrentInDisplayFrame = 0 'first frame in the list
    Sub InitFrames()
        Frames.Add(New Frame)
        LoadFrame(0)

    End Sub
    Sub LoadFrame(ByVal id%)
        'Don't load frame in case there is no frame
        If Frames.Count = 0 Then Exit Sub
        For i = 0 To 3
            Selectors(i) = Frames(id).UV(i)
        Next
        'Frames(CurrentFrame).Rotation += DeltaMousePos.X
        Form1.Label14.Text = CInt(Frames(CurrentFrame).Rotation * 180 / Math.PI) & "°"
        Select Case Form1.Label14.Text
            Case "0°"
                Form1.Label13.Text = "~ 0"
            Case "30°"
                Form1.Label13.Text = "~ π/6"
            Case "45°"
                Form1.Label13.Text = "~ π/4"
            Case "60°"
                Form1.Label13.Text = "~ π/3"
            Case "90°"
                Form1.Label13.Text = "~ π/2"
            Case "120°"
                Form1.Label13.Text = "~ 2π/3"
            Case "135°"
                Form1.Label13.Text = "~ 3π/4"
            Case "150°"
                Form1.Label13.Text = "~ 5π/6"
            Case "180°"
                Form1.Label13.Text = "~ π"
            Case "360°"
                Form1.Label13.Text = "~ 2π"
            Case Else
                Form1.Label13.Text = "~ " & Strings.Format((Replace(Form1.Label14.Text, "°", "") * Math.PI / 180) / (Math.PI / 6), "0.#0") & "π/6"

        End Select

        Form1.ComboBox1.SelectedIndex = Frames(id).Tex
        UpdateUV()

    End Sub
    Dim CC As New List(Of Drawing.Color)
    Sub InitCC()

        For i = 0 To 100
            Randomize()
            CC.Add(Drawing.Color.FromArgb(Rnd() * 200, Rnd() * 200, Rnd() * 200))
        Next
    End Sub

    Sub SaveFrame(ByVal id%)

        'Don't save frame in case it's playing or frames count  =0
        If Form1.Timer2.Enabled Then Exit Sub
        If Frames.Count = 0 Then Exit Sub

        For i = 0 To 3
            Frames(id).UV(i) = Selectors(i)
        Next

        If Frames(id).Cloned Then

            DirectCast(Form1.Panel4.Controls.Find("Panel" & 12 + id - firstFrameInRow, True)(0), Panel).BackColor = CC((id - firstFrameInRow))
            DirectCast(Form1.Panel4.Controls.Find("Panel" & 11 + id - firstFrameInRow, True)(0), Panel).BackColor = CC((id - firstFrameInRow))

     
        Else
            If Frames.Count <= id + 1 Then GoTo Els
            If Frames(id + 1).Cloned Then GoTo Els

            DirectCast(Form1.Panel4.Controls.Find("Panel" & 12 + id - firstFrameInRow, True)(0), Panel).BorderStyle = BorderStyle.FixedSingle
            If Frames(id).Cloned = False Then DirectCast(Form1.Panel4.Controls.Find("Panel" & 12 + id - firstFrameInRow, True)(0), Panel).BackColor = Drawing.Color.Gray
            If id - firstFrameInRow > 0 Then If Frames(id - 1).Cloned = False Then DirectCast(Form1.Panel4.Controls.Find("Panel" & 11 + id - firstFrameInRow, True)(0), Panel).BackColor = Drawing.Color.Gray


        End If
els:
        If id - firstFrameInRow >= 0 Then
            Frames(id).Image = Form1.Panel4.Controls.Find("Panel" & 12 + id - firstFrameInRow, True)(0).BackgroundImage

        End If
        Frames(id).Tex = Form1.ComboBox1.SelectedIndex
        If id + 1 < Frames.Count Then
            If Frames(id + 1).Cloned Then Frames(id + 1).Tex = Form1.ComboBox1.SelectedIndex
            If Frames(id + 1).Cloned Then If id < 3 Then Form1.Panel4.Controls.Find("Panel" & 13 + id - firstFrameInRow, True)(0).BackgroundImage = Frames(id).Image
        End If
        ' Debugger.Break()
    End Sub
    Enum AnimType
        Static_ = 0
        linear_ = 1
        Rotation_ = 2
        grid_ = 3
        smooth_
        smoothstart_
        smoothend_

    End Enum
End Module
