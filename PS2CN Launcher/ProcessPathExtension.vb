Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text

Module ProcessPathExtension
    <DllImport("kernel32.dll")>
    Private Function QueryFullProcessImageName(hprocess As IntPtr, dwFlags As Integer, lpExeName As StringBuilder, ByRef size As Integer) As Boolean
    End Function
    <DllImport("kernel32.dll")>
    Private Function OpenProcess(dwDesiredAccess As ProcessAccessFlags, bInheritHandle As Boolean, dwProcessId As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function CloseHandle(hHandle As IntPtr) As Boolean
    End Function

    Enum ProcessAccessFlags As UInteger
        All = &H1F0FFF
        Terminate = &H1
        CreateThread = &H2
        VMOperation = &H8
        VMRead = &H10
        VMWrite = &H20
        DupHandle = &H40
        SetInformation = &H200
        QueryInformation = &H400
        QueryLimitedInformation = &H1000
        Synchronize = &H100000
    End Enum

    <Extension()>
    Public Function Path(ByVal _process As Process) As String
        Dim processPath As String = ""

        ' The new QueryLimitedInformation flag is only available on Windows Vista and up.
        If Environment.OSVersion.Version.Major >= 6 Then
            Dim processHandle As IntPtr = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, False, _process.Id)
            Try
                If Not processHandle = IntPtr.Zero Then
                    Dim buffer = New StringBuilder(1024)
                    If QueryFullProcessImageName(processHandle, 0, buffer, buffer.Capacity) Then
                        processPath = buffer.ToString()
                    End If
                End If
            Finally
                CloseHandle(processHandle)
            End Try
        Else
            processPath = _process.MainModule.FileName
        End If

        Return processPath
    End Function
End Module
