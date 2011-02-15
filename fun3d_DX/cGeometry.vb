Imports Microsoft.DirectX
Public Structure cGeometry
    Public Event geometryChanged()
    Public vb() As Vector3
    Public nb() As Vector3
    Public ib() As Int32
    Public eb() As Int32
    Public Sub setGeometry(ByVal vb() As Vector3, ByVal ib() As Int32, ByVal eb() As Int32)
        Me.vb = vb
        Me.nb = Me.calculateNormals(vb, ib)
        Me.ib = ib
        Me.eb = eb
        RaiseEvent geometryChanged()
    End Sub
    Public Function calculateNormals(ByVal vb() As Vector3, ByVal ib() As Int32) As Vector3()
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
End Structure
