Imports Microsoft.DirectX
<System.Serializable()> _
Public Class cGeometry
    Public Event geometryChanged()
    Public vb() As Vector3
    Public ncwb() As Vector3
    Public nccwb() As Vector3
    Public ib() As Int32
    Public eb() As Int32
    Public Sub setGeometry(ByVal vb() As Vector3, ByVal ib() As Int32, ByVal eb() As Int32)
        Me.vb = vb
        Me.ncwb = Me.calculateNormalsClockWise(vb, ib)
        Me.nccwb = Me.calculateNormalsClockWise(vb, ib)
        Me.ib = ib
        Me.eb = eb
        RaiseEvent geometryChanged()
    End Sub
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
    Public Sub setPolyline(ByVal vb() As Vector3, ByVal closed As Boolean)
        Me.vb = vb
        Me.eb = Me.getPolylineEdgesFromPointList(vb, closed)
        RaiseEvent geometryChanged()
    End Sub
    Public Function calculateNormalsClockWise(ByVal vb() As Vector3, ByVal ib() As Int32) As Vector3()
        Dim rv As New List(Of Vector3)
        Dim i As Int32
        Dim v, v1, v2, v3 As Vector3
        For i = 0 To ib.Length - 1 Step 3
            v1 = vb(ib(i))
            v2 = vb(ib(i + 1))
            v3 = vb(ib(i + 2))
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
    Public Function calculateNormalsCounterClockWise(ByVal vb() As Vector3, ByVal ib() As Int32) As Vector3()
        Dim rv As New List(Of Vector3)
        Dim i As Int32
        Dim v, v1, v2, v3 As Vector3
        For i = 0 To ib.Length - 1 Step 3
            v1 = vb(ib(i + 2))
            v2 = vb(ib(i + 1))
            v3 = vb(ib(i))
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
