Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design
Public Class cEquationPropertyEditor
    Inherits UITypeEditor
    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function
    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        Dim wfes As IWindowsFormsEditorService = TryCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
        If wfes IsNot Nothing Then
            Dim tb As New ucEquation
            Try
                tb.params = CType(context.Instance.parametri, List(Of ClassParametri))
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            tb.TextBox1.Text = value.ToString
            wfes.DropDownControl(tb)
            value = tb.TextBox1.Text
        End If
        Return value
    End Function
End Class
