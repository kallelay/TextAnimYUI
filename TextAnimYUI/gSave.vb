Module gSave
    Dim mbmp As Bitmap
    Sub LoadWConsole(ByVal fname$)
        getFileForTexAnim(fname)
        If mFrames Is Nothing Then Exit Sub
        Frames.Clear()
        For i = 0 To mFrames.Count - 1
            Frames.Add(New Frame)
            If mFrames(i) Is Nothing Then
                Frames.RemoveAt(i)
                Continue For
            End If
            Try
                mbmp = Image.FromFile(WorldPath & WorldName & Chr(65 + mFrames(i).texture) & ".bmp", True)

            Catch ex As Exception

            End Try
            Frames(i).Tex = mFrames(i).texture
            Frames(i).UV = mFrames(i).UV
            Frames(i).Delay = mFrames(i).Time * 1000
            Dim myUV(3) As Point
            Try
                For k = 0 To 3
                    myUV(k) = New Point(mbmp.Width * mFrames(i).UV(k).X, mbmp.Height * mFrames(i).UV(k).Y)
                Next
                Frames(i).Image = ExtractPolygonAreaOfBitmap(mbmp, myUV)

            Catch ex As Exception

            End Try

        Next

        CurrentFrame = 0
        firstFrameInRow = 0
        Form1.PreloadImages()

        LoadFrame(CurrentFrame)
        Form1.EnableDisablePanels()

    End Sub

    Dim ExportingProgress% = 0
    Sub SaveWConsole(ByVal fname$)

        If Not cooked() Then
            MsgBox("Not cooked yet!")
            Exit Sub
        End If


        ExportingProgress = 0


        mFrames = Nothing
        ReDim mFrames(Frames.Count)


        Dim str = New IO.FileStream(fname, IO.FileMode.OpenOrCreate)
        Dim stW = New IO.StreamWriter(str)


        For i = 0 To Frames.Count - 1
            ExportingProgress = i * 100 / (mFrames.Count - 1)

            stW.WriteLine("Frame " & i)
            stW.WriteLine(" Texture: " & Frames(i).Tex)
            stW.WriteLine(" Time: " & Frames(i).Delay / 1000)
            For j = 1 To 4
                stW.WriteLine(" uv" & j & ": " & Frames(i).UV(j - 1).X & "x" & Frames(i).UV(j - 1).Y)

            Next
            stW.WriteLine("End Frame")
            stW.WriteLine("")



        Next

        stW.Flush()
        stW.Close()




    End Sub
End Module
