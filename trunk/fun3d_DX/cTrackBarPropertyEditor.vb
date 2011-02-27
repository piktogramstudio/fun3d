Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.ComponentModel

Public Class cTrackBarPropertyEditor
    Inherits UITypeEditor
    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function
    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        Dim wfes As IWindowsFormsEditorService = TryCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
        If wfes IsNot Nothing Then
            Dim tb As New TrackBar()
            tb.Minimum = 0
            tb.Maximum = 100
            tb.Value = CInt(value)
            tb.TickFrequency = 10
            wfes.DropDownControl(tb)
            value = tb.Value
        End If
        Return value
    End Function
End Class
