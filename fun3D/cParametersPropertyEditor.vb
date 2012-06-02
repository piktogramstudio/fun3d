Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design
''' <summary>
''' UITypeEditor for equation's parameters properties
''' </summary>
''' <remarks>
''' To implement use <code>System.ComponentModel.Editor(GetType(cParametersPropertyEditor), GetType(UITypeEditor))</code>
''' <example>
''' <code lang="VB">
''' 
''' &lt;Editor(GetType(cParametersPropertyEditor), GetType(UITypeEditor))&gt; _
''' Public Property Parameters() As New List(Of ClassParametri)
''' 
''' </code>
''' </example>
''' </remarks>
Public Class cParametersPropertyEditor
    Inherits UITypeEditor
    ''' <summary>
    ''' Set edit style to <c>UITypeEditorEditStyle.DropDown</c>
    ''' </summary>
    ''' <param name="context">An <see cref="ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    ''' <returns>A <see cref="UITypeEditorEditStyle" /> value that indicates the style of editor used by the <see cref="EditValue" /> method. If the <see cref="UITypeEditor" /> does not support this method, then GetEditStyle will return None.</returns>
    ''' <remarks></remarks>
    Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function
    ''' <summary>
    ''' EditValue for equation's parameters property
    ''' </summary>
    ''' <param name="context">An <see cref="ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    ''' <param name="provider">An <see cref="IServiceProvider" /> that this editor can use to obtain services.</param>
    ''' <param name="value">The object to edit.</param>
    ''' <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
    ''' <remarks></remarks>
    Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        Dim wfes As IWindowsFormsEditorService = TryCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
        If wfes IsNot Nothing Then
            Dim tb As New ucParameters
            tb.params = CType(value, List(Of ClassParametri))
            tb.context = context.Instance
            wfes.DropDownControl(tb)
            value = tb.params
        End If
        Return value
    End Function
End Class
