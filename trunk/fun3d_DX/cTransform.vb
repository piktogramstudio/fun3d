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

        Return rv
    End Function
End Class
