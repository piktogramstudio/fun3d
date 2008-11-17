Imports System.ComponentModel
Imports Microsoft.DirectX.Direct3D

Public Class ClassMaterial
    Dim ime As String = "Mat1"
    Dim mat As New Material

    'Properties
    Public Property Name() As String
        Get
            Return Me.ime
        End Get
        Set(ByVal value As String)
            Me.ime = value
        End Set
    End Property
    Public Property DiffuseColor() As Color
        Get
            Return mat.Diffuse
        End Get
        Set(ByVal value As Color)
            mat.Diffuse = value
        End Set
    End Property
    Public Property AmbientColor() As Color
        Get
            Return mat.Ambient
        End Get
        Set(ByVal value As Color)
            mat.Ambient = value
        End Set
    End Property
    Public Property EmissiveColor() As Color
        Get
            Return mat.Emissive
        End Get
        Set(ByVal value As Color)
            mat.Emissive = value
        End Set
    End Property
End Class
