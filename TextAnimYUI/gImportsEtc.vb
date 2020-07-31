Imports System.Runtime.InteropServices

Module gImportsEtc
    <DllImport("user32.dll", EntryPoint:="GetWindowThreadProcessId", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
  Public Function GetWindowThreadProcessId(ByVal hWnd As Long, ByVal lpdwProcessId As Long) As Long
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Long
    End Function

    <DllImport("user32.dll", EntryPoint:="GetWindowLongA", SetLastError:=True)> _
    Public Function GetWindowLong(ByVal hwnd As IntPtr, ByVal nIndex As Integer) As Long
    End Function

    <DllImport("user32.dll")> _
    Public Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Function SetWindowPos(ByVal hwnd As IntPtr, ByVal hWndInsertAfter As Long, ByVal x As Long, ByVal y As Long, ByVal cx As Long, ByVal cy As Long, _
     ByVal wFlags As Long) As Long
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Function MoveWindow(ByVal hwnd As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal repaint As Boolean) As Boolean
    End Function

    <DllImport("user32.dll", EntryPoint:="PostMessageA", SetLastError:=True)> _
    Public Function PostMessage(ByVal hwnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
    End Function
    <DllImport("user32.dll", EntryPoint:="FindWindow", SetLastError:=True, CharSet:=CharSet.Auto)> _
 Public Function FindWindowByCaption( _
      ByVal zero As IntPtr, _
      ByVal lpWindowName As String) As IntPtr
    End Function

    Public Const SWP_NOOWNERZORDER As Integer = &H200
    Public Const SWP_NOREDRAW As Integer = &H8
    Public Const SWP_NOZORDER As Integer = &H4
    Public Const SWP_SHOWWINDOW As Integer = &H40
    Public Const WS_EX_MDICHILD As Integer = &H40
    Public Const SWP_FRAMECHANGED As Integer = &H20
    Public Const SWP_NOACTIVATE As Integer = &H10
    Public Const SWP_ASYNCWINDOWPOS As Integer = &H4000
    Public Const SWP_NOMOVE As Integer = &H2
    Public Const SWP_NOSIZE As Integer = &H1
    Public Const GWL_STYLE As Integer = (-16)
    Public Const WS_VISIBLE As Integer = &H10000000
    Public Const WM_CLOSE As Integer = &H10
    Public Const WS_CHILD As Integer = &H40000000
    Public Const WS_MAXIMIZE As Integer = &H1000000





End Module
