Imports System.IO
Module YUIsave
    Public OldFormat As Boolean = False
    Public Sub SaveYUIfile(ByVal fname$)
        Dim str As New FileStream(fname, FileMode.OpenOrCreate)
        Dim stW As New BinaryWriter(str)
        stW.Write(CInt(32768))        'header for 'new file' format
        stW.Write(CInt(Frames.Count)) 'frames count

        For i = 0 To Frames.Count - 1
            With Frames(i)
                stW.Write(CShort(.Tex))
                stW.Write(CInt(.Type))
                stW.Write(CSng(.Delay))
                stW.Write(CInt(.ImageCount))
                stW.Write(CSng(.Rotation))
                For j = 0 To 3
                    stW.Write(CSng(.UV(j).X))
                    stW.Write(CSng(.UV(j).Y))
                Next

                'image...

                ' If 10 = 20 Then 'disabled...
                'Try
                '.Image.Save(Environ("temp") & "\tmp.jpg", Imaging.ImageFormat.Jpeg)
                'Dim Bytes = File.ReadAllBytes(Environ("temp") & "\tmp.jpg")
                'stW.Write(CInt(Bytes.Count))
                'stW.Write(Bytes)

                '                Catch ex As Exception
                'stW.Write(CInt(0))
                'End Try
                'Else
                stW.Write(CInt(0))
                'End If



            End With

        Next
        stW.Flush()
        stW.Close()
        str.Dispose()


    End Sub
    'Dim tFrame As New Frame
    Public Sub LoadYUIfile(ByVal fname$)
        Dim mstr As New FileStream(fname, FileMode.Open)
        Dim str As New BinaryReader(mstr)
        Frames.Clear()

        
        OldFormat = False
        str.ReadInt32()



        Dim Cnt% = str.ReadInt32()

        For i = 0 To Cnt - 1
            Frames.Add(New Frame)
            With Frames(i)
                .Tex = str.ReadInt16
                .Type = str.ReadInt32
                .Delay = str.ReadSingle
                .ImageCount = str.ReadInt32
                If Not OldFormat Then .Rotation = str.ReadSingle
                For j = 0 To 3
                    .UV(j) = New IrrlichtNETCP.Vector2D(str.ReadSingle, str.ReadSingle)
                Next

                Dim ImgSize = str.ReadInt32
                'Debugger.Break()
                '  Dim Image() As Byte

                ' If ImgSize <> 0 Then
                'ReDim Image(ImgSize)
                ' Image = str.ReadBytes(ImgSize)
                ' File.WriteAllBytes(Environ("temp") & "\tmp.jpg", Image)
                ' 'Dim mastr = New IO.FileStream(Environ("temp") & "\tmp.jpg", FileMode.OpenOrCreate)
                '  .Image = Drawing.Image.FromStream(mastr)
                ' mastr.Close()
                '  Else
                ' Dim myUV(3) As Point
                ' Dim mbmp = Drawing.Image.FromFile(WorldPath & WorldName & Chr(65 + Frames(i).Tex) & ".bmp")
                ' For k = 0 To 3
                '
                '  myUV(k) = New Point(mbmp.Width * Frames(i).UV(k).X, mbmp.Height * Frames(i).UV(k).Y)
                '   Next
                'Frames(i).Image = ExtractPolygonAreaOfBitmap(mbmp, myUV)


                ' End If


            End With
        Next


        CurrentFrame = 0
        firstFrameInRow = 0
        Form1.PreloadImages()

        LoadFrame(CurrentFrame)
        Form1.EnableDisablePanels()


        str.Close()


    End Sub
End Module
