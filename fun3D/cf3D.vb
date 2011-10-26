Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Math
Public Class cf3D
    Public angleX As Single = 0.0
    Public angleY As Single = 0.0
    Public angleZ As Single = 0.0
    Public funkcija As String = "0"
    Private vertices2 As CustomVertex.PositionColored()
    Private vertices3 As CustomVertex.PositionColored()
    Public WithEvents device As Direct3D.Device
    Public xcam As Single = 0
    Public ycam As Single = 0
    Public zcam As Single = 30
    Dim ex As Single = 0
    Dim ey As Single = 0
    Dim timerStoped As Boolean = False
    Public RestartDevice As Boolean = False
    Dim present As PresentParameters
    Dim gettingPoints As Boolean = False
    Dim points As List(Of Vector3)
    Dim funGetPoints As MethodInvoker
    Public Sub Initialize()
        present = New PresentParameters
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
        mf.Scena.HUDList.Add(New ClassHUD(mf.Scena, device))
    End Sub

    Private Sub cf3D_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
        'Me.Timer1.Stop()
        If e.Button = Windows.Forms.MouseButtons.Left Then
            mf.Scena.SelectedObject = mf.Scena
            If dfCA.Visible Then dfCA.Close()
            mf.Scena.checkClick = True
            mf.Scena.chkx = e.X
            mf.Scena.chky = e.Y
            Me.Timer1_Tick(sender, New EventArgs)
            mf.Scena.checkClick = False
            If mf.Scena.SelectedObject.GetType Is GetType(ClassCA) Then
                mf.CAInspectorToolStripMenuItem_Click(sender, New EventArgs)
            End If
        End If
        'Me.Timer1.Start()
    End Sub

    Private Sub cf3D_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        ex = e.X
        ey = e.Y
    End Sub

    Private Sub cf3D_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        If Not Me.timerStoped Then
            Me.Focus()
            Me.Timer1.Start()
        End If
    End Sub

    Private Sub cf3D_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        Me.Timer1.Stop()
    End Sub

    Private Sub cf3D_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

        Dim vsc As Vector3 = mf.Scena.LocMouseHitPlane(Plane.FromPointNormal(New Vector3(0, 0, 0), New Vector3(0, 0, 1)), e.X, e.Y, device)
        mf.tsslCoordinates.Text = "[" + vsc.X.ToString("0.00") + ":" + vsc.Y.ToString("0.00") + ":" + vsc.Z.ToString("0.00") + "]"


        If Not Me.gettingPoints Then
            If e.Button = Windows.Forms.MouseButtons.Middle Then
                Me.xcam += CSng((e.X - ex) * 0.1)
                Me.ycam += CSng((e.Y - ey) * 0.1)
                ex = e.X
                ey = e.Y
                tfNavigacija.valNUD = False
                tfNavigacija.NUDxcam.Value = CDec(Me.xcam)
                tfNavigacija.NUDycam.Value = CDec(Me.ycam)
                tfNavigacija.valNUD = True
            End If
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.angleZ -= CSng((e.X - ex) * 0.1)
                Me.angleY += CSng((e.Y - ey) * 0.1)
                ex = e.X
                ey = e.Y
                tfNavigacija.valNUD = False
                If Me.angleZ > 359 Or Me.angleZ < -359 Then Me.angleZ = 0
                tfNavigacija.NUDugaoZ.Value = CDec(Me.angleZ)
                If Me.angleY > 359 Or Me.angleY < -359 Then Me.angleY = 0
                tfNavigacija.NUDugaoY.Value = CDec(Me.angleY)
                tfNavigacija.valNUD = True
            End If
        End If
    End Sub

    Private Sub cf3D_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        If Me.gettingPoints Then
            Select Case e.Button
                Case Windows.Forms.MouseButtons.Left
                    Me.points.Add(mf.Scena.LocMouseHitPlane(Plane.FromPointNormal(New Vector3(0, 0, 0), New Vector3(0, 0, 1)), e.X, e.Y, device))
                    funGetPoints.Invoke()
                Case Windows.Forms.MouseButtons.Right
                    Me.gettingPoints = False
                    Me.Cursor = Cursors.Default
                    Me.ContextMenuStrip = Me.ContextMenuStrip1
                    mf.LStatus.Text = "Ready."
            End Select
        End If
    End Sub

    Private Sub cf3D_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If Me.zcam + e.Delta * 0.1 < tfNavigacija.NUDzcam.Maximum And Me.zcam + e.Delta * 0.1 > tfNavigacija.NUDzcam.Minimum Then
            Me.zcam += CSng(e.Delta * 0.1)
            tfNavigacija.NUDzcam.Value = CDec(Me.zcam)
        End If
    End Sub

    Public Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If mf.Scena.Perspective Then
            device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), CSng(Me.Width / Me.Height), 1, 1000) 'sets field of view, aspect ratio, etc
            device.Transform.View = Matrix.LookAtLH(New Vector3(xcam, ycam, zcam), New Vector3(0 + xcam, 0 + ycam, zcam - 10), New Vector3(0, 1, 0)) 'position and direction
        Else
            device.Transform.Projection = Matrix.OrthoOffCenterLH(xcam + CSng(Me.Width / 4 * zcam / 400), xcam - CSng(Me.Width / 4 * zcam / 400), ycam - CSng(Me.Height / 4 * zcam / 400), ycam + CSng(Me.Height / 4 * zcam / 400), -1000, 1000) 'sets field of view, aspect ratio, etc
        End If

        
        device.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, mf.Scena.backgroundColor, 1.0, 0)

        device.BeginScene() 'all drawings after this line

        device.Transform.World = Matrix.RotationYawPitchRoll(angleX * CSng(Math.PI / 180), angleY * CSng(Math.PI / 180), angleZ * CSng(Math.PI / 180))
        Try
            mf.Scena.drawScene(device)
        Catch ex As Exception

        End Try
        device.EndScene() 'all drawings before this line
        device.Present()


    End Sub
    Public Sub renderToTexture(ByVal obj As Object)
        Me.Timer1.Stop()
        If obj.GetType IsNot GetType(ClassUV) Then Exit Sub
        Dim UV As ClassUV = CType(obj, ClassUV)
        If mf.Scena.Perspective Then
            device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), CSng(Me.Width / Me.Height), 1, 1000) 'sets field of view, aspect ratio, etc
            device.Transform.View = Matrix.LookAtLH(New Vector3(xcam, ycam, zcam), New Vector3(0 + xcam, 0 + ycam, zcam - 10), New Vector3(0, 1, 0)) 'position and direction
        Else
            device.Transform.Projection = Matrix.OrthoOffCenterLH(xcam + CSng(Me.Width / 4 * zcam / 400), xcam - CSng(Me.Width / 4 * zcam / 400), ycam - CSng(Me.Height / 4 * zcam / 400), ycam + CSng(Me.Height / 4 * zcam / 400), -1000, 1000) 'sets field of view, aspect ratio, etc
        End If

        ' render to texture
        Dim surf As Surface = device.GetRenderTarget(0)
        device.SetRenderTarget(0, UV.enviroment.GetSurfaceLevel(0))
        device.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, Color.FromArgb(UV.Transparency, mf.Scena.backgroundColor), 1.0, 0)

        device.BeginScene() 'all drawings after this line
        device.Transform.World = Matrix.RotationYawPitchRoll(angleX * CSng(Math.PI / 180), angleY * CSng(Math.PI / 180), angleZ * CSng(Math.PI / 180))
        Try
            mf.Scena.drawScene(device)
        Catch ex As Exception

        End Try
        device.EndScene() 'all drawings before this line
        device.SetRenderTarget(0, surf)
        Me.Timer1.Start()
    End Sub
    Public Sub saveSlicka(ByVal filename As String)
        Me.Timer1.Stop()
        device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), CSng(Me.Width / Me.Height), 1, 1000) 'sets field of view, aspect ratio, etc
        device.Transform.View = Matrix.LookAtLH(New Vector3(xcam, ycam, zcam), New Vector3(0 + xcam, 0 + ycam, zcam - 10), New Vector3(0, 1, 0)) 'position and direction

        device.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, mf.Scena.backgroundColor, 1.0, 0)
        Try
            device.BeginScene() 'all drawings after this line
            device.Transform.World = Matrix.RotationYawPitchRoll(angleX * CSng(Math.PI / 180), angleY * CSng(Math.PI / 180), angleZ * CSng(Math.PI / 180))

            mf.Scena.drawScene(device)
            Select Case filename.Substring(filename.Length - 3, 3)
                Case "jpg"
                    SurfaceLoader.Save(filename, ImageFileFormat.Jpg, device.GetBackBuffer(0, 0, BackBufferType.Mono))
                Case "png"
                    SurfaceLoader.Save(filename, ImageFileFormat.Png, device.GetBackBuffer(0, 0, BackBufferType.Mono))
                Case "bmp"
                    SurfaceLoader.Save(filename, ImageFileFormat.Bmp, device.GetBackBuffer(0, 0, BackBufferType.Mono))
            End Select
            device.EndScene() 'all drawings before this line
            device.Present()
        Catch ex As Exception
            console.writeline(ex.Message)
        End Try
        Me.Timer1.Start()
    End Sub
    Private Shared Sub saveSlickaRemesh(ByVal sender As Device)
        Dim uv As ClassUV
        Dim iso As ClassISO
        For Each uv In mf.Scena.UVList
            uv.createMesh(sender)
        Next
        For Each iso In mf.Scena.ISOList
            iso.createMesh(sender)
        Next
    End Sub
    Public Sub saveSlicka(ByVal filename As String, ByVal width As Integer, ByVal height As Integer)
        Me.timerStoped = True
        Me.Timer1.Stop()
        Dim bph, bpw As Integer


        Dim pp As New PresentParameters(Me.present)
        With pp
            bph = .BackBufferHeight
            bpw = .BackBufferWidth
            .BackBufferHeight = height
            .BackBufferWidth = width
        End With


        Dim surf As Surface = device.GetRenderTarget(0)
        Dim texture As New Texture(device, width, height, 1, Usage.RenderTarget, Format.A8R8G8B8, 0)
        Dim psurf As Surface = texture.GetSurfaceLevel(0)
        device.SetRenderTarget(0, psurf)

        device.Transform.Projection = Matrix.PerspectiveFovLH(CSng(Math.PI / 4), CSng(Me.Width / Me.Height), 1, 1000) 'sets field of view, aspect ratio, etc
        device.Transform.View = Matrix.LookAtLH(New Vector3(xcam, ycam, zcam), New Vector3(0 + xcam, 0 + ycam, zcam - 10), New Vector3(0, 1, 0)) 'position and direction

        device.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, mf.Scena.backgroundColor, 1.0, 0)
        Try
            device.BeginScene() 'all drawings after this line
            device.Transform.World = Matrix.RotationYawPitchRoll(angleX * CSng(Math.PI / 180), angleY * CSng(Math.PI / 180), angleZ * CSng(Math.PI / 180))

            mf.Scena.drawScene(device)

            Select Case filename.Substring(filename.Length - 3, 3)
                Case "jpg"
                    SurfaceLoader.Save(filename, ImageFileFormat.Jpg, psurf)
                Case "png"
                    SurfaceLoader.Save(filename, ImageFileFormat.Png, psurf)
                Case "bmp"
                    SurfaceLoader.Save(filename, ImageFileFormat.Bmp, psurf)
            End Select

            'Select Case filename.Substring(filename.Length - 3, 3)
            '    Case "jpg"
            '        SurfaceLoader.Save(filename, ImageFileFormat.Jpg, device.GetBackBuffer(0, 0, BackBufferType.Mono))
            '    Case "png"
            '        SurfaceLoader.Save(filename, ImageFileFormat.Png, device.GetBackBuffer(0, 0, BackBufferType.Mono))
            '    Case "bmp"
            '        SurfaceLoader.Save(filename, ImageFileFormat.Bmp, device.GetBackBuffer(0, 0, BackBufferType.Mono))
            'End Select

            device.EndScene() 'all drawings before this line
            device.Present()
            With pp
                .BackBufferHeight = bph
                .BackBufferWidth = bpw
            End With

            device.SetRenderTarget(0, surf)
            texture.Dispose()
        Catch ex As Exception
            console.writeline(ex.Message)
        End Try
        Me.Timer1.Start()
        Me.timerStoped = False
    End Sub

    Private Sub device_DeviceResizing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles device.DeviceResizing
        e.Cancel = True
    End Sub

    Private Sub cf3D_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Not Me.Timer1.Enabled And Not Me.timerStoped Then
            Me.Timer1_Tick(sender, New EventArgs)
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        mf.CopyToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        mf.PasteToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub CovertToMeshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CovertToMeshToolStripMenuItem.Click
        If mf.Scena.SelectedObject.GetType Is GetType(ClassUV) Then
            Dim cm As New ClassMesh
            Dim v As CustomVertex.PositionNormalTextured
            Dim i As Integer
            Dim UV As ClassUV = CType(mf.Scena.SelectedObject, ClassUV)
            For Each v In UV.vBuffer
                cm.vbfo.Add(v.Position)
            Next
            For Each i In UV.indices
                cm.ibf.Add(i)
            Next
            cm.Name = UV.Name + " mesh"
            cm.c = UV.bojaPolja1
            cm.refreshBuffer()
            cm.createMesh(Me.device)
            mf.Scena.MeshList.Add(cm)
            Exit Sub
        End If
        If mf.Scena.SelectedObject.GetType Is GetType(ClassISO) Then
            Dim cm As New ClassMesh
            Dim v As CustomVertex.PositionNormalTextured
            Dim i As Integer
            Dim ISO As ClassISO = CType(mf.Scena.SelectedObject, ClassISO)
            For Each v In ISO.vBuffer
                cm.vbfo.Add(v.Position)
            Next
            For Each i In ISO.iBuffer
                cm.ibf.Add(i)
            Next
            cm.Name = ISO.Name + " mesh"
            cm.c = ISO.FrontColor
            cm.refreshBuffer()
            cm.createMesh(Me.device)
            mf.Scena.MeshList.Add(cm)
            Exit Sub
        End If
        MsgBox("Only UV and ISO surfaces can be converted")
    End Sub
    Public Sub getPoints(ByRef pointList As List(Of Vector3), ByVal fun As MethodInvoker, Optional ByVal numberOfPoints As Integer = 0)
        pointList.Clear()
        points = pointList
        Me.funGetPoints = fun
        Me.gettingPoints = True
        Me.Cursor = Cursors.Cross
        Me.ContextMenuStrip = Nothing
        mf.LStatus.Text = "LClick set point; RClick confirm"
    End Sub
End Class