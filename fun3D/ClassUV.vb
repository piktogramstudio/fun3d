Imports System.Math
Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Drawing.Design

<System.Serializable()> _
Public Class ClassUV
    Implements IFun3DObject

#Region "Fields"
    Dim metaData As New sMetaData("UV surface")
    Dim lineAppearance As New sLineAppearance(2, Color.Black, 255)
    Dim frontSurfaceAppearance As New sSurfaceAppearance(Color.Green, Color.Green, Color.White, Color.White, 1)
    Dim backSurfaceAppearance As New sSurfaceAppearance(Color.Yellow, Color.Yellow, Color.White, Color.White, 1)
    'TO DO CreateColoredTwoSidedMeshFromGeometry
#End Region

    Private UGustina As Integer = 10
    Private VGustina As Integer = 10
    Public maxU As Single = 10
    Public maxV As Single = 10
    Public minU As Single = -10
    Public minV As Single = -10
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
    Public a As Single = 0
    Public u As Single = 0
    Public v As Single = 0
    Public prm As New List(Of ClassParametri)
    <System.NonSerialized()> _
    Public vBuffer As New List(Of CustomVertex.PositionNormalTextured)
    <System.NonSerialized()> _
    Public vsBuffer As New List(Of CustomVertex.PositionNormalColored)
    <System.NonSerialized()> _
    Public lineBuffer As New List(Of CustomVertex.PositionColored)
    Public lineBuffer1 As New List(Of Vector3)
    Public subset As New List(Of Integer)
    Public selectedStyle As VisualStyles = VisualStyles.defaultStyle
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
    Public Event bufferRefreshed() Implements IFun3DObject.bufferRefreshed
    Public Event progressStart() Implements IFun3DObject.progressStart
    Public Event progressEnd() Implements IFun3DObject.progressEnd
    Public Event progress(ByVal p As Integer, ByVal m As String) Implements IFun3DObject.progress
#End Region

#Region "1. Meta Properties"
    <Category("1. Meta"), DisplayName("a. Name"), Description("Name of the object." + vbCrLf + "Recomended to be unique for easy identification.")> _
    Public Property Name() As String
        Get
            Return Me.metaData.Name
        End Get
        Set(ByVal value As String)
            If value = "" Then
                value = "No name"
            End If
            Me.metaData.Name = value
        End Set
    End Property
    <Category("1. Meta"), DisplayName("b. Description"), Description("Description of the object.")> _
    Public Property Description() As String
        Get
            Return Me.metaData.Description
        End Get
        Set(ByVal value As String)
            Me.metaData.Description = value
        End Set
    End Property
#End Region

#Region "2. Appearance Properties"
    <Category("2. Appearance"), DisplayName("a. Line Color"), Description("The color of the curve." + vbCrLf + "Choose from palettes, enter color name or rgb color value in format ""red;green;blue"" (exp. 255;0;0 or Red)")> _
    Public Property LineColor() As Color
        Get
            Return Me.lineAppearance.LineColor
        End Get
        Set(ByVal value As Color)
            Me.lineAppearance.LineColor = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("b. Line Width"), Description("Line width on screen in pixels")> _
    Public Property LineWidth() As Single
        Get
            Return Me.lineAppearance.LineWidth
        End Get
        Set(ByVal value As Single)
            Me.lineAppearance.LineWidth = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("c. Transparency"), Description("Alpha value of line color between 0 transparent and 255 no transparent)")> _
    Public Property Transparency() As Byte
        Get
            Return Me.lineAppearance.LineTransparency
        End Get
        Set(ByVal value As Byte)
            Me.lineAppearance.LineTransparency = value
            Me.alphaLevel = value
        End Set
    End Property

#End Region

    Dim coloringStress As Boolean = False

#Region "Uncategorised properties"
    <Browsable(False)> _
    Property geom As New cGeometry() Implements IFun3DObject.geom
    <Browsable(False)> _
    Public Property tgeom As New cGeometry() Implements IFun3DObject.tgeom
    <Browsable(False)> _
    Public Property parent As Object = Nothing Implements IFun3DObject.parent
    <DisplayName("Output Level Verbosity"), Description("Verbosity level of error messages output in console." + vbCrLf + "Value is between 0 (less) and 3 (more).")> _
    Public Property VerbosityOutputLevel As Byte = 0
