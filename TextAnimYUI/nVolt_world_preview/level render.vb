Imports System.IO
Imports IrrlichtNETCP

Module level_render

    'Version GVE-nVolt engine: 1.1
    ' 2020/08/06


    'Keep track on scene Node to remove them 
    Public SceneNodes As New List(Of SceneNode)

    Public Timer_List As New List(Of Timers.Timer)

    Public Class WorldFile

        Function Clone() As WorldFile
            Clone = New WorldFile(Me.worldLoad.clone())
            Clone.texAnimHandlerList.AddRange(texAnimHandlerList)
        End Function


        Public worldLoad As WORLD


        Public texAnimHandlerList As New List(Of TexAnimHandler)
        Sub New(ByRef WrldFile As WORLD)
            If WrldFile Is Nothing Then
                WrldFile = New WORLD(Gmodule.WorldFile)
            End If
            worldLoad = WrldFile

        End Sub




        Dim Current_animation_index = -1


        Function curanimidx(j%)
            Current_animation_index += 1

            Return Current_animation_index

        End Function

        Public Material(26) As Material

        Public RenderBoundingBox As Box3D
        Public CoM As New Vector3D
        Public cCoM = 0
        Sub Render(Optional listofMeshes As List(Of Integer) = Nothing)
            RenderBoundingBox = New Box3D
            CoM = New Vector3D
            cCoM = 0
            '   Dim nn = nVolt_cam.Position '= nVolt_ScnMgr.AddCameraSceneNodeMaya(nVolt_ScnMgr.RootSceneNode, 50, 100, 200, 0)
            For Each item In texAnimHandlerList
                item.Stop()
                item = Nothing
            Next
            texAnimHandlerList.Clear()

            'animation tracker:
            Current_animation_index = 0
            texAnimHandlerList.Clear()

            'Materials

            'nVolt_ScnMgr.Clear() 'clear all?

            For k = SceneNodes.Count - 1 To 0 Step -1

                nVolt_ScnMgr.RootSceneNode.RemoveChild(SceneNodes(k))


                SceneNodes(k) = Nothing
                SceneNodes.RemoveAt(k)

            Next

            'nVolt_ScnMgr.RootSceneNode.RemoveAll()
            GC.WaitForPendingFinalizers()



            nVolt_Render.InitCamera()
            ' nVolt_cam.Position = nn
            '   nVolt_cam.Target = New Vector3D

            'TODO: erm this is kay from 2020... looking at his own N years old code....
            'How bad can this code be? Can someone optimize it?

            'UNDONE: The bucket sorting into env/alpha/transp





            For k = 0 To 25 '26 bitmaps
                Material(k) = New Material
                Material(k).Lighting = False


                Material(k).Texture1 = nVolt_VideoDriver.GetTexture(worldLoad.Directory & worldLoad.DirectoryName & Chr(65 + k) & ".bmp")
                If Material(k).Texture1 IsNot Nothing Then


                    Dim img = New Bitmap(worldLoad.Directory & worldLoad.DirectoryName & Chr(65 + k) & ".bmp", True)
                    If img.PixelFormat <> Imaging.PixelFormat.Format32bppArgb And img.PixelFormat <> Imaging.PixelFormat.Format32bppRgb And img.PixelFormat <> Imaging.PixelFormat.Format32bppPArgb Then
                        Material(k).Texture1.MakeColorKey(nVolt_VideoDriver, Color.Black) '(on 24bits) black goes transparrrr 

                    End If
                    img.Dispose()
                    img = Nothing


                    '  Material(k).Texture1.RegenerateMipMapLevels()
                End If

            Next
            Material(26) = New Material


            If listofMeshes Is Nothing Then
                listofMeshes = New List(Of Integer)
                For k = 0 To worldLoad.meshCount - 1
                    listofMeshes.Add(k)
                Next
            End If
            For Each p In listofMeshes

                Dim add_mesh_to_texanim_bucket As Boolean = False

                Dim j As Long = 0
                'mainform.Text = "Loading " & Strings.Format(p / (meshCount - 1) * 100, "00.00") & "%"

                Dim quads = 0
                Dim vx3d As New Vertex3D()
                Dim polys() = worldLoad.mMesh(p).polyl 'clone polys (less code will be used)
                Dim vexs() = worldLoad.mMesh(p).vexl   'clone vertex(s) ( same reason)



                Dim Texbuckets(26) As List(Of Integer)
                Dim TexbucketsVx(26) As List(Of Integer)





                For i = 0 To worldLoad.mMesh(p).polynum

                    If polys(i).tpage <> -1 Then
                        If Texbuckets(polys(i).tpage) Is Nothing Then
                            Texbuckets(polys(i).tpage) = New List(Of Integer)
                            TexbucketsVx(polys(i).tpage) = New List(Of Integer)
                        End If
                        Texbuckets(polys(i).tpage).Add(i)
                    Else
                        If Texbuckets(26) Is Nothing Then Texbuckets(26) = New List(Of Integer)
                        Texbuckets(26).Add(i)
                    End If

                Next i
                For k = 0 To 26 '26th = last material no gfx
                    add_mesh_to_texanim_bucket = False
                    If Texbuckets(k) Is Nothing Then Continue For
                    If Texbuckets(k).Count < 2 Then Continue For

                    'ok, new vx list...
                    '   Dim newVxList() As Vertex3D
                    '  Dim maxNewVx As Long

                    Dim alphavxk As New List(Of Integer)
                    Dim firstRun As Boolean = True


                    Dim mesh As New Mesh()

                    Dim VxNor As New MeshBuffer(VertexType.Standard)

                    Dim VxAnim As New List(Of MeshBuffer)

