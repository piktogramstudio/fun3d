Imports Eval3
Imports System.Math
Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Public Class ClassISO
    Dim naziv As String = "new1"
    Public xyzVal As New List(Of CustomVertex.PositionColored)
    Public x, y, z As Single
    Dim f As String = "0"
    Dim ee As New Eval3.Evaluator
    Dim xd As Integer = 10
    Dim yd As Integer = 10
    Dim zd As Integer = 10
    Dim maxX As Single = 10
    Dim maxY As Single = 10
    Dim maxZ As Single = 10
    Dim minX As Single = -10
    Dim minY As Single = -10
    Dim minZ As Single = -10
    Public Sub New()
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
    <Category("Definition")> _
    Public Property Fun() As String
        Get
            Return Me.f
        End Get
        Set(ByVal value As String)
            Me.f = value
            Me.refreshBuffer()
        End Set
    End Property
    Public ReadOnly Property vVal() As List(Of CustomVertex.PositionColored)
        Get
            Dim vkt As CustomVertex.PositionColored
            Dim rv As New List(Of CustomVertex.PositionColored)
            Dim xs, ys, zs As Single
            xs = Me.XStep
            ys = Me.YStep
            zs = Me.ZStep
            For z = minZ To maxZ Step xs
                For y = minY To maxY Step ys
                    For x = minX To maxX Step zs
                        Try
                            If Round(ee.Parse(Me.f).value, 0, MidpointRounding.ToEven) = 0 Then
                                vkt = New CustomVertex.PositionColored
                                vkt.Position = New Vector3(x, y, z)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                                vkt.Position = New Vector3(x, y, z + zs)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                                vkt.Position = New Vector3(x + xs, y, z + zs)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)

                                vkt.Position = New Vector3(x, y, z)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                                vkt.Position = New Vector3(x + xs, y, z + zs)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                                vkt.Position = New Vector3(x + xs, y, z)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)

                                vkt.Position = New Vector3(x, y, z)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                                vkt.Position = New Vector3(x, y, z + zs)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                                vkt.Position = New Vector3(x, y + ys, z + zs)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)

                                vkt.Position = New Vector3(x, y, z)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                                vkt.Position = New Vector3(x, y + zs, z + zs)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                                vkt.Position = New Vector3(x, y + ys, z)
                                vkt.Color = Color.Red.ToArgb
                                rv.Add(vkt)
                            End If
                        Catch ex As Exception
                            vkt = New CustomVertex.PositionColored
                            vkt.Position = New Vector3(0, 0, 0)
                            rv.Add(vkt)
                            Return rv
                        End Try
                    Next
                Next
            Next
            Return rv
        End Get
    End Property
    Public Property Xdensity() As Integer
        Get
            Return Me.xd
        End Get
        Set(ByVal value As Integer)
            Me.xd = value
        End Set
    End Property
    Public Property Ydensity() As Integer
        Get
            Return Me.yd
        End Get
        Set(ByVal value As Integer)
            Me.yd = value
        End Set
    End Property
    Public Property Zdensity() As Integer
        Get
            Return Me.zd
        End Get
        Set(ByVal value As Integer)
            Me.zd = value
        End Set
    End Property
    Public Property Xmaksimalno() As Single
        Get
            Return Me.maxX
        End Get
        Set(ByVal value As Single)
            Me.maxX = value
        End Set
    End Property
    Public Property Ymaksimalno() As Single
        Get
            Return Me.maxY
        End Get
        Set(ByVal value As Single)
            Me.maxY = value
        End Set
    End Property
    Public Property Zmaksimalno() As Single
        Get
            Return Me.maxZ
        End Get
        Set(ByVal value As Single)
            Me.maxZ = value
        End Set
    End Property
    Public Property Xminimalno() As Single
        Get
            Return Me.minX
        End Get
        Set(ByVal value As Single)
            Me.minX = value
        End Set
    End Property
    Public Property Yminimalno() As Single
        Get
            Return Me.minY
        End Get
        Set(ByVal value As Single)
            Me.minY = value
        End Set
    End Property
    Public Property Zminimalno() As Single
        Get
            Return Me.minZ
        End Get
        Set(ByVal value As Single)
            Me.minZ = value
        End Set
    End Property
    Private ReadOnly Property XStep() As Single
        Get
            Return (Me.maxX - Me.minX) / Me.xd
        End Get
    End Property
    Private ReadOnly Property YStep() As Single
        Get
            Return (Me.maxY - Me.minY) / Me.yd
        End Get
    End Property
    Private ReadOnly Property ZStep() As Single
        Get
            Return (Me.maxZ - Me.minZ) / Me.zd
        End Get
    End Property
    Private Sub loadFun()
        ee = New Eval3.Evaluator
        ee.AddEnvironmentFunctions(Me)
        ee.AddEnvironmentFunctions(New EvalFunctions)
    End Sub
    Public Sub refreshBuffer()
        Me.xyzVal = Me.vVal
    End Sub
End Class
