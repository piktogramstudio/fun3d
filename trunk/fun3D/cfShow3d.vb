Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports Eval3
Imports System.Math
Public Class cfShow3d
    Public angleX As Single = 0.0
    Public angleY As Single = 0.0
    Public angleZ As Single = 0.0
    Public funkcija As String = "0"
    Private vertices2 As CustomVertex.PositionColored()
    Private vertices3 As CustomVertex.PositionColored()
    Private device As Direct3D.Device
    Public xcam As Single = 0
    Public ycam As Single = 0
    Public zcam As Single = 30
    Dim ex As Single = 0
    Dim ey As Single = 0

    Private Sub InitVertices()
        vertices2 = New CustomVertex.PositionColored(2) {}
        vertices2(0).Position = New Vector3(0, 0, 0)
        vertices2(0).Color = Color.White.ToArgb
        vertices2(1).Position = New Vector3(5, 10, 0)
        vertices2(1).Color = Color.White.ToArgb

        vertices3 = New CustomVertex.PositionColored(2) {}
        vertices3(0).Position = New Vector3(5, 10, 0)
        vertices3(0).Color = Color.White.ToArgb
        vertices3(1).Position = New Vector3(10, 0, 0)
        vertices3(1).Color = Color.White.ToArgb
    End Sub

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
        device = New Direct3D.Device(0, DeviceType.Hardware, Me, CreateFlags.SoftwareVertexProcessing, present)


        device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), Me.Width / Me.Height, 1, 50) 'sets field of view, aspect ratio, etc
        device.Transform.View = Matrix.LookAtLH(New Vector3(0, 0, 30), New Vector3(0, 0, 0), New Vector3(0, 1, 0)) 'position and direction

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True) 'Do not draw form?s background\
        Initialize()
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
            Me.Invalidate()
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
            Me.Invalidate()
        End If
    End Sub

    Private Sub cf3D_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If Me.zcam + e.Delta * 0.1 < tfNavigacija.NUDzcam.Maximum And Me.zcam + e.Delta * 0.1 > tfNavigacija.NUDzcam.Minimum Then
            Me.zcam += e.Delta * 0.1
            tfNavigacija.NUDzcam.Value = Me.zcam
        End If
    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        InitVertices()

        device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), Me.Width / Me.Height, 1, 1000) 'sets field of view, aspect ratio, etc
        device.Transform.View = Matrix.LookAtLH(New Vector3(xcam, ycam, zcam), New Vector3(0 + xcam, 0 + ycam, zcam - 10), New Vector3(0, 1, 0)) 'position and direction

        device.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, dfPredefined.Scena.backgroundColor, 1.0, 0)
        Try
            device.BeginScene() 'all drawings after this line

            device.Transform.World = Matrix.RotationYawPitchRoll(angleX * Math.PI / 180, angleY * Math.PI / 180, angleZ * Math.PI / 180)


            dfPredefined.Scena.drawScene(device)



            device.EndScene() 'all drawings before this line

            device.Present()
        Catch ex As Exception
        End Try
    End Sub
End Class