neg:
                    For kk = 0 To Texbuckets(k).Count - 1


                        Dim vx = VxNor


                        Dim i = Texbuckets(k)(kk)

                        '  If polys(i).type And 512 Then Continue For

                        If Not (polys(i).type And 4) And firstRun Then GoTo endit ' 4: trans vertex alpha





                        '   Next
                        If k <> 26 Then vx.Material.Texture1 = Material(k).Texture1

                        If (polys(i).type And 512) Then 'Texanim
                            add_mesh_to_texanim_bucket = True

                            VxAnim.Add(New MeshBuffer(VertexType.Standard))
                            vx = VxAnim(VxAnim.Count - 1)

                            texAnimHandlerList.Add(New TexAnimHandler)

                            ' texAnimHandlerList(texAnimHandlerList.Count - 1).animation = AllFrames(curanimidx(k))
                            texAnimHandlerList(texAnimHandlerList.Count - 1).animation = worldLoad.AllFrames(polys(i).tpage)
                            '  texAnimHandlerList(texAnimHandlerList.Count - 1).animMesh = Nothing
                            texAnimHandlerList(texAnimHandlerList.Count - 1).firstVxIdx = 0
                            texAnimHandlerList(texAnimHandlerList.Count - 1).isQuad = (polys(i).type Mod 2) = 1
                            ' texAnimHandlerList(texAnimHandlerList.Count - 1).MeshBufferIdx = VxAnim.Count
                            texAnimHandlerList(texAnimHandlerList.Count - 1).MeshBuffer = VxAnim.Last

                            vx.SetVertex(0, New Vertex3D(vexs(polys(i).vi0).Position,
                                                           vexs(polys(i).vi0).normal,
                                                           ColorsToRGB(polys(i).c0),
                                                           New Vector2D(polys(i).u0, polys(i).v0)))

                            vx.SetVertex(1, New Vertex3D(vexs(polys(i).vi1).Position,
                                                                     vexs(polys(i).vi1).normal,
                                                                     ColorsToRGB(polys(i).c1),
                                                                     New Vector2D(polys(i).u1, polys(i).v1)))

                            vx.SetVertex(2, New Vertex3D(vexs(polys(i).vi2).Position,
                                                               vexs(polys(i).vi2).normal,
                                                               ColorsToRGB(polys(i).c2),
                                                               New Vector2D(polys(i).u2, polys(i).v2)))



                            vx.SetIndex(0, 0)
                            vx.SetIndex(1, 1)
                            vx.SetIndex(2, 2)


                            RenderBoundingBox.AddInternalPoint(vx.GetVertex(j).Position)
                            RenderBoundingBox.AddInternalPoint(vx.GetVertex(j + 1).Position)
                            RenderBoundingBox.AddInternalPoint(vx.GetVertex(j + 2).Position)
                            CoM += vx.GetVertex(j).Position
                            CoM += vx.GetVertex(j + 1).Position
                            CoM += vx.GetVertex(j + 2).Position
                            cCoM += 3


                            If polys(i).type Mod 2 = 1 Then
                                'it's a quad!!! hey don't panic, I'll split it!

                                vx.SetVertex(3, New Vertex3D(vexs(polys(i).vi0).Position,
                                                             vexs(polys(i).vi0).normal,
                                                             ColorsToRGB(polys(i).c0),
                                                             New Vector2D(polys(i).u0, polys(i).v0)))

                                vx.SetVertex(4, New Vertex3D(vexs(polys(i).vi2).Position,
                                                            vexs(polys(i).vi2).normal,
                                                            ColorsToRGB(polys(i).c2),
                                                            New Vector2D(polys(i).u2, polys(i).v2)))






                                vx.SetVertex(5, New Vertex3D(vexs(polys(i).vi3).Position,
                                               vexs(polys(i).vi3).normal,
                                                  ColorsToRGB(polys(i).c3),
                                                 New Vector2D(polys(i).u3, polys(i).v3)))

                                vx.SetIndex(3, 3)
                                vx.SetIndex(4, 4)
                                vx.SetIndex(5, 5)

                                RenderBoundingBox.AddInternalPoint(vx.GetVertex(j + 3).Position)
                                CoM += vx.GetVertex(j + 3).Position
                                cCoM += 1
                            End If

                            vx.Material.Texture1 = Material(worldLoad.AllFrames(Current_animation_index)(0).Tex).Texture1

                        Else




                            vx = VxNor

                            vx.SetVertex(j, New Vertex3D(vexs(polys(i).vi0).Position,
                                                        vexs(polys(i).vi0).normal,
                                                        ColorsToRGB(polys(i).c0),
                                                        New Vector2D(polys(i).u0, polys(i).v0)))

                            vx.SetVertex(j + 1, New Vertex3D(vexs(polys(i).vi1).Position,
                                                                     vexs(polys(i).vi1).normal,
                                                                     ColorsToRGB(polys(i).c1),
                                                                     New Vector2D(polys(i).u1, polys(i).v1)))

                            vx.SetVertex(j + 2, New Vertex3D(vexs(polys(i).vi2).Position,
                                                               vexs(polys(i).vi2).normal,
                                                               ColorsToRGB(polys(i).c2),
                                                               New Vector2D(polys(i).u2, polys(i).v2)))




                            vx.SetIndex(j, j)
                            vx.SetIndex(j + 1, j + 1)
                            vx.SetIndex(j + 2, j + 2)


                            RenderBoundingBox.AddInternalPoint(vx.GetVertex(j).Position)
                            RenderBoundingBox.AddInternalPoint(vx.GetVertex(j + 1).Position)
                            RenderBoundingBox.AddInternalPoint(vx.GetVertex(j + 2).Position)
                            CoM += vx.GetVertex(j).Position
                            CoM += vx.GetVertex(j + 1).Position
                            CoM += vx.GetVertex(j + 2).Position
                            cCoM += 3


                            j += 3



                            If polys(i).type Mod 2 = 1 Then
                                'it's a quad!!! hey don't panic, I'll split it!

                                vx.SetVertex(j, New Vertex3D(vexs(polys(i).vi0).Position,
                                                             vexs(polys(i).vi0).normal,
                                                             ColorsToRGB(polys(i).c0),
                                                             New Vector2D(polys(i).u0, polys(i).v0)))

                                vx.SetVertex(j + 1, New Vertex3D(vexs(polys(i).vi2).Position,
                                                            vexs(polys(i).vi2).normal,
                                                            ColorsToRGB(polys(i).c2),
                                                            New Vector2D(polys(i).u2, polys(i).v2)))






                                vx.SetVertex(j + 2, New Vertex3D(vexs(polys(i).vi3).Position,
                                               vexs(polys(i).vi3).normal,
                                                  ColorsToRGB(polys(i).c3),
                                                 New Vector2D(polys(i).u3, polys(i).v3)))

                                vx.SetIndex(j, j)
                                vx.SetIndex(j + 1, j + 1)
                                vx.SetIndex(j + 2, j + 2)



                                RenderBoundingBox.AddInternalPoint(vx.GetVertex(j + 3).Position)
                                CoM += vx.GetVertex(j + 3).Position
                                cCoM += 1


                                j += 3
                            End If
                        End If



