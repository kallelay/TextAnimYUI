Module YUIcook
    Function cooked() As Boolean
        For i = 0 To Frames.Count - 1
            If Frames(i).Type <> AnimType.Static_ Then Return False
        Next
        Return True
    End Function
    Sub Cook()

        Dim flag_last_frame_is_a_linker As Integer = 0

        Dim linDisp! 'For linear displacements:
        Dim InputList As New List(Of Frame) 'new list of interpolated frames

        Select Case Frames(CurrentFrame).Type
            Case AnimType.LAccIn_, AnimType.LEaseIn_, AnimType.LEaseInOut_, AnimType.LEaseOut_, AnimType.Linear_, AnimType.LSmooth_, AnimType.LSmthBmerng_


                flag_last_frame_is_a_linker = (Frames(CurrentFrame + 1).Delay = 0) '= True
                flag_last_frame_is_a_linker = Math.Abs(flag_last_frame_is_a_linker)


                For i = 0 To Frames(CurrentFrame).ImageCount - 1



                    'Generate new dispVector 
                    Select Case Frames(CurrentFrame).Type
                        Case AnimType.Linear_ : linDisp! = Animation_Linear(i, Frames(CurrentFrame).ImageCount - flag_last_frame_is_a_linker)
                        Case AnimType.LSmooth_ : linDisp = Animation_SmoothCos(i, Frames(CurrentFrame).ImageCount - flag_last_frame_is_a_linker)
                        Case AnimType.LSmthBmerng_ : linDisp = Animation_SmoothCosBoomerang(i, Frames(CurrentFrame).ImageCount - flag_last_frame_is_a_linker)
                        Case AnimType.LEaseIn_ : linDisp = Animation_EaseInQuad(i, Frames(CurrentFrame).ImageCount - flag_last_frame_is_a_linker)
                        Case AnimType.LAccIn_ : linDisp = Animation_AccInSqrt(i, Frames(CurrentFrame).ImageCount - flag_last_frame_is_a_linker)
                        Case AnimType.LEaseOut_ : linDisp = Animation_EaseOutQuad(i, Frames(CurrentFrame).ImageCount - flag_last_frame_is_a_linker)
                        Case AnimType.LEaseInOut_ : linDisp = Animation_EaseInOutArctanAsSigmoid(i, Frames(CurrentFrame).ImageCount - flag_last_frame_is_a_linker)
                    End Select


                    'Add new frame and interpolate it
                    InputList.Add(New Frame)
                    InputList(i).Delay = Frames(CurrentFrame).Delay / Frames(CurrentFrame).ImageCount
                    InputList(i).Tex = Frames(CurrentFrame).Tex
                    InputList(i).Type = AnimType.Static_
                    For j = 0 To 3
                        InputList(i).UV(j) = Frames(CurrentFrame).UV(j) + (Frames(CurrentFrame + 1).UV(j) - Frames(CurrentFrame).UV(j)) * linDisp
                    Next


                Next

                ' If flag_last_frame_is_a_linker And False Then
                'InputList.Add(New Frame)
                'InputList.Last.Delay = Frames(CurrentFrame).Delay / Frames(CurrentFrame).ImageCount
                'InputList.Last.Tex = Frames(CurrentFrame).Tex
                'InputList.Last.Type = AnimType.Static_
                'For j = 0 To 3
                'InputList.Last.UV(j) = Frames(CurrentFrame + 1).UV(j)
                'Next
                ' End If




            Case AnimType.Rotation_ '------------------ ROTATION

                Dim Mat As New Matrix2x2

                For i = 0 To Frames(CurrentFrame).ImageCount - 1
                    InputList.Add(New Frame)
                    InputList(i).Delay = Frames(CurrentFrame).Delay / Frames(CurrentFrame).ImageCount
                    InputList(i).Tex = Frames(CurrentFrame).Tex
                    InputList(i).Type = AnimType.Static_
                    For j = 0 To 3
                        Dim PivotU = (Frames(CurrentFrame).UV(0) + Frames(CurrentFrame).UV(1) + Frames(CurrentFrame).UV(2) + Frames(CurrentFrame).UV(3)) / 4
                        Mat.CreateRotationMatrix((Frames(CurrentFrame + 1).Rotation - Frames(CurrentFrame).Rotation) * i / Frames(CurrentFrame).ImageCount)
                        InputList(i).UV(j) = Mat * (Frames(CurrentFrame).UV(j) - PivotU) + PivotU
                    Next

                Next


            Case AnimType.Shake_ '------------------ SHAKE



                Dim Random = New Random(Form1.NumericUpDown1.Value) 'Get Random Handle from Random Seed Generator

                Dim ofx As Single, ofy As Single, dispVec As IrrlichtNETCP.Vector2D



                For i = 0 To Frames(CurrentFrame).ImageCount - 1


                    Try
                        ofx! = Math.Abs(Frames(CurrentFrame).UV(1).X - Frames(CurrentFrame).UV(0).X) * Frames(CurrentFrame).NoiseLevel / 100 * (Random.NextDouble() - 0.5) * 2 'Single.Parse(Frames(CurrentFrame).NoiseLevel)
                        ofy! = Math.Abs(Frames(CurrentFrame).UV(3).Y - Frames(CurrentFrame).UV(0).Y) * Frames(CurrentFrame).NoiseLevel / 100 * (Random.NextDouble() - 0.5) * 2 'Single.Parse(TextBox6.Text)
                        dispVec = New IrrlichtNETCP.Vector2D(ofx, ofy)
                    Catch ex As Exception

                    End Try


                    InputList.Add(New Frame)
                    InputList(i).Delay = Frames(CurrentFrame).Delay / Frames(CurrentFrame).ImageCount
                    InputList(i).Tex = Frames(CurrentFrame).Tex
                    InputList(i).Type = AnimType.Static_
                    For j = 0 To 3
                        InputList(i).UV(j) = Frames(CurrentFrame).UV(j) + dispVec
                    Next j

                Next i


            Case AnimType.Grid_
                'TODO: Grid 1

                '3 0 2 1'

                '0 1 
                '3 2'
                Dim ofx!, ofy!, sx!, sy!

                Dim cy!, cx!

                ofx! = Math.Min(Frames(CurrentFrame).UV(3).X, Frames(CurrentFrame).UV(0).X)
                ofy! = Math.Min(Frames(CurrentFrame).UV(0).Y, Frames(CurrentFrame).UV(1).Y)
                sx! = (Frames(CurrentFrame).UV(1).X - Frames(CurrentFrame).UV(0).X) / Math.Sqrt(Frames(CurrentFrame).ImageCount)
                sy! = (Frames(CurrentFrame).UV(3).Y - Frames(CurrentFrame).UV(0).Y) / Math.Sqrt(Frames(CurrentFrame).ImageCount)



                For i = 0 To Frames(CurrentFrame).ImageCount - 1

                    cy = Math.Floor(i / Math.Sqrt(Frames(CurrentFrame).ImageCount))
                    cx = i / Math.Sqrt(Frames(CurrentFrame).ImageCount) - Math.Floor(i / Math.Sqrt(Frames(CurrentFrame).ImageCount))

                    '  Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = New IrrlichtNETCP.Vector2D((cx + 0) * sx + ofx, (cy + 1) * sy + ofy)
                    ' Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = New IrrlichtNETCP.Vector2D((cx + 0) * sx + ofx, (cy + 0) * sy + ofy)
                    ' Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = New IrrlichtNETCP.Vector2D((cx + 1) * sx + ofx, (cy + 1) * sy + ofy)
                    ' Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = New IrrlichtNETCP.Vector2D((cx + 1) * sx + ofx, (cy + 0) * sy + ofy)


                    InputList.Add(New Frame)
                    InputList(i).Delay = Frames(CurrentFrame).Delay / Frames(CurrentFrame).ImageCount
                    InputList(i).Tex = Frames(CurrentFrame).Tex
                    InputList(i).Type = AnimType.Static_

                    InputList(i).UV(0) = New IrrlichtNETCP.Vector2D((cx + 0) * sx + ofx, (cy + 0) * sy + ofy)
                    InputList(i).UV(1) = New IrrlichtNETCP.Vector2D((cx + 1) * sx + ofx, (cy + 0) * sy + ofy)
                    InputList(i).UV(2) = New IrrlichtNETCP.Vector2D((cx + 1) * sx + ofx, (cy + 1) * sy + ofy)
                    InputList(i).UV(3) = New IrrlichtNETCP.Vector2D((cx + 0) * sx + ofx, (cy + 1) * sy + ofy)


                Next i



        End Select

        If flag_last_frame_is_a_linker Then Frames.Remove(Frames(CurrentFrame + 1)) 'remove the linker frame

        Frames.Remove(Frames(CurrentFrame)) 'Remove old frame
        Frames.InsertRange(CurrentFrame, InputList) 'Replace it with the new interpolated frames
        Form1.PreloadImages() 'Preload
        Form1.EnableDisablePanels() 'Check the panels < | > 

    End Sub
End Module
