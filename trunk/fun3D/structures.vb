Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Public Structure sCursorRay
    Public Property vNear As Vector3
    Public Property vFar As Vector3
    Public Property vDirection As Vector3
    Public Sub New(x As Single, y As Single, device As Device)
        Dim viewport As Viewport
        Dim world As Matrix
        Dim proj As Matrix
        Dim view As Matrix

        Dim vIn As Vector3

        viewport = device.Viewport
        world = device.Transform.World
        proj = device.Transform.Projection
        view = device.Transform.View

        vIn.X = x * device.Viewport.Width / cf3D.Width : vIn.Y = y * device.Viewport.Height / cf3D.Height

        'Najblize kursoru
        vIn.Z = 0
        Me.vNear = Microsoft.DirectX.Vector3.Unproject(vIn, viewport, proj, view, world)

        'Udaljeno od kursora
        vIn.Z = 1
        Me.vFar = Microsoft.DirectX.Vector3.Unproject(vIn, viewport, proj, view, world)

        'Pravac zraka
        Me.vDirection = Microsoft.DirectX.Vector3.Subtract(vFar, vNear)
    End Sub
End Structure
<System.Serializable()> _
Public Structure sLineAppearance
    Public LineWidth As Single
    Public LineColor As Color
    Public LineTransparency As Byte
    Public Sub New(ByVal Width As Single, ByVal Color As Color, ByVal Transparency As Byte)
        Me.LineWidth = Width
        Me.LineColor = Color
        Me.LineTransparency = Transparency
    End Sub
End Structure
<System.Serializable()> _
Public Structure sMetaData
    Public Name As String
    Public Description As String
    Public Sub New(ByVal name As String, Optional ByVal description As String = "No description")
        Me.Name = name
        Me.Description = description
    End Sub
End Structure
<System.Serializable()> _
Public Structure sSurfaceAppearance
    Public Color As Color
    Public AmbientColor As Color
    Public EmissiveColor As Color
    Public SpecularColor As Color
    Public SpecularSharpness As Single
    Public Sub New(ByVal Color As Color, ByVal AmbientColor As Color, ByVal EmissiveColor As Color, ByVal SpecularColor As Color, ByVal SpecularSharpness As Single)
        Me.Color = Color
        Me.AmbientColor = AmbientColor
        Me.EmissiveColor = EmissiveColor
        Me.SpecularColor = SpecularColor
        Me.SpecularSharpness = SpecularSharpness
    End Sub
End Structure
