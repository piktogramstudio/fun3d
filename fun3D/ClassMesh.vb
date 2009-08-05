Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
<System.Serializable()> _
Public Class ClassMesh
    Dim meshName As String = "new mesh"
    <System.NonSerialized()> _
    Public mesh As Mesh
    <System.NonSerialized()> _
    Public vbf As New List(Of CustomVertex.PositionNormalTextured)
    Public vbfo As New List(Of Vector3)
    Public ibf As New List(Of Integer)
    Public c As Color = Drawing.Color.Red
    Public t As Byte = 255
    Private scX As Single = 1
    Private scY As Single = 1
    Private scZ As Single = 1
    Private xpol As Single = 0
    Private ypol As Single = 0
    Private zpol As Single = 0
    Dim xrot As Single = 0
    Dim yrot As Single = 0
    Dim zrot As Single = 0

    Public Shared Event bufferRefreshed()
    <Category("1. Meta")> _
    Public Property Name() As String
        Get
            Return Me.meshName
        End Get
        Set(ByVal value As String)
            Me.meshName = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property color() As Color
        Get
            Return Me.c
        End Get
        Set(ByVal value As Color)
            Me.c = value
        End Set
    End Property
    <Category("2. Appearance")> _
    Public Property transparency() As Byte
        Get
            Return Me.t
        End Get
        Set(ByVal value As Byte)
            Me.t = value
        End Set
    End Property

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

    Public Sub createMesh(ByVal device As Device)
        Try
            Me.mesh.Dispose()
        Catch ex As Exception

        End Try
        ' RUTINA ZA PRAVLJENJE MESHA
        Dim vvv() As CustomVertex.PositionNormalTextured = vbf.ToArray
        Dim c1 As Integer
        c1 = vvv.Length

        Dim ind() As Int32 = ibf.ToArray

        Me.mesh = New Mesh(ind.Length / 3, c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, cf3D.device)

        Try
            Me.mesh.SetVertexBufferData(vvv, LockFlags.None)
            Me.mesh.SetIndexBufferData(ind, LockFlags.None)
            Me.mesh.ComputeNormals()
            Dim subset() As Integer = Me.mesh.LockAttributeBufferArray(LockFlags.Discard)
            'subset = Me.subset.ToArray
            Me.mesh.UnlockAttributeBuffer(subset)
            ' Optimize.
            Dim adjacency(Me.mesh.NumberFaces * 3 - 1) As Integer
            Me.mesh.GenerateAdjacency(CSng(0.1), adjacency)
            Me.mesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
            'Me.Scena.moMesh = Mesh.TessellateNPatches(Me.Scena.moMesh, adjacency, 1, True)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub refreshBuffer(Optional ByVal device As Device = Nothing)
        ' transformation matrix
        Dim m, mm As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Translate(Me.xPolozaj, Me.yPolozaj, Me.zPolozaj)
        m = Matrix.Multiply(mm, m)
        mm.Scale(Me.scX, Me.scY, Me.scZ)
        m = Matrix.Multiply(mm, m)
        mm.RotateX(Me.xRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        mm.RotateY(Me.yRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        mm.RotateZ(Me.zRotation * Math.PI / 180)
        m = Matrix.Multiply(mm, m)
        
        Dim v As New CustomVertex.PositionNormalTextured
        Dim i As Integer
        Me.vbf.Clear()
        For i = 0 To Me.vbfo.Count - 1
            v.Position = Vector3.TransformCoordinate(Me.vbfo(i), m)
            Me.vbf.Add(v)
        Next
        Try
            If device IsNot Nothing Then
                Me.createMesh(device)
            Else
                Me.createMesh(Me.mesh.Device)
            End If
        Catch ex As Exception

        End Try
        RaiseEvent bufferRefreshed()
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        Me.vbf = New List(Of CustomVertex.PositionNormalTextured)
        refreshBuffer(device)
    End Sub
End Class
