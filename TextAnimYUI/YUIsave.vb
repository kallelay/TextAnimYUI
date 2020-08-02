Imports System.IO
Module YUIsave
    Public OldFormat As Boolean = False
    Public Sub SaveYUIfile(ByVal fname$)
        Dim str As New FileStream(fname, FileMode.OpenOrCreate)
        Dim stW As New BinaryWriter(str)
        stW.Write(CInt(1))        'header for 'new file' format

        '32768: TEXYUI version 2012/2013
        '00001: TEXYUI version 2020/08/01

        stW.Write(CInt(Frames.Count)) 'frames count

        For i = 0 To Frames.Count - 1
            With Frames(i)
                stW.Write(CShort(.Tex))
                stW.Write(CInt(.Type))
                stW.Write(CInt(.AnimationSpeed)) 'v01
                stW.Write(CSng(.Delay))
                stW.Write(CInt(.ImageCount))
                stW.Write(CSng(.Rotation))
                For j = 0 To 3
                    stW.Write(CSng(.UV(j).X))
                    stW.Write(CSng(.UV(j).Y))
                Next
                stW.Write(CInt(.Cloned)) 'v01 Cloned
                stW.Write(CSng(.NoiseLevel)) 'Noise level

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


        Select Case str.ReadInt32()
            Case 32768
                OldFormat = True

            Case 1
                OldFormat = False

        End Select



        Dim Cnt% = str.ReadInt32()

        For i = 0 To Cnt - 1
            Frames.Add(New Frame)
            With Frames(i)
                .Tex = str.ReadInt16
                .Type = str.ReadInt32
                If Not OldFormat Then .AnimationSpeed = str.ReadInt32
                .Delay = str.ReadSingle
                .ImageCount = str.ReadInt32
                If Not OldFormat Then .Rotation = str.ReadSingle
                For j = 0 To 3
                    .UV(j) = New IrrlichtNETCP.Vector2D(str.ReadSingle, str.ReadSingle)
                Next

                If Not OldFormat Then .Cloned = str.ReadInt32
                If Not OldFormat Then .NoiseLevel = str.ReadSingle

                Dim ImgSize = str.ReadInt32 'skipped
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
