Imports System.ComponentModel
Public Class ClassDynamicParametri
    Dim naziv As String = "new1"
    Dim formula As String = "0"
    Dim cv As Single = 0
    <Category("Value")> _
    Public Property value() As Single
        Get
            Return Me.cv
        End Get
        Set(ByVal value As Single)
            Me.cv = value
        End Set
    End Property
    <Category("Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Me.naziv = value
        End Set
    End Property
    <Category("Evaluate")> _
    Public Property funkcija() As String
        Get
            Return Me.formula
        End Get
        Set(ByVal value As String)
            Me.formula = value
        End Set
    End Property
End Class
