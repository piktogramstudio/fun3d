Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
<System.Serializable()> _
Public Class ClassCrackedPoly
    Inherits ClassFun3DObject
    Dim polyVertices As New List(Of Vector3)
    <System.NonSerialized()> _
    Public mesh As Mesh
    <System.NonSerialized()> _
    Public triangles As New List(Of CustomVertex.PositionNormalTextured)
    <System.NonSerialized()> _
    Public lineBuffer As New List(Of CustomVertex.PositionColored)
    <System.NonSerialized()> _
    Dim dev As Device = Nothing
    Dim extr As Single = 0.4
    Dim cExtr As Single = 2
    Dim iter As Integer = 2
    
    Public c As Color = Drawing.Color.Red
    Public t As Byte = 255
    Public Shared Event bufferRefreshed()
    Dim ct As crackingType = crackingType.NormalExtrude
    Dim lc As Color = color.Black
    <Category("2. Appearance")> _
        Public Property LineColor() As Color
        Get
            Return Me.lc
        End Get
        Set(ByVal value As Color)
            Me.lc = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property color() As Color
        Get
            Return Me.c
        End Get
        Set(ByVal value As Color)
            Me.c = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property transparency() As Byte
        Get
            Return Me.t
        End Get
        Set(ByVal value As Byte)
            Me.t = value
        End Set
    End Property
    
    
    <Category("2. Appearance")> _
    Public Property Iterations() As Integer
        Get
            Return Me.iter
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then
                MsgBox("Value must be positive number")
                Exit Property
            End If
            Me.iter = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property CentralPointExtrusion() As Single
        Get
            Return Me.cExtr
        End Get
        Set(ByVal value As Single)
            Me.cExtr = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property PointExtrusion() As Single
        Get
            Return Me.extr
        End Get
        Set(ByVal value As Single)
            Me.extr = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property PolygonPoints() As List(Of Vector3)
        Get
            Return Me.polyVertices
        End Get
        Set(ByVal value As List(Of Vector3))
            Me.polyVertices = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property TypeOfCracking() As crackingType
        Get
            Return Me.ct
        End Get
        Set(ByVal value As crackingType)
            Me.ct = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property edgePoints() As String()
        Get
            Dim rv() As String
            ReDim rv(Me.polyVertices.Count - 1)
            Dim i As Integer
            For i = 0 To Me.polyVertices.Count - 1
                rv(i) = Me.polyVertices(i).X.ToString + ":" + Me.polyVertices(i).Y.ToString + ":" + Me.polyVertices(i).Z.ToString
            Next
            Return rv
        End Get
        Set(ByVal value As String())
            Me.polyVertices.Clear()
            Dim i As Integer
            Dim s() As String
            For i = 0 To value.Length - 1
                s = value(i).Split(":")
                Me.polyVertices.Add(New Vector3(Val(s(0)), Val(s(1)), Val(s(2))))
            Next
        End Set
    End Property
    Public Sub New()
        Me.Name = "New cracked poly"
        Me.polyVertices.Add(New Vector3(0, 0, 0))
        Me.polyVertices.Add(New Vector3(10, 0, 0))
        Me.polyVertices.Add(New Vector3(15, 5, 0))
        Me.polyVertices.Add(New Vector3(10, 10, 0))
        Me.polyVertices.Add(New Vector3(0, 10, 0))
        Me.polyVertices.Add(New Vector3(-5, 5, 0))
        Me.refreshBuffer()
    End Sub
    Public Sub New(ByVal Name As String, ByVal device As Device)
        Me.Name = Name
        Me.refreshBuffer(device)
        dev = device
    End Sub
    Public Function cCenter(ByVal shp As List(Of Vector3), ByVal extr As Single) As Vector3
        Dim rv As Vector3 = Nothing
        Dim rv1 As Vector3 = Nothing
        Dim shpEnum As IEnumerator(Of Vector3) = shp.GetEnumerator()
        shpEnum.Reset()
        If shpEnum.MoveNext() Then
            rv = shpEnum.Current
            While shpEnum.MoveNext()
                rv.Add(shpEnum.Current)
            End While
            Select Case ct
                Case crackingType.NormalExtrude
                    rv1 = Vector3.Cross(shp(1) - shp(0), shp(2) - shp(0))
                    rv1.Normalize()
                    rv1.Multiply(extr)
                    rv.Multiply(1 / shp.Count)
                    rv = Vector3.TransformCoordinate(rv1, Matrix.Translation(rv))
                Case crackingType.ZAxisExtrude
                    rv.Multiply(1 / shp.Count)
                    rv.Z = extr
                Case crackingType.NonInverseNormalExtrude
                    rv1 = Vector3.Cross(shp(1) - shp(0), shp(2) - shp(0))
                    rv1.Normalize()
                    rv1.Multiply(Math.Abs(extr))
                    rv.Multiply(1 / shp.Count)
                    rv = Vector3.TransformCoordinate(rv1, Matrix.Translation(rv))
                Case crackingType.NonInverseZAxisExtrude
                    rv.Multiply(1 / shp.Count)
                    rv.Z = Math.Abs(extr)
            End Select

        End If
        Return rv
    End Function
    Public Sub triangulatePoly()
        Dim cnt As Vector3 = cCenter(polyVertices, cExtr)
        Dim i As Integer
        triangles.Clear()
        For i = 0 To polyVertices.Count - 2
            triangles.Add(New CustomVertex.PositionNormalTextured(polyVertices(i), Vector3.Cross(polyVertices(i), polyVertices(i + 1)), 0, 0))
            triangles.Add(New CustomVertex.PositionNormalTextured(polyVertices(i + 1), Vector3.Cross(polyVertices(i + 1), cnt), 0, 1))
            triangles.Add(New CustomVertex.PositionNormalTextured(cnt, Vector3.Cross(cnt, polyVertices(i)), 1, 0))
        Next
        triangles.Add(New CustomVertex.PositionNormalTextured(polyVertices(polyVertices.Count - 1), Vector3.Cross(polyVertices(polyVertices.Count - 1), polyVertices(0)), 0, 0))
        triangles.Add(New CustomVertex.PositionNormalTextured(polyVertices(0), Vector3.Cross(polyVertices(0), cnt), 0, 1))
        triangles.Add(New CustomVertex.PositionNormalTextured(cnt, Vector3.Cross(cnt, polyVertices(polyVertices.Count - 1)), 1, 0))
    End Sub
    Public Sub iterate()
        extr = -extr
        If polyVertices.Count > 2 Then
            Dim newTriangles As New List(Of CustomVertex.PositionNormalTextured)
            Dim i As Integer
            For i = 0 To triangles.Count - 1 Step 3
                Dim shp As New List(Of Vector3)
                Dim ii As Integer
                For ii = 0 To 2
                    shp.Add(triangles(i + ii).Position)
                Next
                Dim cnt As Vector3 = cCenter(shp, extr)
                For ii = 0 To 1
                    newTriangles.Add(New CustomVertex.PositionNormalTextured(triangles(i + ii).Position, Vector3.Cross(triangles(i + ii).Position, triangles(i + ii + 1).Position), 0, 0))
                    newTriangles.Add(New CustomVertex.PositionNormalTextured(triangles(i + ii + 1).Position, Vector3.Cross(triangles(i + ii + 1).Position, cnt), 0, 1))
                    newTriangles.Add(New CustomVertex.PositionNormalTextured(cnt, Vector3.Cross(cnt, triangles(i + ii).Position), 1, 0))
                Next
                newTriangles.Add(New CustomVertex.PositionNormalTextured(triangles(i + 2).Position, Vector3.Cross(triangles(i + 2).Position, triangles(i).Position), 0, 0))
                newTriangles.Add(New CustomVertex.PositionNormalTextured(triangles(i).Position, Vector3.Cross(triangles(i).Position, cnt), 0, 1))
                newTriangles.Add(New CustomVertex.PositionNormalTextured(cnt, Vector3.Cross(cnt, triangles(i + 2).Position), 1, 0))
            Next
            triangles.Clear()
            triangles.AddRange(newTriangles)
        End If
    End Sub
    Public Sub nIterate()
        If Me.polyVertices.Count > 2 Then
            Me.triangulatePoly()
            Dim i As Integer
            For i = 0 To Me.iter - 1
                Me.iterate()
            Next
        End If
    End Sub
    Public Sub createMesh(ByVal device As Device)
        Try
            Me.mesh.Dispose()
        Catch ex As Exception

        End Try
        ' RUTINA ZA PRAVLJENJE MESHA
        Dim vvv() As CustomVertex.PositionNormalTextured = Me.triangles.ToArray
        Dim c1 As Integer
        c1 = vvv.Length
        Dim i As Int32
        Dim ibf As New List(Of Int32)
        For i = 0 To c1
            ibf.Add(i)
        Next
        Dim ind() As Int32 = ibf.ToArray

        Me.mesh = New Mesh(ind.Length / 3, c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, cf3D.device)

        Try
            Me.mesh.SetVertexBufferData(vvv, LockFlags.None)
            Me.mesh.SetIndexBufferData(ind, LockFlags.None)
            Me.mesh.ComputeNormals()
            Dim subset() As Integer = Me.mesh.LockAttributeBufferArray(LockFlags.Discard)
            'subset = Me.subset.ToArray
            Me.mesh.UnlockAttributeBuffer(subset)
            ' Optimize.
            Dim adjacency(Me.mesh.NumberFaces * 3 - 1) As Integer
            Me.mesh.GenerateAdjacency(CSng(0.1), adjacency)
            Me.mesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
            'Me.Scena.moMesh = Mesh.TessellateNPatches(Me.Scena.moMesh, adjacency, 1, True)
        Catch ex As Exception
            'console.writeline(ex.Message)
        End Try
    End Sub
    Public Sub refreshBuffer(Optional ByVal device As Device = Nothing)
        Dim m As Matrix = Me.TransformMatrix

        Dim v As New CustomVertex.PositionNormalTextured
        Dim i As Integer
        Me.nIterate()
        For i = 0 To Me.triangles.Count - 1
            v = New CustomVertex.PositionNormalTextured(Vector3.TransformCoordinate(Me.triangles(i).Position, m), Me.triangles(i).Normal, Me.triangles(i).Tu, Me.triangles(i).Tv)
            Me.triangles(i) = v
        Next
        Me.lineBuffer.Clear()
        For i = 0 To Me.triangles.Count - 1 Step 3
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i).Position, lc.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i + 1).Position, lc.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i + 1).Position, lc.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i + 2).Position, lc.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i + 2).Position, lc.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i).Position, lc.ToArgb))
        Next
        Try
            If device IsNot Nothing Then
                Me.createMesh(device)
            Else
                Me.createMesh(Me.mesh.Device)
            End If
        Catch ex As Exception

        End Try
        RaiseEvent bufferRefreshed()
    End Sub
    Public Sub refreshBufferMI()
        Me.refreshBuffer(dev)
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        triangles = New List(Of CustomVertex.PositionNormalTextured)
        lineBuffer = New List(Of CustomVertex.PositionColored)
        refreshBuffer(device)
    End Sub
    Public Enum crackingType As Byte
        NormalExtrude = 0
        ZAxisExtrude = 1
        NonInverseNormalExtrude = 2
        NonInverseZAxisExtrude = 3
    End Enum
End Class
