''' <summary>
''' Interface for Fun3D objects
''' </summary>
''' <remarks></remarks>
Public Interface IFun3DObject
#Region "Events"
    ''' <summary>
    ''' Geometry description buffer is refreshed
    ''' </summary>
    Event bufferRefreshed()
    ''' <summary>
    ''' Calculation process started
    ''' </summary>
    Event progressStart()
    ''' <summary>
    ''' Calculation process ended
    ''' </summary>
    Event progressEnd()
    ''' <summary>
    ''' Calculation process progress
    ''' </summary>
    ''' <param name="p">Progress percent (0-100)</param>
    ''' <param name="m">Message describing progress</param>
    Event progress(ByVal p As Integer, ByVal m As String)
#End Region
#Region "Properties"
    ''' <summary>
    ''' Object geometry
    ''' </summary>
    ''' <value>Geometry description</value>
    ''' <returns>Geometry description</returns>
    Property geom As cGeometry
    ''' <summary>
    ''' Transformed geometry
    ''' </summary>
    ''' <value>Geometry description</value>
    ''' <returns>Geometry description</returns>
    Property tgeom As cGeometry
    ''' <summary>
    ''' Object transformation
    ''' </summary>
    ''' <value>Transform</value>
    ''' <returns>Transform</returns>
    Property transform As cTransform
    ''' <summary>
    ''' Parent object
    ''' </summary>
    ''' <value>Parent object</value>
    ''' <returns>Parent object</returns>
    ''' <remarks>Parent object is usualy scene object.</remarks>
    Property parent As Object
#End Region
#Region "Methods"
    ''' <summary>
    ''' Draws object using drawing device
    ''' </summary>
    ''' <param name="device">Drawing device</param>
    Sub draw(ByVal device As Microsoft.DirectX.Direct3D.Device)
#End Region
End Interface
