Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
<System.Serializable()> _
Public Class ClassPacking
    Inherits ClassFun3DObject

    <System.NonSerialized()> _
    Public device As Device
    <System.NonSerialized()> _
    Public bnd As ClassMesh

    Public cMesh As New List(Of ClassMesh)

    Dim ns As Int16 = 6
    Dim minR As Single = 0.2
    Dim maxR As Single = 2
    Dim maxRT As Single
    Dim dmrai As Integer = 500
    Dim dmraisf As Single = 1.5
    Dim bndR As Single = 10
    Dim bndS As Int16 = 6
    Dim maxM As Int16 = 10
    Dim minE As Single = 2
    Dim maxE As Single = 10
    Dim maxAtmp As Integer = 5000
    Dim dwbnd As Boolean = False
    Dim pt As ClassPacking.PackType = PackType.ngon
    Dim et As ClassPacking.ExtrusionType = ExtrusionType.random
    Public Property DecreaseRadiusScaleFactor() As Single
        Get
            Return Me.dmraisf
        End Get
        Set(ByVal value As Single)
            If value < 1 Then
                MsgBox("Value must be greater or equal to 1")
            Else
                Me.dmraisf = value
            End If
        End Set
    End Property
    Public Property DecreaseRadiusAfterIterations() As Integer
        Get
            Return Me.dmrai
        End Get
        Set(ByVal value As Integer)
            Me.dmrai = value
        End Set
    End Property
    Public Property maximumAtmp() As Integer
        Get
            Return Me.maxAtmp
        End Get
        Set(ByVal value As Integer)
            Me.maxAtmp = value
        End Set
    End Property
    Public Property TypeOfPacking() As ClassPacking.PackType
        Get
            Return Me.pt
        End Get
        Set(ByVal value As ClassPacking.PackType)
            Me.pt = value
        End Set
    End Property
    Public Property TypeOfExtrusion() As ClassPacking.ExtrusionType
        Get
            Return Me.et
        End Get
        Set(ByVal value As ClassPacking.ExtrusionType)
            Me.et = value
        End Set
    End Property
    Public Property MinimumExtrusion() As Single
        Get
            Return minE
        End Get
        Set(ByVal value As Single)
            minE = value
        End Set
    End Property
    Public Property MaximumExtrusion() As Single
        Get
            Return maxE
        End Get
        Set(ByVal value As Single)
            maxE = value
        End Set
    End Property
    Public Property DrawBound() As Boolean
        Get
            Return dwbnd
        End Get
        Set(ByVal value As Boolean)
            Me.dwbnd = value
        End Set
    End Property
    Public Property BoundSides() As Int16
        Get
            Return Me.bndS
        End Get
        Set(ByVal value As Int16)
            Me.bndS = value
        End Set
    End Property
    Public Property BoundRadius() As Single
        Get
            Return bndR
        End Get
        Set(ByVal value As Single)
            bndR = value
        End Set
    End Property
    Public Property NumberOfPackedObjects() As Int16
        Get
            Return Me.maxM
        End Get
        Set(ByVal value As Int16)
            Me.maxM = value
        End Set
    End Property
    Public Property MinimumNgonRadius() As Single
        Get
            Return minR
        End Get
        Set(ByVal value As Single)
            minR = value
        End Set
    End Property
    Public Property MaximumNgonRadius() As Single
        Get
            Return maxR
        End Get
        Set(ByVal value As Single)
            maxR = value
        End Set
    End Property
    Public Property NgonSides() As Int16
        Get
            Return ns
        End Get
        Set(ByVal value As Int16)
            If value < 3 Or value > Int16.MaxValue Then
                MsgBox("Value must be between 3 and " + Int16.MaxValue.ToString)
                Exit Property
            End If
            ns = value
        End Set
    End Property
    Public Sub refreshBuffer(Optional ByVal device As Device = Nothing)
        Me.rEventProgressStart()
        If device Is Nothing Then
            device = Me.device
        End If
        Me.maxRT = Me.maxR
        Dim m As Matrix = Me.transform.getTransformMatrix()
        If Me.bnd IsNot Nothing Then
            If Me.bnd.mesh IsNot Nothing Then
                Me.bnd.mesh.Dispose()
            End If
        End If
        Me.bnd = triangulatedNGonMesh(nGon(bndS, bndR))
        Me.bnd.c = Color.White
        Me.bnd.t = 64
        Me.bnd.refreshBuffer(device)
        Me.bnd.parent = Me
        Dim cm As ClassMesh
        For Each cm In Me.cMesh
            Try
                cm.mesh.Dispose()
            Catch ex As Exception

            End Try
        Next
        Me.cMesh.Clear()
        Dim badAtmp As Integer = 0

        While Me.cMesh.Count < Me.maxM And badAtmp < Me.maxAtmp
            If Me.dmrai * (badAtmp / Me.dmrai) = Me.dmrai * Int(badAtmp / Me.dmrai) Then Me.maxRT = (Me.maxRT - Me.minR) / Me.dmraisf + Me.minR
            badAtmp += 1
            Me.rEventProgress(CInt(100 * Me.cMesh.Count / Me.maxM), Me.cMesh.Count.ToString + "/" + Me.maxM.ToString + " | Atmp:" + badAtmp.ToString)
            Me.iterate(device)
            If My.Computer.Keyboard.CtrlKeyDown Then Exit While
        End While
        Dim ev As Vector3
        If Me.pt = PackType.ngonExtruded Then
            ev = New Vector3(0, 0, maxE)
            For Each cm In Me.cMesh
                If Me.et = ExtrusionType.random Then ev = New Vector3(0, 0, CSng(Math.Round(Me.maxE * Rnd() + Me.minE, 2, MidpointRounding.ToEven)))
                extrudeFlatMesh(ev, cm)
                cm.refreshBuffer(device)
            Next
        End If
        For Each cm In Me.cMesh
            cm.xPolozaj += Me.transform.tx
            cm.yPolozaj += Me.transform.ty
            cm.zPolozaj += Me.transform.tz

            cm.xRotation += Me.transform.rx
            cm.yRotation += Me.transform.ry
            cm.zRotation += Me.transform.rz

            cm.scaleX = Me.transform.sx
            cm.scaleY = Me.transform.sy
            cm.scaleZ = Me.transform.sz
            cm.refreshBuffer(device)
        Next
        'Dim i, ii As Integer
        'Dim b As Boolean = False
        'For i = 0 To Me.cMesh.Count - 2

        '    While Not b
        '        For ii = i + 1 To Me.cMesh.Count - 1
        '            b = b Or mdTools.ClassMeshesIntersect(Me.cMesh(i), Me.cMesh(ii))
        '            Me.rEventProgress(Int(100 * i / Me.cMesh.Count), i.ToString + "/" + Me.cMesh.Count.ToString + " | M-M:" + ii.ToString + "/" + Me.cMesh.Count.ToString)
        '            If b Then Exit For
        '        Next
        '        If Not b Then
        '            Me.cMesh(i).scaleX += 0.001
        '            Me.cMesh(i).scaleY += 0.001

        '            'Me.cMesh(i).scaleZ += 0.001
        '            Me.cMesh(i).refreshBuffer(device)
        '            'cf3D.Timer1_Tick(New Object, New System.EventArgs)
        '        End If
        '    End While
        '    b = False
        'Next
        Me.rEventProgressEnd()
        Me.rEventBufferRefreshed()
    End Sub
    Public Function nGon(ByVal sides As Int16, ByVal radius As Single) As List(Of Vector3)
        Dim rv As New List(Of Vector3)
        Dim angleStep As Single = CSng(Math.Round(2 * Math.PI / sides, 4, MidpointRounding.ToEven))
        Dim currentAngle As Single = 0
        Dim v As Vector3
        While currentAngle < Math.Round(2 * Math.PI, 2, MidpointRounding.ToEven)
            v = New Vector3(CSng(radius * Math.Cos(currentAngle)), CSng(radius * Math.Sin(currentAngle)), 0)
            rv.Add(v)
            currentAngle += angleStep
        End While
        Return rv
    End Function
    Public Function triangulatedNGonMesh(ByVal vertices As List(Of Vector3)) As ClassMesh
        Dim rv As New ClassMesh
        Dim cnt As Vector3 = mdTools.MassCenter(vertices)
        Dim i As Integer
        rv.vbfo.AddRange(vertices)
        rv.vbfo.Add(cnt)
        For i = 0 To vertices.Count - 2
            rv.ibf.Add(i)
            rv.ibf.Add(i + 1)
            rv.ibf.Add(rv.vbfo.Count - 1)
        Next
        rv.ibf.Add(vertices.Count - 1)
        rv.ibf.Add(0)
        rv.ibf.Add(rv.vbfo.Count - 1)
        Return rv
    End Function
    Public Sub extrudeFlatMesh(ByVal dir As Vector3, ByRef CM As ClassMesh)
        Dim vbf As New List(Of Vector3)
        Dim v As Vector3
        Dim m, mm As New Matrix
        Dim i As Integer
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Translate(dir)
        m = Matrix.Multiply(mm, m)
        For Each v In CM.vbfo
            vbf.Add(Vector3.TransformCoordinate(v, m))
        Next
        CM.vbfo.AddRange(vbf)
        For i = vbf.Count To CM.vbfo.Count - 3
            CM.ibf.Add(CM.vbfo.Count - 1)
            CM.ibf.Add(i + 1)
            CM.ibf.Add(i)

            CM.ibf.Add(i)
            CM.ibf.Add(i + 1)
            CM.ibf.Add(i - vbf.Count)

            CM.ibf.Add(i - vbf.Count + 1)
            CM.ibf.Add(i - vbf.Count)
            CM.ibf.Add(i + 1)
        Next
        CM.ibf.Add(CM.vbfo.Count - 1)
        CM.ibf.Add(vbf.Count)
        CM.ibf.Add(i)

        CM.ibf.Add(i)
        CM.ibf.Add(vbf.Count)
        CM.ibf.Add(i - vbf.Count)

        CM.ibf.Add(0)
        CM.ibf.Add(i - vbf.Count)
        CM.ibf.Add(vbf.Count)
    End Sub
    Public Sub New(ByVal name As String, ByVal device As Device)
        Me.Name = name
        Me.device = device
        Me.refreshBuffer(Me.device)
    End Sub
    Public Sub iterate(Optional ByVal device As Device = Nothing)
        Dim i As Integer
        Dim radius As Single = CSng(Math.Round(Me.maxRT * Rnd() + Me.minR, 2, MidpointRounding.ToEven))
        Dim x, y, z As Single
        Dim m As ClassMesh
        Dim b, bbnd As Boolean
       
        x = CSng(Math.Round(2 * Me.bndR * Rnd() - Me.bndR, 2, MidpointRounding.ToEven))
        y = CSng(Math.Round(2 * Me.bndR * Rnd() - Me.bndR, 2, MidpointRounding.ToEven))
        z = CSng(Math.Round(2 * Me.bndR * Rnd() - Me.bndR, 2, MidpointRounding.ToEven))
        If Me.pt = PackType.ngon Or Me.pt = PackType.ngonExtruded Or Me.pt = PackType.Mesh Then
            m = triangulatedNGonMesh(nGon(ns, radius))

            m.xPolozaj = x
            m.yPolozaj = y
            ' m.zPolozaj = z
            m.refreshBuffer(device)
            bbnd = mdTools.ClassMeshesIntersect(Me.bnd, m)

            For i = 0 To Me.cMesh.Count - 1
                b = mdTools.ClassMeshesIntersect(Me.cMesh(i), m) Or Not bbnd
                If b Then
                    m.mesh.Dispose()
                    Exit Sub
                End If
            Next
            m.parent = Me
            Me.cMesh.Add(m)
        End If

    End Sub
    Public Sub afterPaste(ByVal device As Device)
        refreshBuffer(device)
    End Sub
    Public Enum PackType
        ngon = 0
        Mesh = 1
        ngonExtruded = 2
    End Enum
    Public Enum ExtrusionType
        constant = 0
        random = 1
    End Enum
End Class
