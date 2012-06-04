Imports Microsoft.DirectX
Imports System.ComponentModel
''' <summary>
''' Class used for transforming (affine transformations), applying to geometry or using generated transform matrix
''' </summary>
''' <remarks>All transformations uses origin as center of transformations.</remarks>
<System.Serializable()> _
Public Class cTransform
    ''' <summary>
    ''' X translation
    ''' </summary>
    ''' <value>Translation along X axis</value>
    ''' <returns>Translation along X axis</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property tx As Single = 0
    ''' <summary>
    ''' Y translation
    ''' </summary>
    ''' <value>Translation along Y axis</value>
    ''' <returns>Translation along Y axis</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property ty As Single = 0
    ''' <summary>
    ''' Z translation
    ''' </summary>
    ''' <value>Translation along Z axis</value>
    ''' <returns>Translation along Z axis</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property tz As Single = 0
    ''' <summary>
    ''' Rotation around X axis
    ''' </summary>
    ''' <value>Rotation around X axis in degrees</value>
    ''' <returns>Rotation around X axis in degrees</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property rx As Single = 0
    ''' <summary>
    ''' Rotation around Y axis
    ''' </summary>
    ''' <value>Rotation around Y axis in degrees</value>
    ''' <returns>Rotation around Y axis in degrees</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property ry As Single = 0
    ''' <summary>
    ''' Rotation around Z axis
    ''' </summary>
    ''' <value>Rotation around Z axis in degrees</value>
    ''' <returns>Rotation around Z axis in degrees</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property rz As Single = 0
    ''' <summary>
    ''' X scale
    ''' </summary>
    ''' <value>Scale along X axis</value>
    ''' <returns>Scale along X axis</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property sx As Single = 1
    ''' <summary>
    ''' Y scale
    ''' </summary>
    ''' <value>Scale along Y axis</value>
    ''' <returns>Scale along Y axis</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property sy As Single = 1
    ''' <summary>
    ''' Z scale
    ''' </summary>
    ''' <value>Scale along Z axis</value>
    ''' <returns>Scale along Z axis</returns>
    ''' <remarks></remarks>
    <Bindable(True)> _
    Public Property sz As Single = 1
    ''' <summary>
    ''' Creates transformed geometry
    ''' </summary>
    ''' <param name="geom">Geometry to transform</param>
    ''' <returns>Transformed geometry</returns>
    ''' <remarks></remarks>
    Public Function getTransformedGeometry(ByVal geom As cGeometry) As cGeometry
        Dim rv As New cGeometry()
        Dim i As Integer
        rv.setGeometry(geom)
        Dim m As Matrix = Me.getTransformMatrix()
        For i = 0 To rv.vb.Length - 1
            rv.vb(i) = Vector3.TransformCoordinate(rv.vb(i), m)
        Next
        Try
            If rv.ncwb IsNot Nothing Then
                For i = 0 To rv.ncwb.Length - 1
                    rv.ncwb(i) = Vector3.TransformCoordinate(rv.ncwb(i), m)
                Next
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Try
            If rv.nccwb IsNot Nothing Then
                For i = 0 To rv.nccwb.Length - 1
                    rv.nccwb(i) = Vector3.TransformCoordinate(rv.nccwb(i), m)
                Next
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return rv
    End Function
    ''' <summary>
    ''' Creates transformed matrix
    ''' </summary>
    ''' <returns>Transformed matrix</returns>
    ''' <remarks></remarks>
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
