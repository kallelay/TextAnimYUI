Imports System.Math
Imports IrrlichtNETCP

'TODO: support for tri 
Module YUIAutoCook_TimeMachine

    'Try to find the inner 
    Sub TimeMachine_ReturnRecipe()

        Dim nFrames As New List(Of gFrames.Frame)
        '   nFrames.AddRange(Frames.ToList)


        If Frames.Count < 2 Then Exit Sub 'not possible, don't enter...


        Debugx("Running Time Machine to get back the receipe")

        Dim lastdt! = Frames(0).Delay
        Dim lastDispVec(3) As Vector2D
        Dim curDispVec(3) As Vector2D
        Dim jointList As New List(Of Integer)

        jointList.Add(0) 'attempt to add the first keyframe in the joint list


        'Calculate the first displacement vector
        For k = 0 To 3 : lastDispVec(k) = Frames(1).UV(k) - Frames(0).UV(k) : Next k



        'Support for constant time diff, constant phase
        For iframe = 1 To Frames.Count - 1

state_init:


state_search:
            'Calculate UV difference (implemented this way for future use)
            For k = 0 To 3 : curDispVec(k) = Frames(iframe).UV(k) - Frames(iframe - 1).UV(k) : Next k

            'Check constant time interval and texture
            If Frames(iframe).Delay <> Frames(iframe - 1).Delay Then GoTo state_joint_broken
            If Frames(iframe).Tex <> Frames(iframe - 1).Tex Then GoTo state_joint_broken


            'Try for constant displacement
            For k = 0 To 3
                If curDispVec(k) <> lastDispVec(k) Then GoTo state_joint_broken
            Next
            jointList.Add(iframe)
            If iframe < Frames.Count - 1 Then GoTo state_joint_finally 'Break at last joint

state_joint_broken:


            If jointList.Count >= 3 Then
                Dim StartFrm As New TextAnimYUI.gFrames.Frame
                StartFrm.Delay = (jointList.Count) * Frames(jointList(0)).Delay
                StartFrm.ImageCount = jointList.Count
                StartFrm.Tex = Frames(jointList(0)).Tex
                StartFrm.Type = AnimType.Linear_
                StartFrm.UV = Frames(jointList(0)).UV

                Dim EndFrm As New TextAnimYUI.gFrames.Frame
                EndFrm.Delay = 0 ' Frames(jointList.Last).Delay
                EndFrm.ImageCount = jointList.Count
                EndFrm.Tex = Frames(jointList.Last).Tex
                EndFrm.Type = AnimType.Static_
                EndFrm.UV = Frames(jointList.Last).UV

                'nFrames.RemoveRange(jointList.First, jointList.Count)
                ' nFrames.Insert(jointList.First, StartFrm)
                ' nFrames.Insert(jointList.First + 1, EndFrm)
                nFrames.Add(StartFrm)
                nFrames.Add(EndFrm)
                Debugx("Linking " & jointList(0) & "--" & jointList.Last)
            Else
                For Each l In jointList
                    nFrames.Add(Frames(l))
                Next

                If iframe = Frames.Count - 1 And jointList.IndexOf(Frames.Count - 1) = -1 Then
                    nFrames.Add(Frames.Last)
                End If

            End If


            jointList.Clear()
            jointList.Add(iframe) 'attempt for next tentative



state_joint_finally:
            For k = 0 To 3 : lastDispVec(k) = curDispVec(k) : Next k


        Next



        Frames = nFrames 'Flip buffer
        Form1.PreloadImages() 'Preload
        Debugx("Time Machine is now stopped")






    End Sub

End Module
