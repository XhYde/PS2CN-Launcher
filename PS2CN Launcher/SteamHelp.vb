Public Class SteamHelp

    Private Sub SteamHelp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '窗体在主界面左侧显示，且不超出屏幕左上
        Me.Left = IIf(
            LauncherForm1.Left - 639 > 0,
            LauncherForm1.Left - 639,
            0
            )
        Me.Top = IIf(
            LauncherForm1.Bottom - 591 > 0,
            LauncherForm1.Bottom - 591,
            0
            )
    End Sub
End Class