Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
''' <summary>Cellular automata object class</summary>
<System.Serializable()> _
Public Class ClassCA
#Region "Non Serialized Fields"
    ''' <summary>Vertex buffer for mesh with cubes</summary>
    <System.NonSerialized()> _
    Public bufferT As New List(Of CustomVertex.PositionNormalTextured)
    ''' <summary>Vertex buffer for mesh with cubes dxf export</summary>
    <System.NonSerialized()> _
    Public DXFBuffer As New List(Of CustomVertex.PositionColored)
    ''' <summary>Vertex buffer for mesh with cubes edges</summary>
    <System.NonSerialized()> _
    Public LineBuffer As New List(Of CustomVertex.PositionColored)
    ''' <summary>Mesh with cubes Direct3D definition</summary>
    <System.NonSerialized()> _
    Public CAMesh As Mesh
    ''' <summary>Vertex buffer for ISO surface mesh</summary>
    <System.NonSerialized()> _
    Dim vBuffer As New List(Of CustomVertex.PositionNormalTextured)
    ''' <summary>ISO surface mesh Direct3D definition</summary>
    <System.NonSerialized()> _
    Public ISOMesh As Mesh = Nothing
    <System.NonSerialized()> _
    Public meshBuffer As New List(Of ClassMesh)
#End Region
#Region "Public Fields"
    Public praviloL As Byte = 54
    Public providnost As Byte = 255
    Public minBC As Byte = 2
    Public maxBC As Byte = 3
    Public toLive As Byte = 3
    Public sf As Byte = 1
    Public xFields As Integer = 15
    Public yFields As Integer = 15
    Public nOfLevels As Integer = 5
    Public w, h, l, razmak, spcX, spcY, spcZ As Single
    Public xpolozaj As Single = 0
    Public ypolozaj As Single = 0
    Public zpolozaj As Single = 0
    Public xrot As Single = 0
    Public yrot As Single = 0
    Public zrot As Single = 0
    Public bojaKocke As Color = Color.Silver
    Public bojaLinije As Color = Color.Black
    Public selectedStyle As VisualStyles = VisualStyles.defaultStyle
    Public matrica As New List(Of Byte)
    Public matrice As New List(Of List(Of Byte))
    Public subset As New List(Of Integer)
    Public shp As shapes = shapes.Cube
#End Region
#Region "Fields"
    Dim sEdges As Boolean = True
    Dim conway As Boolean = True
    Dim dISO As Boolean = False
    Dim lw As Byte = 0
    Dim ime As String = "New CA"
    Dim llevelColor As Color = Color.SkyBlue
    Dim pp(,,) As Vector3 = {}
    Dim pi(,,) As Single = {}
    Dim iBuffer As New List(Of Integer)
    Dim mName As String = ""
#End Region
#Region "Events"
    Public Shared Event bufferRefreshed()
    Public Shared Event progressStart()
    Public Shared Event progressEnd()
    Public Shared Event progress(ByVal p As Integer, ByVal m As String)
#End Region
#Region "Browsable Properties"
    <Category("1. Meta")> _
    Public Property Name() As String
        Get
            Return ime
        End Get
        Set(ByVal value As String)
            ime = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Line Width"), Description("Edges line width")> _
    Public Property LineWidth() As Byte
        Get
            Return Me.lw
        End Get
        Set(ByVal value As Byte)
            Me.lw = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Show Edges")> _
    Public Property ShowEdges() As Boolean
        Get
            Return Me.sEdges
        End Get
        Set(ByVal value As Boolean)
            Me.sEdges = value
        End Set
    End Property
    <Category("2. Appearance"), Description("Rendering style")> _
    Public Property Style() As VisualStyles
        Get
            Return Me.selectedStyle
        End Get
        Set(ByVal value As VisualStyles)
            Me.selectedStyle = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Line Color"), Description("Edges color")> _
    Public Property LineColor() As Color
        Get
            Return Me.bojaLinije
        End Get
        Set(ByVal value As Color)
            Me.bojaLinije = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("Last Level Color"), Description("Color of cubes in last level")> _
    Public Property LastLevelColor() As Color
        Get
            Return Me.llevelColor
        End Get
        Set(ByVal value As Color)
            Me.llevelColor = value
        End Set
    End Property
    <Category("2. Appearance"), DisplayName("First Level Color"), Description("Color of cubes in first level")> _
    Public Property CubeColor() As Color
        Get
            Return Me.bojaKocke
        End Get
        Set(ByVal value As Color)
            Me.bojaKocke = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property Transparency() As Byte
        Get
            Return Me.providnost
        End Get
        Set(ByVal value As Byte)
            Me.providnost = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property Shape() As shapes
        Get
            Return Me.shp
        End Get
        Set(ByVal value As shapes)
            Me.shp = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property MeshName() As String
        Get
            Return Me.mName
        End Get
        Set(ByVal value As String)
            Me.mName = value
        End Set
    End Property
    <Category("3. Geometry"), Description("Cube width")> _
    Public Property width() As Single
        Get
            Return w
        End Get
        Set(ByVal value As Single)
            w = value
        End Set
    End Property
    <Category("3. Geometry"), Description("Cube height")> _
    Public Property height() As Single
        Get
            Return h
        End Get
        Set(ByVal value As Single)
            h = value
        End Set
    End Property
    <Category("3. Geometry"), Description("Cube lenght")> _
    Public Property lenght() As Single
        Get
            Return l
        End Get
        Set(ByVal value As Single)
            l = value
        End Set
    End Property
    <Category("3. Geometry"), Description("Distance between cubes X direction")> _
    Public Property SpaceX() As Single
        Get
            Return Me.spcX
        End Get
        Set(ByVal value As Single)
            Me.spcX = value
        End Set
    End Property
    <Category("3. Geometry"), Description("Distance between cubes Y direction")> _
    Public Property SpaceY() As Single
        Get
            Return Me.spcY
        End Get
        Set(ByVal value As Single)
            Me.spcY = value
        End Set
    End Property
    <Category("3. Geometry"), Description("Distance between cubes Z direction")> _
    Public Property SpaceZ() As Single
        Get
            Return Me.spcZ
        End Get
        Set(ByVal value As Single)
            Me.spcZ = value
        End Set
    End Property
    <Category("3. Geometry"), Description("Distance between cubes")> _
    Public Property Space() As Single
        Get
            Return razmak
        End Get
        Set(ByVal value As Single)
            Me.spcX = value
            Me.spcY = value
            Me.spcZ = value
            razmak = value
        End Set
    End Property
    <Category("4. Position")> _
    Public Property xPosition() As Single
        Get
            Return Me.xpolozaj
        End Get
        Set(ByVal value As Single)
            Me.xpolozaj = value
        End Set
    End Property
    <Category("4. Position")> _
    Public Property yPosition() As Single
        Get
            Return Me.ypolozaj
        End Get
        Set(ByVal value As Single)
            Me.ypolozaj = value
        End Set
    End Property
    <Category("4. Position")> _
    Public Property zPosition() As Single
        Get
            Return Me.zpolozaj
        End Get
        Set(ByVal value As Single)
            Me.zpolozaj = value
        End Set
    End Property
    <Category("5. Rotation")> _
    Public Property xRotation() As Single
        Get
            Return Me.xrot
        End Get
        Set(ByVal value As Single)
            Me.xrot = value
        End Set
    End Property
    <Category("5. Rotation")> _
    Public Property yRotation() As Single
        Get
            Return Me.yrot
        End Get
        Set(ByVal value As Single)
            Me.yrot = value
        End Set
    End Property
    <Category("5. Rotation")> _
    Public Property zRotation() As Single
        Get
            Return Me.zrot
        End Get
        Set(ByVal value As Single)
            Me.zrot = value
        End Set
    End Property
    <Category("6. ISO Surface"), DisplayName("Draw ISO"), Description("Draw ISO surface generated by cubes")> _
    Public Property drawISO() As Boolean
        Get
            Return Me.dISO
        End Get
        Set(ByVal value As Boolean)
            Me.dISO = value
        End Set
    End Property
    <Category("6. ISO Surface"), Description("ISO surface smooth factor")> _
    Public Property smooth() As Byte
        Get
            Return Me.sf
        End Get
        Set(ByVal value As Byte)
            Me.sf = value
        End Set
    End Property
#End Region
#Region "Non Browsable Properties"
    <Browsable(False)> _
    Public ReadOnly Property levelColor(ByVal level As Integer) As Color
        Get
            Dim r, g, b As Byte
            Dim r1, g1, b1, r2, g2, b2 As Byte
            r1 = Me.CubeColor.R
            g1 = Me.CubeColor.G
            b1 = Me.CubeColor.B
            r2 = Me.llevelColor.R
            g2 = Me.llevelColor.G
            b2 = Me.llevelColor.B
            If r1 > r2 Then
                r = CByte(r2 + Int(((r1 - r2) / Me.nOfLevels) * level))
            Else
                r = CByte(r1 + Int(((r2 - r1) / Me.nOfLevels) * level))
            End If
            If g1 > g2 Then
                g = CByte(g2 + Int(((g1 - g2) / Me.nOfLevels) * level))
            Else
                g = CByte(g1 + Int(((g2 - g1) / Me.nOfLevels) * level))
            End If
            If b1 > b2 Then
                b = CByte(b2 + Int(((b1 - b2) / Me.nOfLevels) * level))
            Else
                b = CByte(b1 + Int(((b2 - b1) / Me.nOfLevels) * level))
            End If
            Return Color.FromArgb(r, g, b)
        End Get
    End Property

    <Category("Rule"), Browsable(False)> _
    Public Property MinimalCells() As Byte
        Get
            Return Me.minBC
        End Get
        Set(ByVal value As Byte)
            If value > 7 Or value < 0 Or value >= Me.maxBC Then
                MsgBox("Value must be between 0 and 7, and lower than MaximalCells Value")
            Else
                Me.minBC = value
                Me.createLevels()
                Me.refreshBuffer()
            End If
        End Set
    End Property
    <Category("Rule"), Browsable(False)> _
    Public Property MaximalCells() As Byte
        Get
            Return Me.maxBC
        End Get
        Set(ByVal value As Byte)
            If value > 8 Or value < 1 Or value <= Me.minBC Then
                MsgBox("Value must be between 1 and 8, and lower than MinimalCells Value")
            Else
                Me.maxBC = value
                Me.createLevels()
                Me.refreshBuffer()
            End If
        End Set
    End Property
    <Category("Rule"), Browsable(False)> _
    Public Property TurnOnCells() As Byte
        Get
            Return Me.toLive
        End Get
        Set(ByVal value As Byte)
            If value > 8 Or value < 0 Then
                MsgBox("Value must be between 0 and 8")
            Else
                Me.toLive = value
                Me.createLevels()
                Me.refreshBuffer()
            End If
        End Set
    End Property
    <Category("Matrix Definition"), Browsable(False)> _
    Public Property brojPoljaPoXosi() As Integer
        Get
            Return xFields
        End Get
        Set(ByVal value As Integer)
            xFields = value
            Me.generisiSlucajnuMatricu()
            createLevels()
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Matrix Definition"), Browsable(False)> _
    Public Property brojPoljaPoYosi() As Integer
        Get
            Return yFields
        End Get
        Set(ByVal value As Integer)
            yFields = value
            Me.generisiSlucajnuMatricu()
            createLevels()
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Matrix Definition"), Browsable(False)> _
    Public Property Levels() As Integer
        Get
            Return nOfLevels
        End Get
        Set(ByVal value As Integer)
            nOfLevels = value
            createLevels()
            Me.refreshBuffer()
        End Set
    End Property
    <Browsable(False)> _
    Public Property Rule() As Byte
        Get
            Return praviloL
        End Get
        Set(ByVal value As Byte)
            praviloL = value
            Me.createLevels()
            Me.refreshBuffer()
        End Set
    End Property
    <Browsable(False)> _
    Public Property CellMatrix() As List(Of Byte)
        Get
            Return Me.matrice(0)
        End Get
        Set(ByVal value As List(Of Byte))
            Me.matrice(0) = value
        End Set
    End Property
#End Region
#Region "Constructors"
    Public Sub New(ByVal device As Device)
        w = 2
        h = 2
        l = 2
        razmak = 0.5
        spcX = razmak
        spcY = razmak
        spcZ = razmak
        Me.generisiSlucajnuMatricu()
        createLevels()
        Me.refreshBuffer(device)
    End Sub
    Public Sub New(ByVal device As Device, ByVal x As Integer, ByVal y As Integer, ByVal l1 As Integer)
        xFields = x
        yFields = y
        nOfLevels = l1
        w = 2
        h = 2
        l = 2
        razmak = 0.5
        spcX = razmak
        spcY = razmak
        spcZ = razmak
        Me.generisiSlucajnuMatricu()
        createLevels()
        Me.refreshBuffer(device)
    End Sub
#End Region
#Region "Public Methods"
    Public Sub generisiPraznuMatricu()
        Dim a, b As Integer
        Me.matrice = New List(Of List(Of Byte))
        Me.matrice.Add(New List(Of Byte))
        For a = 0 To Me.xFields - 1
            For b = 0 To Me.yFields - 1
                Me.matrice(0).Add(0)
            Next
        Next
    End Sub
    Public Sub generisiSlucajnuMatricu()
        Dim a, b As Integer
        Me.matrice = New List(Of List(Of Byte))
        Me.matrice.Add(New List(Of Byte))
        For a = 0 To Me.xFields - 1
            For b = 0 To Me.yFields - 1
                Randomize()
                Me.matrice(0).Add(CByte(Int((1 * Rnd()) + 0.35)))
            Next
        Next
    End Sub
    Public Sub refreshBuffer(Optional ByVal device As Device = Nothing)
        RaiseEvent progressStart()
        Dim cmm As ClassMesh
        For Each cmm In Me.meshBuffer
            Try
                cmm.mesh.Dispose()
            Catch ex As Exception

            End Try
        Next
        Me.meshBuffer.Clear()
        ' TRANSFORMATION
        Dim m, mm As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Translate(Me.xpolozaj, Me.ypolozaj, Me.zpolozaj)
        m = Matrix.Multiply(mm, m)
        mm.RotateX(CSng(Me.xRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.RotateY(CSng(Me.yRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.RotateZ(CSng(Me.zRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        ' ------------------
        Dim b As Byte
        Dim c1, c2, c3, c4, c5, c6, c7, c8 As CustomVertex.PositionNormalTextured
        Dim v(7) As Vector3
        Dim vertices(2) As CustomVertex.PositionNormalTextured
        Dim w1, l1, h1 As Single
        Dim n1, n2, n3, n4, n5, n6 As Vector3
        n1 = New Vector3(0, 0, 1)
        n2 = New Vector3(0, 1, 0)
        n3 = New Vector3(1, 0, 0)
        n4 = New Vector3(0, 0, -1)
        n5 = New Vector3(0, -1, 0)
        n6 = New Vector3(-1, 0, 0)
        w1 = w + Me.spcX
        l1 = l + Me.spcY
        h1 = h + Me.spcZ
        c1 = New CustomVertex.PositionNormalTextured
        c2 = New CustomVertex.PositionNormalTextured
        c3 = New CustomVertex.PositionNormalTextured
        c4 = New CustomVertex.PositionNormalTextured
        c5 = New CustomVertex.PositionNormalTextured
        c6 = New CustomVertex.PositionNormalTextured
        c7 = New CustomVertex.PositionNormalTextured
        c8 = New CustomVertex.PositionNormalTextured
        
        Me.bufferT = New List(Of CustomVertex.PositionNormalTextured)
        Me.DXFBuffer = New List(Of CustomVertex.PositionColored)
        Me.LineBuffer = New List(Of CustomVertex.PositionColored)
        Me.subset = New List(Of Integer)
        Dim bi, mi, ni, level As Integer
        ni = 0
        level = 0
        Dim pi, p, plen As Integer
        plen = Me.matrice.Count * Me.matrice(0).Count
        pi = 0
        Dim matrica As List(Of Byte)
        For Each matrica In Me.matrice
            bi = 0 : mi = 0
            For Each b In matrica
                p = 100 * pi \ plen
                pi += 1
                RaiseEvent progress(p, "Creating CA 3D matrix...")
                If b > 0 Then
                    If Me.Shape = shapes.Mesh Then
                        Dim cm As New ClassMesh
                        Dim i As Integer
                        Dim vMesh, tMesh As Vector3
                        Dim m1 As Matrix
                        tMesh = New Vector3(CSng((mi - Int(mi / Me.xFields) * Me.xFields) * w1 + w / 2), CSng(Int(mi / Me.xFields) * l1 + l / 2), ni * h1 + h / 2)
                        mm.Translate(tMesh)
                        m1 = Matrix.Multiply(mm, m)
                        Try
                            Dim bm As ClassMesh = mf.Scena.MeshList.Find(AddressOf Me.findMesh)
                            For i = 0 To bm.vbf.Count - 1
                                vMesh = Vector3.TransformCoordinate(bm.vbf(i).Position, m1)
                                cm.vbfo.Add(vMesh)
                            Next
                            For i = 0 To bm.ibf.Count - 1
                                cm.ibf.Add(bm.ibf(i))
                            Next
                            With cm
                                .c = bm.c
                                .t = bm.t
                            End With
                            cm.refreshBuffer(bm.mesh.Device)
                            Me.meshBuffer.Add(cm)
                        Catch ex As Exception

                        End Try
                    End If
                    ' Create material subset
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)
                    Me.subset.Add(level)

                    ' Calculate cube coordinates 
                    v(0) = New Vector3(CSng((mi - Int(mi / Me.xFields) * Me.xFields) * w1), CSng(Int(mi / Me.xFields) * l1), ni * h1)
                    v(1) = New Vector3(v(0).X + w, v(0).Y, v(0).Z)
                    v(2) = New Vector3(v(0).X + w, v(0).Y + l, v(0).Z)
                    v(3) = New Vector3(v(0).X, v(0).Y + l, v(0).Z)
                    v(4) = New Vector3(v(0).X, v(0).Y, v(0).Z + h)
                    v(5) = New Vector3(v(0).X + w, v(0).Y, v(0).Z + h)
                    v(6) = New Vector3(v(0).X + w, v(0).Y + l, v(0).Z + h)
                    v(7) = New Vector3(v(0).X, v(0).Y + l, v(0).Z + h)

                    ' Transform cube coordinates
                    c1.Position = Vector3.TransformCoordinate(v(0), m)
                    c2.Position = Vector3.TransformCoordinate(v(1), m)
                    c3.Position = Vector3.TransformCoordinate(v(2), m)
                    c4.Position = Vector3.TransformCoordinate(v(3), m)
                    c5.Position = Vector3.TransformCoordinate(v(4), m)
                    c6.Position = Vector3.TransformCoordinate(v(5), m)
                    c7.Position = Vector3.TransformCoordinate(v(6), m)
                    c8.Position = Vector3.TransformCoordinate(v(7), m)

                    'line buffer 
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))

                    ' dxf buffer
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije.ToArgb))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije.ToArgb))
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
                    bi += 1
                End If
                mi += 1
            Next
            ni += 1
            level += 1
        Next
        If device IsNot Nothing Then
            Me.createISOMesh(device)
            Me.createMesh(device)
        Else
            If Me.ISOMesh IsNot Nothing Then
                Try
                    Me.createISOMesh(Me.ISOMesh.Device)
                    Me.createMesh(Me.CAMesh.Device)
                Catch ex As Exception

                End Try
            ElseIf Me.CAMesh IsNot Nothing Then
                Try
                    Me.createISOMesh(Me.ISOMesh.Device)
                    Me.createMesh(Me.CAMesh.Device)
                Catch ex As Exception

                End Try
            End If
        End If
        RaiseEvent bufferRefreshed()
        RaiseEvent progressEnd()
    End Sub
    Public Sub createMesh(ByVal device As Device)
        Try
            Me.CAMesh.Dispose()
        Catch ex As Exception

        End Try
        Dim vvv() As CustomVertex.PositionNormalTextured = bufferT.ToArray
        Dim c, i As Integer
        c = vvv.Length

        ' Creating mesh
        Dim indices As New List(Of Integer)
        For i = 0 To c - 1
            indices.Add(i)
        Next
        Dim ind() As Integer = indices.ToArray


        Try
            Me.CAMesh = New Mesh(CInt(c / 3), c, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, device)
            Me.CAMesh.SetVertexBufferData(vvv, LockFlags.None)
            Me.CAMesh.SetIndexBufferData(ind, LockFlags.None)
            Me.CAMesh.ComputeNormals()
            Dim subset() As Integer = Me.CAMesh.LockAttributeBufferArray(LockFlags.Discard)
            subset = Me.subset.ToArray
            Me.CAMesh.UnlockAttributeBuffer(subset)
            ' Optimize.
            Dim adjacency(Me.CAMesh.NumberFaces * 3 - 1) As Integer
            Me.CAMesh.GenerateAdjacency(CSng(0.1), adjacency)
            Me.CAMesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
        Catch ex As Exception
            Me.CAMesh = Nothing
        End Try
    End Sub
    Public Sub createLevels()
        Me.matrica = Me.matrice(0)
        Me.matrice = New List(Of List(Of Byte))
        Me.matrice.Add(Me.matrica)
        Dim ln, endP As Integer
        Dim p0, p1, p2, p3, p4, p5, p6, p7 As Integer
        Try
            endP = Me.matrice(0).Count - 1
        Catch ex As Exception
            Exit Sub
        End Try
        For ln = 1 To Me.nOfLevels - 1
            Me.matrice.Add(New List(Of Byte))
            Dim b As Byte
            Dim mi As Integer
            mi = 0
            For Each b In Me.matrice(ln - 1)
                Dim k As Byte = 0
                p0 = mi - Me.xFields - 1
                p1 = mi - Me.xFields
                p2 = mi - Me.xFields + 1
                p3 = mi - 1
                p4 = mi + 1
                p5 = mi + Me.xFields - 1
                p6 = mi + Me.xFields
                p7 = mi + Me.xFields + 1
                Dim ukupnoZivih As Byte = 0
                If p0 >= 0 And p0 <= endP And Int((p0 + 1) / Me.xFields) * xFields <> (p0 + 1) Then
                    If Me.matrice(ln - 1)(p0) > 0 Then
                        ukupnoZivih += CByte(1)
                        k = k Or CByte(1)
                    End If
                End If
                If p1 >= 0 And p1 <= endP Then
                    If Me.matrice(ln - 1)(p1) > 0 Then
                        ukupnoZivih += CByte(1)
                        k = k Or CByte(2)
                    End If
                End If
                If p2 >= 0 And p2 <= endP And Int(p2 / Me.xFields) * xFields <> p2 Then
                    If Me.matrice(ln - 1)(p2) > 0 Then
                        ukupnoZivih += CByte(1)
                        k = k Or CByte(4)
                    End If
                End If
                If p3 >= 0 And p3 <= endP And (p3 - Int(p3 / Me.xFields) * xFields) <> (Me.xFields - 1) Then
                    If Me.matrice(ln - 1)(p3) > 0 Then
                        ukupnoZivih += CByte(1)
                        k = k Or CByte(8)
                    End If

                End If
                If p4 >= 0 And p4 <= endP And Int(p4 / Me.xFields) * xFields <> p4 Then
                    If Me.matrice(ln - 1)(p4) > 0 Then
                        ukupnoZivih += CByte(1)
                        k = k Or CByte(16)
                    End If
                End If
                If p5 >= 0 And p5 <= endP And (p5 - Int(p5 / Me.xFields) * xFields) <> (Me.xFields - 1) Then
                    If Me.matrice(ln - 1)(p5) > 0 Then
                        ukupnoZivih += CByte(1)
                        k = k Or CByte(32)
                    End If
                End If
                If p6 >= 0 And p6 <= endP Then
                    If Me.matrice(ln - 1)(p6) > 0 Then
                        ukupnoZivih += CByte(1)
                        k = k Or CByte(64)
                    End If
                End If
                If p7 >= 0 And p7 <= endP And Int(p7 / Me.xFields) * xFields <> p7 Then
                    If Me.matrice(ln - 1)(p7) > 0 Then
                        ukupnoZivih += CByte(1)
                        k = k Or CByte(128)
                    End If
                End If
                If conway Then
                    If ukupnoZivih < Me.minBC Or ukupnoZivih > Me.maxBC Then
                        Me.matrice(ln).Add(0)
                    ElseIf ukupnoZivih = Me.toLive Then
                        Me.matrice(ln).Add(1)
                    Else
                        Me.matrice(ln).Add(b)
                    End If
                Else
                    If CBool(k And Me.Rule) Then
                        Me.matrice(ln).Add(Not b)
                    Else
                        Me.matrice(ln).Add(0)
                    End If
                End If
                mi += 1
            Next
        Next
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        Me.vBuffer = New List(Of CustomVertex.PositionNormalTextured)
        Me.bufferT = New List(Of CustomVertex.PositionNormalTextured)
        Me.DXFBuffer = New List(Of CustomVertex.PositionColored)
        Me.meshBuffer = New List(Of ClassMesh)
        refreshBuffer(device)
    End Sub
    Public Sub createISOMesh(ByVal device As Device)
        ReDim pp(Me.xFields, Me.yFields, Me.nOfLevels)
        ReDim pi(Me.xFields, Me.yFields, Me.nOfLevels)
        Me.vBuffer.Clear()
        Me.iBuffer.Clear()
        ' add values and coordinates pairs
        Dim w1, l1, h1 As Single
        Dim bi, mi, ni, level As Integer
        Dim b As Byte
        w1 = w + Me.spcX
        l1 = l + Me.spcY
        h1 = h + Me.spcZ
        ni = 0
        level = 0
        Dim matrica As List(Of Byte)
        For Each matrica In Me.matrice
            bi = 0 : mi = 0
            For Each b In matrica
                pp(CInt(mi - Int(mi / Me.xFields) * Me.xFields), CInt(mi / Me.xFields), ni) = New Vector3(CInt((mi - Int(mi / Me.xFields) * Me.xFields) * w1 + w / 2), CInt(mi / Me.xFields) * l1 + l / 2, ni * h1 + h / 2)
                If b > 0 Then
                    pi(CInt(mi - Int(mi / Me.xFields) * Me.xFields), CInt(mi / Me.xFields), ni) = 1
                Else
                    pi(CInt(mi - Int(mi / Me.xFields) * Me.xFields), CInt(mi / Me.xFields), ni) = 0
                End If
                mi += 1
            Next
            ni += 1
            level += 1
        Next

        Dim cx, cy, cz, x, y, z As Integer

        Dim i As Integer = 0

        cz = -1
        Dim gcell As New ClassMTeth.GRIDCELL
        Dim trg() As ClassMTeth.TRIANGLE = {New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, _
                                                   New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, _
                                                   New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE}
        Dim cmt As New ClassMTeth
        Dim cmc As New ClassMCube
        Dim nind As Integer
        For z = 0 To Me.nOfLevels - 1
            cz += 1
            cy = -1
            For y = 0 To Me.yFields - 1
                cy += 1
                cx = -1
                For x = 0 To Me.xFields - 1
                    cx += 1
                    gcell.p(3) = pp(cx, cy, cz)
                    gcell.p(2) = pp(cx + 1, cy, cz)
                    gcell.p(1) = pp(cx + 1, cy + 1, cz)
                    gcell.p(0) = pp(cx, cy + 1, cz)
                    gcell.p(7) = pp(cx, cy, cz + 1)
                    gcell.p(6) = pp(cx + 1, cy, cz + 1)
                    gcell.p(5) = pp(cx + 1, cy + 1, cz + 1)
                    gcell.p(4) = pp(cx, cy + 1, cz + 1)

                    gcell.val(3) = pi(cx, cy, cz)
                    gcell.val(2) = pi(cx + 1, cy, cz)
                    gcell.val(1) = pi(cx + 1, cy + 1, cz)
                    gcell.val(0) = pi(cx, cy + 1, cz)
                    gcell.val(7) = pi(cx, cy, cz + 1)
                    gcell.val(6) = pi(cx + 1, cy, cz + 1)
                    gcell.val(5) = pi(cx + 1, cy + 1, cz + 1)
                    gcell.val(4) = pi(cx, cy + 1, cz + 1)

                    ' Marching tetrahedrons
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 2, 3, 7)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 2, 6, 7)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 4, 6, 7)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 6, 1, 2)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 6, 1, 4)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 5, 6, 1, 4)
                    'addTriangles(trg, i, nind)

                    ' Marching cubes
                    nind = cmc.Polygonise(gcell, 1, trg)
                    addTriangles(trg, i, nind)

                Next
            Next
        Next
        Me.applyTransform()
        Try
            Me.ISOMesh.Dispose()
        Catch ex As Exception

        End Try
        ' Create mesh
        Dim vvv() As CustomVertex.PositionNormalTextured = Me.vBuffer.ToArray
        Dim c1 As Integer
        c1 = vvv.Length
        Dim ind() As Int32 = Me.iBuffer.ToArray
        ISOMesh = New Mesh(CInt(ind.Length / 3), c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, device)
        Try
            ISOMesh.SetVertexBufferData(vvv, LockFlags.None)
            ISOMesh.SetIndexBufferData(ind, LockFlags.None)
            ISOMesh.ComputeNormals()
            Dim subset() As Integer = ISOMesh.LockAttributeBufferArray(LockFlags.Discard)
            ISOMesh.UnlockAttributeBuffer(subset)
            ' Optimize.
            Dim adjacency(ISOMesh.NumberFaces * 3 - 1) As Integer
            ISOMesh.GenerateAdjacency(CSng(0.1), adjacency)
            ISOMesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
            ISOMesh = Mesh.TessellateNPatches(ISOMesh, adjacency, Me.sf, True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub applyTransform()
        ' TRANSFORMATION
        Dim m, mm As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Translate(Me.xpolozaj, Me.ypolozaj, Me.zpolozaj)
        m = Matrix.Multiply(mm, m)
        mm.RotateX(CSng(Me.xRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.RotateY(CSng(Me.yRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.RotateZ(CSng(Me.zRotation * Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        Dim ii As Integer
        Dim v As New CustomVertex.PositionNormalTextured
        For ii = 0 To Me.vBuffer.Count - 1
            v.Normal = Me.vBuffer(ii).Normal
            v.Position = Vector3.TransformCoordinate(Me.vBuffer(ii).Position, m)
            Me.vBuffer(ii) = v
        Next
    End Sub
#End Region
#Region "Private Methods"
    Private Sub addTriangles(ByVal trg As ClassMTeth.TRIANGLE(), ByVal i As Integer, ByVal nind As Integer)
        Dim indx As Integer
        For indx = 0 To nind
            triangle(trg(indx).p(0), trg(indx).p(1), trg(indx).p(2), i)
        Next
    End Sub
    Private Sub triangle(ByVal vkt1 As Vector3, ByVal vkt2 As Vector3, ByVal vkt3 As Vector3, ByRef i As Integer)
        Dim vktn As Vector3
        Dim a As Integer
        vktn = Vector3.Cross(vkt2, vkt1)
        'vktn.Normalize()
        a = indiceNumber(vkt1)
        If a > -1 Then
            Me.iBuffer.Add(a)
        Else
            Me.vBuffer.Add(New CustomVertex.PositionNormalTextured(vkt1, vktn, 1, 1))
            Me.iBuffer.Add(Me.vBuffer.Count - 1)
        End If
        a = indiceNumber(vkt2)
        If a > -1 Then
            Me.iBuffer.Add(a)
        Else
            Me.vBuffer.Add(New CustomVertex.PositionNormalTextured(vkt2, vktn, 1, 1))
            Me.iBuffer.Add(Me.vBuffer.Count - 1)
        End If
        a = indiceNumber(vkt3)
        If a > -1 Then
            Me.iBuffer.Add(a)
        Else
            Me.vBuffer.Add(New CustomVertex.PositionNormalTextured(vkt3, vktn, 1, 1))
            Me.iBuffer.Add(Me.vBuffer.Count - 1)
        End If
    End Sub
#End Region
#Region "Functions"
    Private Function indiceNumber(ByVal vect As Vector3) As Integer
        Dim rv As Integer = -1
        Dim i As Integer
        For i = 0 To Me.vBuffer.Count - 1
            If Me.vBuffer(i).Position.X = vect.X And Me.vBuffer(i).Position.Y = vect.Y And Me.vBuffer(i).Position.Z = vect.Z Then
                rv = i
                Return rv
            End If
        Next
        Return rv
    End Function
    Public Function findMesh(ByVal mesh As ClassMesh) As Boolean
        If mesh.Name = Me.mName Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class
