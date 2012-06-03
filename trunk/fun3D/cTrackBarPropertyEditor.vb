Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System.ComponentModel
''' <summary>
''' EditValue for properties edited with TrackBar (Slider)
''' </summary>
''' <remarks>This editor uses integer values between 0 and 100. Passing other values requires propotional conversion in "Set" and "Get" statements.</remarks>
Public Class cTrackBarPropertyEditor
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
    ''' EditValue for properties edited with TrackBar (Slider)
    ''' </summary>
    ''' <param name="context">An <see cref="ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
    ''' <param name="provider">An <see cref="IServiceProvider" /> that this editor can use to obtain services.</param>
    ''' <param name="value">The object to edit.</param>
    ''' <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
    ''' <remarks></remarks>
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
