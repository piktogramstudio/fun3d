Public Interface IFun3DObject
    Event bufferRefreshed()
    Event progressStart()
    Event progressEnd()
    Event progress(ByVal p As Integer, ByVal m As String)

    Property tgeom As cGeometry
End Interface
