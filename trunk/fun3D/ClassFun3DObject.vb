Imports System.ComponentModel
Imports Microsoft.DirectX
Imports System.Drawing.Design
<System.Serializable()> _
Public Class ClassFun3DObject
    Implements IFun3DObject

    Public Event bufferRefreshed() Implements IFun3DObject.bufferRefreshed
    Public Event progress(ByVal p As Integer, ByVal m As String) Implements IFun3DObject.progress
    Public Event progressEnd() Implements IFun3DObject.progressEnd
    Public Event progressStart() Implements IFun3DObject.progressStart
    <Browsable(False)> _
    Property geom As New cGeometry() Implements IFun3DObject.geom
    <Browsable(False)> _
    Public Property tgeom As New cGeometry() Implements IFun3DObject.tgeom

    <Category("5. Transforms"), Editor(GetType(cTransformPropertyEditor), GetType(UITypeEditor)), Description("Affine transformations of the line (rotation, position, scale)" + vbCrLf + "Click on button to open transform tool" + vbCrLf + "For mirror transform use negative scale number")> _
    Public Property transform As New cTransform() Implements IFun3DObject.transform
    <Browsable(False)> _
    Public Property parent As Object = Nothing Implements IFun3DObject.parent
#Region "Private fields"
    Dim objName As String = "New Fun3D Object"
#End Region
#Region "PROPERTIES"
#Region "1. Meta"
    <Category("1. Meta")> _
        Public Property Name() As String
        Get
            Return objName
        End Get
        Set(ByVal value As String)
            objName = value
        End Set
    End Property
#End Region
#End Region
    Public Sub draw(ByVal device As Direct3D.Device) Implements IFun3DObject.draw

    End Sub
    Public Sub rEventBufferRefreshed()
        RaiseEvent bufferRefreshed()
    End Sub
    Public Sub rEventProgress(ByVal p As Integer, ByVal m As String)
        RaiseEvent progress(p, m)
    End Sub
    Public Sub rEventProgressEnd()
        RaiseEvent progressEnd()
    End Sub
    Public Sub rEventProgressStart()
        RaiseEvent progressStart()
    End Sub
End Class
