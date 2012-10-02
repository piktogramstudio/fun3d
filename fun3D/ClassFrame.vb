''' <summary>
''' Describes key frame for animation
''' </summary>
''' <remarks></remarks>
<System.Serializable()> _
Public Class ClassFrame
    ''' <summary>
    ''' Camera rotation around x axis
    ''' </summary>
    Public ax As Single
    ''' <summary>
    ''' Camera rotation around y axis
    ''' </summary>
    Public ay As Single
    ''' <summary>
    ''' Camera rotation around z axis
    ''' </summary>
    Public az As Single
    ''' <summary>
    ''' Camera X position
    ''' </summary>
    Public xc As Single
    ''' <summary>
    ''' Camera Y position
    ''' </summary>
    Public yc As Single
    ''' <summary>
    ''' Camera Z position
    ''' </summary>
    Public zc As Single
    Dim ime As String = "NewFrame"
    ''' <summary>
    ''' Creates frame with current camera angle and position
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ax = cf3D.angleX
        ay = cf3D.angleY
        az = cf3D.angleZ
        xc = cf3D.xcam
        yc = cf3D.ycam
        zc = cf3D.zcam
    End Sub
    ''' <summary>
    ''' Creates frame with given angle and position
    ''' </summary>
    ''' <param name="anglex">Rotation around x axis</param>
    ''' <param name="angley">Rotation around y axis</param>
    ''' <param name="anglez">Rotation around z axis</param>
    ''' <param name="xcamera">X position</param>
    ''' <param name="ycamera">Y position</param>
    ''' <param name="zcamera">Z position</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal anglex As Single, ByVal angley As Single, ByVal anglez As Single, ByVal xcamera As Single, ByVal ycamera As Single, ByVal zcamera As Single)
        ax = anglex
        ay = angley
        az = anglez
        xc = xcamera
        yc = ycamera
        zc = zcamera
    End Sub
    ''' <summary>
    ''' Frame name (label)
    ''' </summary>
    ''' <value>Name for frame</value>
    ''' <returns>Name of frame</returns>
    ''' <remarks></remarks>
    Public Property Name() As String
        Get
            Return Me.ime
        End Get
        Set(ByVal value As String)
            Me.ime = value
        End Set
    End Property
End Class
