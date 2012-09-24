Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
''' <summary>
''' Cracked Poly is mesh created using modified "Recepie for Cracking" from "Pamphlet Architecture 27 - Tooling" by ARANDA/LASCH http://issuu.com/papress/docs/9781568985473/1
''' </summary>
<System.Serializable()> _
Public Class ClassCrackedPoly
    Inherits ClassFun3DObject
#Region "Non Serialized Fields"
    ''' <summary>
    ''' DirectX mesh of cracking structure
    ''' </summary>
    <System.NonSerialized()> _
    Public mesh As Mesh
    ''' <summary>
    ''' Triangles of cracking structure
    ''' </summary>
    <System.NonSerialized()> _
    Public triangles As New List(Of CustomVertex.PositionNormalTextured)
    ''' <summary>
    ''' Line buffer
    ''' </summary>
    <System.NonSerialized()> _
    Public lineBuffer As New List(Of CustomVertex.PositionColored)
    ''' <summary>
    ''' Drawing device
    ''' </summary>
    <System.NonSerialized()> _
    Dim dev As Device = Nothing
#End Region
#Region "Fields"
    ''' <summary>
    ''' Amount of extrusion
    ''' </summary>
    Dim extr As Single = 0.4
    ''' <summary>
    ''' Extrusion central point
    ''' </summary>
    Dim cExtr As Single = 2
    ''' <summary>
    ''' Number of iterations
    ''' </summary>
    Dim iter As Integer = 2
#End Region
#Region "Properties"
    ''' <summary>
    ''' Edges color
    ''' </summary>
    ''' <value>Color of edges</value>
    ''' <returns>Color of edges</returns>
    <Category("2. Appearance")> _
    Public Property LineColor() As Color = Drawing.Color.Black
    ''' <summary>
    ''' Faces color
    ''' </summary>
    ''' <value>Color of faces</value>
    ''' <returns>Color of faces</returns>
    <Category("2. Appearance")> _
    Public Property color() As Color = Drawing.Color.Red
    ''' <summary>
    ''' Transparency of cracking structure
    ''' </summary>
    ''' <value>Value between 0 and 255 (0 = transparent, 255 = opaque)</value>
    ''' <returns>Value between 0 and 255 (0 = transparent, 255 = opaque)</returns>
    <Category("2. Appearance")> _
    Public Property transparency() As Byte = 255
    ''' <summary>
    ''' Number of iterations
    ''' </summary>
    ''' <value>Positive number</value>
    ''' <returns>Number of iterations</returns>
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
    ''' <summary>
    ''' Extrusion of central point
    ''' </summary>
    ''' <value>Single unit value</value>
    ''' <returns>Single unit value</returns>
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
    ''' <summary>
    ''' Extrusion for each iteration
    ''' </summary>
    ''' <value>Extrusion</value>
    ''' <returns>Extrusion</returns>
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
    ''' <summary>
    ''' Points of cracked polygon
    ''' </summary>
    ''' <value>List of polygon coordinates</value>
    ''' <returns>List of polygon coordinates</returns>
    <Category("2. Appearance")> _
    Public Property PolygonPoints() As List(Of Vector3) = New List(Of Vector3)
    ''' <summary>
    ''' Cracking type
    ''' </summary>
    ''' <value>Cracking type</value>
    ''' <returns>Cracking type</returns>
    <Category("2. Appearance")> _
    Public Property TypeOfCracking() As crackingType = crackingType.NormalExtrude
    ''' <summary>
    ''' Editable points of cracked polygon
    ''' </summary>
    ''' <value>Polygon coordinates</value>
    ''' <returns>Polygon coordinates</returns>
    <Category("2. Appearance")> _
    Public Property edgePoints() As String()
        Get
            Dim rv() As String
            ReDim rv(Me.PolygonPoints.Count - 1)
            Dim i As Integer
            For i = 0 To Me.PolygonPoints.Count - 1
                rv(i) = Me.PolygonPoints(i).X.ToString + ":" + Me.PolygonPoints(i).Y.ToString + ":" + Me.PolygonPoints(i).Z.ToString
            Next
            Return rv
        End Get
        Set(ByVal value As String())
            Me.PolygonPoints.Clear()
            Dim i As Integer
            Dim s() As String
            For i = 0 To value.Length - 1
                s = value(i).Split(CChar(":"))
                Me.PolygonPoints.Add(New Vector3(CSng(Val(s(0))), CSng(Val(s(1))), CSng(Val(s(2)))))
            Next
        End Set
    End Property
#End Region
#Region "Constructors"
    ''' <summary>
    ''' Creates cracked poly structure with default polygon points
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me.Name = "New cracked poly"
        Me.PolygonPoints.Add(New Vector3(0, 0, 0))
        Me.PolygonPoints.Add(New Vector3(10, 0, 0))
        Me.PolygonPoints.Add(New Vector3(15, 5, 0))
        Me.PolygonPoints.Add(New Vector3(10, 10, 0))
        Me.PolygonPoints.Add(New Vector3(0, 10, 0))
        Me.PolygonPoints.Add(New Vector3(-5, 5, 0))
        Me.refreshBuffer()
    End Sub
    ''' <summary>
    ''' Creates cracked poly structure for specified device
    ''' </summary>
    ''' <param name="Name">Object name</param>
    ''' <param name="device">Drawing device</param>
    Public Sub New(ByVal Name As String, ByVal device As Device)
        Me.Name = Name
        Me.refreshBuffer(device)
        dev = device
    End Sub
