Module YUIcook
    Function cooked() As Boolean
        For i = 0 To Frames.Count - 1
            If Frames(i).Type <> AnimType.Static_ Then Return False
        Next
        Return True
    End Function
    Sub Cook()
        Select Case Frames(CurrentFrame).Type
            Case AnimType.linear_ '-------------- LINEAR
                Dim InputList As New List(Of Frame)
                For i = 0 To Frames(CurrentFrame).ImageCount - 1
                    InputList.Add(New Frame)
                    InputList(i).Delay = Frames(CurrentFrame).Delay / Frames(CurrentFrame).ImageCount
                    InputList(i).Tex = Frames(CurrentFrame).Tex
                    InputList(i).Type = AnimType.Static_
                    For j = 0 To 3

                        InputList(i).UV(j) = Frames(CurrentFrame).UV(j) + (Frames(CurrentFrame + 1).UV(j) - Frames(CurrentFrame).UV(j)) * i / Frames(CurrentFrame).ImageCount
                    Next

                Next

                Frames.Remove(Frames(CurrentFrame))
                Frames.InsertRange(CurrentFrame, InputList)

                Form1.PreloadImages()

                Form1.EnableDisablePanels()
            Case AnimType.Rotation_ '------------------ ROTATION

                Dim InputList As New List(Of Frame)
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


                Frames.Remove(Frames(CurrentFrame))
                Frames.InsertRange(CurrentFrame, InputList)

                Form1.PreloadImages()

                Form1.EnableDisablePanels()



        End Select
    End Sub
End Module
