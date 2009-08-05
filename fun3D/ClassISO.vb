Imports System.Math
Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports MSScriptControl
Imports CTools
<System.Serializable()> _
Public Class ClassISO
#Region "Non Serialized Fields"
    <System.NonSerialized()> _
    Dim ee As New MSScriptControl.ScriptControl
    <System.NonSerialized()> _
    Public ISOMesh As Mesh
    <System.NonSerialized()> _
    Public vBuffer As New List(Of CustomVertex.PositionNormalTextured)
#End Region
#Region "Public Fields"
    Public sf As Byte = 1
    Public x, y, z As Single
    Public xpolozaj As Single = 0
    Public ypolozaj As Single = 0
    Public zpolozaj As Single = 0
    Public xrot As Single = 0
    Public yrot As Single = 0
    Public zrot As Single = 0
    Public iBuffer As New List(Of Integer)
#End Region
#Region "Private Fields"
    Dim naziv As String = "new1"
    Dim f As String = "0"
    Dim tol As String = "0"
    Dim alpha As Byte = 255
    Dim xd As Integer = 10
    Dim yd As Integer = 10
    Dim zd As Integer = 10
    Dim maxX As Single = 10
    Dim maxY As Single = 10
    Dim maxZ As Single = 10
    Dim minX As Single = -10
    Dim minY As Single = -10
    Dim minZ As Single = -10
    Dim scX As Single = 1
    Dim scY As Single = 1
    Dim scZ As Single = 1
    Dim fcolor As Color = Color.DarkRed
    Dim bcolor As Color = Color.White
    Dim pp(,,) As Vector3 = {}
    Dim pi(,,) As Single = {}
#End Region
#Region "Events"
    Public Shared Event bufferRefreshed()
    Public Shared Event progressStart()
    Public Shared Event progressEnd()
    Public Shared Event progress(ByVal p As Integer, ByVal m As String)
