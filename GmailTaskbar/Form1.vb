Imports Gmail.Connector
Imports System.Timers
Imports System.Text
Imports Microsoft.Win32
Imports System.Globalization

Public Class Form1

#Region "Private Variables"
    Private WithEvents tray As NotifyIcon
    Private WithEvents MainMenu As ContextMenuStrip
    Private WithEvents mnuDisplayConfig As ToolStripMenuItem
    Private WithEvents mnuOpenGmail As ToolStripMenuItem
    Private WithEvents mnuCheckGmail As ToolStripMenuItem
    Private WithEvents mnuSep1 As ToolStripSeparator
    Private WithEvents mnuExit As ToolStripMenuItem
    Private Shared aTimer As System.Timers.Timer
    Private Shared closeTimer As System.Timers.Timer
    Private dontExit As Boolean = True
    Private myPath As String = My.Application.Info.DirectoryPath
    Private user As String
    Private psw As String
    Private url As String
    Private timer As Integer
    Private autoStartUp As Boolean
    Private TryConnectionTime As Integer = 5
    Private IsWorking As Boolean = False

#End Region

#Region "Methods"
    Private Sub ApplicationInTray()

        Try
            mnuOpenGmail = New ToolStripMenuItem(My.Resources.Resource.menuOpenMail)
            mnuCheckGmail = New ToolStripMenuItem(My.Resources.Resource.menuCheckMail)
            mnuDisplayConfig = New ToolStripMenuItem(My.Resources.Resource.menuConfiguration)
            mnuSep1 = New ToolStripSeparator()
            mnuExit = New ToolStripMenuItem(My.Resources.Resource.menuExit)
            MainMenu = New ContextMenuStrip
            MainMenu.Items.AddRange(New ToolStripItem() {mnuOpenGmail, mnuCheckGmail, mnuDisplayConfig, mnuSep1, mnuExit})

            tray = New NotifyIcon
            tray.Icon = New System.Drawing.Icon(myPath & "\Icons\senzaPosta.ico")
            tray.Visible = True
            Me.ShowInTaskbar = False
            Me.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckUnreadEmail()
        If IsWorking = False Then
            IsWorking = True

            Dim ed As New Tools.EncryptorDecryptor
            Dim gc As New GmailConnect(user, ed.Decrypt(psw), url)
            Dim gb As feed = gc.Connect

            If Not gb Is Nothing Then
                If gb.error = 0 Then
                    If gb.UnreadedMail = True Then
                        ChangeIcon(True)
                    Else
                        ChangeIcon(False)
                    End If

                    If gb.fullcount > 0 Then
                        Me.Hide()

                        Dim sb As New StringBuilder
                        For Each g As feedEntry In gb.entry
                            Dim title As String = g.title
                            If title.Length > 25 Then
                                title = title.Substring(0, 22) & "..."
                            End If
                            sb.AppendLine(String.Concat(g.author.email, " - ", title))
                        Next
                        If Not String.IsNullOrEmpty(sb.ToString) Then
                            tray.Visible = True
                            tray.BalloonTipIcon = ToolTipIcon.None
                            tray.BalloonTipTitle = String.Format("{0} {1}", gb.fullcount, My.Resources.Resource.ballonUnreadedMsg)
                            tray.BalloonTipText = sb.ToString
                            tray.ShowBalloonTip(5000)
                        End If
                    End If

                ElseIf gb.error = 1 Then
                    If TryConnectionTime = 0 Then
                        MessageBox.Show(My.Resources.Resource.errorConnection)
                        Application.Exit()
                    Else
                        TryConnectionTime -= 1
                    End If
                ElseIf gb.error = 2 Then
                    MessageBox.Show(My.Resources.Resource.errorParseXml)
                    Application.Exit()
                End If
            Else
                MessageBox.Show(My.Resources.Resource.errorGeneric)
                Application.Exit()
            End If
            IsWorking = False
        End If
    End Sub

    Private Sub ChangeIcon(ByVal ConPosta As Boolean)
        If ConPosta = False Then
            tray.Icon = New System.Drawing.Icon(myPath & "\Icons\senzaPosta.ico")
        Else
            tray.Icon = New System.Drawing.Icon(myPath & "\Icons\conPosta.ico")
        End If
    End Sub

    Private Sub readConfigFile()
        Dim ConfigMng As New Tools.ConfigurationManager
        Dim GConfig As GmailConfiguration = ConfigMng.ReadXmlConfig()
        user = GConfig.Username
        psw = GConfig.Password
        url = GConfig.GmailUrl
        timer = GConfig.Timer
        autoStartUp = GConfig.AutoStartUp
    End Sub

    Private Sub SetAutoStart(ByVal isAutomatic As Boolean)
        Try
            If isAutomatic = True Then
                My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
            Else
                My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Form1 Events"
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = dontExit
        Me.Visible = False
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        readConfigFile()
        
        Dim version As String = String.Format(" - Ver. {0}.{1}.{2}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build)
        Me.Text += version
        If user <> "" Then
            ApplicationInTray()
            CheckUnreadEmail()
            Dim intervallo As Integer = 600000 'Default 30 minuti
            If timer <= 0 Then
                intervallo = timer
                intervallo = intervallo * 60000
            End If
            aTimer = New Timer(intervallo) '6000
            aTimer.Enabled = True
            AddHandler aTimer.Elapsed, AddressOf CheckUnreadEmail
        Else
            Dim ed As New Tools.EncryptorDecryptor
            txtGmailUrl.Text = url
            txtPassword.Text = ed.Decrypt(psw)
            txtTimer.Text = timer.ToString
            chkStartUp.Checked = autoStartUp
            ApplicationInTray()
            Me.Visible = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtUsername.Text <> "" And txtPassword.Text <> "" Then
            Dim ed As New Tools.EncryptorDecryptor
            Dim gc As New GmailConfiguration
            Dim configMnr As New Tools.ConfigurationManager
            gc.Username = txtUsername.Text
            gc.Password = ed.Encrypt(txtPassword.Text)
            gc.Timer = txtTimer.Text
            gc.GmailUrl = txtGmailUrl.Text
            gc.AutoStartUp = chkStartUp.Checked
            SetAutoStart(gc.AutoStartUp)
            configMnr.WriteXmlConfig(gc)
            readConfigFile()
        Else
            MessageBox.Show(My.Resources.Resource.configError)
        End If
        Me.Visible = False
        CheckUnreadEmail()
        Dim intervallo As Integer = 600000 'Default 30 minuti
        If timer >= 0 Then
            intervallo = timer * 60000
        End If
        aTimer = New Timer(intervallo)
        aTimer.Enabled = True
        AddHandler aTimer.Elapsed, AddressOf CheckUnreadEmail
    End Sub
#End Region

#Region "Menu Events"
    Private Sub mnuDisplayConfig_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles mnuDisplayConfig.Click
        Dim ed As New Tools.EncryptorDecryptor
        txtUsername.Text = user
        txtPassword.Text = ed.Decrypt(psw)
        txtGmailUrl.Text = url
        txtTimer.Text = timer.ToString
        Me.Visible = True
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuExit.Click
        dontExit = False
        Application.Exit()
    End Sub

    Private Sub tray_Click(sender As Object, e As EventArgs) Handles tray.Click
        MainMenu.Show()
        MainMenu.Left = Screen.PrimaryScreen.Bounds.Width - MainMenu.Width
        MainMenu.Top = Screen.PrimaryScreen.Bounds.Height - MainMenu.Height
    End Sub

    Private Sub mnuOpenGmail_Click(sender As Object, e As EventArgs) Handles mnuOpenGmail.Click
        System.Diagnostics.Process.Start("https://mail.google.com/mail/u/0/?tab=wm#inbox")
    End Sub

    Private Sub mnuCheckGmail_Click(sender As Object, e As EventArgs) Handles mnuCheckGmail.Click
        CheckUnreadEmail()
    End Sub
#End Region

End Class
