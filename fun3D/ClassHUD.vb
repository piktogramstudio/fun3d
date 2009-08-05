Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
<System.Serializable()> _
Public Class ClassHUD
    <System.NonSerialized()> _
    Public s As Sprite
    <System.NonSerialized()> _
    Public t As Texture
    Public Sub New(ByVal scene As ClassScena, ByVal device As Device)
        s = New Sprite(device)
        t = Texture.FromBitmap(device, My.Resources.newUV, 0, Pool.Managed)
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        s = New Sprite(device)
        t = Texture.FromBitmap(device, My.Resources.newUV, 0, Pool.Managed)
    End Sub
End Class
