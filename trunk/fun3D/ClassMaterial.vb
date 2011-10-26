Imports System.ComponentModel
Imports Microsoft.DirectX.Direct3D

<System.Serializable()> _
Public Class ClassMaterial
    Dim ime As String = "Mat1"
    <System.NonSerialized()> _
    Dim mat As New Material
    Dim tex As IO.MemoryStream = Nothing
    Dim alfa As Byte = 255
    Dim difCol As Color = Color.Blue
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
            Return difCol
        End Get
        Set(ByVal value As Color)
            difCol = value
            mat.Diffuse = Color.FromArgb(alfa, value)
        End Set
    End Property
    Public ReadOnly Property DifColorValue() As Color
        Get
            Return mat.Diffuse
        End Get
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
    Public Property texture() As IO.MemoryStream
        Get
            Return tex
        End Get
        Set(ByVal value As IO.MemoryStream)
            tex = value
        End Set
    End Property
    Public Property transparency() As Byte
        Get
            Return alfa
        End Get
        Set(ByVal value As Byte)
            alfa = value
            mat.Diffuse = Color.FromArgb(value, difCol)
        End Set
    End Property
    ' Methods
    Sub New(ByVal name As String, ByVal color As Color)
        Me.ime = name
        Me.difCol = color
    End Sub
    Public Sub afterPaste(ByVal device As Device)
        mat = New Material
    End Sub
End Class