#End Region
#Region "Browsable Properties"
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
    <Category("2. Appearance")> _
    Public Property FrontColor() As Color
        Get
            Return Me.fcolor
        End Get
        Set(ByVal value As Color)
            Me.fcolor = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property BackColor() As Color
        Get
            Return Me.bcolor
        End Get
        Set(ByVal value As Color)
            Me.bcolor = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property Transparency() As Byte
        Get
            Return Me.alpha
        End Get
        Set(ByVal value As Byte)
            Me.alpha = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property smooth() As Byte
        Get
            Return Me.sf
        End Get
        Set(ByVal value As Byte)
            Me.sf = value
        End Set
    End Property
    <Category("1. Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Me.naziv = value
        End Set
    End Property
    <Category("3. Definition"), DisplayName("ISO Value")> _
    Public Property Tolerance() As String
        Get
            Return Me.tol
        End Get
        Set(ByVal value As String)
            Me.tol = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("3. Definition"), DisplayName("Function")> _
    Public Property Fun() As String
        Get
            Return Me.f
        End Get
        Set(ByVal value As String)
            Me.f = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("3. Definition")> _
    Public Property Xdensity() As Integer
        Get
            Return Me.xd
        End Get
        Set(ByVal value As Integer)
            Me.xd = value
        End Set
    End Property
    <Category("3. Definition")> _
    Public Property Ydensity() As Integer
        Get
            Return Me.yd
        End Get
        Set(ByVal value As Integer)
            Me.yd = value
        End Set
    End Property
    <Category("3. Definition")> _
    Public Property Zdensity() As Integer
        Get
            Return Me.zd
        End Get
        Set(ByVal value As Integer)
            Me.zd = value
        End Set
    End Property
    <Category("3. Definition"), DisplayName("Max X")> _
    Public Property Xmaksimalno() As Single
        Get
            Return Me.maxX
        End Get
        Set(ByVal value As Single)
            Me.maxX = value
        End Set
    End Property
    <Category("3. Definition"), DisplayName("Max Y")> _
    Public Property Ymaksimalno() As Single
        Get
            Return Me.maxY
        End Get
        Set(ByVal value As Single)
            Me.maxY = value
        End Set
    End Property
    <Category("3. Definition"), DisplayName("Max Z")> _
    Public Property Zmaksimalno() As Single
        Get
            Return Me.maxZ
        End Get
        Set(ByVal value As Single)
            Me.maxZ = value
        End Set
    End Property
    <Category("3. Definition"), DisplayName("Min X")> _
    Public Property Xminimalno() As Single
        Get
            Return Me.minX
        End Get
        Set(ByVal value As Single)
            Me.minX = value
        End Set
    End Property
    <Category("3. Definition"), DisplayName("Min Y")> _
    Public Property Yminimalno() As Single
        Get
            Return Me.minY
        End Get
        Set(ByVal value As Single)
            Me.minY = value
        End Set
    End Property
    <Category("3. Definition"), DisplayName("Min Z")> _
    Public Property Zminimalno() As Single
        Get
            Return Me.minZ
        End Get
        Set(ByVal value As Single)
            Me.minZ = value
        End Set
    End Property
#End Region
#Region "Private Properties"
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
#End Region
#Region "Constructors"
    Public Sub New()
        Me.loadFun()
        Me.refreshBuffer()
    End Sub
    Public Sub New(ByVal Name As String, ByVal device As Direct3D.Device)
        Me.Name = Name
        Me.minX = -3.14
        Me.maxX = 3.14
        Me.minY = -3.14
        Me.maxY = 3.14
        Me.minZ = -3.14
        Me.maxZ = 3.14
        Me.f = "cos(x)+cos(y)+cos(z)"
        Me.loadFun()
        Me.refreshBuffer(device)
    End Sub
#End Region
#Region "Public Methods"
    Public Sub evaluateISO()
        RaiseEvent progressStart()
        Dim vkt As Vector3
        Dim xs, ys, zs As Single
        Dim cx, cy, cz As Integer
        Dim ISOVal As Single = 0
        xs = Me.XStep
        ys = Me.YStep
        zs = Me.ZStep
        Array.Clear(pp, 0, pp.Length)
        Array.Clear(pi, 0, pi.Length)
        ReDim pp(xd + 1, yd + 1, zd + 1)
        ReDim pi(xd + 1, yd + 1, zd + 1)
        cz = -1
        Dim plen, pin, p As Integer
        plen = (xd + 1) * (yd + 1) * (zd + 1)
        pin = 0
        For z = minZ To maxZ Step zs
            cz += 1
            cy = -1
            For y = minY To maxY Step ys
                cy += 1
                cx = -1
                For x = minX To maxX Step xs
                    p = 100 * pin \ plen
                    RaiseEvent progress(p, "Calculating points " + pin.ToString + "/" + plen.ToString)
                    pin += 1
                    cx += 1
                    Try
                        vkt = New Vector3(x, y, z)
                        pp(cx, cy, cz) = vkt
                        ISOVal = ee.Eval(Me.f) - ee.Eval(Me.tol)
                        pi(cx, cy, cz) = ISOVal
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        Array.Clear(pi, 0, pi.Length)
                        Exit Sub
                    End Try
                Next
            Next
        Next
        RaiseEvent progressEnd()
    End Sub
    Public Sub refreshBuffer(Optional ByVal device As Direct3D.Device = Nothing)
        Me.evaluateISO()
        RaiseEvent progressStart()
        Me.vBuffer.Clear()
        Me.iBuffer.Clear()
        Dim xs, ys, zs As Single
        Dim cx, cy, cz As Integer

        Dim i As Integer = 0
        xs = Me.XStep
        ys = Me.YStep
        zs = Me.ZStep
        cz = -1
        Dim gcell As New CTools.ClassMTeth.GRIDCELL
        Dim trg() As CTools.ClassMTeth.TRIANGLE = {New CTools.ClassMTeth.TRIANGLE, New CTools.ClassMTeth.TRIANGLE, New CTools.ClassMTeth.TRIANGLE, _
                                                   New CTools.ClassMTeth.TRIANGLE, New CTools.ClassMTeth.TRIANGLE, New CTools.ClassMTeth.TRIANGLE, _
                                                   New CTools.ClassMTeth.TRIANGLE, New CTools.ClassMTeth.TRIANGLE, New CTools.ClassMTeth.TRIANGLE}
        Dim cmt As New CTools.ClassMTeth
        Dim cmc As New CTools.ClassMCube
        Dim nind As Integer
        Dim plen, pin, p As Integer
        plen = (xd + 1) * (yd + 1) * (zd + 1)
        pin = 0
        For z = minZ To maxZ - xs Step zs
            cz += 1
            cy = -1
            For y = minY To maxY - ys Step ys
                cy += 1
                cx = -1
                For x = minX To maxX - zs Step xs
                    p = 100 * pin \ plen
                    RaiseEvent progress(p, "Creating surface " + pin.ToString + "/" + plen.ToString)
                    pin += 1
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

                    nind = cmc.Polygonise(gcell, 0, trg)
                    addTriangles(trg, i, nind)

                Next
            Next
        Next

        applyTransform()

        Try
            If device = Nothing Then
                device = Me.ISOMesh.Device
            End If
            Me.createMesh(device)
        Catch ex As Exception

        End Try
        RaiseEvent progressEnd()
    End Sub
    Public Sub createMesh(ByVal device As Direct3D.Device)
        Try
            Me.ISOMesh.Dispose()
        Catch ex As Exception

        End Try
        ' RUTINA ZA PRAVLJENJE MESHA
        Dim vvv() As CustomVertex.PositionNormalTextured = Me.vBuffer.ToArray
        Dim c1 As Integer
        c1 = vvv.Length

        Dim ind() As Int32 = Me.iBuffer.ToArray
        ISOMesh = New Mesh(ind.Length / 3, c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, device)

        Try
            ISOMesh.SetVertexBufferData(vvv, LockFlags.None)
            ISOMesh.SetIndexBufferData(ind, LockFlags.None)
            ISOMesh.ComputeNormals()
            Dim subset() As Integer = ISOMesh.LockAttributeBufferArray(LockFlags.Discard)
            'subset = Me.subset.ToArray
            ISOMesh.UnlockAttributeBuffer(subset)
            ' Optimize.
            Dim adjacency(ISOMesh.NumberFaces * 3 - 1) As Integer
            ISOMesh.GenerateAdjacency(CSng(0.1), adjacency)
            ISOMesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
            ISOMesh = Mesh.TessellateNPatches(ISOMesh, adjacency, Me.sf, True)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        Me.vBuffer = New List(Of CustomVertex.PositionNormalTextured)
        loadFun()
        refreshBuffer(device)
    End Sub
#End Region
#Region "Private methods"
    Private Sub loadFun()
        ee = New MSScriptControl.ScriptControl
        ee.Language = "vbscript"
        ee.AllowUI = True
        ee.AddObject("mathis", New EvalFunctions, True)
        ee.AddObject("uv", Me, True)
    End Sub
    Private Sub applyTransform()
        ' TRANSFORMATION
        Dim m, mm As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Translate(Me.xpolozaj, Me.ypolozaj, Me.zpolozaj)
        m = Matrix.Multiply(mm, m)
        mm.Scale(Me.scX, Me.scY, Me.scZ)
        m = Matrix.Multiply(mm, m)
        mm.RotateX(Me.xRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        mm.RotateY(Me.yRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        mm.RotateZ(Me.zRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        Dim ii As Integer
        Dim v As New CustomVertex.PositionNormalTextured
        Dim plen, pin, p As Integer
        plen = Me.vBuffer.Count
        pin = 0
        For ii = 0 To Me.vBuffer.Count - 1
            p = 100 * pin \ plen
            RaiseEvent progress(p, "Transforming " + pin.ToString + "/" + plen.ToString)
            pin += 1
            v.Normal = Me.vBuffer(ii).Normal
            v.Position = Vector3.TransformCoordinate(Me.vBuffer(ii).Position, m)
            Me.vBuffer(ii) = v
        Next
    End Sub
    Private Sub addTriangles(ByVal trg As CTools.ClassMTeth.TRIANGLE(), ByVal i As Integer, ByVal nind As Integer)
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
#End Region
End Class
