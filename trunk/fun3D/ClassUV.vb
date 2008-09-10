Imports Eval3
Imports System.Math
Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
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
    Private fc1 As Color = Color.Blue
    Private fc2 As Color = Color.White
    Private xpol As Single = 0
    Private ypol As Single = 0
    Private zpol As Single = 0
    Dim xrot As Single = 0
    Dim yrot As Single = 0
    Dim zrot As Single = 0
    Private naziv As String = "new1"
    Public xVal As List(Of Single)
    Public yVal As List(Of Single)
    Public zVal As List(Of Single)
    Public a As Single = 0
    Public u As Single = 0
    Public v As Single = 0
    Public prm As New List(Of ClassParametri)
    Public dynprm As New List(Of ClassDynamicParametri)
    Public vBuffer As New List(Of CustomVertex.PositionNormalTextured)
    Public lineBuffer As New List(Of CustomVertex.PositionColored)
    Public subset As New List(Of Integer)
    Public selectedStyle As ClassCA.VisualStyles = ClassCA.VisualStyles.defaultStyle
    Dim ee As New Eval3.Evaluator
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
    Public Event bufferRefreshed()
    <Category("Appearance")> _
    Public Property Style() As ClassCA.VisualStyles
        Get
            Return Me.selectedStyle
        End Get
        Set(ByVal value As ClassCA.VisualStyles)
            Me.selectedStyle = value
        End Set
    End Property
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
    <Category("V")> _
    Public Property sliderMinimumVmax() As Single
        Get
            Return Me.minSVm
        End Get
        Set(ByVal value As Single)
            Me.minSVm = value
        End Set
    End Property
    <Category("V")> _
    Public Property sliderMaximumVmax() As Single
        Get
            Return Me.maxSVm
        End Get
        Set(ByVal value As Single)
            Me.maxSVm = value
        End Set
    End Property
    <Category("V")> _
    Public Property sliderStepVmax() As Single
        Get
            Return Me.stepSVm
        End Get
        Set(ByVal value As Single)
            Me.stepSVm = value
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
    <Category("V")> _
    Public Property sliderMinimumVmin() As Single
        Get
            Return Me.minSV
        End Get
        Set(ByVal value As Single)
            Me.minSV = value
        End Set
    End Property
    <Category("V")> _
    Public Property sliderMaximumVmin() As Single
        Get
            Return Me.maxSV
        End Get
        Set(ByVal value As Single)
            Me.maxSV = value
        End Set
    End Property
    <Category("V")> _
    Public Property sliderStepVmin() As Single
        Get
            Return Me.stepSV
        End Get
        Set(ByVal value As Single)
            Me.stepSV = value
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
    <Category("V")> _
    Public Property Vdens() As Integer
        Get
            Return Me.VGustina
        End Get
        Set(ByVal value As Integer)
            Me.VGustina = value
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
    <Category("V")> _
    Public Property maksimalnoV() As Single
        Get
            Return maxV
        End Get
        Set(ByVal value As Single)
            maxV = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("V")> _
    Public Property minimalnoV() As Single
        Get
            Return minV
        End Get
        Set(ByVal value As Single)
            minV = value
            Me.refreshBuffer()
        End Set
    End Property
    <Browsable(False)> _
    Public ReadOnly Property X() As List(Of Single)
        Get
            Dim rv As New List(Of Single)
            Dim us, vs As Single
            Dim rfun As String = ""
            us = Me.stepU
            vs = Me.stepV
            For u = Me.minU To Me.maxU Step us
                For v = Me.minV To Me.maxV Step vs
                    rfun = Me.funX
                    Try
                        Dim i As Integer
                        For i = 0 To Me.dynprm.Count - 1
                            Me.dynprm(i).value = ee.Parse(Me.dynprm(i).funkcija).value
                        Next
                        rv.Add(ee.Parse(rfun).value * Me.scX)
                    Catch ex As Exception

                        rv.Add(0)
                        Return rv
                    End Try
                Next
            Next
            Return rv
        End Get
    End Property
    <Browsable(False)> _
    Public ReadOnly Property Y() As List(Of Single)
        Get
            Dim rv As New List(Of Single)
            Dim us, vs As Single
            Dim rfun As String = ""
            us = Me.stepU
            vs = Me.stepV
            For u = Me.minU To Me.maxU Step us
                For v = Me.minV To Me.maxV Step vs
                    rfun = Me.funY
                    Try
                        Dim i As Integer
                        For i = 0 To Me.dynprm.Count - 1
                            Me.dynprm(i).value = ee.Parse(Me.dynprm(i).funkcija).value
                        Next
                        rv.Add(ee.Parse(rfun).value * Me.scY)
                    Catch ex As Exception

                        rv.Add(0)
                        Return rv
                    End Try
                Next
            Next
            Return rv
        End Get
    End Property
    <Browsable(False)> _
    Public ReadOnly Property Z() As List(Of Single)
        Get
            Dim rv As New List(Of Single)
            Dim us, vs As Single
            Dim rfun As String = ""
            us = Me.stepU
            vs = Me.stepV
            For u = Me.minU To Me.maxU Step us
                For v = Me.minV To Me.maxV Step vs
                    rfun = Me.funZ
                    Try
                        Dim i As Integer
                        For i = 0 To Me.dynprm.Count - 1
                            Me.dynprm(i).value = ee.Parse(Me.dynprm(i).funkcija).value
                        Next
                        rv.Add(ee.Parse(rfun).value * Me.scZ)
                    Catch ex As Exception

                        rv.Add(0)
                        Return rv
                    End Try
                Next
            Next
            Return rv
        End Get
    End Property
    <Category("Functions"), DisplayName("X(u,v)")> _
    Public Property XF() As String
        Get
            Return Me.funX
        End Get
        Set(ByVal value As String)
            Me.funX = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Functions"), DisplayName("Y(u,v)")> _
    Public Property YF() As String
        Get
            Return Me.funY
        End Get
        Set(ByVal value As String)
            Me.funY = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Functions"), DisplayName("Z(u,v)")> _
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
    Private ReadOnly Property stepV() As Single
        Get
            Return (Me.maxV - Me.minV) / Me.VGustina
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
    Public Property bojaPolja1() As Color
        Get
            Return Me.fc1
        End Get
        Set(ByVal value As Color)
            Me.fc1 = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property bojaPolja2() As Color
        Get
            Return Me.fc2
        End Get
        Set(ByVal value As Color)
            Me.fc2 = value
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
        End Set
    End Property
    <Category("Position")> _
    Public Property yPolozaj() As Single
        Get
            Return Me.ypol
        End Get
        Set(ByVal value As Single)
            Me.ypol = value
        End Set
    End Property
    <Category("Position")> _
    Public Property zPolozaj() As Single
        Get
            Return Me.zpol
        End Get
        Set(ByVal value As Single)
            Me.zpol = value
        End Set
    End Property
    <Category("Rotation")> _
    Public Property xRotation() As Single
        Get
            Return Me.xrot
        End Get
        Set(ByVal value As Single)
            Me.xrot = value
        End Set
    End Property
    <Category("Rotation")> _
    Public Property yRotation() As Single
        Get
            Return Me.yrot
        End Get
        Set(ByVal value As Single)
            Me.yrot = value
        End Set
    End Property
    <Category("Rotation")> _
    Public Property zRotation() As Single
        Get
            Return Me.zrot
        End Get
        Set(ByVal value As Single)
            Me.zrot = value
        End Set
    End Property
    Public Sub refreshBuffer()
        Me.xVal = Me.X
        Me.yVal = Me.Y
        Me.zVal = Me.Z
        Me.vBuffer.Clear()
        Me.lineBuffer.Clear()
        Me.subset.Clear()
        Dim vertices As CustomVertex.PositionNormalTextured() 'an array of vertices
        Dim vertices1 As CustomVertex.PositionColored()
        Dim m, om, m1 As New Matrix
        vertices = New CustomVertex.PositionNormalTextured(2) {}

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
        Dim a, b, c, d, u, v, sc As Single
        Dim black As Boolean = True
        Dim maxI As Integer = xVal.Count - 1
        sc = 100
        For u = 0 To Me.Udens - 1
            black = Not black
            For v = 0 To Me.Vdens - 1
                If black Then
                    subset.Add(0)
                    subset.Add(0)
                    black = False
                Else
                    subset.Add(1)
                    subset.Add(1)
                    black = True
                End If
                a = u * (Me.Vdens + 1) + v
                b = u * (Me.Vdens + 1) + v + 1
                c = (u + 1) * (Me.Vdens + 1) + v + 1
                d = (u + 1) * (Me.Vdens + 1) + v
                If a > maxI Or b > maxI Or c > maxI Or d > maxI Then Exit For
                ' polja

                'device.SetTexture(0, textureDefault)
                vertices(0).Position = New Vector3(xVal(a), yVal(a), zVal(a))
                vertices(1).Position = New Vector3(xVal(b), yVal(b), zVal(b))
                vertices(2).Position = New Vector3(xVal(c), yVal(c), zVal(c))
                vertices(0).Tu = 0
                vertices(0).Tv = 0
                vertices(1).Tu = 0
                vertices(1).Tv = 1
                vertices(2).Tu = 1
                vertices(2).Tv = 1
                Me.vBuffer.Add(vertices(2))
                Me.vBuffer.Add(vertices(1))
                Me.vBuffer.Add(vertices(0))

                vertices(0).Position = New Vector3(xVal(a), yVal(a), zVal(a))
                vertices(1).Position = New Vector3(xVal(c), yVal(c), zVal(c))
                vertices(2).Position = New Vector3(xVal(d), yVal(d), zVal(d))
                vertices(0).Tu = 0
                vertices(0).Tv = 0
                vertices(1).Tu = 1
                vertices(1).Tv = 1
                vertices(2).Tu = 1
                vertices(2).Tv = 0
                Me.vBuffer.Add(vertices(2))
                Me.vBuffer.Add(vertices(1))
                Me.vBuffer.Add(vertices(0))

                ' linije

                vertices1(0).Position = New Vector3(xVal(a), yVal(a), zVal(a))
                vertices1(1).Position = New Vector3(xVal(b), yVal(b), zVal(b))
                vertices1(2).Position = New Vector3(xVal(b), yVal(b), zVal(b))
                vertices1(3).Position = New Vector3(xVal(c), yVal(c), zVal(c))
                vertices1(4).Position = New Vector3(xVal(c), yVal(c), zVal(c))
                vertices1(5).Position = New Vector3(xVal(d), yVal(d), zVal(d))
                vertices1(6).Position = New Vector3(xVal(d), yVal(d), zVal(d))
                vertices1(7).Position = New Vector3(xVal(a), yVal(a), zVal(a))
                Me.lineBuffer.Add(vertices1(0))
                Me.lineBuffer.Add(vertices1(1))
                Me.lineBuffer.Add(vertices1(2))
                Me.lineBuffer.Add(vertices1(3))
                Me.lineBuffer.Add(vertices1(4))
                Me.lineBuffer.Add(vertices1(5))
                Me.lineBuffer.Add(vertices1(6))
                Me.lineBuffer.Add(vertices1(7))
            Next
        Next
        RaiseEvent bufferRefreshed()
    End Sub
    Private Sub loadFun()
        ee = New Eval3.Evaluator
        ee.AddEnvironmentFunctions(Me)
        ee.AddEnvironmentFunctions(New EvalFunctions)
    End Sub
End Class


