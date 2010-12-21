Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
<System.Serializable()> _
Public Class ClassBlending
#Region "Non Serialized Fields"
    ''' <summary>Vertex buffer for mesh</summary>
    <System.NonSerialized()> _
    Public vBuffer As New List(Of CustomVertex.PositionNormalTextured)
    ''' <summary>Vertex buffer for mesh dxf export</summary>
    <System.NonSerialized()> _
    Public DXFBuffer As New List(Of CustomVertex.PositionColored)
    ''' <summary>Vertex buffer for mesh edges</summary>
    <System.NonSerialized()> _
    Public LineBuffer As New List(Of CustomVertex.PositionColored)
    ''' <summary>Mesh Direct3D definition</summary>
    <System.NonSerialized()> _
    Public BlendingMesh As Mesh
#End Region

    Dim objName As String = "New Blending Structure"
    Dim objSet As New List(Of Object)
    Dim lpfe As Single = 1
    Dim lpfs As Single = 0
    Public density As Integer = 3
    Public Property Name() As String
        Get
            Return objName
        End Get
        Set(ByVal value As String)
            objName = value
        End Set
    End Property
    Public Property interpolationStart() As Single
        Get
            Return Me.lpfs
        End Get
        Set(ByVal value As Single)
            Me.lpfs = value
        End Set
    End Property
    Public Property interpolationEnd() As Single
        Get
            Return Me.lpfe
        End Get
        Set(ByVal value As Single)
            Me.lpfe = value
        End Set
    End Property
    Public Property blendedObjects() As List(Of Object)
        Get
            Return objSet
        End Get
        Set(ByVal value As List(Of Object))
            objSet = value
        End Set
    End Property
    Public Property blendDensity() As Integer
        Get
            Return Me.density
        End Get
        Set(ByVal value As Integer)
            Me.density = value
        End Set
    End Property

    Public Sub New(ByVal name As String, ByVal device As Device, ByVal blendingObjects As List(Of Object))
        Me.objName = name
        Me.objSet = blendingObjects
        Me.refreshBuffer(device)
    End Sub

    Public Sub refreshBuffer(Optional ByVal device As Device = Nothing)
        LineBuffer.Clear()
        Dim o As Object
        Dim v1, v2 As List(Of Vector3)
        Dim blendingLayerU As New List(Of List(Of Vector3))
        Dim blendedLayers As New List(Of List(Of Vector3))
        For Each o In Me.objSet
            If o.GetType Is GetType(ClassU) Then
                Dim UC As ClassU = o
                blendingLayerU.Add(UC.vectorBuffer)
            End If
        Next
        Dim i As Integer
        Dim d As Single
        For i = 0 To blendingLayerU.Count - 2
            v1 = blendingLayerU(i)
            v2 = blendingLayerU(i + 1)
            For d = lpfs To lpfe Step (lpfe - lpfs) / Me.density
                Dim vectNum As Integer
                Dim v As New List(Of Vector3)
                For vectNum = 0 To v1.Count - 1
                    v.Add(Vector3.Lerp(v1(vectNum), v2(vectNum), d))
                    ' test only
                    LineBuffer.Add(New CustomVertex.PositionColored(Vector3.Lerp(v1(vectNum), v2(vectNum), d), Color.Red.ToArgb))
                Next
                blendedLayers.Add(v)
            Next
        Next
        ' TODO Make all U equal and make generic UV Class for surface
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        vBuffer = New List(Of CustomVertex.PositionNormalTextured)
        LineBuffer = New List(Of CustomVertex.PositionColored)
        DXFBuffer = New List(Of CustomVertex.PositionColored)
        refreshBuffer(device)
    End Sub
End Class
