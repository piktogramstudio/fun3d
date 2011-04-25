Public Structure sSurfaceAppearance
    Public FrontColor As Color
    Public BackColor As Color
    Public AmbientFrontColor As Color
    Public AmbientBackColor As Color
    Public EmissiveFrontColor As Color
    Public EmissiveBackColor As Color
    Public SpecularFrontColor As Color
    Public SpecularBackColor As Color
    Public SpecularFrontSharpness As Single
    Public SpecularBackSharpness As Single
    Public Sub New(ByVal Color As Color, ByVal AmbientColor As Color, ByVal EmissiveColor As Color, ByVal SpecularColor As Color, ByVal SpecularSharpness As Single)
        Me.FrontColor = Color
        Me.BackColor = Color
        Me.AmbientFrontColor = AmbientColor
        Me.AmbientBackColor = AmbientColor
        Me.EmissiveFrontColor = EmissiveColor
        Me.EmissiveBackColor = EmissiveColor
        Me.SpecularFrontColor = SpecularColor
        Me.SpecularBackColor = SpecularColor
        Me.SpecularFrontSharpness = SpecularSharpness
        Me.SpecularBackSharpness = SpecularSharpness
    End Sub
End Structure
