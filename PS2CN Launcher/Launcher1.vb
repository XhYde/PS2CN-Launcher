Imports System.Net.Http 'httpClient类。
Imports System.Net.NetworkInformation      'process类，用于获得系统进程

Public Class LauncherForm1

    Const Version As UInt32 = 20190425      '软件版本号，日期。每次【更新】记得【修改】！

    '每次更新后，github先提交【更改】，再【同步】到服务器

    '切勿使用Power Packs内的图形控件！！！

    Const fontCNKOR As String = "\msyh_RixMGoM.ttf"     '设定韩文客户端字体常量，用合成字体，前置目录为汉化器目录
    Const customFontDIR As String = "\自定义字体"        '设定自定义字体文件夹目录，前置目录为汉化器目录
    Const useDefaultFont As String = "默认字体"       '设定只用默认字体的文字描述
    Const useChineseFont As String = "中文黑体"       '设定使用中文黑体的文字描述
    'Dim fontCNEN As String = launchPad.DirectoryName & "\UI\Resource\Fonts\simhei.ttf"
    '设定美服客户端字体常量，用客户端黑体，前置目录为客户端目录
    Dim launchPad As FileInfo               '用于取得LaunchPad路径，endat修改时间
    Dim localeVersionEN As String = ""      '根据客户端类型，记录表示不同的英文dat修改时间
    Dim localeVersionNEN As String = ""     '根据客户端类型，记录表示不同的非英文dat修改时间
    Dim dataNameNEN As String = "\ko_kr_data"           '根据不同客户端类型，返回值为不同的非英文语言文本名称
    'Dim tmpFileInfo As FileInfo             '临时FileInfo文件，改为每个函数各自定义tempfile，无需作为全局变量
    Dim sendPing As New Ping                '发送ping
    Dim optionsPing As New PingOptions(128, True)       '设置ping选项ttl As Integer, dontFragment As Boolean
    Dim pingTimeout(200) As Boolean         '判断ping是否超时，false为ping成功，true为超时。数组默认值0(false)
    Dim pingNum As Byte = 0                 '对应上面数组，表示第几次测试。范围0-199
    Dim pingRound1 As Boolean = True        '判断是否为第一轮ping
    Dim pingRound1NOK As Byte = 0           '第一轮ping中，丢包次数
    Dim pingLosePkg As Single = 0           '带小数，统计丢包率
    Const ipLocTimerMax As Byte = 120       '设置IP归属地查询的最大时间间隔，[120]×1秒=120秒
    Const ipLocTimerMin As Byte = 10       '设定点击IP归属地查询的最小时间间隔，必须小于上方ipLocTimerMax，并大于下方ipLocTimeout
    Const ipLocTimeout As Byte = 8       '设定IP归属地查询的超时时间
    Dim ipLocTimer As Byte = 0              '读取IP位置的倒计时，>ipLocTimerMax时暂停计时，表示需手工重启计时。
    Const serverStatusTimerMax As Byte = 90     '设置查询美服状态的时间间隔，[90]×1秒=90秒
    Dim serverStatusTimer As Byte = 0           '查询美服状态的倒计时，>serverStatusTimerMax时暂停计时，表示需手工重启计时。
    '初始值设为1，可避免汉化器一打开时visible_change事件触发，防止直接查询服务器状态。
    Dim serverIP As String = "0"            '0作为判断符号，表示不测试ping。
    Dim formActive As Boolean = True        '当前窗体是否激活
    Dim bkFontFolder As String = Application.StartupPath & "\Font"      '设定字体备份文件夹名称
    Dim bkDataFolder As String = Application.StartupPath & "\Data\STEAM"   '设定文本备份文件夹名称
    Dim bkEXEFolder As String = Application.StartupPath & "\EXE\STEAM"     '设定exe备份文件夹名称
    Dim resultStatusNum As Integer = MsgBoxStyle.Information            '根据运行结果，设定msgbox类型，info为正常
    Dim GameRunningStatSwitch As Boolean = False    '状态切换标识符，标示此前游戏是在运行/停止状态，用于判断游戏开始/结束运行的瞬间
    '用于在游戏开始运行和结束运行的瞬间最大/最小化窗体。若只判断正在运行状态，则每秒都会判断一次，每秒都会自动将窗体最小化，这不合理。
    Dim ManuallyTranslated As Boolean = False       '内存中的手动汉化标识符，若程序运行后执行过手动汉化，则本次运行时屏蔽自动还原备份功能
    Dim doubleTime As Boolean               '通过一个是非判断，将判断延时设定为timer tick的2倍。
    Dim launchedFromSteam As Boolean = False        '内存中的Steam启动标识符，若程序运行后以steam://rungameid启动登录器，则记录之后内存中的登录器路径

    Private Enum ClientTypeInSettings As Byte     '定义设置中的客户端类型
        '由于ComboBox的Index数值连续且无法自定义，若有删减项则Index与Settings的对应关系会破坏，因此需要转换函数
        '此处数字不需要连续，一旦定下不可更改。对应关系应该在2个转换函数中修改
        '转换函数为 ClientTypeSelectedIndexToSetting 和 SettingToClientTypeSelectedIndex

        EN = 0
        TEST = 1
        KOR = 2
        RU = 3
        STEAM = 4
        'EN由美服改名为官方版
        '新增Steam版
        'RU = 2     俄服已无法汉化
        'KOR= 3
    End Enum

    Private Function ClientTypeSelectedIndexToSetting(selectedComboBoxClientType As Byte)
        '根据ComboBoxClientType控件选择值，赋值给My.Settings.ClientType

        '由于ComboBox的Index数值连续且无法自定义，若有删减项则Index与Settings的对应关系会破坏，因此需要转换函数
        '若控件下拉菜单文字和顺序修改, 只需修改本函数的对应关系即可。

        Try
            Select Case selectedComboBoxClientType
                Case 0
                    '下拉菜单第1项，对应Settings中客户端类型如下
                    Return ClientTypeInSettings.STEAM
                Case 1
                    '下拉菜单第2项，对应Settings中客户端类型如下
                    Return ClientTypeInSettings.EN
                Case 2
                    '下拉菜单第3项，对应Settings中客户端类型如下
                    Return ClientTypeInSettings.TEST

                    'Case 0
                    '    '下拉菜单第1项，对应Settings中客户端类型如下
                    '    Return ClientTypeInSettings.EN
                    'Case 1
                    '    '下拉菜单第2项，对应Settings中客户端类型如下
                    '    Return ClientTypeInSettings.TEST

                    '俄服已关服，取消此功能
                    'Case 2
                    '下拉菜单第3项，对应Settings中客户端类型如下
                    'Return ClientType.RU

                    '韩服已关服，取消此功能
                    'Case 3
                    '下拉菜单第3项，对应Settings中客户端类型如下
                    'Return ClientType.KOR

                Case Else
                    '下拉菜单其他项，对应Settings中客户端类型如下
                    Return ClientTypeInSettings.STEAM

            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ClientTypeSelectedIndexToSetting Error")
            Return ClientTypeInSettings.STEAM
        End Try
    End Function

    Private Function SettingToClientTypeSelectedIndex(ClientTypeSetting As Byte)
        '根据ComboBoxClientType控件选择值，赋值给My.Settings.ClientType

        '由于ComboBox的Index数值连续且无法自定义，若有删减项则Index与Settings的对应关系会破坏，因此需要转换函数
        '若控件下拉菜单文字和顺序修改, 只需修改本函数的对应关系即可。

        Try
            Select Case ClientTypeSetting
                Case ClientTypeInSettings.STEAM
                    '下拉菜单第1项，对应Settings中客户端类型如下
                    Return 0
                Case ClientTypeInSettings.EN
                    '下拉菜单第2项，对应Settings中客户端类型如下
                    Return 1
                Case ClientTypeInSettings.TEST
                    '下拉菜单第3项，对应Settings中客户端类型如下
                    Return 2

                    'Case ClientTypeInSettings.EN
                    '    '下拉菜单第1项，对应Settings中客户端类型如下
                    '    Return 0
                    'Case ClientTypeInSettings.TEST
                    '    '下拉菜单第2项，对应Settings中客户端类型如下
                    '    Return 1

                    '韩服已关服，取消此功能
                    'Case ClientType.KOR
                    '    '下拉菜单第3项，对应Settings中客户端类型如下
                    '    Return 2

                    '俄服已关服，取消此功能
                    'Case ClientType.RU
                    '下拉菜单第4项，对应Settings中客户端类型如下
                    '    Return 2

                Case Else
                    '下拉菜单其他项，对应Settings中客户端类型如下
                    Return ClientTypeInSettings.STEAM
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ClientTypeSelectedIndexToSetting Error")
            Return ClientTypeInSettings.STEAM
        End Try
    End Function

    Private Enum FunctionSelector As Byte   '定义设置中的功能编号
        '把程序中通用的功能编号,全部使用文字枚举来代替，避免出错
        '无需转换函数，与设置中的功能编号对应好即可
        '此处数字不需要连续，一旦定下不可更改
        '如有功能增减，在 SettingsToFunctionSelection 函数中，将相关判断和操作语句进行修改即可

        HideFunctionUI = 0
        CheckIPLoc = 1
        TimeZoneConvertor = 2
        MiscFunctions = 3
        ServerStatus = 4
    End Enum

    Private Sub SettingsToFunctionSelection(functionSelectionInSettings As Byte)
        '根据设定中的选项，选择功能，更新单选框状态，显示相应工具区
        Try
            If My.Settings.CheckedShowFunctionUI = False Then
                functionSelectionInSettings = FunctionSelector.HideFunctionUI
            End If

            Select Case functionSelectionInSettings

                Case FunctionSelector.HideFunctionUI
                    '若选择隐藏工具区

                    ComboBoxFunctionSelector.SelectedIndex = 0

                    PanelCheckIPLoc.Visible = False                 '不显示 查询IP归属地相关控件
                    PanelTimeZoneConverter.Visible = False          '不显示 时区转换器相关控件
                    PanelServerStatus.Visible = False               '不显示 美服状态查询相关控件
                    PanelMiscFunctions.Visible = False              '不显示 实用功能相关控件

                Case FunctionSelector.CheckIPLoc
                    '若选择功能为查询IP归属地

                    ComboBoxFunctionSelector.SelectedIndex = 2

                    PanelCheckIPLoc.Visible = True                  '显示 查询IP归属地相关控件
                    PanelTimeZoneConverter.Visible = False          '不显示    时区转换器相关控件
                    PanelServerStatus.Visible = False               '不显示    美服状态查询相关控件
                    PanelMiscFunctions.Visible = False              '不显示    实用功能相关控件

                Case FunctionSelector.TimeZoneConvertor
                    '若选择功能为时区转换器

                    ComboBoxFunctionSelector.SelectedIndex = 3

                    PanelCheckIPLoc.Visible = False                 '不显示    查询IP归属地相关控件
                    PanelTimeZoneConverter.Visible = True           '显示 时区转换器相关控件
                    PanelServerStatus.Visible = False               '不显示    美服状态查询相关控件
                    PanelMiscFunctions.Visible = False              '不显示    实用功能相关控件

                    'Case FunctionSelector.ServerStatus
                    '    '若选择功能为美服状态查询

                    '    'ComboBoxFunctionSelector.SelectedIndex = 4

                    '    PanelCheckIPLoc.Visible = False                 '不显示    查询IP归属地相关控件
                    '    PanelTimeZoneConverter.Visible = False          '不显示    时区转换器相关控件
                    '    PanelServerStatus.Visible = True                '显示 美服状态查询相关控件
                    '    PanelMiscFunctions.Visible = False              '不显示    实用功能相关控件

                Case Else
                    '若选择功能为其他，包括选择实用功能，则强制切换回实用功能

                    ComboBoxFunctionSelector.SelectedIndex = 1

                    PanelCheckIPLoc.Visible = False                 '不显示    查询IP归属地相关控件
                    PanelTimeZoneConverter.Visible = False          '不显示    时区转换器相关控件
                    PanelServerStatus.Visible = False               '不显示    美服状态查询相关控件
                    PanelMiscFunctions.Visible = True               '显示 实用功能相关控件

            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "SettingsToFunctionSelection Error")
        End Try
    End Sub

    'Private Sub ShowOrHideFunctionUI()      '根据设定中显示工具区选项状态，显示/隐藏工具区
    '    Try
    '        If My.Settings.CheckedShowFunctionUI Then
    '            '若设定为显示工具区
    '            SettingsToFunctionSelection(My.Settings.FunctionSelector)   '根据设定中的选项，选择功能，更新单选框状态，显示相应工具区

    '            PanelFunctionRadioButtonList.Visible = True     '显示单选框列表

    '            'LabelSplitLine2.Visible = True                  '显示工具区分割线
    '            '分割线已删除

    '        Else
    '            '若设定为隐藏工具区
    '            PanelFunctionRadioButtonList.Visible = False    '隐藏单选框列表

    '            'LabelSplitLine2.Visible = False                 '隐藏工具区分割线
    '            '分割线已删除

    '            PanelCheckIPLoc.Visible = False                 '不显示 查询IP归属地相关控件
    '            PanelTimeZoneConverter.Visible = False          '不显示 时区转换器相关控件
    '            PanelServerStatus.Visible = False               '不显示 美服状态查询相关控件
    '            PanelMiscFunctions.Visible = False              '不显示 实用功能相关控件
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ShowOrHideFunctionUI Error")
    '    End Try
    'End Sub

    'Private Sub SettingsToFunctionSelection(functionSelectionInSettings As Byte)
    '    '根据设定中的选项，选择功能，更新单选框状态，显示相应工具区
    '    Try
    '        Select Case functionSelectionInSettings

    '            Case FunctionSelector.TimeZoneConvertor
    '                '若选择功能为时区转换器

    '                RadioButtonCheckIPLoc.Checked = False           '取消勾选   查询IP归属地
    '                RadioButtonTimeZoneConverter.Checked = True     '勾选 时区转换器
    '                RadioButtonServerStatus.Checked = False         '取消勾选   美服状态查询
    '                RadioButtonMiscFunctions.Checked = False        '取消勾选   实用功能

    '                If My.Settings.CheckedShowFunctionUI Then
    '                    '仅当显示工具区已勾选，才显示相应工具区
    '                    PanelCheckIPLoc.Visible = False                 '不显示    查询IP归属地相关控件
    '                    PanelTimeZoneConverter.Visible = True           '显示 时区转换器相关控件
    '                    PanelServerStatus.Visible = False               '不显示    美服状态查询相关控件
    '                    PanelMiscFunctions.Visible = False              '不显示    实用功能相关控件
    '                Else

    '                End If

    '            Case FunctionSelector.ServerStatus
    '                '若选择功能为美服状态查询

    '                RadioButtonCheckIPLoc.Checked = False           '取消勾选   查询IP归属地
    '                RadioButtonTimeZoneConverter.Checked = False    '取消勾选   时区转换器
    '                RadioButtonServerStatus.Checked = True          '勾选 美服状态查询
    '                RadioButtonMiscFunctions.Checked = False        '取消勾选   实用功能

    '                If My.Settings.CheckedShowFunctionUI Then
    '                    '仅当显示工具区已勾选，才显示相应工具区
    '                    PanelCheckIPLoc.Visible = False                 '不显示    查询IP归属地相关控件
    '                    PanelTimeZoneConverter.Visible = False          '不显示    时区转换器相关控件
    '                    PanelServerStatus.Visible = True                '显示 美服状态查询相关控件
    '                    PanelMiscFunctions.Visible = False              '不显示    实用功能相关控件
    '                End If

    '            Case FunctionSelector.MiscFunctions
    '                '若选择功能为实用功能

    '                RadioButtonCheckIPLoc.Checked = False           '取消勾选   查询IP归属地
    '                RadioButtonTimeZoneConverter.Checked = False    '取消勾选   时区转换器
    '                RadioButtonServerStatus.Checked = False         '取消勾选   美服状态查询
    '                RadioButtonMiscFunctions.Checked = True         '勾选 实用功能

    '                If My.Settings.CheckedShowFunctionUI Then
    '                    '仅当显示工具区已勾选，才显示相应工具区
    '                    PanelCheckIPLoc.Visible = False                 '不显示    查询IP归属地相关控件
    '                    PanelTimeZoneConverter.Visible = False          '不显示    时区转换器相关控件
    '                    PanelServerStatus.Visible = False               '不显示    美服状态查询相关控件
    '                    PanelMiscFunctions.Visible = True               '显示 实用功能相关控件
    '                End If

    '            Case Else
    '                '若选择功能为其他，包括选择查询IP归属地，则强制切换回查询IP归属地

    '                RadioButtonCheckIPLoc.Checked = True            '勾选 查询IP归属地
    '                RadioButtonTimeZoneConverter.Checked = False    '取消勾选   时区转换器
    '                RadioButtonServerStatus.Checked = False         '取消勾选   美服状态查询
    '                RadioButtonMiscFunctions.Checked = False        '取消勾选   实用功能

    '                If My.Settings.CheckedShowFunctionUI Then
    '                    '仅当显示工具区已勾选，才显示相应工具区
    '                    PanelCheckIPLoc.Visible = True                  '显示 查询IP归属地相关控件
    '                    PanelTimeZoneConverter.Visible = False          '不显示    时区转换器相关控件
    '                    PanelServerStatus.Visible = False               '不显示    美服状态查询相关控件
    '                    PanelMiscFunctions.Visible = False              '不显示    实用功能相关控件
    '                End If

    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "SettingsToFunctionSelection Error")
    '    End Try
    'End Sub

    Private Structure ServerStatusColor
        '定义美服状态颜色
        Shared SSCup As Color = Color.Lime              '在线
        Shared SSClock As Color = Color.Yellow          '锁定（无法登录，但是服务器仍在运行，玩家未被踢出）
        Shared SSCmaintain As Color = Color.Aquamarine  '维护，服务器关闭
        Shared SSCdown As Color = Color.Red             '服务器关闭
    End Structure

    Private Enum ButtonStatus As Byte   '定义按钮状态类型
        SetLocation = 0
        Backup = 1
        Launch = 2
        Modify = 3
        Start = 4
        Playing = 5
    End Enum

    Private Sub LauncherForm1_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            formActive = True       '窗体获得焦点后，且离上次查询已过60秒，立即查询一次，并启用自动定时查询计时

            '取消自动查询IP归属
            'If ipLocTimer = ipLocTimerMax Then
            '    QueryIPLoc()        '查询IP归属
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LauncherForm1_Activated Error")
        End Try
    End Sub

    Private Sub LauncherForm1_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Try
            formActive = False   '停止后台查询IP地址
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LauncherForm1_Deactivate Error")
        End Try
    End Sub

    Private Sub LauncherForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Try
                Dim workingRectangle As Rectangle
                workingRectangle = Screen.PrimaryScreen.Bounds  '用workingRectangle长方形，获取复制屏幕显示区域的分辨率

                'Me此处为LauncherForm1，即汉化器主窗体From

                If workingRectangle.Height > 950 Then
                    '登录器Launcher一半高度338 + 汉化器保留高度（汉化/字体选项及汉化按钮）137 = 475，乘以2，得950。反推得到屏幕分辨率高度的下限

                    '若屏幕高度大于下限，则可将汉化器窗体置于游戏登录器Launcher右下位置，且不超出屏幕下沿
                    'Launcher覆盖窗体的左上部分，露出右下功能区

                    Me.Top = workingRectangle.Height / 2 + 475 - Me.Height       'Launcher下方，露出汉化器左下方汉化/字体选项，及右下角汉化按钮
                    '登录器Launcher一半高度338 + 汉化器保留高度（汉化/字体选项及汉化按钮）137 = 475
                    Me.Left = workingRectangle.Width / 2 + 602 - Me.Width       'Launcher右侧，露出汉化器右上角测ping功能
                    '登录器Launcher一半宽度456 + 汉化器保留宽度（右上角测ping功能）146 = 602

                Else
                    '若屏幕高度小于下限，则汉化器底端紧贴屏幕下沿

                    Me.Top = workingRectangle.Height - Me.Height    '汉化器底端紧贴屏幕下沿

                    If workingRectangle.Width > 1432 Then
                        '登录器Launcher一半宽度456 + 汉化器保留宽度（汉化/字体选项及汉化按钮）260 = 716，乘以2，1432。反推得到屏幕分辨率高度的下限

                        '若屏幕宽度大于下限，则可将汉化器置于游戏登录器Launcher右侧，且不超出屏幕右边沿
                        'Launcher覆盖窗体左侧部分，露出汉化器右下角汉化/ 字体选项及汉化按钮

                        Me.Left = workingRectangle.Width / 2 + 716 - Me.Width   'Launcher右侧，露出汉化器右下角汉化/字体选项及汉化按钮
                        '登录器Launcher一半宽度456 + 汉化器保留宽度（汉化/字体选项及汉化按钮）260 = 716

                    Else
                        '若屏幕宽度小于下限，则汉化器右侧紧贴屏幕右边沿

                        Me.Left = workingRectangle.Width - Me.Width      '汉化器右侧紧贴屏幕右边沿

                    End If
                End If

            Catch exSetFormLocation As Exception
                If exSetFormLocation.InnerException Is Nothing Then
                    '若包含InnerException异常，则显示详细信息
                    MsgBox(exSetFormLocation.Message, MsgBoxStyle.Exclamation,
                           "SetFormLocation Error")
                Else
                    MsgBox(exSetFormLocation.Message & Chr(13) & Chr(10) & exSetFormLocation.InnerException.Message,
                           MsgBoxStyle.Information,
                           "SetFormLocation Error")
                End If
            End Try

            LauncherForm1ShowTitle()
            '读取version常量，显示版本号

            ToolTip1.SetToolTip(ButtonRequestServerStatus, "查询间隔" & serverStatusTimerMax & "秒。请勿频繁查询")
            '根据设定的查询间隔，显示设置按钮的ToolTip。

            CreateFolder()      '建立相应文件夹，避免文件复制时路径错误
            '无任何关联，直接创建目录，最先执行

            Try
                LabelUpdate.Text = ""

                If My.Settings.CheckedCN Then   '根据用户的设定，显示汉化ComboBox状态
                    ComboBoxCN.SelectedIndex = 1
                Else
                    ComboBoxCN.SelectedIndex = 0
                End If
                'CheckBoxCN.Checked = My.Settings.CheckedCN      '根据用户的设定，显示汉化checkbox状态

                'CheckBoxAutoRestore.Checked = My.Settings.CheckedAutoRestore '根据用户的设定，显示自动恢复备份checkbox状态
                My.Settings.CheckedAutoRestore = True       '自动恢复备份功能工作正常，改为标配，不再提供设置选项
                CheckBoxAutoRestore.Checked = Enabled       '目前也没有其他启动方式不同的海外服务器了。

                'CheckBox32bit.Checked = My.Settings.Checked32bit    '根据用户的设定，显示32位客户端checkbox状态
                My.Settings.Checked32bit = False            '使用32位客户端功能关闭，俄服只有32位，美服只有64位……
                CheckBox32bit.Checked = False               '俄服更新了64位，然而并不想恢复此功能……

                'CheckBoxENVoice.Checked = My.Settings.CheckedENVoice    '根据用户的设定，显示英文语音checkbox状态
                My.Settings.CheckedENVoice = False      '俄服已不支持删除俄文ru文本，来调用英文语音客户端，取消此功能。
                CheckBoxENVoice.Checked = False

                'If My.Settings.ClientType = ClientTypeInSettings.STEAM _
                '    Or My.Settings.ClientType = ClientTypeInSettings.EN _
                '    Or My.Settings.ClientType = ClientTypeInSettings.TEST Then
                '    '判断设置值范围是否合法
                '    ComboBoxClientType.SelectedIndex = SettingToClientTypeSelectedIndex(My.Settings.ClientType)   '选择用户设定的客户端类型。
                'Else
                '    ComboBoxClientType.SelectedIndex = ClientTypeInSettings.STEAM    '韩服、俄服已关服，若设置值不合法，默认客户端类型设为Steam版
                'End If
                'My.Settings.ClientType = ClientTypeSelectedIndexToSetting(ComboBoxClientType.SelectedIndex)        '将程序设置值，相应更改

                Select Case My.Settings.ClientType
                    Case ClientTypeInSettings.STEAM, ClientTypeInSettings.EN, ClientTypeInSettings.TEST
                        '判断设置值范围是否合法
                        ComboBoxClientType.SelectedIndex = SettingToClientTypeSelectedIndex(My.Settings.ClientType)   '选择用户设定的客户端类型。
                    Case Else
                        ComboBoxClientType.SelectedIndex = ClientTypeInSettings.STEAM    '韩服、俄服已关服，若设置值不合法，默认客户端类型设为Steam版
                End Select
                My.Settings.ClientType = ClientTypeSelectedIndexToSetting(ComboBoxClientType.SelectedIndex)        '将程序设置值，相应更改

                BuildComboBoxFontSelectorList()
                '重新读取自定义字体文件夹中的字体文件，生成新的下拉菜单，并将text设为保存的设置值

                If My.Settings.PingServer < 6 Then      '判断设置值是否合法
                    ComboBoxServer.SelectedIndex = My.Settings.PingServer    '选择用户设定的服务器
                Else
                    ComboBoxServer.SelectedIndex = 0    '若设置值不合法，默认服务器设为0
                    My.Settings.PingServer = 0
                End If

                'If My.Settings.FunctionSelector < 4 AndAlso My.Settings.FunctionSelector > 0 Then    '判断设置值是否合法
                '    '目前服务器状态功能（4）无法使用，因而限制为<4
                '    '目前0为保留功能编号，未使用，因而不得为0
                '    '程序初始FunctionSelector设置为0，首次运行会主动调用下方默认功能，更直观。避免设置内指定编号而不知道对应功能是啥，及是否变更。
                '    SettingsToFunctionSelection(My.Settings.FunctionSelector)        '根据设定值，选择功能
                'Else
                '    SettingsToFunctionSelection(FunctionSelector.MiscFunctions)       '若值非法，强制切换为实用功能
                '    My.Settings.FunctionSelector = FunctionSelector.MiscFunctions
                'End If

                'CheckBoxShowFunctionUI.Checked = My.Settings.CheckedShowFunctionUI      '根据用户的设定，设定工具区显示/隐藏状态
                ''ShowOrHideFunctionUI()          '根据设定中显示工具区选项状态，显示/隐藏工具区

                SettingsToFunctionSelection(My.Settings.FunctionSelector)   '根据设定中的选项，选择功能，更新ComboBox状态，显示相应工具区

                If My.Settings.TimeZone1 < 24 Then      '判断设置值是否合法
                    ComboBoxTimeZone1.SelectedIndex = My.Settings.TimeZone1    '选择用户设定的时区
                Else
                    ComboBoxTimeZone1.SelectedIndex = 4    '若设置值不合法，默认时区设为美西
                    My.Settings.TimeZone1 = 4
                End If

                If My.Settings.TimeZone2 < 24 Then      '判断设置值是否合法
                    ComboBoxTimeZone2.SelectedIndex = My.Settings.TimeZone2    '选择用户设定的时区
                Else
                    ComboBoxTimeZone2.SelectedIndex = 20    '若设置值不合法，默认时区设为北京
                    My.Settings.TimeZone2 = 20
                End If

                If My.Settings.TimeZone3 < 24 Then      '判断设置值是否合法
                    ComboBoxTimeZone3.SelectedIndex = My.Settings.TimeZone3    '选择用户设定的时区
                Else
                    ComboBoxTimeZone3.SelectedIndex = 13    '若设置值不合法，默认时区设为德国
                    My.Settings.TimeZone3 = 13
                End If

                If My.Settings.TimeHour > 24 Then
                    My.Settings.TimeHour = 0         '若设置值不合法，默认时间设为0
                End If

                If My.Settings.TimeSelection < 3 Then      '判断设置值是否合法
                    Select Case My.Settings.TimeSelection
                        Case 0
                            ComboBoxHour1.SelectedIndex = My.Settings.TimeHour    '选择用户设定的时间
                        Case 1
                            ComboBoxHour2.SelectedIndex = My.Settings.TimeHour    '选择用户设定的时间
                        Case 2
                            ComboBoxHour3.SelectedIndex = My.Settings.TimeHour    '选择用户设定的时间
                    End Select
                    ConvertTime(My.Settings.TimeSelection)      '根据用户设定的基准时间，转换成其他时区时间
                Else
                    My.Settings.TimeSelection = 0       '若设置值不合法，默认服务器设为0
                End If

                CheckBoxDST1.Checked = My.Settings.CheckedDST1        '根据用户的设定，显示夏令时checkbox状态
                CheckBoxDST2.Checked = My.Settings.CheckedDST2
                CheckBoxDST3.Checked = My.Settings.CheckedDST3


                'My.Settings.Save()
                '根据设置，将界面选项加载完毕，其它函数都会引用界面元素作为判断条件

            Catch exSettingsProcess As Exception
                If exSettingsProcess.InnerException Is Nothing Then
                    '若包含InnerException异常，则显示详细信息
                    MsgBox(exSettingsProcess.Message, MsgBoxStyle.Exclamation,
                           "SettingsProcess Error")
                Else
                    MsgBox(exSettingsProcess.Message & Chr(13) & Chr(10) & exSettingsProcess.InnerException.Message,
                           MsgBoxStyle.Information,
                           "SettingsProcess Error")
                End If
            End Try

            SetClientType()     '根据客户端不同，修改相关内存变量及参数，用于条件判断

            'SetFileAttributes() '取消只读属性，同时检测路径是否合法
            ''窗体加载时取消执行，避免程序一打开，直接让用户选择客户端路径。

            SelectIP()  '根据设置值，将服务器IP定义到字符串serverIP中

            RunOrShowButton(False)        '更新显示按钮状态

            CheckVersion()      '检查版本更新

            ClearUnusedFiles()      '删除由汉化器创建，但是已废弃不再使用的文件

            AddHandler sendPing.PingCompleted, AddressOf PingRequestCompleted
            '指定异步接收PingCompleted事件所调用的操作方法

            Timer1.Enabled = True       '启用计时器
            Timer1.Start()

        Catch ex As Exception
            If ex.InnerException Is Nothing Then
                '若包含InnerException异常，则显示详细信息
                MsgBox(ex.Message, MsgBoxStyle.Exclamation,
                       "LauncherForm1_Load Error")
            Else
                MsgBox(ex.Message & Chr(13) & Chr(10) & ex.InnerException.Message,
                       MsgBoxStyle.Information,
                       "LauncherForm1_Load Error")
            End If
        End Try
    End Sub

    Private Sub LauncherForm1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            sendPing.SendAsyncCancel()      '取消当前正在等待接收的sendping，需在ping.complete事件中添加关于getPing.Cancelled判断
            Timer1.Stop()
            My.Settings.Save()  '关闭窗体前保存程序设置
            SteamHelp.Dispose()
            Me.Dispose()    '清空所有已占用资源
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LauncherForm1_FormClosing Error")
        End Try
    End Sub

    Private Sub PingRequestCompleted(ByVal sender As Object, ByVal getPing As PingCompletedEventArgs)
        '异步接收Ping.SendAsync事件的响应，收到PingCompleted事件后，执行以下操作
        Try
            If serverIP <> "0" AndAlso Not (getPing.Cancelled) Then
                '仅当启用测ping功能，且异步发送ping未被异步取消时，才继续执行

                If getPing.Reply.Status = IPStatus.Success Then   '如果ping成功得到响应
                    If pingRound1 Then    '第一轮使用除法确保精度。
                        PingRound1Fail(False)  'ping第一轮，并且ping成功，没有超时
                    Else        '第二轮以后，直接用加减法，减轻系统负担
                        If pingTimeout(pingNum) = True Then     '统计丢包率,若本数组初始值为1，
                            pingLosePkg -= 0.5                  '本次成功ping通将使丢包率下降0.5%
                        End If
                    End If
                    LabelPing.Text = getPing.Reply.RoundtripTime & "毫秒"   '显示ping数值
                    pingTimeout(pingNum) = False    '设置超时统计，本次没有超时
                Else        '如果超时
                    If pingRound1 Then    '第一轮使用除法确保精度。
                        PingRound1Fail(True) 'ping第一轮，并且ping超时
                    Else        '第二轮以后，直接用加减法，减轻系统负担
                        If pingTimeout(pingNum) = False Then    '统计丢包率,若本数组初始值为0，
                            pingLosePkg += 0.5                  '本次ping失败将使丢包率增加0.5%
                        End If
                    End If
                    LabelPing.Text = "连接超时"     '显示测ping失败
                    pingTimeout(pingNum) = True    '超时统计为1，本次超时
                End If

                LabelLosePkg.Text = String.Format("{0:n}", pingLosePkg) & "%"       'label上文本输出丢包率

                If pingNum >= 199 Then   '超时统计数组上限200，标识位在0-199之间循环
                    pingNum = 0         '从头开始循环，向后覆盖数组值
                    If pingRound1 Then
                        pingRound1 = False
                    End If
                Else
                    pingNum += 1        '数组标识位+1
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "PingCompleteProcess Error")
            '屏蔽所有ping的意外错误，例如断网
        End Try
    End Sub

    Private Sub LauncherForm1ShowTitle(Optional CheckVersion As String = "")
        '修改总窗体顶部的文字标题
        Me.Text = "行星边际2 PS2CN汉化器 - " _
                & Version \ 10000 & "." _
                & (Version Mod 10000) \ 100 & "." _
                & Version Mod 100 _
                & IIf(CheckVersion = "", "", "  " & CheckVersion)
        '读取version常量，显示版本号
        '最后依据输入字符串变量CheckVersion，显示检查版本信息
        '若CheckVersion参数未指定，则末尾不接字符串。若指定了参数，则先输入2个空格，再显示CheckVersion的文本
    End Sub

    Private Sub CreateFolder()      '建立相应文件夹，避免文件复制时路径错误
        Try

            If Not (Directory.Exists(Application.StartupPath & "\Data\STEAM")) Then
                Directory.CreateDirectory(Application.StartupPath & "\Data\STEAM")
            End If

            If Not (Directory.Exists(Application.StartupPath & "\Data\EN")) Then
                Directory.CreateDirectory(Application.StartupPath & "\Data\EN")
            End If

            If Not (Directory.Exists(Application.StartupPath & "\Data\TEST")) Then
                Directory.CreateDirectory(Application.StartupPath & "\Data\TEST")
            End If

            'Directory.CreateDirectory(Application.StartupPath & "\Data\RU")        '俄服已死
            'Directory.CreateDirectory(Application.StartupPath & "\Data\KOR")       '韩服已死

            '俄服韩服已死，美服全部64位取消32位，此功能已无意义。以后可以考虑用前置控制变量来开关此功能
            'If Not (Directory.Exists(Application.StartupPath & "\EXE\TEST")) Then
            '    Directory.CreateDirectory(Application.StartupPath & "\EXE\EN")
            '    Directory.CreateDirectory(Application.StartupPath & "\EXE\TEST")
            '    'Directory.CreateDirectory(Application.StartupPath & "\EXE\RU")         '俄服没有64位客户端
            '    Directory.CreateDirectory(Application.StartupPath & "\EXE\KOR")
            'End If

            If Not (Directory.Exists(Application.StartupPath & customFontDIR)) Then
                Directory.CreateDirectory(Application.StartupPath & customFontDIR)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CreateFolder Error")   '收集错误信息，建议保留
        End Try
    End Sub

    Private Sub ClearUnusedFiles()      '删除由汉化器创建，但是已废弃不再使用的文件
        Try
            If Directory.Exists(Application.StartupPath & "\EXE") Then
                '若此exe备份文件夹存在，进行后续判断

                CheckAndDeleteFile(Application.StartupPath & "\EXE\EN\PlanetSide2_x64.exe")
                CheckAndDeleteFile(Application.StartupPath & "\EXE\TEST\PlanetSide2_x64.exe")
                CheckAndDeleteFile(Application.StartupPath & "\EXE\RU\PlanetSide2_x64.exe")
                CheckAndDeleteFile(Application.StartupPath & "\EXE\KOR\PlanetSide2_x64.exe")
                '此游戏已无32位客户端，韩服俄服已死，删除所有备份的exe文件

                CheckAndDeleteFolder(Application.StartupPath & "\EXE\EN")
                CheckAndDeleteFolder(Application.StartupPath & "\EXE\TEST")
                CheckAndDeleteFolder(Application.StartupPath & "\EXE\RU")
                CheckAndDeleteFolder(Application.StartupPath & "\EXE\KOR")
                CheckAndDeleteFolder(Application.StartupPath & "\EXE")
                '检查文件夹是否存在&为空，并删除空文件夹
            End If

            If Directory.Exists(Application.StartupPath & "\DATA\RU") Then
                '若此韩服俄服语言备份文件夹存在，进行后续判断

                CheckAndDeleteFile(Application.StartupPath & "\DATA\RU\en_us_data.dat")
                CheckAndDeleteFile(Application.StartupPath & "\DATA\RU\en_us_data.dir")
                CheckAndDeleteFile(Application.StartupPath & "\DATA\RU\ru_ru_data.dat")
                CheckAndDeleteFile(Application.StartupPath & "\DATA\RU\ru_ru_data.dir")

                CheckAndDeleteFile(Application.StartupPath & "\DATA\KOR\en_us_data.dat")
                CheckAndDeleteFile(Application.StartupPath & "\DATA\KOR\en_us_data.dir")
                CheckAndDeleteFile(Application.StartupPath & "\DATA\KOR\ko_kr_data.dat")
                CheckAndDeleteFile(Application.StartupPath & "\DATA\KOR\ko_kr_data.dir")
                '韩服俄服已死，删除所有备份的语言文件

                CheckAndDeleteFolder(Application.StartupPath & "\DATA\RU")
                CheckAndDeleteFolder(Application.StartupPath & "\DATA\KOR")
                '检查文件夹是否存在&为空，并删除空文件夹
            End If

            CheckAndDeleteFile(Application.StartupPath & "\Font\YDYGO550.ttf")     '新版韩文字体备份
            CheckAndDeleteFile(Application.StartupPath & "\Font\RixMGoM.ttf")      '旧版韩文字体备份
            CheckAndDeleteFile(Application.StartupPath & "\Font\msyh_RixMGoM.ttf")     '韩文+中文合成字体，汉化用
            CheckAndDeleteFile(Application.StartupPath & "\Font\ROSA Verde Normal.ttf")    '俄文字体备份
            CheckAndDeleteFile(Application.StartupPath & "\Font\Geo-Md - RU.ttf")      '俄文客户端英文字体备份
            '删除已弃用韩文字体/俄文字体的字体备份文件

            CheckAndDeleteFolder（"%UserProfile%\AppData\Local\PS2CN_Launcher", False)
            '删除旧版设置，onlyDelEmptyFolder为False，表示需删除非空文件夹

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ClearUnusedFiles Error")   '收集错误信息，建议保留
        End Try
    End Sub

    Private Sub CheckAndDeleteFile(filePath As String)      '检查并删除文件
        Try
            If File.Exists(filePath) Then
                '检查文件是否存在
                '若路径无效/不存在/字符串错误，都返回false；权限不足也返回false

                File.Delete(filePath)
                '删除此文件
                '文件不存在不报错，但是路径不存在会报错…… 必须先检查路径和文件是否存在

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckAndDeleteFile Error")   '收集错误信息，建议保留
        End Try
    End Sub

    Private Sub CheckAndDeleteFolder(folderPath As String, Optional onlyDelEmptyFolder As Boolean = True)
        '检查并删除文件夹，默认isEmpty为True只删除空文件夹，如注明onlyDelEmptyFolder为False，可删除非空文件夹

        Try
            If Directory.Exists(folderPath) Then
                '检查文件夹是否存在

                If onlyDelEmptyFolder Then
                    '若只删除空文件夹，需要继续执行判断，是否为空文件夹

                    Dim fileSystemEntries As String() = Directory.GetFileSystemEntries(folderPath)
                    '读取文件夹内所有文件和文件夹

                    If fileSystemEntries.Length > 0 Then
                        '如果数组长度不为零（空数组），说明文件夹内容非空

                        '判断后不符合执行条件。

                        Return
                        '=exit sub，结束语句。
                    End If
                End If

                Directory.Delete(folderPath)
                '删除此文件夹

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckAndDeleteEmptyFolder Error")   '收集错误信息，建议保留
        End Try
    End Sub

    Private Sub SetClientType()    '根据客户端不同，修改相关内存变量及参数，用于条件判断
        Try
            Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                Case ClientTypeInSettings.STEAM
                    bkDataFolder = Application.StartupPath & "\Data\STEAM"  '设定文本备份文件夹名称
                    bkEXEFolder = Application.StartupPath & "\EXE\STEAM"  '设定exe备份文件夹名称
                    launchPad = New FileInfo(My.Settings.STEAMClientLocation)
                    localeVersionEN = My.Settings.STEAMLocaleVersion
                Case ClientTypeInSettings.EN
                    bkDataFolder = Application.StartupPath & "\Data\EN"  '设定文本备份文件夹名称
                    bkEXEFolder = Application.StartupPath & "\EXE\EN"  '设定exe备份文件夹名称
                    launchPad = New FileInfo(My.Settings.ENClientLocation)
                    localeVersionEN = My.Settings.ENLocaleVersion
                Case ClientTypeInSettings.TEST
                    bkDataFolder = Application.StartupPath & "\Data\TEST"  '设定文本备份文件夹名称
                    bkEXEFolder = Application.StartupPath & "\EXE\TEST"  '设定exe备份文件夹名称
                    launchPad = New FileInfo(My.Settings.TESTClientLLocation)
                    localeVersionEN = My.Settings.TESTLocaleVersion
                Case ClientTypeInSettings.RU
                    bkDataFolder = Application.StartupPath & "\Data\RU"  '设定文本备份文件夹名称
                    bkEXEFolder = Application.StartupPath & "\EXE\RU"  '设定exe备份文件夹名称
                    launchPad = New FileInfo(My.Settings.RUClientLLocation)
                    localeVersionEN = My.Settings.RULocaleVersionEN
                    localeVersionNEN = My.Settings.RULocaleVersionRU
                    dataNameNEN = "\ru_ru_data"
                Case ClientTypeInSettings.KOR
                    bkDataFolder = Application.StartupPath & "\Data\KOR"  '设定文本备份文件夹名称
                    bkEXEFolder = Application.StartupPath & "\EXE\KOR"  '设定exe备份文件夹名称
                    launchPad = New FileInfo(My.Settings.KORClientLLocation)
                    localeVersionEN = My.Settings.KORLocaleVersionEN
                    localeVersionNEN = My.Settings.KORLocaleVersionKOR
                    dataNameNEN = "\ko_kr_data"
            End Select

            If My.Settings.ClientType = ClientTypeInSettings.STEAM Then
                LabelDir.Text = "Steam://rungameid/218230"      '显示Steam启动游戏命令
            Else
                LabelDir.Text = launchPad.FullName      '修改界面上显示的路径

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "SetClientType Error")   '收集错误信息，建议保留
        End Try
    End Sub

    Private Function SetFileAttributes() As Boolean
        '将文件属性设为空（避免只读文件错误）；
        '包含了测试反馈美服客户端位置是否可用(TestDir)
        Try
            Dim PS2CNLauncherFiles = My.Computer.FileSystem.GetFiles(
                    Application.StartupPath _
                    , FileIO.SearchOption.SearchAllSubDirectories
                    )
            For Each PS2CNLauncherFile As String In PS2CNLauncherFiles
                File.SetAttributes(PS2CNLauncherFile, FileAttributes.Normal)
            Next
            '将PS2CN汉化器所有文件设为非只读，非隐藏属性。

            If TestDir(False) Then   '判断游戏客户端路径是否有效，不强制弹出选择路径对话框
                If File.Exists(launchPad.DirectoryName & "\PlanetSide2_x64.exe") Then
                    File.SetAttributes(launchPad.DirectoryName & "\PlanetSide2_x64.exe", FileAttributes.Normal)
                End If
                '将游戏客户端64位程序文件设为非只读，非隐藏属性。

                Dim PS2ClientLocaleFiles = My.Computer.FileSystem.GetFiles(
                    launchPad.DirectoryName & "\Locale" _
                    , FileIO.SearchOption.SearchTopLevelOnly
                    )
                For Each PS2ClientLocaleFile As String In PS2ClientLocaleFiles
                    File.SetAttributes(PS2ClientLocaleFile, FileAttributes.Normal)
                Next
                '将游戏客户端语言文件设为非只读，非隐藏属性。

                Dim PS2ClientFontFiles = My.Computer.FileSystem.GetFiles(
                    launchPad.DirectoryName & "\UI\Resource\Fonts" _
                    , FileIO.SearchOption.SearchTopLevelOnly
                    )
                For Each PS2ClientFontFile As String In PS2ClientFontFiles
                    File.SetAttributes(PS2ClientFontFile, FileAttributes.Normal)
                Next
                '将游戏客户端字体文件设为非只读，非隐藏属性。

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "SetFileAttributes Error")
            Return False
        End Try
    End Function

    Private Function TestDir(Optional needSetDir As Boolean = True) As Boolean       '测试设定的客户端位置是否有效
        '可选参数needSetDir表示是否需要弹出对话框立即选择美服位置，默认为true，若为false，仅执行路径合法性验证。
        Try
            If launchPad IsNot Nothing Then
                '若launchPad尚未定义，则不执行后续判断，避免null出错

                If File.Exists(launchPad.DirectoryName & "\PlanetSide2_x64.exe") _
                    OrElse File.Exists(launchPad.DirectoryName & "\PlanetSide2.exe") _
                    Then
                    '测试客户端位置是否可用
                    Return True
                Else
                    '若客户端位置不可用
                    If needSetDir Then
                        '可选参数needSetDir表示是否需要弹出对话框立即选择美服位置，默认为true，若为false，仅执行路径合法性验证。
                        Dim msgStringTD As String           '消息框显示文字
                        Select Case My.Settings.ClientType
                            Case ClientTypeInSettings.STEAM
                                msgStringTD = "请选择【Steam版】客户端位置。" & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                                "如果使用【其他】版本客户端（测试服），请点击【取消】。" & Chr(13) & Chr(10) &
                                "Steam用户如不清楚游戏安装位置，请【取消】并查看帮助。"
                            Case ClientTypeInSettings.EN
                                msgStringTD = "请选择【官方版】客户端位置。" & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                                "如果使用【其他】版本客户端（测试服），请点击【取消】。" & Chr(13) & Chr(10) &
                                "Steam用户如不清楚游戏安装位置，请【取消】并查看帮助。"
                            Case ClientTypeInSettings.TEST
                                msgStringTD = "请选择【测试服】客户端位置。" & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                                "如果使用【其他】版本客户端（美服），请点击【取消】。"
                            Case ClientTypeInSettings.RU
                                msgStringTD = "请选择【俄服】客户端位置。" & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                                "如果使用【其他】版本客户端（美服、测试服等），请点击【取消】。"
                            Case ClientTypeInSettings.KOR
                                msgStringTD = "请选择【韩服】客户端位置。" & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                                "如果使用【其他】版本客户端（美服、测试服等），请点击【取消】。"
                            Case Else
                                msgStringTD = "请选择游戏客户端位置。" & Chr(13) & Chr(10) & Chr(13) & Chr(10) &
                                "Steam用户如不清楚游戏安装位置，请【取消】并查看帮助。"
                        End Select

                        If MsgBox(msgStringTD,
                              MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2,
                              "选择游戏客户端位置") _
                          = MsgBoxResult.Ok Then
                            '根据用户的“是/否”选择，决定是否打开路径选择窗
                            'msgbox格式值的321为累加计算得出，按钮格式“OK Cancel”=1，info=64，默认按钮2=256
                            SetLaunchPadLocation()   '提示选择客户端路径
                        End If
                    End If

                    Return False
                End If

            Else
                '若launchPad尚未定义，则不执行后续判断，直接返回false，避免null出错
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "TestDir Error")

            If needSetDir AndAlso
                MsgBox("请选择游戏客户端位置。" & Chr(13) & Chr(10) &
                       "Steam用户如不清楚游戏安装位置，请取消并查看帮助。",
                       MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2,
                       "选择游戏客户端位置") _
                   = MsgBoxResult.Ok Then
                '可选参数needSetDir表示是否需要弹出对话框立即选择美服位置，默认为true，若为false，仅执行路径合法性验证。
                '根据用户的“是/否”选择，决定是否打开路径选择窗
                'msgbox格式值的321为累加计算得出，按钮格式“OK Cancel”=1，info=64，默认按钮2=256
                SetLaunchPadLocation()   '提示选择客户端路径
            End If

            Return False
        End Try
    End Function

    Private Function ValidateBackupFontIntegrity(Optional errMsg As Boolean = False) As Boolean
        '验证PS2CN汉化器字体备份文件完整性。
        'errMsg表示是否需要弹出消息框提示出错。
        '返回值True为验证通过，False为文件缺失

        Try
            If Directory.Exists(bkFontFolder) _
                AndAlso File.Exists(bkFontFolder & "\Geo-Md.ttf") _
                AndAlso File.Exists(bkFontFolder & "\simhei.ttf") _
                Then
                '验证备份字体文件夹及备份字体是否存在

                'AndAlso File.Exists(bkFontFolder & "\Geo-Md - RU.ttf") _
                'AndAlso File.Exists(bkFontFolder & "\ROSA Verde Normal.ttf") _
                '俄服已关服，删除俄服英文/俄文字体

                'AndAlso File.Exists(bkFontFolder & "\YDYGO550.ttf") _
                'AndAlso File.Exists(bkFontFolder & fontCNKOR) _
                '韩服已关服，删除中韩合成字体

                Return True
            Else
                If errMsg Then
                    MsgBox(
                    "汉化器所需文件缺失！" & Chr(13) & Chr(10) &
                    "请点击左上方图标，访问PS2CN汉化器发布页，下载最新完整版汉化器。"
                   )
                End If

                Return False

            End If

        Catch exDirectoryNotFound As DirectoryNotFoundException
            If errMsg Then
                MsgBox(
                "汉化器所需文件缺失！" & Chr(13) & Chr(10) &
                "请点击左上方图标，访问PS2CN汉化器发布页，下载最新完整版汉化器。" _
                & Chr(13) & Chr(10) & Chr(13) & Chr(10) & exDirectoryNotFound.Message _
                , MsgBoxStyle.Critical, "ValidateProgramIntegrity Error"
                )
            End If

            Return False

        Catch exFileNotFound As FileNotFoundException
            If errMsg Then
                MsgBox(
                "汉化器所需文件缺失！" & Chr(13) & Chr(10) &
                "请点击左上方图标，访问PS2CN汉化器发布页，下载最新完整版汉化器。" _
                & Chr(13) & Chr(10) & Chr(13) & Chr(10) & exFileNotFound.Message _
                , MsgBoxStyle.Critical, "ValidateProgramIntegrity Error")
            End If

            Return False

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ValidateProgramIntegrity Error")
            Return False
        End Try
    End Function

    Private Sub BuildComboBoxFontSelectorList()
        '重新读取自定义字体文件夹中的字体文件，生成新的下拉菜单，并将text设为保存的设置值

        Try
            ComboBoxFontSelector.Items.Clear()
            '清空下拉菜单所有项

            ComboBoxFontSelector.Items.Add(useDefaultFont)
            '添加默认项

            ComboBoxFontSelector.Items.Add(useChineseFont)
            '添加使用中文黑体选项

            If My.Computer.FileSystem.DirectoryExists(Application.StartupPath & customFontDIR) Then
                '当自定义字体文件夹路径存在时

                Dim customFontFiles = My.Computer.FileSystem.GetFiles(
                    Application.StartupPath & customFontDIR _
                    , FileIO.SearchOption.SearchTopLevelOnly _
                    , "*.ttf"
                    )
                '列出自定义字体文件夹下所有ttf字体文件，仅搜索首层，不搜索子文件夹。
                '不可搜索子文件夹，因为在重新生成字体全路径时只带自定义字体文件夹主路径，无法记录子文件夹路径。

                For Each customFontFile As String In customFontFiles
                    '根据各个字体路径，创建临时FiloInfo类，然后取文件名属性，不带扩展名。

                    Dim customFontFileInfo As FileInfo = New FileInfo(customFontFile)

                    ComboBoxFontSelector.Items.Add(customFontFileInfo.Name)
                    '将文件名添加入Combobox下拉菜单
                Next
            End If

            ComboBoxFontSelector.Text = ValidateAndSetFontSelection(My.Settings.FontSelector)
            '当设置中的字体文件存在时，将combobox的text设为该值。否则设为默认

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxFontSelector_DropDown Error")
        End Try
    End Sub

    Private Sub SetLaunchPadLocation()
        Try
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then     '弹出文件路径选择窗，判断用户已选择客户端汉化器exe文件路径
                launchPad = New FileInfo(OpenFileDialog1.FileName)      '将用户选择的路径记录为fileinfo以备使用
                LabelDir.Text = launchPad.FullName      '显示用户选择的路径

                '将用户选择的路径，保存到设定文件
                Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                    Case ClientTypeInSettings.STEAM
                        My.Settings.STEAMClientLocation = launchPad.FullName
                    Case ClientTypeInSettings.EN
                        My.Settings.ENClientLocation = launchPad.FullName
                    Case ClientTypeInSettings.TEST
                        My.Settings.TESTClientLLocation = launchPad.FullName
                    Case ClientTypeInSettings.RU
                        My.Settings.RUClientLLocation = launchPad.FullName
                    Case ClientTypeInSettings.KOR
                        My.Settings.KORClientLLocation = launchPad.FullName
                End Select
                'My.Settings.Save()
            End If
        Catch ex As Exception
            LabelDir.Text = ex.Message  '如出错，显示出错信息
        End Try
    End Sub

    Private Sub SelectIP()  '根据设置值，将服务器IP定义到字符串serverIP中
        Try
            Select Case ComboBoxServer.SelectedIndex   '设置不为0，则将服务器相应IP赋值给字符串
                Case 0      '如果测ping设置为0，则为禁用
                    serverIP = "0"      '0作为判断，表示禁用测试
                    LabelPing.Text = "已禁用"  '清除labelPing的显示

                    PanelPingTester.Visible = False     '隐藏测ping面板

                Case 1      'Connery
                    serverIP = "64.37.174.141"
                Case 2      'Emerald, was Mattherson
                    serverIP = "69.174.216.27"
                Case 3      'Briggs
                    serverIP = "69.174.220.22"
                Case 4      'Cobalt
                    serverIP = "69.174.194.168"
                Case 5      'Miller
                    serverIP = "69.174.194.166"
                    'Case 6      'Russia Briggs 俄服已关闭
                    '    serverIP = "109.105.156.172"
                Case Else   '防意外
                    serverIP = "0"      '0作为判断，表示禁用测试
                    LabelPing.Text = "已禁用"  '清除labelPing的显示
            End Select

            If ComboBoxServer.SelectedIndex <> 0 Then
                PanelPingTester.Visible = True     '显示测ping面板
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "SelectIP Error")
        End Try
    End Sub

    Private Async Sub CheckVersion()
        '检查版本更新，Async表示异步方法，其中包含Await运算符

        'Dim verResponse As WebResponse = Nothing

        Dim verHttpClient As New HttpClient

        Dim verStream As Stream = Nothing
        Dim verStreamReader As StreamReader = Nothing
        Try
            'Dim verRequest As WebRequest = WebRequest.Create("http://tieba.baidu.com/p/3594739603")   ' Create a request for the URL. 查询最新版本号
            ''verRequest.Timeout = 10000        '10000毫秒为超时时间，异步无效
            'verResponse = Await verRequest.GetResponseAsync()            ' Async get the response. Return Task(of WebResponse)
            'verStream = verResponse.GetResponseStream()   ' Get the stream containing content returned by the server.

            Dim verHttpTimeout As New TimeSpan(0, 0, 15)     '设定TimeSpan格式，0时0分10秒
            verHttpClient.Timeout = verHttpTimeout            '将HttpClient的超时时间设为上行的值

            verHttpClient.DefaultRequestHeaders.Add("User-Agent", "Chrome")

            verStream = Await verHttpClient.GetStreamAsync("http://tieba.baidu.com/p/3594739603")
            ' Get the stream containing content returned by the server.
            verStreamReader = New StreamReader(verStream, System.Text.Encoding.GetEncoding("utf-8"))
            ' Open the stream using a StreamReader for easy access.
            '重要！必须使用GetStream + StreamReader指定编码。若直接用GetStringAsync的话无法指定编码，中文乱码！
            Dim verResponseString As String = Await verStreamReader.ReadToEndAsync()   ' Read the content.

            Dim latestVersion As UInt32 = 0    '最新版版本号

            '百度贴吧版本更新帖网页格式如下：
            '2楼内容例：版本号20150220。
            '此处以“版本号”作为搜索符。“版本号”之后，第一个“2”起始，查找8位字符（数字）。
            If verResponseString.Contains("版本号") Then
                verResponseString = verResponseString.Remove(0, verResponseString.IndexOf("版本号") + 3)
                latestVersion = Val(verResponseString.Substring(verResponseString.IndexOf("2"), 8))
                If latestVersion > Version Then

                    LauncherForm1ShowTitle("有更新")
                    '修改主窗体标题栏，更新版本状态

                    LabelUpdate.Text = "汉化器版本有更新！最新版：" & latestVersion & "。" _
                    & Chr(13) & Chr(10) & "点击访问汉化发布页。"
                    'LabelUpdate.Text = "汉化器版本有更新！最新版：" _
                    '& IIf(1, verResponseString.Substring(verResponseString.IndexOf("。<br>") + 5, verResponseString.IndexOf("<")), "") _
                    '& Chr(13) & Chr(10) _
                    '& "点此访问汉化更新发布页。"
                Else

                    LauncherForm1ShowTitle("最新版")
                    '修改主窗体标题栏，更新版本状态

                    'LabelUpdate.Text = "汉化器已是最新版本。"
                End If
            End If

            ToolTip1.SetToolTip(LabelUpdate, "点击访问汉化发布页。")

            verResponseString = Nothing     '清空字符串，释放内存。

        Catch ex As Exception
            Try
                If ex.Message.Contains("已取消一个任务。") _
                    Or ex.Message.Contains("A task was canceled.") _
                    Then

                    LauncherForm1ShowTitle()
                    '修改主窗体标题栏，清空版本状态

                    'LabelUpdate.Text = "版本更新查询超时。点击可重新查询。"
                Else
                    If ex.InnerException Is Nothing Then
                        '若包含InnerException异常，则显示详细信息

                        LauncherForm1ShowTitle()
                        '修改主窗体标题栏，清空版本状态

                        'LabelUpdate.Text = "版本更新查询出错。点击可重新查询。" & Chr(13) & Chr(10) & ex.Message
                    Else

                        LauncherForm1ShowTitle()
                        '修改主窗体标题栏，清空版本状态

                        'LabelUpdate.Text = "版本更新查询出错。点击可重新查询。" & Chr(13) & Chr(10) & ex.InnerException.Message
                    End If
                End If
                ToolTip1.SetToolTip(LabelUpdate, "点击可重新查询。")
            Catch exINex As Exception
                MsgBox(exINex.Message, MsgBoxStyle.Exclamation, "CheckVersionException Error")
            End Try
        Finally
            If verStreamReader IsNot Nothing Then verStreamReader.Dispose()
            If verStream IsNot Nothing Then verStream.Dispose()

            If verHttpClient IsNot Nothing Then verHttpClient.Dispose()

            'If verResponse IsNot Nothing Then verResponse.Dispose()

            '清空缓存，重新获取网页信息？断网时会触发null错误
            '虽然貌似close也会调用dispose，但是实测单独使用close无法刷新，只能用dispose？
            '调用Close函数释放资源后可能还需要再次使用，而Dispose函数释放的资源不再使用？
        End Try
    End Sub

    Private Sub RunOrShowButton(Optional runButton As Boolean = False)
        '更新显示按钮状态，False表示显示按钮状态，True表示点击执行功能
        Try
            If TestDir(False) Then
                '判断路径是否正确设置，是则继续判断，否则显示【设置路径】

                If GameNotRunning() Then
                    '判断游戏是否未运行，如不在运行则继续判断，否则将按钮设为【正在游戏】

                    If GameRunningStatSwitch = True Then
                        '当游戏结束运行瞬间，（由运行状态转入停止状态），恢复显示汉化器窗体
                        Me.WindowState = FormWindowState.Normal
                    End If
                    GameRunningStatSwitch = False
                    '将状态切换标识符设为停止状态

                    If My.Settings.ClientType <> ClientTypeInSettings.RU _
                        AndAlso LaunchPadRunning() _
                        Then
                        '1、先判断是否非俄服，如是则跳过下一判断；
                        '2、判断登录器是否运行，如正在运行则按钮设为【开始修改】/【进入游戏】

                        If My.Settings.CheckedCN = ModifiedLocale(True) _
                            AndAlso My.Settings.Checked32bit = ModifiedX64() _
                            AndAlso IsSelectedFont() _
                            Then
                            '判断“是否汉化”与客户端文件修改状态是否相同；
                            '判断32位客户端选项及文件状态是否相同；
                            '判断字体是否为已修改的；

                            RunButtonStatus(ButtonStatus.Start)         '登录器修改完成，按钮显示为【进入游戏】

                            If runButton Then
                                ActivateLauncher()          '切换到客户端登录器窗口
                            End If
                        Else
                            RunButtonStatus(ButtonStatus.Modify)        '可以直接修改，则按钮变为【开始修改】

                            If runButton Then

                                'UpdateBackup()      '登录器更新完成后，点击按钮都要执行检查备份操作，如有更新，则替换掉备份文件。
                                '将备份客户端移到RunModify之中，确认对话框出现之后才执行备份。

                                RunModify(False)         '执行修改
                            End If
                        End If

                    Else
                        '如登录器不在运行，或客户端为俄服，进行下一步判断

                        If Not ManuallyTranslated _
                            AndAlso
                            (ModifiedFont() _
                            OrElse ModifiedLocale(False) _
                            OrElse ModifiedX64()
                            ) _
                            Then
                            '判断是否可以【启动登录器】，还是需要【恢复备份】

                            '判断是否手动修改过。仅当本次运行中从未手动修改时，才继续判断是否恢复备份
                            '判断字体是否被修改过。若修改过，就需要恢复备份，无需后续判断
                            '判断语言文本是否被修改过(是否需要恢复备份)。若修改过，就需要恢复备份，无需后续判断；
                            'noNeedRestore为false表示需要先判断备份文件是否合法，用于判断是否需要恢复备份；true表示仅判断客户端是否修改
                            '判断64位是否被修改过。若修改过，需要恢复备份

                            RunButtonStatus(ButtonStatus.Backup)        '需要先恢复备份，按钮显示为【恢复备份】


                            If (My.Settings.CheckedAutoRestore OrElse runButton) _
                                AndAlso SetFileAttributes() _
                                Then
                                '如果勾选了自动恢复备份，则每次调用本函数自动判断时都自动【恢复备份】
                                '如果前项没有勾选，则当【判断并执行】条件runButton为True时，才执行【恢复备份】
                                '前两项判断通过后，SetFileAttributes将文件属性设为普通（避免只读文件错误），其中包含客户端位置验证TestDir(False)；

                                '【恢复备份】恢复客户端原始文件，用以通过校验节省流量。

                                Dim restoreSuccess As Byte = 0
                                '设定变量，统计恢复备份是否成功

                                If ModifiedLocale(False) Then
                                    '判断语言文本是否被修改过(是否需要恢复备份)
                                    'noNeedRestore为false表示需要先判断备份文件是否合法，用于判断是否需要恢复备份；true表示仅判断客户端是否修改

                                    If RestoreLocale() = "成功取消汉化，已恢复为备份文件。" Then     '1、恢复语言文本；
                                        restoreSuccess += 1
                                    End If
                                End If

                                If ModifiedFont() Then
                                    '判断字体是否被修改过

                                    If RestoreFont() = "成功恢复原始字体。" Then       '2、恢复字体；
                                        restoreSuccess += 1
                                    End If
                                End If

                                '使用32位客户端功能关闭，俄服只有32位，美服只有64位……
                                'If ModifiedX64() Then
                                '    '判断64位是否被修改过
                                '    RestoreX64()        '3、恢复64位备份(自带判断是否俄服，俄服则无需恢复64位；)
                                'End If

                                If restoreSuccess >= 2 Then
                                    RunOrShowButton(False)
                                    '如果前两项恢复操作都成功，则再次执行判断，确定按钮状态
                                    '增加状态判断，是为了避免当勾选自动恢复 + 恢复备份出错时，仍然不断反复调用自身导致卡死
                                    '不直接设置按钮状态，见下一段落代码，是因为俄服美服启动方式不同，没必要重新复制下方大段代码

                                    restoreSuccess = 0      '避免变量释放出错，手动清零
                                End If

                            End If

                        Else
                            '经判断，无需要恢复备份

                            If My.Settings.ClientType = ClientTypeInSettings.RU Then   '判断是否俄服（、韩服×），无需【启动登录器】，按钮变为【开始修改】
                                RunButtonStatus(ButtonStatus.Modify)        '可以直接修改，则按钮变为【开始修改】

                                If runButton Then

                                    QueryIPLoc()        '查询IP归属。参数为默认值不做设定，表示距上次查询已过了最大查询间隔

                                    'UpdateBackup()      '登录器更新完成后，点击按钮都要执行检查备份操作，如有更新，则替换掉备份文件。
                                    '将备份客户端移到RunModify之中，确认对话框出现之后才执行备份。

                                    RunModify(False)         '执行修改

                                End If
                            Else
                                RunButtonStatus(ButtonStatus.Launch)    '可直接启动，按钮显示为【启动登录器】

                                If runButton Then

                                    QueryIPLoc()        '查询IP归属。参数为默认值不做设定，表示距上次查询已过了最大查询间隔

                                    StartClient()   '启动游戏客户端登录器

                                    'If My.Settings.ClientType = ClientTypeInSettings.STEAM _
                                    '    OrElse My.Settings.ClientType = ClientTypeInSettings.EN _
                                    '    OrElse My.Settings.ClientType = ClientTypeInSettings.TEST Then
                                    '    '当选择美服或测试服，查询美服状态

                                    '    QueryServerStatus()     '查询美服状态
                                    'End If

                                    Select Case My.Settings.ClientType
                                        Case ClientTypeInSettings.STEAM, ClientTypeInSettings.EN, ClientTypeInSettings.TEST
                                            '当选择美服或测试服，查询美服状态
                                            QueryServerStatus()     '查询美服状态
                                    End Select

                                End If
                            End If
                        End If
                    End If
                Else        '游戏正在运行
                    RunButtonStatus(ButtonStatus.Playing)       '游戏正在运行，按钮设为【正在游戏】

                    If GameRunningStatSwitch = False Then
                        '当游戏开始运行瞬间，（由停止状态转入运行状态），将汉化器窗体最小化
                        Me.WindowState = FormWindowState.Minimized
                    End If
                    GameRunningStatSwitch = True
                    '将状态切换标识符设为运行中

                    'If runButton Then
                    '    KillGameClient()       '弹出对话框请用户确认，是否需要结束游戏程序。此功能从按钮分离，左侧单独做黄色按钮
                    '    'ActivateGameClient()    '激活游戏窗口。只能激活焦点，但最小化的窗口无法还原，功能鸡肋，舍弃。
                    'End If
                End If
            Else
                '游戏路径尚未选择
                If My.Settings.ClientType = ClientTypeInSettings.STEAM Then
                    '判断是否为Steam版客户端

                    If launchedFromSteam Then
                        '判断是否已由Steam://rungameid启动
                        '需将内存中登录器的路径，记录到设置里

                        SetLaunchPadLoc()   '检测内存中运行的登录器，将路径记录到设置中

                    Else
                        '没有通过Steam://rungameid启动
                        RunButtonStatus(ButtonStatus.Launch)    '可直接启动，按钮显示为【启动登录器】

                        If runButton Then

                            QueryIPLoc()        '查询IP归属。参数为默认值不做设定，表示距上次查询已过了最大查询间隔

                            StartClient()   '启动游戏客户端登录器

                            QueryServerStatus()     '查询美服状态

                            launchedFromSteam = True    '由Steam启动登录器，接下来需将内存中登录器的路径，记录到设置里

                        End If

                    End If

                Else
                    RunButtonStatus(ButtonStatus.SetLocation)   '游戏路径尚未选择，按钮显示为【设定路径】

                    If runButton Then
                        SetLaunchPadLocation()  '提示选择客户端路径
                    End If

                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "RunOrShowButton Error")
        End Try
    End Sub

    Private Sub RunButtonStatus(runStatus As ButtonStatus) '判断登录器是否在运行，相应修改run按钮状态
        Try
            Select Case runStatus
                Case ButtonStatus.SetLocation   '游戏路径尚未选择，按钮显示为【设定路径】
                    LabelRun.Text = "设定路径"
                    LabelStatus.Text = "先选择客户端类型" & Chr(13) & Chr(10) & "再选择exe程序位置。"
                    ToolTip1.SetToolTip(LabelRun, "")
                    LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                    LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                    LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能

                Case ButtonStatus.Backup        '登录器尚未运行，按钮显示为【恢复备份】
                    If My.Settings.ClientType = ClientTypeInSettings.RU Then
                        LabelRun.Text = "恢复备份"
                        LabelStatus.Text = "恢复备份文件" & Chr(13) & Chr(10) & "若修改成功,请返回网页开始游戏。"
                        ToolTip1.SetToolTip(LabelRun, "若修改成功，请回到网页，点击【Играть】开始游戏。")
                        LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                        LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                        LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                    Else
                        LabelRun.Text = "恢复备份"
                        LabelStatus.Text = "恢复备份文件" & Chr(13) & Chr(10) & "避免登录器下载修复客户端" & Chr(13) & Chr(10) & "节省流量。"
                        ToolTip1.SetToolTip(LabelRun, "")
                        LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                        LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                        LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                    End If

                Case ButtonStatus.Launch        '登录器尚未运行，按钮显示为【启动登录器】
                    Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                        Case ClientTypeInSettings.RU      '俄服
                            LabelStatus.Text = "请在网页上选择登录账号" & Chr(13) & Chr(10) & "等到出现【Играть】按钮" & Chr(13) & Chr(10) & "然后点击【开始修改】"
                            ToolTip1.SetToolTip(LabelRun, "修改成功后,回到游戏登录器，点【Играть】开始游戏。")
                            LabelManuallyTranslate.Visible = True      '仅当读不到登录器时，才显示手动修改功能
                            LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                            LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                        Case ClientTypeInSettings.KOR     '韩服
                            LabelRun.Text = "打开登录页"
                            LabelStatus.Text = "打开网页登录账号" & Chr(13) & Chr(10) & "然后点【Game Start】启动登录器"
                            ToolTip1.SetToolTip(LabelRun, "登录器更新完成后，返回本程序开始修改。")
                            LabelManuallyTranslate.Visible = True      '仅当读不到登录器时，才显示手动修改功能
                            LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                            LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                        Case Else               '美服Steam版、官方版及测试服
                            LabelRun.Text = "启动登录器"
                            LabelStatus.Text = "启动游戏登录器" & Chr(13) & Chr(10) & "可从外部或Steam直接启动。"
                            ToolTip1.SetToolTip(LabelRun, "")
                            LabelManuallyTranslate.Visible = True      '仅当读不到登录器时，才显示手动修改功能
                            LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                            LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                    End Select

                Case ButtonStatus.Modify        '登录器正在运行，按钮显示为【开始修改】
                    LabelRun.Text = "开始修改"
                    Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                        Case ClientTypeInSettings.RU      '俄服
                            LabelStatus.Text = "请在网页上登录账号" & Chr(13) & Chr(10) & "等按钮变为【Играть】" & Chr(13) & Chr(10) & "再点击↓【开始修改】↓"
                            ToolTip1.SetToolTip(LabelRun, "修改成功后，请回到网页，点【Играть】开始游戏。")
                            LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                            LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                            LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                        Case ClientTypeInSettings.KOR     '韩服
                            LabelStatus.Text = "网页上登录账号,打开登录器" & Chr(13) & Chr(10) & "等按钮变为【게임실행】" & Chr(13) & Chr(10) & "再点击↓【开始修改】↓"
                            ToolTip1.SetToolTip(LabelRun, "修改成功后，请回到游戏登录器，点【게임실행】开始游戏。")
                            LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                            LabelKillLaunchPad.Visible = True            '仅当登录器运行时，才显示关闭登录器功能
                            LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                        Case Else               '美服Steam版、官方版及测试服
                            LabelStatus.Text = "请在登录器登录账号,等按钮变为" & Chr(13) & Chr(10) & "【Play】,且绿色进度条填满后" & Chr(13) & Chr(10) & "再点击↓【开始修改】↓"
                            ToolTip1.SetToolTip(LabelRun, "修改成功后，请回到游戏登录器，点【Play】开始游戏。")
                            LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                            LabelKillLaunchPad.Visible = True           '仅当登录器运行时，才显示关闭登录器功能
                            LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                    End Select

                Case ButtonStatus.Start         '登录器修改完成，按钮显示为【进入游戏】
                    LabelRun.Text = "进入游戏"
                    Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                        Case ClientTypeInSettings.KOR     '韩服
                            LabelStatus.Text = "修改成功！" & Chr(13) & Chr(10) & "请回到游戏登录器" & Chr(13) & Chr(10) & "点击【게임실행】,开始游戏。"
                            ToolTip1.SetToolTip(LabelRun, "点【게임실행】开始游戏。")
                            LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                            LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                            LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                        Case Else               '美服Steam版、官方版及测试服
                            LabelStatus.Text = "修改成功！" & Chr(13) & Chr(10) & "请回到游戏登录器" & Chr(13) & Chr(10) & "点击【Play】,开始游戏。"
                            ToolTip1.SetToolTip(LabelRun, "点【Play】开始游戏。")
                            LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                            LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                            LabelKillGameClient.Visible = False         '仅当游戏运行时，才显示关闭游戏进程功能
                    End Select

                Case ButtonStatus.Playing       '游戏正在运行，按钮显示为【正在游戏】
                    LabelRun.Text = "正在游戏"
                    LabelStatus.Text = "游戏正在运行。" & Chr(13) & Chr(10) & "可关闭汉化器，" & Chr(13) & Chr(10) & "下次游戏前开启即可。"
                    ToolTip1.SetToolTip(LabelRun, "")
                    LabelManuallyTranslate.Visible = False      '仅当读不到登录器时，才显示手动修改功能
                    LabelKillLaunchPad.Visible = False          '仅当登录器运行时，才显示关闭登录器功能
                    LabelKillGameClient.Visible = True          '仅当游戏运行时，才显示关闭游戏进程功能

            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "RunButtonStatus Error")
        End Try
    End Sub

    Private Function GameNotRunning() As Boolean
        Try
            Dim pRunning As Boolean = True

            Dim pList64() As Process = Process.GetProcessesByName("PlanetSide2_x64")  '获取游戏程序系统进程列表

            For Each gameProcess In pList64
                '遍历每一个登录器系统进程
                If gameProcess.Path.Equals(
                    launchPad.DirectoryName & "\PlanetSide2_x64.exe",
                    StringComparison.OrdinalIgnoreCase) Then
                    '如果进程主模块的路径文件名，就是当前选定的客户端类型下，游戏程序的完整文件名
                    '内存中查到的模块路径可能大小写不同…… 比较时忽略大小写

                    '引入查询进程文件路径的模块QueryFullProcessImageName，
                    '在vista以后的操作系统中，用自定义的函数process.path代替process.MainModule.FileName

                    pRunning = False    '则表示当前选定的客户端已启动，返回False
                    Exit For            '结束遍历

                End If
            Next

            '亚洲3服已死，再无32位客户端
            'Dim pList() As Process = Process.GetProcessesByName("PlanetSide2")  '获取系统进程列表
            'For Each gameProcess In pList
            '    pRunning = False
            '    Exit For
            'Next

            Return pRunning

        Catch ex32 As ComponentModel.Win32Exception
            Return False
            '若在编译-目标CPU选项中，选择了Any CPU并首选32位，则32位汉化器无法访问64位游戏的模块。“32位进程无法访问64位进程的模块。”
            '但实际上忽略32位程序的出错提示后，程序是可以正常运行的。
            '关于编译选项，参见 https://msdn.microsoft.com/zh-cn/library/kb4wyys2.aspx

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GameNotRunning Error")  '显示错误信息
            Return False
        End Try
    End Function

    Private Function LaunchPadRunning() As Boolean
        '查询Launcher进程，判断返回launcher是否启动。
        Try
            Dim pRunning As Boolean = False
            Dim launchPadExeName As String = "LaunchPad"
            Dim pList() As Process

            If My.Settings.ClientType = ClientTypeInSettings.KOR Then '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                launchPadExeName = "Launcher"      '设置为韩服登录器名称
            Else
                launchPadExeName = "LaunchPad"     '设置为美服登录器名称
            End If

            pList = Process.GetProcessesByName(launchPadExeName)     '获取登录器系统进程列表




            'Test code 测试代码

            LabelTestDisplay1.Text = "设定路径- " & launchPad.DirectoryName & "\" & launchPadExeName & ".exe"
            LabelTestDisplay2.Text = ""

            'Test code 测试代码




            For Each launchProcess In pList
                '遍历每一个登录器系统进程




                'Test code 测试代码

                LabelTestDisplay2.Text += "内存路径- " & launchProcess.MainModule.FileName & Chr(13) & Chr(10)

                'Test code 测试代码




                If launchProcess.Path.Equals(
                    launchPad.DirectoryName & "\" & launchPadExeName & ".exe",
                    StringComparison.OrdinalIgnoreCase) Then
                    '如果进程主模块的含路径文件名，就是当前选定的客户端类型下，登录器的完整文件名
                    '内存中查到的路径可能大小写不同…… 比较时忽略大小写

                    '引入查询进程文件路径的模块QueryFullProcessImageName，
                    '在vista以后的操作系统中，用自定义的函数process.path代替process.MainModule.FileName

                    pRunning = True     '则表示当前选定的客户端已启动，返回True
                    Exit For            '结束遍历
                End If

                If launchProcess.MainModule.FileName.Equals(
                    launchPad.DirectoryName & "\" & launchPadExeName & ".exe",
                    StringComparison.OrdinalIgnoreCase) Then
                    '如果进程主模块的含路径文件名，就是当前选定的客户端类型下，登录器的完整文件名
                    '内存中查到的路径可能大小写不同…… 比较时忽略大小写

                    '有人反映新的查询路径模块QueryFullProcessImageName无法找到路径，故保留旧版判断函数作为冗余备份

                    pRunning = True     '则表示当前选定的客户端已启动，返回True
                    Exit For            '结束遍历
                End If

            Next

            Return pRunning

        Catch ex32 As ComponentModel.Win32Exception
            Return False
            '若在编译-目标CPU选项中，选择了Any CPU并首选32位，则32位汉化器无法访问64位游戏的模块。“32位进程无法访问64位进程的模块。”
            '但实际上忽略32位程序的出错提示后，程序是可以正常运行的。
            '关于编译选项，参见 https://msdn.microsoft.com/zh-cn/library/kb4wyys2.aspx

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LaunchPadRunning Error")  '显示错误信息
            Return False
        End Try
    End Function

    Private Sub SetLaunchPadLoc()
        '通过Steam命令启动Launcher时，查询进程，判断返回launcher是否启动，
        '并将运行中的Launcher路径，保存到设置中。
        Try
            Dim pList() As Process

            pList = Process.GetProcessesByName("LaunchPad")     '获取登录器系统进程列表

            For Each launchProcess In pList
                '遍历每一个登录器系统进程

                If launchPad Is Nothing Then
                    '若launchPad尚未定义，则不执行后续判断，避免null出错

                    launchPad = New FileInfo(launchProcess.Path)      '将内存中登录器的路径记录为fileinfo以备使用
                    '在vista以后的操作系统中，用自定义的函数process.path代替process.MainModule.FileName
                    launchPad = New FileInfo(launchProcess.MainModule.FileName)      '将内存中登录器的路径记录为fileinfo以备使用
                    '有人反映新的查询路径模块QueryFullProcessImageName无法找到路径，故保留旧版判断函数作为冗余备份

                Else
                    If launchProcess.Path.IndexOf("Steam", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        '字符串包含用String.IndexOf函数，可选参数忽略大小写，Contains函数无法指定大小写
                        'IndexOf不包含时返回-1

                        launchPad = New FileInfo(launchProcess.Path)      '将内存中登录器的路径记录为fileinfo以备使用
                        '在vista以后的操作系统中，用自定义的函数process.path代替process.MainModule.FileName
                    End If

                    If launchProcess.MainModule.FileName.IndexOf("Steam", StringComparison.OrdinalIgnoreCase) >= 0 Then
                        '字符串包含用String.IndexOf函数，可选参数忽略大小写，Contains函数无法指定大小写
                        'IndexOf不包含时返回-1

                        launchPad = New FileInfo(launchProcess.MainModule.FileName)      '将内存中登录器的路径记录为fileinfo以备使用
                        '有人反映新的查询路径模块QueryFullProcessImageName无法找到路径，故保留旧版判断函数作为冗余备份
                    End If

                End If

            Next

            My.Settings.STEAMClientLocation = launchPad.FullName

        Catch ex32 As ComponentModel.Win32Exception
            '若在编译-目标CPU选项中，选择了Any CPU并首选32位，则32位汉化器无法访问64位游戏的模块。“32位进程无法访问64位进程的模块。”
            '但实际上忽略32位程序的出错提示后，程序是可以正常运行的。
            '关于编译选项，参见 https://msdn.microsoft.com/zh-cn/library/kb4wyys2.aspx

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "SetLaunchPadLoc Error")  '显示错误信息

        End Try

    End Sub

    Private Function ModifiedLocale(Optional noNeedRestore As Boolean = False) As Boolean
        '判断语言文本是否被修改过(是否需要恢复备份)
        'noNeedRestore为false表示需要先判断备份文件是否合法，用于判断是否需要恢复备份；true表示仅判断客户端是否修改
        Try
            Dim returnModifiedLocale As Boolean = False      '设定临时返回值变量，默认为false

            If noNeedRestore OrElse File.Exists(bkDataFolder & "\en_us_data.dat") _
                AndAlso Not _
                File.GetLastWriteTime(bkDataFolder & "\en_us_data.dat").ToString.
                Equals(localeVersionEN) _
                Then
                '根据输入参数，决定是否需要先判断备份文件合法
                '判断备份文件是否存在，然后判断备份文件是否合法（并非被程序修改过的汉化文件）
                '若上述任一不符, 直接返回false表示未修改（无须还原）

                If File.Exists(launchPad.DirectoryName & "\Locale\en_us_data.dat") Then
                    '判断原始文件是否存在，若不存在，直接返回true表示已修改（须恢复备份）

                    If File.GetLastWriteTime(launchPad.DirectoryName & "\Locale\en_us_data.dat").ToString.Equals _
                        (localeVersionEN) Then
                        '判断语言文本修改时间是否为上次汉化；
                        returnModifiedLocale = True    '返回true表示已修改（须恢复备份）
                    End If
                Else    '判断原始文件是否存在，若不存在，直接返回true表示已修改（须恢复备份）
                    returnModifiedLocale = True
                End If
            Else
                returnModifiedLocale = False    '若if判断任一不符, 直接返回false表示未修改（无须还原）
            End If

            If Not returnModifiedLocale _
                AndAlso (
                My.Settings.ClientType = ClientTypeInSettings.RU _
                OrElse My.Settings.ClientType = ClientTypeInSettings.KOR
                ) Then
                '判断俄服韩服的第二语言
                If noNeedRestore OrElse File.Exists(bkDataFolder & dataNameNEN & ".dat") _
                    AndAlso Not File.GetLastWriteTime(bkDataFolder & dataNameNEN & ".dat").ToString.Equals _
                    (localeVersionNEN) Then
                    '判断备份文件是否存在，然后判断备份文件是否合法（并非被程序修改过的汉化文件）
                    '若上述任一不符, 直接返回false表示未修改（无须还原）

                    If Not returnModifiedLocale AndAlso File.Exists(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dat") Then
                        '判断原始文件是否存在，若不存在，直接返回true表示已修改（须恢复备份）

                        If File.GetLastWriteTime(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dat").ToString.Equals _
                            (localeVersionNEN) Then
                            '判断语言文本修改时间是否为上次汉化；
                            returnModifiedLocale = True    '返回true表示已修改（须恢复备份）
                        End If
                    Else    '判断原始文件是否存在，若不存在，直接返回true表示已修改（须恢复备份）
                        returnModifiedLocale = True
                    End If
                Else
                    returnModifiedLocale = False    '若if判断任一不符, 直接返回false表示未修改（无须还原）
                End If
            End If

            Return returnModifiedLocale
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ModifiedLocale Error")  '显示错误信息
            Return False
        End Try
    End Function

    Private Function ModifiedFont() As Boolean      '判断字体是否被修改过
        '默认出错返回false未被修改，无需还原，让客户端更新以修复可能的错误
        Try
            If ValidateBackupFontIntegrity(False) Then
                '验证PS2CN汉化器核心文件完整性，参数false表示文件缺失时无需弹出对话框提示；

                Select Case My.Settings.ClientType
                        '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                    Case ClientTypeInSettings.STEAM, ClientTypeInSettings.EN, ClientTypeInSettings.TEST
                        '为美服或测试服，判断英文字体
                        If CheckSameFile(
                                bkFontFolder & "\Geo-Md.ttf" _
                                , launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf"
                                ) _
                                Then
                            '判断英字体是否存在，否则返回true修改过
                            '判断英字体是否与原始字体相同，否则返回true修改过
                            '默认出错返回false不同，无需还原，让客户端更新以修复可能的错误
                            Return False    '返回false未修改
                        Else
                            Return True     '返回true修改过
                        End If

                    Case ClientTypeInSettings.RU
                        '为俄服，判断英俄字体
                        If CheckSameFile(
                                bkFontFolder & "\Geo-Md - RU.ttf" _
                                , launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf"
                                ) _
                                Then
                            '判断英字体是否存在，否则返回true修改过
                            '判断英字体是否与原始字体相同，否则返回true修改过
                            '默认出错返回false不同，无需还原，让客户端更新以修复可能的错误

                            If CheckSameFile(
                                bkFontFolder & "\ROSA Verde Normal.ttf" _
                                , launchPad.DirectoryName & "\UI\Resource\Fonts\ROSA Verde Normal.ttf"
                                ) _
                                Then
                                '判断俄字体是否存在，否则返回true修改过
                                '判断俄字体是否与原始字体相同，否则返回true修改过
                                '默认出错返回false不同，无需还原，让客户端更新以修复可能的错误

                                Return False    '返回false未修改
                            Else
                                Return True     '返回true修改过
                            End If
                        Else
                            Return True     '返回true修改过
                        End If

                    Case ClientTypeInSettings.KOR
                        '为韩服，判断英韩字体

                        If CheckSameFile(
                                bkFontFolder & "\YDYGO550.ttf" _
                                , launchPad.DirectoryName & "\UI\Resource\Fonts\YDYGO550.ttf"
                                ) _
                                Then
                            '判断韩字体是否存在，否则返回true修改过
                            '判断韩字体是否与原始字体相同，否则返回true修改过
                            '默认出错返回false不同，无需还原，让客户端更新以修复可能的错误

                            Return False    '返回false未修改
                        Else
                            Return True     '返回true修改过
                        End If

                    Case Else
                        Return False    '为美服测试服，则无需进一步判断，返回false未修改

                End Select

            Else
                Return False    '备份字体不存在，返回false不修改
            End If

        Catch exModifiedFont As Exception
            MsgBox(exModifiedFont.Message, MsgBoxStyle.Exclamation, "ModifiedFont Error")   '显示错误信息
            Return False
        End Try
    End Function

    Private Function CheckSameFile(tmpFileDIR1 As String, tmpFileDIR2 As String) As Boolean
        '输入两文件完整地址，比较文件是否相同。（默认出错返回true相同，，无需还原备份，让客户端更新以修复错误）
        Try
            Dim tmpFileInfo1, tmpFileInfo2 As FileInfo

            If File.Exists(tmpFileDIR1) Then
                '判断文件1是否存在，否则返回true相同，无需还原备份，让客户端更新（以修复可能的错误）
                tmpFileInfo1 = New FileInfo(tmpFileDIR1)
            Else
                Return True
            End If

            If File.Exists(tmpFileDIR2) Then
                '判断文件2是否存在，否则返回true相同，无需还原备份，让客户端更新（以修复可能的错误）
                tmpFileInfo2 = New FileInfo(tmpFileDIR2)
            Else
                Return True
            End If

            If tmpFileInfo1.Length.Equals(tmpFileInfo2.Length) Then
                '判断2文件相同
                Return True
            Else
                Return False
            End If

        Catch exCheckSameFile As Exception
            MsgBox(exCheckSameFile.Message, MsgBoxStyle.Exclamation, "CheckSameFile Error")   '显示错误信息
            Return True
        End Try
    End Function

    Private Function IsSelectedFont() As Boolean     '判断字体是否为所选字体
        Try

            Dim tmpFontSettingFileInfo As FileInfo =
                FontSelectionToFileInfo(
                    ValidateAndSetFontSelection(My.Settings.FontSelector)
                    )
            '当设置中的字体文件不存在时，将字体设为默认


            Dim tmpClientFontFileInfo As FileInfo
            '比较客户端中的字体

            Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                Case ClientTypeInSettings.RU      '俄服
                    tmpClientFontFileInfo = New FileInfo(IIf(CheckBoxENVoice.Checked,
                                           launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf",
                                           launchPad.DirectoryName & "\UI\Resource\Fonts\ROSA Verde Normal.ttf"))
                    '判断是否启用英语语音
                    '替换【俄文】客户端使用的【英文】
                    '或【俄文】字体

                Case ClientTypeInSettings.KOR     '韩服
                    tmpClientFontFileInfo = New FileInfo(launchPad.DirectoryName & "\UI\Resource\Fonts\YDYGO550.ttf")
                    '替换【韩文】客户端使用的【韩文】字体

                    'tmpFontFileInfo.CopyTo(IIf(CheckBoxENVoice.Checked, _
                    '                       launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf", _
                    '                       launchPad.DirectoryName & "\UI\Resource\Fonts\RixMGoM.ttf"), True)
                    '判断是否启用英语语音
                    '替换【韩文】客户端使用的【英文】或【韩文】字体
                    '韩文客户端目前已无法删除韩文启用英文语音，关闭此功能。

                Case Else               '美服官方版、Steam版及测试服
                    tmpClientFontFileInfo = New FileInfo(launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf")
                    '   用中文【黑体】字体，替换【英文】客户端使用的【英文】字体
            End Select

            If tmpFontSettingFileInfo.Length.Equals(tmpClientFontFileInfo.Length) Then
                '判断文件大小是否相同，以此判断是否同一文件
                Return True
            Else
                Return False
            End If

        Catch exIsNotSelectedFont As Exception
            MsgBox(exIsNotSelectedFont.Message, MsgBoxStyle.Exclamation, "IsSelectedFont Error")   '显示错误信息
            Return False
        End Try
    End Function

    Private Function ModifiedX64() As Boolean        '判断64位是否被修改过
        Try
            Return False
            '使用32位客户端功能关闭，俄服只有32位，美服只有64位……

            'If My.Settings.ClientType <> ClientType.RU _
            '                AndAlso File.GetLastWriteTime(launchPad.DirectoryName & "\PlanetSide2_x64.exe").Equals _
            '                    (File.GetLastWriteTime(launchPad.DirectoryName & "\PlanetSide2_x86.exe")) Then
            '    '判断是否为非俄服，若为俄服则终止后续判断；
            '    '判断是否已有64位备份
            '    '判断32位和64位客户端exe是否相同
            '    Return True     '若需要恢复备份，返回true
            'Else
            '    Return False
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Modified64 Error")  '显示错误信息
            Return False
        End Try
    End Function

    Private Sub RunModify(Optional manual As Boolean = False)     '按钮执行修改操作
        Try
            'If MsgBox(
            '    "请·仔细·阅读·右下·闪烁·提示框·内容！" & Chr(13) & Chr(10) &
            '    "【再次确认】！" & Chr(13) & Chr(10) &
            '    "游戏登录器是否已完成更新？" _
            '    , MsgBoxStyle.Question + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 _
            '    , "【注意！】") _
            '= MsgBoxResult.Ok _
            'Then
            If MsgBox(
                IIf(manual,
                    "请确认已在登录器登录账号，" _
                    & Chr(13) & Chr(10) & "且按钮已变为【Play】？" _
                    , "请仔细阅读右下【闪烁提示框】的内容！" _
                    & Chr(13) & Chr(10) & "再次确认！" _
                    & Chr(13) & Chr(10) & "游戏登录器是否已完成更新？"
                    ) _
                    , MsgBoxStyle.Question + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 _
                    , "【注意！】") _
            = MsgBoxResult.Ok _
            Then
                'msgbox格式值为累加计算得出，按钮格式“OK Cancel”=1，question警告=32，默认按钮第二个=256
                '按钮格式必须为OK Cancel, 才能判断是点击OK还是取消

                UpdateBackup()      '登录器更新完成后，点击按钮都要执行检查备份操作，如有更新，则替换掉备份文件。

                If MsgBox(
                    "挂狗死全家！同意的顶~" _
                    , MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 _
                    , "感谢声讨挂狗") _
                = MsgBoxResult.Ok _
                Then
                    'msgbox格式值为累加计算得出，按钮格式“OK Cancel”=1，exclamation警告=48，默认按钮第二个=256。
                    '按钮格式必须为OK Cancel, 才能判断是点击OK还是取消

                    If manual = True Then
                        ManuallyTranslated = True   '确认执行汉化，将手动汉化标识符设为True
                    End If

                    Dim resultStr As String = ""    '设定反馈结果用的字符串
                    'Dim resultCount As Byte = 0     '统计执行了几项功能，用于判断执行结果是否需要换行显示
                    '由于设计更改，无论勾选与否都要执行相关操作，因而无需再判断执行了几项功能，输出字符串固定。

                    resultStatusNum = MsgBoxStyle.Information       '设定msgbox类型为info，表示正常状态

                    If TestDir(True) _
                        AndAlso ValidateBackupFontIntegrity(True) _
                        AndAlso SetFileAttributes() _
                        Then
                        '测试反馈客户端位置是否可用，不可用时弹框提示选择；
                        '验证PS2CN汉化器核心文件完整性，参数true表示文件缺失时需要弹出对话框提示；
                        '将文件属性设为普通（避免只读文件错误），其中包含客户端位置验证TestDir(False)；

                        If ComboBoxCN.SelectedIndex = 1 Then   '当勾选汉化选项
                            resultStr = ModifiyCN()         '运行解码汉化，俄服若勾选使用英文，将汉化英文dat
                        Else
                            resultStr = RestoreLocale()     '当未勾选汉化选项，恢复备份。
                            '会同时恢复英文和主语种dat。因此删除主语种dat需要在此之后进行。
                        End If

                        'If CheckBoxCN.Checked Then   '当勾选汉化选项
                        '    resultStr = ModifiyCN()         '运行解码汉化，俄服若勾选使用英文，将汉化英文dat
                        'Else
                        '    resultStr = RestoreLocale()     '当未勾选汉化选项，恢复备份。
                        '    '会同时恢复英文和主语种dat。因此删除主语种dat需要在此之后进行。
                        'End If

                        If CheckBoxENVoice.Checked Then     '若启用英语语音
                            '之前可能执行过恢复备份RestoreLocale()。
                            '会同时恢复英文和主语种dat。因此删除主语种dat需要在此之后进行。

                            Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                                Case ClientTypeInSettings.RU
                                    If File.Exists(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dat") Then
                                        File.Delete(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dat")
                                    End If
                                    If File.Exists(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dir") Then
                                        File.Delete(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dir")
                                    End If
                                    '   删除【俄文】文本，启用英文语音客户端

                                    '    '韩文客户端目前已无法删除韩文启用英文语音，关闭此功能。

                                    'Case ClientType.KOR
                                    '    If File.Exists(launchPad.DirectoryName & "\Locale\ko_kr_data.dat") Then
                                    '        File.Delete(launchPad.DirectoryName & "\Locale\ko_kr_data.dat")
                                    '    End If
                                    '    If File.Exists(launchPad.DirectoryName & "\Locale\ko_kr_data.dir") Then
                                    '        File.Delete(launchPad.DirectoryName & "\Locale\ko_kr_data.dir")
                                    '    End If
                                    '    '   删除【韩文】文本，启用英文语音客户端

                            End Select
                        End If

                        If Not IsSelectedFont() Then    '判断字体是否"不是"所选字体
                            resultStr &= Chr(13) & Chr(10) &
                                ModifiyFont()
                            '修改字体，返回值为操作成功或出错信息
                        End If

                        '使用32位客户端功能关闭，俄服只有32位，美服只有64位……
                        'If My.Settings.ClientType <> ClientType.RU Then   '若为俄服，没有64位，屏蔽下列功能
                        '    If CheckBox32bit.Checked Then    '当勾选使用32位客户端
                        '        resultStr &= Chr(13) & Chr(10) & Modify32bit()     '使用32位客户端
                        '    Else
                        '        resultStr &= Chr(13) & Chr(10) & RestoreX64()   '如未勾选使用32位客户端选项，则恢复备份
                        '    End If
                        'End If

                        If resultStatusNum = MsgBoxStyle.Information Then   '判断若结果没有出错，额外提示可以正常启动游戏
                            resultStr &= Chr(13) & Chr(10) & Chr(13) & Chr(10) & "【可以开始游戏】"
                            MsgBox(resultStr, resultStatusNum, "修改完成！")     '输出修改结果，msgbox类型，根据操作结果决定

                            ActivateLauncher()          '切换到客户端登录器窗口
                        Else
                            MsgBox(resultStr, resultStatusNum, "修改出错")     '输出修改结果，msgbox类型，根据操作结果决定
                        End If

                    End If
                Else
                    MsgBox("不顶不给使用~ 蟹蟹。", MsgBoxStyle.Critical)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "RunModify Error")      '显示错误信息
        End Try
    End Sub

    Private Sub UpdateBackup()      '登录器更新完成后，点击按钮都要执行检查备份操作，如有更新，则替换掉备份文件。
        Try     '检查客户端语言包是否有更新需要备份

            Dim tmpFileInfo As FileInfo             '临时FileInfo文件

            If (My.Settings.ClientType = ClientTypeInSettings.STEAM _
                OrElse My.Settings.ClientType = ClientTypeInSettings.EN _
                OrElse My.Settings.ClientType = ClientTypeInSettings.TEST) _
                AndAlso File.GetLastWriteTime(launchPad.DirectoryName & "\Locale\en_us_data.dat").CompareTo _
                (File.GetLastWriteTime("PS2CN Launcher.exe")) > 0 _
                AndAlso Not ModifiedLocale(True) Then
                '检查客户端语言包是否比汉化器新；
                '再额外比较日期是否不相等(目标文本是否为上次汉化文本)，true表示无需判断备份是否合法

                If Not LabelUpdate.Text.Contains("有更新") Then
                    '若未检测到汉化器版本更新（该text未被修改），则提示客户端已有更新

                    LauncherForm1ShowTitle("已过时")
                    '修改主窗体标题栏，更新版本状态

                    LabelUpdate.Text = "汉化器已落后于游戏版本！请等待将来更新。" & Chr(13) & Chr(10) &
                        "当前汉化可能与游戏更新后的内容不符，并出现乱码。建议暂时恢复英文原版（将"“进行汉化"”改为"“英文原版"”）。"
                End If
            End If

            '使用32位客户端功能关闭，俄服只有32位，美服只有64位……
            'If My.Settings.ClientType <> ClientType.RU _
            '    AndAlso (File.GetLastWriteTime(launchPad.DirectoryName & "\PlanetSide2_x64.exe").CompareTo _
            '         (File.GetLastWriteTime(launchPad.DirectoryName & "\PlanetSide2_x86.exe")) <> 0 _
            '         OrElse Not (File.Exists(bkEXEFolder & "\PlanetSide2_x64.exe"))) Then
            '    '备份64位客户端；
            '    '1、判断是否非俄服
            '    '2、判断当前是否未启用32位客户端, 如当前为客户端登录器更新后的官方原始64位客户端, 则需要备份
            '    '3、判断x64.exe备份是否不存在
            '    tmpFileInfo = New FileInfo(launchPad.DirectoryName & "\PlanetSide2_x64.exe")
            '    tmpFileInfo.CopyTo(bkEXEFolder & "\PlanetSide2_x64.exe", True)     '创建备份
            'End If

            If Not (File.Exists(bkDataFolder & "\en_us_data.dat")) _
                OrElse (
                    Not ModifiedLocale(True) _
                    AndAlso
                    Not (
                        File.GetLastWriteTime(launchPad.DirectoryName & "\Locale\en_us_data.dat").ToString.
                        Equals(File.GetLastWriteTime(bkDataFolder & "\en_us_data.dat").ToString) _
                        AndAlso
                        File.GetLastWriteTime(launchPad.DirectoryName & "\Locale\en_us_data.dir").ToString.
                        Equals(File.GetLastWriteTime(bkDataFolder & "\en_us_data.dir"))
                        )
                    ) Then
                '如备份不存在，为true，直接备份
                '再判断客户端英文文本是否为上次汉化文本(参数true表示无需判断备份是否合法)，若为修改版为false，直接放弃备份
                '判断客户端英文dat与dir是否和备份英文dat与dir一样，都相同，则为false放弃备份，任一不同，则为true需要备份；

                If File.Exists(launchPad.DirectoryName & "\Locale\en_us_data.dat") Then
                    tmpFileInfo = New FileInfo(launchPad.DirectoryName & "\Locale\en_us_data.dat")
                    tmpFileInfo.CopyTo(bkDataFolder & "\en_us_data.dat", True)
                    '用最新客户端语言包，替换掉英文的备份dat
                End If
                If File.Exists(launchPad.DirectoryName & "\Locale\en_us_data.dir") Then
                    tmpFileInfo = New FileInfo(launchPad.DirectoryName & "\Locale\en_us_data.dir")
                    tmpFileInfo.CopyTo(bkDataFolder & "\en_us_data.dir", True)
                    '用最新客户端语言包，替换掉英文的备份dir
                End If

                If My.Settings.ClientType = ClientTypeInSettings.RU _
                    OrElse My.Settings.ClientType = ClientTypeInSettings.KOR Then
                    '对于非英语客户端，需要用到的语言文件有两个
                    If File.Exists(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dat") Then
                        tmpFileInfo = New FileInfo(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dat")
                        tmpFileInfo.CopyTo(bkDataFolder & dataNameNEN & ".dat", True)
                        '用最新客户端语言包，替换掉非英文的备份dat
                    End If
                    If File.Exists(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dir") Then
                        tmpFileInfo = New FileInfo(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dir")
                        tmpFileInfo.CopyTo(bkDataFolder & dataNameNEN & ".dir", True)
                        '用最新客户端语言包，替换掉非英文的备份dir
                    End If
                End If
            End If

        Catch exUpdate As Exception
            MsgBox(exUpdate.Message, MsgBoxStyle.Exclamation, "UpdateBackup Error")        '在状态栏显示错误信息
        End Try

    End Sub

    Private Function RestoreLocale() As String      '恢复客户端原始语言文本，用以通过校验节省流量
        Try
            '由于需要替换现有文件，不能直接用一句file.copy,必须用fileinfo

            Dim tmpFileInfo As FileInfo             '临时FileInfo文件

            If File.Exists(bkDataFolder & "\en_us_data.dat") Then
                tmpFileInfo = New FileInfo(bkDataFolder & "\en_us_data.dat")
                tmpFileInfo.CopyTo(launchPad.DirectoryName & "\Locale\en_us_data.dat", True)
            End If
            If File.Exists(bkDataFolder & "\en_us_data.dir") Then
                tmpFileInfo = New FileInfo(bkDataFolder & "\en_us_data.dir")
                tmpFileInfo.CopyTo(launchPad.DirectoryName & "\Locale\en_us_data.dir", True)
            End If

            If My.Settings.ClientType = ClientTypeInSettings.RU _
                OrElse My.Settings.ClientType = ClientTypeInSettings.KOR Then
                '对于非英语客户端，需要恢复的语言文件有两个
                '由于需要替换现有文件，不能直接用一句file.copy,必须用fileinfo
                If File.Exists(bkDataFolder & dataNameNEN & ".dat") Then
                    tmpFileInfo = New FileInfo(bkDataFolder & dataNameNEN & ".dat")
                    tmpFileInfo.CopyTo(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dat", True)
                End If
                If File.Exists(bkDataFolder & dataNameNEN & ".dir") Then
                    tmpFileInfo = New FileInfo(bkDataFolder & dataNameNEN & ".dir")
                    tmpFileInfo.CopyTo(launchPad.DirectoryName & "\Locale" & dataNameNEN & ".dir", True)
                End If
            End If

            Return "成功取消汉化，已恢复为备份文件。"

        Catch exRestoreLocale As Exception
            resultStatusNum = MsgBoxStyle.Critical      '设定msgbox类型为critical      '将全局变量修改结果出错标识设为false
            Return "汉化取消出错：" & exRestoreLocale.Message
        End Try
    End Function

    Private Function RestoreFont() As String      '恢复客户端原始字体，用以通过校验节省流量
        Try
            If ValidateBackupFontIntegrity(True) Then
                '验证PS2CN汉化器核心文件完整性，参数true表示文件缺失时需要弹出对话框提示；

                Dim tmpFileInfo As FileInfo             '临时FileInfo文件

                Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                    Case ClientTypeInSettings.STEAM, ClientTypeInSettings.EN, ClientTypeInSettings.TEST
                        tmpFileInfo = New FileInfo(bkFontFolder & "\Geo-Md.ttf")                '恢复英文字体
                        tmpFileInfo.CopyTo(launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf", True)

                    Case ClientTypeInSettings.RU
                        tmpFileInfo = New FileInfo(bkFontFolder & "\Geo-Md - RU.ttf")                '恢复英文字体
                        tmpFileInfo.CopyTo(launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf", True)
                        tmpFileInfo = New FileInfo(bkFontFolder & "\ROSA Verde Normal.ttf")     '恢复俄文字体
                        tmpFileInfo.CopyTo(launchPad.DirectoryName & "\UI\Resource\Fonts\ROSA Verde Normal.ttf", True)

                    Case ClientTypeInSettings.KOR
                        tmpFileInfo = New FileInfo(bkFontFolder & "\Geo-Md.ttf")                '恢复英文字体
                        tmpFileInfo.CopyTo(launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf", True)
                        tmpFileInfo = New FileInfo(bkFontFolder & "\YDYGO550.ttf")               '恢复韩文字体
                        tmpFileInfo.CopyTo(launchPad.DirectoryName & "\UI\Resource\Fonts\YDYGO550.ttf", True)
                End Select

                Return "成功恢复原始字体。"
            Else
                Return "还原字体出错，原始字体备份缺失。"
            End If

        Catch exRestoreFont As Exception
            resultStatusNum = MsgBoxStyle.Critical      '设定msgbox类型为critical      '将全局变量修改结果出错标识设为false
            Return "还原字体出错：" & exRestoreFont.Message
        End Try
    End Function

    Private Function RestoreX64() As String         '非俄服情况下，恢复客户端64位客户端，用以通过校验节省流量
        Try
            If My.Settings.ClientType <> ClientTypeInSettings.RU _
                AndAlso File.Exists(bkEXEFolder & "\PlanetSide2_x64.exe") _
                AndAlso File.GetLastWriteTime(launchPad.DirectoryName & "\PlanetSide2_x64.exe").Equals _
                    (File.GetLastWriteTime(launchPad.DirectoryName & "\PlanetSide2_x86.exe")) Then
                '判断是否为非俄服，若为俄服则终止后续判断；
                '判断是否已有64位备份
                '判断32位和64位客户端exe是否相同

                Dim tmpFileInfo As FileInfo = New FileInfo(bkEXEFolder & "\PlanetSide2_x64.exe")
                '临时FileInfo文件
                tmpFileInfo.CopyTo(launchPad.DirectoryName & "\PlanetSide2_x64.exe", True)
            End If

            Return "成功恢复64位客户端。"

        Catch exRestore As Exception
            resultStatusNum = MsgBoxStyle.Critical      '设定msgbox类型为critical      '将全局变量修改结果出错标识设为false
            Return "恢复64位客户端时出错：" & exRestore.Message
        End Try
    End Function

    Private Function Modify32bit() As String   '强制替换32位客户端，返回值为操作成功或出错信息
        Try
            Dim tmpFileInfo As FileInfo = New FileInfo(launchPad.DirectoryName & "\PlanetSide2_x86.exe")
            '临时FileInfo文件
            tmpFileInfo.CopyTo(launchPad.DirectoryName & "\PlanetSide2_x64.exe", True)   '用32位客户端替换64位
            Return "32位客户端替换成功。"
        Catch ex As Exception
            resultStatusNum = MsgBoxStyle.Critical      '设定msgbox类型为critical      '将全局变量修改结果出错标识设为false
            Return "32位客户端替换出错：" & ex.Message
        End Try
    End Function

    Private Function ModifiyCN() As String      '进行汉化，返回值为操作成功或出错信息
        '以下代码将要转移进“生成dir”按钮事件下。

        Dim zhdatStream As MemoryStream = Nothing           '使用内置资源流
        Dim endatStream As FileStream = Nothing             '以FileStream文件流形式打开dat，dir
        Dim endirStream As FileStream = Nothing
        Dim zhdatBinReader As BinaryReader = Nothing        '二进制读取dat文件
        Dim endatBinWriter As BinaryWriter = Nothing        '二进制模式写入endat文件
        Dim endirWriter As StreamWriter = Nothing           '文本模式写入endir文件
        Dim zhdatPosition As Long = 0           '记录dat文件头位置
        Dim zhdatCharPointer As Char = Nothing      '二进制读取zhdat，得到的一字符Char 16位 0到65535
        Dim zhdatBytePointer As Byte = Nothing      '二进制读取zhdat，得到的一字节Byte 8位 -128到127
        Dim zhdatString As String = Nothing     '用于写入的临时字符串
        Dim endirStart As UInteger = 3      'dir中的起始位
        Dim endirOffset As Short = 0        'dir中的字符串长度（偏移量）
        Dim endir1stTab As Boolean = True   '首列标示符，判断是否为第一个Tab制表符，用于写入文本编号
        Dim runCNDataName As String = ""    '临时文本名称
        Dim returnSuccess As Boolean = False    '判断是否汉化成功的返回值

        Try
            Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                Case ClientTypeInSettings.RU      '俄服
                    runCNDataName = IIf(CheckBoxENVoice.Checked, "\en_us_data", "\ru_ru_data")    '判断是否启用英文语音

                Case ClientTypeInSettings.KOR     '韩服
                    runCNDataName = "\ko_kr_data"
                    'runCNDataName = IIf(CheckBoxENVoice.Checked, "\en_us_data", "\ko_kr_data")    '判断是否启用英文语音
                    '韩文客户端目前已无法删除韩文启用英文语音，关闭此功能。

                Case Else               '美服及测试服
                    runCNDataName = "\en_us_data"

            End Select

            '测试使用内置资源，内置资源dat默认打开为byte[]，正好适合作为内存流构造函数的参数导入，进行流操作
            If My.Settings.ClientType = ClientTypeInSettings.TEST Then
                zhdatStream = New MemoryStream(My.Resources.zh_cn_test_data)
            Else
                zhdatStream = New MemoryStream(My.Resources.zh_cn_data)
            End If
            zhdatBinReader = New BinaryReader(zhdatStream)
            endatStream = New FileStream(launchPad.DirectoryName & "\Locale" & runCNDataName & ".dat", FileMode.Create, FileAccess.Write)
            endatBinWriter = New BinaryWriter(endatStream)
            endirStream = New FileStream(launchPad.DirectoryName & "\Locale" & runCNDataName & ".dir", FileMode.Create, FileAccess.Write)
            endirWriter = New StreamWriter(endirStream)


            '读取zhdat文件头3个字符，写入endat
            Do Until (zhdatBinReader.PeekChar = &H23)   'endat读取文件头十六进制字符&H EF BB BF，直到行首为##，再开始读入dir。
                endatBinWriter.Write(zhdatBinReader.ReadByte())
            Loop
            endatBinWriter.Flush()


            '将zhdat的10行 ## 起始的注释语句写入endir头部。
            zhdatBytePointer = zhdatBinReader.ReadByte      '由于循环顺序原因，手动读入第一个#
            Do      '将dat从##开始读取，写入dir头部，读到回车后下一行没有##为止。
                zhdatString &= Chr(zhdatBytePointer)    '将读入字符加入流中
                zhdatBytePointer = zhdatBinReader.ReadByte  '从dat读一个字节(8位2进制)，文件指针向后移动
            Loop Until (zhdatBytePointer = &HA AndAlso zhdatBinReader.PeekChar <> &H23)     'dir头部，读到十六进制换行符&H0A，判断下一行首字符是否为#
            endirWriter.Write(zhdatString)    '将文件头写入dir
            endirWriter.Flush()       '将缓冲区内容写入流……不用这命令就不会实际执行写入艹……
            zhdatString = Nothing     'String写入文件后，不会自动清空，跟C++不同…… String空值为Nothing


            '读取zhdat ## 之后的正文部分，完整写入endat
            zhdatPosition = zhdatStream.Position    '记住zhdat流指针位置，此处为dat正文起始处
            Do While zhdatBinReader.PeekChar >= 0   '读取zhdat正文，写入endat

                zhdatCharPointer = zhdatBinReader.ReadChar      '由于是读取char，因而无法并入下面dir计算，dir计数需要读取byte

                Select Case zhdatCharPointer
                    'zhdat中，将0D作为一句词条的结尾判断符，而0A是每一行的换行符。写入endat时，将所有0A增补为0D 0A；而原文0D在写入时做忽略处理。

                    Case Chr(13)        '遇到0D符号，直接跳过本字符，不写入endat，继续读取下一个字符

                    Case Chr(10)        '遇到0A符号，视作行结尾，写入endat时增补为0D 0A
                        endatBinWriter.Write(Chr(13))   '将0D写入endat
                        endatBinWriter.Write(Chr(10))
                    Case Else
                        endatBinWriter.Write(zhdatCharPointer)   '将当前字符写入endat
                End Select

            Loop
            endatBinWriter.Flush()      '将缓冲区内容写入流……不用这命令就不会实际执行写入艹……
            zhdatStream.Position = zhdatPosition    '重新定位回zhdat正文起始处


            '回到zhdat正文开头位置，开始计算并写入endir
            Do      '从zhdat正文起始处，正式开始计算dir
                If (zhdatBytePointer <> 9 AndAlso endir1stTab) Then    '读取条目编号，读到第一个制表符ASCII=9，&H9之前，
                    zhdatString &= Chr(zhdatBytePointer)        '将dat文本条目编号，写入dir第一列。
                ElseIf (zhdatBytePointer = 9 AndAlso endir1stTab) Then    '读到第一个制表符&H9，
                    endir1stTab = False   '标示符改为0，第一个制表符之后已经不是编号了，无需读取。
                ElseIf (zhdatBytePointer = 13) Then   '读到&H0D，是zhdat一句词条的真正结尾标示
                    zhdatString &= Chr(9) & endirStart & Chr(9) & endirOffset & Chr(9) & "d" & Chr(13) & Chr(10)
                    '将第二列start、第三列offset和第四列"d"加入字符串，最后0D 0A换行。整句结尾的0D0A不计入offset字符数，但读到0D时计数忽略，因而offset计数截止0D前一位。
                    endirWriter.Write(zhdatString)  '将行写入dir
                    endirWriter.Flush()     'write命令必须要flush才会写入文件…… 不然不写入就被下一行清空了……
                    zhdatString = Nothing   '手动清空字符串
                    endirStart += endirOffset + 2   '新一句的起始位，由上一句Start加上offset，再加上未计数的0D和最后一位0A。
                    endirOffset = 0       '重置单行字符数
                    endir1stTab = True        '进入下一行，重置首列标示符
                    zhdatBytePointer = zhdatBinReader.ReadByte()     '0D后面还有一位0A，dir无需计入，手动往后空读一位
                End If

                zhdatBytePointer = zhdatBinReader.ReadByte

                Select Case zhdatBytePointer
                    Case 10         'zhdat中的0A结尾，写入endat后都会增补为0D 0A，因而遇到0A就将offset计数+2
                        endirOffset += 2
                    Case 13         '由于逢0A计数就+2，所以0D需要被忽略，不计数

                    Case Else       '一般情况下，读1字节，计数+1
                        endirOffset += 1    '将本byte计入endir
                End Select
            Loop


        Catch errcode As EndOfStreamException
            '检查是否读到文件结束

            returnSuccess = True    '汉化成功，设定返回值
            Return "汉化成功。"
        Catch errcode As Exception
            returnSuccess = False   '汉化失败，设定返回值
            resultStatusNum = MsgBoxStyle.Critical      '设定msgbox类型为critical      '将全局变量修改结果出错标识设为false
            Return "汉化出错：" & errcode.Message & Chr(13) & Chr(10) & RestoreLocale()
            '如出错，显示错误码；同时还原语言文件备份，并显示还原状态。
        Finally     'Try结束前，执行关闭文件指令，无论是否发生错误(肯定是eof错误弹出么)。
            '调用Close函数释放资源后可能还需要再次使用，而Dispose函数释放的资源不再使用？
            If zhdatBinReader IsNot Nothing Then zhdatBinReader.Dispose()
            If zhdatStream IsNot Nothing Then zhdatStream.Dispose()
            'zhdatStream.dispose()
            If endatBinWriter IsNot Nothing Then endatBinWriter.Dispose()
            If endatStream IsNot Nothing Then endatStream.Dispose()
            'endatStream.dispose()
            If endirWriter IsNot Nothing Then endirWriter.Dispose()
            If endirStream IsNot Nothing Then endirStream.Dispose()
            'endirStream.dispose()

            If returnSuccess Then
                '若汉化成功，在关闭文件后，记录修改时间
                RecordLocaleVersion(File.GetLastWriteTime(launchPad.DirectoryName & "\Locale" & runCNDataName & ".dat").ToString)
                '记录文件修改时间，保存到设置，用于判断当前语言文件是否为汉化版
            End If
        End Try
    End Function

    Private Function ModifiyFont() As String      '修改字体，返回值为操作成功或出错信息
        'tmpFontFileInfo作为输入参数。当设置中的字体文件不存在时，将字体设为默认
        Try
            If ValidateBackupFontIntegrity(True) Then
                '验证PS2CN汉化器核心文件完整性，参数true表示文件缺失时需要弹出对话框提示；

                Dim inputFontFileInfo As FileInfo =
                    FontSelectionToFileInfo(
                        ValidateAndSetFontSelection(My.Settings.FontSelector)
                        )
                '当设置中的字体文件不存在时，将字体设为默认

                Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变

                    Case ClientTypeInSettings.RU      '俄服
                        inputFontFileInfo.CopyTo(IIf(CheckBoxENVoice.Checked,
                                               launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf",
                                               launchPad.DirectoryName & "\UI\Resource\Fonts\ROSA Verde Normal.ttf"), True)
                        '判断是否启用英语语音
                        '替换【俄文】客户端使用的【英文】
                        '或【俄文】字体

                    Case ClientTypeInSettings.KOR     '韩服
                        inputFontFileInfo.CopyTo(launchPad.DirectoryName & "\UI\Resource\Fonts\YDYGO550.ttf", True)
                        '替换【韩文】客户端使用的【韩文】字体

                        'tmpFontFileInfo.CopyTo(IIf(CheckBoxENVoice.Checked, _
                        '                       launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf", _
                        '                       launchPad.DirectoryName & "\UI\Resource\Fonts\RixMGoM.ttf"), True)
                        '判断是否启用英语语音
                        '替换【韩文】客户端使用的【英文】或【韩文】字体
                        '韩文客户端目前已无法删除韩文启用英文语音，关闭此功能。

                    Case Else               '美服及测试服
                        inputFontFileInfo.CopyTo(launchPad.DirectoryName & "\UI\Resource\Fonts\Geo-Md.ttf", True)
                        '   用中文【黑体】字体，替换【英文】客户端使用的【英文】字体
                End Select

                Return "字体修改成功。"
            Else
                Return "字体修改出错，原始字体备份缺失。"
            End If
        Catch exModifiyFont As Exception
            resultStatusNum = MsgBoxStyle.Critical      '设定msgbox类型为critical      '将全局变量修改结果出错标识设为false
            Return "字体修改出错：" & exModifiyFont.Message
        End Try
    End Function

    Private Function FontSelectionToFileInfo(testFontSelection As String) As FileInfo
        '将ComboBoxFontSelector的字体名转化为路径。返回程序启动路径（非合法文件路径）表示出错

        Try

            If testFontSelection = useDefaultFont Then
                '若选择使用默认字体

                Return DefaulFontFileInfo()
                '用默认字体

            ElseIf testFontSelection = useChineseFont Then  '若选择使用中文黑体
                Return New FileInfo(bkFontFolder & "\simhei.ttf")
                '用中文【黑体】字体

            ElseIf File.Exists(Application.StartupPath & customFontDIR & "\" & testFontSelection) Then
                '判断是否为合法的自定义字体

                Return New FileInfo(Application.StartupPath & customFontDIR & "\" & testFontSelection)
                '若使用自定义字体
            Else
                '错误的设定值，使用默认字体

                Return DefaulFontFileInfo()
                '用默认字体

            End If

        Catch exIsNotSelectedFont As Exception
            MsgBox(exIsNotSelectedFont.Message, MsgBoxStyle.Exclamation, "FontSelectionToFileInfo Error")   '显示错误信息
            Return New FileInfo("C:\")      '表示出错时，故意返回的错误路径。
        End Try
    End Function

    Private Function DefaulFontFileInfo() As FileInfo
        '返回默认字体所在路径。返回程序启动路径（非合法文件路径）表示出错

        Try
            If My.Settings.CheckedCN Then
                '若使用汉化，使用中文默认字体

                Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变

                    Case ClientTypeInSettings.RU      '俄服
                        Return New FileInfo(bkFontFolder & "\simhei.ttf")
                            '   用中文【黑体】字体

                    Case ClientTypeInSettings.KOR     '韩服
                        Return New FileInfo(bkFontFolder & fontCNKOR)
                        '   用【中韩俄合成】字体

                    Case Else               '美服及测试服
                        Return New FileInfo(bkFontFolder & "\simhei.ttf")
                        '   用中文【黑体】字体
                End Select

            Else
                '若不使用汉化，使用英文默认字体

                Return New FileInfo(bkFontFolder & "\Geo-Md.ttf")
                '   用英文【默认】字体
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "DefaulFontFileInfo Error")   '显示错误信息
            Return New FileInfo("C:\")      '表示出错时，故意返回的错误路径。
        End Try
    End Function

    Private Function ValidateAndSetFontSelection(inputFontSelection As String) As String
        '验证并设置所选字体（从设置到combobox，或反向）

        Try
            If FontSelectionToFileInfo(inputFontSelection).Exists Then
                '当combobox的text中的字体文件存在时，将设置设为该值。否则设为默认
                Return inputFontSelection
            Else
                Return useDefaultFont
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ValidateAndSetFontSelection Error")
            Return useDefaultFont       '若出错，返回默认
        End Try
    End Function

    Private Sub RecordLocaleVersion(tempLocaleVersion As String)    '记录文件修改时间，保存到设置，用于判断当前语言文件是否为汉化版
        Try
            Select Case My.Settings.ClientType  '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                Case ClientTypeInSettings.STEAM
                    My.Settings.STEAMLocaleVersion = tempLocaleVersion
                Case ClientTypeInSettings.EN
                    My.Settings.ENLocaleVersion = tempLocaleVersion
                Case ClientTypeInSettings.TEST
                    My.Settings.TESTLocaleVersion = tempLocaleVersion
                Case ClientTypeInSettings.RU
                    If CheckBoxENVoice.Checked Then
                        My.Settings.RULocaleVersionEN = tempLocaleVersion
                    Else
                        My.Settings.RULocaleVersionRU = tempLocaleVersion
                    End If
                Case ClientTypeInSettings.KOR
                    'If CheckBoxENVoice.Checked Then
                    '    My.Settings.KORLocaleVersionEN = tempLocaleVersion
                    'Else
                    '    My.Settings.KORLocaleVersionKOR = tempLocaleVersion
                    'End If
                    '韩文客户端目前已无法删除韩文启用英文语音，关闭此功能。
                    My.Settings.KORLocaleVersionKOR = tempLocaleVersion
            End Select

            My.Settings.Save()
            '汉化修改后，必须立即保存修改时间，避免设定未保存而异常关机后，下次汉化器对游戏语言是否已汉化的误判。

            SetClientType()     '根据客户端不同，修改相关内存变量及参数，用于条件判断
            '将修改时间，赋值给内存变量localeVersion
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "RecordLocaleVersion Error")        '显示错误信息
        End Try
    End Sub

    Private Sub ActivateLauncher()      '切换到客户端登录器窗口
        Try
            Select Case My.Settings.ClientType
                Case ClientTypeInSettings.RU
                    '俄服没有汉化器，留空
                Case ClientTypeInSettings.KOR
                    AppActivate("Launcher")     '切换到韩服登录器窗口
                Case Else
                    AppActivate("Planetside 2")     '切换到美服登录器窗口
            End Select
        Catch exWindowNotFound As ArgumentException
            '若登录器窗口未启动，无需显示错误信息
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ActivateLauncher Error")        '无需显示错误信息
        End Try
    End Sub

    Private Sub StartClient()       '启动游戏客户端登录器
        Try
            Select Case My.Settings.ClientType
                Case ClientTypeInSettings.STEAM
                    Process.Start("Steam://rungameid/218230")   '通过Steam命令运行客户端
                Case ClientTypeInSettings.KOR
                    Process.Start("http://ps2.daum.net")      '打开浏览器，登录韩服
                Case ClientTypeInSettings.RU
                    '无登录器，留空无操作
                Case Else
                    Process.Start(launchPad.DirectoryName & "\LaunchPad.exe")           '运行客户端
                    'Shell(launchPad.DirectoryName & "\LaunchPad.exe", AppWinStyle.NormalFocus, False)   '运行客户端汉化器
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "StartClient Error")
        End Try
    End Sub

    'Private Sub ActivateGameClient()      '切换到客户端登录器窗口
    '    Try

    '        AppActivate("Planetside2")     '切换到美服登录器窗口
    '    Catch exWindowNotFound As ArgumentException
    '        '若登录器窗口未启动，无需显示错误信息
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ActivateGameClient Error")        '无需显示错误信息
    '    End Try
    'End Sub

    Private Sub KillGameClient()       '弹出对话框请用户确认，是否需要结束游戏程序
        Try
            If MsgBox("确认要结束游戏进程？", 49, "关闭游戏程序") = MsgBoxResult.Ok Then    '根据用户选择，决定是否执行

                Dim pList64() As Process = Process.GetProcessesByName("PlanetSide2_x64")  '获取游戏程序系统进程列表

                For Each gameProcess In pList64
                    '遍历每一个登录器系统进程
                    If gameProcess.Path.Equals(
                        launchPad.DirectoryName & "\PlanetSide2_x64.exe",
                        StringComparison.OrdinalIgnoreCase) Then
                        '如果进程主模块的含路径文件名，就是当前选定的客户端类型下，游戏程序的完整文件名
                        '内存中查到的模块路径可能大小写不同…… 比较时忽略大小写

                        '引入查询进程文件路径的模块QueryFullProcessImageName，
                        '在vista以后的操作系统中，用自定义的函数process.path代替process.MainModule.FileName

                        gameProcess.Kill()      '结束游戏进程
                        Exit For                '结束遍历

                    End If
                Next

                '亚洲3服已死，再无32位客户端
                'Shell("cmd.exe /c taskkill /f /t /IM " & Chr(34) & "PlanetSide2.exe" & Chr(34), AppWinStyle.Hide)
                '结束游戏程序。Chr(34)表示"。听说也可以用一对相邻引号""来表示"，未测试。
            End If

        Catch ex32 As ComponentModel.Win32Exception
            '若在编译-目标CPU选项中，选择了Any CPU并首选32位，则32位汉化器无法访问64位游戏的模块。“32位进程无法访问64位进程的模块。”
            '但实际上忽略32位程序的出错提示后，程序是可以正常运行的。
            '关于编译选项，参见 https://msdn.microsoft.com/zh-cn/library/kb4wyys2.aspx

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "KillGameClient Error")
        End Try
    End Sub

    Private Sub KillLaunchPad()
        Try
            If MsgBox("确认要结束登录器LaunchPad进程？", 49, "关闭登录器") = MsgBoxResult.Ok Then    '根据用户选择，决定是否执行

                Dim killLaunchPadExeName As String = "LaunchPad"
                Dim pList64() As Process = Process.GetProcessesByName(killLaunchPadExeName)  '获取游戏程序系统进程列表

                If My.Settings.ClientType = ClientTypeInSettings.KOR Then '判断客户端类型，不建议使用Combobox的SelectedIndex，会随鼠标hover而改变
                    killLaunchPadExeName = "Launcher"      '设置为韩服登录器名称
                Else
                    killLaunchPadExeName = "LaunchPad"     '设置为美服登录器名称
                End If

                For Each LaunchPadProcess In pList64
                    '遍历每一个登录器系统进程

                    If LaunchPadProcess.Path.Equals(
                        launchPad.DirectoryName & "\" & killLaunchPadExeName & ".exe",
                    StringComparison.OrdinalIgnoreCase) Then
                        '如果进程主模块的含路径文件名，就是当前选定的客户端类型下，游戏程序的完整文件名
                        '内存中查到的模块路径可能大小写不同…… 比较时忽略大小写

                        '引入查询进程文件路径的模块QueryFullProcessImageName，
                        '在vista以后的操作系统中，用自定义的函数process.path代替process.MainModule.FileName

                        LaunchPadProcess.Kill()      '结束游戏进程
                        Exit For                '结束遍历
                    End If

                    If LaunchPadProcess.MainModule.FileName.Equals(
                    launchPad.DirectoryName & "\" & killLaunchPadExeName & ".exe",
                    StringComparison.OrdinalIgnoreCase) Then
                        '如果进程主模块的含路径文件名，就是当前选定的客户端类型下，登录器的完整文件名
                        '内存中查到的路径可能大小写不同…… 比较时忽略大小写

                        '有人反映新的查询路径模块QueryFullProcessImageName无法找到路径，故保留旧版判断函数作为冗余备份

                        LaunchPadProcess.Kill()      '结束游戏进程
                        Exit For                '结束遍历
                    End If

                Next

                '亚洲3服已死，再无32位客户端
                'Shell("cmd.exe /c taskkill /f /t /IM " & Chr(34) & "PlanetSide2.exe" & Chr(34), AppWinStyle.Hide)
                '结束游戏程序。Chr(34)表示"。听说也可以用一对相邻引号""来表示"，未测试。
            End If

        Catch ex32 As ComponentModel.Win32Exception
            '若在编译-目标CPU选项中，选择了Any CPU并首选32位，则32位汉化器无法访问64位游戏的模块。“32位进程无法访问64位进程的模块。”
            '但实际上忽略32位程序的出错提示后，程序是可以正常运行的。
            '关于编译选项，参见 https://msdn.microsoft.com/zh-cn/library/kb4wyys2.aspx

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "KillLaunchPad Error")
        End Try
    End Sub

    Private Sub PingRound1Fail(pingFail As Boolean)
        Try
            If pingFail Then
                pingRound1NOK += 1
            End If
            pingLosePkg = 100 * pingRound1NOK / (pingNum + 1)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "PingRound1Fail Error")
        End Try
    End Sub

    Private Sub ResetLosePKG()      '丢包率统计清零
        Try
            pingRound1 = True       '重设为第一轮测丢包率
            pingRound1NOK = 0       '丢包计数清零
            For Each pingTimeoutI In pingTimeout    '清空重设丢包检验数组为默认值，全false
                pingTimeoutI = False
            Next
            pingNum = 0     '数组标识位指向数组首位
            LabelLosePkg.Text = "0.00%"       '重置label上丢包率数值

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ResetLosePKG Error")
        End Try
    End Sub

    Private Async Sub QueryIPLoc(Optional QueryIPLocInterval As Byte = ipLocTimerMax)
        '查询显示IP归属地,Async表示异步方法，其中包含Await运算符
        '设定可选的Optional传入参数QueryIPLocInterval，来确定距离上一次IP地址查询的时间间隔。
        '默认值为ipLocTimerMax，表示必须经过最大间隔时间后，才可查询。若手动设置时间(如10秒)，则距离上次10秒之后即可再次查询。

        'Dim ipResponse As WebResponse = Nothing         ' Async get the response. Return Task(of WebResponse)

        Try

            Dim ipHttpClient As New HttpClient

            Dim ipStream As Stream = Nothing                ' Get the stream containing content returned by the server.
            Dim ipStreamReader As StreamReader = Nothing    ' Open the stream using a StreamReader for easy access.

            If PanelCheckIPLoc.Visible _
                AndAlso QueryIPLocInterval <= ipLocTimerMax _
                AndAlso ipLocTimer <= ipLocTimerMax - QueryIPLocInterval _
                Then
                '判断IP归属地查询功能是否开启
                '传入参数必须小等于ipLocTimerMax，否则非法
                '若距上次查询已经过了传入参数QueryIPLocInterval设定的间隔时间，则执行操作
                '任一判断条件不符，则不执行任何操作

                '判断语句必须写在实际操作的Try之前，因为Finally语句中重设了ipLocTimer计时器，导致每次调用本函数，无论是否实际执行操作，都会重设计时器

                Try

                    ipLocTimer = ipLocTimerMax + 2
                    '开始查询时，暂停即时。
                    '查询完成后，将倒计时设为最大间隔，启动循环计时

                    LabelIPLoc.Text = "查询中……"
                    LabelIPLoc.Refresh()        '不知道为什么需要手动刷新显示……
                    LabelLocStatus.Text = "请稍候。"
                    LabelLocStatus.Refresh()    '不知道为什么需要手动刷新显示……


                    'Dim ipRequest As WebRequest = WebRequest.Create("http://ipaddress.com/")   ' Create a request for the URL. 查询ip所在地
                    ''ipRequest.Timeout = 10000               '10000毫秒为超时时间，异步无效
                    'ipResponse = Await ipRequest.GetResponseAsync()            ' Async get the response. Return Task(of WebResponse)
                    'ipStream = ipResponse.GetResponseStream()   ' Get the stream containing content returned by the server.
                    'ipStreamReader = New StreamReader(ipStream, System.Text.Encoding.GetEncoding("utf-8"))  ' Open the stream using a StreamReader for easy access.
                    'Dim ipResponseString As String = ipStreamReader.ReadToEnd()   ' Read the content.


                    Dim ipHttpTimeout As New TimeSpan(0, 0, ipLocTimeout)     '设定TimeSpan格式，0时0分ipLocTimeout秒
                    ipHttpClient.Timeout = ipHttpTimeout            '将HttpClient的超时时间设为上行的值

                    'ipHttpClient.DefaultRequestHeaders.Remove("User-Agent")
                    ipHttpClient.DefaultRequestHeaders.Add("User-Agent", "Chrome")
                    'http header添加头字符串User-Agent，Chrome，伪装成Chrome浏览器，以通过服务器端校验，正确获得数据。

                    ipStream = Await ipHttpClient.GetStreamAsync("http://www.geoiptool.com/en/")   ' Read the content.
                    ipStreamReader = New StreamReader(ipStream, System.Text.Encoding.GetEncoding("utf-8"))
                    ' Open the stream using a StreamReader for easy access.
                    '重要！必须使用GetStream + StreamReader指定编码。若直接用GetStringAsync的话无法指定编码，中文乱码！
                    Dim ipResponseString As String = Await ipStreamReader.ReadToEndAsync()   ' Read the content.


                    Select Case My.Settings.ClientType
                        Case ClientTypeInSettings.STEAM, ClientTypeInSettings.EN, ClientTypeInSettings.TEST     '判断是否为美服官方版、Steam版和测试服
                            If ipResponseString.Contains("China") Then  '判断IP是否为中国
                                LabelLocStatus.Text = "IP归属地为中国大陆，不建议登录DB账号。" & Chr(13) & Chr(10) & "请确认VPN是否连接成功。"
                            ElseIf ipResponseString.Contains("Korea") Then
                                LabelLocStatus.Text = "IP归属地非中国大陆，可以登录DB账号。"
                                'LabelLocStatus.Text = "IP归属地为韩国，无法登录行星边际2美服。" & Chr(13) & Chr(10) & "请使用其它地区VPN线路进行连接。"
                            Else
                                LabelLocStatus.Text = "IP归属地非中国大陆，可以登录DB账号。"
                            End If
                        Case ClientTypeInSettings.KOR                     '判断是否为韩服
                            If ipResponseString.Contains("Korea") Then  '判断IP是否为韩国
                                LabelLocStatus.Text = "IP归属地为韩国，可以登录韩服。"
                            Else
                                LabelLocStatus.Text = "IP归属地非韩国，无法登录韩服！" & Chr(13) & Chr(10) & "请重新确认韩国VPN是否连接成功。"
                            End If
                        Case Else                               '若为俄服，无需VPN。
                            LabelLocStatus.Text = "俄服没有IP归属地限制，" & Chr(13) & Chr(10) & "任何IP都可登录。"
                    End Select

                    ToolTip1.SetToolTip(LabelLocStatus, "点击黄字，检测IP归属地。")


                    'geoiptool.com/en/ 网页格式如下：
                    'var maker_country = "<div>Country: Taiwan</div>";
                    'var marker_city = "<div>City: Taipei</div>";
                    'var marker_ip = "<div>IP Address: 114.34.18.155</div>";
                    '以<div>Country: 、</div>、<div>City: 、</div> 作为搜索符。
                    If ipResponseString.Contains("<div>City:") Then
                        ipResponseString = ipResponseString.Remove(0, ipResponseString.IndexOf("<div>Country: ") + 14)
                        '删除国家信息前的所有内容

                        LabelIPLoc.Text = ipResponseString.Substring(0, ipResponseString.IndexOf("</div>"))
                        '截取并显示字符串中国家信息 Display the content.

                        ipResponseString = ipResponseString.Remove(0, ipResponseString.IndexOf("<div>City: ") + 11)
                        '删除城市信息前的所有内容

                        If Not ipResponseString.StartsWith("None") Then
                            '若城市信息不为空（网页此处代码不为None），则显示城市信息。
                            LabelIPLoc.Text += "，" +
                            ipResponseString.Substring(0, ipResponseString.IndexOf("</div>"))
                            '截取字符串中城市信息 Display the content.
                        End If
                    End If


                    ''ipaddress.com/ 网页格式如下：
                    ''<tr><th>City:</th><td>Shanghai</td></tr>
                    ''<tr><th>Country:</th><td><img src="/flags/cn.gif" alt="" title="China (CN)" style="vertical-align:baseline"> China</td></tr>
                    ''此处以City:</th><td>、title=、style=作为搜索符。
                    'If ipResponseString.Contains("City:") Then      '截取国家信息 Display the content.
                    '    ipResponseString = ipResponseString.Remove(0, ipResponseString.IndexOf("Browser:"))
                    '    ipResponseString = ipResponseString.Remove(0, ipResponseString.IndexOf("City:</th>") + 15)
                    '    LabelIPLoc.Text = ipResponseString.Substring(0, ipResponseString.IndexOf("</td>")) + "，" + _
                    '        ipResponseString.Substring(ipResponseString.IndexOf("title=") + 7, _
                    '                         ipResponseString.IndexOf("style=") - 2 - ipResponseString.IndexOf("title=") - 7)
                    'End If


                    ''ip.cn/ 已屏蔽本程序…… 切换至ipaddress.com
                    ''ip.cn网页格式为"&nbsp:[地区信息]</p>"此处以：和<作为搜索符。
                    'If ipResponseString.Contains("&nbsp") Then      '截取国家信息 Display the content.
                    '    ipResponseString = ipResponseString.Remove(0, ipResponseString.IndexOf("&nbsp"))
                    '    LabelIPLoc.Text = ipResponseString.Substring(ipResponseString.IndexOf("：") + 1, _
                    '                        ipResponseString.IndexOf("<") - ipResponseString.IndexOf("：") - 1)
                    'End If


                    ipResponseString = Nothing  '清空字符串，释放内存。

                    'Catch exTimeout As TimeoutException
                    '    LabelIPLoc.Text = "IP归属地查询超时。"
                    '    LabelLocStatus.Text = "有可能是查询网站或汉化器问题，未必影响游戏。" & Chr(13) & Chr(10) & exTimeout.Message
                    '    ToolTip1.SetToolTip(LabelLocStatus, "有可能是查询网站或汉化器问题，未必影响游戏。" & Chr(13) & Chr(10) & exTimeout.Message)
                Catch ex As Exception
                    Try
                        If ex.Message.Contains("已取消一个任务。") _
                        Or ex.Message.Contains("A task was canceled.") _
                        Then
                            LabelIPLoc.Text = "IP归属地查询超时。"
                            LabelLocStatus.Text = "有可能是查询网站或汉化器问题，未必影响游戏。"
                        Else
                            LabelIPLoc.Text = "IP归属地查询出错。"
                            If ex.InnerException Is Nothing Then
                                '若包含InnerException异常，则显示详细信息
                                LabelLocStatus.Text = "有可能是查询网站或汉化器问题，未必影响游戏。" & Chr(13) & Chr(10) & ex.Message
                            Else
                                LabelLocStatus.Text = "有可能是查询网站或汉化器问题，未必影响游戏。" & Chr(13) & Chr(10) & ex.InnerException.Message
                            End If
                        End If

                        ToolTip1.SetToolTip(LabelLocStatus, "有可能是查询网站或汉化器问题，未必影响游戏。" & Chr(13) & Chr(10) & ex.Message)

                    Catch exINex As Exception
                        MsgBox(exINex.Message, MsgBoxStyle.Exclamation, "QueryIPLocException Error")
                    End Try

                Finally
                    If ipStreamReader IsNot Nothing Then ipStreamReader.Dispose()
                    If ipStream IsNot Nothing Then ipStream.Dispose()

                    If ipHttpClient IsNot Nothing Then ipHttpClient.Dispose()

                    'If ipResponse IsNot Nothing Then ipResponse.Dispose()

                    '清空缓存，重新获取网页信息？断网时会触发null错误
                    '虽然貌似close也会调用dispose，但是实测单独使用close无法刷新，只能用dispose？
                    '调用Close函数释放资源后可能还需要再次使用，而Dispose函数释放的资源不再使用？

                    ipLocTimer = ipLocTimerMax      '查询完成后，将倒计时设为最大间隔，启动循环计时
                End Try

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "QueryIPLoc Error")
        End Try
    End Sub

    Private Sub ConvertTime(ctTimeSelection As Byte)
        '根据设定时间，转换不同时区间的时间。hourSelection表示以哪一项为基准时间
        Try

            Dim tempTimeHour As SByte

            Select Case ctTimeSelection
                Case 0
                    CheckBoxDST1.Text = "当天"

                    tempTimeHour = ComboBoxHour1.SelectedIndex +
                        (ComboBoxTimeZone2.SelectedIndex + IIf(CheckBoxDST2.Checked, 1, 0)) -
                        (ComboBoxTimeZone1.SelectedIndex + IIf(CheckBoxDST1.Checked, 1, 0))
                    '当前时间，加上带夏令时的时区之间的差距，即可得目标时区的时间，夏令时为时区+1
                    Select Case tempTimeHour
                        Case Is < 0
                            ComboBoxHour2.SelectedIndex = tempTimeHour + 24
                            CheckBoxDST2.Text = "前一天"
                        Case Is > 23
                            ComboBoxHour2.SelectedIndex = tempTimeHour - 24
                            CheckBoxDST2.Text = "后一天"
                        Case Else
                            ComboBoxHour2.SelectedIndex = tempTimeHour
                            CheckBoxDST2.Text = "当天"
                    End Select

                    tempTimeHour = ComboBoxHour1.SelectedIndex +
                        (ComboBoxTimeZone3.SelectedIndex + IIf(CheckBoxDST3.Checked, 1, 0)) -
                        (ComboBoxTimeZone1.SelectedIndex + IIf(CheckBoxDST1.Checked, 1, 0))
                    '当前时间，加上带夏令时的时区之间的差距，即可得目标时区的时间，夏令时为时区+1
                    Select Case tempTimeHour
                        Case Is < 0
                            ComboBoxHour3.SelectedIndex = tempTimeHour + 24
                            CheckBoxDST3.Text = "前一天"
                        Case Is > 23
                            ComboBoxHour3.SelectedIndex = tempTimeHour - 24
                            CheckBoxDST3.Text = "后一天"
                        Case Else
                            ComboBoxHour3.SelectedIndex = tempTimeHour
                            CheckBoxDST3.Text = "当天"
                    End Select

                Case 1
                    CheckBoxDST2.Text = "当天"

                    tempTimeHour = ComboBoxHour2.SelectedIndex +
                        (ComboBoxTimeZone1.SelectedIndex + IIf(CheckBoxDST1.Checked, 1, 0)) -
                        (ComboBoxTimeZone2.SelectedIndex + IIf(CheckBoxDST2.Checked, 1, 0))
                    '当前时间，加上带夏令时的时区之间的差距，即可得目标时区的时间，夏令时为时区+1
                    Select Case tempTimeHour
                        Case Is < 0
                            ComboBoxHour1.SelectedIndex = tempTimeHour + 24
                            CheckBoxDST1.Text = "前一天"
                        Case Is > 23
                            ComboBoxHour1.SelectedIndex = tempTimeHour - 24
                            CheckBoxDST1.Text = "后一天"
                        Case Else
                            ComboBoxHour1.SelectedIndex = tempTimeHour
                            CheckBoxDST1.Text = "当天"
                    End Select

                    tempTimeHour = ComboBoxHour2.SelectedIndex +
                        (ComboBoxTimeZone3.SelectedIndex + IIf(CheckBoxDST3.Checked, 1, 0)) -
                        (ComboBoxTimeZone2.SelectedIndex + IIf(CheckBoxDST2.Checked, 1, 0))
                    '当前时间，加上带夏令时的时区之间的差距，即可得目标时区的时间，夏令时为时区+1
                    Select Case tempTimeHour
                        Case Is < 0
                            ComboBoxHour3.SelectedIndex = tempTimeHour + 24
                            CheckBoxDST3.Text = "前一天"
                        Case Is > 23
                            ComboBoxHour3.SelectedIndex = tempTimeHour - 24
                            CheckBoxDST3.Text = "后一天"
                        Case Else
                            ComboBoxHour3.SelectedIndex = tempTimeHour
                            CheckBoxDST3.Text = "当天"
                    End Select

                Case 2
                    CheckBoxDST3.Text = "当天"

                    tempTimeHour = ComboBoxHour3.SelectedIndex +
                        (ComboBoxTimeZone1.SelectedIndex + IIf(CheckBoxDST1.Checked, 1, 0)) -
                        (ComboBoxTimeZone3.SelectedIndex + IIf(CheckBoxDST3.Checked, 1, 0))
                    '当前时间，加上带夏令时的时区之间的差距，即可得目标时区的时间，夏令时为时区+1
                    Select Case tempTimeHour
                        Case Is < 0
                            ComboBoxHour1.SelectedIndex = tempTimeHour + 24
                            CheckBoxDST1.Text = "前一天"
                        Case Is > 23
                            ComboBoxHour1.SelectedIndex = tempTimeHour - 24
                            CheckBoxDST1.Text = "后一天"
                        Case Else
                            ComboBoxHour1.SelectedIndex = tempTimeHour
                            CheckBoxDST1.Text = "当天"
                    End Select

                    tempTimeHour = ComboBoxHour3.SelectedIndex +
                        (ComboBoxTimeZone2.SelectedIndex + IIf(CheckBoxDST2.Checked, 1, 0)) -
                        (ComboBoxTimeZone3.SelectedIndex + IIf(CheckBoxDST3.Checked, 1, 0))
                    '当前时间，加上带夏令时的时区之间的差距，即可得目标时区的时间，夏令时为时区+1
                    Select Case tempTimeHour
                        Case Is < 0
                            ComboBoxHour2.SelectedIndex = tempTimeHour + 24
                            CheckBoxDST2.Text = "前一天"
                        Case Is > 23
                            ComboBoxHour2.SelectedIndex = tempTimeHour - 24
                            CheckBoxDST2.Text = "后一天"
                        Case Else
                            ComboBoxHour2.SelectedIndex = tempTimeHour
                            CheckBoxDST2.Text = "当天"
                    End Select
            End Select

            'My.Settings.Save()          '保存程序设定
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ConvertTime Error")
        End Try
    End Sub

    Private Async Sub QueryServerStatus()    '查询美服状态,Async表示异步方法，其中包含Await运算符

        Dim ssHttpClient As New HttpClient

        Dim ssStream As Stream = Nothing                ' Get the stream containing content returned by the server.
        Dim ssStreamReader As StreamReader = Nothing    ' Open the stream using a StreamReader for easy access.

        Try
            If PanelServerStatus.Visible AndAlso serverStatusTimer = 0 Then
                '仅当此功能激活，且距离上次查询已超过预设间隔时间，才查询美服状态

                serverStatusTimer = serverStatusTimerMax + 2
                '开始查询时，暂停即时。
                '查询完成后，若成功，将倒计时设为最大间隔，启动循环计时；若失败，计时清零，可立即重新查询。

                For labelIndexClear As Byte = 1 To 8
                    '按控件名称来获取所有LabelServer，格式为"LabelServer"+编号，一共8个。

                    Dim labelServerInitiateArray As Control() = PanelServerStatus.Controls.Find("LabelServer" & labelIndexClear, False)
                    '在PanelServerStatus内按名称查找，False表示不查找子控件，将查找结果一维数组赋值给此数组

                    If labelServerInitiateArray.Any Then
                        '若查找结果不为空

                        Dim labelServerInitiate As Control = labelServerInitiateArray.First
                        '将获得的控件结果赋值给此变量

                        '将panel中所有LabelServer清空
                        labelServerInitiate.ForeColor = PanelServerStatus.ForeColor
                        labelServerInitiate.Text = ""
                        ToolTip1.SetToolTip(
                                labelServerInitiate,
                                ""
                                )
                    End If
                Next

                LabelServer1.Text = "美服状态查询中……"
                LabelServer1.Refresh()        '不知道为什么需要手动刷新显示……
                LabelServer2.Text = "请稍候。"
                LabelServer2.Refresh()    '不知道为什么需要手动刷新显示……


                Dim ssHttpTimeout As New TimeSpan(0, 0, 20)     '设定TimeSpan格式，0时0分20秒
                ssHttpClient.Timeout = ssHttpTimeout            '将HttpClient的超时时间设为上行的值

                'ssHttpClient.DefaultRequestHeaders.Remove("User-Agent")
                ssHttpClient.DefaultRequestHeaders.Add("User-Agent", "Chrome")
                'http header添加头字符串User-Agent，Chrome，伪装成Chrome浏览器，以通过服务器端校验，正确获得数据。

                ssStream = Await ssHttpClient.GetStreamAsync("https://forums.station.sony.com/ps2/status/print_status.php")
                ' Read the content.
                ssStreamReader = New StreamReader(ssStream, System.Text.Encoding.GetEncoding("utf-8"))
                'Open the stream using a StreamReader for easy access.
                '重要！必须使用GetStream + StreamReader指定编码。若直接用GetStringAsync的话无法指定编码，中文乱码！
                Dim ssResponseString As String = Await ssStreamReader.ReadToEndAsync()   ' Read the content.


                'ps2/status/print_status.php 网页格式如下：
                '<div class="ssName">Briggs (AU)</div><div class="ssStatus"><span style="color: green;">UP</span>
                '以<div class="ssName">、</div>、<span 、>、</span>作为搜索符。包含</span>前的</div>防错。

                For labelIndexWrite As Byte = 1 To 8
                    '按控件名称来获取所有LabelServer，格式为"LabelServer"+编号，一共8个。

                    Dim labelServerWriteArray As Control() = PanelServerStatus.Controls.Find("LabelServer" & labelIndexWrite, False)
                    '在PanelServerStatus内按名称查找，False表示不查找子控件，将查找结果一维数组赋值给此数组

                    If labelServerWriteArray.Any Then
                        '若查找结果不为空

                        Dim labelServerWrite As Control = labelServerWriteArray.First
                        '将获得的控件结果赋值给此变量

                        If ssResponseString.Contains("<div class=""ssName"">") Then

                            ssResponseString = ssResponseString.Remove(
                                0,
                                ssResponseString.IndexOf("<div class=""ssName"">") + 20
                                )
                            '删除服务器名前的所有内容

                            labelServerWrite.Text =
                                TranslateENtoCN(
                                    ssResponseString.Substring(
                                        0,
                                        ssResponseString.IndexOf("</div>")
                                        )
                                    )
                            '截取并显示字符串中服务器信息 Display the content.

                            ssResponseString = ssResponseString.Remove(
                                0,
                                ssResponseString.IndexOf("<span style=""color:") + 19
                                )
                            ssResponseString = ssResponseString.Remove(
                                0,
                                ssResponseString.IndexOf(""">") + 2
                                )
                            '删除服务器状态前的所有内容

                            labelServerWrite.Text += "-" +
                                TranslateENtoCN(
                                    ssResponseString.Substring(
                                        0,
                                        ssResponseString.IndexOf("</span>")
                                        ),
                                    labelServerWrite
                                    )
                            '截取字符串中服务器状态 Display the content.

                        Else
                            '将panel中剩余label清空
                            labelServerWrite.ForeColor = PanelServerStatus.ForeColor
                            labelServerWrite.Text = ""
                            ToolTip1.SetToolTip(
                                labelServerWrite,
                                ""
                                )
                        End If
                    End If
                Next

                ssResponseString = Nothing  '清空字符串，释放内存。

                serverStatusTimer = serverStatusTimerMax      '查询完成后，将倒计时设为最大间隔，启动循环计时

            End If
        Catch ex As Exception
            Try
                serverStatusTimer = 0      '查询出错，可立即再次查询

                For Each labelServerError In PanelServerStatus.Controls
                    '不可在变量中直接定义为label，会将容器中所有对象错误转换成label并出错。
                    If TypeOf (labelServerError) Is Label Then
                        '将panel中所有label清空
                        labelServerError.ForeColor = PanelServerStatus.ForeColor
                        labelServerError.Text = ""
                    End If
                Next
                If ex.Message.Contains("已取消一个任务。") _
                    Or ex.Message.Contains("A task was canceled.") _
                    Then
                    LabelServer1.Text = "美服状态查询超时。"
                Else
                    LabelServer1.Text = "美服状态查询出错。"
                    If ex.InnerException Is Nothing Then
                        '若包含InnerException异常，则显示详细信息
                        ToolTip1.SetToolTip(LabelServer1, ex.Message)
                    Else
                        ToolTip1.SetToolTip(LabelServer1, ex.InnerException.Message)
                    End If
                End If
            Catch exINex As Exception
                MsgBox(exINex.Message, MsgBoxStyle.Exclamation, "QueryServerStatusException Error")
            End Try
        Finally
            If ssStreamReader IsNot Nothing Then ssStreamReader.Dispose()
            If ssStream IsNot Nothing Then ssStream.Dispose()

            If ssHttpClient IsNot Nothing Then ssHttpClient.Dispose()

            '清空缓存，重新获取网页信息？断网时会触发null错误
            '虽然貌似close也会调用dispose，但是实测单独使用close无法刷新，只能用dispose？
            '调用Close函数释放资源后可能还需要再次使用，而Dispose函数释放的资源不再使用？
        End Try
    End Sub

    Private Function TranslateENtoCN(ENString As String, Optional ByVal ControlLabel As Control = Nothing) As String
        '将英文字符ENString翻译成中文。若带入控件名ControlLabel，则将该控件字色相应修改
        Try
            Dim ControlLabelExists As Boolean = ControlLabel IsNot Nothing
            '判断函数是否引入了控件名，未引入控件名时不执行修改控件字色的功能

            Select Case ENString
                Case "UP"
                    If ControlLabelExists Then
                        ControlLabel.ForeColor = ServerStatusColor.SSCup
                        ToolTip1.SetToolTip(
                            ControlLabel,
                            "服务器在线"
                            )
                    End If
                    Return "在线"
                Case "LOCKED"
                    If ControlLabelExists Then
                        ControlLabel.ForeColor = ServerStatusColor.SSClock
                        ToolTip1.SetToolTip(
                            ControlLabel,
                            "服务器已锁定，玩家无法登录游戏，但服务器中的玩家仍可继续游戏"
                            )
                    End If
                    Return "锁定"
                Case "MAINT"
                    If ControlLabelExists Then
                        ControlLabel.ForeColor = ServerStatusColor.SSCmaintain
                        ToolTip1.SetToolTip(
                            ControlLabel,
                            "服务器正在维护，无法登录"
                            )
                    End If
                    Return "维护"
                Case "DOWN"
                    If ControlLabelExists Then
                        ControlLabel.ForeColor = ServerStatusColor.SSCdown
                        ToolTip1.SetToolTip(
                            ControlLabel,
                            "服务器已停机，无法登录"
                            )
                    End If
                    Return "停机"

                Case "Briggs (AU)"
                    Return "布里格斯(澳洲)"
                Case "Cobalt (EU)"
                    Return "柯伯特(欧洲)"
                Case "Connery (US West)"
                    Return "康纳利(美西)"
                Case "Emerald (US East)"
                    Return "艾莫若德(美东)"
                Case "Miller (EU)"
                    Return "米勒(欧洲)"
                Case "Jaeger (US East)"
                    Return "猎人(美东)"

                Case Else
                    If ControlLabelExists Then
                        ControlLabel.ForeColor = ServerStatusColor.SSCdown
                    End If
                    Return ENString
            End Select
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            If LabelStatus.ForeColor = Color.Orange Then
                LabelStatus.ForeColor = Color.Gold
            Else
                LabelStatus.ForeColor = Color.Orange
            End If
            '提示框文字每秒黄绿交替闪烁

            If formActive OrElse doubleTime Then
                RunOrShowButton(False)    '更新显示按钮状态
                doubleTime = False
            Else
                doubleTime = True
            End If

            Try
                If PanelPingTester.Visible _
                    AndAlso serverIP <> "0" Then
                    '测Ping功能启用
                    '且服务器选项也启用了测ping功能

                    sendPing.SendAsync(serverIP, 1300, serverIP)    'ping指定的服务器IP，timeout=1300ms，传递给异步接收事件的参数为serverIP(未使用)
                End If
            Catch exSendPingConfilict As InvalidOperationException
                '屏蔽“调用正在进行”错误

            Catch exSendPing As Exception
                MsgBox(exSendPing.Message, MsgBoxStyle.Exclamation, "SendPing Error")
                '屏蔽所有ping的意外错误，例如断网
            End Try

            Select Case ipLocTimer     '查询ip所在地
                Case Is > ipLocTimerMax     '当计时大于最大值时，暂停即时。查询完成后，将倒计时设为最大间隔，启动循环计时

                Case 0     '设定查询ip所在地的间隔，间隔时间为此处的数值乘以timer1.interval
                    '取消自动查询IP归属
                    '功能更改，每次激活窗体，且间隔大于2分钟才自动检测，故取消显示倒计时。
                    'LabelLocStatus.Text = "点击上方黄字，可手动检测IP所属地区，" & Chr(13) & Chr(10) & ipLocTimer & "秒后自动检测。"

                    'If formActive Then  '仅当计时到60，并且窗口激活的情况下才查询归属地并继续循环
                    '    QueryIPLoc()    '查询显示IP归属地，手动-1已包含于此函数中，设置时间为59s
                    'End If

                Case Is < 0             '出现负数为出错情况，重置为0
                    ipLocTimer = 0

                Case Is = ipLocTimerMax - 30    '每次查询之后30秒，将提示信息恢复默认。避免用户被残留的VPN信息误导。
                    If ToolTip1.GetToolTip(LabelLocStatus) = "点击上方黄字，可手动检测IP归属地。" Then
                        '仅当上次查询成功，符合ToolTip1特征时，才将上次查询结果转到ToolTip1里
                        ToolTip1.SetToolTip(LabelLocStatus, LabelLocStatus.Text)
                    End If
                    LabelLocStatus.Text = "以上为上次查询结果，若VPN重新连接，" & Chr(13) & Chr(10) & "可点击上方黄字，手动检测IP所属地区。"
                    ipLocTimer -= 1

                Case Else       '60-50之间，保持IP归属地查询信息，仅计数。
                    ipLocTimer -= 1
            End Select


            Select Case serverStatusTimer     '查询美服状态
                Case Is > serverStatusTimerMax     '当计时大于最大值时，暂停即时。查询完成后，将倒计时设为最大间隔，启动循环计时

                Case 0     '设定查询ip所在地的间隔，间隔时间为此处的数值乘以timer1.interval

                Case Is < 0     '若出现负数，为出错情况，重置为0
                    serverStatusTimer = 0

                Case Else       '倒计时。
                    serverStatusTimer -= 1
            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Timer1_Tick Error")
        End Try
    End Sub

    Private Sub ButtonRunClick()
        '按钮按下
        Try
            RunOrShowButton(True)       '执行按钮功能

            RunOrShowButton(False)      '更新显示按钮状态

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ButtonRunClick Error")
        End Try
    End Sub

    Private Sub LabelManuallyTranslate_Click(sender As Object, e As EventArgs) Handles LabelManuallyTranslate.Click
        Try
            RunModify(True)    '手动执行汉化

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelManuallyTranslate_Click Error")
        End Try
    End Sub

    Private Sub LabelKillLaunchPad_Click(sender As Object, e As EventArgs) Handles LabelKillLaunchPad.Click
        Try
            KillLaunchPad()    '结束LaunchPad进程

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelKillLaunchPad_Click Error")
        End Try
    End Sub

    Private Sub LabelKillGameClient_Click(sender As Object, e As EventArgs) Handles LabelKillGameClient.Click
        Try
            KillGameClient()    '结束PlanetSide 2_64.exe进程
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelKillGameClient_Click Error")
        End Try
    End Sub

    Private Sub LabelRun_Click(sender As Object, e As EventArgs) Handles LabelRun.Click
        ButtonRunClick()        '按钮按下
    End Sub

    Private Sub ButtonRun_Click(sender As Object, e As EventArgs) Handles ButtonRun.Click
        ButtonRunClick()        '按钮按下
    End Sub

    Private Sub ButtonPicStandard()
        '显示按钮正常时图片
        Try
            LabelRun.Image = My.Resources.开始正常1     '显示按钮正常时图片

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ButtonPicStandard Error")
        End Try
    End Sub

    Private Sub ButtonPicMouseOver()
        '显示按钮悬停时图片
        Try
            LabelRun.Image = My.Resources.开始按下1     '显示按钮悬停时图片

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ButtonPicMouseOver Error")
        End Try
    End Sub

    Private Sub LabelRun_MouseHover(sender As Object, e As EventArgs) Handles LabelRun.MouseHover
        ButtonPicMouseOver()    '显示按钮悬停时图片

        ButtonRun.Focus()       '获得焦点，以便键盘操作
    End Sub

    Private Sub LabelRun_MouseLeave(sender As Object, e As EventArgs) Handles LabelRun.MouseLeave
        ButtonPicStandard()     '显示按钮正常时图片
    End Sub

    Private Sub ButtonRun_GotFocus(sender As Object, e As EventArgs) Handles ButtonRun.GotFocus
        ButtonPicMouseOver()    '显示按钮悬停时图片
    End Sub

    Private Sub ButtonRun_LostFocus(sender As Object, e As EventArgs) Handles ButtonRun.LostFocus
        ButtonPicStandard()     '显示按钮正常时图片
    End Sub

    Private Sub LabelDir_Click(sender As Object, e As EventArgs) Handles LabelDir.Click
        Try
            SetLaunchPadLocation()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelDir_Click Error")
        End Try
    End Sub

    Private Sub ButtonOpenFile_Click(sender As Object, e As EventArgs) Handles ButtonOpenFile.Click
        Try
            SetLaunchPadLocation()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ButtonOpenFile_Click Error")
        End Try
    End Sub

    Private Sub LabelSteam_Click(sender As Object, e As EventArgs) Handles LabelSteam.Click
        Try
            If SteamHelp.Visible Then
                '判断steam用户帮助的窗体状态
                SteamHelp.Close()   '关闭steam用户使用帮助图
            Else
                SteamHelp.Show()    '显示steam用户使用帮助图
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelSteam_Click Error")
        End Try
    End Sub

    Private Sub LabelLosePkg_Click(sender As Object, e As EventArgs) Handles LabelLosePkg.Click
        Try
            sendPing.SendAsyncCancel()      '取消当前正在等待接收的sendping，需在ping.complete事件中添加关于getPing.Cancelled判断

            ResetLosePKG()      '丢包率统计清零

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelLosePkg_Click Error")
        End Try
    End Sub

    Private Sub LabelIPLoc_Click(sender As Object, e As EventArgs) Handles LabelIPLoc.Click
        Try

            If ipLocTimer > ipLocTimerMax - ipLocTimerMin Then     '距上次查询还未经过设定的最小间隔时间

                If ToolTip1.GetToolTip(LabelLocStatus) = "点击上方黄字，可手动检测IP归属地。" Then
                    '仅当上次查询成功，符合ToolTip1特征时，才将上次查询结果转到ToolTip1里

                    ToolTip1.SetToolTip(LabelLocStatus, LabelLocStatus.Text)
                End If

                LabelLocStatus.Text = "IP归属地查询最小间隔为" & ipLocTimerMin & "秒。" & Chr(13) & Chr(10) & "请稍候再试。"
            End If
            '以上If段落必须位于查询语句之前，否则查询时会修改ipLocTimer，导致本段落的操作错误执行。

            QueryIPLoc(ipLocTimerMin)        '查询IP归属，设定距离上次查询已经过最小间隔时间

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelIPLoc_Click Error")
        End Try
    End Sub

    Private Sub LabelLogo_Click(sender As Object, e As EventArgs) Handles LabelLogo.Click
        Try
            Process.Start("https://github.com/XhYde/PS2CN-Launcher/wiki")
            '打开浏览器，访问论坛
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "VisitPS2CN Error")
        End Try
    End Sub

    Private Sub LabelVisitOfficialSite_Click(sender As Object, e As EventArgs) Handles LabelVisitOfficialSite.Click
        Try
            Process.Start("https://www.planetside2.com/home")
            '打开浏览器，访问PlanetSide2.com英文官网
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "VisitOfficialSite Error")
        End Try
    End Sub

    Private Sub LabelVisitReddit_Click(sender As Object, e As EventArgs) Handles LabelVisitReddit.Click
        Try
            Process.Start("https://www.reddit.com/r/Planetside/")
            '打开浏览器，访问reddit.com/r/Planetside/英文论坛
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "VisitReddit Error")
        End Try
    End Sub
    Private Sub LabelGErrorCodeQuery_Click(sender As Object, e As EventArgs) Handles LabelGErrorCodeQuery.Click
        Try
            Process.Start("https://help.daybreakgames.com/hc/zh-cn/articles/230631147--H1Z1-%E4%B8%AD%E7%9A%84G%E9%94%99%E8%AF%AF%E4%BB%A3%E7%A0%81%E4%BB%A3%E8%A1%A8%E4%BB%80%E4%B9%88%E5%90%AB%E4%B9%89-/")
            '打开浏览器，访问H1Z1的登录器G错误代码含义说明页面，H1Z1与PS2相通。
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GErrorCodeQuery Error")
        End Try
    End Sub

    Private Sub LabelPatchNote_Click(sender As Object, e As EventArgs) Handles LabelPatchNote.Click
        Try
            Process.Start("http://www.ps2cn.com/forum.php?mod=forumdisplay&fid=48&orderby=dateline")
            '打开浏览器，访问PS2CN游戏更新补丁说明区，按发布日期排序
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckPatchNote Error")
        End Try
    End Sub

    Private Sub LabelDownloadGameUpdate_Click(sender As Object, e As EventArgs) Handles LabelDownloadGameUpdate.Click
        Try
            Process.Start("http://www.ps2cn.com/forum.php?mod=viewthread&tid=2283")
            '打开浏览器，访问PS2CN游戏更新下载页
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "VisitGameUpdateDownloadPage Error")
        End Try
    End Sub

    Private Sub LabelPlayerName_MouseClick(sender As Object, e As MouseEventArgs) Handles LabelPlayerName.MouseClick
        '将Label伪装成TextBox的文本框部分，点击Label，相当于点击TextBox

        Try
            LabelPlayerName.Visible = False         '隐藏上层Label，显示下层TextBox
            TextBoxPlayerName.Focus()               '让TextBox获得焦点，输入光标转到TextBox上
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelPlayerName.MouseClick Error")
        End Try
    End Sub

    Private Sub TextBoxPlayerName_GotFocus(sender As Object, e As EventArgs) Handles TextBoxPlayerName.GotFocus
        '玩家名称输入框获得焦点时，（比如用户点击了TextBox的边框），同样要隐藏上层Label

        Try
            LabelPlayerName.Visible = False         '隐藏上层Label，显示下层TextBox

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "TextBoxPlayerName.GotFocus Error")
        End Try
    End Sub

    Private Sub TextBoxPlayerName_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPlayerName.LostFocus
        '玩家名称输入框失去焦点时，（比如用户切换到了别的窗口），同样要隐藏上层Label

        Try
            If TextBoxPlayerName.Text = Nothing Or TextBoxPlayerName.Text = "" Then
                '玩家名称输入框内容为空时，显示提示语句
                LabelPlayerName.Visible = True         '显示上层Label的提示语句

            Else
                '玩家名称输入框有内容时，隐藏上层Label，显示下层TextBox
                LabelPlayerName.Visible = False         '隐藏上层Label，显示下层TextBox

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "TextBoxPlayerName.LostFocus Error")
        End Try

    End Sub

    Private Sub TextBoxPlayerName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPlayerName.TextChanged
        '玩家名称输入框内容发生改变，且内容为空时，字色改为灰色Silver，并显示提示语句

        Try
            If TextBoxPlayerName.Text = Nothing Or TextBoxPlayerName.Text = "" Then
                '玩家名称输入框内容为空时，显示提示语句
                LabelPlayerName.Visible = True         '显示上层Label的提示语句

            Else
                '玩家名称输入框有内容时，隐藏上层Label，显示下层TextBox
                LabelPlayerName.Visible = False         '隐藏上层Label，显示下层TextBox

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "TextBoxPlayerName.TextChanged Error")
        End Try
    End Sub

    Private Sub LabelPlayerStatistics_Click(sender As Object, e As EventArgs) Handles LabelPlayerStatistics.Click
        Try
            If TextBoxPlayerName.Text = Nothing Or TextBoxPlayerName.Text = "" Then
                '玩家名称输入框内容为空时，打开浏览器，直接打开数据查询网站主页
                Process.Start("http://ps2.fisu.pw/player/")

            Else
                '玩家名称输入框有内容时，打开浏览器，直接查询玩家数据
                Process.Start("http://ps2.fisu.pw/player/?name=" & TextBoxPlayerName.Text)

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckPlayerStatistics Error")
        End Try

    End Sub

    Private Sub LabelPlayerKillboard_Click(sender As Object, e As EventArgs) Handles LabelPlayerKillboard.Click
        Try
            If TextBoxPlayerName.Text = Nothing Or TextBoxPlayerName.Text = "" Then
                '玩家名称输入框内容为空时，打开浏览器，直接打开玩家击杀记录主页
                Process.Start("http://ps2.fisu.pw/killboard/")

            Else
                '玩家名称输入框有内容时，打开浏览器，直接查询玩家击杀记录
                Process.Start("http://ps2.fisu.pw/killboard/?name=" & TextBoxPlayerName.Text)

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckPlayerKillboard Error")
        End Try
    End Sub

    Private Sub LabelReadme_Click(sender As Object, e As EventArgs) Handles LabelReadme.Click
        Try
            Process.Start(Application.StartupPath & "\PS2CN汉化器-使用说明.txt")         '打开使用说明
            'Shell("explorer.exe """ & Application.StartupPath & "\PS2CN汉化器-使用说明.txt""", AppWinStyle.NormalFocus)    '打开使用说明
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "OpenReadme Error")
        End Try
    End Sub

    Private Sub LabelUpdate_Click(sender As Object, e As EventArgs) Handles LabelUpdate.Click
        Try
            Process.Start("https://pan.baidu.com/s/1qWUf0VM")
            '打开浏览器，访问汉化器发布页
            'ps2cn暂时休站，临时改用百毒网盘发布


            '更新按钮提示信息精简，取消重新查询功能
            'If LabelUpdate.Text.StartsWith("版本更新查询") Then
            '    '若版本查询出错，错误反馈的开头都是"版本更新查询"。
            '    '若查询有问题，则点击按钮改为重新查询
            '    LabelUpdate.Text = "版本更新查询中……"
            '    ToolTip1.SetToolTip(LabelUpdate, "请稍候。")
            '    CheckVersion()
            'ElseIf LabelUpdate.Text = "版本更新查询中……" Then
            '    '查询中屏蔽任何操作，直到查询返回结果
            'Else
            '    Process.Start("https://pan.baidu.com/s/1qWUf0VM")
            '    '打开浏览器，访问汉化器发布页

            '    'Process.Start("http://www.ps2cn.com/forum.php?mod=viewthread&tid=973")
            '    'ps2cn暂时休站，临时改用百毒网盘发布
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckVersion Error")
        End Try
    End Sub

    Private Sub ComboBoxCN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCN.SelectedIndexChanged
        'SelectionChangeCommitted事件，由用户鼠标左键确定后才选择触发 / 控件获得focus，并且droplist未展开时，方向键的选择可触发。
        '若droplist展开，用方向键改变选项，然后直接点击别处，让ComboBox失去焦点，此时选项已改变，但是不触发SelectionChangeCommitted事件……
        'SelectedIndexChanged事件，任何情况下只要选项变化，就会触发。还会捕捉编程方式的选择更改？

        Try
            If ComboBoxCN.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                If ComboBoxCN.SelectedIndex = 1 Then
                    My.Settings.CheckedCN = True
                Else
                    My.Settings.CheckedCN = False
                End If
                '将程序设置值，相应更改

                'My.Settings.Save()      '保存程序设定

                RunOrShowButton(False)        '更新按钮状态

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxCN_SelectedIndexChanged Error")
        End Try
    End Sub

    'Private Sub CheckBoxCN_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCN.CheckedChanged
    '    Try
    '        If CheckBoxCN.Focused Then
    '            '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

    '            My.Settings.CheckedCN = CheckBoxCN.Checked      '将程序设置值，相应更改
    '            'My.Settings.Save()      '保存程序设定

    '            RunOrShowButton(False)        '更新按钮状态

    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckBoxCN_CheckedChanged Error")
    '    End Try
    'End Sub

    Private Sub CheckBoxAutoRestore_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAutoRestore.CheckedChanged
        Try
            If CheckBoxAutoRestore.Focused Then
                '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

                My.Settings.CheckedAutoRestore = CheckBoxAutoRestore.Checked    '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定

                RunOrShowButton(False)        '更新按钮状态
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckBoxAutoRestore_CheckedChanged Error")
        End Try
    End Sub

    Private Sub CheckBox32bit_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox32bit.CheckedChanged
        Try
            If CheckBox32bit.Focused Then
                '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

                My.Settings.Checked32bit = CheckBox32bit.Checked    '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定

                RunOrShowButton(False)        '更新按钮状态
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckBox32bit_CheckedChanged Error")
        End Try
    End Sub

    Private Sub CheckBoxENVoice_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxENVoice.CheckedChanged
        Try
            If CheckBoxENVoice.Focused Then
                '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

                My.Settings.CheckedENVoice = CheckBoxENVoice.Checked
                'My.Settings.Save()

                SetClientType()     '根据客户端不同，修改相关内存变量及参数，用于条件判断

                RunOrShowButton(False)        '更新按钮状态
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckBoxENVoice_CheckedChanged Error")
        End Try
    End Sub

    Private Sub ButtonFixLauncher503_Click(sender As Object, e As EventArgs) Handles ButtonFixLauncher503.Click
        '修复登录器Launcher的4-503错误
        Try
            If MsgBox(
                "登录器LaunchPad出现4-503错误的【根本原因】是【网络差】，请先【确保网络通畅】。网络正常后，应可自动修复错误。" & Chr(13) & Chr(10) &
                "仅当4-503错误【总是出现】时，才需执行修复。" & Chr(13) & Chr(10) &
                Chr(13) & Chr(10) &
                "修复就是删除 [PlanetSide 2]\LaunchPad.libs\LaunchPad.Cache 文件夹下的所有缓存文件，让登录器重新下载它们。" & Chr(13) & Chr(10) &
                "修复不会操作 Cookies 文件夹，此文件夹加密保存了登录器记住的PS2登录账号密码。" &
                "登录器应该不会遗失账号密码。" & Chr(13) & Chr(10) &
                Chr(13) & Chr(10) &
                "请先确认需要修复的【客户端类型】及【exe程序位置】。" & Chr(13) & Chr(10) &
                Chr(13) & Chr(10) &
                "是否要【执行修复】？" _
                , MsgBoxStyle.Question + MsgBoxStyle.OkCancel _
                , "修复登录器4-503错误") _
            = MsgBoxResult.Ok _
            AndAlso TestDir(True) _
            Then
                '弹出消息框，仅当点击确认后，继续执行
                '继续判断launchPad路径是否合法，合法才继续执行

                Dim launchPadCacheFiles As String() =
                    Directory.GetFiles(
                    launchPad.DirectoryName & "\LaunchPad.libs\LaunchPad.Cache",
                    "*",
                    SearchOption.TopDirectoryOnly)
                '列出登录器LaunchPad缓存目录下所有缓存文件，仅搜索首层，不搜索子文件夹。

                For Each launchPadCacheFile As String In launchPadCacheFiles
                    '对搜索得到的每个文件名执行删除

                    If File.Exists(launchPadCacheFile) Then
                        '检查文件是否存在
                        '若路径无效/不存在/字符串错误，都返回false；权限不足也返回false

                        File.Delete(launchPadCacheFile)
                        '删除此文件
                        '文件不存在不报错，但是路径不存在会报错…… 必须先检查路径和文件是否存在

                    End If
                Next

                launchPadCacheFiles =
                    Directory.GetFiles(
                    launchPad.DirectoryName & "\LaunchPad.libs\LaunchPad.Cache",
                    "*",
                    SearchOption.TopDirectoryOnly)
                '再次校验文件夹是否已清空，并弹出修复完成提示。

                If launchPadCacheFiles.Length = 0 Then
                    '若查询文件夹下文件，返回的数组内元素个数为0，说明文件夹中无文件（顶层）
                    MsgBox("修复完成。", MsgBoxStyle.Information, "修复登录器4-503错误")
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ButtonFixLauncher503_Click Error")
        End Try
    End Sub

    Private Sub LabelComboBoxFontSelector_Click(sender As Object, e As EventArgs) Handles LabelComboBoxFontSelector.Click
        '将Label伪装成ComboBox的文本框部分，点击Label，相当于点击ComboBox
        Try
            ComboBoxFontSelector.Focus()        '让下层ComboBox获得焦点
            ComboBoxFontSelector.DroppedDown = True     '打开ComboBox的DroppedDownList
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "LabelComboBoxFontSelector_Click Error")
        End Try
    End Sub

    Private Sub ComboBoxFontSelector_DropDown(sender As Object, e As EventArgs) Handles ComboBoxFontSelector.DropDown
        '每次点开下拉菜单
        Try
            BuildComboBoxFontSelectorList()
            '重新读取自定义字体文件夹中的字体文件，生成新的下拉菜单，并将text设为保存的设置值
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxFontSelector_DropDown Error")
        End Try
    End Sub

    Private Sub ComboBoxFontSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFontSelector.SelectedIndexChanged
        'SelectionChangeCommitted事件，由用户鼠标左键确定后才选择触发 / 控件获得focus，并且droplist未展开时，方向键的选择可触发。
        '若droplist展开，用方向键改变选项，然后直接点击别处，让ComboBox失去焦点，此时选项已改变，但是不触发SelectionChangeCommitted事件……
        'SelectedIndexChanged事件，任何情况下只要选项变化，就会触发。还会捕捉编程方式的选择更改？

        Try
            LabelComboBoxFontSelector.Text = ComboBoxFontSelector.Text
            '用Label伪装ComboBox的文本框，无论什么情况(包括主窗体加载时，和程序修改)Index更改，都要随之更改

            If ComboBoxFontSelector.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                My.Settings.FontSelector = ValidateAndSetFontSelection(ComboBoxFontSelector.Text)
                '当combobox的text中的字体文件存在时，将设置设为该值。否则设为默认

                'My.Settings.Save()

                RunOrShowButton(False)        '更新按钮状态

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxFontSelector_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub ComboBoxServer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxServer.SelectedIndexChanged
        'SelectionChangeCommitted事件，由用户鼠标左键确定后才选择触发 / 控件获得focus，并且droplist未展开时，方向键的选择可触发。
        '若droplist展开，用方向键改变选项，然后直接点击别处，让ComboBox失去焦点，此时选项已改变，但是不触发SelectionChangeCommitted事件……
        'SelectedIndexChanged事件，任何情况下只要选项变化，就会触发。还会捕捉编程方式的选择更改？

        Try
            If ComboBoxServer.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                My.Settings.PingServer = ComboBoxServer.SelectedIndex        '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定

                SelectIP()  '根据设置值，将服务器IP定义到字符串serverIP中

                sendPing.SendAsyncCancel()      '取消当前正在等待接收的sendping，需在ping.complete事件中添加关于getPing.Cancelled判断

                ResetLosePKG()      '丢包率统计清零
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxServer_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub ComboBoxClientType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxClientType.SelectedIndexChanged
        'SelectionChangeCommitted事件，由用户鼠标左键确定后才选择触发 / 控件获得focus，并且droplist未展开时，方向键的选择可触发。
        '若droplist展开，用方向键改变选项，然后直接点击别处，让ComboBox失去焦点，此时选项已改变，但是不触发SelectionChangeCommitted事件……
        'SelectedIndexChanged事件，任何情况下只要选项变化，就会触发。还会捕捉编程方式的选择更改？

        Try

            If ComboBoxClientType.Focused Then
                '程序修改本控件的数值时，包括初始主窗体加载时，也会触发SelectedIndexChanged事件…… 必须先判断本控件当前是否获得焦点

                My.Settings.ClientType = ClientTypeSelectedIndexToSetting(ComboBoxClientType.SelectedIndex)        '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定

                SetClientType()     '根据客户端不同，修改相关内存变量及参数，用于条件判断
                '必须写在修改Settings之后。
                '必须写在TestDir之前。测TestDir之前，先要将更改后的客户端类型，写入内存里客户端类型的变量

                If My.Settings.ClientType <> ClientTypeInSettings.STEAM Then
                    '若客户端为Steam版，不检测路径是否有效，可由Steam://rungameid命令启动

                    TestDir(True)           '测试设定的客户端位置是否有效
                    '先判断本控件是否Focused，可以避免未设置路径的童鞋，在打开程序后，立即弹出对话框要求选择路径
                End If


                RunOrShowButton(False)        '更新显示按钮状态
                '应写在TestDir之后。若路径不合法，会弹出对话框，若用户指定了合法路径，再判断按钮状态，可以更准确。
                '不可写到判断语句以外。程序加载时会修改Index，但此时不应调用本函数。函数中有TestDir，LaunchPad路径变量未定义，会出错。

            End If

            'QueryIPLoc()        '查询显示IP归属地。此处不再查询，减轻负载
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxClientType_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub ComboBoxTimeZone1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxTimeZone1.SelectedIndexChanged
        'SelectionChangeCommitted事件，由用户鼠标左键确定后才选择触发 / 控件获得focus，并且droplist未展开时，方向键的选择可触发。
        '若droplist展开，用方向键改变选项，然后直接点击别处，让ComboBox失去焦点，此时选项已改变，但是不触发SelectionChangeCommitted事件……
        'SelectedIndexChanged事件，任何情况下只要选项变化，就会触发。还会捕捉编程方式的选择更改？

        Try
            If ComboBoxTimeZone1.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                My.Settings.TimeZone1 = ComboBoxTimeZone1.SelectedIndex     '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(My.Settings.TimeSelection)      '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxTimeZone1_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub ComboBoxTimeZone2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxTimeZone2.SelectedIndexChanged
        'SelectionChangeCommitted事件不可靠，理由见上方ComboBox_SelectedIndexChanged内的注释

        Try
            If ComboBoxTimeZone2.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                My.Settings.TimeZone2 = ComboBoxTimeZone2.SelectedIndex     '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(My.Settings.TimeSelection)      '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxTimeZone2_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub ComboBoxTimeZone3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxTimeZone3.SelectedIndexChanged
        'SelectionChangeCommitted事件不可靠，理由见上方ComboBox_SelectedIndexChanged内的注释

        Try
            If ComboBoxTimeZone3.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                My.Settings.TimeZone3 = ComboBoxTimeZone3.SelectedIndex     '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(My.Settings.TimeSelection)      '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxTimeZone3_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub ComboBoxHour1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxHour1.SelectedIndexChanged
        'SelectionChangeCommitted事件不可靠，理由见上方ComboBox_SelectedIndexChanged内的注释

        Try
            If ComboBoxHour1.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                My.Settings.TimeSelection = 0       '以此时间为基准时间
                My.Settings.TimeHour = ComboBoxHour1.SelectedIndex     '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(0)          '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxHour1_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub ComboBoxHour2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxHour2.SelectedIndexChanged
        'SelectionChangeCommitted事件不可靠，理由见上方ComboBox_SelectedIndexChanged内的注释

        Try
            If ComboBoxHour2.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                My.Settings.TimeSelection = 1       '以此时间为基准时间
                My.Settings.TimeHour = ComboBoxHour2.SelectedIndex     '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(1)          '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxHour2_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub ComboBoxHour3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxHour3.SelectedIndexChanged
        'SelectionChangeCommitted事件不可靠，理由见上方ComboBox_SelectedIndexChanged内的注释

        Try
            If ComboBoxHour3.Focused Then
                '程序修改本控件的数值时，也会触发SelectedIndexChanged事件……必须先判断本控件当前是否获得焦点，是否是用户手动操作

                My.Settings.TimeSelection = 2       '以此时间为基准时间
                My.Settings.TimeHour = ComboBoxHour3.SelectedIndex     '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(2)          '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxHour3_SelectedIndexChanged Error")
        End Try
    End Sub

    Private Sub CheckBoxDST1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDST1.CheckedChanged
        Try
            If CheckBoxDST1.Focused Then
                '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

                My.Settings.CheckedDST1 = CheckBoxDST1.Checked      '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(My.Settings.TimeSelection)      '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckBoxDST1_CheckedChanged Error")
        End Try
    End Sub

    Private Sub CheckBoxDST2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDST2.CheckedChanged
        Try
            If CheckBoxDST2.Focused Then
                '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

                My.Settings.CheckedDST2 = CheckBoxDST2.Checked      '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(My.Settings.TimeSelection)      '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckBoxDST2_CheckedChanged Error")
        End Try
    End Sub

    Private Sub CheckBoxDST3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDST3.CheckedChanged
        Try
            If CheckBoxDST3.Focused Then
                '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

                My.Settings.CheckedDST3 = CheckBoxDST3.Checked      '将程序设置值，相应更改
                'My.Settings.Save()      '保存程序设定
                ConvertTime(My.Settings.TimeSelection)      '转换时区时间
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckBoxDST3_CheckedChanged Error")
        End Try
    End Sub

    Private Sub PanelServerStatus_VisibleChanged(sender As Object, e As EventArgs) Handles PanelServerStatus.VisibleChanged
        Try

            Select Case My.Settings.ClientType
                Case ClientTypeInSettings.STEAM, ClientTypeInSettings.EN, ClientTypeInSettings.TEST
                    '当选择美服或测试服，查询美服状态
                    QueryServerStatus()     '查询美服状态
            End Select

            'If My.Settings.ClientType <= ClientTypeInSettings.TEST Then
            '    '当选择美服或测试服，查询美服状态
            '    QueryServerStatus()     '查询美服状态
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "PanelServerStatus_VisibleChanged Error")
        End Try
    End Sub

    Private Sub ButtonRequestServerStatus_Click(sender As Object, e As EventArgs) Handles ButtonRequestServerStatus.Click
        Try
            QueryServerStatus()     '查询美服状态

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ButtonRequestServerStatus_Click Error")
        End Try
    End Sub

    Private Sub ComboBoxFunctionSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFunctionSelector.SelectedIndexChanged
        Try
            If ComboBoxFunctionSelector.Focused Then
                '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

                My.Settings.CheckedShowFunctionUI = True
                '仅当选择了隐藏功能区时，需要将设置项也修改为不显示，其他情况都需要设置为显示。
                '因而此处预先定义为true，而在下方Case Else中单独执行false定义。

                Select Case ComboBoxFunctionSelector.SelectedIndex
                    Case 1
                        My.Settings.FunctionSelector = FunctionSelector.MiscFunctions
                        '下拉菜单第2项，实用功能

                        SettingsToFunctionSelection(FunctionSelector.MiscFunctions)
                        '显示相应功能区

                    Case 2
                        My.Settings.FunctionSelector = FunctionSelector.CheckIPLoc
                        '下拉菜单第3项，检测IP地区

                        SettingsToFunctionSelection(FunctionSelector.CheckIPLoc)
                        '显示相应功能区

                    Case 3
                        My.Settings.FunctionSelector = FunctionSelector.TimeZoneConvertor
                        '下拉菜单第4项，时区换算器

                        SettingsToFunctionSelection(FunctionSelector.TimeZoneConvertor)
                        '显示相应功能区

                        'Case 4
                        '    My.Settings.FunctionSelector = FunctionSelector.ServerStatus
                        '    '下拉菜单第5项，查询服务器状态

                        'SettingsToFunctionSelection(FunctionSelector.ServerStatus)
                        ''显示相应功能区

                    Case Else
                        My.Settings.FunctionSelector = FunctionSelector.HideFunctionUI
                        '下拉菜单第1项，隐藏工具区
                        '错误及默认选项，都指向隐藏工具区

                        My.Settings.CheckedShowFunctionUI = False
                        '若选择隐藏功能区，则将设置项也修改为不显示

                        SettingsToFunctionSelection(FunctionSelector.HideFunctionUI)
                        '显示相应功能区

                End Select

                'My.Settings.Save()      '保存程序设定

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "ComboBoxFunctionSelector_SelectedIndexChanged Error")
        End Try
    End Sub

    'Private Sub CheckBoxShowFunctionUI_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowFunctionUI.CheckedChanged
    '    Try
    '        If CheckBoxShowFunctionUI.Focused Then
    '            '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

    '            My.Settings.CheckedShowFunctionUI = CheckBoxShowFunctionUI.Checked  '根据显示工具区选项状态，保存到用户的设定

    '            ShowOrHideFunctionUI()      '显示/隐藏工具区
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "CheckBoxShowFunctionUI_CheckedChanged Error")
    '    End Try
    'End Sub

    'Private Sub RadioButtonCheckIPLoc_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonCheckIPLoc.CheckedChanged
    '    Try
    '        If RadioButtonCheckIPLoc.Focused Then
    '            '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

    '            If RadioButtonCheckIPLoc.Checked Then
    '                '仅当勾选时才执行下列语句

    '                My.Settings.FunctionSelector = FunctionSelector.CheckIPLoc   '选择功能为查询IP归属地

    '                SettingsToFunctionSelection(FunctionSelector.CheckIPLoc)

    '                'My.Settings.Save()      '保存程序设定
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "RadioButtonCheckIPLoc_CheckedChanged Error")
    '    End Try
    'End Sub

    'Private Sub RadioButtonTimeZoneConverter_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTimeZoneConverter.CheckedChanged
    '    Try
    '        If RadioButtonTimeZoneConverter.Focused Then
    '            '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

    '            If RadioButtonTimeZoneConverter.Checked Then
    '                '仅当勾选时才执行下列语句

    '                My.Settings.FunctionSelector = FunctionSelector.TimeZoneConvertor   '选择功能为时区转换器

    '                SettingsToFunctionSelection(FunctionSelector.TimeZoneConvertor)

    '                'My.Settings.Save()      '保存程序设定
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "RadioButtonTimeZoneConverter_CheckedChanged Error")
    '    End Try
    'End Sub

    'Private Sub RadioButtonServerStatus_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonServerStatus.CheckedChanged
    '    Try
    '        If RadioButtonServerStatus.Focused Then
    '            '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

    '            If RadioButtonServerStatus.Checked Then
    '                '仅当勾选时才执行下列语句

    '                My.Settings.FunctionSelector = FunctionSelector.ServerStatus        '选择功能为美服状态查询

    '                SettingsToFunctionSelection(FunctionSelector.ServerStatus)

    '                'My.Settings.Save()      '保存程序设定
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "RadioButtonServerStatus_CheckedChanged Error")
    '    End Try
    'End Sub

    'Private Sub RadioButtonMiscFunctions_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonMiscFunctions.CheckedChanged
    '    Try
    '        If RadioButtonMiscFunctions.Focused Then
    '            '程序改变勾选状态也会触发此事件，需要先判断本控件是否获得焦点，以确认是否是用户手动操作

    '            If RadioButtonMiscFunctions.Checked Then
    '                '仅当勾选时才执行下列语句

    '                My.Settings.FunctionSelector = FunctionSelector.MiscFunctions        '选择功能为美服状态查询

    '                SettingsToFunctionSelection(FunctionSelector.MiscFunctions)

    '                'My.Settings.Save()      '保存程序设定
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "RadioButtonMiscFunctions_CheckedChanged Error")
    '    End Try
    'End Sub

    Private Sub LabelLocStatus_Resize(sender As Object, e As EventArgs) Handles LabelLocStatus.Resize
        Try
            '每次状态Label的尺寸（行数行高）发生更改时，相应地将说明label紧贴到状态Label的底部
            LabelCheckIPLocDesc.Top = LabelLocStatus.Bottom     '将说明Label的顶部，紧贴到状态Label的底部来显示
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, " LabelLocStatus_Resize Error")
        End Try
    End Sub

    Private Sub LauncherForm1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick
        '此事件仅用于测试和查错

        Try
            'MsgBox("Critical", MsgBoxStyle.Critical)
            'MsgBox("Exclamation", MsgBoxStyle.Exclamation)
            'MsgBox("Information", MsgBoxStyle.Information)
            'MsgBox("Question", MsgBoxStyle.Question)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, " Test Error")
        End Try

    End Sub

    Private Sub LauncherForm1_Click(sender As Object, e As EventArgs) Handles Me.Click
        ButtonRun.Focus()       '获得焦点，以便键盘操作
    End Sub

    Private Sub LabelSelectClientType_MouseHover(sender As Object, e As EventArgs) Handles LabelSelectClientType.MouseHover
        PanelDir.Visible = True
    End Sub

    Private Sub LabelCN_MouseHover(sender As Object, e As EventArgs) Handles LabelCN.MouseHover
        PanelDir.Visible = True
    End Sub

    Private Sub LabelFontSelector_MouseHover(sender As Object, e As EventArgs) Handles LabelFontSelector.MouseHover
        PanelDir.Visible = True
    End Sub

    Private Sub ComboBoxClientType_MouseHover(sender As Object, e As EventArgs) Handles ComboBoxClientType.MouseHover
        PanelDir.Visible = True
    End Sub

    Private Sub PanelSelectClientTypeDir_MouseHover(sender As Object, e As EventArgs) Handles PanelSelectClientTypeDir.MouseHover
        PanelDir.Visible = True
    End Sub

    Private Sub PanelDir_MouseHover(sender As Object, e As EventArgs) Handles PanelDir.MouseHover
        PanelDir.Visible = True
    End Sub

    Private Sub PanelDir_MouseLeave(sender As Object, e As EventArgs) Handles PanelDir.MouseLeave
        PanelDir.Visible = False
    End Sub

    Private Sub LabelTestDisplay1_Click(sender As Object, e As EventArgs) Handles LabelTestDisplay1.Click
        LabelTestDisplay1.Visible = False
    End Sub

    Private Sub LabelTestDisplay2_Click(sender As Object, e As EventArgs) Handles LabelTestDisplay2.Click
        LabelTestDisplay2.Visible = False
    End Sub

    Private Sub LauncherForm1_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        LabelTestDisplay1.Visible = True
        LabelTestDisplay2.Visible = True
    End Sub

End Class