'Imports 

Imports System.ComponentModel

Public Class texanimWExport

    Public FORM_SPOLIES_LOST_FOCUS As Boolean = True
    Dim InitPos As Point
    Dim clicked = False
    Dim started = False
    Dim approX, approY


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
        '  ComboBox1.SelectedIndex = 0
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CurrentWorld Is Nothing Then Exit Sub
        Try
            '    If IO.File.Exists(CurrentWorld.Directory & CurrentWorld.DirectoryName & ComboBox1.Text & ".bmp") Then
            '    Panel1.BackgroundImage = Image.FromFile(CurrentWorld.Directory & CurrentWorld.DirectoryName & ComboBox1.Text & ".bmp")
            '    Else
            '   Panel1.BackgroundImage = Nothing
            '   End If



        Catch ex As Exception

        End Try
    End Sub

    Public isLoading = True
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        isLoading = True
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        If nVolt_Main.isInitialized = False Then Exit Sub


        'clone world

        For k = thisWorldFile.texAnimHandlerList.Count - 1 To 0 Step -1
            thisWorldFile.texAnimHandlerList(k).Stop()
            thisWorldFile.texAnimHandlerList(k) = Nothing
        Next

        thisWorldFile = Nothing
        thisWorldFile = theOriginalWorldFile.Clone

        'Add listbox1 to nList render list
        Dim nList As New List(Of Integer)
        For Each it In ListBox1.SelectedIndices
            nList.Add(it)
        Next

        'Add listbox3
        ListBox3.Items.Clear()
        For k = 0 To thisWorldFile.worldLoad.mMesh(ListBox1.SelectedIndex).polynum - 1
            ListBox3.Items.Add(k)
            '  ListBox3.SelectedIndices.Add(k)
        Next



        ' nList.AddRange(Me.ListBox1.Items)
        thisWorldFile.Render(nList)

        nVolt_cam.Position = thisWorldFile.CoM / thisWorldFile.cCoM + (thisWorldFile.RenderBoundingBox.MaxEdge - thisWorldFile.RenderBoundingBox.MinEdge).Length * New IrrlichtNETCP.Vector3D(1, 1, 1) * nVolt_Zoom
        nVolt_cam.Target = thisWorldFile.CoM / thisWorldFile.cCoM





        ' nVolt_ScnMgr.RootSceneNode.Position =-

        For Each item In thisWorldFile.texAnimHandlerList
            item.Play()
        Next
        isLoading = False


        applyTA()

    End Sub


    Dim myData$


    Sub StartThread()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf StartThread))
            Exit Sub
        End If
        CurrentWorld = New WORLD(myData)
        '  ComboBox1.SelectedIndex = 0
        '  If IO.File.Exists(CurrentWorld.Directory & CurrentWorld.DirectoryName & ComboBox1.Text & ".bmp") Then
        '  Panel1.BackgroundImage = Image.FromFile(CurrentWorld.Directory & CurrentWorld.DirectoryName & ComboBox1.Text & ".bmp")
        ''  Else
        '  Panel1.BackgroundImage = Nothing
        ' End If


    End Sub
    Public Sub Swap(Of T)(ByRef d1 As T, ByRef d2 As T)
        Dim d = d2
        d2 = d1
        d1 = d
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If ListBox1.SelectedIndex = -1 Then
            MsgBox("first select a mesh!")
            Exit Sub
        End If
        CurrentMesh = ListBox1.SelectedIndex
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


        'check which indices to go with
        Dim i0 = Curmesh.polyl(1).vi0
        Dim i1 = Curmesh.polyl(1).vi1
        Dim i2 = Curmesh.polyl(1).vi2

        Dim index_to_use = 0
        If i0 <> Curmesh.polyl(0).vi0 And i0 <> Curmesh.polyl(0).vi1 And i0 <> Curmesh.polyl(0).vi2 Then
            index_to_use = 0
        ElseIf i1 <> Curmesh.polyl(0).vi0 And i1 <> Curmesh.polyl(0).vi0 And i1 <> Curmesh.polyl(0).vi2 Then
            index_to_use = 1
        ElseIf i2 <> Curmesh.polyl(0).vi0 And i2 <> Curmesh.polyl(0).vi0 And i2 <> Curmesh.polyl(0).vi2 Then
            index_to_use = 2
        Else
            MsgBox("Permaerror, cannot find the extra index", MsgBoxStyle.Critical)
            Exit Sub
        End If







        For i = 0 To (Curmesh.polynum - 1) \ 2
            iMesh.polyl(i).type += Curmesh.polyl(i * 2).type + 1
            iMesh.polyl(i).tpage = Curmesh.polyl(i * 2).tpage
            iMesh.polyl(i).vi0 = Curmesh.polyl(i * 2).vi0
            iMesh.polyl(i).vi1 = Curmesh.polyl(i * 2).vi1
            iMesh.polyl(i).vi2 = Curmesh.polyl(i * 2).vi2
            Select Case index_to_use
                Case 0 : iMesh.polyl(i).vi3 = Curmesh.polyl(i * 2 + 1).vi0
                Case 1 : iMesh.polyl(i).vi3 = Curmesh.polyl(i * 2 + 1).vi1
                Case 2 : iMesh.polyl(i).vi3 = Curmesh.polyl(i * 2 + 1).vi2
            End Select
            If index_to_use = 2 Then swap(iMesh.polyl(i).vi2, iMesh.polyl(i).vi3)




            iMesh.polyl(i).c0 = Curmesh.polyl(i * 2).c0
            iMesh.polyl(i).c1 = Curmesh.polyl(i * 2).c1
            iMesh.polyl(i).c2 = Curmesh.polyl(i * 2).c2
            Select Case index_to_use
                Case 0 : iMesh.polyl(i).c3 = Curmesh.polyl(i * 2 + 1).c0
                Case 1 : iMesh.polyl(i).c3 = Curmesh.polyl(i * 2 + 1).c1
                Case 2 : iMesh.polyl(i).c3 = Curmesh.polyl(i * 2 + 1).c2
            End Select
            If index_to_use = 2 Then Swap(iMesh.polyl(i).c2, iMesh.polyl(i).c3)

            iMesh.polyl(i).u0 = Curmesh.polyl(i * 2).u0
            iMesh.polyl(i).v0 = Curmesh.polyl(i * 2).v0
            iMesh.polyl(i).u1 = Curmesh.polyl(i * 2).u1
            iMesh.polyl(i).v1 = Curmesh.polyl(i * 2).v1
            iMesh.polyl(i).u2 = Curmesh.polyl(i * 2).u2
            iMesh.polyl(i).v2 = Curmesh.polyl(i * 2).v2
            Select Case index_to_use
                Case 0 : iMesh.polyl(i).u3 = Curmesh.polyl(i * 2 + 1).u0
                Case 1 : iMesh.polyl(i).u3 = Curmesh.polyl(i * 2 + 1).u1
                Case 2 : iMesh.polyl(i).u3 = Curmesh.polyl(i * 2 + 1).u2
            End Select
            Select Case index_to_use
                Case 0 : iMesh.polyl(i).v3 = Curmesh.polyl(i * 2 + 1).v0
                Case 1 : iMesh.polyl(i).v3 = Curmesh.polyl(i * 2 + 1).v1
                Case 2 : iMesh.polyl(i).v3 = Curmesh.polyl(i * 2 + 1).v2
            End Select
            If index_to_use = 2 Then Swap(iMesh.polyl(i).u2, iMesh.polyl(i).u3)
            If index_to_use = 2 Then Swap(iMesh.polyl(i).v2, iMesh.polyl(i).v3)



        Next

        CurrentWorld.mMesh(CurrentMesh) = iMesh
        IO.File.Copy(Main.TextBox1.Text, Main.TextBox1.Text & "_" & Int(Rnd() * 100) & ".bak")
        CurrentWorld.Save(Main.TextBox1.Text)


        nVolt_Main.Main(Gmodule.WorldFile)
        StartThread() '
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        If Not cooked() Then
            'TODO: auto cook!
            MsgBox("- Darling... Dinner isn't ready yet!" & vbNewLine & "- Cook it Cook it!" & vbNewLine, MsgBoxStyle.Critical)
            Exit Sub
        End If
        Dim Score% = 513
        'If CheckBox1.Checked Then Score += 2 ^ 11
        ' If CheckBox2.Checked Then Score += 2
        ' If CheckBox3.Checked Then Score += 4
        'If CheckBox4.Checked Then Score += 8

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
        If ListBox2.SelectedIndex = -1 Then Exit Sub

        If ListBox2.SelectedIndex = ListBox2.Items.Count - 1 Then
            MsgBox("Unimplemented")
            Exit Sub
        End If

        Dim Score% = 513
        '  If CheckBox1.Checked Then Score += 2 ^ 11
        '  If CheckBox2.Checked Then Score += 2
        '  If CheckBox3.Checked Then Score += 4
        '  If CheckBox4.Checked Then Score += 8

        CurrentMesh = ListBox1.SelectedItem
        For Each i In ListBox3.SelectedItems
                CurrentWorld.mMesh(CurrentMesh).polyl(i).tpage = ListBox2.SelectedIndex
            CurrentWorld.mMesh(CurrentMesh).polyl(i).type = 512 Or CurrentWorld.mMesh(CurrentMesh).polyl(i).type

            If ListBox4.SelectedIndex <> 0 Then
                MsgBox("not implemented yet")
            End If

        Next
        IO.File.Copy(Main.TextBox1.Text, My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & Int(Rnd() * 2000) & Split(Main.TextBox1.Text, "\").Last)
        CurrentWorld.Save(Main.TextBox1.Text)
    End Sub



    Private Sub renderWindowPanel_Paint(sender As Object, e As PaintEventArgs) Handles renderWindowPanel.Paint

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub

    Sub applyTA()
        If isLoading Then Exit Sub
        If ListBox3.SelectedIndices.Count = 0 Then Exit Sub
        If ListBox1.SelectedIndex = -1 Then Exit Sub
        If ListBox2.SelectedIndex = -1 Then Exit Sub
        If ListBox4.SelectedIndex = -1 Then ListBox4.SelectedIndex = 0

        Dim ocpos = nVolt_cam.Position
        Dim ocloc = nVolt_cam.Target

        'thisWorldFile.worldLoad.AllFrames.Add()

        thisWorldFile = Nothing
        thisWorldFile = theOriginalWorldFile.Clone()
        thisWorldFile.worldLoad.AllFrames.Add(Frames)

        Dim dict = Split(ListBox4.SelectedItem, ",")
        If ListBox4.SelectedIndex <> 0 Then
            For Each fl In thisWorldFile.worldLoad.AllFrames
                For Each flit In fl
                    Dim olduv = flit.UV.Clone()
                    flit.UV(0) = olduv(Int(dict(0)))
                    flit.UV(1) = olduv(Int(dict(1)))
                    flit.UV(2) = olduv(Int(dict(2)))
                    flit.UV(3) = olduv(Int(dict(3)))

                Next
            Next
        End If

        For Each it In ListBox3.SelectedIndices
            thisWorldFile.worldLoad.mMesh(ListBox1.SelectedIndex).polyl(it).type = thisWorldFile.worldLoad.mMesh(ListBox1.SelectedIndex).polyl(it).type Or 512
            thisWorldFile.worldLoad.mMesh(ListBox1.SelectedIndex).polyl(it).tpage = ListBox2.SelectedIndex
        Next

        Dim k As New List(Of Integer)
        k.Add(ListBox1.SelectedIndex)
        thisWorldFile.Render(k)


        For Each item In thisWorldFile.texAnimHandlerList
            item.Play()
        Next


        nVolt_cam.Position = ocpos
        nVolt_cam.Target = ocloc
    End Sub
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        applyTA()
    End Sub

    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged
        applyTA()


    End Sub

    Private Sub ListBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged
        applyTA()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        ListBox3.SelectedIndices.Clear()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        isLoading = True
        For k = 0 To ListBox3.Items.Count - 1
            ListBox3.SelectedIndices.Add(k)
        Next
        isLoading = False
    End Sub

    Private Sub texanimWExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()
    End Sub

    Private Sub texanimWExport_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub
End Class