Imports IrrlichtNETCP
Imports System.Math
Module Gmodule 'Global Module

    Public WorldPath$
    Public WorldFile$
    Public WorldName$

    Public Loading As Boolean = False
    Public Saved = True

    'UV translator
    Function TranslateUV(ByVal vector As Vector2D) As Vector3D
        Return New Vector3D(-0.5 + vector.X, 0.02, +0.5 - vector.Y) * 0.98
    End Function
    Function TranslateUV(ByVal x!, ByVal y!) As Vector3D
        Return New Vector3D(-0.5 + x, 0.02, +0.5 - y) * 0.98
    End Function


    'UV : update
    Public Sub UpdateUV()

        For i = 0 To 3
            If Not Form1.Timer2.Enabled Then Frames(CurrentFrame).UV(i) = Selectors(i)
        Next

        Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(0).TCoords = Selectors(3)
        Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(1).TCoords = Selectors(0)
        Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(2).TCoords = Selectors(2)
        Plane.GetMesh(0).GetMeshBuffer(0).GetVertex(3).TCoords = Selectors(1)

        If Not Form1.Timer2.Enabled Then SaveFrame(CurrentFrame)
    End Sub


    'Selectors
    Public Selectors(4) As Vector2D
    Public Sel(4) As SceneNode
    '  Public cubes(4) As AnimatedMesh
    Sub InitSelectors()


        For i = 0 To 3
            Sel(i) = ScnMgr2.AddCubeSceneNode(0.014, Nothing, Nothing)
            Sel(i).GetMaterial(0).EmissiveColor = SelectedColor
        Next




    End Sub
    Sub MapSelectors()
        '  If Form1.Timer2.Enabled Then
        '   MapSelectorsNoCh(Frames(Form1.LatestFrame))
        'Exit Sub
        '  End If


        For i = 0 To 3
            If Frames.Count = 0 Then Exit Sub
            Sel(i).Position = TranslateUV(Frames(CurrentFrame).UV(i))
            videoDriver2.Draw3DLine(TranslateUV(Selectors(i)), TranslateUV(Selectors(If(i + 1 = 4, 0, i + 1))), SelectedColor)

        Next
    End Sub
    Public SelectedColor As Color = Color.Blue




    'Edit modes
    Enum EditModes
        Nothing_
        Move_
        Rotate_
        Scale_
        Rect_
        Select_

    End Enum
    Public State As EditModes = EditModes.Select_







    'W_Console's get clean code

    Public Function getAllVars(ByVal buffer$)
        Return Mid(buffer, Len(getCommand(buffer)) + 2)
    End Function

    Public Function getCommand(ByVal buffer$)
        buffer = Replace(buffer, vbTab, "")
        Return Split(buffer, " ")(0)
    End Function




    Public Class Matrix2x2
        Public Values(1, 1) As Single
        Sub New()
            ' For i = 0 To 1
            'For j = 0 To 1
            'Values(i, j) = 0
            '  Next
            '  Next
        End Sub
        Sub CreateRotationMatrix(ByVal Theta!)
            Values(0, 0) = Cos(Theta)
            Values(0, 1) = -Sin(Theta)
            Values(1, 0) = Sin(Theta)
            Values(1, 1) = Cos(Theta)
        End Sub

        Sub CreateScaleMatrix(ByVal Scale!)
            Values(0, 0) = Scale
            Values(0, 1) = 0
            Values(1, 0) = Scale
            Values(1, 1) = 0
        End Sub


        Public Shared Operator *(ByVal Mat As Matrix2x2, ByVal Vec As Vector2D) As Vector2D
            Return New Vector2D(Mat.Values(0, 0) * Vec.X + Mat.Values(0, 1) * Vec.Y, Mat.Values(1, 0) * Vec.X + Mat.Values(1, 1) * Vec.Y)

        End Operator

    End Class

    Sub LaunchInsidePanel()

    End Sub


    Public Function RGBToUint(ByVal color As Color)
        Return CUInt(color.A) << 24 Or CUInt(color.R) << 16 Or CUInt(color.G) << 8 Or CUInt(color.B) << 0
    End Function

    Public Function ColorsToRGB(ByVal cl As UInt32) As Color
        'long rgb value, is composed from 0~255 R, G, B
        'according to net: (2^8)^cn
        ' cn: R = 0 , G = 1, B = 2


        'simple...
        Dim a = cl >> 24

        If a = 0 Then a = 251


        ' 
        ' If a = 0 Then a = 255
        Dim r = cl >> 16 And &HFF

        Dim g = cl >> 8 And &HFF
        Dim b = cl >> 0 And &HFF


        Return New Color(a, r, g, b)


    End Function

End Module
