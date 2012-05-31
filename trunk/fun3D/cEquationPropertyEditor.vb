Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design
''' <summary>
''' UITypeEditor for properties that requires a equation input
''' </summary>
''' <remarks>To implement use <code>System.ComponentModel.Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))</code>
''' <example>
''' <code>
''' &lt;Editor(GetType(cEquationPropertyEditor), GetType(UITypeEditor))&gt; _
''' Public Property funX As String = "u"
''' </code>
''' </example>
''' </remarks>
Public Class cEquationPropertyEditor
    Inherits UITypeEditor
    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function
    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        Dim wfes As IWindowsFormsEditorService = TryCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
        If wfes IsNot Nothing Then
            Dim tb As New ucEquation
            ' context.Instance can be any type with Parameters property (List(Of ClassParametri))
            ' TODO Make equation interface or class for inheritance
            Try
                tb.params = CType(context.Instance.Parameters, List(Of ClassParametri))
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
