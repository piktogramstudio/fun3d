Imports System.Math
Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
<System.Serializable()> _
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
    Public lw As Single = 2
    Public vectorBuffer As New List(Of Vector3)
    <System.NonSerialized()> _
    Public vertexBuffer As New List(Of CustomVertex.PositionColored)
    Public ht As New Hashtable()
    <System.NonSerialized()> _
    Dim ee As New MSScriptControl.ScriptControl
    Dim minSU As Single = -10
    Dim maxSU As Single = 10
    Dim stepSU As Single = 1
    Dim pColor As Color = Color.Red
    Dim minSUm As Single = -10
    Dim maxSUm As Single = 10
    Dim stepSUm As Single = 1
    Dim alphaLevel As Byte = 255
#Region "Events"
    Public Shared Event bufferRefreshed()
    Public Shared Event progressStart()
    Public Shared Event progressEnd()
    Public Shared Event progress(ByVal p As Integer, ByVal m As String)
#End Region
    <Category("4. Parameters")> _
    Public Property np(ByVal key As String) As Single
        Get
            Dim c As ClassParametri
            ht.Clear()
            For Each c In Me.prm
                Me.ht.Add(c.Name, c.value)
            Next
            Return Me.ht(key)
        End Get
        Set(ByVal value As Single)
            Me.ht(key) = value
        End Set
    End Property
    <Category("4. Parameters"), Browsable(False)> _
    Public ReadOnly Property parametriCode()
        Get
            Dim rv As String = ""
            Dim ppp As ClassParametri
            For Each ppp In Me.prm
                rv += "public " + ppp.Name + "=" + ppp.value.ToString + vbCrLf
            Next
            Return rv
        End Get
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
    Public Property p(ByVal nazivParametra As String) As Single
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
    <Category("1. Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Me.naziv = value
        End Set
    End Property
    <Category("7. U")> _
    Public Property Udens() As Integer
        Get
            Return Me.UGustina
        End Get
        Set(ByVal value As Integer)
            Me.UGustina = value
        End Set
    End Property

    <Category("7. U"), DisplayName("U max")> _
    Public Property maksimalnoU() As Single
        Get
            Return maxU
        End Get
        Set(ByVal value As Single)
            maxU = value
        End Set
    End Property
    <Category("7. U"), DisplayName("U min")> _
    Public Property minimalnoU() As Single
        Get
            Return minU
        End Get
        Set(ByVal value As Single)
            minU = value
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property X() As List(Of Single)
        Get
            RaiseEvent progressStart()
            Dim rv As New List(Of Single)
            Dim us As Single
            Dim rfun As String = ""
            us = Me.stepU
            Dim plen, pi, p As Integer
            plen = Me.UGustina + 1
            pi = 0
            For u = Me.minU To Me.maxU Step us
                p = 100 * pi / plen
                RaiseEvent progress(p, "Calculating X...")
                pi += 1
                rfun = Me.funX
                Try
                    Dim i As Integer
                    For i = 0 To Me.dynprm.Count - 1
                        Me.dynprm(i).value = ee.Eval(Me.dynprm(i).funkcija)
                    Next
                    rv.Add(ee.Eval(rfun) * Me.scX - Me.xpol)
                Catch ex As Exception

                    rv.Add(0)
                    Return rv
                End Try
            Next
            RaiseEvent progressEnd()
            Return rv
        End Get
    End Property
    <Browsable(False)> _
    Public ReadOnly Property Y() As List(Of Single)
        Get
            RaiseEvent progressStart()
            Dim rv As New List(Of Single)
            Dim us As Single
            Dim rfun As String = ""
            us = Me.stepU
            Dim plen, pi, p As Integer
            plen = Me.UGustina + 1
            pi = 0
            For u = Me.minU To Me.maxU Step us
                p = 100 * pi / plen
                RaiseEvent progress(p, "Calculating Y...")
                pi += 1
                rfun = Me.funY
                Try
                    Dim i As Integer
                    For i = 0 To Me.dynprm.Count - 1
                        Me.dynprm(i).value = ee.Eval(Me.dynprm(i).funkcija)
                    Next
                    rv.Add(ee.Eval(rfun) * Me.scY + Me.ypol)
                Catch ex As Exception

                    rv.Add(0)
                    Return rv
                End Try
            Next
            RaiseEvent progressEnd()
            Return rv
        End Get
    End Property
    <Browsable(False)> _
    Public ReadOnly Property Z() As List(Of Single)
        Get
            RaiseEvent progressStart()
            Dim rv As New List(Of Single)
            Dim us As Single
            Dim rfun As String = ""
            us = Me.stepU
            Dim plen, pi, p As Integer
            plen = Me.UGustina + 1
            pi = 0
            For u = Me.minU To Me.maxU Step us
                p = 100 * pi / plen
                RaiseEvent progress(p, "Calculating Z...")
                pi += 1
                rfun = Me.funZ
                Try
                    Dim i As Integer
                    For i = 0 To Me.dynprm.Count - 1
                        Me.dynprm(i).value = ee.Eval(Me.dynprm(i).funkcija)
                    Next
                    rv.Add(ee.Eval(rfun) * Me.scZ + Me.zpol)
                Catch ex As Exception

                    rv.Add(0)
                    Return rv
                End Try
            Next
            RaiseEvent progressEnd()
            Return rv
        End Get
    End Property
    <Category("3. Functions"), DisplayName("X(u)")> _
    Public Property XF() As String
        Get
            Return Me.funX
        End Get
        Set(ByVal value As String)
            Me.funX = value
        End Set
    End Property
    <Category("3. Functions"), DisplayName("Y(u)")> _
    Public Property YF() As String
        Get
            Return Me.funY
        End Get
        Set(ByVal value As String)
            Me.funY = value
        End Set
    End Property
    <Category("3. Functions"), DisplayName("Z(u)")> _
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

    <Category("6. Scale")> _
    Public Property scaleX() As Single
        Get
            Return Me.scX
        End Get
        Set(ByVal value As Single)
            Me.scX = value
        End Set
    End Property
    <Category("6. Scale")> _
    Public Property scaleY() As Single
        Get
            Return Me.scY
        End Get
        Set(ByVal value As Single)
            Me.scY = value
        End Set
    End Property
    <Category("6. Scale")> _
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

    <Category("2. Appearance"), DisplayName("Point Color")> _
    Public Property bojaTacke() As Color
        Get
            Return Me.pColor
        End Get
        Set(ByVal value As Color)
            Me.pColor = value
        End Set
    End Property

    <Category("2. Appearance"), DisplayName("Line Width")> _
    Public Property debljinaLinije() As Single
        Get
            Return Me.lw
        End Get
        Set(ByVal value As Single)
            Me.lw = value
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
    Public Sub refreshBuffer()
        Me.xVal = Me.X
        Me.yVal = Me.Y
        Me.zVal = Me.Z
        Dim maxI As Integer = xVal.Count - 1
        Dim indx As Integer
        Dim clist As New List(Of Vector3)
        Me.vertexBuffer.Clear()
        For indx = 0 To maxI
            clist.Add(New Vector3(Me.xVal(indx), Me.yVal(indx), Me.zVal(indx)))
        Next
        Me.vertexBuffer.Add(New CustomVertex.PositionColored(New Vector3(Me.xVal(0), Me.yVal(0), Me.zVal(0)), Me.pColor.ToArgb))
        Me.vertexBuffer.Add(New CustomVertex.PositionColored(New Vector3(Me.xVal(indx - 1), Me.yVal(indx - 1), Me.zVal(indx - 1)), Me.pColor.ToArgb))
        Me.vectorBuffer = clist
    End Sub
    Private Sub loadFun()
        ee = New MSScriptControl.ScriptControl
        ee.Language = "vbscript"
        ee.AllowUI = True

        'ee.AddCode(Me.parametriCode)

        ee.AddObject("mathis", New EvalFunctions, True)
        ee.AddObject("uv", Me, True)
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        Me.vertexBuffer = New List(Of CustomVertex.PositionColored)
        loadFun()
        refreshBuffer()
    End Sub
End Class
