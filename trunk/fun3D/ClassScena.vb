Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Math
Imports System.ComponentModel
<System.Serializable()> _
Public Class ClassScena
    Implements iFun3DScene
    Private minV As Single = -100
    Private maxV As Single = 100
    Private gridStep As Single = 10
    Private gridColor As Color = Color.Black
    Private Uclass As New List(Of ClassU)
    Private UVclass As New List(Of ClassUV)
    Private ISOclass As New List(Of ClassISO)
    Private CAclass As New List(Of ClassCA)
    Private LSclass As New List(Of ClassLS)
    Private HUDClass As New List(Of ClassHUD)
    Private MeshClass As New List(Of ClassMesh)
    Private BlendingClass As New List(Of ClassBlending)
    Private CrackedPolyClass As New List(Of ClassCrackedPoly)
    Private PackingClass As New List(Of ClassPacking)
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
    Public Property preview As Image
    'Our Mesh Definitions
    <System.NonSerialized()> _
    Public moMesh As Mesh = Nothing
    Public mlMeshMaterials As Int32 = -1
    <System.NonSerialized()> _
    Public moMaterials() As Material
    <System.NonSerialized()> _
    Public moTextures() As Texture
    Public chkx As Integer = 0
    Public chky As Integer = 0
    Public checkClick As Boolean = False
    <Browsable(False)> _
    Public WithEvents selectedCA As ClassCA
    <Browsable(False)> _
    Public WithEvents selectedUV As ClassUV
    <Browsable(False)> _
    Public WithEvents selectedLS As ClassLS
    <Browsable(False)> _
    Public WithEvents selectedISO As ClassISO
    <Browsable(False)> _
    Public WithEvents selectedFun3DObject As IFun3DObject
    Dim selektovaniObjekat As Object
    Public Shared Event SelectionChanged()
    Public Shared Event propertyChanged()
    Public Shared Event progressStart()
    Public Shared Event progressEnd()
    Public Shared Event progress(ByVal p As Integer, ByVal m As String)
    Dim rgbv As Integer = 0
    Public Property Fun3DFileVersion As Integer Implements iFun3DScene.Fun3DFileVersion
    Public Sub New()
        Me.LightClass.Clear()
        Me.LightClass.Add(New ClassLight)
        Me.LightClass.Add(New ClassLight)
        Me.LightClass(1).pPosition = New Vector3(30, 30, 500)
        ' Shadow light position
        Me.lv(0) = 100
        Me.lv(1) = 100
        Me.lv(2) = 300
        Me.SelectedObject = Me
        Me.Fun3DFileVersion = 2
    End Sub
    <Browsable(False)> _
    Public Property SelectedObject() As Object
        Get
            Return selektovaniObjekat
        End Get
        Set(ByVal value As Object)
            Me.selektovaniObjekat = value
            If value.GetType Is GetType(ClassCA) Then
                Me.selectedCA = CType(value, ClassCA)
            ElseIf value.GetType Is GetType(ClassUV) Then
                Me.selectedUV = CType(value, ClassUV)
            ElseIf value.GetType Is GetType(ClassLS) Then
                Me.selectedLS = CType(value, ClassLS)
            ElseIf value.GetType Is GetType(ClassISO) Then
                Me.selectedISO = CType(value, ClassISO)
            ElseIf value.GetType Is GetType(ClassU) Then
                Me.selectedFun3DObject = CType(value, ClassU)
            End If
            RaiseEvent SelectionChanged()
        End Set
    End Property
    <Category("1. Meta")> _
    Public Property Description() As String
        Get
            Return Me.descr
        End Get
        Set(ByVal value As String)
            Me.descr = value
        End Set
    End Property
    <Category("1. Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Me.naziv = value
        End Set
    End Property
    <Category("5. Animation")> _
    Public Property SceneFrames() As List(Of ClassFrame)
        Get
            Return Me.sceneList
        End Get
        Set(ByVal value As List(Of ClassFrame))
            Me.sceneList = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property showShadows() As Boolean
        Get
            Return Me.shadow
        End Get
        Set(ByVal value As Boolean)
            Me.shadow = value
        End Set
    End Property
    <Category("2. Appearance")> _
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
    <Category("2. Appearance")> _
    Public Property ShadowsPosition() As Single()
        Get
            Return Me.lv
        End Get
        Set(ByVal value As Single())
            Me.lv = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property shadowsLightType() As ClassScena.lightType
        Get
            Return Me.shadowLT
        End Get
        Set(ByVal value As ClassScena.lightType)
            Me.shadowLT = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property backgroundColor() As Color
        Get
            Return Me.bckg
        End Get
        Set(ByVal value As Color)
            Me.bckg = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property PointSize() As Integer
        Get
            Return Me.pSize
        End Get
        Set(ByVal value As Integer)
            Me.pSize = value
        End Set
    End Property
    <Category("3. Light")> _
    Public Property sceneLights() As List(Of ClassLight)
        Get
            Return Me.LightClass
        End Get
        Set(ByVal value As List(Of ClassLight))
            Me.LightClass = value
        End Set
    End Property
    <Category("3. Light")> _
    Public Property Lighting() As Boolean
        Get
            Return Me.pLighting
        End Get
        Set(ByVal value As Boolean)
            Me.pLighting = value
        End Set
    End Property
    <Category("3. Light")> _
    Public Property Highlight() As Boolean
        Get
            Return Me.hglght
        End Get
        Set(ByVal value As Boolean)
            Me.hglght = value
        End Set
    End Property
    <Category("3. Light")> _
    Public Property BlendOperation() As Direct3D.BlendOperation
        Get
            Return Me.bo
        End Get
        Set(ByVal value As Direct3D.BlendOperation)
            Me.bo = value
        End Set
    End Property
    <Category("3. Light")> _
    Public Property AlphaFunction() As Direct3D.Compare
        Get
            Return Me.comf
        End Get
        Set(ByVal value As Direct3D.Compare)
            Me.comf = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property FillMode() As FillMode
        Get
            Return Me.pFillMode
        End Get
        Set(ByVal value As FillMode)
            Me.pFillMode = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property ShadeMode() As ShadeMode
        Get
            Return Me.pShadeMode
        End Get
        Set(ByVal value As ShadeMode)
            Me.pShadeMode = value
        End Set
    End Property
    <Category("3. Light")> _
    Public Property Ambient() As Color
        Get
            Return Me.pAmbient
        End Get
        Set(ByVal value As Color)
            Me.pAmbient = value
        End Set
    End Property
    <Category("4. Grid")> _
    Public Property gridMinV() As Single
        Get
            Return Me.minV
        End Get
        Set(ByVal value As Single)
            Me.minV = value
        End Set
    End Property
    <Category("4. Grid")> _
    Public Property gridMaxV() As Single
        Get
            Return Me.maxV
        End Get
        Set(ByVal value As Single)
            Me.maxV = value
        End Set
    End Property
    <Category("4. Grid")> _
    Public Property stepGrid() As Single
        Get
            Return Me.gridStep
        End Get
        Set(ByVal value As Single)
            Me.gridStep = value
        End Set
    End Property
    <Category("4. Grid")> _
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
    <Browsable(False)> _
    Public Property HUDList() As List(Of ClassHUD)
        Get
            Return Me.HUDClass
        End Get
        Set(ByVal value As List(Of ClassHUD))
            Me.HUDClass = value
        End Set
    End Property
    <Browsable(False)> _
    Public Property MeshList() As List(Of ClassMesh)
        Get
            Return Me.MeshClass
        End Get
        Set(ByVal value As List(Of ClassMesh))
            Me.MeshClass = value
        End Set
    End Property
    Public Property BlendingList() As List(Of ClassBlending)
        Get
            Return Me.BlendingClass
        End Get
        Set(ByVal value As List(Of ClassBlending))
            Me.BlendingClass = value
        End Set
    End Property
    <Browsable(False)> _
    Public Property CrackedPolyList() As List(Of ClassCrackedPoly)
        Get
            Return Me.CrackedPolyClass
        End Get
        Set(ByVal value As List(Of ClassCrackedPoly))
            Me.CrackedPolyClass = value
        End Set
    End Property
    '<Browsable(False)> _
    Public Property PackingList() As List(Of ClassPacking)
        Get
            Return Me.PackingClass
        End Get
        Set(ByVal value As List(Of ClassPacking))
            Me.PackingClass = value
        End Set
    End Property
    Public Sub drawScene(ByVal device As Direct3D.Device)

        Me.setRenderState(device)
        Me.setLights(device)
        Me.drawGrid(device)
        Dim UVC As ClassUV
        Dim UC As ClassU
        Dim ISO As ClassISO
        Dim CA As ClassCA
        Dim LS As ClassLS
        'Dim HUD As ClassHUD
        Dim CM As ClassMesh
        Dim BS As ClassBlending
        Dim CP As ClassCrackedPoly
        Dim PC As ClassPacking
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

        For Each CM In Me.MeshClass
            Me.drawMesh(CM, device)
        Next
        For Each BS In Me.BlendingClass
            Me.drawBlending(device, BS)
        Next
        For Each CP In Me.CrackedPolyClass
            Me.drawCrackedPoly(device, CP)
        Next
        For Each PC In Me.PackingClass
            Me.drawPacking(device, PC)
        Next
        'For Each HUD In Me.HUDClass
        '    Me.drawHUD(device, HUD)
        'Next
        If Me.showShadows Then
            Dim om As Matrix = device.Transform.World
            Dim nm As Matrix = device.Transform.World
            nm.Shadow(New Vector4(lv(0), lv(1), lv(2), 1), New Plane(0, 0, 1, 0))
            device.Transform.World = Matrix.Multiply(nm, device.Transform.World)
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
            'For Each HUD In Me.HUDClass
            '    Me.drawHUD(device, HUD)
            'Next
            For Each CM In Me.MeshClass
                Me.drawMesh(CM, device)
            Next
            For Each BS In Me.BlendingClass
                Me.drawBlending(device, BS)
            Next
            For Each CP In Me.CrackedPolyClass
                Me.drawCrackedPoly(device, CP)
            Next
            For Each PC In Me.PackingClass
                Me.drawPacking(device, PC)
            Next
            device.Transform.World = om
        End If
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

    End Sub
    Public Sub drawPacking(ByVal device As Direct3D.Device, ByVal PC As ClassPacking)
        'device.RenderState.CullMode = Cull.Clockwise
        Dim CM As ClassMesh
        For Each CM In PC.cMesh
            Me.drawMesh(CM, device)
        Next
        If PC.DrawBound Then
            Me.drawMesh(PC.bnd, device)
        End If
        device.RenderState.CullMode = Cull.None
    End Sub
    Public Sub drawBlending(ByVal device As Direct3D.Device, ByVal BS As ClassBlending)
        Dim vvv() As CustomVertex.PositionColored = BS.LineBuffer.ToArray
        Dim oldformat As Integer
        oldformat = device.VertexFormat
        device.VertexFormat = CustomVertex.PositionColored.Format
        device.DrawUserPrimitives(PrimitiveType.LineStrip, vvv.Length, vvv)

        device.VertexFormat = CType(oldformat, VertexFormats)
    End Sub
    Public Sub drawCrackedPoly(ByVal device As Direct3D.Device, ByVal CP As ClassCrackedPoly)
        If CP.triangles.Count > 0 Then
            Dim vvv() As CustomVertex.PositionColored = CP.lineBuffer.ToArray
            device.VertexFormat = CustomVertex.PositionColored.Format
            device.DrawUserPrimitives(PrimitiveType.LineStrip, CInt(vvv.Length / 2), vvv)
            Dim oldformat As Integer
            oldformat = device.VertexFormat
            device.VertexFormat = CustomVertex.PositionNormalTextured.Format
            Dim mat As New Direct3D.Material
            mat.SpecularSharpness = 22
            mat.Diffuse = Color.FromArgb(CP.transparency, CP.color)
            device.Material = mat
            CP.mesh.DrawSubset(0)
            Try
                If Me.checkClick And Me.DoesMouseHitMesh(CP.mesh, chkx, chky, device) Then
                    Me.SelectedObject = CP
                End If
            Catch ex As Exception
                'console.writeline(ex.Message)
            End Try
            device.VertexFormat = CustomVertex.PositionColored.Format
            device.DrawUserPrimitives(PrimitiveType.LineList, CInt(vvv.Length / 2), vvv)
            device.VertexFormat = CType(oldformat, VertexFormats)
        ElseIf CP.PolygonPoints.Count > 0 Then
            Dim v As Vector3
            Dim vvv As New List(Of CustomVertex.PositionColored)
            For Each v In CP.PolygonPoints
                vvv.Add(New CustomVertex.PositionColored(v, Color.Red.ToArgb))
            Next
            device.RenderState.PointSize = Me.PointSize
            device.VertexFormat = CustomVertex.PositionColored.Format
            device.DrawUserPrimitives(PrimitiveType.PointList, vvv.Count, vvv.ToArray)
        End If
    End Sub
    Public Sub drawHUD(ByVal device As Direct3D.Device, ByVal HUD As ClassHUD)
        HUD.s.Begin(SpriteFlags.AlphaBlend)
        HUD.s.Draw2D(HUD.t, New Point(0, 0), 0, New Point(0, 0), Color.White)
        HUD.s.End()
    End Sub
    Public Sub drawLS(ByVal device As Direct3D.Device, ByVal LS As ClassLS)

        Dim mat As New Direct3D.Material
        mat.Diffuse = LS.Colour
        device.Material = mat
        Dim oldm As Matrix = device.Transform.World
        Dim lvlm As Matrix = device.Transform.World
        Dim str As String = LS.States(LS.States.Count - 1)
        'If LS.showAllIter Then
        '    'For Each str In LS.States
        '    '    drawLSExtracted(device, LS, str)
        '    '    lvlm = Matrix.Multiply(Matrix.Translation(0, 0, LS.levelDistance), lvlm)
        '    '    device.Transform.World = lvlm
        '    'Next
        'Else
        If LS.Shape = ClassLS.oblik.line Then
            Dim vvv() As CustomVertex.PositionColored = LS.LineBuffer.ToArray
            Dim oldformat As Integer
            oldformat = device.VertexFormat
            device.VertexFormat = CustomVertex.PositionColored.Format
            device.DrawUserPrimitives(PrimitiveType.LineList, CInt(vvv.Length / 2), vvv)

            device.VertexFormat = CType(oldformat, VertexFormats)
        ElseIf LS.Shape = ClassLS.oblik.mesh Then
            Dim cm As ClassMesh
            For Each cm In LS.meshBuffer
                drawMesh(cm, device)
            Next
        Else
            Dim vvv1() As CustomVertex.PositionNormalTextured = LS.bufferT.ToArray
            Dim c, i As Integer
            c = vvv1.Length

            ' RUTINA ZA PRAVLJENJE MESHA
            Dim indices As New List(Of Integer)
            For i = 0 To c - 1
                indices.Add(i)
            Next
            Dim ind() As Integer = indices.ToArray

            Dim box As New Mesh(CInt(c / 3), c, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, device)
            Try
                box.SetVertexBufferData(vvv1, LockFlags.None)
                box.SetIndexBufferData(ind, LockFlags.None)
                box.ComputeNormals()
                ' Optimize.
                Dim adjacency(box.NumberFaces * 3 - 1) As Integer
                box.GenerateAdjacency(CSng(0.1), adjacency)
                box.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            Try
                If Me.checkClick And Me.DoesMouseHitMesh(box, chkx, chky, device) Then
                    Me.SelectedObject = LS
                End If
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            Try

                box.DrawSubset(0)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

            box.Dispose()
        End If
        'End If
        device.Transform.World = oldm
    End Sub
    Public Sub drawU(ByVal device As Direct3D.Device, ByVal UV As ClassU, ByVal selected As Boolean)
        Dim lineMatrix = device.Transform.World * device.Transform.View * device.Transform.Projection
        ' linije
        device.VertexFormat = CustomVertex.PositionNormalColored.Format
        Dim d3l As New Direct3D.Line(device)

        Dim i As Integer = 0
        Dim pcount As Integer = 512
        Dim tpc As Integer = UV.tgeom.vb.Length - 1
        d3l.GlLines = True
        d3l.Width = UV.LineWidth
        d3l.Antialias = True

        If ((tpc - i) Mod 512) <> 0 Then pcount = (tpc - i) Mod 512
        d3l.Begin()
        d3l.DrawTransform(UV.tgeom.vb, lineMatrix, Color.FromArgb(UV.Transparency, UV.LineColor))
        d3l.End()

        d3l.Dispose()
        ' tacke
        If UV.Equals(Me.SelectedObject) Then
            device.RenderState.PointSize = Me.PointSize
            Dim ver(2) As CustomVertex.PositionColored
            ver(0) = New CustomVertex.PositionColored(UV.tgeom.vb(0), UV.LineColor.ToArgb)
            ver(1) = New CustomVertex.PositionColored(UV.tgeom.vb(UV.tgeom.vb.Length - 1), UV.LineColor.ToArgb)
            device.VertexFormat = CustomVertex.PositionColored.Format
            device.DrawUserPrimitives(PrimitiveType.PointList, ver.Length, ver)
        End If

    End Sub
    Public Sub drawUV(ByVal device As Direct3D.Device, ByVal UV As ClassUV)

        Dim vertices As CustomVertex.PositionNormalTextured() 'an array of vertices
        Dim vertices1 As CustomVertex.PositionNormalColored()
        vertices = New CustomVertex.PositionNormalTextured(2) {}
        vertices1 = New CustomVertex.PositionNormalColored(7) {}
        Dim l As Color
        l = UV.LineColor
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
            Case ClassCA.VisualStyles.Fluid
                Dim m As New Bitmap(100, 100, Imaging.PixelFormat.Format32bppArgb)
                Dim g As Graphics = Graphics.FromImage(m)
                Dim p As New Pen(Color.Black, UV.lineWidth)
                Dim b As New Drawing2D.HatchBrush(UV.Hatch, Color.FromArgb(UV.Transparency, UV.bojaPolja1), Color.FromArgb(UV.Transparency, UV.bojaPolja2))
                Dim b1 As New Drawing2D.LinearGradientBrush(New Rectangle(0, 0, 100, 100), f1, f2, Drawing2D.LinearGradientMode.ForwardDiagonal)
                g.FillRectangle(b1, 0, 0, 100, 100)
                g.DrawRectangle(p, 0, 0, 100, 100)
                Dim matr As New Drawing2D.Matrix
                Me.rgbv += 1
                If Me.rgbv > 359 Then Me.rgbv = 0
                matr.RotateAt(Me.rgbv, New PointF(50, 50))
                g.MultiplyTransform(matr)
                g.FillEllipse(Brushes.ForestGreen, 0, 0, 70, 70)
                Dim m1 As New IO.MemoryStream
                m.Save(m1, Imaging.ImageFormat.Bmp)
                m1.Seek(0, IO.SeekOrigin.Begin)
                textureDefault = TextureLoader.FromStream(device, m1)
                'textureDefault = TextureLoader.FromStream(device, m1, 0, 0, 1, Direct3D.Usage.None, Direct3D.Format.A8R8G8B8, Direct3D.Pool.Managed, Direct3D.Filter.None, Direct3D.Filter.None, System.Drawing.Color.ForestGreen.ToArgb)
                f1 = Color.FromArgb(UV.Transparency, Color.White)
                f2 = Color.FromArgb(UV.Transparency, Color.White)
            Case ClassCA.VisualStyles.Enviroment
                textureDefault = UV.enviroment
        End Select
        If Not textureDefault Is Nothing Then
            device.SetTexture(0, textureDefault)
            device.SetTexture(1, textureDefault)
        End If


        Try
            If Me.checkClick And Me.DoesMouseHitMesh(UV.UVMesh, chkx, chky, device) Then
                Me.SelectedObject = UV
            End If
        Catch ex As Exception
            'console.writeline(ex.Message)
        End Try
        Dim mat As New Direct3D.Material
        mat.SpecularSharpness = 22

        Dim vvl() As CustomVertex.PositionColored = UV.lineBuffer.ToArray
        If UV.Equals(Me.SelectedObject) Then
            device.RenderState.PointSize = 10
            device.DrawUserPrimitives(PrimitiveType.PointList, vvl.Length, vvl)
        End If
        device.VertexFormat = CustomVertex.PositionColored.Format
        device.DrawUserPrimitives(PrimitiveType.LineList, CInt(vvl.Length / 2), vvl)
        'If UV.Transparency < 255 Then
        '    device.RenderState.ZBufferWriteEnable = False
        'End If
        Try
            If Not UV.stress Then
                device.VertexFormat = CustomVertex.PositionNormalTextured.Format
            Else
                device.VertexFormat = CustomVertex.PositionNormalColored.Format
            End If

            device.RenderState.CullMode = Cull.Clockwise
            mat.Diffuse = f1
            'mat.Ambient = f1
            device.Material = mat
            UV.UVMesh.DrawSubset(0)
            device.RenderState.CullMode = Cull.CounterClockwise
            mat.Diffuse = f2
            'mat.Ambient = f2
            device.Material = mat
            UV.UVMesh.DrawSubset(0)
            device.RenderState.CullMode = Cull.None

        Catch ex As Exception
            'console.writeline(ex.Message)
        End Try
        'device.RenderState.ZBufferWriteEnable = True

        If Not textureDefault Is Nothing Then
            device.SetTexture(0, Nothing)
            device.SetTexture(1, Nothing)
            If Not textureDefault.Equals(UV.enviroment) Then
                textureDefault.Dispose()
            End If

        End If
        If UV.lineWidth > 0 Then
            Dim lineMatrix = device.Transform.World * device.Transform.View * device.Transform.Projection
            device.VertexFormat = CustomVertex.PositionColored.Format

            Try
                Dim d3l As New Direct3D.Line(device)

                d3l.Width = UV.lineWidth
                d3l.Antialias = True
                d3l.Begin()
                Dim i As Integer
                Dim lb(1) As Vector3
                For i = 0 To UV.lineBuffer1.Count - 1 Step 2
                    lb(0) = UV.lineBuffer1(i)
                    lb(1) = UV.lineBuffer1(i + 1)
                    d3l.DrawTransform(lb, lineMatrix, UV.LineColor)
                Next
                d3l.End()
                d3l.Dispose()
            Catch ex As Exception
                'console.writeline(ex.Message)
            End Try
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
        Dim i As Single = 0
        For i = 0 To maxV Step gridStep
            vertices1(0).Position = New Vector3(i, minV, 0)
            vertices1(1).Position = New Vector3(i, maxV, 0)
            slika.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices1)
            vertices2(0).Position = New Vector3(minV, i, 0)
            vertices2(1).Position = New Vector3(maxV, i, 0)
            slika.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices2)
        Next
        For i = -gridStep To minV Step -gridStep
            vertices1(0).Position = New Vector3(i, minV, 0)
            vertices1(1).Position = New Vector3(i, maxV, 0)
            slika.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices1)
            vertices2(0).Position = New Vector3(minV, i, 0)
            vertices2(1).Position = New Vector3(maxV, i, 0)
            slika.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices2)
        Next
        vertices1(0).Color = Color.Red.ToArgb
        vertices1(1).Color = Color.Red.ToArgb
        vertices2(0).Color = Color.Green.ToArgb
        vertices2(1).Color = Color.Green.ToArgb
        vertices1(0).Position = New Vector3(0, minV, 0)
        vertices1(1).Position = New Vector3(0, maxV, 0)
        slika.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices1)
        vertices2(0).Position = New Vector3(minV, 0, 0)
        vertices2(1).Position = New Vector3(maxV, 0, 0)
        slika.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices2)
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
            .ReferenceAlpha = Convert.ToInt32("01000000", 16)
            '.ZBufferFunction = Compare.Always
            .ZBufferWriteEnable = True
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
                .Range = Me.LightClass(i).Range
                .Falloff = 1
                .Attenuation0 = 0.75
                .Attenuation1 = 0
                .Attenuation2 = 0
                .Enabled = Me.LightClass(i).Enabled
                .Update()

            End With
        Next
    End Sub
    Public Sub drawMesh(ByVal mesh As ClassMesh, ByVal device As Direct3D.Device)
        Try
            Dim m As New Material
            m.Diffuse = Color.FromArgb(mesh.transparency, mesh.color)
            'm.Ambient = Color.FromArgb(mesh.transparency, mesh.color)
            'm.Emissive = Color.FromArgb(mesh.transparency, Color.Black)
            'm.Specular = Color.FromArgb(mesh.transparency, Color.White)
            m.SpecularSharpness = 22

            device.Material = m
            device.VertexFormat = CustomVertex.PositionNormalTextured.Format
            mesh.mesh.DrawSubset(0)
            Try
                If Me.checkClick And Me.DoesMouseHitMesh(mesh.mesh, chkx, chky, device) Then
                    If mesh.parent Is Nothing Then
                        Me.SelectedObject = mesh
                    Else
                        Me.SelectedObject = mesh.parent
                    End If

                End If
            Catch ex As Exception
                'console.writeline(ex.Message)
            End Try
        Catch ex As Exception

        End Try
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
            If mlMeshMaterials < 0 Then
                Dim m As New Material
                m.Diffuse = Color.Red
                device.Material = m
                device.VertexFormat = CustomVertex.PositionNormalTextured.Format
                moMesh.DrawSubset(0)
            End If
        End With
    End Sub
    Public Sub drawISO(ByVal device As Direct3D.Device, ByVal ISO As ClassISO)
        device.VertexFormat = CustomVertex.PositionNormalTextured.Format

        ' select
        Try
            If Me.checkClick And Me.DoesMouseHitMesh(ISO.ISOMesh, chkx, chky, device) Then
                Me.SelectedObject = ISO
            End If
        Catch ex As Exception
            'console.writeline(ex.Message)
        End Try
        Try
            Dim mat As Material
            device.RenderState.CullMode = Cull.CounterClockwise
            mat.Diffuse = Color.FromArgb(ISO.Transparency, ISO.BackColor)
            'mat.Ambient = f1
            device.Material = mat
            ISO.ISOMesh.DrawSubset(0)
            device.RenderState.CullMode = Cull.Clockwise
            mat.Diffuse = Color.FromArgb(ISO.Transparency, ISO.FrontColor)
            'mat.Ambient = f1
            device.Material = mat
            ISO.ISOMesh.DrawSubset(0)

        Catch ex As Exception

        End Try
        device.RenderState.CullMode = Cull.None
    End Sub
    Public Sub drawCA(ByVal device As Direct3D.Device, ByVal ca As ClassCA)
        Try

            Dim mat As New Direct3D.Material
            mat.SpecularSharpness = 22
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

            Dim i As Integer
            If ca.matrice(0).Contains(1) Then
                If ca.meshBuffer.Count > 0 And ca.shp = ClassCA.shapes.Mesh Then
                    Dim cm As ClassMesh
                    For Each cm In ca.meshBuffer
                        drawMesh(cm, device)
                    Next
                ElseIf ca.ISOMesh IsNot Nothing And ca.drawISO Then
                    mat.Diffuse = Color.FromArgb(ca.Transparency, ca.CubeColor)
                    device.Material = mat
                    ca.ISOMesh.DrawSubset(0)
                    Try
                        If Me.checkClick And Me.DoesMouseHitMesh(ca.ISOMesh, chkx, chky, device) Then
                            Me.SelectedObject = ca
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                ElseIf ca.CAMesh IsNot Nothing Then


                    Try
                        If Me.checkClick And Me.DoesMouseHitMesh(ca.CAMesh, chkx, chky, device) Then
                            Me.SelectedObject = ca
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                    Try
                        For i = 0 To ca.nOfLevels
                            mat.Diffuse = Color.FromArgb(ca.providnost, ca.levelColor(i))
                            mat.SpecularSharpness = 22
                            device.Material = mat
                            ca.CAMesh.DrawSubset(i)
                        Next

                    Catch ex As Exception
                        'console.writeline(ex.Message)
                    End Try
                End If
                Dim vvl() As CustomVertex.PositionColored = ca.LineBuffer.ToArray
                Dim vvp() As CustomVertex.PositionColored = ca.DXFBuffer.ToArray
                device.VertexFormat = CustomVertex.PositionColored.Format
                If ca.Equals(Me.SelectedObject) Then
                    device.RenderState.PointSize = 10
                    device.DrawUserPrimitives(PrimitiveType.PointList, vvp.Length, vvp)
                End If
                If ca.ShowEdges Then
                    If ca.LineWidth > 0 Then
                        Dim lineMatrix = device.Transform.World * device.Transform.View * device.Transform.Projection
                        device.VertexFormat = CustomVertex.PositionColored.Format

                        Try
                            Dim d3l As New Direct3D.Line(device)

                            d3l.Width = ca.LineWidth
                            d3l.Antialias = True
                            d3l.Begin()

                            Dim lb(1) As Vector3
                            For i = 0 To vvl.Length - 1 Step 2
                                lb(0) = vvl(i).Position
                                lb(1) = vvl(i + 1).Position
                                d3l.DrawTransform(lb, lineMatrix, ca.LineColor)
                            Next
                            d3l.End()
                            d3l.Dispose()
                        Catch ex As Exception
                            'console.writeline(ex.Message)
                        End Try
                    Else
                        device.DrawUserPrimitives(PrimitiveType.LineList, CInt(vvl.Length / 2), vvl)
                    End If
                End If
            End If

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
            'console.writeline(ex.Message)
        End Try

    End Function
    Public Function LocMouseHitPlane(ByVal pln As Plane, ByVal x As Single, ByVal y As Single, ByVal device As Direct3D.Device) As Vector3
        Dim viewport As Viewport
        Dim world As Matrix
        Dim proj As Matrix
        Dim view As Matrix

        Dim vIn As Vector3, vNear As Vector3, vFar As Vector3, vDir As Vector3


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

            Return Plane.IntersectLine(pln, vNear, vFar)

        Catch ex As Exception
            'console.writeline(ex.Message)
        End Try

    End Function
    Enum lightType As Byte
        directional = 0
        point = 1
    End Enum
    Public Sub afterPaste(ByVal device As Device)
        Dim UVC As ClassUV
        Dim UC As ClassU
        Dim ISO As ClassISO
        Dim CA As ClassCA
        Dim LS As ClassLS
        Dim HUD As ClassHUD
        Dim CM As ClassMesh
        Dim CP As ClassCrackedPoly
        Dim BS As ClassBlending
        For Each CM In Me.MeshClass
            CM.afterPaste(device)
        Next
        For Each UVC In Me.UVclass
            UVC.afterPaste(device)
        Next
        For Each UC In Me.Uclass
            UC.afterPaste(device)
        Next
        For Each ISO In Me.ISOclass
            ISO.afterPaste(device)
        Next
        For Each CA In Me.CAclass
            CA.afterPaste(device)
        Next
        For Each LS In Me.LSclass
            LS.afterPaste(device)
        Next
        For Each HUD In Me.HUDClass
            HUD.afterPaste(device)
        Next
        If Me.BlendingClass Is Nothing Then
            Me.BlendingClass = New List(Of ClassBlending)
        End If
        For Each BS In Me.BlendingClass
            BS.afterPaste(device)
        Next
        If Me.CrackedPolyClass Is Nothing Then
            Me.CrackedPolyClass = New List(Of ClassCrackedPoly)
        End If
        For Each CP In Me.CrackedPolyClass
            CP.afterPaste(device)
        Next
    End Sub
    Private Sub selectedCA_bufferRefreshed() Handles selectedCA.bufferRefreshed, selectedUV.bufferRefreshed
        RaiseEvent propertyChanged()
    End Sub

    Private Sub selectedLS_progress(ByVal p As Integer, ByVal m As String) Handles selectedLS.progress, selectedCA.progress, selectedISO.progress, selectedUV.progress
        RaiseEvent progress(p, m)
    End Sub

    Private Sub selectedLS_progressEnd() Handles selectedLS.progressEnd, selectedCA.progressEnd, selectedISO.progressEnd, selectedUV.progressEnd
        RaiseEvent progressEnd()
    End Sub

    Private Sub selectedLS_progressStart() Handles selectedLS.progressStart, selectedCA.progressStart, selectedISO.progressStart, selectedUV.progressStart
        RaiseEvent progressStart()
    End Sub

    Private Sub selectedFun3DObject_bufferRefreshed() Handles selectedFun3DObject.bufferRefreshed
        RaiseEvent propertyChanged()
    End Sub

    Private Sub selectedFun3DObject_progress(ByVal p As Integer, ByVal m As String) Handles selectedFun3DObject.progress
        RaiseEvent progress(p, m)
    End Sub

    Private Sub selectedFun3DObject_progressEnd() Handles selectedFun3DObject.progressEnd
        RaiseEvent progressEnd()
    End Sub

    Private Sub selectedFun3DObject_progressStart() Handles selectedFun3DObject.progressStart
        RaiseEvent progressStart()
    End Sub
End Class
