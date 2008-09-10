Imports Eval3
Imports System.Math
Imports System.ComponentModel
Public Class ClassU
    Private UGustina As Integer = 30
    Private maxU As Single = 10
    Private minU As Single = -10
    Private funX As String = "u"
    Private funY As String = "u"
    Private funZ As String = "1"
    Private scX As Single = 1
    Private scY As Single = 1
    Private scZ As Single = 1
    Private lc As Color = Color.Black
    Private xpol As Single = 0
    Private ypol As Single = 0
    Private zpol As Single = 0
    Private naziv As String = "new1"
    Public xVal As List(Of Single)
    Public yVal As List(Of Single)
    Public zVal As List(Of Single)
    Public a As Single = 0
    Public u As Single = 0
    Public prm As New List(Of ClassParametri)
    Public dynprm As New List(Of ClassDynamicParametri)
    Dim ee As New Eval3.Evaluator
    Dim minSU As Single = -10
    Dim maxSU As Single = 10
    Dim stepSU As Single = 1
    Dim pColor As Color = Color.Red
    Dim minSUm As Single = -10
    Dim maxSUm As Single = 10
    Dim stepSUm As Single = 1
    Dim alphaLevel As Byte = 255
    <Category("Parameters")> _
    Public Property dynapicParameters() As List(Of ClassDynamicParametri)
        Get
            Return Me.dynprm
        End Get
        Set(ByVal value As List(Of ClassDynamicParametri))
            Me.dynprm = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Parameters")> _
    Public Property dParam(ByVal index As Integer) As Single
        Get
            Return Me.dynprm(index).value
        End Get
        Set(ByVal value As Single)
            Me.dynprm(index).value = value
        End Set
    End Property
    <Category("U")> _
    Public Property sliderMinimumUmax() As Single
        Get
            Return Me.minSUm
        End Get
        Set(ByVal value As Single)
            Me.minSUm = value
        End Set
    End Property
    <Category("U")> _
    Public Property sliderMaximumUmax() As Single
        Get
            Return Me.maxSUm
        End Get
        Set(ByVal value As Single)
            Me.maxSUm = value
        End Set
    End Property
    <Category("U")> _
    Public Property sliderStepUmax() As Single
        Get
            Return Me.stepSUm
        End Get
        Set(ByVal value As Single)
            Me.stepSUm = value
        End Set
    End Property
    
    <Category("U")> _
    Public Property sliderMinimumUmin() As Single
        Get
            Return Me.minSU
        End Get
        Set(ByVal value As Single)
            Me.minSU = value
        End Set
    End Property
    <Category("U")> _
    Public Property sliderMaximumUmin() As Single
        Get
            Return Me.maxSU
        End Get
        Set(ByVal value As Single)
            Me.maxSU = value
        End Set
    End Property
    <Category("U")> _
    Public Property sliderStepUmin() As Single
        Get
            Return Me.stepSU
        End Get
        Set(ByVal value As Single)
            Me.stepSU = value
        End Set
    End Property
  
    <Category("Parameters")> _
    Public Property param(ByVal index As Integer) As Single
        Get
            Return prm(index).value
        End Get
        Set(ByVal value As Single)
            prm(index).value = value
        End Set
    End Property
    <Category("Parameters")> _
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
    Public Sub New()
        Me.loadFun()
        Me.refreshBuffer()
    End Sub
    Public Sub New(ByVal ime As String)
        Me.naziv = ime
        Me.loadFun()
        Me.refreshBuffer()
    End Sub
    <Category("Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Me.naziv = value
        End Set
    End Property
    <Category("U")> _
    Public Property Udens() As Integer
        Get
            Return Me.UGustina
        End Get
        Set(ByVal value As Integer)
            Me.UGustina = value
            Me.refreshBuffer()
        End Set
    End Property
    
    <Category("U")> _
    Public Property maksimalnoU() As Single
        Get
            Return maxU
        End Get
        Set(ByVal value As Single)
            maxU = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("U")> _
    Public Property minimalnoU() As Single
        Get
            Return minU
        End Get
        Set(ByVal value As Single)
            minU = value
            Me.refreshBuffer()
        End Set
    End Property
   
    <Browsable(False)> _
    Public ReadOnly Property X() As List(Of Single)
        Get
            Dim rv As New List(Of Single)
            Dim us As Single
            Dim rfun As String = ""
            us = Me.stepU
            For u = Me.minU To Me.maxU Step us
                rfun = Me.funX
                Try
                    Dim i As Integer
                    For i = 0 To Me.dynprm.Count - 1
                        Me.dynprm(i).value = ee.Parse(Me.dynprm(i).funkcija).value
                    Next
                    rv.Add(ee.Parse(rfun).value * Me.scX - Me.xpol)
                Catch ex As Exception

                    rv.Add(0)
                    Return rv
                End Try
            Next
            Return rv
        End Get
    End Property
    <Browsable(False)> _
    Public ReadOnly Property Y() As List(Of Single)
        Get
            Dim rv As New List(Of Single)
            Dim us As Single
            Dim rfun As String = ""
            us = Me.stepU
            For u = Me.minU To Me.maxU Step us
                rfun = Me.funY
                Try
                    Dim i As Integer
                    For i = 0 To Me.dynprm.Count - 1
                        Me.dynprm(i).value = ee.Parse(Me.dynprm(i).funkcija).value
                    Next
                    rv.Add(ee.Parse(rfun).value * Me.scY + Me.ypol)
                Catch ex As Exception

                    rv.Add(0)
                    Return rv
                End Try
            Next
            Return rv
        End Get
    End Property
    <Browsable(False)> _
    Public ReadOnly Property Z() As List(Of Single)
        Get
            Dim rv As New List(Of Single)
            Dim us As Single
            Dim rfun As String = ""
            us = Me.stepU
            For u = Me.minU To Me.maxU Step us
                rfun = Me.funZ
                Try
                    Dim i As Integer
                    For i = 0 To Me.dynprm.Count - 1
                        Me.dynprm(i).value = ee.Parse(Me.dynprm(i).funkcija).value
                    Next
                    rv.Add(ee.Parse(rfun).value * Me.scZ + Me.zpol)
                Catch ex As Exception

                    rv.Add(0)
                    Return rv
                End Try
            Next
            Return rv
        End Get
    End Property
    <Category("Functions"), DisplayName("X(u)")> _
    Public Property XF() As String
        Get
            Return Me.funX
        End Get
        Set(ByVal value As String)
            Me.funX = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Functions"), DisplayName("Y(u)")> _
    Public Property YF() As String
        Get
            Return Me.funY
        End Get
        Set(ByVal value As String)
            Me.funY = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Functions"), DisplayName("Z(u)")> _
    Public Property ZF() As String
        Get
            Return Me.funZ
        End Get
        Set(ByVal value As String)
            Me.funZ = value
            Me.refreshBuffer()
        End Set
    End Property
    Private ReadOnly Property stepU() As Single
        Get
            Return (Me.maxU - Me.minU) / Me.UGustina
        End Get
    End Property
    
    <Category("Scale")> _
    Public Property scaleX() As Single
        Get
            Return Me.scX
        End Get
        Set(ByVal value As Single)
            Me.scX = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Scale")> _
    Public Property scaleY() As Single
        Get
            Return Me.scY
        End Get
        Set(ByVal value As Single)
            Me.scY = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Scale")> _
    Public Property scaleZ() As Single
        Get
            Return Me.scZ
        End Get
        Set(ByVal value As Single)
            Me.scZ = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Parameters")> _
    Public Property parametri() As List(Of ClassParametri)
        Get
            Return Me.prm
        End Get
        Set(ByVal value As List(Of ClassParametri))
            prm = value
            Me.loadFun()
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Appearance")> _
    Public Property bojaLinija() As Color
        Get
            Return Me.lc
        End Get
        Set(ByVal value As Color)
            Me.lc = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property bojaTacke() As Color
        Get
            Return Me.pColor
        End Get
        Set(ByVal value As Color)
            Me.pColor = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property Transparency() As Byte
        Get
            Return Me.alphaLevel
        End Get
        Set(ByVal value As Byte)
            Me.alphaLevel = value
        End Set
    End Property
    <Category("Position")> _
    Public Property xPolozaj() As Single
        Get
            Return Me.xpol
        End Get
        Set(ByVal value As Single)
            Me.xpol = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Position")> _
    Public Property yPolozaj() As Single
        Get
            Return Me.ypol
        End Get
        Set(ByVal value As Single)
            Me.ypol = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Position")> _
    Public Property zPolozaj() As Single
        Get
            Return Me.zpol
        End Get
        Set(ByVal value As Single)
            Me.zpol = value
            Me.refreshBuffer()
        End Set
    End Property
    Public Sub refreshBuffer()
        Me.xVal = Me.X
        Me.yVal = Me.Y
        Me.zVal = Me.Z
    End Sub
    Private Sub loadFun()
        ee = New Eval3.Evaluator
        ee.AddEnvironmentFunctions(Me)
        ee.AddEnvironmentFunctions(New EvalFunctions)
    End Sub
End Class
