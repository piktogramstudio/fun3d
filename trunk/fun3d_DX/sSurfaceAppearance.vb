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
