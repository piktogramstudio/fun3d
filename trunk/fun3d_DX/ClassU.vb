Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Drawing.Design

<System.Serializable()> _
Public Class ClassU
    Implements IFun3DObject
    Dim metaData As New sMetaData("U curve")
    Dim lineAppearance As New sLineAppearance(2, Color.Black, 255)

    Public UDensity As Short = 30
    Public maxU As Single = 10
    Public minU As Single = -10
    Public minSU As Single = -10
    Public maxSU As Single = 10
    Public stepSU As Single = 1
    Public minSUm As Single = -10
    Public maxSUm As Single = 10
    Public stepSUm As Single = 1

    Public geom As New cGeometry()

    <Browsable(False)> _
    Public Property tgeom As New cGeometry() Implements IFun3DObject.tgeom

    <DisplayName("a. Name"), Category("1. Meta"), Description("Name of the object." + vbCrLf + "Recomended to be unique for easy identification.")> _
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
    <DisplayName("b. Description"), Category("1. Meta"), Description("Description of the object.")> _
    Public Property Description() As String
        Get
            Return Me.metaData.Description
        End Get
        Set(ByVal value As String)
            Me.metaData.Description = value
        End Set
    End Property

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
    <DisplayName("c. Transparency"), Category("2. Appearance"), Description("Alpha value of line color between 0 transparent and 255 no transparent)")> _
    Public Property Transparency() As Byte
        Get
            Return Me.lineAppearance.LineTransparency
        End Get
        Set(ByVal value As Byte)
            Me.lineAppearance.LineTransparency = value
        End Set
    End Property

    <Category("3. Functions"), DisplayName("X(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("X(u) parametric form function. Click on button to open equation editor")> _
    Public Property funX As String = "u"
    <Category("3. Functions"), DisplayName("Y(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Y(u) parametric form function. Click on button to open equation editor")> _
    Public Property funY As String = "u"
    <Category("3. Functions"), DisplayName("Z(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Z(u) parametric form function. Click on button to open equation editor")> _
    Public Property funZ As String = "1"

    <Category("4. Parameters"), DisplayName("Parameters"), Editor(GetType(cParametersPropertyEditor), GetType(UITypeEditor)), Description("Interactive real time parameters adjustements, To add or remove parameter use equation editor in any equation property (exp. X(u))")> _
    Public Property Parameters() As New List(Of ClassParametri)

    <Category("5. Transforms"), Editor(GetType(cTransformPropertyEditor), GetType(UITypeEditor)), Description("Affine transformations of the line (rotation, position, scale)" + vbCrLf + "Click on button to open transform tool" + vbCrLf + "For mirror transform use negative scale number")> _
    Public Property Transform As New cTransform()


#Region "Events"
    Public Event bufferRefreshed() Implements IFun3DObject.bufferRefreshed
    Public Event progressStart() Implements IFun3DObject.progressStart
    Public Event progressEnd() Implements IFun3DObject.progressEnd
    Public Event progress(ByVal p As Integer, ByVal m As String) Implements IFun3DObject.progress
#End Region

    <Category("7. U"), DisplayName("Minimum Umax Value")> _
    Public Property sliderMinimumUmax() As Single
        Get
            Return Me.minSUm
        End Get
        Set(ByVal value As Single)
            Me.minSUm = value
        End Set
    End Property
    <Category("7. U"), DisplayName("Maximum Umax Value")> _
    Public Property sliderMaximumUmax() As Single
        Get
            Return Me.maxSUm
        End Get
        Set(ByVal value As Single)
            Me.maxSUm = value
        End Set
    End Property
    <Category("7. U"), DisplayName("Step for Umax Value")> _
    Public Property sliderStepUmax() As Single
        Get
            Return Me.stepSUm
        End Get
        Set(ByVal value As Single)
            Me.stepSUm = value
        End Set
    End Property

    <Category("7. U"), DisplayName("Minimum Umin Value")> _
    Public Property sliderMinimumUmin() As Single
        Get
            Return Me.minSU
        End Get
        Set(ByVal value As Single)
            Me.minSU = value
        End Set
    End Property
    <Category("7. U"), DisplayName("Maximum Umin Value")> _
    Public Property sliderMaximumUmin() As Single
        Get
            Return Me.maxSU
        End Get
        Set(ByVal value As Single)
            Me.maxSU = value
        End Set
    End Property
    <Category("7. U"), DisplayName("Step for Umin Value")> _
    Public Property sliderStepUmin() As Single
        Get
            Return Me.stepSU
        End Get
        Set(ByVal value As Single)
            Me.stepSU = value
        End Set
    End Property
    Public Sub New()
        Me.refreshBuffer()
    End Sub
    Public Sub New(ByVal ime As String)
        Me.metaData.Name = ime
        Me.refreshBuffer()
    End Sub
    
    <Category("7. U")> _
    Public Property Udens() As Short
        Get
            Return Me.UDensity
        End Get
        Set(ByVal value As Short)
            If 0 < value And value < 512 Then
                Me.UDensity = value
            Else
                Console.WriteLine("<b>Value must be between 1 and 511!</b>")
            End If
        End Set
    End Property
    <Category("7. U"), DisplayName("U max"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property maksimalnoU() As String
        Get
            Return Str(maxU)
        End Get
        Set(ByVal value As String)
            maxU = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
    <Category("7. U"), DisplayName("U min"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property minimalnoU() As String
        Get
            Return Str(minU)
        End Get
        Set(ByVal value As String)
            minU = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
    
    
    Public Sub refreshBuffer()
        Dim p As ClassParametri
        Dim cd As String = ""
        Dim vba As New Microsoft.VisualBasic.VBCodeProvider()
        Dim cp As New CodeDom.Compiler.CompilerParameters()
        cd += "Imports Microsoft.DirectX" + vbCrLf
        cd += "Imports system.collections.generic" + vbCrLf
        cd += "Imports System.Math" + vbCrLf
        cd += "Namespace FlyAss" + vbCrLf
        cd += "Class Evaluator" + vbCrLf
        cd += "Public Function Evaluate() As List(of Vector3)" + vbCrLf
        cd += "Dim rv as New List(of Vector3)" + vbCrLf
        cd += "Dim param as New List(of Single)" + vbCrLf
        For Each p In Me.Parameters
            cd += "Dim " + p.Name + " as Single = " + Str(p.value) + vbCrLf
            cd += "param.add(" + Str(p.value) + ")" + vbCrLf
        Next
        cd += "Dim u as Single" + vbCrLf
        cd += "Dim uStep as Single = (" + Str(Me.maxU) + "-" + Str(Me.minU) + ")/" + Me.UDensity.ToString + "" + vbCrLf
        cd += "For u=" + Str(Me.minU) + " To " + Str(Me.maxU) + "+uStep/2 Step uStep" + vbCrLf
        cd += "rv.add(New Vector3(" + Me.funX + "," + Me.funY + "," + Me.funZ + "))" + vbCrLf
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

            Me.geom.setPolyline(CType(mi.Invoke(exeins, Nothing), List(Of Vector3)).ToArray(), False)
        Else
            Dim ce As CodeDom.Compiler.CompilerError
            For Each ce In cr.Errors
                'Console.WriteLine(cd)
                Console.WriteLine(ce.ErrorText)
                'Console.WriteLine("Line: " + ce.Line.ToString)
            Next
        End If
        tgeom = Me.transform.getTransformedGeometry(Me.geom)
        cp.TempFiles.Delete()
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        refreshBuffer()
    End Sub
End Class