endit:
                        If kk = Texbuckets(k).Count - 1 And firstRun = True Then
                            firstRun = False
                            GoTo neg
                        End If
                    Next


                    VxNor.Material.NormalizeNormals = True



                    mesh.AddMeshBuffer(VxNor)
                    For Each mb In VxAnim
                        mesh.AddMeshBuffer(mb)
                    Next





                    'For each animation add this current mesh
                    '   If VxAnim.Count > 0 Then
                    '   For a = texAnimHandlerList.Count - 1 To 0 Step -1
                    '   If texAnimHandlerList(a).animMesh Is Nothing Then texAnimHandlerList(a).animMesh = mesh
                    'Next
                    '   End If


                    Dim scnNode As New SceneNode

                    '

                    '  setMaterialType(video: EMT_ONETEXTURE_BLEND);
                    'getMaterial(0).MaterialTypeParam = IRR() :
                    'video : pack_textureBlendFunc(IRR: video : EBF_SRC_ALPHA, IRR: video : EBF_ONE_MINUS_SRC_ALPHA, IRR: video : EMFN_MODULATE_1X, IRR: video : EAS_TEXTURE | irr:video : EAS_VERTEX_COLOR);
                    '                       getMaterial(0).setFlag(EMF_BLEND_OPERATION, True);


                    If firstRun Then scnNode = nVolt_ScnMgr.AddMeshSceneNode(mesh, nVolt_ScnMgr.RootSceneNode, -1) Else scnNode = nVolt_ScnMgr.AddMeshSceneNode(mesh, tScnNode, -1)


                    'scale 1, -1, 1
                    scnNode.Scale = New Vector3D(1, -1, 1)
                    RenderBoundingBox.Set(RenderBoundingBox.MinEdge.X, -RenderBoundingBox.MaxEdge.Y, RenderBoundingBox.MinEdge.Z,
RenderBoundingBox.MaxEdge.X, -RenderBoundingBox.MinEdge.Y, RenderBoundingBox.MaxEdge.Z)


                    scnNode.SetMaterialFlag(MaterialFlag.BackFaceCulling, False)
                    scnNode.AutomaticCulling = CullingType.Off
                    ' If k <> 26 Then scnNode.SetMaterialTexture(0, Material(k).Texture1)
                    If k <> 26 Then scnNode.GetMaterial(0).DiffuseColor.Set(255, 255, 255, 255)
                    If k <> 26 Then scnNode.SetMaterialType(MaterialType.TransparentAlphaChannel) 'TransparentVertexAlpha




                    If Not firstRun Then 'first run is alpha
                        '  scnNode.SetMaterialTexture(1, Material(polys(i).tpage).Texture1)
                        'scnNode.SetMaterialFlag(MaterialFlag.ZWriteEnable, True)                        'scnNode.SetMaterialType(MaterialType.TransparentVertexAlpha) 'TransparentVertexAlpha

                        '  scnNode.SetMaterialType(MaterialType.TransparentReflection2Layer) 'TransparentVertexAlpha
                    End If


                    If k <> 26 Then scnNode.SetMaterialFlag(MaterialFlag.Lighting, False)

                    '  scnNode.SetMaterialType(MaterialType.TransparentAlphaChannel)
                    '   scnNode(k).SetMaterialFlag(MaterialFlag.AnisotropicFilter, True)
                    'scnNode(k).SetMaterialType(MaterialType.TransparentAddColor)
                    scnNode.SetMaterialFlag(MaterialFlag.BilinearFilter, True)
                    ' scnNode(k).SetMaterialFlag(MaterialFlag.TrilinearFilter, True)

                    SceneNodes.Add(scnNode)


                Next








            Next



            ' ScnMgr.RegisterNodeForRendering(tScnNode, SceneNodeRenderPass.Transparent)
        End Sub

    End Class

    Public Structure MODEL_POLY_LOAD
        Dim type As Int16
        Dim tpage As Int16
        Dim vi0, vi1, vi2, vi3 As uInt16
        Dim c0, c1, c2, c3 As Long
        Dim u0, v0, u1, v1, u2, v2, u3, v3 As Single
    End Structure

    Public Structure MODEL_VERTEX_MORPH
        Dim Position As Vector3D
        Dim normal As Vector3D
    End Structure
    Public Class WorldMesh
        Public BoundingSphere As Sphere
        Public bbox As BBOX
        Public polynum, vertnum As UShort
        Public polyl() As MODEL_POLY_LOAD
        Public vexl() As MODEL_VERTEX_MORPH


    End Class

End Module
