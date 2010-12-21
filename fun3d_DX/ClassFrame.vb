<System.Serializable()> _
Public Class ClassFrame
    Public ax, ay, az, xc, yc, zc As Single
    Dim ime As String = "NewFrame"
    Public Sub New()
        ax = cf3D.angleX
        ay = cf3D.angleY
        az = cf3D.angleZ
        xc = cf3D.xcam
        yc = cf3D.ycam
        zc = cf3D.zcam
    End Sub
    Public Sub New(ByVal anglex As Single, ByVal angley As Single, ByVal anglez As Single, ByVal xcamera As Single, ByVal ycamera As Single, ByVal zcamera As Single)
        ax = anglex
        ay = angley
        az = anglez
        xc = xcamera
        yc = ycamera
        zc = zcamera
    End Sub
    Public Property Name() As String
        Get
            Return Me.ime
        End Get
        Set(ByVal value As String)
            Me.ime = value
        End Set
    End Property
End Class
