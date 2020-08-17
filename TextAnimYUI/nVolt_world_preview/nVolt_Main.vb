
Imports IrrlichtNETCP
    Imports System.Math
Module nVolt_Main

    Public thisWorldFile As WorldFile
    Public theOriginalWorldFile As WorldFile
    Public isInitialized As Boolean = False

    Sub Main(path$)


        'Init graphics
        nVolt_Init()




        thisWorldFile = New WorldFile(CurrentWorld)
        theOriginalWorldFile = New WorldFile(CurrentWorld)
        Dim nList As New List(Of Integer)

        texanimWExport.ListBox1.Items.Clear()
        texanimWExport.ListBox2.Items.Clear()

        For k = 0 To thisWorldFile.worldLoad.meshCount - 1
            texanimWExport.ListBox1.Items.Add(Strings.Format(k, "000"))
            nList.Add(k)
            texanimWExport.ListBox1.SelectedIndices.Add(k)
        Next

        For k = 0 To thisWorldFile.worldLoad.AllFrames.Count - 1
            texanimWExport.ListBox2.Items.Add(k)
        Next
        texanimWExport.ListBox2.Items.Add(thisWorldFile.worldLoad.AllFrames.Count & " [new]")

        thisWorldFile.worldLoad.AllFrames.Add(Frames)


        thisWorldFile.Render(nList)

        isInitialized = True
        For Each item In thisWorldFile.texAnimHandlerList
            item.Play()
        Next



        nVolt_Render.Go()

    End Sub
End Module
