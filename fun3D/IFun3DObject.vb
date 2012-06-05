Public Interface IFun3DObject
    Event bufferRefreshed()
    Event progressStart()
    Event progressEnd()
    Event progress(ByVal p As Integer, ByVal m As String)

    Property geom As cGeometry
    Property tgeom As cGeometry
    Property transform As cTransform
    Property parent As Object
End Interface
