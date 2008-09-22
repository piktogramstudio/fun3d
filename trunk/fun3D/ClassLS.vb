Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
Public Class ClassLS

    Dim ime As String = "New LS"
    Dim start As String = "F"
    Dim pravilo As New List(Of ClassLS.RulePar)
    Dim xUgao As Single = 60
    Dim yUgao As Single = 60
    Dim zUgao As Single = 45
    Dim xMov As Single = 2
    Dim yMov As Single = 2
    Dim zMov As Single = 2
    Dim lsp As Single = 2
    Dim w As Single = 2
    Dim h As Single = 0.5
    Dim l As Single = 0.5
    Dim iter As Integer = 3
    Dim stanja As New List(Of String)
    Dim sve As Boolean = False
    Dim boja As Color = Color.White
    Dim oblik1 As ClassLS.oblik = oblik.cube
    Public LineBuffer As New List(Of CustomVertex.PositionColored)
    Public bufferT As New List(Of CustomVertex.PositionNormalTextured)
    Public DXFBuffer As New List(Of CustomVertex.PositionColored)
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
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Definition")> _
    Public Property Rules() As List(Of ClassLS.RulePar)
        Get
            Return pravilo
        End Get
        Set(ByVal value As List(Of ClassLS.RulePar))
            pravilo = value
            Me.createStates()
            Me.RefreshBuffer()
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
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Transformation")> _
        Public Property AngleY() As Single
        Get
            Return yUgao
        End Get
        Set(ByVal value As Single)
            yUgao = value
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Transformation")> _
        Public Property AngleZ() As Single
        Get
            Return zUgao
        End Get
        Set(ByVal value As Single)
            zUgao = value
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Transformation")> _
        Public Property TranslateX() As Single
        Get
            Return xMov
        End Get
        Set(ByVal value As Single)
            xMov = value
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Transformation")> _
        Public Property TranslateY() As Single
        Get
            Return yMov
        End Get
        Set(ByVal value As Single)
            yMov = value
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Transformation")> _
        Public Property TranslateZ() As Single
        Get
            Return zMov
        End Get
        Set(ByVal value As Single)
            zMov = value
            Me.RefreshBuffer()
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
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Geometry")> _
    Public Property width() As Single
        Get
            Return w
        End Get
        Set(ByVal value As Single)
            w = value
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Geometry")> _
    Public Property height() As Single
        Get
            Return h
        End Get
        Set(ByVal value As Single)
            h = value
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Geometry")> _
    Public Property lenght() As Single
        Get
            Return l
        End Get
        Set(ByVal value As Single)
            l = value
            Me.RefreshBuffer()
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
            Me.RefreshBuffer()
        End Set
    End Property
    <Category("Appearance")> _
   Public Property Shape() As oblik
        Get
            Return oblik1
        End Get
        Set(ByVal value As oblik)
            oblik1 = value
            Me.RefreshBuffer()
        End Set
    End Property
    ' metode
    Public Sub New()
        pravilo.Clear()
        pravilo.Add(New ClassLS.RulePar("F", "F>x+zF>x-z-zF"))
        Me.createStates()
        Me.RefreshBuffer()
    End Sub
    Public Sub New(ByVal nm As String)
        Me.ime = nm
        pravilo.Clear()
        pravilo.Add(New ClassLS.RulePar("F", "F>x+zF>x-z-zF"))
        Me.createStates()
        Me.RefreshBuffer()
    End Sub
    Public Sub createStates()
        Dim cst As ClassLS.RulePar
        Dim rst As String
        Dim i As Integer
        Me.stanja.Clear()
        Me.stanja.Add(Me.start)
        For i = 0 To Me.iter - 1
            rst = Me.stanja(i)
            For Each cst In Me.pravilo
                rst = rst.Replace(cst.ime, cst.vrednost.ToLower)
            Next
            Dim str As String = rst.ToUpper
            Me.stanja.Add(str)
        Next
        For i = 0 To Me.stanja.Count - 1
            Dim str As String = Me.stanja(i)
            str = str.Replace("+X", "x")
            str = str.Replace("-X", "X")
            str = str.Replace(">X", "u")
            str = str.Replace("<X", "U")
            str = str.Replace("+Y", "y")
            str = str.Replace("-Y", "Y")
            str = str.Replace(">Y", "v")
            str = str.Replace("<Y", "V")
            str = str.Replace("+Z", "z")
            str = str.Replace("-Z", "Z")
            str = str.Replace(">Z", "w")
            str = str.Replace("<Z", "W")
            Me.stanja(i) = str
        Next
    End Sub

    Public Sub RefreshBuffer()
        Me.LineBuffer.Clear()
        Me.bufferT.Clear()
        Me.DXFBuffer.Clear()
        Dim str As String = Me.States(Me.States.Count - 1)
        Dim savedm As New List(Of Matrix)
        Dim m As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        Dim konst As Boolean = False
        Dim slovo As Char
        For Each slovo In str
            Select Case slovo
                Case "O", "P", "Q", "R", "S", "T"
                    konst = True
                Case "x"
                    m = Matrix.Multiply(Matrix.RotationX(Me.AngleX * Math.PI / 180), m)
                    konst = True
                Case "y"
                    m = Matrix.Multiply(Matrix.RotationY(Me.AngleY * Math.PI / 180), m)
                    konst = True
                Case "z"
                    m = Matrix.Multiply(Matrix.RotationZ(Me.AngleZ * Math.PI / 180), m)
                    konst = True
                Case "u"
                    m = Matrix.Multiply(Matrix.Translation(Me.TranslateX, 0, 0), m)
                    konst = True
                Case "v"
                    m = Matrix.Multiply(Matrix.Translation(0, Me.TranslateY, 0), m)
                    konst = True
                Case "w"
                    m = Matrix.Multiply(Matrix.Translation(0, 0, Me.TranslateZ), m)
                    konst = True
                Case "X"
                    m = Matrix.Multiply(Matrix.RotationX(-Me.AngleX * Math.PI / 180), m)
                    konst = True
                Case "Y"
                    m = Matrix.Multiply(Matrix.RotationY(-Me.AngleY * Math.PI / 180), m)
                    konst = True
                Case "Z"
                    m = Matrix.Multiply(Matrix.RotationZ(-Me.AngleZ * Math.PI / 180), m)
                    konst = True
                Case "U"
                    m = Matrix.Multiply(Matrix.Translation(-Me.TranslateX, 0, 0), m)
                    konst = True
                Case "V"
                    m = Matrix.Multiply(Matrix.Translation(0, -Me.TranslateY, 0), m)
                    konst = True
                Case "W"
                    m = Matrix.Multiply(Matrix.Translation(0, 0, -Me.TranslateZ), m)
                    konst = True
                Case "["
                    savedm.Add(m)
                    konst = True
                Case "]"
                    m = savedm(savedm.Count - 1)
                    savedm.RemoveAt(savedm.Count - 1)
                    konst = True
            End Select
            If Not konst Then
                ' cube buffer
                Dim c1, c2, c3, c4, c5, c6, c7, c8 As CustomVertex.PositionNormalTextured
                c1.Position = New Vector3(0, 0, 0)
                c2.Position = New Vector3(c1.X + w, c1.Y, c1.Z)
                c3.Position = New Vector3(c1.X + w, c1.Y + l, c1.Z)
                c4.Position = New Vector3(c1.X, c1.Y + l, c1.Z)
                c5.Position = New Vector3(c1.X, c1.Y, c1.Z + h)
                c6.Position = New Vector3(c1.X + w, c1.Y, c1.Z + h)
                c7.Position = New Vector3(c1.X + w, c1.Y + l, c1.Z + h)
                c8.Position = New Vector3(c1.X, c1.Y + l, c1.Z + h)

                c1.Position = Vector3.TransformCoordinate(c1.Position, m)
                c2.Position = Vector3.TransformCoordinate(c2.Position, m)
                c3.Position = Vector3.TransformCoordinate(c3.Position, m)
                c4.Position = Vector3.TransformCoordinate(c4.Position, m)
                c5.Position = Vector3.TransformCoordinate(c5.Position, m)
                c6.Position = Vector3.TransformCoordinate(c6.Position, m)
                c7.Position = Vector3.TransformCoordinate(c7.Position, m)
                c8.Position = Vector3.TransformCoordinate(c8.Position, m)

                ' dxf buffer
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.Colour.ToArgb))
                Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.Colour.ToArgb))

                'f1
                ' uv
                c1.Tu = 0
                c1.Tv = 0
                c2.Tu = 0
                c2.Tv = 1
                c3.Tu = 1
                c3.Tv = 1
                c4.Tu = 1
                c4.Tv = 0
                Me.bufferT.Add(c3)
                Me.bufferT.Add(c2)
                Me.bufferT.Add(c1)

                Me.bufferT.Add(c4)
                Me.bufferT.Add(c3)
                Me.bufferT.Add(c1)

                'f2
                ' uv
                c1.Tu = 0
                c1.Tv = 0
                c2.Tu = 0
                c2.Tv = 1
                c6.Tu = 1
                c6.Tv = 1
                c5.Tu = 1
                c5.Tv = 0
                Me.bufferT.Add(c1)
                Me.bufferT.Add(c2)
                Me.bufferT.Add(c6)

                Me.bufferT.Add(c1)
                Me.bufferT.Add(c6)
                Me.bufferT.Add(c5)

                'f3
                ' uv
                c1.Tu = 0
                c1.Tv = 0
                c8.Tu = 1
                c8.Tv = 1
                c4.Tu = 1
                c4.Tv = 0
                c5.Tu = 0
                c5.Tv = 1
                Me.bufferT.Add(c1)
                Me.bufferT.Add(c8)
                Me.bufferT.Add(c4)

                Me.bufferT.Add(c1)
                Me.bufferT.Add(c5)
                Me.bufferT.Add(c8)

                'f4
                ' uv
                c7.Tu = 1
                c7.Tv = 0
                c3.Tu = 0
                c3.Tv = 0
                c2.Tu = 0
                c2.Tv = 1
                c6.Tu = 0
                c6.Tv = 0
                Me.bufferT.Add(c2)
                Me.bufferT.Add(c3)
                Me.bufferT.Add(c7)

                Me.bufferT.Add(c6)
                Me.bufferT.Add(c2)
                Me.bufferT.Add(c7)

                'f5
                ' uv
                c7.Tu = 0
                c7.Tv = 0
                c8.Tu = 0
                c8.Tv = 1
                c5.Tu = 1
                c5.Tv = 1
                c6.Tu = 1
                c6.Tv = 0
                Me.bufferT.Add(c7)
                Me.bufferT.Add(c8)
                Me.bufferT.Add(c5)

                Me.bufferT.Add(c7)
                Me.bufferT.Add(c5)
                Me.bufferT.Add(c6)

                'f6
                ' uv
                c7.Tu = 0
                c7.Tv = 0
                c8.Tu = 0
                c8.Tv = 1
                c4.Tu = 1
                c4.Tv = 1
                c3.Tu = 1
                c3.Tv = 0
                Me.bufferT.Add(c4)
                Me.bufferT.Add(c8)
                Me.bufferT.Add(c7)

                Me.bufferT.Add(c3)
                Me.bufferT.Add(c4)
                Me.bufferT.Add(c7)

                ' line buffer
                Dim lines(1) As CustomVertex.PositionColored
                Dim v1 As New Vector3(0, 0, 0)
                Dim v2 As New Vector3(Me.width, 0, 0)
                v1 = Vector3.TransformCoordinate(v1, m)
                v2 = Vector3.TransformCoordinate(v2, m)
                lines(0) = New CustomVertex.PositionColored(v1, Me.Colour.ToArgb)
                lines(1) = New CustomVertex.PositionColored(v2, Me.Colour.ToArgb)
                Me.LineBuffer.Add(lines(0))
                Me.LineBuffer.Add(lines(1))
            End If
            konst = False
        Next
        savedm.Clear()
    End Sub
    Public Class RulePar
        Public ime As String
        Public vrednost As String
        Public Property Name() As String
            Get
                Return ime
            End Get
            Set(ByVal value As String)
                ime = value
            End Set
        End Property
        Public Property Value() As String
            Get
                Return vrednost
            End Get
            Set(ByVal value As String)
                vrednost = value
            End Set
        End Property
        Public Sub New()
            ime = "F"
        End Sub
        Public Sub New(ByVal name As String, ByVal value As String)
            ime = name
            vrednost = value
        End Sub
    End Class
    Enum oblik As Byte
        cube = 0
        sphere = 1
        torus = 2
        teapot = 3
        line = 4
    End Enum
End Class
