<System.Serializable()> _
Public Structure sMetaData
    Public Name As String
    Public Description As String
    Public Sub New(ByVal name As String, Optional ByVal description As String = "No description")
        Me.Name = name
        Me.Description = description
    End Sub
End Structure
