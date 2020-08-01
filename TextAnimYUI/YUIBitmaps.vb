Imports IrrlichtNETCP
Imports System.Drawing.Drawing2D

Module YUIBitmaps
    '---------------------------------------------
    ' Loading stuffs
    '---------------------------------------------
    ' Public Bitmap(10) As String

    'Bitmap paths
    Sub LoadBitmaps()
        For i = 0 To 25

            If IO.File.Exists(WorldPath & WorldName & Chr(65 + i) & ".bmp") Then

                Try
                    videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + i) & ".bmp")

                    videoDriver2.GetTexture(WorldPath & WorldName & Chr(65 + i) & ".bmp")
                    Dim tx = videoDriver2.GetTexture(WorldPath & WorldName & Chr(65 + i) & ".bmp")
                    Debugx("Texture " & Chr(97 + i) & ": OK")
                Catch ex As Exception
                    Debugx("Texture " & Chr(97 + i) & ": [err]  " & ex.Message)
                End Try









            Else
                videoDriver2.AddTexture(New Dimension2D(1, 1), "temp" & i, ColorFormat.A1R5G5B5)
                videoDriver.AddTexture(New Dimension2D(1, 1), "temp" & i, ColorFormat.A1R5G5B5)
                '  Bitmap(i) = "FALSE"
            End If
        Next
    End Sub
    Dim Cnt&
    Function SaveUVintoPICTURE_FailedAlgo(ByVal i%) As Drawing.Image
        Dim tex = videoDriver.GetTexture(WorldPath & WorldName & Chr(65 + i) & ".bmp")

        For i = 0 To 255
            For j = 0 To 255
                For k = 1 To 3
                    Dim vec = (Selectors(k) - Selectors(If(k + 1 = 4, 0, k + 1))) / (Selectors(k).Length + Selectors(If(k + 1 = 4, 0, k + 1)).Length)
                    Dim projectVect = vec.DotProduct(New Vector2D(i, j) / 255) * vec

                    If (projectVect.Length > vec.Length) Then
                        tex.SetPixel(i, j, Color.Black)
                    End If


                Next
            Next
        Next

        Return tex.DOTNETImage


    End Function
    Function SaveUVintoPICTUREbck() As Drawing.Image
        '  Cnt = Cnt + 1

        ' If Cnt > 60 Then
        ' Cnt = 0


        Dim bmp As New Bitmap(Form1.Panel2.Width, Form1.Panel2.Height)
        Dim g = Graphics.FromImage(bmp)
        g.CopyFromScreen(Form1.Panel2.Left + Form1.Left, Form1.Panel2.Top + Form1.Top, 0, 0, Form1.Panel2.Size)
        g.Flush()
        Return bmp



        ' End If

        ' Return Nothing

    End Function
    Dim selec(3) As Point

    Function SaveUVintoPICTURE(ByVal FrameIndex%) As Drawing.Image
        If FrameIndex >= Frames.Count Then Return Nothing
        Dim mbmp = Drawing.Image.FromFile(WorldPath & WorldName & Chr(65 + Frames(FrameIndex%).Tex) & ".bmp")
        For i = 0 To 3
            selec(i) = New Point(Frames(FrameIndex%).UV(i).X * mbmp.Width, Frames(FrameIndex%).UV(i).Y * mbmp.Height)
        Next

        Return ExtractPolygonAreaOfBitmap(mbmp, selec)


    End Function
    Function SaveUVintoPICTURE() As Drawing.Image

        If Frames.Count = 0 Then Return Nothing
        Dim mbmp = Drawing.Image.FromFile(WorldPath & WorldName & Chr(65 + Frames(CurrentFrame).Tex) & ".bmp")
        For i = 0 To 3
            selec(i) = New Point(Frames(CurrentFrame).UV(i).X * mbmp.Width, Frames(CurrentFrame).UV(i).Y * mbmp.Height)
        Next

        Return ExtractPolygonAreaOfBitmap(mbmp, selec)


    End Function
    Public Function ExtractPolygonAreaOfBitmap(ByRef b As Bitmap, ByRef pts() As Point) As Bitmap
        Try

       ' A GraphicsPath will allow us to clip the output
            Dim gp As New GraphicsPath()

            ' the path should be composed of the polygon
            gp.AddPolygon(pts)


            ' ask the path to tell us how big the bounding rectangle is
            ' we'll need that later to construct a new bitmap of the right size
            Dim rc As RectangleF = gp.GetBounds()

            ' and it needs to be translated to the origin for clipping
            Dim m As New Matrix(1, 0, 0, 1, -rc.X, -rc.Y)

            ' Now we'll have a clipping path ready to use
            gp.Transform(m)

            ' Create a new bitmap the same size as the polygon area
            Dim bmp As New Bitmap(CInt(rc.Width), CInt(rc.Height), Imaging.PixelFormat.Format24bppRgb)

            ' We need a Graphics so we can draw on our new bitmap
            Dim g As Graphics = Graphics.FromImage(bmp)

            ' Initialize the new bitmap to some color (tranparent? white? your choice)
            g.Clear(Drawing.Color.Transparent)

            ' We're going to need a target rectangle too
            Dim rcDraw As New RectangleF(0, 0, rc.Width, rc.Height)

            ' set up the clipping
            g.Clip = New Region(gp)

            ' all set - now draw the clipped image
            g.DrawImage(b, rcDraw, rc, GraphicsUnit.Pixel)

            ' clean up
            gp.Dispose()
            g.Dispose()

            ' return the result
            Return bmp
        Catch ex As Exception
            Return New Bitmap(256, 256)
        End Try

    End Function

    Sub LoadBitmapIntoUV(ByVal index%)
        PScn2.GetMaterial(0).Texture1 = videoDriver2.GetTextureByIndex(index + 1)
        PScn.GetMaterial(0).Texture1 = videoDriver.GetTextureByIndex(index + 1)
        PScn.SetMaterialFlag(MaterialFlag.TextureWrap, True)

    End Sub
End Module
