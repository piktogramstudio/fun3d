Imports Microsoft.DirectX

' TODO Example for cGeometry class
' TODO Better error handling

''' <summary>
''' Geometry description of object
''' </summary>
''' <remarks>
''' Use this class to store geometry properties of object
''' </remarks>
<System.Serializable()> _
Public Class cGeometry
    ''' <summary>
    ''' Occurs if any of geometry properties is changed
    ''' </summary>
    ''' <remarks></remarks>
    Public Event geometryChanged()
    ''' <summary>
    ''' Vertex buffer
    ''' </summary>
    ''' <remarks></remarks>
    Public vb() As Vector3
    ''' <summary>
    ''' Normals buffer (clock wise)
    ''' </summary>
    ''' <remarks></remarks>
    Public ncwb() As Vector3
    ''' <summary>
    ''' Normals buffer (counter clock wise)
    ''' </summary>
    ''' <remarks></remarks>
    Public nccwb() As Vector3
    ''' <summary>
    ''' Indices buffer
    ''' </summary>
    ''' <remarks></remarks>
    Public ib() As Int32
    ''' <summary>
    ''' Edges buffer
    ''' </summary>
    ''' <remarks></remarks>
    Public eb() As Int32
    ''' <summary>
    ''' Set the geometry information (geometry object with faces)
    ''' </summary>
    ''' <param name="vb">Vertex buffer</param>
    ''' <param name="ib">Indices buffer</param>
    ''' <param name="eb">Edges buffer</param>
    ''' <remarks></remarks>
    Public Sub setGeometry(ByVal vb() As Vector3, ByVal ib() As Int32, ByVal eb() As Int32)
        Me.vb = vb
        Me.ncwb = Me.calculateNormals(vb, ib, Direct3D.Cull.Clockwise)
        Me.nccwb = Me.calculateNormals(vb, ib, Direct3D.Cull.CounterClockwise)
        Me.ib = ib
        Me.eb = eb
        RaiseEvent geometryChanged()
    End Sub
    ''' <summary>
    ''' Set the geometry information from geometry object
    ''' </summary>
    ''' <param name="geom">Geometry object with geometry informations</param>
    ''' <remarks></remarks>
    Public Sub setGeometry(ByVal geom As cGeometry)
        Dim i As Integer
        ReDim Me.vb(geom.vb.Length - 1)
        ReDim Me.eb(geom.eb.Length - 1)
        For i = 0 To geom.vb.Length - 1
            Me.vb(i) = geom.vb(i)
        Next
        Try
            If geom.ncwb IsNot Nothing Then
                ReDim Me.ncwb(geom.ncwb.Length - 1)
                For i = 0 To geom.ncwb.Length - 1
                    Me.ncwb(i) = geom.ncwb(i)
                Next
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Try
            If geom.nccwb IsNot Nothing Then
                ReDim Me.nccwb(geom.nccwb.Length - 1)
                For i = 0 To geom.nccwb.Length - 1
                    Me.nccwb(i) = geom.nccwb(i)
                Next
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Try
            If geom.ib IsNot Nothing Then
                ReDim Me.ib(geom.ib.Length - 1)
                For i = 0 To geom.ib.Length - 1
                    Me.ib(i) = geom.ib(i)
                Next
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        For i = 0 To geom.eb.Length - 1
            Me.eb(i) = geom.eb(i)
        Next
        RaiseEvent geometryChanged()
    End Sub
    ''' <summary>
    ''' Set polyline geometry information
    ''' </summary>
    ''' <param name="vb">Vertex buffer</param>
    ''' <param name="closed">True if polyline is closed</param>
    ''' <remarks></remarks>
    Public Sub setPolyline(ByVal vb() As Vector3, ByVal closed As Boolean)
        Me.vb = vb
        Me.eb = Me.getPolylineEdgesFromPointList(vb, closed)
        RaiseEvent geometryChanged()
    End Sub
    ''' <summary>
    ''' Calculate normals for front faces
    ''' </summary>
    ''' <param name="vb">Vertex buffer</param>
    ''' <param name="ib">Indices buffer</param>
    ''' <param name="culling">Normal direction</param>
    ''' <returns>Returns normals buffer</returns>
    ''' <remarks></remarks>
    Public Function calculateNormals(ByVal vb() As Vector3, ByVal ib() As Int32, culling As Direct3D.Cull) As Vector3()
        Dim rv As New List(Of Vector3)
        Dim i As Int32
        Dim v, v1, v2, v3 As Vector3
        For i = 0 To ib.Length - 1 Step 3
            If culling = Direct3D.Cull.Clockwise Then
                v1 = vb(ib(i))
                v2 = vb(ib(i + 1))
                v3 = vb(ib(i + 2))
            Else
                v1 = vb(ib(i + 2))
                v2 = vb(ib(i + 1))
                v3 = vb(ib(i))
            End If
            v = Vector3.Cross(v2 - v1, v3 - v1)
            v.Normalize()
            rv.Add(v)
            v = Vector3.Cross(v3 - v2, v1 - v2)
            v.Normalize()
            rv.Add(v)
            v = Vector3.Cross(v1 - v3, v2 - v3)
            v.Normalize()
            rv.Add(v)
        Next
        Return rv.ToArray
    End Function
    ''' <summary>
    ''' Edges buffer for polyline
    ''' </summary>
    ''' <param name="vb">Vertex buffer</param>
    ''' <param name="closed">True if polyline is closed</param>
    ''' <returns>Returns edges buffer</returns>
    ''' <remarks></remarks>
    Public Function getPolylineEdgesFromPointList(ByVal vb() As Vector3, ByVal closed As Boolean) As Int32()
        Dim rv As New List(Of Int32)
        Dim i As Integer
        For i = 0 To vb.Length - 2
            rv.Add(i)
            rv.Add(i + 1)
        Next
        If closed Then
            rv.Add(vb.Length - 1)
            rv.Add(0)
        End If
        Return rv.ToArray
    End Function
End Class
