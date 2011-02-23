Imports Microsoft.DirectX
<System.Serializable()> _
Public Class cTransform
    Public Property tx As Single = 0
    Public Property ty As Single = 0
    Public Property tz As Single = 0
    Public Property rx As Single = 0
    Public Property ry As Single = 0
    Public Property rz As Single = 0
    Public Property sx As Single = 1
    Public Property sy As Single = 1
    Public Property sz As Single = 1
    Public Function getTransformedGeometry(ByVal geom As cGeometry) As cGeometry
        Dim rv As New cGeometry()
        Dim i As Integer
        rv.setGeometry(geom)
        Dim m As Matrix = Me.getTransformMatrix()
        For i = 0 To rv.vb.Length - 1
            rv.vb(i) = Vector3.TransformCoordinate(rv.vb(i), m)
        Next
        Try
            For i = 0 To rv.nb.Length - 1
                rv.nb(i) = Vector3.TransformCoordinate(rv.vb(i), m)
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return rv
    End Function
    Public Function getTransformMatrix() As Matrix
        Dim m, mm As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Scale(Me.sx, Me.sy, Me.sz)
        m = Matrix.Multiply(mm, m)
        mm.RotateX(Direct3D.Geometry.DegreeToRadian(Me.rx))
        m = Matrix.Multiply(mm, m)
        mm.RotateY(Direct3D.Geometry.DegreeToRadian(Me.ry))
        m = Matrix.Multiply(mm, m)
        mm.RotateZ(Direct3D.Geometry.DegreeToRadian(Me.rz))
        m = Matrix.Multiply(mm, m)
        mm.Translate(Me.tx, Me.ty, Me.tz)
        m = Matrix.Multiply(mm, m)
        Return m
    End Function
End Class
