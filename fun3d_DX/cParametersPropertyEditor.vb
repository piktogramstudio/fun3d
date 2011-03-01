Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design
Public Class cParametersPropertyEditor
    Inherits UITypeEditor
    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function
    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        Dim wfes As IWindowsFormsEditorService = TryCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
        If wfes IsNot Nothing Then
            Dim tb As New ucParameters
            tb.params = value
            tb.context = context.Instance
            wfes.DropDownControl(tb)
            value = tb.params
        End If
        Return value
    End Function
End Class
