Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Drawing.Design

<System.Serializable()> _
Public Class ClassU
    Implements IFun3DObject

#Region "Fields"
    Dim metaData As New sMetaData("U curve")
    Dim lineAppearance As New sLineAppearance(2, Color.Black, 255)

    Public UDensity As Short = 300
    Public maxU As Single = 10
    Public minU As Single = -10
    Public minSU As Single = -10
    Public maxSU As Single = 10
    Public stepSU As Single = 1
    Public minSUm As Single = -10
    Public maxSUm As Single = 10
    Public stepSUm As Single = 1
#End Region

#Region "Properties"
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
        End Set
    End Property
#End Region

#Region "3. Functions Properties"
    <Category("3. Functions"), DisplayName("X(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("X(u) parametric form function. Click on button to open equation editor")> _
    Public Property funX As String = "u"
    <Category("3. Functions"), DisplayName("Y(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Y(u) parametric form function. Click on button to open equation editor")> _
    Public Property funY As String = "u"
    <Category("3. Functions"), DisplayName("Z(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Z(u) parametric form function. Click on button to open equation editor")> _
    Public Property funZ As String = "1"
#End Region

#Region "4. Parameters Properties"
    <Category("4. Parameters"), DisplayName("Parameters"), Editor(GetType(cParametersPropertyEditor), GetType(UITypeEditor)), Description("Interactive real time parameters adjustements, To add or remove parameter use equation editor in any equation property (exp. X(u))")> _
    Public Property Parameters() As New List(Of ClassParametri)
#End Region

#Region "5. Transforms Properties"
    <Category("5. Transforms"), Editor(GetType(cTransformPropertyEditor), GetType(UITypeEditor)), Description("Affine transformations of the line (rotation, position, scale)" + vbCrLf + "Click on button to open transform tool" + vbCrLf + "For mirror transform use negative scale number")> _
    Public Property transform As New cTransform() Implements IFun3DObject.transform
#End Region

#Region "6. Parameter 'u' settings Properties"
    <Category("6. Parameter 'u' settings"), DisplayName("a. Density"), Description("Curve density or 'u' value step. This value is number of line segments along curve and must be between 1 and 511.")> _
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
    <Category("6. Parameter 'u' settings"), DisplayName("b. Minimum 'u'"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Minimum parameter 'u' value")> _
    Public Property Umin() As String
        Get
            Return Str(minU)
        End Get
        Set(ByVal value As String)
            minU = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
    <Category("6. Parameter 'u' settings"), DisplayName("c. Maximum 'u'"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Maximum parameter 'u' value")> _
    Public Property Umax() As String
        Get
            Return Str(maxU)
        End Get
        Set(ByVal value As String)
            maxU = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
#End Region

#Region "7. Parameter 'u' slider settings Properties"
    <Category("7. Parameter 'u' slider settings"), DisplayName("a. Minimum Maximum 'u' Value"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Minimum Maximum 'u' value for slider in value inspector")> _
    Public Property sliderMinimumUmax() As String
        Get
            Return Str(Me.minSUm)
        End Get
        Set(ByVal value As String)
            Me.minSUm = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
    <Category("7. Parameter 'u' slider settings"), DisplayName("b. Maximum Maximum 'u' Value"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Maximum Maximum 'u' value for slider in value inspector")> _
    Public Property sliderMaximumUmax() As String
        Get
            Return Str(Me.maxSUm)
        End Get
        Set(ByVal value As String)
            Me.maxSUm = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
    <Category("7. Parameter 'u' slider settings"), DisplayName("c. Step for Maximum 'u' Value"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Maximum 'u' value step for slider in value inspector")> _
    Public Property sliderStepUmax() As String
        Get
            Return Str(Me.stepSUm)
        End Get
        Set(ByVal value As String)
            Me.stepSUm = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
    <Category("7. Parameter 'u' slider settings"), DisplayName("d. Minimum Minimum 'u' Value"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Minimum Minimum 'u' value for slider in value inspector")> _
    Public Property sliderMinimumUmin() As String
        Get
            Return Str(Me.minSU)
        End Get
        Set(ByVal value As String)
            Me.minSU = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
    <Category("7. Parameter 'u' slider settings"), DisplayName("e. Maximum Minimum 'u' Value"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Maximum Minimum 'u' value for slider in value inspector")> _
    Public Property sliderMaximumUmin() As String
        Get
            Return Str(Me.maxSU)
        End Get
        Set(ByVal value As String)
            Me.maxSU = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
    <Category("7. Parameter 'u' slider settings"), DisplayName("f. Step for Minimum 'u' Value"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor)), Description("Minimum 'u' value step for slider in value inspector")> _
    Public Property sliderStepUmin() As String
        Get
            Return Str(Me.stepSU)
        End Get
        Set(ByVal value As String)
            Me.stepSU = mdTools.Evaluate(value, Me.Parameters.ToArray)
        End Set
    End Property
#End Region
#End Region

#Region "Events"
    Public Event bufferRefreshed() Implements IFun3DObject.bufferRefreshed
    Public Event progressStart() Implements IFun3DObject.progressStart
    Public Event progressEnd() Implements IFun3DObject.progressEnd
    Public Event progress(ByVal p As Integer, ByVal m As String) Implements IFun3DObject.progress
#End Region

#Region "Methods"
    Public Sub New()
        Me.refreshBuffer()
    End Sub
    Public Sub New(ByVal name As String)
        Me.metaData.Name = name
        Me.refreshBuffer()
    End Sub
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
        cd += "Dim u, t as Single" + vbCrLf
        cd += "Dim uStep as Single = (" + Str(Me.maxU) + "-" + Str(Me.minU) + ")/" + Me.UDensity.ToString + "" + vbCrLf
        cd += "For u=" + Str(Me.minU) + " To " + Str(Me.maxU) + "+uStep/2 Step uStep" + vbCrLf
        cd += "t = u" + vbCrLf
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
        cp.GenerateExecutable = False
        cp.IncludeDebugInformation = False
        Dim cr As CodeDom.Compiler.CompilerResults = vba.CompileAssemblyFromSource(cp, cd)
        If Not cr.Errors.Count <> 0 Then
            Dim exeins As Object = cr.CompiledAssembly.CreateInstance("FlyAss.Evaluator")
            Dim mi As Reflection.MethodInfo = exeins.GetType().GetMethod("Evaluate")

            Me.geom.setPolyline(CType(mi.Invoke(exeins, Nothing), List(Of Vector3)).ToArray(), False)
        Else
            Dim ce As CodeDom.Compiler.CompilerError
            For Each ce In cr.Errors
                Select Case VerbosityOutputLevel
                    Case 1
                        Console.WriteLine(ce.ErrorText)
                    Case 2
                        Console.WriteLine(ce.ErrorText)
                        Console.WriteLine("Line: " + ce.Line.ToString)
                    Case 3
                        Console.WriteLine(cd)
                        Console.WriteLine(ce.ErrorText)
                        Console.WriteLine("Line: " + ce.Line.ToString)
                End Select
            Next
        End If
        tgeom = Me.Transform.getTransformedGeometry(Me.geom)
        cp.TempFiles.Delete()
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        refreshBuffer()
    End Sub
#End Region
    ' TODO Save data declaration
End Class
