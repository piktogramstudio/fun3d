Imports System.Math
Public Class ucEquation
    Public params() As ClassParametri
    Private Sub ucEquation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ListBox1.Items.Clear()
        Me.ListBox2.Items.Clear()
        Dim param As ClassParametri
        For Each param In Me.params
            Me.ListBox2.Items.Add(param.Name)
        Next
        Me.ListBox2.Items.Add("PI")
        Me.ListBox2.Items.Add("E")
        Dim lastcm As String = ""
        For Each mi In GetType(Math).GetMethods
            'If lastcm <> mi.Name Then
            Me.ListBox1.Items.Add(mi.Name)
            'End If
            lastcm = mi.Name
        Next
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim mi As Reflection.MethodInfo
        Dim pi As Reflection.ParameterInfo
        Dim s As String = ""
        Try
            mi = GetType(Math).GetMethods()(Me.ListBox1.SelectedIndex)
            For Each pi In mi.GetParameters()
                s += pi.Name + " As " + pi.ParameterType.Name + vbCrLf
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Me.Label1.Text = s
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Dim mi As Reflection.MethodInfo
        Dim pi As Reflection.ParameterInfo
        Dim s As String = ""
        Try
            mi = GetType(Math).GetMethods()(Me.ListBox1.SelectedIndex)
            s += mi.Name + "("
            For Each pi In mi.GetParameters()
                s += pi.Name + ","
            Next
            If mi.GetParameters.Length > 0 Then
                s = s.Remove(s.Length - 1, 1)
            End If
            s += ")"
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Me.TextBox1.SelectedText = s
    End Sub

    Private Sub ListBox2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.DoubleClick
        Me.TextBox1.SelectedText = Me.ListBox2.SelectedItem.ToString
    End Sub
End Class