#End Region
#Region "Methods"
    ''' <summary>
    ''' Determines the center of polygon extruded by extrusion value
    ''' </summary>
    ''' <param name="shp">Polygon points</param>
    ''' <param name="extr">Extrusion</param>
    ''' <returns>Center of polygon</returns>
    ''' <remarks>Center position also depends on cracking type</remarks>
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
            Select Case TypeOfCracking
                Case crackingType.NormalExtrude
                    rv1 = Vector3.Cross(shp(1) - shp(0), shp(2) - shp(0))
                    rv1.Normalize()
                    rv1.Multiply(extr)
                    rv.Multiply(CSng(1 / shp.Count))
                    rv = Vector3.TransformCoordinate(rv1, Matrix.Translation(rv))
                Case crackingType.ZAxisExtrude
                    rv.Multiply(CSng(1 / shp.Count))
                    rv.Z = extr
                Case crackingType.NonInverseNormalExtrude
                    rv1 = Vector3.Cross(shp(1) - shp(0), shp(2) - shp(0))
                    rv1.Normalize()
                    rv1.Multiply(Math.Abs(extr))
                    rv.Multiply(CSng(1 / shp.Count))
                    rv = Vector3.TransformCoordinate(rv1, Matrix.Translation(rv))
                Case crackingType.NonInverseZAxisExtrude
                    rv.Multiply(CSng(1 / shp.Count))
                    rv.Z = Math.Abs(extr)
            End Select
        End If
        Return rv
    End Function
    ''' <summary>
    ''' Triangulates cracked polygon structure
    ''' </summary>
    Public Sub triangulatePoly()
        Dim cnt As Vector3 = cCenter(PolygonPoints, cExtr)
        Dim i As Integer
        triangles.Clear()
        For i = 0 To PolygonPoints.Count - 2
            triangles.Add(New CustomVertex.PositionNormalTextured(PolygonPoints(i), Vector3.Cross(PolygonPoints(i), PolygonPoints(i + 1)), 0, 0))
            triangles.Add(New CustomVertex.PositionNormalTextured(PolygonPoints(i + 1), Vector3.Cross(PolygonPoints(i + 1), cnt), 0, 1))
            triangles.Add(New CustomVertex.PositionNormalTextured(cnt, Vector3.Cross(cnt, PolygonPoints(i)), 1, 0))
        Next
        triangles.Add(New CustomVertex.PositionNormalTextured(PolygonPoints(PolygonPoints.Count - 1), Vector3.Cross(PolygonPoints(PolygonPoints.Count - 1), PolygonPoints(0)), 0, 0))
        triangles.Add(New CustomVertex.PositionNormalTextured(PolygonPoints(0), Vector3.Cross(PolygonPoints(0), cnt), 0, 1))
        triangles.Add(New CustomVertex.PositionNormalTextured(cnt, Vector3.Cross(cnt, PolygonPoints(PolygonPoints.Count - 1)), 1, 0))
    End Sub
    ''' <summary>
    ''' One iteration
    ''' </summary>
    Public Sub iterate()
        extr = -extr
        If PolygonPoints.Count > 2 Then
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
    ''' <summary>
    ''' Iterate for given iterations number
    ''' </summary>
    Public Sub nIterate()
        If Me.PolygonPoints.Count > 2 Then
            Me.triangulatePoly()
            Dim i As Integer
            For i = 0 To Me.iter - 1
                Me.iterate()
            Next
        End If
    End Sub
    ''' <summary>
    ''' Creates DirectX mesh definition
    ''' </summary>
    ''' <param name="device">3D device</param>
    ''' <remarks></remarks>
    Public Sub createMesh(ByVal device As Device)
        Try
            Me.mesh.Dispose()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
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
        Me.mesh = New Mesh(CInt(ind.Length / 3), c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, cf3D.device)
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
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Recreates a geometry of object
    ''' </summary>
    ''' <param name="device">3D device</param>
    ''' <remarks></remarks>
    Public Sub refreshBuffer(Optional ByVal device As Device = Nothing)
        Dim m As Matrix = Me.transform.getTransformMatrix()
        Dim v As New CustomVertex.PositionNormalTextured
        Dim i As Integer
        Me.nIterate()
        For i = 0 To Me.triangles.Count - 1
            v = New CustomVertex.PositionNormalTextured(Vector3.TransformCoordinate(Me.triangles(i).Position, m), Me.triangles(i).Normal, Me.triangles(i).Tu, Me.triangles(i).Tv)
            Me.triangles(i) = v
        Next
        Me.lineBuffer.Clear()
        For i = 0 To Me.triangles.Count - 1 Step 3
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i).Position, Me.LineColor.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i + 1).Position, Me.LineColor.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i + 1).Position, Me.LineColor.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i + 2).Position, Me.LineColor.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i + 2).Position, Me.LineColor.ToArgb))
            Me.lineBuffer.Add(New CustomVertex.PositionColored(Me.triangles(i).Position, Me.LineColor.ToArgb))
        Next
        Try
            If device IsNot Nothing Then
                Me.createMesh(device)
            Else
                Me.createMesh(Me.mesh.Device)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        rEventBufferRefreshed()
    End Sub
    ''' <summary>
    ''' Recreates a geometry of object in creating mode
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub refreshBufferMI()
        Me.refreshBuffer(dev)
    End Sub
    ''' <summary>
    ''' Used for Undo/Redo operations
    ''' </summary>
    ''' <param name="device">3D device</param>
    ''' <remarks></remarks>
    Public Sub afterPaste(ByVal device As Device)
        triangles = New List(Of CustomVertex.PositionNormalTextured)
        lineBuffer = New List(Of CustomVertex.PositionColored)
        refreshBuffer(device)
    End Sub
    ''' <summary>
    ''' The type of cracking extrusion
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum crackingType As Byte
        NormalExtrude = 0
        ZAxisExtrude = 1
        NonInverseNormalExtrude = 2
        NonInverseZAxisExtrude = 3
    End Enum
#End Region
End Class
