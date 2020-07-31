Imports IrrlichtNETCP
Module YUIRender

    'Previewer
    Public device As IrrlichtDevice
    Public videoDriver As VideoDriver
    Public ScnMgr As SceneManager
    Public cam As CameraSceneNode
    Public Plane As AnimatedMesh
    Public PScn As SceneNode 'Plane Scene Node


    'UV editor
    Public device2 As IrrlichtDevice
    Public videoDriver2 As VideoDriver
    Public ScnMgr2 As SceneManager
    Public cam2 As CameraSceneNode
    Public Plane2 As AnimatedMesh
    Public PScn2 As SceneNode

    'RENDER STUFFS
    '------------------------------------------------------------------------------------------------------------------------------
    Public Sub Init()
        'MAIN (init)
        device = New IrrlichtDevice(DriverType.Direct3D9, New Dimension2D(380, 340), 32, False, True, False, False, Form1.Panel2.Handle)
        device.CursorControl.Visible = True
        videoDriver = device.VideoDriver
        ScnMgr = device.SceneManager

        'Mesh, Camera  of main
        Plane = ScnMgr.AddHillPlaneMesh("plane", New Dimension2Df(1, 1), New Dimension2D(1, 1), 0, New Dimension2D(1, 1), New Dimension2Df(1, 1))
        PScn = ScnMgr.AddAnimatedMeshSceneNode(Plane)

        InitCamera()

        '----------------------------------------------------------------------------------------
        'UV editor (hardware accelerated, better!)
        device2 = New IrrlichtDevice(DriverType.Direct3D8, New Dimension2D(256, 256), 32, False, True, False, False, Form1.Panel16.Handle)

        videoDriver2 = device2.VideoDriver
        ScnMgr2 = device2.SceneManager

        'small plane...
        Plane2 = ScnMgr2.AddHillPlaneMesh("plane2", New Dimension2Df(1, 1), New Dimension2D(1, 1), 0, New Dimension2D(1, 1), New Dimension2Df(1, 1))
        PScn2 = ScnMgr2.AddAnimatedMeshSceneNode(Plane2)

        PScn2.GetMaterial(0).Lighting = False
        Dim cam2 = ScnMgr2.AddCameraSceneNode(Nothing)
        cam2.Position = New Vector3D(0, 0.7, -0.001)
        cam2.Target = New Vector3D(0, 0, 0)

        ' camera

        cam2.NearValue = 0.001
        cam2.AutomaticCulling = CullingType.Off
        '  videoDriver2.SetTransform(TransformationState.View, cam2.AbsoluteTransformation)
        cam2.UpVector = New Vector3D(0, 1, 0)

        '  cam2.Rotation = New Vector3D(90, 0, 0)
        'light
        'ScnMgr2.AddLightSceneNode(Nothing, cam.Position, New Colorf(255, 230, 230, 230), 1, -1)
        '------------------------------------------------------------------------------------------


        'Cook!
        LoadBitmaps()
        InitFrames()
        InitSelectors()

        'Enable Disable and start
        Form1.EnableDisablePanels()
        Form1.ComboBox1.SelectedIndex = 0

        'Init CC
        InitCC()


        Go()
    End Sub
    Public Sub InitCamera()
        cam = ScnMgr.AddCameraSceneNodeMaya(ScnMgr.RootSceneNode, 50, 20, 0, -1)


        cam.Position = New Vector3D(0, 1, 0)
        cam.Target = New Vector3D(0, 0, 0)
        cam.AutomaticCulling = CullingType.Off
        videoDriver.SetTransform(TransformationState.View, cam.AbsoluteTransformation)
        cam.NearValue = 0.01
        cam.FarValue = 32768



        Dim Light As New Light
        ScnMgr.AddLightSceneNode(Nothing, cam.Position, New Colorf(255, 230, 230, 230), 1, -1)

    End Sub
    Public Sub Go()
        Try
            Do Until (device.Run Or device2.Run) = False


                videoDriver.BeginScene(True, True, Color.From(255, 230, 230, 230))
                videoDriver2.BeginScene(True, True, Color.From(255, 230, 230, 230))

                Form1.Label1.Text = "FPS: " & videoDriver.FPS


                ScnMgr.DrawAll()
                ScnMgr2.DrawAll()

                'Selectors
                If Not Loading Then MapSelectors()

                'draw everything
                videoDriver.EndScene()
                videoDriver2.EndScene()
            Loop

        Catch ex As Exception



            Go()
        End Try




    End Sub

End Module
