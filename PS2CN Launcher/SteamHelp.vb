Public Class SteamHelp

    Private Sub SteamHelp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '窗体在主界面左侧显示，且不超出屏幕左上
        Me.Left = IIf( _
            PS2CN_Launcher.LauncherForm1.Left - 639 > 0, _
            PS2CN_Launcher.LauncherForm1.Left - 639, _
            0 _
            )
        Me.Top = IIf( _
            PS2CN_Launcher.LauncherForm1.Bottom - 591 > 0, _
            PS2CN_Launcher.LauncherForm1.Bottom - 591, _
            0 _
            )
    End Sub
End Class