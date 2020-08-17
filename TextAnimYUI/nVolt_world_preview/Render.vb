Imports IrrlichtNETCP
Module nVolt_Render
    Public nVolt_Zoom = 1 / 10

    Public BackColor As Color = New Color(159, 35, 138, 255)

    Public nVolt_Device As IrrlichtDevice
    Public nVolt_VideoDriver As VideoDriver
    Public nVolt_ScnMgr As SceneManager
    Public tScnNode As SceneNode
    Public nVolt_guiEnv As GUIEnvironment
    Public nVolt_mEvent As IrrlichtNETCP.Event

    Public nVolt_cam As CameraSceneNode

    Dim Width = 1024, Height = 800
    Dim DvType As DriverType = DriverType.Direct3D9
    Dim bits = 32
    Dim FullScreen As Boolean = False
    Dim Vsync As Boolean = True
    Dim Stencil As Boolean = False
    Dim AntiAlias As Boolean = False

    Sub nVolt_Init()
        'ok, what about init?
        nVolt_Device = New IrrlichtDevice(DvType, New Dimension2D(Width, Height), bits, FullScreen, Stencil, Vsync, AntiAlias, texanimWExport.renderWindowPanel.Handle)
        nVolt_VideoDriver = nVolt_Device.VideoDriver
        nVolt_ScnMgr = nVolt_Device.SceneManager

        tScnNode = New SceneNode() ' ScnMgr.AddCameraSceneNode(Nothing)

        nVolt_Device.CursorControl.Visible = True
        nVolt_Device.Resizeable = True
        AddHandler nVolt_Device.OnEvent, AddressOf onEvent
        nVolt_guiEnv = nVolt_Device.GUIEnvironment
        nVolt_Device.WindowCaption = "nVolt"
        InitCamera()


    End Sub
    Function onEvent(ByVal Ev As IrrlichtNETCP.Event) As Boolean
        ' If Ev.KeyPressedDown <> True Then Exit Function


        ' cam = ScnMgr.AddCameraSceneNodeMaya(ScnMgr.RootSceneNode, 50, 100, 200, -1)
        '  If Ev.KeyCode = KeyCode.Escape Or Ev.KeyCode = KeyCode.Space Then
        '   If mainform.CheckBox2.Checked And True Then
        ''       mainform.CheckBox2.Checked = False
        '   Else
        '       mainform.CheckBox2.Checked = True
        '   End If
        '   = False
        '  End If
        'Device.Dispose()
        ' End
        ' End If

    End Function

    Sub Go()
        'For Start rendering & should be the last thing
        'Try
        'Device.Run()
        On Error Resume Next
        Do Until nVolt_Device.Run = False
                nVolt_Device.WindowCaption = "nVolt ~ fps:" & nVolt_VideoDriver.FPS          'fps?
                texanimWExport.Label1.Text = String.Format("Cam: {0}   {1}  {2}.", nVolt_cam.Position.X / nVolt_Zoom, nVolt_cam.Position.Y / nVolt_Zoom, nVolt_cam.Position.Z / nVolt_Zoom)
                texanimWExport.Text = "Texanim W Export | nVolt ~ fps:" & nVolt_VideoDriver.FPS          'fps?
                nVolt_VideoDriver.BeginScene(True, True, BackColor)    'clear buffer
                nVolt_ScnMgr.DrawAll()
                nVolt_guiEnv.DrawAll()
                nVolt_VideoDriver.EndScene()

            Loop



    End Sub
    Sub InitCamera()
        nVolt_cam = nVolt_ScnMgr.AddCameraSceneNodeMaya(nVolt_ScnMgr.RootSceneNode, 100, 200, 5000, -1)

        '  Volt_cam = ScnMgr.AddCameraSceneNodeFPS(nVolt_ScnMgr.RootSceneNode, 50, 100, False)

        nVolt_cam.Position = New Vector3D(-4000, 400, 8000)
        '  Debugger.Break()
        nVolt_cam.Target = New Vector3D(0, 0, 0)
        nVolt_cam.AutomaticCulling = CullingType.Off
        nVolt_VideoDriver.SetTransform(TransformationState.View, cam.AbsoluteTransformation)
        nVolt_cam.NearValue = 0.01
        nVolt_cam.FarValue = 32768

        'nVolt_cam.UpVector = New IrrlichtNETCP.Vector3D(0, 1, 0)


    End Sub


End Module

Public Module Conv
    Function fromIrrColorToColor(ByVal irrColor As IrrlichtNETCP.Color) As System.Drawing.Color
        Return System.Drawing.Color.FromArgb(irrColor.A, irrColor.R, irrColor.G, irrColor.B)
    End Function
    Function fromColorToIrrColor(ByVal c As System.Drawing.Color) As Color
        Return New Color(c.A, c.R, c.G, c.B)
    End Function
End Module
