Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
Public Class ClassLS

    Dim ime As String = "New LS"
    Dim start As String = "A"
    Dim pravilo As New List(Of String)
    Dim xUgao As Single = 60
    Dim yUgao As Single = 60
    Dim zUgao As Single = 60
    Dim xMov As Single = 2
    Dim yMov As Single = 2
    Dim zMov As Single = 2
    Dim lsp As Single = 2
    Dim w As Single = 2
    Dim h As Single = 2
    Dim l As Single = 2
    Dim iter As Integer = 3
    Dim stanja As New List(Of String)
    Dim sve As Boolean = False
    Dim boja As Color = Color.White
    Dim oblik1 As ClassLS.oblik = oblik.cube
    ' Osobine
    <Category("Meta")> _
    Public Property Name() As String
        Get
            Return ime
        End Get
        Set(ByVal value As String)
            ime = value
        End Set
    End Property
    <Category("Definition")> _
    Public Property Initial() As String
        Get
            Return start
        End Get
        Set(ByVal value As String)
            start = value
            Me.createStates()
        End Set
    End Property
    <Category("Definition")> _
    Public Property Rules() As List(Of String)
        Get
            Return pravilo
        End Get
        Set(ByVal value As List(Of String))
            pravilo = value
            Me.createStates()
        End Set
    End Property
    <Category("Definition")> _
    Public Property States() As List(Of String)
        Get
            Return stanja
        End Get
        Set(ByVal value As List(Of String))
            stanja = value
        End Set
    End Property
    <Category("Transformation")> _
        Public Property AngleX() As Single
        Get
            Return xUgao
        End Get
        Set(ByVal value As Single)
            xUgao = value
        End Set
    End Property
    <Category("Transformation")> _
        Public Property AngleY() As Single
        Get
            Return yUgao
        End Get
        Set(ByVal value As Single)
            yUgao = value
        End Set
    End Property
    <Category("Transformation")> _
        Public Property AngleZ() As Single
        Get
            Return zUgao
        End Get
        Set(ByVal value As Single)
            zUgao = value
        End Set
    End Property
    <Category("Transformation")> _
        Public Property TranslateX() As Single
        Get
            Return xMov
        End Get
        Set(ByVal value As Single)
            xMov = value
        End Set
    End Property
    <Category("Transformation")> _
        Public Property TranslateY() As Single
        Get
            Return yMov
        End Get
        Set(ByVal value As Single)
            yMov = value
        End Set
    End Property
    <Category("Transformation")> _
        Public Property TranslateZ() As Single
        Get
            Return zMov
        End Get
        Set(ByVal value As Single)
            zMov = value
        End Set
    End Property
    <Category("Transformation")> _
        Public Property levelDistance() As Single
        Get
            Return lsp
        End Get
        Set(ByVal value As Single)
            lsp = value
        End Set
    End Property
    <Category("Definition")> _
        Public Property Iterations() As Integer
        Get
            Return iter
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then
                MsgBox("Number of iterations must have values higher than 0")
                value = 1
            End If
            iter = value
            Me.createStates()
        End Set
    End Property
    <Category("Geometry")> _
    Public Property width() As Single
        Get
            Return w
        End Get
        Set(ByVal value As Single)
            w = value
        End Set
    End Property
    <Category("Geometry")> _
    Public Property height() As Single
        Get
            Return h
        End Get
        Set(ByVal value As Single)
            h = value
        End Set
    End Property
    <Category("Geometry")> _
    Public Property lenght() As Single
        Get
            Return l
        End Get
        Set(ByVal value As Single)
            l = value
        End Set
    End Property
    Public Property showAllIter() As Boolean
        Get
            Return sve
        End Get
        Set(ByVal value As Boolean)
            sve = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property Colour() As Color
        Get
            Return boja
        End Get
        Set(ByVal value As Color)
            boja = value
        End Set
    End Property
    <Category("Appearance")> _
   Public Property Shape() As oblik
        Get
            Return oblik1
        End Get
        Set(ByVal value As oblik)
            oblik1 = value
        End Set
    End Property
    ' metode
    Public Sub New()
        pravilo.Clear()
        pravilo.Add("A=AB")
        pravilo.Add("B=A")
        Me.createStates()
    End Sub
    Public Sub New(ByVal nm As String)
        Me.ime = nm
        pravilo.Clear()
        pravilo.Add("A=AB")
        pravilo.Add("B=A")
        Me.createStates()
    End Sub
    Public Sub createStates()
        Dim cst As String
        Dim nst() As String
        Dim rst As String
        Dim i As Integer
        Me.stanja.Clear()
        Me.stanja.Add(Me.start)
        For i = 0 To Me.iter - 1
            rst = Me.stanja(i)
            For Each cst In Me.pravilo
                nst = cst.Split("=")
                rst = rst.Replace(nst(0), nst(1).ToLower)
            Next
            Me.stanja.Add(rst.ToUpper)
        Next
    End Sub
    Enum oblik As Byte
        cube = 0
        sphere = 1
        torus = 2
        teapot = 3
        line = 4
    End Enum
End Class