#End Region

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
    Public Property Style() As VisualStyles
        Get
            Return Me.selectedStyle
        End Get
        Set(ByVal value As VisualStyles)
            Me.selectedStyle = value
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
        Me.refreshBuffer(device)
    End Sub
    Public Sub New(ByVal UV As ClassUV, ByVal device As Device)
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
            .LineColor = UV.LineColor
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
            .Parameters = UV.Parameters
        End With
        Me.refreshBuffer(device)
    End Sub
    Public Sub New(ByVal name As String, ByVal device As Device)
        Me.Name = name
        Me.refreshBuffer(device)
    End Sub

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
    <Category("8. U"), DisplayName("U max"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property maksimalnoU() As String
        Get
            Return Str(maxU)
        End Get
        Set(ByVal value As String)
            maxU = mdTools.Evaluate(value, Me.prm.ToArray)
        End Set
    End Property
    <Category("8. U"), DisplayName("U min"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property minimalnoU() As String
        Get
            Return Str(minU)
        End Get
        Set(ByVal value As String)
            minU = mdTools.Evaluate(value, Me.prm.ToArray)
        End Set
    End Property
    <Category("9. V"), DisplayName("V max"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property maksimalnoV() As String
        Get
            Return Str(maxV)
        End Get
        Set(ByVal value As String)
            maxV = mdTools.Evaluate(value, Me.prm.ToArray)
        End Set
    End Property
    <Category("9. V"), DisplayName("V min"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property minimalnoV() As String
        Get
            Return Str(minV)
        End Get
        Set(ByVal value As String)
            minV = mdTools.Evaluate(value, Me.prm.ToArray)
        End Set
    End Property
    <Category("3. Functions"), DisplayName("X(u,v)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property XF() As String
        Get
            Return Me.funX
        End Get
        Set(ByVal value As String)
            Me.funX = value
        End Set
    End Property
    <Category("3. Functions"), DisplayName("Y(u,v)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property YF() As String
        Get
            Return Me.funY
        End Get
        Set(ByVal value As String)
            Me.funY = value
        End Set
    End Property
    <Category("3. Functions"), DisplayName("Z(u,v)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property ZF() As String
        Get
            Return Me.funZ
        End Get
        Set(ByVal value As String)
            Me.funZ = value
        End Set
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
    <Category("4. Parameters"), DisplayName("Parameters"), Editor(GetType(cParametersPropertyEditor), GetType(UITypeEditor))> _
    Public Property Parameters() As List(Of ClassParametri)
        Get
            Return Me.prm
        End Get
        Set(ByVal value As List(Of ClassParametri))
            prm = value
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

#Region "5. Transforms Properties"
    <Category("5. Transforms"), Editor(GetType(cTransformPropertyEditor), GetType(UITypeEditor)), Description("Affine transformations of the line (rotation, position, scale)" + vbCrLf + "Click on button to open transform tool" + vbCrLf + "For mirror transform use negative scale number")> _
    Public Property transform As New cTransform() Implements IFun3DObject.transform
#End Region

    Public Sub refreshBuffer(Optional ByVal device As Device = Nothing)
        If Me.UGustina * Me.VGustina > 32 * 32 Then
            RaiseEvent progressStart()
        End If

        ' transformation matrix
        Dim m, mm As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Translate(Me.xPolozaj, Me.yPolozaj, Me.zPolozaj)
        m = Matrix.Multiply(mm, m)
        mm.RotateX(CSng(Me.xRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.RotateY(CSng(Me.yRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.RotateZ(CSng(Me.zRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.Scale(Me.scX, Me.scY, Me.scZ)
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


        Dim a, b, c, d As Integer

        Dim vertice As CustomVertex.PositionNormalTextured
        Dim svertice As CustomVertex.PositionNormalColored

        Dim plen, pi, p As Integer
        plen = (Me.Udens + 1) * (Me.Vdens + 1)
        pi = 0

        ' -----------------------------------------------------------------------
        Dim cd As String = ""
        Dim vba As New Microsoft.VisualBasic.VBCodeProvider()
        Dim cp As New CodeDom.Compiler.CompilerParameters()
        Dim prmt As ClassParametri

        cd += "Imports Microsoft.DirectX" + vbCrLf
        cd += "Imports system.collections.generic" + vbCrLf
        cd += "Imports System.Math" + vbCrLf
        cd += "Namespace FlyAss" + vbCrLf
        cd += "Class Evaluator" + vbCrLf
        cd += "Public Function Evaluate() As List(of Vector3)" + vbCrLf
        cd += "Dim rv as New List(of Vector3)" + vbCrLf
        cd += "Dim param as New List(of Single)" + vbCrLf
        For Each prmt In Me.prm
            cd += "Dim " + prmt.Name + " as Single = " + Str(prmt.value) + vbCrLf
            cd += "param.add(" + Str(prmt.value) + ")" + vbCrLf
        Next
        cd += "Dim u, v as Single" + vbCrLf
        cd += "Dim uStep as Single = (" + Str(Me.maxU) + "-" + Str(Me.minU) + ")/" + Str(Me.UGustina) + "" + vbCrLf
        cd += "Dim vStep as Single = (" + Str(Me.maxV) + "-" + Str(Me.minV) + ")/" + Str(Me.VGustina) + "" + vbCrLf
        cd += "For u=" + Str(Me.minU) + " To " + Str(Me.maxU) + "+uStep/2 Step uStep" + vbCrLf
        cd += "For v=" + Str(Me.minV) + " To " + Str(Me.maxV) + "+vStep/2 Step vStep" + vbCrLf
        cd += "rv.add(New Vector3(" + Me.funX + "," + Me.funY + "," + Me.funZ + "))" + vbCrLf
        cd += "Next" + vbCrLf
        cd += "Next" + vbCrLf
        cd += "Return rv" + vbCrLf
        cd += "End Function" + vbCrLf
        cd += "End Class" + vbCrLf
        cd += "End Namespace" + vbCrLf
        ' Setup the Compiler Parameters  
        ' Add any referenced assemblies
        cp.ReferencedAssemblies.Add("system.dll")
        cp.ReferencedAssemblies.Add("system.xml.dll")
        cp.ReferencedAssemblies.Add("system.data.dll")
        cp.ReferencedAssemblies.Add("microsoft.directx.dll")
        cp.CompilerOptions = "/t:library"
        cp.GenerateInMemory = True
        Dim cr As CodeDom.Compiler.CompilerResults = vba.CompileAssemblyFromSource(cp, cd)

        If Not cr.Errors.Count <> 0 Then
            Dim exeins As Object = cr.CompiledAssembly.CreateInstance("FlyAss.Evaluator")
            Dim mi As Reflection.MethodInfo = exeins.GetType().GetMethod("Evaluate")

            ' Geometry data
            Dim vbf As New List(Of Vector3)     ' vertex  buffer
            Dim ebf As New List(Of Int32)       ' edges   buffer
            Dim ibf As New List(Of Int32)       ' indices buffer

            vbf = CType(mi.Invoke(exeins, Nothing), List(Of Vector3))

            Dim u, v As Integer
            Dim ib As New List(Of Integer)
            Dim nb As New List(Of Vector3)
            Dim i As Integer
            For i = 0 To vbf.Count - 1
                vertice = New CustomVertex.PositionNormalTextured
                svertice = New CustomVertex.PositionNormalColored
                vertice.Position = Vector3.TransformCoordinate(vbf(i), m)
                svertice.Position = Vector3.TransformCoordinate(vbf(i), m)
                Dim clr As Color = mdTools.valueColor(0, 180, Math.Abs(mdTools.angleBetween2Vectors(New Vector3(0, 0, 1), Vector3.Normalize(svertice.Position))))
                svertice.Color = Color.FromArgb(Me.alphaLevel, clr).ToArgb
                Me.vsBuffer.Add(svertice)
                u = i \ (Me.VGustina + 1)
                v = i Mod (Me.VGustina + 1)
                If Me.fcd Then
                    vertice.Tu = u
                    vertice.Tv = v
                Else
                    vertice.Tu = (u - CSng(Me.minU)) / (CSng(Me.maxU) - CSng(Me.minU))
                    vertice.Tv = (v - CSng(Me.minV)) / (CSng(Me.maxV) - CSng(Me.minV))
                End If
                Me.vBuffer.Add(vertice)
            Next

            Dim vertices1 As CustomVertex.PositionColored()

            vertices1 = New CustomVertex.PositionColored(7) {}
            Dim l As Color
            l = Me.LineColor
            vertices1(0).Color = l.ToArgb
            vertices1(1).Color = l.ToArgb
            vertices1(2).Color = l.ToArgb
            vertices1(3).Color = l.ToArgb
            vertices1(4).Color = l.ToArgb
            vertices1(5).Color = l.ToArgb
            vertices1(6).Color = l.ToArgb
            vertices1(7).Color = l.ToArgb

            pi = 0
            For u = 0 To CInt(Me.UGustina) - 1
                For v = 0 To CInt(Me.VGustina) - 1
                    p = 100 * pi \ plen
                    If Me.UGustina * Me.VGustina > 32 * 32 Then
                        RaiseEvent progress(p, "Generating surface...")
                    End If
                    pi += 1

                    a = u * (Me.VGustina + 1) + v
                    b = u * (Me.VGustina + 1) + v + 1
                    c = (u + 1) * (Me.VGustina + 1) + v + 1
                    d = (u + 1) * (Me.VGustina + 1) + v
                    ib.Add(a)
                    ib.Add(b)
                    ib.Add(c)
                    ib.Add(a)
                    ib.Add(c)
                    ib.Add(d)
                    Dim nv As Vector3
                    nv = Vector3.Cross(vbf(b) - vbf(a), vbf(d) - vbf(a))
                    nv.Normalize()
                    nb.Add(nv)
                    nv = Vector3.Cross(vbf(c) - vbf(b), vbf(a) - vbf(b))
                    nv.Normalize()
                    nb.Add(nv)
                    nv = Vector3.Cross(vbf(d) - vbf(c), vbf(b) - vbf(c))
                    nv.Normalize()
                    nb.Add(nv)
                    nv = Vector3.Cross(vbf(a) - vbf(d), vbf(c) - vbf(d))
                    nv.Normalize()
                    nb.Add(nv)
                    subset.Add(0)
                    subset.Add(0)

                    ' linije

                    vertices1(0).Position = Vector3.TransformCoordinate(vbf(a), m)
                    vertices1(1).Position = Vector3.TransformCoordinate(vbf(b), m)
                    vertices1(2).Position = Vector3.TransformCoordinate(vbf(b), m)
                    vertices1(3).Position = Vector3.TransformCoordinate(vbf(c), m)
                    vertices1(4).Position = Vector3.TransformCoordinate(vbf(c), m)
                    vertices1(5).Position = Vector3.TransformCoordinate(vbf(d), m)
                    vertices1(6).Position = Vector3.TransformCoordinate(vbf(d), m)
                    vertices1(7).Position = Vector3.TransformCoordinate(vbf(a), m)
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
            Me.indices = ib

            Try
                If device IsNot Nothing Then
                    Me.createMesh(device)
                Else
                    Me.createMesh(Me.UVMesh.Device)
                End If
            Catch ex As Exception

            End Try

        Else
            Dim ce As CodeDom.Compiler.CompilerError
            For Each ce In cr.Errors
                Console.WriteLine(ce.ErrorText)
            Next
        End If
        
        If Me.UGustina * Me.VGustina > 32 * 32 Then
            RaiseEvent progressEnd()
        End If
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
            Me.UVMesh = New Mesh(CInt(ind.Length / 3), c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, device)
        Else
            Me.UVMesh = New Mesh(CInt(ind.Length / 3), c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalColored.Format, device)
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
            Console.WriteLine(ex.Message)
            Me.UVMesh = Mesh.Box(device, 10, 10, 10)
        End Try
    End Sub
    Public Sub draw(ByVal device As Device) Implements IFun3DObject.draw

    End Sub
    Public Sub afterPaste(ByVal device As Device)
        Me.vBuffer = New List(Of CustomVertex.PositionNormalTextured)
        Me.vsBuffer = New List(Of CustomVertex.PositionNormalColored)
        Me.lineBuffer = New List(Of CustomVertex.PositionColored)
        refreshBuffer(device)
    End Sub
End Class


