Imports IrrlichtNETCP
Imports System.IO
Module gWConsoleFile

    Public mFrames() As TEX_ANIM_FRAME
    Public Class TEX_ANIM_FRAME
        Public texture As Int32
        Public Time As Single
        Public UV(4) As Vector2D
        Public index As Integer
        Sub New()
            texture = 0
            Time = 0.33
            UV(0) = New Vector2D(0, 0)
            UV(1) = New Vector2D(1, 0)
            UV(2) = New Vector2D(1, 1)
            UV(3) = New Vector2D(0, 1)


        End Sub
    End Class


    Sub getFileForTexAnim(ByVal filepath$)
       

        Dim ii As Integer = 0
        Dim txt$ = IO.File.ReadAllText(filepath)
        Dim AllF As Integer = Split(txt, "frame", , CompareMethod.Text).Length \ 2
        ReDim mFrames(AllF)



        Dim X As New StreamReader(New FileStream(filepath, FileMode.Open))
        Dim buffer$
        Dim mFrame As New TEX_ANIM_FRAME
        Do Until X.EndOfStream
            buffer = TreatBuffer(X.ReadLine)


            Select Case LCase(Replace(getCommand(buffer), ":", ""))
                Case "frame"
                    mFrame = New TEX_ANIM_FRAME
                    If IsNumeric(getAllVars(buffer)) Then
                        mFrame.index = getAllVars(buffer)
                    Else
                        mFrame.index = ii
                        ii += 1

                    End If
                Case "end"
                    'will be out of "SWITCH"
                Case "tex", "texture"
                    If IsNumeric(getAllVars(buffer)) Then
                        mFrame.texture = getAllVars(buffer)
                    Else
                        mFrame.texture = Asc(UCase(getAllVars(buffer))) - Asc("A")
                    End If



                Case "time", "delay"
                    mFrame.Time = CSng(getAllVars(buffer))
                Case "v1", "uv1"
                    If InStr(CStr(CSng(0.5)), ",") > 0 Then
                        buffer = Replace(buffer, ".", ",")
                    End If
                    mFrame.UV(0) = New Vector2D(CSng(Split(getAllVars(buffer), "x")(0)), CSng(Split(getAllVars(buffer), "x")(1)))
                Case "v2", "uv2"
                    If InStr(CStr(CSng(0.5)), ",") > 0 Then
                        buffer = Replace(buffer, ".", ",")
                    End If
                    mFrame.UV(1) = New Vector2D(CSng(Split(getAllVars(buffer), "x")(0)), CSng(Split(getAllVars(buffer), "x")(1)))
                Case "v3", "uv3"
                    If InStr(CStr(CSng(0.5)), ",") > 0 Then
                        buffer = Replace(buffer, ".", ",")
                    End If
                    mFrame.UV(2) = New Vector2D(CSng(Split(getAllVars(buffer), "x")(0)), CSng(Split(getAllVars(buffer), "x")(1)))
                Case "v4", "uv4"
                    If InStr(CStr(CSng(0.5)), ",") > 0 Then
                        buffer = Replace(buffer, ".", ",")
                    End If
                    mFrame.UV(3) = New Vector2D(CSng(Split(getAllVars(buffer), "x")(0)), CSng(Split(getAllVars(buffer), "x")(1)))




            End Select


            If LCase(getCommand(buffer)) = "end" And LCase(TreatBuffer(getAllVars(buffer))) = "frame" Then
                mFrames(mFrame.index) = mFrame
            End If



        Loop
        X.Close()


        If mFrames(0) Is Nothing Then
            mFrames = Nothing
            MsgBox("Error, Invalid W_Console file", MsgBoxStyle.Critical, "Error")
        End If
        '  Debugger.Break()


    End Sub
    'texanim C:\Games\pc\revolt\revolt\levels\nhood1 - Copie\framelistEXAMPLE.txt
    'from KDL...
    Public Function TreatBuffer(ByVal buffer$)
        Dim temp$ = buffer






        'get first position of char
        Dim i As Int16 = 0
        If buffer <> Nothing Then
            If buffer.Length > 3 Then


                'tabs and spaces
                temp = Replace(temp, vbTab, " ")
                temp = Replace(temp, Space(2), Space(1))
                i = 0
                Do Until (UCase(buffer(i)) >= "A" And UCase(buffer(i)) <= "Z") Or i = Len(buffer) - 1 Or buffer(i) = "{" Or buffer(i) = "}" Or buffer(i) = "/"
                    i += 1
                Loop

                If buffer.Substring(0, 2) <> "//" Then     'ok, found the first char
                    temp = Mid(buffer, i + 1)

                    If InStr(temp, "//", CompareMethod.Text) > 0 Then
                        temp = Split(temp, "//")(0)
                    End If


                Else
                    temp = ""
                End If

            End If
        End If


        Return temp
    End Function


End Module

