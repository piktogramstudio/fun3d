Public Interface IFun3DObject
#Region "Events"
    Event bufferRefreshed()
    Event progressStart()
    Event progressEnd()
    Event progress(ByVal p As Integer, ByVal m As String)
#End Region
#Region "Properties"
    Property geom As cGeometry
    Property tgeom As cGeometry
    Property transform As cTransform
    Property parent As Object
#End Region
#Region "Methods"
    Sub draw(ByVal device As Microsoft.DirectX.Direct3D.Device)
#End Region
End Interface
