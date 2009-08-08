Imports System.Math
Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
<System.Serializable()> _
Public Class ClassUV
    Private UGustina As Integer = 10
    Private VGustina As Integer = 10
    Private maxU As Single = 10
    Private maxV As Single = 10
    Private minU As Single = -10
    Private minV As Single = -10
    Private funX As String = "u"
    Private funY As String = "v"
    Private funZ As String = "1"
    Private scX As Single = 1
    Private scY As Single = 1
    Private scZ As Single = 1
    Private lc As Color = Color.Black
    Private fc1 As Color = Color.Bisque
    Private fc2 As Color = Color.White
    Private xpol As Single = 0
    Private ypol As Single = 0
    Private zpol As Single = 0
    Dim xrot As Single = 0
    Dim yrot As Single = 0
    Dim zrot As Single = 0
    Dim sf As Byte = 1
    Dim fcd As Boolean = True
    Private naziv As String = "new1"
    Public xVal As New List(Of Single)
    Public yVal As New List(Of Single)
    Public zVal As New List(Of Single)
    Public a As Single = 0
    Public u As Single = 0
    Public v As Single = 0
    Public prm As New List(Of ClassParametri)
    Public dynprm As New List(Of ClassDynamicParametri)
    <System.NonSerialized()> _
    Public vBuffer As New List(Of CustomVertex.PositionNormalTextured)
    <System.NonSerialized()> _
    Public vsBuffer As New List(Of CustomVertex.PositionNormalColored)
    <System.NonSerialized()> _
    Public lineBuffer As New List(Of CustomVertex.PositionColored)
    Public lineBuffer1 As New List(Of Vector3)
    Public subset As New List(Of Integer)
    Public selectedStyle As ClassCA.VisualStyles = ClassCA.VisualStyles.defaultStyle
    <System.NonSerialized()> _
    Dim ee As New MSScriptControl.ScriptControl
    Dim minSU As Single = -10
    Dim maxSU As Single = 10
    Dim stepSU As Single = 1
    Dim minSV As Single = -10
    Dim maxSV As Single = 10
    Dim stepSV As Single = 1
    Dim minSUm As Single = -10
    Dim maxSUm As Single = 10
    Dim stepSUm As Single = 1
    Dim minSVm As Single = -10
    Dim maxSVm As Single = 10
    Dim stepSVm As Single = 1
    Dim alphaLevel As Byte = 255
    Dim lineW As Byte = 1
    Dim mhatch As Drawing2D.HatchStyle
    Dim brojProlaza As Integer = 0
    Public indices As New List(Of Int32)
    <System.NonSerialized()> _
    Public UVMesh As Mesh
    <System.NonSerialized()> _
    Public enviroment As Texture = Nothing
#Region "Events"
    Public Shared Event bufferRefreshed()
    Public Shared Event progressStart()
    Public Shared Event progressEnd()
    Public Shared Event progress(ByVal p As Integer, ByVal m As String)
