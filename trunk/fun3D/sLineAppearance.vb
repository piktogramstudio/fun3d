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
