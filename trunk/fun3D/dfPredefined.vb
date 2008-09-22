Imports System.Windows.Forms
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports Eval3
Imports System.Math
Public Class dfPredefined
    Public Scena As New ClassScena
    Dim saveDataSet As New dsProjekat
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dfPredefined_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        cfShow3d.Close()
    End Sub

    Private Sub dfPredefined_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ListBoxSamples.Items.Clear()
        Dim f As String
        For Each f In My.Computer.FileSystem.GetFiles(My.Application.Info.DirectoryPath + "\samples", FileIO.SearchOption.SearchTopLevelOnly, "*.f3d")
            Dim naziv() As String = f.Split("\")
            Me.ListBoxSamples.Items.Add(naziv(naziv.Length - 1).Split(".")(0))
        Next
        cfShow3d.Dock = DockStyle.Fill
        cfShow3d.TopLevel = False
        cfShow3d.Parent = Me
        cfShow3d.Show()
        cfShow3d.BringToFront()
    End Sub

    Private Sub ListBoxSamples_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxSamples.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Me.Scena = New ClassScena
        Me.saveDataSet.Clear()
        Try
            Me.saveDataSet.ReadXml(My.Application.Info.DirectoryPath + "\samples\" + Me.ListBoxSamples.SelectedItem.ToString + ".f3d")
            Dim os As dsProjekat.scenaRow = Me.saveDataSet.scena(0)
            With Me.Scena
                .Ambient = Color.FromArgb(Val(os.AmbientColor))
                .backgroundColor = Color.FromArgb(Val(os.BackgroundColor))
                .colorGrid = Color.FromArgb(Val(os.GridColor))
                .FillMode = os.FillMode
                .gridMaxV = os.GridMaxValue
                .gridMinV = os.GridMinValue
                .stepGrid = os.GridStep
                .Lighting = os.Lighting
                .Name = os.Name
                .ShadeMode = os.ShadeMode
                .UVList.Clear()
                .sceneLights.Clear()
            End With
            Dim ouv As dsProjekat.UVRow
            For Each ouv In os.GetUVRows
                Dim UV As New ClassUV
                With UV
                    .maksimalnoU = ouv.UMax
                    .maksimalnoV = ouv.VMax
                    .minimalnoU = ouv.UMin
                    .minimalnoV = ouv.VMin
                    .scaleX = ouv.XScale
                    .scaleY = ouv.YScale
                    .scaleZ = ouv.ZScale
                    .Udens = ouv.UDensity
                    .Vdens = ouv.VDensity
                    .XF = ouv.XFun
                    .YF = ouv.YFun
                    .ZF = ouv.ZFun
                    .bojaLinija = Color.FromArgb(Val(ouv.LineColor))
                    .bojaPolja1 = Color.FromArgb(Val(ouv.FieldColor1))
                    .bojaPolja2 = Color.FromArgb(Val(ouv.FieldColor2))
                    .xPolozaj = ouv.XPos
                    .yPolozaj = ouv.YPos
                    .zPolozaj = ouv.ZPos
                    .Name = ouv.Name
                    .sliderMaximumUmin = ouv.maxSU
                    .sliderMinimumUmin = ouv.minSU
                    .sliderStepUmin = ouv.stepSU
                    .sliderMaximumVmin = ouv.maxSV
                    .sliderMinimumVmin = ouv.minSV
                    .sliderStepVmin = ouv.stepSV
                    .sliderMaximumUmax = ouv.maxSUm
                    .sliderMinimumUmax = ouv.minSUm
                    .sliderStepUmax = ouv.stepSUm
                    .sliderMaximumVmax = ouv.maxSVm
                    .sliderMinimumVmax = ouv.minSVm
                    .sliderStepVmax = ouv.stepSVm
                    .parametri.Clear()
                    Dim op As dsProjekat.parametriRow
                    For Each op In ouv.GetparametriRows
                        Dim plAdd As New ClassParametri
                        plAdd.Name = op.Name
                        plAdd.value = op.Value
                        plAdd.sliderMaximum = op.maxS
                        plAdd.sliderMinimum = op.minS
                        plAdd.sliderStep = op.stepS
                        .parametri.Add(plAdd)
                    Next
                    Dim odp As dsProjekat.dynParametriRow
                    For Each odp In ouv.GetdynParametriRows
                        Dim dp As New ClassDynamicParametri
                        dp.Name = odp.Name
                        dp.funkcija = odp.funkcija
                        .dynprm.Add(dp)
                    Next
                End With
                Me.Scena.UVList.Add(UV)
            Next
            Dim ol As dsProjekat.LightsRow
            For Each ol In os.GetLightsRows
                Dim l As New ClassLight
                With l
                    .Ambient = Color.FromArgb(Val(ol.Ambient))
                    .Diffuse = Color.FromArgb(Val(ol.Diffuse))
                    .Specular = Color.FromArgb(Val(ol.Specular))
                    .Direction = ol.Direction
                    .Position = ol.Position
                    .Enabled = ol.Enabled
                    .Name = ol.Name
                    .Type = ol.Type
                End With
                Me.Scena.sceneLights.Add(l)
            Next
            tfOsobine.PropertyGridUV.SelectedObject = Me.Scena
            tfOsobine.PropertyGridUV.Refresh()
            Dim UV1 As ClassUV
            For Each UV1 In Me.Scena.UVList
                UV1.refreshBuffer()
            Next
            cfShow3d.Invalidate()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
End Class
