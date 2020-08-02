Imports System.IO

Module YUI_blender_texanimcomp



    Public Sub save_ta_csv(ByVal fname$)
        Dim str As New FileStream(fname, FileMode.OpenOrCreate)
        Dim stW As New StreamWriter(str)

        If Not cooked() Then
            MsgBox("Please cook before exporting to .ta.csv", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If

        stW.WriteLine("Slot,Frame,Texture,Delay,U0,V0,U1,V1,U2,V2,U3,V3")
        For i = 0 To Frames.Count - 1
            stW.WriteLine("{0:G},{1:G},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", _
                          0, i, Frames(i).Tex, Frames(i).Delay / 1000, _
                          Frames(i).UV(0).X, Frames(i).UV(0).Y, _
                          Frames(i).UV(1).X, Frames(i).UV(1).Y, _
                          Frames(i).UV(2).X, Frames(i).UV(2).Y, _
                          Frames(i).UV(3).X, Frames(i).UV(3).Y)



        Next
        stW.Flush()
        stW.Close()
        str.Close()


    End Sub


    Public Function read_ta_csv_as_world(ByVal fname$)

        TempWorld = New WORLD("")
        TempWorld.AllFrames = New List(Of List(Of Frame))

        Dim res(0) As List(Of Frame)
        res(0) = New List(Of Frame)

        Dim str As New FileStream(fname, FileMode.OpenOrCreate)
        Dim stRd As New StreamReader(str)

        stRd.ReadLine()

        TempWorld.AllFrames.Add(New List(Of Frame))
        Dim tmp$
        Dim tmpsp$()

        Dim lastanimIdx = 0

        While stRd.EndOfStream = False
            tmp = stRd.ReadLine
            tmpsp = tmp.Split(",")

            If res.Count < tmpsp(0) Then
                ReDim Preserve res(tmpsp(0))
                res(tmpsp(0)) = New List(Of Frame)
                TempWorld.AllFrames.Insert(tmpsp(0), New List(Of Frame))
            End If



            res(tmpsp(0)).Insert(tmpsp(1), New Frame With {.Tex = tmpsp(2), .Delay = tmpsp(3) * 1000})
            With res(tmpsp(0)).Last
                .UV(0) = New IrrlichtNETCP.Vector2D(tmpsp(4), tmpsp(5))
                .UV(1) = New IrrlichtNETCP.Vector2D(tmpsp(6), tmpsp(7))
                .UV(2) = New IrrlichtNETCP.Vector2D(tmpsp(8), tmpsp(9))
                .UV(3) = New IrrlichtNETCP.Vector2D(tmpsp(10), tmpsp(11))
            End With

            TempWorld.AllFrames(tmpsp(0)).Add(res(tmpsp(0))(tmpsp(1)))


        End While


        stRd.Close()

        Return res
    End Function

    ' Public Function read_ta_csv(ByVal fname$) As List(Of Frame)()

    ' Dim res As List(Of Frame)()

    '        ReDim res(0)
    '        res(0) = New List(Of Frame)

    '    Dim str As New FileStream(fname, FileMode.OpenOrCreate)
    '   Dim stRd As New StreamReader(Str)

    '   stRd.ReadLine()
    '
    'Dim tmp$
    'Dim tmpsp$()

    '    Dim lastanimIdx = 0

    '        While stRd.EndOfStream = False
    '            tmp = stRd.ReadLine
    '            tmpsp = tmp.Split(",")
    '
    '           If res.Count < tmpsp(0) Then
    '               ReDim Preserve res(tmpsp(0))
    '               res(tmpsp(0)) = New List(Of Frame)
    '          End If

    '         res(tmpsp(0)).Insert(tmpsp(1), New Frame With {.Tex = tmpsp(2), .Delay = tmpsp(3)})
    '        With res(tmpsp(0)).Last
    '           .UV(0) = New IrrlichtNETCP.Vector2D(tmpsp(4), tmpsp(5))
    '          .UV(1) = New IrrlichtNETCP.Vector2D(tmpsp(6), tmpsp(7))
    '         .UV(2) = New IrrlichtNETCP.Vector2D(tmpsp(8), tmpsp(9))
    '        .UV(3) = New IrrlichtNETCP.Vector2D(tmpsp(10), tmpsp(11))
    '   End With



    '        End While


    '       stRd.Close()

    '      Return res


    ' End Function

End Module