#End Region
    Dim coloringStress As Boolean = False
    <Category("2. Appearance")> _
    Public Property stress() As Boolean
        Get
            Return Me.coloringStress
        End Get
        Set(ByVal value As Boolean)
            Me.coloringStress = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property FaceMap() As Boolean
        Get
            Return Me.fcd
        End Get
        Set(ByVal value As Boolean)
            Me.fcd = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property smoothFactor() As Byte
        Get
            Return Me.sf
        End Get
        Set(ByVal value As Byte)
            Me.sf = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property Style() As ClassCA.VisualStyles
        Get
            Return Me.selectedStyle
        End Get
        Set(ByVal value As ClassCA.VisualStyles)
            Me.selectedStyle = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property lineWidth() As Byte
        Get
            Return Me.lineW
        End Get
        Set(ByVal value As Byte)
            Me.lineW = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property Hatch() As Drawing2D.HatchStyle
        Get
            Return Me.mhatch
        End Get
        Set(ByVal value As Drawing2D.HatchStyle)
            Me.mhatch = value
        End Set
    End Property
    <Category("4. Parameters")> _
    Public Property dynapicParameters() As List(Of ClassDynamicParametri)
        Get
            Return Me.dynprm
        End Get
        Set(ByVal value As List(Of ClassDynamicParametri))
            Me.dynprm = value
        End Set
    End Property
    <Category("4. Parameters")> _
    Public Property dParam(ByVal index As Integer) As Single
        Get
            Return Me.dynprm(index).value
        End Get
        Set(ByVal value As Single)
            Me.dynprm(index).value = value
        End Set
    End Property
    <Category("8. U"), DisplayName("Minimal Umax Value")> _
    Public Property sliderMinimumUmax() As Single
        Get
            Return Me.minSUm
        End Get
        Set(ByVal value As Single)
            Me.minSUm = value
        End Set
    End Property
    <Category("8. U"), DisplayName("Maximal Umax Value")> _
    Public Property sliderMaximumUmax() As Single
        Get
            Return Me.maxSUm
        End Get
        Set(ByVal value As Single)
            Me.maxSUm = value
        End Set
    End Property
    <Category("8. U"), DisplayName("Step for Umax Value")> _
    Public Property sliderStepUmax() As Single
        Get
            Return Me.stepSUm
        End Get
        Set(ByVal value As Single)
            Me.stepSUm = value
        End Set
    End Property
    <Category("9. V"), DisplayName("Minimal Vmax Value")> _
    Public Property sliderMinimumVmax() As Single
        Get
            Return Me.minSVm
        End Get
        Set(ByVal value As Single)
            Me.minSVm = value
        End Set
    End Property
    <Category("9. V"), DisplayName("Maximal Vmax Value")> _
    Public Property sliderMaximumVmax() As Single
        Get
            Return Me.maxSVm
        End Get
        Set(ByVal value As Single)
            Me.maxSVm = value
        End Set
    End Property
    <Category("9. V"), DisplayName("Step for Vmax Value")> _
    Public Property sliderStepVmax() As Single
        Get
            Return Me.stepSVm
        End Get
        Set(ByVal value As Single)
            Me.stepSVm = value
        End Set
    End Property
    <Category("8. U"), DisplayName("Minimal Umin Value")> _
    Public Property sliderMinimumUmin() As Single
        Get
            Return Me.minSU
        End Get
        Set(ByVal value As Single)
            Me.minSU = value
        End Set
    End Property
    <Category("8. U"), DisplayName("Maximal Umin Value")> _
    Public Property sliderMaximumUmin() As Single
        Get
            Return Me.maxSU
        End Get
        Set(ByVal value As Single)
            Me.maxSU = value
        End Set
    End Property
    <Category("8. U"), DisplayName("Step for Umin Value")> _
    Public Property sliderStepUmin() As Single
        Get
            Return Me.stepSU
        End Get
        Set(ByVal value As Single)
            Me.stepSU = value
        End Set
    End Property
    <Category("9. V"), DisplayName("Minimal Vmin Value")> _
    Public Property sliderMinimumVmin() As Single
        Get
            Return Me.minSV
        End Get
        Set(ByVal value As Single)
            Me.minSV = value
        End Set
    End Property
    <Category("9. V"), DisplayName("Maximal Vmin Value")> _
    Public Property sliderMaximumVmin() As Single
        Get
            Return Me.maxSV
        End Get
        Set(ByVal value As Single)
            Me.maxSV = value
        End Set
    End Property
    <Category("9. V"), DisplayName("Step for Vmin Value")> _
    Public Property sliderStepVmin() As Single
        Get
            Return Me.stepSV
        End Get
        Set(ByVal value As Single)
            Me.stepSV = value
        End Set
    End Property
    <Category("4. Parameters")> _
    Public Property param(ByVal index As Integer) As Single
        Get
            Return prm(index).value
        End Get
        Set(ByVal value As Single)
            prm(index).value = value
        End Set
    End Property
    <Category("4. Parameters")> _
    Public Property param(ByVal nazivParametra As String) As Single
        Get
            Dim rv As Single
            Dim sp As ClassParametri
            For Each sp In prm
                If nazivParametra = sp.Name Then rv = sp.value
            Next
            Return rv
        End Get
        Set(ByVal value As Single)
            Dim sp As ClassParametri
            For Each sp In prm
                If nazivParametra = sp.Name Then sp.value = value
            Next
        End Set
    End Property
    Public Sub New(ByVal device As Device)
        Me.loadFun()
        Me.refreshBuffer(device)
    End Sub
    Public Sub New(ByVal UV As ClassUV, ByVal device As Device)
        Me.loadFun()
        With Me
            .maksimalnoU = UV.maksimalnoU
            .maksimalnoV = UV.maksimalnoV
            .minimalnoU = UV.minimalnoU
            .minimalnoV = UV.minimalnoV
            .scaleX = UV.scaleX
            .scaleY = UV.scaleY
            .scaleZ = UV.scaleZ
            .Udens = UV.Udens
            .Vdens = UV.Vdens
            .XF = UV.XF
            .YF = UV.YF
            .ZF = UV.ZF
            .bojaLinija = UV.bojaLinija
            .bojaPolja1 = UV.bojaPolja1
            .bojaPolja2 = UV.bojaPolja2
            .xPolozaj = UV.xPolozaj
            .yPolozaj = UV.yPolozaj
            .zPolozaj = UV.zPolozaj
            .Name = UV.Name
            .sliderMaximumUmin = UV.maxSU
            .sliderMinimumUmin = UV.minSU
            .sliderStepUmin = UV.stepSU
            .sliderMaximumVmin = UV.maxSV
            .sliderMinimumVmin = UV.minSV
            .sliderStepVmin = UV.stepSV
            .sliderMaximumUmax = UV.maxSUm
            .sliderMinimumUmax = UV.minSUm
            .sliderStepUmax = UV.stepSUm
            .sliderMaximumVmax = UV.maxSVm
            .sliderMinimumVmax = UV.minSVm
            .sliderStepVmax = UV.stepSVm
            Try
                .Transparency = UV.Transparency
                .xRotation = UV.xrot
                .yRotation = UV.yrot
                .zRotation = UV.zrot
            Catch ex As Exception

            End Try
            .parametri = UV.parametri
            .dynapicParameters = UV.dynapicParameters
        End With
        Me.refreshBuffer(device)
    End Sub
    Public Sub New(ByVal ime As String, ByVal device As Device)
        Me.naziv = ime
        Me.loadFun()
        Me.refreshBuffer(device)
    End Sub
    <Category("1. Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Me.naziv = value
        End Set
    End Property
    <Category("8. U")> _
    Public Property Udens() As Integer
        Get
            Return Me.UGustina
        End Get
        Set(ByVal value As Integer)
            Me.UGustina = value
        End Set
    End Property
    <Category("9. V")> _
    Public Property Vdens() As Integer
        Get
            Return Me.VGustina
        End Get
        Set(ByVal value As Integer)
            Me.VGustina = value
        End Set
    End Property
    <Category("8. U"), DisplayName("U max")> _
    Public Property maksimalnoU() As Single
        Get
            Return maxU
        End Get
        Set(ByVal value As Single)
            maxU = value
        End Set
    End Property
    <Category("8. U"), DisplayName("U min")> _
    Public Property minimalnoU() As Single
        Get
            Return minU
        End Get
        Set(ByVal value As Single)
            minU = value
        End Set
    End Property
    <Category("9. V"), DisplayName("V max")> _
    Public Property maksimalnoV() As Single
        Get
            Return maxV
        End Get
        Set(ByVal value As Single)
            maxV = value
        End Set
    End Property
    <Category("9. V"), DisplayName("V min")> _
    Public Property minimalnoV() As Single
        Get
            Return minV
        End Get
        Set(ByVal value As Single)
            minV = value
        End Set
    End Property
    <Category("3. Functions"), DisplayName("X(u,v)")> _
    Public Property XF() As String
        Get
            Return Me.funX
        End Get
        Set(ByVal value As String)
            Me.funX = value
        End Set
    End Property
    <Category("3. Functions"), DisplayName("Y(u,v)")> _
    Public Property YF() As String
        Get
            Return Me.funY
        End Get
        Set(ByVal value As String)
            Me.funY = value
        End Set
    End Property
    <Category("3. Functions"), DisplayName("Z(u,v)")> _
    Public Property ZF() As String
        Get
            Return Me.funZ
        End Get
        Set(ByVal value As String)
            Me.funZ = value
        End Set
    End Property
    Private ReadOnly Property stepU() As Single
        Get
            Return (Me.maxU - Me.minU) / Me.UGustina
        End Get
    End Property
    Private ReadOnly Property stepV() As Single
        Get
            Return (Me.maxV - Me.minV) / Me.VGustina
        End Get
    End Property
    <Category("7. Scale")> _
    Public Property scaleX() As Single
        Get
            Return Me.scX
        End Get
        Set(ByVal value As Single)
            Me.scX = value
        End Set
    End Property
    <Category("7. Scale")> _
    Public Property scaleY() As Single
        Get
            Return Me.scY
        End Get
        Set(ByVal value As Single)
            Me.scY = value
        End Set
    End Property
    <Category("7. Scale")> _
    Public Property scaleZ() As Single
        Get
            Return Me.scZ
        End Get
        Set(ByVal value As Single)
            Me.scZ = value
        End Set
    End Property
    <Category("4. Parameters"), DisplayName("Parameters")> _
    Public Property parametri() As List(Of ClassParametri)
        Get
            Return Me.prm
        End Get
        Set(ByVal value As List(Of ClassParametri))
            prm = value
            Me.loadFun()
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Line Color")> _
    Public Property bojaLinija() As Color
        Get
            Return Me.lc
        End Get
        Set(ByVal value As Color)
            Me.lc = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Front Color")> _
    Public Property bojaPolja1() As Color
        Get
            Return Me.fc1
        End Get
        Set(ByVal value As Color)
            Me.fc1 = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Back Color")> _
    Public Property bojaPolja2() As Color
        Get
            Return Me.fc2
        End Get
        Set(ByVal value As Color)
            Me.fc2 = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property Transparency() As Byte
        Get
            Return Me.alphaLevel
        End Get
        Set(ByVal value As Byte)
            Me.alphaLevel = value
        End Set
    End Property
    <Category("5. Position"), DisplayName("X Position")> _
    Public Property xPolozaj() As Single
        Get
            Return Me.xpol
        End Get
        Set(ByVal value As Single)
            Me.xpol = value
        End Set
    End Property
    <Category("5. Position"), DisplayName("Y Position")> _
    Public Property yPolozaj() As Single
        Get
            Return Me.ypol
        End Get
        Set(ByVal value As Single)
            Me.ypol = value
        End Set
    End Property
    <Category("5. Position"), DisplayName("Z Position")> _
    Public Property zPolozaj() As Single
        Get
            Return Me.zpol
        End Get
        Set(ByVal value As Single)
            Me.zpol = value
        End Set
    End Property
    <Category("6. Rotation")> _
    Public Property xRotation() As Single
        Get
            Return Me.xrot
        End Get
        Set(ByVal value As Single)
            Me.xrot = value
        End Set
    End Property
    <Category("6. Rotation")> _
    Public Property yRotation() As Single
        Get
            Return Me.yrot
        End Get
        Set(ByVal value As Single)
            Me.yrot = value
        End Set
    End Property
    <Category("6. Rotation")> _
    Public Property zRotation() As Single
        Get
            Return Me.zrot
        End Get
        Set(ByVal value As Single)
            Me.zrot = value
        End Set
    End Property
    Public Sub refreshBuffer(Optional ByVal device As Device = Nothing)
        RaiseEvent progressStart()
        ' transformation matrix
        Dim m, mm As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Translate(Me.xPolozaj, Me.yPolozaj, Me.zPolozaj)
        m = Matrix.Multiply(mm, m)
        mm.RotateX(Me.xRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        mm.RotateY(Me.yRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        mm.RotateZ(Me.zRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        ' --------------------------------------------------

        ' clear buffers
        Me.vBuffer.Clear()              ' clear vertices
        Me.vsBuffer.Clear()             ' clear vertices
        Me.indices.Clear()              ' clear indices
        Me.lineBuffer.Clear()           ' clear lines
        Me.lineBuffer1.Clear()          ' clear lines
        Me.subset.Clear()               ' clear subset
        ' --------------------------------------------------

        Me.xVal.Clear()
        Me.yVal.Clear()
        Me.zVal.Clear()
        Dim us, vs As Single
        us = Me.stepU
        vs = Me.stepV
        Dim ui, vi As Integer
        Dim a, b, c, d As Single
        ui = 0
        Dim vertice As CustomVertex.PositionNormalTextured
        Dim svertice As CustomVertex.PositionNormalColored
        Dim x, y, z As Single
        Dim plen, pi, p As Integer
        plen = (Me.Udens + 1) * (Me.Vdens + 1)
        pi = 0
        For u = Me.minU To Me.maxU + 0.0001 Step us
            vi = 0
            For v = Me.minV To Me.maxV + 0.0001 Step vs
                p = 100 * pi \ plen
                RaiseEvent progress(p, "Calculating UV points...")
                pi += 1
                Try
                    vertice = New CustomVertex.PositionNormalTextured
                    svertice = New CustomVertex.PositionNormalColored
                    Dim i As Integer
                    For i = 0 To Me.dynprm.Count - 1
                        Me.dynprm(i).value = ee.Eval(Me.dynprm(i).funkcija)
                    Next
                    x = ee.Eval(Me.funX) * Me.scX
                    y = ee.Eval(Me.funY) * Me.scY
                    z = ee.Eval(Me.funZ) * Me.scZ
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Exit Sub
                    x = 0
                    y = 0
                    z = 0
                End Try

                'If a > maxI Or b > maxI Or c > maxI Or d > maxI Then Exit For
                Me.xVal.Add(x)
                Me.yVal.Add(y)
                Me.zVal.Add(z)
                vertice.Position = Vector3.TransformCoordinate(New Vector3(x, y, z), m)
                '---------------
                svertice.Position = Vector3.TransformCoordinate(New Vector3(x, y, z), m)
                Dim clr As Color = Color.FromArgb((-Math.Round(Vector3.Dot(New Vector3(0, 0, 100000), svertice.Position), 0, MidpointRounding.ToEven) + 50000) / Me.scaleZ)
                svertice.Color = Color.FromArgb(Me.alphaLevel, clr).ToArgb
                Me.vsBuffer.Add(svertice)
                '--------------
                If Me.fcd Then
                    vertice.Tu = ui
                    vertice.Tv = vi
                Else
                    vertice.Tu = (u - Me.minU) / (Me.maxU - Me.minU)
                    vertice.Tv = (v - Me.minV) / (Me.maxV - Me.minV)
                End If

                Me.vBuffer.Add(vertice)
                If ui < Me.Udens And vi < Me.Vdens Then
                    a = ui * (Me.Vdens + 1) + vi
                    b = ui * (Me.Vdens + 1) + vi + 1
                    c = (ui + 1) * (Me.Vdens + 1) + vi + 1
                    d = (ui + 1) * (Me.Vdens + 1) + vi
                    Me.indices.Add(a)
                    Me.indices.Add(b)
                    Me.indices.Add(c)
                    Me.indices.Add(a)
                    Me.indices.Add(c)
                    Me.indices.Add(d)

                    subset.Add(0)
                    subset.Add(0)
                End If

                vi += 1
            Next
            ui += 1
        Next

        Dim vertices1 As CustomVertex.PositionColored()


        'Dim textureDefault As Texture = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/textureDefault.bmp")
        vertices1 = New CustomVertex.PositionColored(7) {}
        Dim l As Color
        l = Me.bojaLinija
        vertices1(0).Color = l.ToArgb
        vertices1(1).Color = l.ToArgb
        vertices1(2).Color = l.ToArgb
        vertices1(3).Color = l.ToArgb
        vertices1(4).Color = l.ToArgb
        vertices1(5).Color = l.ToArgb
        vertices1(6).Color = l.ToArgb
        vertices1(7).Color = l.ToArgb

        Dim u1, v1 As Integer

        Dim maxI As Integer = xVal.Count - 1
        pi = 0
        For u1 = 0 To Me.Udens - 1

            For v1 = 0 To Me.Vdens - 1
                p = 100 * pi \ plen
                RaiseEvent progress(p, "Generating surface...")
                pi += 1

                a = u1 * (Me.Vdens + 1) + v1
                b = u1 * (Me.Vdens + 1) + v1 + 1
                c = (u1 + 1) * (Me.Vdens + 1) + v1 + 1
                d = (u1 + 1) * (Me.Vdens + 1) + v1
                If a > maxI Or b > maxI Or c > maxI Or d > maxI Then Exit For

                ' linije

                vertices1(0).Position = Vector3.TransformCoordinate(New Vector3(xVal(a), yVal(a), zVal(a)), m)
                vertices1(1).Position = Vector3.TransformCoordinate(New Vector3(xVal(b), yVal(b), zVal(b)), m)
                vertices1(2).Position = Vector3.TransformCoordinate(New Vector3(xVal(b), yVal(b), zVal(b)), m)
                vertices1(3).Position = Vector3.TransformCoordinate(New Vector3(xVal(c), yVal(c), zVal(c)), m)
                vertices1(4).Position = Vector3.TransformCoordinate(New Vector3(xVal(c), yVal(c), zVal(c)), m)
                vertices1(5).Position = Vector3.TransformCoordinate(New Vector3(xVal(d), yVal(d), zVal(d)), m)
                vertices1(6).Position = Vector3.TransformCoordinate(New Vector3(xVal(d), yVal(d), zVal(d)), m)
                vertices1(7).Position = Vector3.TransformCoordinate(New Vector3(xVal(a), yVal(a), zVal(a)), m)
                Me.lineBuffer.Add(vertices1(0))
                Me.lineBuffer.Add(vertices1(1))
                Me.lineBuffer.Add(vertices1(2))
                Me.lineBuffer.Add(vertices1(3))
                Me.lineBuffer.Add(vertices1(4))
                Me.lineBuffer.Add(vertices1(5))
                Me.lineBuffer.Add(vertices1(6))
                Me.lineBuffer.Add(vertices1(7))

                Me.lineBuffer1.Add(vertices1(0).Position)
                Me.lineBuffer1.Add(vertices1(1).Position)
                Me.lineBuffer1.Add(vertices1(2).Position)
                Me.lineBuffer1.Add(vertices1(3).Position)
                Me.lineBuffer1.Add(vertices1(4).Position)
                Me.lineBuffer1.Add(vertices1(5).Position)
                Me.lineBuffer1.Add(vertices1(6).Position)
                Me.lineBuffer1.Add(vertices1(7).Position)
            Next
        Next
        Try
            If device IsNot Nothing Then
                Me.createMesh(device)
            Else
                Me.createMesh(Me.UVMesh.Device)
            End If
        Catch ex As Exception

        End Try
        RaiseEvent progressEnd()
        RaiseEvent bufferRefreshed()
    End Sub
    Public Sub createMesh(ByVal device As Direct3D.Device)
        Try
            Me.UVMesh.Dispose()
            Me.enviroment.Dispose()
        Catch ex As Exception

        End Try
        ' RUTINA ZA PRAVLJENJE MESHA
        Dim vvv() As CustomVertex.PositionNormalTextured = Me.vBuffer.ToArray
        Dim vvvs() As CustomVertex.PositionNormalColored = Me.vsBuffer.ToArray
        Dim c1 As Integer
        c1 = vvv.Length

        Dim ind() As Int32 = indices.ToArray
        If Not coloringStress Then
            Me.UVMesh = New Mesh(ind.Length / 3, c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, device)
        Else
            Me.UVMesh = New Mesh(ind.Length / 3, c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalColored.Format, device)
        End If


        Try
            If Not coloringStress Then
                Me.UVMesh.SetVertexBufferData(vvv, LockFlags.None)
            Else
                Me.UVMesh.SetVertexBufferData(vvvs, LockFlags.None)
            End If

            Me.UVMesh.SetIndexBufferData(ind, LockFlags.None)
            Me.UVMesh.ComputeNormals()
            Dim subset() As Integer = Me.UVMesh.LockAttributeBufferArray(LockFlags.Discard)
            subset = Me.subset.ToArray
            Me.UVMesh.UnlockAttributeBuffer(subset)
            ' Optimize.
            Dim adjacency(Me.UVMesh.NumberFaces * 3 - 1) As Integer
            Me.UVMesh.GenerateAdjacency(CSng(0.1), adjacency)
            Me.UVMesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
            Me.UVMesh = Mesh.TessellateNPatches(Me.UVMesh, adjacency, sf, True)
            Me.enviroment = New Texture(device, 800, 600, 1, Usage.RenderTarget, Format.A8R8G8B8, 0)
            Me.enviroment.AutoGenerateFilterType = TextureFilter.Anisotropic
        Catch ex As Exception
            MsgBox(ex.Message)
            Me.UVMesh = Mesh.Box(device, 10, 10, 10)
        End Try
    End Sub
    Private Sub loadFun()
        ee = New MSScriptControl.ScriptControl
        ee.Language = "vbscript"
        ee.AllowUI = True
        ee.AddObject("mathis", New EvalFunctions, True)
        ee.AddObject("uv", Me, True)
    End Sub
    
    Public Sub afterPaste(ByVal device As Device)
        Me.vBuffer = New List(Of CustomVertex.PositionNormalTextured)
        Me.vsBuffer = New List(Of CustomVertex.PositionNormalColored)
        Me.lineBuffer = New List(Of CustomVertex.PositionColored)
        loadFun()
        refreshBuffer(device)
    End Sub
End Class


