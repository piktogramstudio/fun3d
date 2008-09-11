Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Math
Imports System.ComponentModel
Public Class ClassScena
    Private minV As Single = -100
    Private maxV As Single = 100
    Private gridStep As Single = 10
    Private gridColor As Color = Color.Black
    Private Uclass As New List(Of ClassU)
    Private UVclass As New List(Of ClassUV)
    Private ISOclass As New List(Of ClassISO)
    Private CAclass As New List(Of ClassCA)
    Private LSclass As New List(Of ClassLS)
    Dim persp As Boolean = True
    Dim pLighting As Boolean = True
    Dim pFillMode As FillMode = FillMode.Solid
    Dim pShadeMode As ShadeMode = ShadeMode.Gouraud
    Dim pAmbient As Color = Color.White
    Dim LightClass As New List(Of ClassLight)
    Dim bckg As Color = Color.Gainsboro
    Dim naziv As String = "Scene1"
    Dim descr As String = ""
    Dim hglght As Boolean = False
    Dim bo As Direct3D.BlendOperation = Direct3D.BlendOperation.Add
    Dim comf As Direct3D.Compare = Compare.Always
    Dim pSize As Integer = 15
    Dim shadow As Boolean = False
    Dim shadowLT As ClassScena.lightType = lightType.directional
    Dim lv(2) As Single
    Dim sceneList As New List(Of ClassFrame)
    'Our Mesh Definitions
    Public moMesh As Mesh = Nothing
    Public mlMeshMaterials As Int32 = -1
    Public moMaterials() As Material
    Public moTextures() As Texture
    Public chkx As Integer = 0
    Public chky As Integer = 0
    Public checkClick As Boolean = False
    Public WithEvents selectedCA As ClassCA
    Public WithEvents selectedUV As ClassUV
    Dim selektovaniObjekat As Object
    Public Event SelectionChanged()
    Public Event propertyChanged()
    Public Sub New()
        Me.LightClass.Clear()
        Me.LightClass.Add(New ClassLight)
        Me.LightClass.Add(New ClassLight)
        Me.LightClass(1).pPosition = New Vector3(0, 0, 500)
        ' polozaj svetla za senke
        Me.lv(0) = 100
        Me.lv(1) = 100
        Me.lv(2) = 300
        Me.SelectedObject = Me
    End Sub
    Public Property SelectedObject() As Object
        Get
            Return selektovaniObjekat
        End Get
        Set(ByVal value As Object)
            Me.selektovaniObjekat = value
            If value.GetType Is GetType(ClassCA) Then
                Me.selectedCA = value
            ElseIf value.GetType Is GetType(ClassUV) Then
                Me.selectedUV = value
            End If
            RaiseEvent SelectionChanged()
        End Set
    End Property
    <Category("Meta")> _
    Public Property Description() As String
        Get
            Return Me.descr
        End Get
        Set(ByVal value As String)
            Me.descr = value
        End Set
    End Property
    <Category("Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Me.naziv = value
        End Set
    End Property
    <Category("Animation")> _
    Public Property SceneFrames() As List(Of ClassFrame)
        Get
            Return Me.sceneList
        End Get
        Set(ByVal value As List(Of ClassFrame))
            Me.sceneList = value
        End Set
    End Property
    <Category("Appearance"), Browsable(False)> _
        Public Property showShadows() As Boolean
        Get
            Return Me.shadow
        End Get
        Set(ByVal value As Boolean)
            Me.shadow = value
        End Set
    End Property
    <Category("Appearance")> _
            Public Property Perspective() As Boolean
        Get
            Return Me.persp
        End Get
        Set(ByVal value As Boolean)
            Me.persp = value
            cf3D.xcam = 0
            cf3D.ycam = 0
            cf3D.zcam = 30
            cf3D.angleX = 0
            cf3D.angleY = 0
            cf3D.angleZ = 0
        End Set
    End Property
    <Category("Appearance"), Browsable(False)> _
        Public Property ShadowsPosition() As Single()
        Get
            Return Me.lv
        End Get
        Set(ByVal value As Single())
            Me.lv = value
        End Set
    End Property
    <Category("Appearance"), Browsable(False)> _
        Public Property shadowsLightType() As ClassScena.lightType
        Get
            Return Me.shadowLT
        End Get
        Set(ByVal value As ClassScena.lightType)
            Me.shadowLT = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property backgroundColor() As Color
        Get
            Return Me.bckg
        End Get
        Set(ByVal value As Color)
            Me.bckg = value
        End Set
    End Property
    <Category("Appearance")> _
        Public Property PointSize() As Integer
        Get
            Return Me.pSize
        End Get
        Set(ByVal value As Integer)
            Me.pSize = value
        End Set
    End Property
    <Category("Light")> _
    Public Property sceneLights() As List(Of ClassLight)
        Get
            Return Me.LightClass
        End Get
        Set(ByVal value As List(Of ClassLight))
            Me.LightClass = value
        End Set
    End Property
    <Category("Light")> _
    Public Property Lighting() As Boolean
        Get
            Return Me.pLighting
        End Get
        Set(ByVal value As Boolean)
            Me.pLighting = value
        End Set
    End Property
    <Category("Light")> _
        Public Property Highlight() As Boolean
        Get
            Return Me.hglght
        End Get
        Set(ByVal value As Boolean)
            Me.hglght = value
        End Set
    End Property
    <Category("Light")> _
        Public Property BlendOperation() As Direct3D.BlendOperation
        Get
            Return Me.bo
        End Get
        Set(ByVal value As Direct3D.BlendOperation)
            Me.bo = value
        End Set
    End Property
    <Category("Light")> _
        Public Property AlphaFunction() As Direct3D.Compare
        Get
            Return Me.comf
        End Get
        Set(ByVal value As Direct3D.Compare)
            Me.comf = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property FillMode() As FillMode
        Get
            Return Me.pFillMode
        End Get
        Set(ByVal value As FillMode)
            Me.pFillMode = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property ShadeMode() As ShadeMode
        Get
            Return Me.pShadeMode
        End Get
        Set(ByVal value As ShadeMode)
            Me.pShadeMode = value
        End Set
    End Property
    <Category("Light")> _
    Public Property Ambient() As Color
        Get
            Return Me.pAmbient
        End Get
        Set(ByVal value As Color)
            Me.pAmbient = value
        End Set
    End Property
    <Category("Grid")> _
    Public Property gridMinV() As Single
        Get
            Return Me.minV
        End Get
        Set(ByVal value As Single)
            Me.minV = value
        End Set
    End Property
    <Category("Grid")> _
    Public Property gridMaxV() As Single
        Get
            Return Me.maxV
        End Get
        Set(ByVal value As Single)
            Me.maxV = value
        End Set
    End Property
    <Category("Grid")> _
    Public Property stepGrid() As Single
        Get
            Return Me.gridStep
        End Get
        Set(ByVal value As Single)
            Me.gridStep = value
        End Set
    End Property
    <Category("Grid")> _
    Public Property colorGrid() As Color
        Get
            Return Me.gridColor
        End Get
        Set(ByVal value As Color)
            Me.gridColor = value
        End Set
    End Property
    ' OBJEKTI
    <Browsable(False)> _
    Public Property LSList() As List(Of ClassLS)
        Get
            Return Me.LSclass
        End Get
        Set(ByVal value As List(Of ClassLS))
            Me.LSclass = value
        End Set
    End Property
    <Browsable(False)> _
    Public Property CAList() As List(Of ClassCA)
        Get
            Return Me.CAclass
        End Get
        Set(ByVal value As List(Of ClassCA))
            Me.CAclass = value
        End Set
    End Property
    <Browsable(False)> _
    Public Property UList() As List(Of ClassU)
        Get
            Return Me.Uclass
        End Get
        Set(ByVal value As List(Of ClassU))
            Me.Uclass = value
        End Set
    End Property
    <Browsable(False)> _
    Public Property UVList() As List(Of ClassUV)
        Get
            Return Me.UVclass
        End Get
        Set(ByVal value As List(Of ClassUV))
            Me.UVclass = value
        End Set
    End Property
    <Browsable(False)> _
    Public Property ISOList() As List(Of ClassISO)
        Get
            Return Me.ISOclass
        End Get
        Set(ByVal value As List(Of ClassISO))
            Me.ISOclass = value
        End Set
    End Property
    Public Sub drawScene(ByVal device As Direct3D.Device)
        Me.setRenderState(device)
        Me.setLights(device)
        Dim UVC As ClassUV
        Dim UC As ClassU
        Dim ISO As ClassISO
        Dim CA As ClassCA
        Dim LS As ClassLS
        Dim i As Integer = 0
        If Not moMesh Is Nothing Then
            Me.drawMesh(device)
        End If
        For Each UVC In Me.UVclass
            Me.drawUV(device, UVC)
        Next
        For Each UC In Me.Uclass
            Me.drawU(device, UC, True)
        Next
        For Each ISO In Me.ISOclass
            Me.drawISO(device, ISO)
        Next
        For Each CA In Me.CAclass
            Me.drawCA(device, CA)
        Next
        For Each LS In Me.LSclass
            Me.drawLS(device, LS)
        Next
        Dim ll As ClassLight
        For Each ll In Me.LightClass
            Dim lm As Mesh = Mesh.Sphere(device, 1, 16, 16)
            Dim oldm As Matrix = device.Transform.World
            device.Transform.World = Matrix.Multiply(Matrix.Translation(ll.pPosition), device.Transform.World)
            Dim m As New Material
            m.Diffuse = ll.Diffuse
            device.Material = m
            lm.DrawSubset(0)
            lm.Dispose()
            device.Transform.World = oldm
        Next
        Me.drawGrid(device)
    End Sub
    Private Sub drawLSExtracted(ByVal device As Direct3D.Device, ByVal LS As ClassLS, ByVal slovo As Char, ByVal str As String)
        Dim savedm As New List(Of Matrix)
        Dim konst As Boolean = False
        str = str.Replace("]X", "x")
        str = str.Replace("[X", "X")
        str = str.Replace(">X", "u")
        str = str.Replace("<X", "U")
        str = str.Replace("]Y", "y")
        str = str.Replace("[Y", "Y")
        str = str.Replace(">Y", "v")
        str = str.Replace("<Y", "V")
        str = str.Replace("]Z", "z")
        str = str.Replace("[Z", "Z")
        str = str.Replace(">Z", "w")
        str = str.Replace("<Z", "W")
        For Each slovo In str
            Select Case slovo
                Case "x"
                    device.Transform.World = Matrix.Multiply(Matrix.RotationX(LS.AngleX * Math.PI / 180), device.Transform.World)
                    konst = True
                Case "y"
                    device.Transform.World = Matrix.Multiply(Matrix.RotationY(LS.AngleY * Math.PI / 180), device.Transform.World)
                    konst = True
                Case "z"
                    device.Transform.World = Matrix.Multiply(Matrix.RotationZ(LS.AngleZ * Math.PI / 180), device.Transform.World)
                    konst = True
                Case "u"
                    device.Transform.World = Matrix.Multiply(Matrix.Translation(LS.TranslateX, 0, 0), device.Transform.World)
                    konst = True
                Case "v"
                    device.Transform.World = Matrix.Multiply(Matrix.Translation(0, LS.TranslateY, 0), device.Transform.World)
                    konst = True
                Case "w"
                    device.Transform.World = Matrix.Multiply(Matrix.Translation(0, 0, LS.TranslateZ), device.Transform.World)
                    konst = True
                Case "X"
                    device.Transform.World = Matrix.Multiply(Matrix.RotationX(-LS.AngleX * Math.PI / 180), device.Transform.World)
                    konst = True
                Case "Y"
                    device.Transform.World = Matrix.Multiply(Matrix.RotationY(-LS.AngleY * Math.PI / 180), device.Transform.World)
                    konst = True
                Case "Z"
                    device.Transform.World = Matrix.Multiply(Matrix.RotationZ(-LS.AngleZ * Math.PI / 180), device.Transform.World)
                    konst = True
                Case "U"
                    device.Transform.World = Matrix.Multiply(Matrix.Translation(-LS.TranslateX, 0, 0), device.Transform.World)
                    konst = True
                Case "V"
                    device.Transform.World = Matrix.Multiply(Matrix.Translation(0, -LS.TranslateY, 0), device.Transform.World)
                    konst = True
                Case "W"
                    device.Transform.World = Matrix.Multiply(Matrix.Translation(0, 0, -LS.TranslateZ), device.Transform.World)
                    konst = True
                Case "{"
                    savedm.Add(device.Transform.World)
                    konst = True
                Case "}"
                    device.Transform.World = savedm(savedm.Count - 1)
                    savedm.RemoveAt(savedm.Count - 1)
                    konst = True
            End Select
            If Not konst Then
                Dim msh As Mesh = Mesh.Box(device, LS.width, LS.height, LS.lenght)
                Dim crtajLiniju As Boolean = False
                Select Case LS.Shape
                    Case ClassLS.oblik.cube
                        msh = Mesh.Box(device, LS.width, LS.height, LS.lenght)
                    Case ClassLS.oblik.sphere
                        msh = Mesh.Sphere(device, LS.width, 8, 8)
                    Case ClassLS.oblik.teapot
                        msh = Mesh.Teapot(device)
                    Case ClassLS.oblik.torus
                        msh = Mesh.Torus(device, LS.lenght, LS.width, 8, 8)
                    Case ClassLS.oblik.line
                        crtajLiniju = True
                End Select
                If Not crtajLiniju Then
                    msh.DrawSubset(0)
                    Try
                        If Me.checkClick And Me.DoesMouseHitMesh(msh, chkx, chky, device) Then
                            Me.SelectedObject = LS
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Else
                    Dim lines(1) As CustomVertex.PositionColored
                    lines(0) = New CustomVertex.PositionColored(New Vector3(0, 0, 0), LS.Colour.ToArgb)
                    lines(1) = New CustomVertex.PositionColored(New Vector3(LS.width, 0, 0), LS.Colour.ToArgb)
                    Dim oldformat As Integer
                    oldformat = device.VertexFormat
                    device.VertexFormat = CustomVertex.PositionColored.Format
                    device.DrawUserPrimitives(PrimitiveType.LineList, 1, lines)
                    device.DrawUserPrimitives(PrimitiveType.PointList, 2, lines)
                    device.VertexFormat = oldformat
                End If
                msh.Dispose()
            End If
            konst = False
        Next
    End Sub
    Public Sub drawLS(ByVal device As Direct3D.Device, ByVal LS As ClassLS)
        Dim mat As New Direct3D.Material
        mat.Diffuse = LS.Colour
        device.Material = mat
        Dim oldm As Matrix = device.Transform.World
        Dim lvlm As Matrix = device.Transform.World
        Dim slovo As Char
        Dim str As String = LS.States(LS.States.Count - 1)
        If LS.showAllIter Then
            For Each str In LS.States
                drawLSExtracted(device, LS, slovo, str)
                lvlm = Matrix.Multiply(Matrix.Translation(0, 0, LS.levelDistance), lvlm)
                device.Transform.World = lvlm
            Next
        Else
            drawLSExtracted(device, LS, slovo, str)
        End If
        device.Transform.World = oldm
    End Sub
    Public Sub drawU(ByVal device As Direct3D.Device, ByVal UV As ClassU, ByVal selected As Boolean)
        Dim ver As New CustomVertex.PositionColored
        Dim vertices1 As CustomVertex.PositionNormalColored()

        Dim mat As New Direct3D.Material
        mat.Diffuse = Color.White
        device.Material = mat
        vertices1 = New CustomVertex.PositionNormalColored(1) {}
        Dim l As Color
        l = UV.bojaLinija
        vertices1(0).Color = l.ToArgb
        vertices1(1).Color = l.ToArgb
        Dim xVal As New List(Of Single)
        Dim yVal As New List(Of Single)
        Dim zVal As New List(Of Single)
        Dim a, b, u, sc As Single


        xVal = UV.xVal
        yVal = UV.yVal
        zVal = UV.zVal
        Dim maxI As Integer = xVal.Count - 1
        sc = 100
        device.RenderState.PointSize = Me.pSize
        For u = 0 To UV.Udens - 1
            a = u
            b = u + 1
            If a > maxI Or b > maxI Then Exit For
            ' linije
            device.VertexFormat = CustomVertex.PositionNormalColored.Format
            vertices1(0).Position = New Vector3(xVal(a), yVal(a), zVal(a))
            vertices1(1).Position = New Vector3(xVal(b), yVal(b), zVal(b))
            device.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices1)
            ver.Color = UV.bojaTacke.ToArgb
            device.VertexFormat = CustomVertex.PositionColored.Format
            ver.Position = New Vector3(xVal(a), yVal(a), zVal(a))
            device.DrawUserPrimitives(PrimitiveType.PointList, 1, ver)
            ver.Position = New Vector3(xVal(b), yVal(b), zVal(b))
            device.DrawUserPrimitives(PrimitiveType.PointList, 1, ver)
        Next
    End Sub
    Public Sub drawUV(ByVal device As Direct3D.Device, ByVal UV As ClassUV)
        Dim vertices As CustomVertex.PositionNormalTextured() 'an array of vertices
        Dim vertices1 As CustomVertex.PositionNormalColored()
        Dim m, om, m1 As New Matrix
        vertices = New CustomVertex.PositionNormalTextured(2) {}
        vertices1 = New CustomVertex.PositionNormalColored(7) {}
        Dim l As Color
        l = UV.bojaLinija
        vertices1(0).Color = l.ToArgb
        vertices1(1).Color = l.ToArgb
        vertices1(2).Color = l.ToArgb
        vertices1(3).Color = l.ToArgb
        vertices1(4).Color = l.ToArgb
        vertices1(5).Color = l.ToArgb
        vertices1(6).Color = l.ToArgb
        vertices1(7).Color = l.ToArgb
        Dim sc As Single
        Dim f1, f2 As Color
        f1 = Color.FromArgb(UV.Transparency, UV.bojaPolja1)
        f2 = Color.FromArgb(UV.Transparency, UV.bojaPolja2)
        sc = 100
        om = device.Transform.World
        m.Translate(UV.xPolozaj, UV.yPolozaj, UV.zPolozaj)
        device.Transform.World = Matrix.Multiply(m, device.Transform.World)
        m.RotateX(UV.xRotation * Math.PI / 180)
        device.Transform.World = Matrix.Multiply(m, device.Transform.World)
        m.RotateY(UV.yRotation * Math.PI / 180)
        device.Transform.World = Matrix.Multiply(m, device.Transform.World)
        m.RotateZ(UV.zRotation * Math.PI / 180)
        device.Transform.World = Matrix.Multiply(m, device.Transform.World)
        ' VISUAL STYLE
        Dim textureDefault As Texture = Nothing
        Select Case UV.selectedStyle
            Case ClassCA.VisualStyles.defaultStyle
                textureDefault = Nothing
            Case ClassCA.VisualStyles.FlatTransparent
                textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/glassflat.png")
            Case ClassCA.VisualStyles.GlassCube
                textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/glasstry.png")
            Case ClassCA.VisualStyles.Sketchy
                textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/strongline.png")
            Case ClassCA.VisualStyles.GlassSky
                textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/glasssky.png")
            Case ClassCA.VisualStyles.GlassBlur
                textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/glassblur.png")
            Case ClassCA.VisualStyles.KohInor2B
                textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/kohinor.png")
        End Select
        If Not textureDefault Is Nothing Then
            device.SetTexture(0, textureDefault)
            device.SetTexture(1, textureDefault)
        End If

        ' RUTINA ZA PRAVLJENJE MESHA
        Dim mat As New Direct3D.Material
        mat.SpecularSharpness = 22
        Dim vvv() As CustomVertex.PositionNormalTextured = UV.vBuffer.ToArray
        Dim c1, i As Integer
        c1 = vvv.Length
        Dim indices As New List(Of Short)
        For i = 0 To c1 - 1
            indices.Add(i)
        Next
        Dim ind() As Short = indices.ToArray

        Dim box As New Mesh(c1 / 3, c1, 0, CustomVertex.PositionNormalTextured.Format, device)
        Try
            box.SetVertexBufferData(vvv, LockFlags.None)
            box.SetIndexBufferData(ind, LockFlags.None)
            box.ComputeNormals()
            Dim subset() As Integer = box.LockAttributeBufferArray(LockFlags.Discard)
            subset = UV.subset.ToArray
            box.UnlockAttributeBuffer(subset)
            ' Optimize.
            Dim adjacency(box.NumberFaces * 3 - 1) As Integer
            box.GenerateAdjacency(CSng(0.1), adjacency)
            box.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            If Me.checkClick And Me.DoesMouseHitMesh(box, chkx, chky, device) Then
                Me.SelectedObject = UV
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            device.VertexFormat = CustomVertex.PositionNormalTextured.Format
            mat.Diffuse = f1
            'mat.Ambient = f1
            device.Material = mat
            box.DrawSubset(0)
            mat.Diffuse = f2
            'mat.Ambient = f2
            device.Material = mat
            box.DrawSubset(1)
            box.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim vvl() As CustomVertex.PositionColored = UV.lineBuffer.ToArray
        If UV.Equals(Me.SelectedObject) Then
            device.RenderState.PointSize = 10
            device.DrawUserPrimitives(PrimitiveType.PointList, vvl.Length, vvl)
        End If
        device.VertexFormat = CustomVertex.PositionColored.Format
        device.DrawUserPrimitives(PrimitiveType.LineList, vvl.Length / 2, vvl)
        device.Transform.World = om
        If Not textureDefault Is Nothing Then
            device.SetTexture(0, Nothing)
            device.SetTexture(1, Nothing)
            textureDefault.Dispose()
        End If
    End Sub
    Public Sub drawGrid(ByVal slika As Device)
        slika.VertexFormat = CustomVertex.PositionColored.Format
        Dim vertices1 As CustomVertex.PositionColored() = New CustomVertex.PositionColored(2) {}
        Dim vertices2 As CustomVertex.PositionColored() = New CustomVertex.PositionColored(2) {}
        vertices1(0).Color = gridColor.ToArgb
        vertices1(1).Color = gridColor.ToArgb
        vertices2(0).Color = gridColor.ToArgb
        vertices2(1).Color = gridColor.ToArgb
        Dim i As Integer = 0
        For i = minV To maxV Step gridStep
            vertices1(0).Position = New Vector3(i, minV, 0)
            vertices1(1).Position = New Vector3(i, maxV, 0)
            slika.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices1)
            vertices2(0).Position = New Vector3(minV, i, 0)
            vertices2(1).Position = New Vector3(maxV, i, 0)
            slika.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices2)
        Next
    End Sub
    Public Sub setRenderState(ByVal device As Direct3D.Device)
        With device.RenderState
            .Lighting = Me.pLighting
            .FillMode = Me.pFillMode
            .ZBufferEnable = True
            .AntiAliasedLineEnable = True
            .MultiSampleAntiAlias = True
            .DitherEnable = True
            .NormalizeNormals = True
            .TwoSidedStencilMode = True
            .ShadeMode = Me.pShadeMode
            .CullMode = Cull.None
            .AmbientMaterialSource = ColorSource.Color1
            .Ambient = Me.pAmbient
            .SourceBlend = Blend.SourceAlpha
            .DestinationBlend = Blend.InvSourceAlpha
            .LocalViewer = Me.hglght
            .SeparateAlphaBlendEnabled = True
            .AlphaBlendOperation = Me.bo
            .AlphaBlendEnable = True
            .AlphaTestEnable = True
            .AlphaFunction = Me.comf
            .SpecularEnable = True
        End With
    End Sub
    Public Sub setLights(ByVal device As Direct3D.Device)
        Dim i As Integer
        For i = 0 To Me.LightClass.Count - 1
            With device.Lights(i)
                .Type = Me.LightClass(i).Type
                .Diffuse = Me.LightClass(i).Diffuse
                .Ambient = Me.LightClass(i).Ambient
                .Specular = Me.LightClass(i).Specular
                .Position = Me.LightClass(i).pPosition
                .Direction = Me.LightClass(i).pDirection
                .Range = 1000
                .Falloff = 1
                .Attenuation0 = 1
                .Attenuation1 = 0
                .Attenuation2 = 0
                .Enabled = Me.LightClass(i).Enabled
                .Update()
            End With
        Next
    End Sub
    Public Sub drawMesh(ByVal device As Direct3D.Device)
        'Now, with this simple routine, we can render our mesh object
        Dim X As Int32
        With moMesh
            For X = 0 To mlMeshMaterials - 1
                device.Material = moMaterials(X)
                device.SetTexture(0, moTextures(X))
                moMesh.DrawSubset(X)
            Next X
        End With
    End Sub
    Public Sub drawISO(ByVal device As Direct3D.Device, ByVal ISO As ClassISO)
        Dim vvv() As CustomVertex.PositionColored = ISO.xyzVal.ToArray
        device.RenderState.PointSize = 10
        device.VertexFormat = CustomVertex.PositionColored.Format
        device.DrawUserPrimitives(PrimitiveType.TriangleList, vvv.Length / 3, vvv)
    End Sub
    Public Sub drawCA(ByVal device As Direct3D.Device, ByVal ca As ClassCA)
        Try
            Dim vvv() As CustomVertex.PositionNormalTextured = ca.bufferT.ToArray
            Dim mat As New Direct3D.Material
            mat.Diffuse = Color.FromArgb(ca.providnost, ca.CubeColor)
            mat.SpecularSharpness = 22
            device.Material = mat
            Dim textureDefault As Texture = Nothing
            Select Case ca.selectedStyle
                Case ClassCA.VisualStyles.defaultStyle
                    textureDefault = Nothing
                Case ClassCA.VisualStyles.FlatTransparent
                    textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/glassflat.png")
                Case ClassCA.VisualStyles.GlassCube
                    textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/glasstry.png")
                Case ClassCA.VisualStyles.Sketchy
                    textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/strongline.png")
                Case ClassCA.VisualStyles.GlassSky
                    textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/glasssky.png")
                Case ClassCA.VisualStyles.GlassBlur
                    textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/glassblur.png")
                Case ClassCA.VisualStyles.KohInor2B
                    textureDefault = TextureLoader.FromFile(device, My.Application.Info.DirectoryPath + "/shaders/kohinor.png")
            End Select
            device.SetTexture(0, textureDefault)
            device.VertexFormat = CustomVertex.PositionNormalTextured.Format
            Dim oldmatrix, tmatrix As Matrix
            oldmatrix = device.Transform.World
            tmatrix.Translate(ca.xpolozaj, ca.ypolozaj, ca.zpolozaj)
            device.Transform.World = Matrix.Multiply(tmatrix, device.Transform.World)
            tmatrix.RotateX(ca.xRotation * Math.PI / 180)
            device.Transform.World = Matrix.Multiply(tmatrix, device.Transform.World)
            tmatrix.RotateY(ca.yRotation * Math.PI / 180)
            device.Transform.World = Matrix.Multiply(tmatrix, device.Transform.World)
            tmatrix.RotateZ(ca.zRotation * Math.PI / 180)
            device.Transform.World = Matrix.Multiply(tmatrix, device.Transform.World)
            Dim c, i As Integer
            c = vvv.Length
            If ca.matrice(0).Contains(1) Then
                If (c - 1) > device.DeviceCaps.MaxVertexIndex Then
                    device.DrawUserPrimitives(PrimitiveType.TriangleList, vvv.Length / 3, vvv)
                Else
                    ' RUTINA ZA PRAVLJENJE MESHA
                    Dim indices As New List(Of Short)
                    For i = 0 To c - 1
                        indices.Add(i)
                    Next
                    Dim ind() As Short = indices.ToArray

                    Dim box As New Mesh(c / 3, c, 0, CustomVertex.PositionNormalTextured.Format, device)
                    Try
                        box.SetVertexBufferData(vvv, LockFlags.None)
                        box.SetIndexBufferData(ind, LockFlags.None)
                        box.ComputeNormals()
                        ' Optimize.
                        Dim adjacency(box.NumberFaces * 3 - 1) As Integer
                        box.GenerateAdjacency(CSng(0.1), adjacency)
                        box.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    Try
                        If Me.checkClick And Me.DoesMouseHitMesh(box, chkx, chky, device) Then
                            Me.SelectedObject = ca
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    Try

                        box.DrawSubset(0)
                        box.Dispose()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
                Dim vvl() As CustomVertex.PositionColored = ca.LineBuffer.ToArray
                Dim vvp() As CustomVertex.PositionColored = ca.DXFBuffer.ToArray
                device.VertexFormat = CustomVertex.PositionColored.Format
                If ca.Equals(Me.SelectedObject) Then
                    device.RenderState.PointSize = 10
                    device.DrawUserPrimitives(PrimitiveType.PointList, vvp.Length, vvp)
                End If
                device.DrawUserPrimitives(PrimitiveType.LineList, vvl.Length / 2, vvl)
            End If
            device.Transform.World = oldmatrix
            device.SetTexture(0, Nothing)
            textureDefault.Dispose()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub sceneFrameAdd(ByVal cf As ClassFrame)
        Me.sceneList.Add(cf)
    End Sub
    Function DoesMouseHitMesh(ByVal mesh As Mesh, ByVal x As Single, ByVal y As Single, ByVal device As Direct3D.Device) As Boolean
        Dim viewport As Viewport
        Dim world As Matrix
        Dim proj As Matrix
        Dim view As Matrix

        Dim vIn As Vector3, vNear As Vector3, vFar As Vector3, vDir As Vector3
        Dim ClosestHit As IntersectInformation

        viewport = device.Viewport
        world = device.Transform.World
        proj = device.Transform.Projection
        view = device.Transform.View

        vIn.X = x * device.Viewport.Width / cf3D.Width : vIn.Y = y * device.Viewport.Height / cf3D.Height

        'Najblize kursoru
        vIn.Z = 0
        vNear = Microsoft.DirectX.Vector3.Unproject(vIn, viewport, proj, view, world)

        'Udaljeno od kursora
        vIn.Z = 1
        vFar = Microsoft.DirectX.Vector3.Unproject(vIn, viewport, proj, view, world)

        'Pravac zraka
        vDir = Microsoft.DirectX.Vector3.Subtract(vFar, vNear)

        Try
            If mesh.Intersect(vNear, vDir, ClosestHit) = True Then
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function
    
    Enum lightType As Byte
        directional = 0
        point = 1
    End Enum

    Private Sub selectedCA_bufferRefreshed() Handles selectedCA.bufferRefreshed, selectedUV.bufferRefreshed
        RaiseEvent propertyChanged()
    End Sub
End Class