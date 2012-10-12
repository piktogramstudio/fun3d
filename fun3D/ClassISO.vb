Imports System.Math
Imports System.ComponentModel
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
''' <summary>
''' ISO surfaces - f(x,y,z)=0
''' </summary>
<System.Serializable()> _
Public Class ClassISO
#Region "Non Serialized Fields"
    <System.NonSerialized()> _
    Public ISOMesh As Mesh
    <System.NonSerialized()> _
    Public vBuffer As New List(Of CustomVertex.PositionNormalTextured)
#End Region
#Region "Public Fields"
    Public iBuffer As New List(Of Integer)
#End Region
#Region "Private Fields"
    Dim pp(,,) As Vector3 = {}
    Dim pi(,,) As Single = {}
#End Region
#Region "Events"
    Public Shared Event bufferRefreshed()
    Public Shared Event progressStart()
    Public Shared Event progressEnd()
    Public Shared Event progress(ByVal p As Integer, ByVal m As String)
#End Region
#Region "Browsable Properties"
    <Category("4. Position")> _
    Public Property xPosition() As Single = 0
    <Category("4. Position")> _
    Public Property yPosition() As Single = 0
    <Category("4. Position")> _
    Public Property zPosition() As Single = 0
    <Category("5. Rotation")> _
    Public Property xRotation() As Single = 0
    <Category("5. Rotation")> _
    Public Property yRotation() As Single = 0
    <Category("5. Rotation")> _
    Public Property zRotation() As Single = 0
    <Category("6. Scale")> _
    Public Property scaleX() As Single = 1
    <Category("6. Scale")> _
    Public Property scaleY() As Single = 1
    <Category("6. Scale")> _
    Public Property scaleZ() As Single = 1
    <Category("2. Appearance")> _
    Public Property FrontColor() As Color = Color.DarkRed
    <Category("2. Appearance")> _
    Public Property BackColor() As Color = Color.White
    <Category("2. Appearance")> _
    Public Property Transparency() As Byte = 255
    <Category("2. Appearance")> _
    Public Property smooth() As Byte = 1
    <Category("1. Meta")> _
    Public Property Name() As String = "New ISO surface"
    <Category("3. Definition"), DisplayName("ISO Value")> _
    Public Property Tolerance() As String = "0"
    <Category("3. Definition"), DisplayName("Function")> _
    Public Property Fun() As String = "0"
    <Category("3. Definition")> _
    Public Property Xdensity() As Integer = 10
    <Category("3. Definition")> _
    Public Property Ydensity() As Integer = 10
    <Category("3. Definition")> _
    Public Property Zdensity() As Integer = 10
    <Category("3. Definition"), DisplayName("Max X")> _
    Public Property Xmaksimalno() As Single = 10
    <Category("3. Definition"), DisplayName("Max Y")> _
    Public Property Ymaksimalno() As Single = 10
    <Category("3. Definition"), DisplayName("Max Z")> _
    Public Property Zmaksimalno() As Single = 10
    <Category("3. Definition"), DisplayName("Min X")> _
    Public Property Xminimalno() As Single = -10
    <Category("3. Definition"), DisplayName("Min Y")> _
    Public Property Yminimalno() As Single = -10
    <Category("3. Definition"), DisplayName("Min Z")> _
    Public Property Zminimalno() As Single = -10
#End Region
#Region "Private Properties"
    Private ReadOnly Property XStep() As Single
        Get
            Return (Me.Xmaksimalno - Me.Xminimalno) / Me.Xdensity
        End Get
    End Property
    Private ReadOnly Property YStep() As Single
        Get
            Return (Me.Ymaksimalno - Me.Yminimalno) / Me.Ydensity
        End Get
    End Property
    Private ReadOnly Property ZStep() As Single
        Get
            Return (Me.Zmaksimalno - Me.Zminimalno) / Me.Zdensity
        End Get
    End Property
#End Region
#Region "Constructors"
    Public Sub New()
        Me.refreshBuffer()
    End Sub
    Public Sub New(ByVal Name As String, ByVal device As Direct3D.Device)
        Me.Name = Name
        Me.Xminimalno = -3.14
        Me.Xmaksimalno = 3.14
        Me.Yminimalno = -3.14
        Me.Ymaksimalno = 3.14
        Me.Zminimalno = -3.14
        Me.Zmaksimalno = 3.14
        Me.Fun = "cos(x)+cos(y)+cos(z)"
        Me.refreshBuffer(device)
    End Sub
#End Region
#Region "Public Methods"
    Public Function evaluateISO() As Boolean
        Dim cd As String = ""
        Dim vba As New Microsoft.VisualBasic.VBCodeProvider()
        Dim cp As New CodeDom.Compiler.CompilerParameters()
        cd += "Imports Microsoft.DirectX" + vbCrLf
        cd += "Imports system.collections.generic" + vbCrLf
        cd += "Imports System" + vbCrLf
        cd += "Imports System.Math" + vbCrLf
        cd += "Namespace FlyAss" + vbCrLf
        cd += "Class Evaluator" + vbCrLf
        cd += "Public Function Evaluate(ByRef pp(,,) as Vector3, ByRef pi(,,) as Single) As Boolean" + vbCrLf
        cd += "dim x, y, z As Single" + vbCrLf
        cd += "Dim vkt As Vector3" + vbCrLf
        cd += "Dim xs, ys, zs As Single" + vbCrLf
        cd += "Dim cx, cy, cz As Integer" + vbCrLf
        cd += "Dim ISOVal As Single = 0" + vbCrLf
        cd += "xs = " + Str(Me.XStep) + vbCrLf
        cd += "ys = " + Str(Me.YStep) + vbCrLf
        cd += "zs = " + Str(Me.ZStep) + vbCrLf
        cd += "Array.Clear(pp, 0, pp.Length)" + vbCrLf
        cd += "Array.Clear(pi, 0, pi.Length)" + vbCrLf
        cd += "ReDim pp(" + Str(Me.Xdensity) + "+ 1, " + Str(Me.Ydensity) + "+ 1, " + Str(Me.Zdensity) + "+ 1)" + vbCrLf
        cd += "ReDim pi(" + Str(Me.Xdensity) + "+ 1, " + Str(Me.Ydensity) + "+ 1, " + Str(Me.Zdensity) + "+ 1)" + vbCrLf
        cd += "cz = -1" + vbCrLf
        cd += "For z = " + Str(Zminimalno) + " To " + Str(Zmaksimalno) + "+zs/2 Step zs" + vbCrLf
        cd += "cz += 1" + vbCrLf
        cd += "cy = -1" + vbCrLf
        cd += "For y = " + Str(Yminimalno) + " To " + Str(Ymaksimalno) + "+ys/2 Step ys" + vbCrLf
        cd += "cy += 1" + vbCrLf
        cd += "cx = -1" + vbCrLf
        cd += "For x = " + Str(Zminimalno) + " To " + Str(Xmaksimalno) + "+xs/2 Step xs" + vbCrLf
        cd += "cx += 1" + vbCrLf
        cd += "Try" + vbCrLf
        cd += "vkt = New Vector3(x, y, z)" + vbCrLf
        cd += "pp(cx, cy, cz) = vkt" + vbCrLf
        cd += "ISOVal = " + Me.Fun + " - " + Me.Tolerance + vbCrLf
        cd += "pi(cx, cy, cz) = ISOVal" + vbCrLf
        cd += "Catch ex As Exception" + vbCrLf
        cd += "Console.WriteLine(ex.Message)" + vbCrLf
        cd += "Array.Clear(pi, 0, pi.Length)" + vbCrLf
        cd += "Return False" + vbCrLf
        cd += "Exit Function" + vbCrLf
        cd += "End Try" + vbCrLf
        cd += "Next" + vbCrLf
        cd += "Next" + vbCrLf
        cd += "Next" + vbCrLf
        cd += "Return True" + vbCrLf
        cd += "End Function" + vbCrLf
        cd += "End Class" + vbCrLf
        cd += "End Namespace" + vbCrLf
        ' Setup the Compiler Parameters  
        ' Add any referenced assemblies
        cp.ReferencedAssemblies.Add("system.dll")
        cp.ReferencedAssemblies.Add("system.xml.dll")
        cp.ReferencedAssemblies.Add("system.data.dll")
        cp.ReferencedAssemblies.Add("microsoft.directx.dll")
        cp.CompilerOptions = "/t:library"
        cp.GenerateInMemory = True
        Dim cr As CodeDom.Compiler.CompilerResults = vba.CompileAssemblyFromSource(cp, cd)
        If Not cr.Errors.Count <> 0 Then
            Dim exeins As Object = cr.CompiledAssembly.CreateInstance("FlyAss.Evaluator")
            Dim mi As Reflection.MethodInfo = exeins.GetType().GetMethod("Evaluate")
            Dim sns(1) As Object
            Dim p0(1, 1, 1) As Vector3
            Dim p1(1, 1, 1) As Single
            sns(0) = p0
            sns(1) = p1
            Dim b As Boolean = CBool(mi.Invoke(exeins, sns))
            If b Then
                pp = CType(sns(0), Vector3(,,))
                pi = CType(sns(1), Single(,,))
            End If
            Return b

        Else
            Dim ce As CodeDom.Compiler.CompilerError
            For Each ce In cr.Errors
                'Console.WriteLine(cd)
                Console.WriteLine(ce.ErrorText)
                Array.Clear(pi, 0, pi.Length)
            Next
            Return False
        End If
        
    End Function
    Public Sub refreshBuffer(Optional ByVal device As Direct3D.Device = Nothing)
        If Not Me.evaluateISO() Then Exit Sub
        RaiseEvent progressStart()
        Me.vBuffer.Clear()
        Me.iBuffer.Clear()
        Dim xs, ys, zs As Single
        Dim cx, cy, cz As Integer

        Dim i As Integer = 0
        xs = Me.XStep
        ys = Me.YStep
        zs = Me.ZStep
        cz = -1
        Dim gcell As New ClassMTeth.GRIDCELL
        Dim trg() As ClassMTeth.TRIANGLE = {New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, _
                                                   New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, _
                                                   New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE, New ClassMTeth.TRIANGLE}
        Dim cmt As New ClassMTeth
        Dim cmc As New ClassMCube
        Dim nind As Integer
        Dim plen, pin, p As Integer
        plen = (Me.Xdensity + 1) * (Me.Ydensity + 1) * (Me.Zdensity + 1)
        pin = 0
        Dim x, y, z As Single
        For z = Me.Zminimalno To Me.Zmaksimalno - xs / 2 Step zs
            cz += 1
            cy = -1
            For y = Me.Yminimalno To Me.Ymaksimalno - ys / 2 Step ys
                cy += 1
                cx = -1
                For x = Me.Xminimalno To Me.Xmaksimalno - zs / 2 Step xs
                    p = 100 * pin \ plen
                    RaiseEvent progress(p, "Creating surface " + pin.ToString + "/" + plen.ToString)
                    pin += 1
                    cx += 1
                    gcell.p(3) = pp(cx, cy, cz)
                    gcell.p(2) = pp(cx + 1, cy, cz)
                    gcell.p(1) = pp(cx + 1, cy + 1, cz)
                    gcell.p(0) = pp(cx, cy + 1, cz)
                    gcell.p(7) = pp(cx, cy, cz + 1)
                    gcell.p(6) = pp(cx + 1, cy, cz + 1)
                    gcell.p(5) = pp(cx + 1, cy + 1, cz + 1)
                    gcell.p(4) = pp(cx, cy + 1, cz + 1)

                    gcell.val(3) = pi(cx, cy, cz)
                    gcell.val(2) = pi(cx + 1, cy, cz)
                    gcell.val(1) = pi(cx + 1, cy + 1, cz)
                    gcell.val(0) = pi(cx, cy + 1, cz)
                    gcell.val(7) = pi(cx, cy, cz + 1)
                    gcell.val(6) = pi(cx + 1, cy, cz + 1)
                    gcell.val(5) = pi(cx + 1, cy + 1, cz + 1)
                    gcell.val(4) = pi(cx, cy + 1, cz + 1)

                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 2, 3, 7)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 2, 6, 7)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 4, 6, 7)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 6, 1, 2)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 0, 6, 1, 4)
                    'addTriangles(trg, i, nind)
                    'nind = cmt.PolygoniseTri(gcell, Me.tol, trg, 5, 6, 1, 4)
                    'addTriangles(trg, i, nind)

                    nind = cmc.Polygonise(gcell, 0, trg)
                    addTriangles(trg, i, nind)

                Next
            Next
        Next

        applyTransform()

        Try
            If device = Nothing Then
                device = Me.ISOMesh.Device
            End If
            Me.createMesh(device)
        Catch ex As Exception

        End Try
        RaiseEvent progressEnd()
    End Sub
    Public Sub createMesh(ByVal device As Direct3D.Device)
        Try
            Me.ISOMesh.Dispose()
        Catch ex As Exception

        End Try
        ' RUTINA ZA PRAVLJENJE MESHA
        Dim vvv() As CustomVertex.PositionNormalTextured = Me.vBuffer.ToArray
        Dim c1 As Integer
        c1 = vvv.Length

        Dim ind() As Int32 = Me.iBuffer.ToArray
        ISOMesh = New Mesh(CInt(ind.Length / 3), c1, MeshFlags.Use32Bit, CustomVertex.PositionNormalTextured.Format, device)

        Try
            ISOMesh.SetVertexBufferData(vvv, LockFlags.None)
            ISOMesh.SetIndexBufferData(ind, LockFlags.None)

            Dim subset() As Integer = ISOMesh.LockAttributeBufferArray(LockFlags.Discard)
            'subset = Me.subset.ToArray
            ISOMesh.UnlockAttributeBuffer(subset)
            ' Optimize.
            Dim adjacency(ISOMesh.NumberFaces * 3 - 1) As Integer
            ISOMesh.GenerateAdjacency(CSng(0.1), adjacency)
            ISOMesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adjacency)
            ISOMesh.ComputeNormals()
            ISOMesh = Mesh.TessellateNPatches(ISOMesh, adjacency, Me.smooth, True)
        Catch ex As Exception
            'console.writeline(ex.Message)
        End Try
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        Me.vBuffer = New List(Of CustomVertex.PositionNormalTextured)
        refreshBuffer(device)
    End Sub
#End Region
#Region "Private methods"
    Private Sub applyTransform()
        ' TRANSFORMATION
        Dim m, mm As New Matrix
        m = Matrix.RotationYawPitchRoll(0, 0, 0)
        mm.Translate(Me.xPosition, Me.yPosition, Me.zPosition)
        m = Matrix.Multiply(mm, m)
        mm.Scale(Me.scaleX, Me.scaleY, Me.scaleZ)
        m = Matrix.Multiply(mm, m)
        mm.RotateX(Me.xRotation * CSng(Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.RotateY(Me.yRotation * CSng(Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        mm.RotateZ(Me.zRotation * CSng(Math.PI / 180))
        m = Matrix.Multiply(mm, m)
        Dim ii As Integer
        Dim v As New CustomVertex.PositionNormalTextured
        Dim plen, pin, p As Integer
        plen = Me.vBuffer.Count
        pin = 0
        For ii = 0 To Me.vBuffer.Count - 1
            p = 100 * pin \ plen
            RaiseEvent progress(p, "Transforming " + pin.ToString + "/" + plen.ToString)
            pin += 1
            v.Normal = Me.vBuffer(ii).Normal
            v.Position = Vector3.TransformCoordinate(Me.vBuffer(ii).Position, m)
            Me.vBuffer(ii) = v
        Next
    End Sub
    Private Sub addTriangles(ByVal trg As ClassMTeth.TRIANGLE(), ByVal i As Integer, ByVal nind As Integer)
        Dim indx As Integer
        For indx = 0 To nind
            triangle(trg(indx).p(0), trg(indx).p(1), trg(indx).p(2), i)
        Next
    End Sub
    Private Sub triangle(ByVal vkt1 As Vector3, ByVal vkt2 As Vector3, ByVal vkt3 As Vector3, ByRef i As Integer)
        Dim vktn As Vector3
        Dim a As Integer
        vktn = Vector3.Cross(vkt2, vkt1)
        'vktn.Normalize()
        a = indiceNumber(vkt1)
        If a > -1 Then
            Me.iBuffer.Add(a)
        Else
            Me.vBuffer.Add(New CustomVertex.PositionNormalTextured(vkt1, vktn, 1, 1))
            Me.iBuffer.Add(Me.vBuffer.Count - 1)
        End If
        a = indiceNumber(vkt2)
        If a > -1 Then
            Me.iBuffer.Add(a)
        Else
            Me.vBuffer.Add(New CustomVertex.PositionNormalTextured(vkt2, vktn, 1, 1))
            Me.iBuffer.Add(Me.vBuffer.Count - 1)
        End If
        a = indiceNumber(vkt3)
        If a > -1 Then
            Me.iBuffer.Add(a)
        Else
            Me.vBuffer.Add(New CustomVertex.PositionNormalTextured(vkt3, vktn, 1, 1))
            Me.iBuffer.Add(Me.vBuffer.Count - 1)
        End If
    End Sub
#End Region
#Region "Functions"
    Private Function indiceNumber(ByVal vect As Vector3) As Integer
        Dim rv As Integer = -1
        Dim i As Integer
        For i = 0 To Me.vBuffer.Count - 1
            If Me.vBuffer(i).Position.X = vect.X And Me.vBuffer(i).Position.Y = vect.Y And Me.vBuffer(i).Position.Z = vect.Z Then
                rv = i
                Return rv
            End If
        Next
        Return rv
    End Function
#End Region
End Class
