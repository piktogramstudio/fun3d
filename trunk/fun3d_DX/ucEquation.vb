Imports System.Math
Public Class ucEquation
    Public params As List(Of ClassParametri)
    Private Sub ucEquation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ListBox1.Items.Clear()
        Dim lastcm As String = ""
        For Each mi In GetType(Math).GetMethods
            'If lastcm <> mi.Name Then
            Me.ListBox1.Items.Add(mi.Name)
            'End If
            lastcm = mi.Name
        Next
        Me.populateParamGrid()
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
    Private Sub DGWParameters_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGWParameters.CellDoubleClick
        Try
            Me.TextBox1.SelectedText = DGWParameters.Rows(e.RowIndex).Cells("ParameterName").Value.ToString
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub DGWParameters_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGWParameters.CellEndEdit
        Try
            Dim param As New ClassParametri()
            Dim addedRow As DataGridViewRow = Me.DGWParameters.Rows(e.RowIndex)
            param.Name = addedRow.Cells("ParameterName").Value.ToString
            param.sliderMaximum = CSng(addedRow.Cells("Max").Value)
            param.sliderMinimum = CSng(addedRow.Cells("Min").Value)
            param.sliderStep = CSng(addedRow.Cells("SliderStep").Value)
            param.value = CSng(addedRow.Cells("Value").Value)
            If e.RowIndex < Me.params.Count Then
                Me.params(e.RowIndex) = param
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub populateParamGrid()
        Me.DGWParameters.Rows.Clear()
        Dim param As ClassParametri
        For Each param In Me.params
            Me.DGWParameters.Rows.Add(New Object() {param.Name, param.value, param.sliderMinimum, param.sliderMaximum, param.sliderStep})
        Next
        Me.DGWParameters.Rows.Add(New Object() {"PI", Math.PI, Math.PI, Math.PI, 0})
        Me.DGWParameters.Rows.Add(New Object() {"E", Math.E, Math.E, Math.E, 0})
        Me.DGWParameters.Rows(Me.DGWParameters.Rows.Count - 3).Selected = True
    End Sub
    Private Sub BAddParam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BAddParam.Click
        Try
            Dim param As New ClassParametri()
            Me.params.Add(param)
            Me.populateParamGrid()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub DGWParameters_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DGWParameters.UserDeletingRow
        Try
            Me.params.RemoveAt(e.Row.Index)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            e.Cancel = True
        End Try
    End Sub
End Class
