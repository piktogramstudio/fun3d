Imports System.ComponentModel
<System.Serializable()> _
Public Class ClassParametri
    Private naziv As String = "new1"
    Private vrednost As Single = 0
    Dim minS As Single = -10
    Dim maxS As Single = 10
    Dim stepS As Single = 1
    <Category("Value Inspector"), DisplayName("Min value")> _
    Public Property sliderMinimum() As Single
        Get
            Return Me.minS
        End Get
        Set(ByVal value As Single)
            Me.minS = value
        End Set
    End Property
    <Category("Value Inspector"), DisplayName("Max value")> _
    Public Property sliderMaximum() As Single
        Get
            Return Me.maxS
        End Get
        Set(ByVal value As Single)
            Me.maxS = value
        End Set
    End Property
    <Category("Value Inspector"), DisplayName("Step"), Description("Increment and decrement step for Value inspector")> _
    Public Property sliderStep() As Single
        Get
            Return Me.stepS
        End Get
        Set(ByVal value As Single)
            Me.stepS = value
        End Set
    End Property
    <Category("Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Dim fl As String = Left(value, 1)
            If System.Text.RegularExpressions.Regex.IsMatch(fl, "^[a-zA-Z]*$") And System.Text.RegularExpressions.Regex.IsMatch(value, "^[0-9a-zA-Z]*$") Then
                naziv = value
            Else
                MsgBox("Name must start with alphabet and can contain only alphanumeric characters!")
            End If
        End Set
    End Property
    <Category("Value")> _
    Public Property value() As Single
        Get
            Return vrednost
        End Get
        Set(ByVal value As Single)
            vrednost = value
        End Set
    End Property
End Class
