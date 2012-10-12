Imports System.ComponentModel
Imports Microsoft.DirectX
Imports System.Drawing.Design
''' <summary>
''' Base class for Fun3D objects
''' </summary>
''' <remarks></remarks>
<System.Serializable()> _
Public Class ClassFun3DObject
    Implements IFun3DObject
#Region "Events"
    ''' <summary>
    ''' Geometry description buffer is refreshed
    ''' </summary>
    Public Event bufferRefreshed() Implements IFun3DObject.bufferRefreshed
    ''' <summary>
    ''' Calculation process progress
    ''' </summary>
    ''' <param name="p">Progress percent (0-100)</param>
    ''' <param name="m">Message describing progress</param>
    Public Event progress(ByVal p As Integer, ByVal m As String) Implements IFun3DObject.progress
    ''' <summary>
    ''' Calculation process ended
    ''' </summary>
    Public Event progressEnd() Implements IFun3DObject.progressEnd
    ''' <summary>
    ''' Calculation process started
    ''' </summary>
    Public Event progressStart() Implements IFun3DObject.progressStart
    ''' <summary>
    ''' Object geometry
    ''' </summary>
    ''' <value>Geometry description</value>
    ''' <returns>Geometry description</returns>
#End Region
#Region "PROPERTIES"
    <Browsable(False)> _
    Property geom As New cGeometry() Implements IFun3DObject.geom
    ''' <summary>
    ''' Transformed geometry
    ''' </summary>
    ''' <value>Geometry description</value>
    ''' <returns>Geometry description</returns>
    <Browsable(False)> _
    Public Property tgeom As New cGeometry() Implements IFun3DObject.tgeom
    ''' <summary>
    ''' Object transformation
    ''' </summary>
    ''' <value>Transform</value>
    ''' <returns>Transform</returns>
    <Category("5. Transforms"), Editor(GetType(cTransformPropertyEditor), GetType(UITypeEditor)), Description("Affine transformations of the line (rotation, position, scale)" + vbCrLf + "Click on button to open transform tool" + vbCrLf + "For mirror transform use negative scale number")> _
    Public Property transform As New cTransform() Implements IFun3DObject.transform
    ''' <summary>
    ''' Parent object
    ''' </summary>
    ''' <value>Parent object</value>
    ''' <returns>Parent object</returns>
    ''' <remarks>Parent object is usualy scene object.</remarks>
    <Browsable(False)> _
    Public Property parent As Object = Nothing Implements IFun3DObject.parent
#Region "1. Meta"
    ''' <summary>
    ''' Object name
    ''' </summary>
    ''' <value>Name of the Fun3D object</value>
    ''' <returns>Name of the Fun3D object</returns>
    ''' <remarks></remarks>
    <Category("1. Meta")> _
    Public Property Name() As String = "New Fun3D Object"
#End Region
#End Region
#Region "Methods"
    ''' <summary>
    ''' Draws object using drawing device
    ''' </summary>
    ''' <param name="device">Drawing device</param>
    Public Sub draw(ByVal device As Direct3D.Device) Implements IFun3DObject.draw

    End Sub
    ''' <summary>
    ''' Raise bufferRefreshed event
    ''' </summary>
    Public Sub rEventBufferRefreshed()
        RaiseEvent bufferRefreshed()
    End Sub
    ''' <summary>
    ''' Raise progress event
    ''' </summary>
    Public Sub rEventProgress(ByVal p As Integer, ByVal m As String)
        RaiseEvent progress(p, m)
    End Sub
    ''' <summary>
    ''' Raise progressEnd event
    ''' </summary>
    Public Sub rEventProgressEnd()
        RaiseEvent progressEnd()
    End Sub
    ''' <summary>
    ''' Raise progressStart event
    ''' </summary>
    Public Sub rEventProgressStart()
        RaiseEvent progressStart()
    End Sub
#End Region
End Class
