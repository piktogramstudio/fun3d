Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports Eval3
Imports System.Math
Public Class cf3D
    Public angleX As Single = 0.0
    Public angleY As Single = 0.0
    Public angleZ As Single = 0.0
    Public funkcija As String = "0"
    Private vertices2 As CustomVertex.PositionColored()
    Private vertices3 As CustomVertex.PositionColored()
    Public device As Direct3D.Device
    Public xcam As Single = 0
    Public ycam As Single = 0
    Public zcam As Single = 30
    Dim ex As Single = 0
    Dim ey As Single = 0

    Public Sub Initialize()
        Dim present As PresentParameters = New PresentParameters
        present.Windowed = True 'we?ll draw on a window
        present.SwapEffect = SwapEffect.Discard 'discuss later
        Dim uDispMode As DisplayMode
        uDispMode = Manager.Adapters.Default.CurrentDisplayMode
        With present
            .BackBufferCount = 3
            .BackBufferFormat = uDispMode.Format
            .BackBufferHeight = uDispMode.Height
            .BackBufferWidth = uDispMode.Width

            .PresentationInterval = PresentInterval.Immediate
            .AutoDepthStencilFormat = DepthFormat.D16
            .EnableAutoDepthStencil = True
        End With
        Dim DevCaps As Direct3D.Caps = Direct3D.Manager.GetDeviceCaps(0, Direct3D.DeviceType.Hardware)
        Dim DevType As Direct3D.DeviceType = Direct3D.DeviceType.Reference
        Dim DevFlags As CreateFlags = CreateFlags.SoftwareVertexProcessing
        If ((DevCaps.VertexShaderVersion >= New Version(2, 0)) And (DevCaps.PixelShaderVersion >= New Version(2, 0))) Then
            DevType = Direct3D.DeviceType.Hardware
            If (DevCaps.DeviceCaps.SupportsHardwareTransformAndLight) Then
                DevFlags = CreateFlags.HardwareVertexProcessing
            End If
        End If
        Try
            device = New Direct3D.Device(0, DeviceType.Hardware, Me, DevFlags, present)
        Catch ex As Exception
            Me.Timer1.Stop()
            If MsgBox(ex.Message + Chr(10) + "Do you want to send error information to author?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            End If
            My.Forms.mf.Close()
        End Try

        'device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), Me.Width / Me.Height, 1, 50) 'sets field of view, aspect ratio, etc
        'device.Transform.View = Matrix.LookAtLH(New Vector3(0, 0, 30), New Vector3(0, 0, 0), New Vector3(0, 1, 0)) 'position and direction

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True) 'Do not draw form?s background\
        Initialize()
    End Sub

    Private Sub cf3D_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
        Me.Timer1.Stop()
        If e.Button = Windows.Forms.MouseButtons.Left Then
            mf.Scena.SelectedObject = mf.Scena
            dfCA.Close()
            mf.Scena.checkClick = True
            mf.Scena.chkx = e.X
            mf.Scena.chky = e.Y
            Me.Timer1_Tick(sender, New EventArgs)
            mf.Scena.checkClick = False
            If mf.Scena.SelectedObject.GetType Is GetType(ClassCA) Then
                mf.CAInspectorToolStripMenuItem_Click(sender, New EventArgs)
            End If
        End If
        Me.Timer1.Start()
    End Sub

    Private Sub cf3D_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        ex = e.X
        ey = e.Y
    End Sub

    Private Sub cf3D_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            Me.xcam += (e.X - ex) * 0.1
            Me.ycam += (e.Y - ey) * 0.1
            ex = e.X
            ey = e.Y
            tfNavigacija.valNUD = False
            tfNavigacija.NUDxcam.Value = Me.xcam
            tfNavigacija.NUDycam.Value = Me.ycam
            tfNavigacija.valNUD = True
        End If
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.angleZ -= (e.X - ex) * 0.1
            Me.angleY += (e.Y - ey) * 0.1
            ex = e.X
            ey = e.Y
            tfNavigacija.valNUD = False
            If Me.angleZ > 359 Or Me.angleZ < -359 Then Me.angleZ = 0
            tfNavigacija.NUDugaoZ.Value = Me.angleZ
            If Me.angleY > 359 Or Me.angleY < -359 Then Me.angleY = 0
            tfNavigacija.NUDugaoY.Value = Me.angleY
            tfNavigacija.valNUD = True
        End If
    End Sub

    Private Sub cf3D_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If Me.zcam + e.Delta * 0.1 < tfNavigacija.NUDzcam.Maximum And Me.zcam + e.Delta * 0.1 > tfNavigacija.NUDzcam.Minimum Then
            Me.zcam += e.Delta * 0.1
            tfNavigacija.NUDzcam.Value = Me.zcam
        End If
    End Sub

    Public Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If mf.Scena.Perspective Then
            device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), Me.Width / Me.Height, 1, 1000) 'sets field of view, aspect ratio, etc
            device.Transform.View = Matrix.LookAtLH(New Vector3(xcam, ycam, zcam), New Vector3(0 + xcam, 0 + ycam, zcam - 10), New Vector3(0, 1, 0)) 'position and direction
        Else
            device.Transform.Projection = Matrix.OrthoOffCenterLH(xcam + Me.Width / 4 * zcam / 400, xcam - Me.Width / 4 * zcam / 400, ycam - Me.Height / 4 * zcam / 400, ycam + Me.Height / 4 * zcam / 400, -1000, 1000) 'sets field of view, aspect ratio, etc
        End If

        device.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, mf.Scena.backgroundColor, 1.0, 0)
        Try
            device.BeginScene() 'all drawings after this line
            device.Transform.World = Matrix.RotationYawPitchRoll(angleX * Math.PI / 180, angleY * Math.PI / 180, angleZ * Math.PI / 180)

            mf.Scena.drawScene(device)

            device.EndScene() 'all drawings before this line
            device.Present()
            'device.Dispose()
        Catch ex As Exception
        End Try
        'Me.Initialize()
    End Sub
    Public Sub saveSlicka(ByVal filename As String)
        Me.Timer1.Stop()
        device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), Me.Width / Me.Height, 1, 1000) 'sets field of view, aspect ratio, etc
        device.Transform.View = Matrix.LookAtLH(New Vector3(xcam, ycam, zcam), New Vector3(0 + xcam, 0 + ycam, zcam - 10), New Vector3(0, 1, 0)) 'position and direction

        device.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, mf.Scena.backgroundColor, 1.0, 0)
        Try
            device.BeginScene() 'all drawings after this line
            device.Transform.World = Matrix.RotationYawPitchRoll(angleX * Math.PI / 180, angleY * Math.PI / 180, angleZ * Math.PI / 180)

            mf.Scena.drawScene(device)
            Select Case filename.Substring(filename.Length - 3, 3)
                Case "jpg"
                    SurfaceLoader.Save(filename, ImageFileFormat.Jpg, device.GetBackBuffer(0, 0, BackBufferType.Mono))
                Case "png"
                    SurfaceLoader.Save(filename, ImageFileFormat.Png, device.GetBackBuffer(0, 0, BackBufferType.Mono))
            End Select
            device.EndScene() 'all drawings before this line
            device.Present()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.Timer1.Start()
    End Sub

End Class