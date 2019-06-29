<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LauncherForm1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LauncherForm1))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.LabelDir = New System.Windows.Forms.Label()
        Me.ButtonOpenFile = New System.Windows.Forms.Button()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.LabelPing = New System.Windows.Forms.Label()
        Me.LabelLosePkg = New System.Windows.Forms.Label()
        Me.LabelSteam = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ComboBoxServer = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LabelIPLoc = New System.Windows.Forms.Label()
        Me.LabelLogo = New System.Windows.Forms.Label()
        Me.CheckBox32bit = New System.Windows.Forms.CheckBox()
        Me.CheckBoxENVoice = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCN = New System.Windows.Forms.CheckBox()
        Me.LabelReadme = New System.Windows.Forms.Label()
        Me.LabelLocStatus = New System.Windows.Forms.Label()
        Me.LabelUpdate = New System.Windows.Forms.Label()
        Me.ComboBoxTimeZone1 = New System.Windows.Forms.ComboBox()
        Me.CheckBoxDST1 = New System.Windows.Forms.CheckBox()
        Me.ComboBoxHour1 = New System.Windows.Forms.ComboBox()
        Me.ComboBoxHour2 = New System.Windows.Forms.ComboBox()
        Me.CheckBoxDST2 = New System.Windows.Forms.CheckBox()
        Me.ComboBoxTimeZone2 = New System.Windows.Forms.ComboBox()
        Me.ComboBoxHour3 = New System.Windows.Forms.ComboBox()
        Me.CheckBoxDST3 = New System.Windows.Forms.CheckBox()
        Me.ComboBoxTimeZone3 = New System.Windows.Forms.ComboBox()
        Me.ComboBoxFontSelector = New System.Windows.Forms.ComboBox()
        Me.CheckBoxAutoRestore = New System.Windows.Forms.CheckBox()
        Me.RadioButtonServerStatus = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTimeZoneConverter = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCheckIPLoc = New System.Windows.Forms.RadioButton()
        Me.ButtonFixLauncher503 = New System.Windows.Forms.Button()
        Me.ComboBoxClientType = New System.Windows.Forms.ComboBox()
        Me.LabelComboBoxFontSelector = New System.Windows.Forms.Label()
        Me.CheckBoxShowFunctionUI = New System.Windows.Forms.CheckBox()
        Me.LabelPatchNote = New System.Windows.Forms.Label()
        Me.LabelDownloadGameUpdate = New System.Windows.Forms.Label()
        Me.LabelCheckIPLocTitle = New System.Windows.Forms.Label()
        Me.RadioButtonMiscFunctions = New System.Windows.Forms.RadioButton()
        Me.ComboBoxFunctionSelector = New System.Windows.Forms.ComboBox()
        Me.ComboBoxCN = New System.Windows.Forms.ComboBox()
        Me.LabelPlayerStatistics = New System.Windows.Forms.Label()
        Me.LabelPlayerKillboard = New System.Windows.Forms.Label()
        Me.LabelGErrorCodeQuery = New System.Windows.Forms.Label()
        Me.LabelVisitOfficialSite = New System.Windows.Forms.Label()
        Me.LabelVisitReddit = New System.Windows.Forms.Label()
        Me.LabelManuallyTranslate = New System.Windows.Forms.Label()
        Me.LabelKillLaunchPad = New System.Windows.Forms.Label()
        Me.LabelKillGameClient = New System.Windows.Forms.Label()
        Me.LabelFontSelector = New System.Windows.Forms.Label()
        Me.LabelTimeZoneTitle = New System.Windows.Forms.Label()
        Me.PanelTimeZoneConverter = New System.Windows.Forms.Panel()
        Me.LabelHourTitle = New System.Windows.Forms.Label()
        Me.LabelDSTTitle = New System.Windows.Forms.Label()
        Me.PanelFunctionSelector = New System.Windows.Forms.Panel()
        Me.PanelFunctionRadioButtonList = New System.Windows.Forms.Panel()
        Me.PanelServerStatus = New System.Windows.Forms.Panel()
        Me.LabelServer8 = New System.Windows.Forms.Label()
        Me.ButtonRequestServerStatus = New System.Windows.Forms.Button()
        Me.LabelServer1 = New System.Windows.Forms.Label()
        Me.LabelServer2 = New System.Windows.Forms.Label()
        Me.LabelServer3 = New System.Windows.Forms.Label()
        Me.LabelServer4 = New System.Windows.Forms.Label()
        Me.LabelServer5 = New System.Windows.Forms.Label()
        Me.LabelServer6 = New System.Windows.Forms.Label()
        Me.LabelServer7 = New System.Windows.Forms.Label()
        Me.PanelComboBoxFontSelector = New System.Windows.Forms.Panel()
        Me.ButtonRun = New System.Windows.Forms.Button()
        Me.LabelRun = New System.Windows.Forms.Label()
        Me.LabelSelectClientType = New System.Windows.Forms.Label()
        Me.LabelSplitLine = New System.Windows.Forms.Label()
        Me.PanelCheckIPLoc = New System.Windows.Forms.Panel()
        Me.LabelCheckIPLocDesc = New System.Windows.Forms.Label()
        Me.PanelPingTester = New System.Windows.Forms.Panel()
        Me.PanelSelectClientTypeDir = New System.Windows.Forms.Panel()
        Me.PanelDir = New System.Windows.Forms.Panel()
        Me.LabelCN = New System.Windows.Forms.Label()
        Me.LabelTestDisplay1 = New System.Windows.Forms.Label()
        Me.LabelTestDisplay2 = New System.Windows.Forms.Label()
        Me.PanelMiscFunctions = New System.Windows.Forms.Panel()
        Me.LabelPlayerName = New System.Windows.Forms.Label()
        Me.TextBoxPlayerName = New System.Windows.Forms.TextBox()
        Me.PanelTimeZoneConverter.SuspendLayout()
        Me.PanelFunctionSelector.SuspendLayout()
        Me.PanelFunctionRadioButtonList.SuspendLayout()
        Me.PanelServerStatus.SuspendLayout()
        Me.PanelComboBoxFontSelector.SuspendLayout()
        Me.PanelCheckIPLoc.SuspendLayout()
        Me.PanelPingTester.SuspendLayout()
        Me.PanelSelectClientTypeDir.SuspendLayout()
        Me.PanelDir.SuspendLayout()
        Me.PanelMiscFunctions.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "游戏客户端|PlanetSide2_x64.exe;PlanetSide2.exe;LaunchPad.exe"
        Me.OpenFileDialog1.Title = "选择游戏客户端位置"
        '
        'LabelDir
        '
        Me.LabelDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDir.AutoEllipsis = True
        Me.LabelDir.BackColor = System.Drawing.Color.Transparent
        Me.LabelDir.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelDir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelDir.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelDir.ForeColor = System.Drawing.Color.Gold
        Me.LabelDir.Location = New System.Drawing.Point(1, 4)
        Me.LabelDir.Name = "LabelDir"
        Me.LabelDir.Size = New System.Drawing.Size(444, 21)
        Me.LabelDir.TabIndex = 4
        Me.LabelDir.Text = "Steam://rungameid/218230"
        Me.LabelDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.LabelDir, "选择客户端exe程序位置")
        '
        'ButtonOpenFile
        '
        Me.ButtonOpenFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOpenFile.BackColor = System.Drawing.Color.Silver
        Me.ButtonOpenFile.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonOpenFile.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ButtonOpenFile.ForeColor = System.Drawing.Color.Black
        Me.ButtonOpenFile.Location = New System.Drawing.Point(421, 6)
        Me.ButtonOpenFile.Name = "ButtonOpenFile"
        Me.ButtonOpenFile.Size = New System.Drawing.Size(21, 17)
        Me.ButtonOpenFile.TabIndex = 3
        Me.ButtonOpenFile.Text = "…"
        Me.ButtonOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.ButtonOpenFile, "选择客户端exe程序位置")
        Me.ButtonOpenFile.UseVisualStyleBackColor = False
        '
        'LabelStatus
        '
        Me.LabelStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelStatus.BackColor = System.Drawing.Color.Transparent
        Me.LabelStatus.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelStatus.ForeColor = System.Drawing.Color.Orange
        Me.LabelStatus.Image = Global.PS2CN_Launcher.My.Resources.Resources.VB_bg_ButtonStatDesc
        Me.LabelStatus.Location = New System.Drawing.Point(460, 340)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(250, 61)
        Me.LabelStatus.TabIndex = 41
        Me.LabelStatus.Text = "请在登录器登录账号,等按钮变为" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "【Play】,且绿色进度条填满后" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "再点击↓【开始修改】↓"
        Me.LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelPing
        '
        Me.LabelPing.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPing.BackColor = System.Drawing.Color.Transparent
        Me.LabelPing.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelPing.ForeColor = System.Drawing.Color.Gold
        Me.LabelPing.Image = Global.PS2CN_Launcher.My.Resources.Resources.VB_bg_LatencyTitle
        Me.LabelPing.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LabelPing.Location = New System.Drawing.Point(0, 0)
        Me.LabelPing.Name = "LabelPing"
        Me.LabelPing.Size = New System.Drawing.Size(130, 22)
        Me.LabelPing.TabIndex = 42
        Me.LabelPing.Text = "0毫秒"
        Me.LabelPing.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelLosePkg
        '
        Me.LabelLosePkg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelLosePkg.BackColor = System.Drawing.Color.Transparent
        Me.LabelLosePkg.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelLosePkg.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelLosePkg.ForeColor = System.Drawing.Color.Gold
        Me.LabelLosePkg.Image = Global.PS2CN_Launcher.My.Resources.Resources.VB_bg_LosePkgTitle
        Me.LabelLosePkg.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LabelLosePkg.Location = New System.Drawing.Point(0, 22)
        Me.LabelLosePkg.Name = "LabelLosePkg"
        Me.LabelLosePkg.Size = New System.Drawing.Size(130, 22)
        Me.LabelLosePkg.TabIndex = 43
        Me.LabelLosePkg.Text = "点击重置"
        Me.LabelLosePkg.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.LabelLosePkg, "点击以重置丢包率统计。")
        '
        'LabelSteam
        '
        Me.LabelSteam.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelSteam.BackColor = System.Drawing.Color.Transparent
        Me.LabelSteam.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelSteam.Enabled = False
        Me.LabelSteam.ForeColor = System.Drawing.Color.Gold
        Me.LabelSteam.Image = Global.PS2CN_Launcher.My.Resources.Resources.VB_bg_SteamHelpIcon
        Me.LabelSteam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelSteam.Location = New System.Drawing.Point(349, 337)
        Me.LabelSteam.MinimumSize = New System.Drawing.Size(103, 19)
        Me.LabelSteam.Name = "LabelSteam"
        Me.LabelSteam.Size = New System.Drawing.Size(103, 19)
        Me.LabelSteam.TabIndex = 40
        Me.LabelSteam.Text = "    Steam版位置"
        Me.LabelSteam.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.LabelSteam, "点击查看Steam版客户端路径选取方法。")
        Me.LabelSteam.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'ComboBoxServer
        '
        Me.ComboBoxServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.ComboBoxServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxServer.DropDownWidth = 128
        Me.ComboBoxServer.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ComboBoxServer.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxServer.Items.AddRange(New Object() {"(不测试ping)", "美西-康纳利 (失效)", "美东-艾莫若德", "澳洲-布里格斯 (失效)", "欧洲-柯伯特 (失效)", "欧洲-米勒 (失效)"})
        Me.ComboBoxServer.Location = New System.Drawing.Point(589, 6)
        Me.ComboBoxServer.MaxDropDownItems = 15
        Me.ComboBoxServer.Name = "ComboBoxServer"
        Me.ComboBoxServer.Size = New System.Drawing.Size(126, 20)
        Me.ComboBoxServer.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.ComboBoxServer, "目前美西/澳洲等服务器已禁用测ping，其他服务器对游戏内延时的参考价值不高。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "建议仅用于测试VPN连接性和丢包率。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果断网再恢复后，测ping功能失效，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "请先选择""不测试ping""，十秒后重新选择目标服务器即可。")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 0
        Me.ToolTip1.InitialDelay = 100
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ReshowDelay = 100
        '
        'LabelIPLoc
        '
        Me.LabelIPLoc.AutoSize = True
        Me.LabelIPLoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(19, Byte), Integer))
        Me.LabelIPLoc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelIPLoc.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelIPLoc.ForeColor = System.Drawing.Color.Gold
        Me.LabelIPLoc.Location = New System.Drawing.Point(3, 19)
        Me.LabelIPLoc.MaximumSize = New System.Drawing.Size(275, 23)
        Me.LabelIPLoc.MinimumSize = New System.Drawing.Size(100, 23)
        Me.LabelIPLoc.Name = "LabelIPLoc"
        Me.LabelIPLoc.Size = New System.Drawing.Size(129, 23)
        Me.LabelIPLoc.TabIndex = 38
        Me.LabelIPLoc.Text = "大家嚎，我是黄字"
        Me.ToolTip1.SetToolTip(Me.LabelIPLoc, "点击可检测IP归属地。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "请避免短期连续查询。")
        '
        'LabelLogo
        '
        Me.LabelLogo.BackColor = System.Drawing.Color.Transparent
        Me.LabelLogo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelLogo.Image = Global.PS2CN_Launcher.My.Resources.Resources.ps2cn_logo
        Me.LabelLogo.Location = New System.Drawing.Point(0, 0)
        Me.LabelLogo.Name = "LabelLogo"
        Me.LabelLogo.Size = New System.Drawing.Size(288, 112)
        Me.LabelLogo.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.LabelLogo, "PS2CN.com已退役，现改在Github发布。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(· ω·)つ【珍爱智商，远离贴吧】")
        '
        'CheckBox32bit
        '
        Me.CheckBox32bit.AutoSize = True
        Me.CheckBox32bit.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox32bit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox32bit.Enabled = False
        Me.CheckBox32bit.ForeColor = System.Drawing.Color.Gold
        Me.CheckBox32bit.Location = New System.Drawing.Point(236, 313)
        Me.CheckBox32bit.Name = "CheckBox32bit"
        Me.CheckBox32bit.Size = New System.Drawing.Size(113, 21)
        Me.CheckBox32bit.TabIndex = 11
        Me.CheckBox32bit.Text = "启用32位客户端"
        Me.ToolTip1.SetToolTip(Me.CheckBox32bit, "不建议开启。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "仅当64位客户端不稳定时使用。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "点击选框不会立即生效，直到点击【开始修改】后才会生效。")
        Me.CheckBox32bit.UseVisualStyleBackColor = False
        Me.CheckBox32bit.Visible = False
        '
        'CheckBoxENVoice
        '
        Me.CheckBoxENVoice.AutoSize = True
        Me.CheckBoxENVoice.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxENVoice.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxENVoice.Enabled = False
        Me.CheckBoxENVoice.ForeColor = System.Drawing.Color.Gold
        Me.CheckBoxENVoice.Location = New System.Drawing.Point(12, 313)
        Me.CheckBoxENVoice.Name = "CheckBoxENVoice"
        Me.CheckBoxENVoice.Size = New System.Drawing.Size(99, 21)
        Me.CheckBoxENVoice.TabIndex = 13
        Me.CheckBoxENVoice.Text = "使用英语语音"
        Me.ToolTip1.SetToolTip(Me.CheckBoxENVoice, "仅针对俄服有效。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "点击选框不会立即生效，直到点击【开始修改】后才会生效。")
        Me.CheckBoxENVoice.UseVisualStyleBackColor = False
        Me.CheckBoxENVoice.Visible = False
        '
        'CheckBoxCN
        '
        Me.CheckBoxCN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCN.AutoSize = True
        Me.CheckBoxCN.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCN.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxCN.Enabled = False
        Me.CheckBoxCN.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CheckBoxCN.ForeColor = System.Drawing.Color.Gold
        Me.CheckBoxCN.Location = New System.Drawing.Point(12, 340)
        Me.CheckBoxCN.Name = "CheckBoxCN"
        Me.CheckBoxCN.Size = New System.Drawing.Size(84, 24)
        Me.CheckBoxCN.TabIndex = 7
        Me.CheckBoxCN.Text = "启用汉化"
        Me.ToolTip1.SetToolTip(Me.CheckBoxCN, "【勾选】后将把游戏汉化为中文。【取消勾选】将使用外语原版进行游戏。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "若更新后最新内容出现乱码，可【取消勾选】此选项，查看外语原文。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "点击选框不会立即生效，直到" &
        "点击【开始修改】后才会生效。")
        Me.CheckBoxCN.UseVisualStyleBackColor = False
        Me.CheckBoxCN.Visible = False
        '
        'LabelReadme
        '
        Me.LabelReadme.BackColor = System.Drawing.Color.Transparent
        Me.LabelReadme.Cursor = System.Windows.Forms.Cursors.Help
        Me.LabelReadme.Font = New System.Drawing.Font("微软雅黑", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.LabelReadme.ForeColor = System.Drawing.Color.White
        Me.LabelReadme.Image = Global.PS2CN_Launcher.My.Resources.Resources.VB_bg_ReadmeText
        Me.LabelReadme.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelReadme.Location = New System.Drawing.Point(5, 380)
        Me.LabelReadme.Name = "LabelReadme"
        Me.LabelReadme.Size = New System.Drawing.Size(84, 21)
        Me.LabelReadme.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.LabelReadme, "点击阅读使用说明")
        '
        'LabelLocStatus
        '
        Me.LabelLocStatus.AutoSize = True
        Me.LabelLocStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(24, Byte), Integer))
        Me.LabelLocStatus.ForeColor = System.Drawing.Color.Silver
        Me.LabelLocStatus.Location = New System.Drawing.Point(0, 42)
        Me.LabelLocStatus.MaximumSize = New System.Drawing.Size(278, 76)
        Me.LabelLocStatus.MinimumSize = New System.Drawing.Size(180, 20)
        Me.LabelLocStatus.Name = "LabelLocStatus"
        Me.LabelLocStatus.Size = New System.Drawing.Size(180, 38)
        Me.LabelLocStatus.TabIndex = 39
        Me.LabelLocStatus.Text = "点击黄字，检测IP归属地。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "开始修改时会自动检测。"
        Me.ToolTip1.SetToolTip(Me.LabelLocStatus, "点击黄字，检测IP归属地。")
        '
        'LabelUpdate
        '
        Me.LabelUpdate.AutoSize = True
        Me.LabelUpdate.BackColor = System.Drawing.Color.Transparent
        Me.LabelUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelUpdate.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelUpdate.ForeColor = System.Drawing.Color.Gold
        Me.LabelUpdate.Location = New System.Drawing.Point(4, 112)
        Me.LabelUpdate.MaximumSize = New System.Drawing.Size(278, 85)
        Me.LabelUpdate.MinimumSize = New System.Drawing.Size(200, 17)
        Me.LabelUpdate.Name = "LabelUpdate"
        Me.LabelUpdate.Size = New System.Drawing.Size(200, 17)
        Me.LabelUpdate.TabIndex = 36
        Me.LabelUpdate.Text = "点击访问汉化发布页。"
        Me.ToolTip1.SetToolTip(Me.LabelUpdate, "点击访问汉化发布页。")
        '
        'ComboBoxTimeZone1
        '
        Me.ComboBoxTimeZone1.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.ComboBoxTimeZone1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTimeZone1.DropDownWidth = 370
        Me.ComboBoxTimeZone1.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ComboBoxTimeZone1.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxTimeZone1.FormattingEnabled = True
        Me.ComboBoxTimeZone1.Items.AddRange(New Object() {"UTC-12(国际日期变更线)", "UTC-11(MIT-中途岛标准时间)", "UTC-10(HST-夏威夷－阿留申标准时间)", "UTC-9 (AKST-阿拉斯加标准时间)", "UTC-8 (PST-北美西部加州圣地亚哥时间,PSTA-太平洋标准时间A)", "UTC-7 (MST-北美山区标准时间)", "UTC-6 (CST-北美中部标准时间)", "UTC-5 (EST-北美东部标准时间)", "UTC-4 (AST-大西洋标准时间)", "UTC-3 (SAT-南美标准时间)", "UTC-2 (BRT-巴西时间)", "UTC-1 (CVT-佛得角标准时间)", "UTC   (GMT-格林威治标准时间,WET-欧洲西部时区,英国时间)", "UTC+1 (CET-欧洲中部时区,法国、德国时间)", "UTC+2 (EET-欧洲东部时区)", "UTC+3 (MSK-莫斯科时区)", "UTC+4 (META-中东时区A)", "UTC+5 (METB-中东时区B)", "UTC+6 (BHT-孟加拉标准时间)", "UTC+7 (MST-中南半岛标准时间)", "UTC+8 (CST-中国北京时间,EAT-东亚标准时间,HKT-香港时间)", "UTC+9 (FET-远东标准时间,*日本、韩国时间)", "UTC+10(AEST-澳大利亚东部标准时间)", "UTC+11(VTT-瓦努阿图标准时间)"})
        Me.ComboBoxTimeZone1.Location = New System.Drawing.Point(0, 20)
        Me.ComboBoxTimeZone1.Name = "ComboBoxTimeZone1"
        Me.ComboBoxTimeZone1.Size = New System.Drawing.Size(138, 20)
        Me.ComboBoxTimeZone1.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.ComboBoxTimeZone1, "选择时区")
        '
        'CheckBoxDST1
        '
        Me.CheckBoxDST1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxDST1.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxDST1.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CheckBoxDST1.ForeColor = System.Drawing.Color.Silver
        Me.CheckBoxDST1.Location = New System.Drawing.Point(161, 20)
        Me.CheckBoxDST1.Name = "CheckBoxDST1"
        Me.CheckBoxDST1.Size = New System.Drawing.Size(64, 21)
        Me.CheckBoxDST1.TabIndex = 26
        Me.CheckBoxDST1.Text = "当天"
        Me.CheckBoxDST1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CheckBoxDST1, "是否使用夏令时DST")
        Me.CheckBoxDST1.UseVisualStyleBackColor = False
        '
        'ComboBoxHour1
        '
        Me.ComboBoxHour1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxHour1.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.ComboBoxHour1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxHour1.DropDownWidth = 140
        Me.ComboBoxHour1.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ComboBoxHour1.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxHour1.FormattingEnabled = True
        Me.ComboBoxHour1.Items.AddRange(New Object() {"00:00 上午 12:00 am", "01:00 上午 01:00 am", "02:00 上午 02:00 am", "03:00 上午 03:00 am", "04:00 上午 04:00 am", "05:00 上午 05:00 am", "06:00 上午 06:00 am", "07:00 上午 07:00 am", "08:00 上午 08:00 am", "09:00 上午 09:00 am", "10:00 上午 10:00 am", "11:00 上午 11:00 am", "12:00 下午 12:00 pm", "13:00 下午 01:00 pm", "14:00 下午 02:00 pm", "15:00 下午 03:00 pm", "16:00 下午 04:00 pm", "17:00 下午 05:00 pm", "18:00 下午 06:00 pm", "19:00 下午 07:00 pm", "20:00 下午 08:00 pm", "21:00 下午 09:00 pm", "22:00 下午 10:00 pm", "23:00 下午 11:00 pm"})
        Me.ComboBoxHour1.Location = New System.Drawing.Point(222, 20)
        Me.ComboBoxHour1.Name = "ComboBoxHour1"
        Me.ComboBoxHour1.Size = New System.Drawing.Size(54, 20)
        Me.ComboBoxHour1.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.ComboBoxHour1, "选择时间")
        '
        'ComboBoxHour2
        '
        Me.ComboBoxHour2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxHour2.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(19, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ComboBoxHour2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxHour2.DropDownWidth = 140
        Me.ComboBoxHour2.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ComboBoxHour2.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxHour2.FormattingEnabled = True
        Me.ComboBoxHour2.Items.AddRange(New Object() {"00:00 上午 12:00 am", "01:00 上午 01:00 am", "02:00 上午 02:00 am", "03:00 上午 03:00 am", "04:00 上午 04:00 am", "05:00 上午 05:00 am", "06:00 上午 06:00 am", "07:00 上午 07:00 am", "08:00 上午 08:00 am", "09:00 上午 09:00 am", "10:00 上午 10:00 am", "11:00 上午 11:00 am", "12:00 下午 12:00 pm", "13:00 下午 01:00 pm", "14:00 下午 02:00 pm", "15:00 下午 03:00 pm", "16:00 下午 04:00 pm", "17:00 下午 05:00 pm", "18:00 下午 06:00 pm", "19:00 下午 07:00 pm", "20:00 下午 08:00 pm", "21:00 下午 09:00 pm", "22:00 下午 10:00 pm", "23:00 下午 11:00 pm"})
        Me.ComboBoxHour2.Location = New System.Drawing.Point(222, 41)
        Me.ComboBoxHour2.Name = "ComboBoxHour2"
        Me.ComboBoxHour2.Size = New System.Drawing.Size(54, 20)
        Me.ComboBoxHour2.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.ComboBoxHour2, "选择时间")
        '
        'CheckBoxDST2
        '
        Me.CheckBoxDST2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxDST2.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxDST2.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CheckBoxDST2.ForeColor = System.Drawing.Color.Silver
        Me.CheckBoxDST2.Location = New System.Drawing.Point(161, 41)
        Me.CheckBoxDST2.Name = "CheckBoxDST2"
        Me.CheckBoxDST2.Size = New System.Drawing.Size(64, 21)
        Me.CheckBoxDST2.TabIndex = 29
        Me.CheckBoxDST2.Text = "当天"
        Me.CheckBoxDST2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CheckBoxDST2, "是否使用夏令时DST")
        Me.CheckBoxDST2.UseVisualStyleBackColor = False
        '
        'ComboBoxTimeZone2
        '
        Me.ComboBoxTimeZone2.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(19, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ComboBoxTimeZone2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTimeZone2.DropDownWidth = 370
        Me.ComboBoxTimeZone2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ComboBoxTimeZone2.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxTimeZone2.FormattingEnabled = True
        Me.ComboBoxTimeZone2.Items.AddRange(New Object() {"UTC-12(国际日期变更线)", "UTC-11(MIT-中途岛标准时间)", "UTC-10(HST-夏威夷－阿留申标准时间)", "UTC-9 (AKST-阿拉斯加标准时间)", "UTC-8 (PST-北美西部加州圣地亚哥时间,PSTA-太平洋标准时间A)", "UTC-7 (MST-北美山区标准时间)", "UTC-6 (CST-北美中部标准时间)", "UTC-5 (EST-北美东部标准时间)", "UTC-4 (AST-大西洋标准时间)", "UTC-3 (SAT-南美标准时间)", "UTC-2 (BRT-巴西时间)", "UTC-1 (CVT-佛得角标准时间)", "UTC   (GMT-格林威治标准时间,WET-欧洲西部时区,英国时间)", "UTC+1 (CET-欧洲中部时区,法国、德国时间)", "UTC+2 (EET-欧洲东部时区)", "UTC+3 (MSK-莫斯科时区)", "UTC+4 (META-中东时区A)", "UTC+5 (METB-中东时区B)", "UTC+6 (BHT-孟加拉标准时间)", "UTC+7 (MST-中南半岛标准时间)", "UTC+8 (CST-中国北京时间,EAT-东亚标准时间,HKT-香港时间)", "UTC+9 (FET-远东标准时间,*日本、韩国时间)", "UTC+10(AEST-澳大利亚东部标准时间)", "UTC+11(VTT-瓦努阿图标准时间)"})
        Me.ComboBoxTimeZone2.Location = New System.Drawing.Point(0, 41)
        Me.ComboBoxTimeZone2.Name = "ComboBoxTimeZone2"
        Me.ComboBoxTimeZone2.Size = New System.Drawing.Size(138, 20)
        Me.ComboBoxTimeZone2.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.ComboBoxTimeZone2, "选择时区")
        '
        'ComboBoxHour3
        '
        Me.ComboBoxHour3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxHour3.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(19, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ComboBoxHour3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxHour3.DropDownWidth = 140
        Me.ComboBoxHour3.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ComboBoxHour3.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxHour3.FormattingEnabled = True
        Me.ComboBoxHour3.Items.AddRange(New Object() {"00:00 上午 12:00 am", "01:00 上午 01:00 am", "02:00 上午 02:00 am", "03:00 上午 03:00 am", "04:00 上午 04:00 am", "05:00 上午 05:00 am", "06:00 上午 06:00 am", "07:00 上午 07:00 am", "08:00 上午 08:00 am", "09:00 上午 09:00 am", "10:00 上午 10:00 am", "11:00 上午 11:00 am", "12:00 下午 12:00 pm", "13:00 下午 01:00 pm", "14:00 下午 02:00 pm", "15:00 下午 03:00 pm", "16:00 下午 04:00 pm", "17:00 下午 05:00 pm", "18:00 下午 06:00 pm", "19:00 下午 07:00 pm", "20:00 下午 08:00 pm", "21:00 下午 09:00 pm", "22:00 下午 10:00 pm", "23:00 下午 11:00 pm"})
        Me.ComboBoxHour3.Location = New System.Drawing.Point(222, 62)
        Me.ComboBoxHour3.Name = "ComboBoxHour3"
        Me.ComboBoxHour3.Size = New System.Drawing.Size(54, 20)
        Me.ComboBoxHour3.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.ComboBoxHour3, "选择时间")
        '
        'CheckBoxDST3
        '
        Me.CheckBoxDST3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxDST3.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxDST3.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.CheckBoxDST3.ForeColor = System.Drawing.Color.Silver
        Me.CheckBoxDST3.Location = New System.Drawing.Point(161, 62)
        Me.CheckBoxDST3.Name = "CheckBoxDST3"
        Me.CheckBoxDST3.Size = New System.Drawing.Size(64, 21)
        Me.CheckBoxDST3.TabIndex = 32
        Me.CheckBoxDST3.Text = "当天"
        Me.CheckBoxDST3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CheckBoxDST3, "是否使用夏令时DST")
        Me.CheckBoxDST3.UseVisualStyleBackColor = False
        '
        'ComboBoxTimeZone3
        '
        Me.ComboBoxTimeZone3.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(19, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ComboBoxTimeZone3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTimeZone3.DropDownWidth = 370
        Me.ComboBoxTimeZone3.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ComboBoxTimeZone3.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxTimeZone3.FormattingEnabled = True
        Me.ComboBoxTimeZone3.Items.AddRange(New Object() {"UTC-12(国际日期变更线)", "UTC-11(MIT-中途岛标准时间)", "UTC-10(HST-夏威夷－阿留申标准时间)", "UTC-9 (AKST-阿拉斯加标准时间)", "UTC-8 (PST-北美西部加州圣地亚哥时间,PSTA-太平洋标准时间A)", "UTC-7 (MST-北美山区标准时间)", "UTC-6 (CST-北美中部标准时间)", "UTC-5 (EST-北美东部标准时间)", "UTC-4 (AST-大西洋标准时间)", "UTC-3 (SAT-南美标准时间)", "UTC-2 (BRT-巴西时间)", "UTC-1 (CVT-佛得角标准时间)", "UTC   (GMT-格林威治标准时间,WET-欧洲西部时区,英国时间)", "UTC+1 (CET-欧洲中部时区,法国、德国时间)", "UTC+2 (EET-欧洲东部时区)", "UTC+3 (MSK-莫斯科时区)", "UTC+4 (META-中东时区A)", "UTC+5 (METB-中东时区B)", "UTC+6 (BHT-孟加拉标准时间)", "UTC+7 (MST-中南半岛标准时间)", "UTC+8 (CST-中国北京时间,EAT-东亚标准时间,HKT-香港时间)", "UTC+9 (FET-远东标准时间,*日本、韩国时间)", "UTC+10(AEST-澳大利亚东部标准时间)", "UTC+11(VTT-瓦努阿图标准时间)"})
        Me.ComboBoxTimeZone3.Location = New System.Drawing.Point(0, 62)
        Me.ComboBoxTimeZone3.Name = "ComboBoxTimeZone3"
        Me.ComboBoxTimeZone3.Size = New System.Drawing.Size(138, 20)
        Me.ComboBoxTimeZone3.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.ComboBoxTimeZone3, "选择时区")
        '
        'ComboBoxFontSelector
        '
        Me.ComboBoxFontSelector.BackColor = System.Drawing.Color.Black
        Me.ComboBoxFontSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxFontSelector.DropDownWidth = 238
        Me.ComboBoxFontSelector.Font = New System.Drawing.Font("宋体", 9.0!)
        Me.ComboBoxFontSelector.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxFontSelector.Location = New System.Drawing.Point(0, 0)
        Me.ComboBoxFontSelector.Name = "ComboBoxFontSelector"
        Me.ComboBoxFontSelector.Size = New System.Drawing.Size(238, 20)
        Me.ComboBoxFontSelector.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.ComboBoxFontSelector, "请将喜欢的ttf格式字体，放入汉化启动器的""自定义字体""目录下。然后此处选择游戏要使用的字体。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "选用中文字体后，在【游戏聊天】中可【显示中文】。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "选择字体后不" &
        "会立即生效，直到点击【开始修改】后才会生效。")
        '
        'CheckBoxAutoRestore
        '
        Me.CheckBoxAutoRestore.AutoSize = True
        Me.CheckBoxAutoRestore.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxAutoRestore.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxAutoRestore.Enabled = False
        Me.CheckBoxAutoRestore.ForeColor = System.Drawing.Color.Gold
        Me.CheckBoxAutoRestore.Location = New System.Drawing.Point(124, 313)
        Me.CheckBoxAutoRestore.Name = "CheckBoxAutoRestore"
        Me.CheckBoxAutoRestore.Size = New System.Drawing.Size(99, 21)
        Me.CheckBoxAutoRestore.TabIndex = 8
        Me.CheckBoxAutoRestore.Text = "自动恢复备份"
        Me.ToolTip1.SetToolTip(Me.CheckBoxAutoRestore, "勾选后，汉化启动器将在游戏结束后，自动恢复备份文件。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "省去一次手动点击按钮。")
        Me.CheckBoxAutoRestore.UseVisualStyleBackColor = False
        Me.CheckBoxAutoRestore.Visible = False
        '
        'RadioButtonServerStatus
        '
        Me.RadioButtonServerStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonServerStatus.BackColor = System.Drawing.Color.Transparent
        Me.RadioButtonServerStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RadioButtonServerStatus.Enabled = False
        Me.RadioButtonServerStatus.Location = New System.Drawing.Point(0, 60)
        Me.RadioButtonServerStatus.Name = "RadioButtonServerStatus"
        Me.RadioButtonServerStatus.Size = New System.Drawing.Size(94, 23)
        Me.RadioButtonServerStatus.TabIndex = 18
        Me.RadioButtonServerStatus.TabStop = True
        Me.RadioButtonServerStatus.Text = "服务器状态"
        Me.ToolTip1.SetToolTip(Me.RadioButtonServerStatus, "点选后，在【上方中部】显示【查询服务器状态】工具")
        Me.RadioButtonServerStatus.UseVisualStyleBackColor = False
        Me.RadioButtonServerStatus.Visible = False
        '
        'RadioButtonTimeZoneConverter
        '
        Me.RadioButtonTimeZoneConverter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonTimeZoneConverter.BackColor = System.Drawing.Color.Transparent
        Me.RadioButtonTimeZoneConverter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RadioButtonTimeZoneConverter.Location = New System.Drawing.Point(0, 20)
        Me.RadioButtonTimeZoneConverter.Name = "RadioButtonTimeZoneConverter"
        Me.RadioButtonTimeZoneConverter.Size = New System.Drawing.Size(94, 23)
        Me.RadioButtonTimeZoneConverter.TabIndex = 16
        Me.RadioButtonTimeZoneConverter.TabStop = True
        Me.RadioButtonTimeZoneConverter.Text = "时区换算器"
        Me.ToolTip1.SetToolTip(Me.RadioButtonTimeZoneConverter, "点选后，在【上方中部】显示【时区换算器】工具")
        Me.RadioButtonTimeZoneConverter.UseVisualStyleBackColor = False
        '
        'RadioButtonCheckIPLoc
        '
        Me.RadioButtonCheckIPLoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonCheckIPLoc.BackColor = System.Drawing.Color.Transparent
        Me.RadioButtonCheckIPLoc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RadioButtonCheckIPLoc.Location = New System.Drawing.Point(0, 0)
        Me.RadioButtonCheckIPLoc.Name = "RadioButtonCheckIPLoc"
        Me.RadioButtonCheckIPLoc.Size = New System.Drawing.Size(94, 23)
        Me.RadioButtonCheckIPLoc.TabIndex = 15
        Me.RadioButtonCheckIPLoc.TabStop = True
        Me.RadioButtonCheckIPLoc.Text = "检测IP地区"
        Me.ToolTip1.SetToolTip(Me.RadioButtonCheckIPLoc, "点选后，在【上方中部】显示【检测IP所属地区】工具")
        Me.RadioButtonCheckIPLoc.UseVisualStyleBackColor = False
        '
        'ButtonFixLauncher503
        '
        Me.ButtonFixLauncher503.BackColor = System.Drawing.Color.Transparent
        Me.ButtonFixLauncher503.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonFixLauncher503.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonFixLauncher503.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonFixLauncher503.ForeColor = System.Drawing.Color.Gold
        Me.ButtonFixLauncher503.Image = Global.PS2CN_Launcher.My.Resources.Resources.VB_bg_FixToolIconText
        Me.ButtonFixLauncher503.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonFixLauncher503.Location = New System.Drawing.Point(3, 55)
        Me.ButtonFixLauncher503.Name = "ButtonFixLauncher503"
        Me.ButtonFixLauncher503.Size = New System.Drawing.Size(161, 22)
        Me.ButtonFixLauncher503.TabIndex = 14
        Me.ButtonFixLauncher503.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.ButtonFixLauncher503, "请先确认下方需要修复的【客户端类型】及【exe程序位置】。")
        Me.ButtonFixLauncher503.UseVisualStyleBackColor = False
        '
        'ComboBoxClientType
        '
        Me.ComboBoxClientType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxClientType.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ComboBoxClientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxClientType.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ComboBoxClientType.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxClientType.Items.AddRange(New Object() {"Steam版", "官方版", "测试服"})
        Me.ComboBoxClientType.Location = New System.Drawing.Point(2, 30)
        Me.ComboBoxClientType.Name = "ComboBoxClientType"
        Me.ComboBoxClientType.Size = New System.Drawing.Size(71, 20)
        Me.ComboBoxClientType.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.ComboBoxClientType, "选择游戏客户端类型")
        '
        'LabelComboBoxFontSelector
        '
        Me.LabelComboBoxFontSelector.AutoEllipsis = True
        Me.LabelComboBoxFontSelector.BackColor = System.Drawing.Color.Transparent
        Me.LabelComboBoxFontSelector.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelComboBoxFontSelector.ForeColor = System.Drawing.Color.Gold
        Me.LabelComboBoxFontSelector.Location = New System.Drawing.Point(1, 1)
        Me.LabelComboBoxFontSelector.Name = "LabelComboBoxFontSelector"
        Me.LabelComboBoxFontSelector.Size = New System.Drawing.Size(218, 16)
        Me.LabelComboBoxFontSelector.TabIndex = 6
        Me.LabelComboBoxFontSelector.Text = "默认字体"
        Me.ToolTip1.SetToolTip(Me.LabelComboBoxFontSelector, "请将喜欢的ttf格式字体，放入汉化启动器的""自定义字体""目录下。然后此处选择游戏要使用的字体。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "选用中文字体后，在【游戏聊天】中可【显示中文】。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "选择字体后不" &
        "会立即生效，直到点击【开始修改】后才会生效。")
        '
        'CheckBoxShowFunctionUI
        '
        Me.CheckBoxShowFunctionUI.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxShowFunctionUI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxShowFunctionUI.Location = New System.Drawing.Point(5, 30)
        Me.CheckBoxShowFunctionUI.Name = "CheckBoxShowFunctionUI"
        Me.CheckBoxShowFunctionUI.Size = New System.Drawing.Size(94, 23)
        Me.CheckBoxShowFunctionUI.TabIndex = 14
        Me.CheckBoxShowFunctionUI.Text = "显示工具区"
        Me.ToolTip1.SetToolTip(Me.CheckBoxShowFunctionUI, "显示/隐藏【上方中部】的【附属工具区】")
        Me.CheckBoxShowFunctionUI.UseVisualStyleBackColor = True
        '
        'LabelPatchNote
        '
        Me.LabelPatchNote.BackColor = System.Drawing.Color.Transparent
        Me.LabelPatchNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelPatchNote.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelPatchNote.Enabled = False
        Me.LabelPatchNote.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelPatchNote.ForeColor = System.Drawing.Color.Gold
        Me.LabelPatchNote.Location = New System.Drawing.Point(212, 3)
        Me.LabelPatchNote.Name = "LabelPatchNote"
        Me.LabelPatchNote.Size = New System.Drawing.Size(63, 21)
        Me.LabelPatchNote.TabIndex = 50
        Me.LabelPatchNote.Text = "补丁说明"
        Me.LabelPatchNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.LabelPatchNote, "访问PS2CN.com更新说明区")
        Me.LabelPatchNote.Visible = False
        '
        'LabelDownloadGameUpdate
        '
        Me.LabelDownloadGameUpdate.BackColor = System.Drawing.Color.Transparent
        Me.LabelDownloadGameUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelDownloadGameUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelDownloadGameUpdate.Enabled = False
        Me.LabelDownloadGameUpdate.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelDownloadGameUpdate.ForeColor = System.Drawing.Color.Gold
        Me.LabelDownloadGameUpdate.Location = New System.Drawing.Point(386, 311)
        Me.LabelDownloadGameUpdate.Name = "LabelDownloadGameUpdate"
        Me.LabelDownloadGameUpdate.Size = New System.Drawing.Size(63, 21)
        Me.LabelDownloadGameUpdate.TabIndex = 51
        Me.LabelDownloadGameUpdate.Text = "下载更新"
        Me.LabelDownloadGameUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.LabelDownloadGameUpdate, "访问PS2CN.com游戏下载更新页")
        Me.LabelDownloadGameUpdate.Visible = False
        '
        'LabelCheckIPLocTitle
        '
        Me.LabelCheckIPLocTitle.BackColor = System.Drawing.Color.Transparent
        Me.LabelCheckIPLocTitle.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelCheckIPLocTitle.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.LabelCheckIPLocTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelCheckIPLocTitle.Location = New System.Drawing.Point(0, 0)
        Me.LabelCheckIPLocTitle.Name = "LabelCheckIPLocTitle"
        Me.LabelCheckIPLocTitle.Size = New System.Drawing.Size(278, 19)
        Me.LabelCheckIPLocTitle.TabIndex = 44
        Me.LabelCheckIPLocTitle.Text = "IP归属地（只检测全局代理）"
        Me.ToolTip1.SetToolTip(Me.LabelCheckIPLocTitle, "无法检测智能代理/仅加速游戏程序的代理")
        '
        'RadioButtonMiscFunctions
        '
        Me.RadioButtonMiscFunctions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonMiscFunctions.BackColor = System.Drawing.Color.Transparent
        Me.RadioButtonMiscFunctions.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RadioButtonMiscFunctions.Location = New System.Drawing.Point(0, 40)
        Me.RadioButtonMiscFunctions.Name = "RadioButtonMiscFunctions"
        Me.RadioButtonMiscFunctions.Size = New System.Drawing.Size(94, 23)
        Me.RadioButtonMiscFunctions.TabIndex = 17
        Me.RadioButtonMiscFunctions.TabStop = True
        Me.RadioButtonMiscFunctions.Text = "实用功能"
        Me.ToolTip1.SetToolTip(Me.RadioButtonMiscFunctions, "点选后，在【上方中部】显示【实用功能】工具")
        Me.RadioButtonMiscFunctions.UseVisualStyleBackColor = False
        '
        'ComboBoxFunctionSelector
        '
        Me.ComboBoxFunctionSelector.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFunctionSelector.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.ComboBoxFunctionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxFunctionSelector.DropDownWidth = 96
        Me.ComboBoxFunctionSelector.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ComboBoxFunctionSelector.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxFunctionSelector.Items.AddRange(New Object() {"(隐藏工具区)", "实用功能", "检测IP地区", "时区换算器"})
        Me.ComboBoxFunctionSelector.Location = New System.Drawing.Point(304, 8)
        Me.ComboBoxFunctionSelector.MaxDropDownItems = 15
        Me.ComboBoxFunctionSelector.Name = "ComboBoxFunctionSelector"
        Me.ComboBoxFunctionSelector.Size = New System.Drawing.Size(96, 20)
        Me.ComboBoxFunctionSelector.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.ComboBoxFunctionSelector, "选择功能模块")
        '
        'ComboBoxCN
        '
        Me.ComboBoxCN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCN.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ComboBoxCN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCN.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ComboBoxCN.ForeColor = System.Drawing.Color.Gold
        Me.ComboBoxCN.Items.AddRange(New Object() {"英文原版", "进行汉化"})
        Me.ComboBoxCN.Location = New System.Drawing.Point(103, 30)
        Me.ComboBoxCN.Name = "ComboBoxCN"
        Me.ComboBoxCN.Size = New System.Drawing.Size(79, 20)
        Me.ComboBoxCN.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.ComboBoxCN, "选择是否汉化")
        '
        'LabelPlayerStatistics
        '
        Me.LabelPlayerStatistics.BackColor = System.Drawing.Color.Transparent
        Me.LabelPlayerStatistics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelPlayerStatistics.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelPlayerStatistics.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelPlayerStatistics.ForeColor = System.Drawing.Color.Gold
        Me.LabelPlayerStatistics.Location = New System.Drawing.Point(146, 82)
        Me.LabelPlayerStatistics.Name = "LabelPlayerStatistics"
        Me.LabelPlayerStatistics.Size = New System.Drawing.Size(63, 21)
        Me.LabelPlayerStatistics.TabIndex = 52
        Me.LabelPlayerStatistics.Text = "查询数据"
        Me.LabelPlayerStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.LabelPlayerStatistics, "查询玩家总体数据，如等级、KD、成就等")
        '
        'LabelPlayerKillboard
        '
        Me.LabelPlayerKillboard.BackColor = System.Drawing.Color.Transparent
        Me.LabelPlayerKillboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelPlayerKillboard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelPlayerKillboard.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelPlayerKillboard.ForeColor = System.Drawing.Color.Gold
        Me.LabelPlayerKillboard.Location = New System.Drawing.Point(212, 82)
        Me.LabelPlayerKillboard.Name = "LabelPlayerKillboard"
        Me.LabelPlayerKillboard.Size = New System.Drawing.Size(63, 21)
        Me.LabelPlayerKillboard.TabIndex = 54
        Me.LabelPlayerKillboard.Text = "战斗记录"
        Me.LabelPlayerKillboard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.LabelPlayerKillboard, "查询玩家最近战斗记录，如每一条击杀和被杀详情。")
        '
        'LabelGErrorCodeQuery
        '
        Me.LabelGErrorCodeQuery.BackColor = System.Drawing.Color.Transparent
        Me.LabelGErrorCodeQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelGErrorCodeQuery.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelGErrorCodeQuery.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelGErrorCodeQuery.ForeColor = System.Drawing.Color.Gold
        Me.LabelGErrorCodeQuery.Location = New System.Drawing.Point(176, 56)
        Me.LabelGErrorCodeQuery.Name = "LabelGErrorCodeQuery"
        Me.LabelGErrorCodeQuery.Size = New System.Drawing.Size(99, 21)
        Me.LabelGErrorCodeQuery.TabIndex = 56
        Me.LabelGErrorCodeQuery.Text = "G错误代码含义"
        Me.LabelGErrorCodeQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.LabelGErrorCodeQuery, "查询登录器G错误代码含义，PS2与H1Z1通用。")
        '
        'LabelVisitOfficialSite
        '
        Me.LabelVisitOfficialSite.BackColor = System.Drawing.Color.Transparent
        Me.LabelVisitOfficialSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelVisitOfficialSite.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelVisitOfficialSite.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelVisitOfficialSite.ForeColor = System.Drawing.Color.Gold
        Me.LabelVisitOfficialSite.Location = New System.Drawing.Point(3, 28)
        Me.LabelVisitOfficialSite.Name = "LabelVisitOfficialSite"
        Me.LabelVisitOfficialSite.Size = New System.Drawing.Size(109, 21)
        Me.LabelVisitOfficialSite.TabIndex = 57
        Me.LabelVisitOfficialSite.Text = "PlanetSide2官网"
        Me.LabelVisitOfficialSite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.LabelVisitOfficialSite, "访问PlanetSide2.com英文官网")
        '
        'LabelVisitReddit
        '
        Me.LabelVisitReddit.BackColor = System.Drawing.Color.Transparent
        Me.LabelVisitReddit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LabelVisitReddit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelVisitReddit.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelVisitReddit.ForeColor = System.Drawing.Color.Gold
        Me.LabelVisitReddit.Location = New System.Drawing.Point(115, 28)
        Me.LabelVisitReddit.Name = "LabelVisitReddit"
        Me.LabelVisitReddit.Size = New System.Drawing.Size(102, 21)
        Me.LabelVisitReddit.TabIndex = 58
        Me.LabelVisitReddit.Text = "Reddit英文论坛"
        Me.LabelVisitReddit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.LabelVisitReddit, "访问Reddit.com/PlanetSide2英语论坛。DB开发者常驻，完全替代官网论坛……")
        '
        'LabelManuallyTranslate
        '
        Me.LabelManuallyTranslate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelManuallyTranslate.AutoSize = True
        Me.LabelManuallyTranslate.BackColor = System.Drawing.Color.Transparent
        Me.LabelManuallyTranslate.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelManuallyTranslate.Location = New System.Drawing.Point(300, 384)
        Me.LabelManuallyTranslate.Name = "LabelManuallyTranslate"
        Me.LabelManuallyTranslate.Size = New System.Drawing.Size(152, 17)
        Me.LabelManuallyTranslate.TabIndex = 54
        Me.LabelManuallyTranslate.Text = "读不到登录器？请手动汉化"
        Me.ToolTip1.SetToolTip(Me.LabelManuallyTranslate, "若汉化器无法读取到游戏登录器LaunchPad进程，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "请点击此按钮手动汉化。")
        Me.LabelManuallyTranslate.Visible = False
        '
        'LabelKillLaunchPad
        '
        Me.LabelKillLaunchPad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelKillLaunchPad.AutoSize = True
        Me.LabelKillLaunchPad.BackColor = System.Drawing.Color.Transparent
        Me.LabelKillLaunchPad.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelKillLaunchPad.Location = New System.Drawing.Point(288, 384)
        Me.LabelKillLaunchPad.Name = "LabelKillLaunchPad"
        Me.LabelKillLaunchPad.Size = New System.Drawing.Size(164, 17)
        Me.LabelKillLaunchPad.TabIndex = 55
        Me.LabelKillLaunchPad.Text = "登录器卡死？点击关闭登录器"
        Me.ToolTip1.SetToolTip(Me.LabelKillLaunchPad, "若游戏登录器LaunchPad进程卡死无法显示，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "请点此按钮强制结束登录器进程。")
        Me.LabelKillLaunchPad.Visible = False
        '
        'LabelKillGameClient
        '
        Me.LabelKillGameClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelKillGameClient.AutoSize = True
        Me.LabelKillGameClient.BackColor = System.Drawing.Color.Transparent
        Me.LabelKillGameClient.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelKillGameClient.Location = New System.Drawing.Point(285, 384)
        Me.LabelKillGameClient.Name = "LabelKillGameClient"
        Me.LabelKillGameClient.Size = New System.Drawing.Size(164, 17)
        Me.LabelKillGameClient.TabIndex = 56
        Me.LabelKillGameClient.Text = "游戏卡死？点击结束游戏进程"
        Me.ToolTip1.SetToolTip(Me.LabelKillGameClient, "若游戏PlanetSide2_x64进程卡死无法显示，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "请点此按钮强制结束游戏进程。")
        Me.LabelKillGameClient.Visible = False
        '
        'LabelFontSelector
        '
        Me.LabelFontSelector.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelFontSelector.AutoSize = True
        Me.LabelFontSelector.BackColor = System.Drawing.Color.Transparent
        Me.LabelFontSelector.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelFontSelector.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.LabelFontSelector.Location = New System.Drawing.Point(203, 6)
        Me.LabelFontSelector.Name = "LabelFontSelector"
        Me.LabelFontSelector.Size = New System.Drawing.Size(65, 20)
        Me.LabelFontSelector.TabIndex = 10
        Me.LabelFontSelector.Text = "游戏字体"
        '
        'LabelTimeZoneTitle
        '
        Me.LabelTimeZoneTitle.AutoSize = True
        Me.LabelTimeZoneTitle.BackColor = System.Drawing.Color.Transparent
        Me.LabelTimeZoneTitle.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelTimeZoneTitle.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.LabelTimeZoneTitle.Location = New System.Drawing.Point(37, 0)
        Me.LabelTimeZoneTitle.Name = "LabelTimeZoneTitle"
        Me.LabelTimeZoneTitle.Size = New System.Drawing.Size(65, 19)
        Me.LabelTimeZoneTitle.TabIndex = 34
        Me.LabelTimeZoneTitle.Text = "选择时区"
        '
        'PanelTimeZoneConverter
        '
        Me.PanelTimeZoneConverter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelTimeZoneConverter.BackColor = System.Drawing.Color.Transparent
        Me.PanelTimeZoneConverter.Controls.Add(Me.LabelHourTitle)
        Me.PanelTimeZoneConverter.Controls.Add(Me.LabelDSTTitle)
        Me.PanelTimeZoneConverter.Controls.Add(Me.ComboBoxHour1)
        Me.PanelTimeZoneConverter.Controls.Add(Me.ComboBoxTimeZone1)
        Me.PanelTimeZoneConverter.Controls.Add(Me.ComboBoxHour3)
        Me.PanelTimeZoneConverter.Controls.Add(Me.CheckBoxDST1)
        Me.PanelTimeZoneConverter.Controls.Add(Me.CheckBoxDST3)
        Me.PanelTimeZoneConverter.Controls.Add(Me.ComboBoxTimeZone3)
        Me.PanelTimeZoneConverter.Controls.Add(Me.ComboBoxTimeZone2)
        Me.PanelTimeZoneConverter.Controls.Add(Me.ComboBoxHour2)
        Me.PanelTimeZoneConverter.Controls.Add(Me.CheckBoxDST2)
        Me.PanelTimeZoneConverter.Controls.Add(Me.LabelTimeZoneTitle)
        Me.PanelTimeZoneConverter.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Bold)
        Me.PanelTimeZoneConverter.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.PanelTimeZoneConverter.Location = New System.Drawing.Point(301, 30)
        Me.PanelTimeZoneConverter.Name = "PanelTimeZoneConverter"
        Me.PanelTimeZoneConverter.Size = New System.Drawing.Size(278, 88)
        Me.PanelTimeZoneConverter.TabIndex = 24
        Me.PanelTimeZoneConverter.Visible = False
        '
        'LabelHourTitle
        '
        Me.LabelHourTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelHourTitle.AutoSize = True
        Me.LabelHourTitle.BackColor = System.Drawing.Color.Transparent
        Me.LabelHourTitle.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelHourTitle.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.LabelHourTitle.Location = New System.Drawing.Point(215, 0)
        Me.LabelHourTitle.Name = "LabelHourTitle"
        Me.LabelHourTitle.Size = New System.Drawing.Size(65, 19)
        Me.LabelHourTitle.TabIndex = 37
        Me.LabelHourTitle.Text = "选择时间"
        '
        'LabelDSTTitle
        '
        Me.LabelDSTTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDSTTitle.AutoSize = True
        Me.LabelDSTTitle.BackColor = System.Drawing.Color.Transparent
        Me.LabelDSTTitle.Font = New System.Drawing.Font("微软雅黑", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelDSTTitle.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.LabelDSTTitle.Location = New System.Drawing.Point(142, 0)
        Me.LabelDSTTitle.Name = "LabelDSTTitle"
        Me.LabelDSTTitle.Size = New System.Drawing.Size(51, 19)
        Me.LabelDSTTitle.TabIndex = 36
        Me.LabelDSTTitle.Text = "夏令时"
        '
        'PanelFunctionSelector
        '
        Me.PanelFunctionSelector.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelFunctionSelector.BackColor = System.Drawing.Color.Transparent
        Me.PanelFunctionSelector.Controls.Add(Me.CheckBoxShowFunctionUI)
        Me.PanelFunctionSelector.Controls.Add(Me.PanelFunctionRadioButtonList)
        Me.PanelFunctionSelector.Enabled = False
        Me.PanelFunctionSelector.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.PanelFunctionSelector.ForeColor = System.Drawing.Color.Gold
        Me.PanelFunctionSelector.Location = New System.Drawing.Point(612, 87)
        Me.PanelFunctionSelector.Name = "PanelFunctionSelector"
        Me.PanelFunctionSelector.Size = New System.Drawing.Size(104, 138)
        Me.PanelFunctionSelector.TabIndex = 11
        Me.PanelFunctionSelector.Visible = False
        '
        'PanelFunctionRadioButtonList
        '
        Me.PanelFunctionRadioButtonList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelFunctionRadioButtonList.Controls.Add(Me.RadioButtonMiscFunctions)
        Me.PanelFunctionRadioButtonList.Controls.Add(Me.RadioButtonTimeZoneConverter)
        Me.PanelFunctionRadioButtonList.Controls.Add(Me.RadioButtonServerStatus)
        Me.PanelFunctionRadioButtonList.Controls.Add(Me.RadioButtonCheckIPLoc)
        Me.PanelFunctionRadioButtonList.Location = New System.Drawing.Point(4, 54)
        Me.PanelFunctionRadioButtonList.Name = "PanelFunctionRadioButtonList"
        Me.PanelFunctionRadioButtonList.Size = New System.Drawing.Size(94, 86)
        Me.PanelFunctionRadioButtonList.TabIndex = 17
        '
        'PanelServerStatus
        '
        Me.PanelServerStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelServerStatus.BackColor = System.Drawing.Color.Transparent
        Me.PanelServerStatus.Controls.Add(Me.LabelServer8)
        Me.PanelServerStatus.Controls.Add(Me.ButtonRequestServerStatus)
        Me.PanelServerStatus.Controls.Add(Me.LabelServer1)
        Me.PanelServerStatus.Controls.Add(Me.LabelServer2)
        Me.PanelServerStatus.Controls.Add(Me.LabelServer3)
        Me.PanelServerStatus.Controls.Add(Me.LabelServer4)
        Me.PanelServerStatus.Controls.Add(Me.LabelServer5)
        Me.PanelServerStatus.Controls.Add(Me.LabelServer6)
        Me.PanelServerStatus.Controls.Add(Me.LabelServer7)
        Me.PanelServerStatus.ForeColor = System.Drawing.Color.Lime
        Me.PanelServerStatus.Location = New System.Drawing.Point(288, 7)
        Me.PanelServerStatus.Name = "PanelServerStatus"
        Me.PanelServerStatus.Size = New System.Drawing.Size(300, 118)
        Me.PanelServerStatus.TabIndex = 14
        Me.PanelServerStatus.Visible = False
        '
        'LabelServer8
        '
        Me.LabelServer8.Location = New System.Drawing.Point(150, 95)
        Me.LabelServer8.Name = "LabelServer8"
        Me.LabelServer8.Size = New System.Drawing.Size(150, 19)
        Me.LabelServer8.TabIndex = 23
        Me.LabelServer8.Text = "LabelServer8"
        Me.LabelServer8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ButtonRequestServerStatus
        '
        Me.ButtonRequestServerStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRequestServerStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ButtonRequestServerStatus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonRequestServerStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonRequestServerStatus.ForeColor = System.Drawing.Color.Yellow
        Me.ButtonRequestServerStatus.Image = Global.PS2CN_Launcher.My.Resources.Resources.Refresh_icon
        Me.ButtonRequestServerStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonRequestServerStatus.Location = New System.Drawing.Point(173, 3)
        Me.ButtonRequestServerStatus.Name = "ButtonRequestServerStatus"
        Me.ButtonRequestServerStatus.Size = New System.Drawing.Size(127, 30)
        Me.ButtonRequestServerStatus.TabIndex = 15
        Me.ButtonRequestServerStatus.Text = "刷新美服状态"
        Me.ButtonRequestServerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonRequestServerStatus.UseVisualStyleBackColor = False
        '
        'LabelServer1
        '
        Me.LabelServer1.Location = New System.Drawing.Point(0, 38)
        Me.LabelServer1.Name = "LabelServer1"
        Me.LabelServer1.Size = New System.Drawing.Size(150, 19)
        Me.LabelServer1.TabIndex = 16
        Me.LabelServer1.Text = "LabelServer1"
        Me.LabelServer1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelServer2
        '
        Me.LabelServer2.Location = New System.Drawing.Point(0, 57)
        Me.LabelServer2.Name = "LabelServer2"
        Me.LabelServer2.Size = New System.Drawing.Size(150, 19)
        Me.LabelServer2.TabIndex = 17
        Me.LabelServer2.Text = "LabelServer2"
        Me.LabelServer2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelServer3
        '
        Me.LabelServer3.Location = New System.Drawing.Point(0, 76)
        Me.LabelServer3.Name = "LabelServer3"
        Me.LabelServer3.Size = New System.Drawing.Size(150, 19)
        Me.LabelServer3.TabIndex = 18
        Me.LabelServer3.Text = "LabelServer3"
        Me.LabelServer3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelServer4
        '
        Me.LabelServer4.Location = New System.Drawing.Point(0, 95)
        Me.LabelServer4.Name = "LabelServer4"
        Me.LabelServer4.Size = New System.Drawing.Size(150, 19)
        Me.LabelServer4.TabIndex = 19
        Me.LabelServer4.Text = "LabelServer4"
        Me.LabelServer4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelServer5
        '
        Me.LabelServer5.Location = New System.Drawing.Point(150, 38)
        Me.LabelServer5.Name = "LabelServer5"
        Me.LabelServer5.Size = New System.Drawing.Size(150, 19)
        Me.LabelServer5.TabIndex = 20
        Me.LabelServer5.Text = "LabelServer5"
        Me.LabelServer5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelServer6
        '
        Me.LabelServer6.Location = New System.Drawing.Point(150, 57)
        Me.LabelServer6.Name = "LabelServer6"
        Me.LabelServer6.Size = New System.Drawing.Size(150, 19)
        Me.LabelServer6.TabIndex = 21
        Me.LabelServer6.Text = "LabelServer6"
        Me.LabelServer6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelServer7
        '
        Me.LabelServer7.Location = New System.Drawing.Point(150, 76)
        Me.LabelServer7.Name = "LabelServer7"
        Me.LabelServer7.Size = New System.Drawing.Size(150, 19)
        Me.LabelServer7.TabIndex = 22
        Me.LabelServer7.Text = "LabelServer7"
        Me.LabelServer7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PanelComboBoxFontSelector
        '
        Me.PanelComboBoxFontSelector.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PanelComboBoxFontSelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelComboBoxFontSelector.Controls.Add(Me.LabelComboBoxFontSelector)
        Me.PanelComboBoxFontSelector.Controls.Add(Me.ComboBoxFontSelector)
        Me.PanelComboBoxFontSelector.Location = New System.Drawing.Point(207, 30)
        Me.PanelComboBoxFontSelector.Name = "PanelComboBoxFontSelector"
        Me.PanelComboBoxFontSelector.Size = New System.Drawing.Size(238, 20)
        Me.PanelComboBoxFontSelector.TabIndex = 12
        '
        'ButtonRun
        '
        Me.ButtonRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRun.BackColor = System.Drawing.Color.Transparent
        Me.ButtonRun.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonRun.FlatAppearance.BorderSize = 0
        Me.ButtonRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.ButtonRun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.ButtonRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonRun.Location = New System.Drawing.Point(477, 405)
        Me.ButtonRun.Name = "ButtonRun"
        Me.ButtonRun.Size = New System.Drawing.Size(217, 47)
        Me.ButtonRun.TabIndex = 0
        Me.ButtonRun.Text = "隐藏按钮，用于获取焦点进行键盘操作，及隐藏Tab选择框。如用Button有选择框，切窗口失焦后会出现边框"
        Me.ButtonRun.UseVisualStyleBackColor = False
        '
        'LabelRun
        '
        Me.LabelRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelRun.BackColor = System.Drawing.Color.Transparent
        Me.LabelRun.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelRun.Font = New System.Drawing.Font("微软雅黑", 16.0!, System.Drawing.FontStyle.Bold)
        Me.LabelRun.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.LabelRun.Image = Global.PS2CN_Launcher.My.Resources.Resources.开始正常1
        Me.LabelRun.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.LabelRun.Location = New System.Drawing.Point(468, 399)
        Me.LabelRun.Name = "LabelRun"
        Me.LabelRun.Size = New System.Drawing.Size(235, 58)
        Me.LabelRun.TabIndex = 1
        Me.LabelRun.Text = "恢复备份"
        Me.LabelRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelSelectClientType
        '
        Me.LabelSelectClientType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSelectClientType.AutoSize = True
        Me.LabelSelectClientType.BackColor = System.Drawing.Color.Transparent
        Me.LabelSelectClientType.Font = New System.Drawing.Font("微软雅黑", 10.5!)
        Me.LabelSelectClientType.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.LabelSelectClientType.Location = New System.Drawing.Point(-2, 6)
        Me.LabelSelectClientType.MaximumSize = New System.Drawing.Size(287, 19)
        Me.LabelSelectClientType.Name = "LabelSelectClientType"
        Me.LabelSelectClientType.Size = New System.Drawing.Size(79, 19)
        Me.LabelSelectClientType.TabIndex = 45
        Me.LabelSelectClientType.Text = "客户端类型"
        '
        'LabelSplitLine
        '
        Me.LabelSplitLine.BackColor = System.Drawing.Color.Transparent
        Me.LabelSplitLine.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.LabelSplitLine.Image = Global.PS2CN_Launcher.My.Resources.Resources.VB_bg_SplitLine
        Me.LabelSplitLine.Location = New System.Drawing.Point(-5, 0)
        Me.LabelSplitLine.Name = "LabelSplitLine"
        Me.LabelSplitLine.Size = New System.Drawing.Size(450, 1)
        Me.LabelSplitLine.TabIndex = 46
        '
        'PanelCheckIPLoc
        '
        Me.PanelCheckIPLoc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelCheckIPLoc.BackColor = System.Drawing.Color.Transparent
        Me.PanelCheckIPLoc.Controls.Add(Me.LabelCheckIPLocDesc)
        Me.PanelCheckIPLoc.Controls.Add(Me.LabelCheckIPLocTitle)
        Me.PanelCheckIPLoc.Controls.Add(Me.LabelIPLoc)
        Me.PanelCheckIPLoc.Controls.Add(Me.LabelLocStatus)
        Me.PanelCheckIPLoc.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.PanelCheckIPLoc.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.PanelCheckIPLoc.Location = New System.Drawing.Point(300, 30)
        Me.PanelCheckIPLoc.Name = "PanelCheckIPLoc"
        Me.PanelCheckIPLoc.Size = New System.Drawing.Size(278, 137)
        Me.PanelCheckIPLoc.TabIndex = 47
        '
        'LabelCheckIPLocDesc
        '
        Me.LabelCheckIPLocDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.LabelCheckIPLocDesc.ForeColor = System.Drawing.Color.Silver
        Me.LabelCheckIPLocDesc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelCheckIPLocDesc.Location = New System.Drawing.Point(0, 80)
        Me.LabelCheckIPLocDesc.Name = "LabelCheckIPLocDesc"
        Me.LabelCheckIPLocDesc.Size = New System.Drawing.Size(278, 19)
        Me.LabelCheckIPLocDesc.TabIndex = 45
        Me.LabelCheckIPLocDesc.Text = "（无法检测智能代理/游戏专用加速器）"
        '
        'PanelPingTester
        '
        Me.PanelPingTester.BackColor = System.Drawing.Color.Transparent
        Me.PanelPingTester.Controls.Add(Me.LabelLosePkg)
        Me.PanelPingTester.Controls.Add(Me.LabelPing)
        Me.PanelPingTester.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.PanelPingTester.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.PanelPingTester.Location = New System.Drawing.Point(589, 30)
        Me.PanelPingTester.Name = "PanelPingTester"
        Me.PanelPingTester.Size = New System.Drawing.Size(132, 48)
        Me.PanelPingTester.TabIndex = 48
        '
        'PanelSelectClientTypeDir
        '
        Me.PanelSelectClientTypeDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PanelSelectClientTypeDir.BackColor = System.Drawing.Color.Transparent
        Me.PanelSelectClientTypeDir.Controls.Add(Me.ComboBoxCN)
        Me.PanelSelectClientTypeDir.Controls.Add(Me.PanelDir)
        Me.PanelSelectClientTypeDir.Controls.Add(Me.LabelSplitLine)
        Me.PanelSelectClientTypeDir.Controls.Add(Me.PanelComboBoxFontSelector)
        Me.PanelSelectClientTypeDir.Controls.Add(Me.ComboBoxClientType)
        Me.PanelSelectClientTypeDir.Controls.Add(Me.LabelFontSelector)
        Me.PanelSelectClientTypeDir.Controls.Add(Me.LabelSelectClientType)
        Me.PanelSelectClientTypeDir.Controls.Add(Me.LabelCN)
        Me.PanelSelectClientTypeDir.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.PanelSelectClientTypeDir.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.PanelSelectClientTypeDir.Location = New System.Drawing.Point(7, 405)
        Me.PanelSelectClientTypeDir.Name = "PanelSelectClientTypeDir"
        Me.PanelSelectClientTypeDir.Size = New System.Drawing.Size(445, 50)
        Me.PanelSelectClientTypeDir.TabIndex = 49
        '
        'PanelDir
        '
        Me.PanelDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PanelDir.BackColor = System.Drawing.Color.Transparent
        Me.PanelDir.Controls.Add(Me.ButtonOpenFile)
        Me.PanelDir.Controls.Add(Me.LabelDir)
        Me.PanelDir.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.PanelDir.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.PanelDir.Location = New System.Drawing.Point(0, 1)
        Me.PanelDir.Name = "PanelDir"
        Me.PanelDir.Size = New System.Drawing.Size(445, 25)
        Me.PanelDir.TabIndex = 54
        Me.PanelDir.Visible = False
        '
        'LabelCN
        '
        Me.LabelCN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelCN.AutoSize = True
        Me.LabelCN.BackColor = System.Drawing.Color.Transparent
        Me.LabelCN.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelCN.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.LabelCN.Location = New System.Drawing.Point(99, 6)
        Me.LabelCN.Name = "LabelCN"
        Me.LabelCN.Size = New System.Drawing.Size(65, 20)
        Me.LabelCN.TabIndex = 56
        Me.LabelCN.Text = "是否汉化"
        '
        'LabelTestDisplay1
        '
        Me.LabelTestDisplay1.AutoSize = True
        Me.LabelTestDisplay1.BackColor = System.Drawing.Color.Transparent
        Me.LabelTestDisplay1.ForeColor = System.Drawing.Color.Aqua
        Me.LabelTestDisplay1.Location = New System.Drawing.Point(3, 215)
        Me.LabelTestDisplay1.Name = "LabelTestDisplay1"
        Me.LabelTestDisplay1.Size = New System.Drawing.Size(131, 17)
        Me.LabelTestDisplay1.TabIndex = 51
        Me.LabelTestDisplay1.Text = "TestDisplay1 invisible"
        Me.LabelTestDisplay1.Visible = False
        '
        'LabelTestDisplay2
        '
        Me.LabelTestDisplay2.AutoSize = True
        Me.LabelTestDisplay2.BackColor = System.Drawing.Color.Transparent
        Me.LabelTestDisplay2.ForeColor = System.Drawing.Color.Aqua
        Me.LabelTestDisplay2.Location = New System.Drawing.Point(3, 232)
        Me.LabelTestDisplay2.Name = "LabelTestDisplay2"
        Me.LabelTestDisplay2.Size = New System.Drawing.Size(131, 17)
        Me.LabelTestDisplay2.TabIndex = 52
        Me.LabelTestDisplay2.Text = "TestDisplay2 invisible"
        Me.LabelTestDisplay2.Visible = False
        '
        'PanelMiscFunctions
        '
        Me.PanelMiscFunctions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelMiscFunctions.BackColor = System.Drawing.Color.Transparent
        Me.PanelMiscFunctions.Controls.Add(Me.LabelVisitReddit)
        Me.PanelMiscFunctions.Controls.Add(Me.LabelVisitOfficialSite)
        Me.PanelMiscFunctions.Controls.Add(Me.LabelGErrorCodeQuery)
        Me.PanelMiscFunctions.Controls.Add(Me.LabelPlayerName)
        Me.PanelMiscFunctions.Controls.Add(Me.TextBoxPlayerName)
        Me.PanelMiscFunctions.Controls.Add(Me.LabelPlayerKillboard)
        Me.PanelMiscFunctions.Controls.Add(Me.LabelPlayerStatistics)
        Me.PanelMiscFunctions.Controls.Add(Me.LabelPatchNote)
        Me.PanelMiscFunctions.Controls.Add(Me.ButtonFixLauncher503)
        Me.PanelMiscFunctions.Font = New System.Drawing.Font("微软雅黑", 9.75!)
        Me.PanelMiscFunctions.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.PanelMiscFunctions.Location = New System.Drawing.Point(300, 7)
        Me.PanelMiscFunctions.Name = "PanelMiscFunctions"
        Me.PanelMiscFunctions.Size = New System.Drawing.Size(278, 111)
        Me.PanelMiscFunctions.TabIndex = 53
        '
        'LabelPlayerName
        '
        Me.LabelPlayerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(19, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.LabelPlayerName.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LabelPlayerName.ForeColor = System.Drawing.Color.Silver
        Me.LabelPlayerName.Location = New System.Drawing.Point(4, 83)
        Me.LabelPlayerName.Name = "LabelPlayerName"
        Me.LabelPlayerName.Size = New System.Drawing.Size(138, 19)
        Me.LabelPlayerName.TabIndex = 55
        Me.LabelPlayerName.Text = "输入要查询的玩家名称"
        Me.LabelPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBoxPlayerName
        '
        Me.TextBoxPlayerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(19, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.TextBoxPlayerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxPlayerName.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBoxPlayerName.ForeColor = System.Drawing.Color.Silver
        Me.TextBoxPlayerName.Location = New System.Drawing.Point(3, 82)
        Me.TextBoxPlayerName.Name = "TextBoxPlayerName"
        Me.TextBoxPlayerName.Size = New System.Drawing.Size(140, 21)
        Me.TextBoxPlayerName.TabIndex = 53
        '
        'LauncherForm1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(23, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.BackgroundImage = Global.PS2CN_Launcher.My.Resources.Resources.DRsML8j_720_405
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(720, 464)
        Me.Controls.Add(Me.LabelKillGameClient)
        Me.Controls.Add(Me.LabelKillLaunchPad)
        Me.Controls.Add(Me.LabelSteam)
        Me.Controls.Add(Me.LabelManuallyTranslate)
        Me.Controls.Add(Me.ComboBoxFunctionSelector)
        Me.Controls.Add(Me.CheckBoxENVoice)
        Me.Controls.Add(Me.CheckBoxAutoRestore)
        Me.Controls.Add(Me.ComboBoxServer)
        Me.Controls.Add(Me.LabelReadme)
        Me.Controls.Add(Me.LabelDownloadGameUpdate)
        Me.Controls.Add(Me.CheckBoxCN)
        Me.Controls.Add(Me.CheckBox32bit)
        Me.Controls.Add(Me.PanelMiscFunctions)
        Me.Controls.Add(Me.PanelSelectClientTypeDir)
        Me.Controls.Add(Me.LabelTestDisplay2)
        Me.Controls.Add(Me.LabelUpdate)
        Me.Controls.Add(Me.LabelTestDisplay1)
        Me.Controls.Add(Me.PanelPingTester)
        Me.Controls.Add(Me.PanelCheckIPLoc)
        Me.Controls.Add(Me.PanelFunctionSelector)
        Me.Controls.Add(Me.LabelLogo)
        Me.Controls.Add(Me.PanelTimeZoneConverter)
        Me.Controls.Add(Me.LabelStatus)
        Me.Controls.Add(Me.PanelServerStatus)
        Me.Controls.Add(Me.LabelRun)
        Me.Controls.Add(Me.ButtonRun)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ForeColor = System.Drawing.Color.Gold
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "LauncherForm1"
        Me.Text = "行星边际2 PS2CN汉化器 - 20xx.xx.xx"
        Me.PanelTimeZoneConverter.ResumeLayout(False)
        Me.PanelTimeZoneConverter.PerformLayout()
        Me.PanelFunctionSelector.ResumeLayout(False)
        Me.PanelFunctionRadioButtonList.ResumeLayout(False)
        Me.PanelServerStatus.ResumeLayout(False)
        Me.PanelComboBoxFontSelector.ResumeLayout(False)
        Me.PanelCheckIPLoc.ResumeLayout(False)
        Me.PanelCheckIPLoc.PerformLayout()
        Me.PanelPingTester.ResumeLayout(False)
        Me.PanelSelectClientTypeDir.ResumeLayout(False)
        Me.PanelSelectClientTypeDir.PerformLayout()
        Me.PanelDir.ResumeLayout(False)
        Me.PanelMiscFunctions.ResumeLayout(False)
        Me.PanelMiscFunctions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents LabelDir As System.Windows.Forms.Label
    Friend WithEvents ButtonOpenFile As System.Windows.Forms.Button
    Friend WithEvents LabelStatus As System.Windows.Forms.Label
    Friend WithEvents LabelPing As System.Windows.Forms.Label
    Friend WithEvents LabelLosePkg As System.Windows.Forms.Label
    Friend WithEvents LabelSteam As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ComboBoxServer As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents LabelIPLoc As System.Windows.Forms.Label
    Friend WithEvents LabelUpdate As System.Windows.Forms.Label
    Friend WithEvents LabelLocStatus As System.Windows.Forms.Label
    Friend WithEvents LabelLogo As System.Windows.Forms.Label
    Friend WithEvents CheckBox32bit As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCN As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxClientType As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxENVoice As System.Windows.Forms.CheckBox
    Friend WithEvents LabelReadme As System.Windows.Forms.Label
    Friend WithEvents ComboBoxTimeZone1 As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxDST1 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxHour1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxHour2 As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxDST2 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxTimeZone2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxHour3 As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxDST3 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxTimeZone3 As System.Windows.Forms.ComboBox
    Friend WithEvents LabelTimeZoneTitle As System.Windows.Forms.Label
    Friend WithEvents PanelTimeZoneConverter As System.Windows.Forms.Panel
    Friend WithEvents RadioButtonServerStatus As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonTimeZoneConverter As System.Windows.Forms.RadioButton
    Friend WithEvents PanelFunctionSelector As System.Windows.Forms.Panel
    Friend WithEvents PanelServerStatus As System.Windows.Forms.Panel
    Friend WithEvents LabelServer2 As System.Windows.Forms.Label
    Friend WithEvents LabelServer3 As System.Windows.Forms.Label
    Friend WithEvents LabelServer1 As System.Windows.Forms.Label
    Friend WithEvents LabelServer4 As System.Windows.Forms.Label
    Friend WithEvents LabelServer5 As System.Windows.Forms.Label
    Friend WithEvents LabelServer6 As System.Windows.Forms.Label
    Friend WithEvents LabelServer7 As System.Windows.Forms.Label
    Friend WithEvents ButtonRequestServerStatus As System.Windows.Forms.Button
    Friend WithEvents LabelServer8 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxFontSelector As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonRun As System.Windows.Forms.Button
    Friend WithEvents LabelRun As System.Windows.Forms.Label
    Friend WithEvents LabelFontSelector As System.Windows.Forms.Label
    Friend WithEvents LabelCheckIPLocTitle As Label
    Friend WithEvents LabelSelectClientType As Label
    Friend WithEvents LabelSplitLine As Label
    Friend WithEvents PanelCheckIPLoc As Panel
    Friend WithEvents PanelComboBoxFontSelector As Panel
    Friend WithEvents LabelComboBoxFontSelector As Label
    Friend WithEvents LabelDSTTitle As Label
    Friend WithEvents LabelHourTitle As Label
    Friend WithEvents PanelPingTester As Panel
    Friend WithEvents PanelSelectClientTypeDir As Panel
    Friend WithEvents RadioButtonCheckIPLoc As RadioButton
    Friend WithEvents ButtonFixLauncher503 As Button
    Friend WithEvents CheckBoxAutoRestore As CheckBox
    Friend WithEvents CheckBoxShowFunctionUI As CheckBox
    Friend WithEvents PanelFunctionRadioButtonList As Panel
    Friend WithEvents LabelPatchNote As Label
    Friend WithEvents LabelDownloadGameUpdate As Label
    Friend WithEvents LabelCheckIPLocDesc As Label
    Friend WithEvents LabelTestDisplay1 As Label
    Friend WithEvents LabelTestDisplay2 As Label
    Friend WithEvents RadioButtonMiscFunctions As RadioButton
    Friend WithEvents PanelMiscFunctions As Panel
    Friend WithEvents ComboBoxFunctionSelector As ComboBox
    Friend WithEvents PanelDir As Panel
    Friend WithEvents ComboBoxCN As ComboBox
    Friend WithEvents LabelCN As Label
    Friend WithEvents LabelPlayerStatistics As Label
    Friend WithEvents LabelPlayerKillboard As Label
    Friend WithEvents TextBoxPlayerName As TextBox
    Friend WithEvents LabelPlayerName As Label
    Friend WithEvents LabelGErrorCodeQuery As Label
    Friend WithEvents LabelVisitOfficialSite As Label
    Friend WithEvents LabelVisitReddit As Label
    Friend WithEvents LabelManuallyTranslate As Label
    Friend WithEvents LabelKillLaunchPad As Label
    Friend WithEvents LabelKillGameClient As Label
End Class
