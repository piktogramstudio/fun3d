Imports System.ComponentModel
Imports Microsoft.DirectX
<System.Serializable()> _
Public Class ClassFun3DObject
    Implements IFun3DObject

    Public Event bufferRefreshed() Implements IFun3DObject.bufferRefreshed
    Public Event progress(ByVal p As Integer, ByVal m As String) Implements IFun3DObject.progress
    Public Event progressEnd() Implements IFun3DObject.progressEnd
    Public Event progressStart() Implements IFun3DObject.progressStart

    Public Property tgeom As New cGeometry() Implements IFun3DObject.tgeom

    Public Property parent As Object = Nothing Implements IFun3DObject.parent
#Region "Prifate fields"
    Dim objName As String = "New Fun3D Object"
    Dim scX As Single = 1
    Dim scY As Single = 1
    Dim scZ As Single = 1
    Dim xpol As Single = 0
    Dim ypol As Single = 0
    Dim zpol As Single = 0
    Dim xrot As Single = 0
    Dim yrot As Single = 0
    Dim zrot As Single = 0
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
#Region "3. Position"
    <Category("3. Position"), DisplayName("X Position")> _
            Public Property xPolozaj() As Single
        Get
            Return Me.xpol
        End Get
        Set(ByVal value As Single)
            Me.xpol = value
        End Set
    End Property
    <Category("3. Position"), DisplayName("Y Position")> _
    Public Property yPolozaj() As Single
        Get
            Return Me.ypol
        End Get
        Set(ByVal value As Single)
            Me.ypol = value
        End Set
    End Property
    <Category("3. Position"), DisplayName("Z Position")> _
    Public Property zPolozaj() As Single
        Get
            Return Me.zpol
        End Get
        Set(ByVal value As Single)
            Me.zpol = value
        End Set
    End Property
#End Region
#Region "4. Rotation"
    <Category("4. Rotation")> _
        Public Property xRotation() As Single
        Get
            Return Me.xrot
        End Get
        Set(ByVal value As Single)
            Me.xrot = value
        End Set
    End Property
    <Category("4. Rotation")> _
    Public Property yRotation() As Single
        Get
            Return Me.yrot
        End Get
        Set(ByVal value As Single)
            Me.yrot = value
        End Set
    End Property
    <Category("4. Rotation")> _
    Public Property zRotation() As Single
        Get
            Return Me.zrot
        End Get
        Set(ByVal value As Single)
            Me.zrot = value
        End Set
    End Property
#End Region
#Region "5. Scale"
    <Category("5. Scale")> _
    Public Property scaleX() As Single
        Get
            Return Me.scX
        End Get
        Set(ByVal value As Single)
            Me.scX = value
        End Set
    End Property
    <Category("5. Scale")> _
    Public Property scaleY() As Single
        Get
            Return Me.scY
        End Get
        Set(ByVal value As Single)
            Me.scY = value
        End Set
    End Property
    <Category("5. Scale")> _
    Public Property scaleZ() As Single
        Get
            Return Me.scZ
        End Get
        Set(ByVal value As Single)
            Me.scZ = value
        End Set
    End Property
#End Region
    Public ReadOnly Property TransformMatrix() As Matrix
        Get
            ' transformation matrix
            Dim m, mm As New Matrix
            m = Matrix.RotationYawPitchRoll(0, 0, 0)
            mm.Translate(Me.xPolozaj, Me.yPolozaj, Me.zPolozaj)
            m = Matrix.Multiply(mm, m)
            mm.Scale(Me.scaleX, Me.scaleY, Me.scaleZ)
            m = Matrix.Multiply(mm, m)
            mm.RotateX(CSng(Me.xRotation * Math.PI / 180))
            m = Matrix.Multiply(mm, m)
            mm.RotateY(CSng(Me.yRotation * Math.PI / 180))
            m = Matrix.Multiply(mm, m)
            mm.RotateZ(CSng(Me.zRotation * Math.PI / 180))
            m = Matrix.Multiply(mm, m)
            Return m
        End Get
    End Property
#End Region
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
