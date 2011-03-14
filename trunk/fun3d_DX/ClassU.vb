Imports System.Math
Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.Drawing.Design

<System.Serializable()> _
Public Class ClassU
    Dim metaData As New sMetaData("U curve")
    Dim lineAppearance As New sLineAppearance(2, Color.Black, 255)

    Private UGustina As Short = 30
    Private maxU As Single = 10
    Private minU As Single = -10

    <Category("3. Functions"), DisplayName("X(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property funX As String = "u"
    <Category("3. Functions"), DisplayName("Y(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property funY As String = "u"
    <Category("3. Functions"), DisplayName("Z(u)"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property funZ As String = "1"

    Public prm As New List(Of ClassParametri)
    Public geom As New cGeometry()
    Public tgeom As New cGeometry()

    <Editor(GetType(cTransformPropertyEditor), GetType(UITypeEditor))> _
    Public Property transform As New cTransform()

    Dim minSU As Single = -10
    Dim maxSU As Single = 10
    Dim stepSU As Single = 1
    Dim minSUm As Single = -10
    Dim maxSUm As Single = 10
    Dim stepSUm As Single = 1
#Region "Events"
    Public Shared Event bufferRefreshed()
    Public Shared Event progressStart()
    Public Shared Event progressEnd()
    Public Shared Event progress(ByVal p As Integer, ByVal m As String)
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
    <Category("1. Meta")> _
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
    <Category("1. Meta")> _
    Public Property Description() As String
        Get
            Return Me.metaData.Description
        End Get
        Set(ByVal value As String)
            Me.metaData.Description = value
        End Set
    End Property
    <Category("7. U")> _
    Public Property Udens() As Short
        Get
            Return Me.UGustina
        End Get
        Set(ByVal value As Short)
            If 0 < value And value < 512 Then
                Me.UGustina = value
            Else
                Console.WriteLine("<b>Value must be between 1 and 511!</b>")
            End If
        End Set
    End Property
    <Category("7. U"), DisplayName("U max"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property maksimalnoU() As String
        Get
            Return CStr(maxU)
        End Get
        Set(ByVal value As String)
            maxU = mdTools.Evaluate(value, Me.parametri.ToArray)
        End Set
    End Property
    <Category("7. U"), DisplayName("U min"), Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))> _
    Public Property minimalnoU() As String
        Get
            Return CStr(minU)
        End Get
        Set(ByVal value As String)
            minU = mdTools.Evaluate(value, Me.parametri.ToArray)
        End Set
    End Property
    <Category("4. Parameters"), DisplayName("Parameters"), Editor(GetType(cParametersPropertyEditor), GetType(UITypeEditor))> _
    Public Property parametri() As List(Of ClassParametri)
        Get
            Return Me.prm
        End Get
        Set(ByVal value As List(Of ClassParametri))
            prm = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Line Color")> _
    Public Property bojaLinija() As Color
        Get
            Return Me.lineAppearance.LineColor
        End Get
        Set(ByVal value As Color)
            Me.lineAppearance.LineColor = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Line Width")> _
    Public Property debljinaLinije() As Single
        Get
            Return Me.lineAppearance.LineWidth
        End Get
        Set(ByVal value As Single)
            Me.lineAppearance.LineWidth = value
        End Set
    End Property
    <Category("2. Appearance")> _
        Public Property Transparency() As Byte
        Get
            Return Me.lineAppearance.LineTransparency
        End Get
        Set(ByVal value As Byte)
            Me.lineAppearance.LineTransparency = value
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
        For Each p In Me.prm
            cd += "Dim " + p.Name + " as Single = " + Str(p.value) + vbCrLf
            cd += "param.add(" + Str(p.value) + ")" + vbCrLf
        Next
        cd += "Dim u as Single" + vbCrLf
        cd += "Dim uStep as Single = (" + Str(Me.maxU) + "-" + Str(Me.minU) + ")/" + Me.UGustina.ToString + "" + vbCrLf
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
