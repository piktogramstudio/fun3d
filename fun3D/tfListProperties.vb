Public Class tfListProperties
    Public obj As Object = mf.Scena

    Private Sub tfListProperties_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pl() As System.Reflection.FieldInfo = obj.GetType.GetFields
        Dim pi As System.Reflection.FieldInfo
        Me.TBLP.Text = ""
        For Each pi In pl
            'If pi.FieldType Is GetType(String) Then
            Try
                Me.TBLP.Text += pi.Name + " = " + pi.GetValue(obj).ToString + vbCr
            Catch ex As Exception

            End Try

            'End If
        Next
    End Sub
End Class