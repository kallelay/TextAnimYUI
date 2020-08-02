Public Class SelectPolies

    Public FORM_SPOLIES_LOST_FOCUS As Boolean = True
    Dim InitPos As Point
    Dim clicked = False
    Dim started = False
    Dim approX, approY

    Private Sub Panel1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDoubleClick
        Panel4.Location = New Point
        Panel4.Size = Panel1.Size
    End Sub
    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        clicked = True
        Label3.Show()

        Panel1.Controls.Add(Panel4)
    End Sub
    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        Dim selectALL = False
        approX = e.X
        approY = e.Y
        If clicked Then
            If Not started Then
                If e.X - 8 < 0 Then
                    approX = 0
                End If
                If e.Y - 8 < 0 Then
                    approY = 0
                End If

                Panel4.Location = New Point(approX, approY)


                started = True
            Else
                Panel4.Size = e.Location - Panel4.Location
                Label3.Text = String.Format("({0}x{1}) -> ({2}x{3})", Strings.Format(Panel4.Location.X / 256, "0.00"), Strings.Format(Panel4.Location.Y / 256, "0.00"), _
                                     Strings.Format((Panel4.Location.X + Panel4.Width) / 256, "0.00"), _
                                     Strings.Format((Panel4.Location.Y + Panel4.Height) / 256, "0.00"))




            End If


        End If
    End Sub

    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp
        clicked = False
        started = False

        ListBox1.Items.Clear()
        'Calculate UV
        Dim MinVec As New IrrlichtNETCP.Vector2D(Panel4.Location.X / 256, Panel4.Location.Y / 256)
        Dim MaxVec As New IrrlichtNETCP.Vector2D((Panel4.Location.X + Panel4.Width) / 256, (Panel4.Location.Y + Panel4.Height) / 256)


        Dim Inside As Boolean = False
        'Time to search
        For i = 0 To CurrentWorld.meshCount - 1
            Inside = True
            ' CurrentWorld.mMesh(i).ListOfThatTexture.Clear()
            For k = 0 To CurrentWorld.mMesh(i).polynum - 1

                'First, calculating the box!
                Dim minU = Math.Min(Math.Min(CurrentWorld.mMesh(i).polyl(k).u0, CurrentWorld.mMesh(i).polyl(k).u1), CurrentWorld.mMesh(i).polyl(k).u2)
                Dim minV = Math.Min(Math.Min(CurrentWorld.mMesh(i).polyl(k).v0, CurrentWorld.mMesh(i).polyl(k).v1), CurrentWorld.mMesh(i).polyl(k).v2)
                Dim maxU = Math.Max(Math.Max(CurrentWorld.mMesh(i).polyl(k).u0, CurrentWorld.mMesh(i).polyl(k).u1), CurrentWorld.mMesh(i).polyl(k).u2)
                Dim maxV = Math.Max(Math.Max(CurrentWorld.mMesh(i).polyl(k).v0, CurrentWorld.mMesh(i).polyl(k).v1), CurrentWorld.mMesh(i).polyl(k).v2)


                'Second, check if it's in the box
                If minU < MinVec.X Or minV < MinVec.Y Then
                    Inside = False
                    Exit For
                End If

                If maxU > MaxVec.X Or maxV > MaxVec.Y Then
                    Inside = False
                    Exit For
                End If

                If CurrentWorld.mMesh(i).polyl(k).tpage <> ComboBox1.SelectedIndex Then
                    Inside = False
                    Exit For
                End If




            Next

            If Inside = False Then Continue For

            ListBox1.Items.Add("Mesh n°" & i)

        Next
    End Sub

    Private Sub SelectPolies_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        FORM_SPOLIES_LOST_FOCUS = False
    End Sub

    Private Sub SelectPolies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate

        FORM_SPOLIES_LOST_FOCUS = True
    End Sub


    Private Sub SelectPolies_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If MouseButtons = Windows.Forms.MouseButtons.Left Then
            Me.Location += Cursor.Position - InitPos
        End If
        InitPos = Cursor.Position
    End Sub

    Private Sub SelectPolies_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Panel16_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If CurrentWorld Is Nothing Then Exit Sub
        Try
            If IO.File.Exists(CurrentWorld.Directory & CurrentWorld.DirectoryName & ComboBox1.Text & ".bmp") Then
                Panel1.BackgroundImage = Image.FromFile(CurrentWorld.Directory & CurrentWorld.DirectoryName & ComboBox1.Text & ".bmp")
            Else
                Panel1.BackgroundImage = Nothing
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        CurrentWorld.ExportMesh(Split(ListBox1.Text, "°")(1))
        Dim allNV = Process.GetProcessesByName("nvolt")
        For i = 0 To allNV.Count - 1
            allNV(i).Kill()
        Next


        Dim Proces = _
        Process.Start("nvolt", Chr(34) & CurrentWorld.Directory & "\Temp_preview_mesh.prm" & Chr(34))


        Do Until FindWindow(Nothing, "nVolt") <> 0
            Application.DoEvents()
        Loop
        Dim f = FindWindow(Nothing, "nVolt")
        SetWindowLong(f, GWL_STYLE, WS_VISIBLE + WS_MAXIMIZE)
        Do Until FindWindow(Nothing, "nVolt") <> 0
            Application.DoEvents()
        Loop
        SetParent(FindWindow(Nothing, "nVolt"), PRMpreview.Handle)
        PRMpreview.Show()


    End Sub


    Dim myData$
    Private Sub SelectPolies_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If CurrentWorld Is Nothing Then
            Dim Th = New Threading.Thread(AddressOf StartThread)
            myData = Main.TextBox1.Text
            ' Threading.Thread.SetData(mySlot, Main.TextBox1.Text)
            Th.Start()
        End If
        ComboBox1.SelectedIndex = 0



    End Sub
    Sub StartThread()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf StartThread))
            Exit Sub
        End If
        CurrentWorld = New WORLD(myData)
        ComboBox1.SelectedIndex = 0
        If IO.File.Exists(CurrentWorld.Directory & CurrentWorld.DirectoryName & ComboBox1.Text & ".bmp") Then
            Panel1.BackgroundImage = Image.FromFile(CurrentWorld.Directory & CurrentWorld.DirectoryName & ComboBox1.Text & ".bmp")
        Else
            Panel1.BackgroundImage = Nothing
        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If ListBox1.SelectedIndex = -1 Then
            MsgBox("first select a mesh!")
            Exit Sub
        End If
        CurrentMesh = Split(ListBox1.Text, "°")(1)
        Dim Curmesh = CurrentWorld.mMesh(CurrentMesh)
        Dim iMesh As New Worldf
        iMesh.bbox = Curmesh.bbox
        iMesh.BoundingSphere = Curmesh.BoundingSphere
        iMesh.vexl = Curmesh.vexl
        iMesh.vertnum = Curmesh.vertnum
        iMesh.polynum = Curmesh.polynum \ 2
        ReDim iMesh.polyl(iMesh.polynum)


        Dim found = False
        For i = 0 To Curmesh.polynum - 1
            found = found Or (Curmesh.polyl(i).type And 1)
        Next

        If found Then
            MsgBox("Aborting, Quad meshes were found", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Curmesh.polynum \ 2 <> Curmesh.polynum / 2 Then
            MsgBox("Aborting, Meshes aren't even (they're odd)", MsgBoxStyle.Exclamation)
            Exit Sub
        End If


        For i = 0 To (Curmesh.polynum - 1) \ 2
            iMesh.polyl(i).type += Curmesh.polyl(i * 2).type + 1
            iMesh.polyl(i).tpage = Curmesh.polyl(i * 2).tpage
            iMesh.polyl(i).vi0 = Curmesh.polyl(i * 2).vi0
            iMesh.polyl(i).vi1 = Curmesh.polyl(i * 2).vi1
            iMesh.polyl(i).vi2 = Curmesh.polyl(i * 2).vi2
            iMesh.polyl(i).vi3 = Curmesh.polyl(i * 2 + 1).vi1

            iMesh.polyl(i).c0 = Curmesh.polyl(i * 2).c0
            iMesh.polyl(i).c1 = Curmesh.polyl(i * 2).c1
            iMesh.polyl(i).c2 = Curmesh.polyl(i * 2).c2
            iMesh.polyl(i).c3 = Curmesh.polyl(i * 2 + 1).c1

            iMesh.polyl(i).u0 = Curmesh.polyl(i * 2).u0
            iMesh.polyl(i).v0 = Curmesh.polyl(i * 2).v0
            iMesh.polyl(i).u1 = Curmesh.polyl(i * 2).u1
            iMesh.polyl(i).v1 = Curmesh.polyl(i * 2).v1
            iMesh.polyl(i).u2 = Curmesh.polyl(i * 2).u2
            iMesh.polyl(i).v2 = Curmesh.polyl(i * 2).v2
            iMesh.polyl(i).u3 = Curmesh.polyl(i * 2 + 1).u1
            iMesh.polyl(i).v3 = Curmesh.polyl(i * 2 + 1).v1

        Next

        CurrentWorld.mMesh(CurrentMesh) = iMesh

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        If Not cooked() Then
            'TODO: auto cook!
            MsgBox("- Darling... Dinner isn't ready yet!" & vbNewLine & "- Cook it Cook it!" & vbNewLine, MsgBoxStyle.Critical)
            Exit Sub
        End If
        Dim Score% = 513
        If CheckBox1.Checked Then Score += 2 ^ 11
        If CheckBox2.Checked Then Score += 2
        If CheckBox3.Checked Then Score += 4
        If CheckBox4.Checked Then Score += 8

        CurrentWorld.AllFrames.Add(Frames)
        CurrentMesh = Split(ListBox1.Text, "°")(1)
        For i = 0 To CurrentWorld.mMesh(CurrentMesh).polynum - 1
            CurrentWorld.mMesh(CurrentMesh).polyl(i).tpage = CurrentWorld.AllFrames.Count - 1
            CurrentWorld.mMesh(CurrentMesh).polyl(i).type = Score
        Next
        IO.File.Copy(Main.TextBox1.Text, My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & Int(Rnd() * 2000) & Split(Main.TextBox1.Text, "\").Last)
        CurrentWorld.Save(Main.TextBox1.Text)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        If W_Control.ListBox1.SelectedIndex = -1 Then
            MsgBox("Please select from here your tex anim then assign again!")
            W_Control.Show()
            Exit Sub
        End If
        Dim Score% = 513
        If CheckBox1.Checked Then Score += 2 ^ 11
        If CheckBox2.Checked Then Score += 2
        If CheckBox3.Checked Then Score += 4
        If CheckBox4.Checked Then Score += 8

        CurrentMesh = Split(ListBox1.Text, "°")(1)
        For i = 0 To CurrentWorld.mMesh(CurrentMesh).polynum - 1
            CurrentWorld.mMesh(CurrentMesh).polyl(i).tpage = W_Control.ListBox1.SelectedIndex
            CurrentWorld.mMesh(CurrentMesh).polyl(i).type += Score
        Next
        IO.File.Copy(Main.TextBox1.Text, My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & Int(Rnd() * 2000) & Split(Main.TextBox1.Text, "\").Last)
        CurrentWorld.Save(Main.TextBox1.Text)
    End Sub

    Private Sub Panel4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel4.Click
        Panel4.Height = 0
        Panel4.Width = 0
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Hide()
    End Sub
End Class