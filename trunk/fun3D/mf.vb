Imports System.Math
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Public Class mf
    Public WithEvents Scena As New ClassScena
    Dim saveFile As String = ""
    Dim saveDataSet As New dsProjekat
    Dim currentFrame As Integer = 0
    Public xdfca, ydfca As Integer
    Public undoData As New List(Of ClassScena)
    Public redoData As New List(Of ClassScena)
    Dim undoRedoLimit As Integer = 10
    Private Sub mf_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        cf3D.Timer1.Stop()
        cf3D.Dispose()
    End Sub
    Private Sub mf_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ydfca = Me.Height - dfCA.Height - 20
        xdfca = Me.Width - dfCA.Width - 20
        tfOsobine.PropertyGridUV.SelectedObject = Scena
        tfOsobine.Show(Me)
        Me.NewToolStripMenuItem_Click(sender, e)
        Dim fn As String
        If My.Application.CommandLineArgs.Count > 0 Then
            fn = My.Application.CommandLineArgs(0)
            Me.Scena = Me.openFile(fn)
        End If
        cf3D.Timer1.Enabled = True
    End Sub
    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click, NewToolStripButton.Click
        If cf3D.Visible Then
            If MsgBox("Da li ste sigurni?" + Chr(10) + " Vaš predhodni rad će biti izbrisan", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                Me.Scena = New ClassScena
                tfOsobine.PropertyGridUV.SelectedObject = Me.Scena
                tfOsobine.PropertyGridUV.Refresh()
                Try
                    cf3D.Invalidate()
                Catch ex As Exception

                End Try
            End If
        End If
        cf3D.MdiParent = Me
        cf3D.Show()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim sd As New SaveFileDialog
        sd.Filter = "Fun 3d file (*.f3d)|*.f3d"
        If sd.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.fileSave(sd.FileName)
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        If Me.saveFile <> "" Then
            Me.fileSave(Me.saveFile)
        Else
            Me.SaveAsToolStripMenuItem_Click(sender, e)
        End If
    End Sub

    Private Function openFile(ByVal fnm As String, Optional ByVal frommem As Boolean = False) As ClassScena
        Dim rvalue As New ClassScena
        If Not frommem Then
            Me.saveDataSet.Clear()
            Me.saveDataSet.ReadXml(fnm)
        End If

        Try
            Dim os As dsProjekat.scenaRow = Me.saveDataSet.scena(0)
            With rvalue
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
                Try
                    .Description = os.Description
                    .PointSize = os.pSize
                Catch ex As Exception

                End Try
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
                    Try
                        .Transparency = ouv.Transparency
                        .xRotation = ouv.xRot
                        .yRotation = ouv.yRot
                        .zRotation = ouv.zRot
                    Catch ex As Exception

                    End Try
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
                rvalue.UVList.Add(UV)
            Next
            Dim ouf As dsProjekat.FramesRow
            For Each ouf In os.GetFramesRows
                rvalue.sceneFrameAdd(New ClassFrame(ouf.ax, ouf.ay, ouf.az, ouf.cx, ouf.cy, ouf.cz))
            Next
            Dim ou As dsProjekat.URow
            For Each ou In os.GetURows
                Dim U As New ClassU
                With U
                    .maksimalnoU = ou.UMax
                    .minimalnoU = ou.UMin
                    .scaleX = ou.XScale
                    .scaleY = ou.YScale
                    .scaleZ = ou.ZScale
                    .Udens = ou.UDensity
                    .XF = ou.XFun
                    .YF = ou.YFun
                    .ZF = ou.ZFun
                    .bojaLinija = Color.FromArgb(Val(ou.LineColor))
                    .xPolozaj = ou.XPos
                    .yPolozaj = ou.YPos
                    .zPolozaj = ou.ZPos
                    .Name = ou.Name
                    .sliderMaximumUmin = ou.maxSU
                    .sliderMinimumUmin = ou.minSU
                    .sliderStepUmin = ou.stepSU
                    .sliderMaximumUmax = ou.maxSUm
                    .sliderMinimumUmax = ou.minSUm
                    .sliderStepUmax = ou.stepSUm
                    Try
                        .Transparency = ou.Transparency
                        .bojaTacke = Color.FromArgb(Val(ou.pColor))
                    Catch ex As Exception

                    End Try
                    .parametri.Clear()
                    Dim op As dsProjekat.parametriRow
                    For Each op In ou.GetparametriRows
                        Dim plAdd As New ClassParametri
                        plAdd.Name = op.Name
                        plAdd.value = op.Value
                        plAdd.sliderMaximum = op.maxS
                        plAdd.sliderMinimum = op.minS
                        plAdd.sliderStep = op.stepS
                        .parametri.Add(plAdd)
                    Next
                    Dim odp As dsProjekat.dynParametriRow
                    For Each odp In ou.GetdynParametriRows
                        Dim dp As New ClassDynamicParametri
                        dp.Name = odp.Name
                        dp.funkcija = odp.funkcija
                        .dynprm.Add(dp)
                    Next
                End With
                rvalue.UList.Add(U)
            Next
            Dim oca As dsProjekat.CARow
            For Each oca In os.GetCARows
                Dim CA As New ClassCA
                With CA
                    .bojaKocke = oca.bojaKocke
                    .bojaLinije = oca.bojaLinije
                    .h = oca.h
                    .w = oca.w
                    .l = oca.l
                    .maxBC = oca.maxC
                    .minBC = oca.minC
                    .TurnOnCells = oca.turnOnC
                    .Name = oca.name
                    .brojNivoa = oca.nivoa
                    .Rule = oca.rule
                    .Space = oca.space
                    .Style = oca.style
                    .Transparency = oca.transparency
                    .xPolja = oca.xPolja
                    .yPolja = oca.yPolja
                    .xPosition = oca.xPos
                    .yPosition = oca.yPos
                    .zPosition = oca.zPos
                    .xRotation = oca.xRot
                    .yRotation = oca.yRot
                    .zRotation = oca.zRot
                    Dim mval As dsProjekat.MatrixRow
                    .matrice.Clear()
                    .matrice.Add(New List(Of Byte))
                    For Each mval In oca.GetMatrixRows
                        .matrice(0).Add(mval.value)
                    Next
                End With
                CA.createLevels()
                CA.refreshBuffer()
                rvalue.CAList.Add(CA)
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
                rvalue.sceneLights.Add(l)
            Next
            If Not frommem Then
                tfOsobine.PropertyGridUV.SelectedObject = rvalue
                tfOsobine.PropertyGridUV.Refresh()
            End If
            Try
                cf3D.Invalidate()
            Catch ex As Exception

            End Try
            Me.saveFile = fnm
            Dim UV1 As ClassUV
            For Each UV1 In rvalue.UVList
                UV1.refreshBuffer()
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        cf3D.MdiParent = Me
        cf3D.Show()
        Return rvalue
    End Function
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click, OpenToolStripMenuItem.Click
        Dim opf As New OpenFileDialog
        If Me.saveFile = "" Then
            opf.InitialDirectory = My.Application.Info.DirectoryPath + "\samples"
        End If
        opf.Filter = "Fun 3d file (*.f3d)|*.f3d|All files|*.*"
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fnm As String = opf.FileName
            Me.Scena = openFile(fnm)
        End If
    End Sub

    Private Sub NavigationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NavigationToolStripMenuItem.Click
        Me.NavigationToolStripMenuItem.Checked = Not Me.NavigationToolStripMenuItem.Checked
        If Me.NavigationToolStripMenuItem.Checked Then
            Dim xc, yc, zc, ax, ay, az As Single
            xc = cf3D.xcam
            yc = cf3D.ycam
            zc = cf3D.zcam
            ax = cf3D.angleX
            ay = cf3D.angleY
            az = cf3D.angleZ
            Try
                tfNavigacija.Show(Me)
            Catch ex As Exception

            End Try
            tfNavigacija.NUDxcam.Value = xc
            tfNavigacija.NUDycam.Value = yc
            tfNavigacija.NUDzcam.Value = zc
            tfNavigacija.NUDugaoX.Value = ax
            tfNavigacija.NUDugaoY.Value = ay
            tfNavigacija.NUDugaoZ.Value = az
        Else
            tfNavigacija.Close()
        End If
    End Sub
    Private Sub fileSave(ByVal fileName As String, Optional ByVal tomem As Boolean = False)
        Dim saveString As String = ""
        Dim pl As ClassParametri
        Dim dp As ClassDynamicParametri
        Dim UV As ClassUV
        Dim U As ClassU
        Dim CA As ClassCA
        Dim l As ClassLight
        Dim frm As ClassFrame
        Me.saveDataSet.scena.Clear()
        Dim ns As dsProjekat.scenaRow = Me.saveDataSet.scena.NewscenaRow
        With ns
            .AmbientColor = Me.Scena.Ambient.ToArgb.ToString
            .BackgroundColor = Me.Scena.backgroundColor.ToArgb.ToString
            .FillMode = Me.Scena.FillMode
            .GridColor = Me.Scena.colorGrid.ToArgb.ToString
            .GridMaxValue = Me.Scena.gridMaxV
            .GridMinValue = Me.Scena.gridMinV
            .GridStep = Me.Scena.stepGrid
            .Lighting = Me.Scena.Lighting
            .Name = Me.Scena.Name
            .ShadeMode = Me.Scena.ShadeMode
            .Description = Me.Scena.Description
            .pSize = Me.Scena.PointSize
        End With
        Me.saveDataSet.scena.AddscenaRow(ns)
        For Each UV In Me.Scena.UVList
            Dim nuv As dsProjekat.UVRow = Me.saveDataSet.UV.NewUVRow
            With nuv
                .pripadaSceni = ns.jb
                .FieldColor1 = UV.bojaPolja1.ToArgb.ToString
                .FieldColor2 = UV.bojaPolja2.ToArgb.ToString
                .LineColor = UV.bojaLinija.ToArgb.ToString
                .Name = UV.Name
                .UDensity = UV.Udens
                .UMax = UV.maksimalnoU
                .UMin = UV.minimalnoU
                .VDensity = UV.Vdens
                .VMax = UV.maksimalnoV
                .VMin = UV.minimalnoV
                .XFun = UV.XF
                .XPos = UV.xPolozaj
                .XScale = UV.scaleX
                .YFun = UV.YF
                .YPos = UV.yPolozaj
                .YScale = UV.scaleY
                .ZFun = UV.ZF
                .ZPos = UV.zPolozaj
                .ZScale = UV.scaleZ
                .maxSU = UV.sliderMaximumUmin
                .minSU = UV.sliderMinimumUmin
                .stepSU = UV.sliderStepUmin
                .maxSV = UV.sliderMaximumVmin
                .minSV = UV.sliderMinimumVmin
                .stepSV = UV.sliderStepVmin
                .maxSUm = UV.sliderMaximumUmax
                .minSUm = UV.sliderMinimumUmax
                .stepSUm = UV.sliderStepUmax
                .maxSVm = UV.sliderMaximumVmax
                .minSVm = UV.sliderMinimumVmax
                .stepSVm = UV.sliderStepVmax
                .Transparency = UV.Transparency
                .xRot = UV.xRotation
                .yRot = UV.yRotation
                .zRot = UV.zRotation
                Me.saveDataSet.UV.AddUVRow(nuv)
                For Each pl In UV.parametri
                    Dim np As dsProjekat.parametriRow = Me.saveDataSet.parametri.NewparametriRow
                    np.pripadaUV = nuv.jb
                    np.Name = pl.Name
                    np.Value = pl.value
                    np.stepS = pl.sliderStep
                    np.maxS = pl.sliderMaximum
                    np.minS = pl.sliderMinimum
                    Me.saveDataSet.parametri.AddparametriRow(np)
                Next
                For Each dp In UV.dynprm
                    Dim dp1 As dsProjekat.dynParametriRow = Me.saveDataSet.dynParametri.NewdynParametriRow
                    dp1.pripadaUV = nuv.jb
                    dp1.Name = dp.Name
                    dp1.funkcija = dp.funkcija
                    Me.saveDataSet.dynParametri.AdddynParametriRow(dp1)
                Next
            End With
        Next
        For Each frm In Me.Scena.SceneFrames
            Dim nuf As dsProjekat.FramesRow = Me.saveDataSet.Frames.NewFramesRow
            nuf.ax = frm.ax
            nuf.ay = frm.ay
            nuf.az = frm.az
            nuf.cx = frm.xc
            nuf.cy = frm.yc
            nuf.cz = frm.zc
            nuf.pripadaSceni = ns.jb
            Me.saveDataSet.Frames.AddFramesRow(nuf)
        Next
        For Each U In Me.Scena.UList
            Dim nu As dsProjekat.URow = Me.saveDataSet.U.NewURow
            With nu
                .pripadaSceni = ns.jb
                .LineColor = U.bojaLinija.ToArgb.ToString
                .Name = U.Name
                .UDensity = U.Udens
                .UMax = U.maksimalnoU
                .UMin = U.minimalnoU
                .XFun = U.XF
                .XPos = U.xPolozaj
                .XScale = U.scaleX
                .YFun = U.YF
                .YPos = U.yPolozaj
                .YScale = U.scaleY
                .ZFun = U.ZF
                .ZPos = U.zPolozaj
                .ZScale = U.scaleZ
                .maxSU = U.sliderMaximumUmin
                .minSU = U.sliderMinimumUmin
                .stepSU = U.sliderStepUmin
                .maxSUm = U.sliderMaximumUmax
                .minSUm = U.sliderMinimumUmax
                .stepSUm = U.sliderStepUmax
                .Transparency = U.Transparency
                .pColor = U.bojaTacke.ToArgb.ToString
                Me.saveDataSet.U.AddURow(nu)
                For Each pl In U.parametri
                    Dim np As dsProjekat.parametriRow = Me.saveDataSet.parametri.NewparametriRow
                    np.pripadaU = nu.jb
                    np.Name = pl.Name
                    np.Value = pl.value
                    np.stepS = pl.sliderStep
                    np.maxS = pl.sliderMaximum
                    np.minS = pl.sliderMinimum
                    Me.saveDataSet.parametri.AddparametriRow(np)
                Next
                For Each dp In U.dynprm
                    Dim dp1 As dsProjekat.dynParametriRow = Me.saveDataSet.dynParametri.NewdynParametriRow
                    dp1.pripadaU = nu.jb
                    dp1.Name = dp.Name
                    dp1.funkcija = dp.funkcija
                    Me.saveDataSet.dynParametri.AdddynParametriRow(dp1)
                Next
            End With
        Next
        For Each CA In Me.Scena.CAList
            Dim nca As dsProjekat.CARow = Me.saveDataSet.CA.NewCARow
            With nca
                .bojaKocke = CA.bojaKocke
                .bojaLinije = CA.bojaLinije
                .h = CA.height
                .w = CA.width
                .l = CA.lenght
                .maxC = CA.MaximalCells
                .minC = CA.MinimalCells
                .turnOnC = CA.TurnOnCells
                .name = CA.Name
                .nivoa = CA.Levels
                .pripadaSceni = ns.jb
                .rule = CA.Rule
                .space = CA.Space
                .style = CA.Style
                .transparency = CA.Transparency
                .xPolja = CA.brojPoljaPoXosi
                .yPolja = CA.brojPoljaPoYosi
                .xPos = CA.xPosition
                .yPos = CA.yPosition
                .zPos = CA.zPosition
                .xRot = CA.xRotation
                .yRot = CA.yRotation
                .zRot = CA.zRotation
            End With
            Me.saveDataSet.CA.AddCARow(nca)
            Dim mval As Byte
            For Each mval In CA.matrice(0)
                Dim np As dsProjekat.MatrixRow = Me.saveDataSet.Matrix.NewMatrixRow
                np.pripadaCA = nca.jb
                np.value = mval
                Me.saveDataSet.Matrix.AddMatrixRow(np)
            Next
        Next
        For Each l In Me.Scena.sceneLights
            Dim nl As dsProjekat.LightsRow = Me.saveDataSet.Lights.NewLightsRow
            With nl
                .pripadaSceni = ns.jb
                .Ambient = l.Ambient.ToArgb.ToString
                .Diffuse = l.Diffuse.ToArgb.ToString
                .Direction = l.Direction
                .Enabled = l.Enabled
                .Name = l.Name
                .Position = l.Position
                .Specular = l.Specular.ToArgb.ToString
                .Type = l.Type
            End With
            Me.saveDataSet.Lights.AddLightsRow(nl)
        Next
        Me.saveDataSet.AcceptChanges()
        Try
            If Not tomem Then
                Me.saveDataSet.WriteXml(fileName)
                Me.saveFile = fileName
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub ValueInspectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValueInspectorToolStripMenuItem.Click, ToolStripButtonVal.Click
        If Not tfValueInspector.Visible Then
            tfValueInspector.Show(Me)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        MyBase.Close()
    End Sub

    Private Sub PropertyInspectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertyInspectorToolStripMenuItem.Click, ToolStripButtonProp.Click
        Try
            tfOsobine.Show(Me)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NewUVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewUVToolStripMenuItem.Click, ToolStripButtonNewUV.Click
        Dim naz As String = InputBox("UV Name:")
        If naz <> "" Then
            Dim noviUV As New ClassUV(naz)
            Me.Scena.UVList.Add(noviUV)
            Me.Scena.SelectedObject = noviUV
        End If
    End Sub

    Private Sub DeleteSelectedUVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSelectedUVToolStripMenuItem.Click, ToolStripButtonDelete.Click
        Me.addUndoData()
        Try
            Me.Scena.UVList.Remove(Me.Scena.SelectedObject)
        Catch ex As Exception
            Try
                Me.Scena.UList.Remove(Me.Scena.SelectedObject)
            Catch ex1 As Exception
                Try
                    Me.Scena.CAList.Remove(Me.Scena.SelectedObject)
                Catch ex2 As Exception
                    Try
                        Me.Scena.LSList.Remove(Me.Scena.SelectedObject)
                    Catch ex3 As Exception

                    End Try
                End Try
            End Try
        End Try
    End Sub

    Private Sub NewPredefinedUVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewPredefinedUVToolStripMenuItem.Click, ToolStripButtonUVp.Click
        If dfPredefined.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Me.Scena.UVList.Add(dfPredefined.Scena.UVList(0))
                Me.Scena.SelectedObject = dfPredefined.Scena.UVList(0)
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub ScenePropertiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScenePropertiToolStripMenuItem.Click
        Try
            Me.Scena.SelectedObject = Me.Scena
            If Not tfOsobine.Visible Then tfOsobine.Show(Me)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SelectByNameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectByNameToolStripMenuItem.Click, ToolStripButtonSelect.Click
        If dfUVLista.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim si As Integer = dfUVLista.ListBox1.SelectedIndex
                If si > (Me.Scena.UVList.Count + Me.Scena.UList.Count + Me.Scena.CAList.Count - 1) Then
                    Me.Scena.SelectedObject = Me.Scena.LSList(si - Me.Scena.CAList.Count - Me.Scena.UList.Count - Me.Scena.UVList.Count)
                ElseIf si > (Me.Scena.UVList.Count + Me.Scena.UList.Count - 1) Then
                    Me.Scena.SelectedObject = Me.Scena.CAList(si - Me.Scena.UList.Count - Me.Scena.UVList.Count)
                ElseIf si > Me.Scena.UVList.Count - 1 Then
                    Me.Scena.SelectedObject = Me.Scena.UList(si - Me.Scena.UVList.Count)
                Else
                    Me.Scena.SelectedObject = Me.Scena.UVList(si)
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        dfAbout.ShowDialog()
    End Sub

    Private Sub ContentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContentsToolStripMenuItem.Click, HelpToolStripButton.Click
        System.Diagnostics.Process.Start(My.Application.Info.DirectoryPath + "/help/index.html")
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        Dim sf As New SaveFileDialog
        sf.Filter = "JavaView (*.jvx)|*.jvx|AutoCAD (*.dxf)|*.dxf"
        If sf.ShowDialog = Windows.Forms.DialogResult.OK Then
            Select Case sf.FileName.Substring(sf.FileName.Length - 3, 3)
                Case "jvx"
                    Dim saveString As String = ""
                    saveString += "<?xml version=""1.0"" encoding=""ISO-8859-1"" standalone=""no""?>" + Chr(13)
                    saveString += "<!DOCTYPE jvx-model SYSTEM ""http://www.javaview.de/rsrc/jvx.dtd"">" + Chr(13)
                    saveString += "<jvx-model>" + Chr(13)
                    saveString += "<version type=""dump"">0.10</version>"
                    saveString += "<title>" + Me.Scena.Name + "</title>" + Chr(13)
                    saveString += "<geometries>" + Chr(13)
                    Dim UVE As ClassUV
                    Dim UE As ClassU
                    Dim xVal As New List(Of Single)
                    Dim yVal As New List(Of Single)
                    Dim zVal As New List(Of Single)
                    Dim a, b, c, d, u, v As Single
                    Dim i As Integer
                    For Each UVE In Me.Scena.UVList
                        saveString += "<geometry name=""" + UVE.Name + """>" + Chr(13)
                        UVE.refreshBuffer()
                        xVal = UVE.xVal
                        yVal = UVE.yVal
                        zVal = UVE.zVal
                        saveString += "<pointSet point=""hide"" dim=""3"">" + Chr(13)
                        saveString += "<points num=""" + xVal.Count.ToString + """>" + Chr(13)
                        For i = 0 To xVal.Count - 1
                            saveString += "<p>" + xVal(i).ToString.Replace(",", ".") + " " + yVal(i).ToString.Replace(",", ".") + " " + zVal(i).ToString.Replace(",", ".") + "</p>" + Chr(13)
                        Next
                        saveString += "<thickness>2.0</thickness>" + Chr(13)
                        saveString += "<color type=""rgb"">255 0 0</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</points>" + Chr(13)
                        saveString += "</pointSet>" + Chr(13)
                        Dim maxI As Integer = xVal.Count - 1
                        saveString += "<faceSet face=""show"" edge=""show"">" + Chr(13)
                        saveString += "<faces num=""" + (UVE.Udens * UVE.Vdens).ToString + """>" + Chr(13)
                        For u = 0 To UVE.Udens - 1
                            For v = 0 To UVE.Vdens - 1
                                a = u * (UVE.Vdens + 1) + v
                                b = u * (UVE.Vdens + 1) + v + 1
                                c = (u + 1) * (UVE.Vdens + 1) + v + 1
                                d = (u + 1) * (UVE.Vdens + 1) + v
                                If a > maxI Or b > maxI Or c > maxI Or d > maxI Then Exit For
                                saveString += "<f>" + a.ToString + " " + b.ToString + " " + c.ToString + " " + d.ToString + "</f>" + Chr(13)
                            Next
                        Next
                        saveString += "<color type=""rgb"">" + UVE.bojaPolja1.R.ToString + " " + UVE.bojaPolja1.G.ToString + " " + UVE.bojaPolja1.B.ToString + "</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</faces>" + Chr(13)
                        saveString += "</faceSet>" + Chr(13)
                        saveString += "<material><transparency visible=""show"">" + (1 - UVE.Transparency / 255).ToString.Replace(",", ".") + "</transparency></material>" + Chr(13)
                        saveString += "</geometry>" + Chr(13)
                    Next
                    For Each UE In Me.Scena.UList
                        saveString += "<geometry name=""" + UE.Name + """>" + Chr(13)
                        UE.refreshBuffer()
                        xVal = UE.xVal
                        yVal = UE.yVal
                        zVal = UE.zVal
                        saveString += "<pointSet point=""hide"" dim=""3"">" + Chr(13)
                        saveString += "<points num=""" + xVal.Count.ToString + """>" + Chr(13)
                        For i = 0 To xVal.Count - 1
                            saveString += "<p>" + xVal(i).ToString.Replace(",", ".") + " " + yVal(i).ToString.Replace(",", ".") + " " + zVal(i).ToString.Replace(",", ".") + "</p>" + Chr(13)
                        Next
                        saveString += "<thickness>2.0</thickness>" + Chr(13)
                        saveString += "<color type=""rgb"">255 0 0</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</points>" + Chr(13)
                        saveString += "</pointSet>" + Chr(13)
                        Dim maxI As Integer = xVal.Count - 1
                        saveString += "<lineSet line=""show"">" + Chr(13)
                        saveString += "<lines num=""" + (UE.Udens - 1).ToString + """>" + Chr(13)
                        For u = 0 To UE.Udens - 1
                            a = u
                            b = u + 1
                            If a > maxI Or b > maxI Then Exit For
                            saveString += "<l>" + a.ToString + " " + b.ToString + "</l>" + Chr(13)
                        Next
                        saveString += "<color type=""rgb"">" + UE.bojaLinija.R.ToString + " " + UE.bojaLinija.G.ToString + " " + UE.bojaLinija.B.ToString + "</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</lines>" + Chr(13)
                        saveString += "</lineSet>" + Chr(13)
                        saveString += "<material><transparency visible=""show"">" + (1 - UE.Transparency / 255).ToString.Replace(",", ".") + "</transparency></material>" + Chr(13)
                        saveString += "</geometry>" + Chr(13)
                    Next
                    saveString += "</geometries>" + Chr(13)
                    saveString += "</jvx-model>" + Chr(13)
                    Try
                        My.Computer.FileSystem.WriteAllText(sf.FileName, saveString, False)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Information)
                    End Try
                Case "dxf"
                    Dim saveString As String = ""
                    FileOpen(1, sf.FileName, OpenMode.Output)
                    PrintLine(1, 0)
                    PrintLine(1, "SECTION")
                    PrintLine(1, 2)
                    PrintLine(1, "HEADER")
                    PrintLine(1, 0)
                    PrintLine(1, "ENDSEC")
                    PrintLine(1, 0)
                    PrintLine(1, "SECTION")
                    PrintLine(1, 2)
                    PrintLine(1, "ENTITIES")
                    Dim UVE As ClassUV
                    Dim UE As ClassU
                    Dim CA As ClassCA
                    Dim LS As ClassLS
                    Dim xVal As New List(Of Single)
                    Dim yVal As New List(Of Single)
                    Dim zVal As New List(Of Single)
                    Dim a, b, c, d, u, v As Single
                    For Each UVE In Me.Scena.UVList
                        UVE.refreshBuffer()
                        xVal = UVE.xVal
                        yVal = UVE.yVal
                        zVal = UVE.zVal
                        Dim maxI As Integer = xVal.Count - 1
                        For u = 0 To UVE.Udens - 1
                            For v = 0 To UVE.Vdens - 1
                                a = u * (UVE.Vdens + 1) + v
                                b = u * (UVE.Vdens + 1) + v + 1
                                c = (u + 1) * (UVE.Vdens + 1) + v + 1
                                d = (u + 1) * (UVE.Vdens + 1) + v
                                If a > maxI Or b > maxI Or c > maxI Or d > maxI Then Exit For
                                PrintLine(1, 0)
                                PrintLine(1, "3DFACE")
                                PrintLine(1, 8)
                                PrintLine(1, UVE.Name)
                                PrintLine(1, 10)
                                PrintLine(1, Round(xVal(a), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 20)
                                PrintLine(1, Round(yVal(a), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 30)
                                PrintLine(1, Round(zVal(a), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 11)
                                PrintLine(1, Round(xVal(b), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 21)
                                PrintLine(1, Round(yVal(b), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 31)
                                PrintLine(1, Round(zVal(b), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 12)
                                PrintLine(1, Round(xVal(c), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 22)
                                PrintLine(1, Round(yVal(c), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 32)
                                PrintLine(1, Round(zVal(c), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 13)
                                PrintLine(1, Round(xVal(d), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 23)
                                PrintLine(1, Round(yVal(d), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 33)
                                PrintLine(1, Round(zVal(d), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            Next
                        Next
                    Next
                    For Each UE In Me.Scena.UList
                        UE.refreshBuffer()
                        xVal = UE.xVal
                        yVal = UE.yVal
                        zVal = UE.zVal
                        Dim maxI As Integer = xVal.Count - 1
                        For u = 0 To UE.Udens - 1
                            a = u
                            b = u + 1
                            If a > maxI Or b > maxI Then Exit For
                            PrintLine(1, 0)
                            PrintLine(1, "LINE")
                            PrintLine(1, 8)
                            PrintLine(1, UE.Name)
                            PrintLine(1, 10)
                            PrintLine(1, Round(xVal(a), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 20)
                            PrintLine(1, Round(yVal(a), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 30)
                            PrintLine(1, Round(zVal(a), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 11)
                            PrintLine(1, Round(xVal(b), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 21)
                            PrintLine(1, Round(yVal(b), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 31)
                            PrintLine(1, Round(zVal(b), 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                        Next
                    Next
                    For Each CA In Me.Scena.CAList
                        Dim c1, cc As Integer
                        c1 = CA.DXFBuffer.Count - 5
                        For cc = 0 To c1 Step 4
                            PrintLine(1, 0)
                            PrintLine(1, "3DFACE")
                            PrintLine(1, 8)
                            PrintLine(1, CA.Name)
                            PrintLine(1, 10)
                            PrintLine(1, Round(CA.DXFBuffer(cc).X + CA.xpolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 20)
                            PrintLine(1, Round(CA.DXFBuffer(cc).Y + CA.ypolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 30)
                            PrintLine(1, Round(CA.DXFBuffer(cc).Z + CA.zpolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 11)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 1).X + CA.xpolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 21)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 1).Y + CA.ypolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 31)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 1).Z + CA.zpolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 12)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 2).X + CA.xpolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 22)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 2).Y + CA.ypolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 32)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 2).Z + CA.zpolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 13)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 3).X + CA.xpolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 23)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 3).Y + CA.ypolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 33)
                            PrintLine(1, Round(CA.DXFBuffer(cc + 3).Z + CA.zpolozaj, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                        Next
                    Next
                    For Each LS In Me.Scena.LSList
                        If LS.Shape = ClassLS.oblik.line Then
                            Dim c1, cc As Integer
                            c1 = LS.LineBuffer.Count - 3
                            For cc = 0 To c1 Step 2
                                PrintLine(1, 0)
                                PrintLine(1, "LINE")
                                PrintLine(1, 8)
                                PrintLine(1, LS.Name)
                                PrintLine(1, 10)
                                PrintLine(1, Round(LS.LineBuffer(cc).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 20)
                                PrintLine(1, Round(LS.LineBuffer(cc).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 30)
                                PrintLine(1, Round(LS.LineBuffer(cc).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 11)
                                PrintLine(1, Round(LS.LineBuffer(cc + 1).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 21)
                                PrintLine(1, Round(LS.LineBuffer(cc + 1).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 31)
                                PrintLine(1, Round(LS.LineBuffer(cc + 1).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            Next
                        Else
                            Dim c1, cc As Integer
                            c1 = LS.DXFBuffer.Count - 5
                            For cc = 0 To c1 Step 4
                                PrintLine(1, 0)
                                PrintLine(1, "3DFACE")
                                PrintLine(1, 8)
                                PrintLine(1, LS.Name)
                                PrintLine(1, 10)
                                PrintLine(1, Round(LS.DXFBuffer(cc).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 20)
                                PrintLine(1, Round(LS.DXFBuffer(cc).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 30)
                                PrintLine(1, Round(LS.DXFBuffer(cc).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 11)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 1).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 21)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 1).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 31)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 1).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 12)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 2).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 22)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 2).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 32)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 2).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 13)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 3).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 23)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 3).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 33)
                                PrintLine(1, Round(LS.DXFBuffer(cc + 3).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            Next
                        End If
                    Next
                    PrintLine(1, 0)
                    PrintLine(1, "ENDSEC")
                    PrintLine(1, 0)
                    PrintLine(1, "EOF")
                    FileClose(1)
            End Select
        End If
    End Sub

    Private Sub NewUCurveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewUCurveToolStripMenuItem.Click, ToolStripButton1.Click
        Dim naz As String = InputBox("U Name:")
        If naz <> "" Then
            Dim noviU As New ClassU(naz)
            Me.Scena.UList.Add(noviU)
            Me.Scena.SelectedObject = noviU
        End If
    End Sub

    Private Sub ImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportToolStripMenuItem.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "X Files (*.x)|*.x|3D Studio (*.3ds)|*.3ds"
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            Select Case opf.FileName.Split(".")(opf.FileName.Split(".").Length - 1)
                Case "x", "X"
                    Dim sModel As String
                    Dim mtrlBuffer() As ExtendedMaterial
                    Dim X As Int32
                    Dim sTemp As String

                    Dim sAppPath As String

                    sAppPath = AppDomain.CurrentDomain.BaseDirectory
                    If Mid(sAppPath, Len(sAppPath)) <> "\" Then sAppPath = sAppPath & "\"

                    sModel = opf.FileName
                    If Dir$(sModel) <> "" Then
                        Me.Scena.moMesh = Mesh.FromFile(sModel, MeshFlags.Managed, cf3D.device, mtrlBuffer)


                        'materijali i teksture
                        Me.Scena.mlMeshMaterials = mtrlBuffer.Length
                        ReDim Me.Scena.moMaterials(Me.Scena.mlMeshMaterials - 1)
                        ReDim Me.Scena.moTextures(Me.Scena.mlMeshMaterials - 1)
                        'load texture i materijali
                        For X = 0 To mtrlBuffer.Length - 1
                            Me.Scena.moMaterials(X) = mtrlBuffer(X).Material3D
                            Me.Scena.moMaterials(X).Ambient = Me.Scena.moMaterials(X).Diffuse
                            If mtrlBuffer(X).TextureFilename <> "" Then
                                sTemp = opf.InitialDirectory & mtrlBuffer(X).TextureFilename
                                Me.Scena.moTextures(X) = TextureLoader.FromFile(cf3D.device, sTemp)
                            End If
                        Next X
                    Else
                    End If
                Case "3ds", "3DS"
                    'Try
                    Shell(My.Application.Info.DirectoryPath + "\util\conv3ds.exe -m -o ""tmpx.x"" " + opf.FileName)
                    System.Threading.Thread.Sleep(2000)
                    Dim sModel As String
                    Dim mtrlBuffer() As ExtendedMaterial
                    Dim X As Int32
                    Dim sTemp As String

                    Dim sAppPath As String

                    sAppPath = AppDomain.CurrentDomain.BaseDirectory
                    If Mid(sAppPath, Len(sAppPath)) <> "\" Then sAppPath = sAppPath & "\"

                    sModel = System.IO.Path.GetDirectoryName(opf.FileName) + "\tmpx.x"
                    If Dir$(sModel) <> "" Then
                        Me.Scena.moMesh = Mesh.FromFile(sModel, MeshFlags.Managed, cf3D.device, mtrlBuffer)


                        'materijali i teksture
                        Me.Scena.mlMeshMaterials = mtrlBuffer.Length
                        ReDim Me.Scena.moMaterials(Me.Scena.mlMeshMaterials - 1)
                        ReDim Me.Scena.moTextures(Me.Scena.mlMeshMaterials - 1)
                        'load texture i materijali
                        For X = 0 To mtrlBuffer.Length - 1
                            Me.Scena.moMaterials(X) = mtrlBuffer(X).Material3D

                            If mtrlBuffer(X).TextureFilename <> "" Then
                                sTemp = System.IO.Path.GetDirectoryName(opf.FileName) + "\" & mtrlBuffer(X).TextureFilename
                                Me.Scena.moTextures(X) = TextureLoader.FromFile(cf3D.device, sTemp)
                            End If
                        Next X
                    Else
                    End If
                    'Catch ex As Exception
                    'MsgBox(ex.Message)
                    'End Try

            End Select
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.Scena.sceneFrameAdd(New ClassFrame(cf3D.angleX, cf3D.angleY, cf3D.angleZ, cf3D.xcam, cf3D.ycam, cf3D.zcam))
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim ax, ay, az, xc, yc, zc As Single
        Dim prolazi As Integer = 30
        Dim prolaz As Integer = 0
        Dim i As Integer
        If Me.Scena.SceneFrames.Count > 1 Then
            For i = 0 To Me.Scena.SceneFrames.Count - 2
                While prolazi > prolaz
                    System.Threading.Thread.Sleep(30)
                    ax = Me.Scena.SceneFrames(i).ax + (Me.Scena.SceneFrames(i + 1).ax - Me.Scena.SceneFrames(i).ax) * prolaz / prolazi
                    ay = Me.Scena.SceneFrames(i).ay + (Me.Scena.SceneFrames(i + 1).ay - Me.Scena.SceneFrames(i).ay) * prolaz / prolazi
                    az = Me.Scena.SceneFrames(i).az + (Me.Scena.SceneFrames(i + 1).az - Me.Scena.SceneFrames(i).az) * prolaz / prolazi
                    xc = Me.Scena.SceneFrames(i).xc + (Me.Scena.SceneFrames(i + 1).xc - Me.Scena.SceneFrames(i).xc) * prolaz / prolazi
                    yc = Me.Scena.SceneFrames(i).yc + (Me.Scena.SceneFrames(i + 1).yc - Me.Scena.SceneFrames(i).yc) * prolaz / prolazi
                    zc = Me.Scena.SceneFrames(i).zc + (Me.Scena.SceneFrames(i + 1).zc - Me.Scena.SceneFrames(i).zc) * prolaz / prolazi
                    cf3D.angleX = ax
                    cf3D.angleY = ay
                    cf3D.angleZ = az
                    cf3D.xcam = xc
                    cf3D.ycam = yc
                    cf3D.zcam = zc
                    cf3D.Timer1_Tick(sender, e)
                    prolaz += 1
                End While
                prolaz = 0
            Next
        End If
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Me.currentFrame -= 1
        Try
            If Me.currentFrame < 0 Then Me.currentFrame = Me.Scena.SceneFrames.Count - 1
            cf3D.angleX = Me.Scena.SceneFrames(Me.currentFrame).ax
            cf3D.angleY = Me.Scena.SceneFrames(Me.currentFrame).ay
            cf3D.angleZ = Me.Scena.SceneFrames(Me.currentFrame).az
            cf3D.xcam = Me.Scena.SceneFrames(Me.currentFrame).xc
            cf3D.ycam = Me.Scena.SceneFrames(Me.currentFrame).yc
            cf3D.zcam = Me.Scena.SceneFrames(Me.currentFrame).zc
            cf3D.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        Me.currentFrame += 1
        Try
            If Me.currentFrame > (Me.Scena.SceneFrames.Count - 1) Then Me.currentFrame = 0
            cf3D.angleX = Me.Scena.SceneFrames(Me.currentFrame).ax
            cf3D.angleY = Me.Scena.SceneFrames(Me.currentFrame).ay
            cf3D.angleZ = Me.Scena.SceneFrames(Me.currentFrame).az
            cf3D.xcam = Me.Scena.SceneFrames(Me.currentFrame).xc
            cf3D.ycam = Me.Scena.SceneFrames(Me.currentFrame).yc
            cf3D.zcam = Me.Scena.SceneFrames(Me.currentFrame).zc
            cf3D.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Try
            If MsgBox("Are you sure?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.Scena.SceneFrames.RemoveAt(Me.currentFrame)
                Me.ToolStripButton5_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BnewCA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BnewCA.Click, NewCAToolStripMenuItem.Click
        If dfCA.Visible Then dfCA.Close()
        dfCA.StartPosition = FormStartPosition.CenterParent
        dfCA.TableLayoutPanel1.Visible = True
        dfCA.CA = New ClassCA(6, 6, 3)
        If dfCA.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not Me.Scena.CAList.Contains(dfCA.CA) Then
                Me.Scena.CAList.Add(dfCA.CA)
            End If
            Try
                Me.Scena.SelectedObject = dfCA.CA
                Me.CAInspectorToolStripMenuItem_Click(sender, e)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub CAInspectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CAInspectorToolStripMenuItem.Click, TSBCAEditor.Click
        If dfCA.Visible Then dfCA.Close()
        dfCA.StartPosition = FormStartPosition.Manual
        dfCA.Top = ydfca
        dfCA.Left = xdfca
        If Me.Scena.SelectedObject.GetType Is GetType(ClassCA) Then
            dfCA.TableLayoutPanel1.Visible = False
            dfCA.CA = Me.Scena.SelectedObject
            dfCA.Show(Me)
        Else
            MsgBox("Select CA first!")
        End If
    End Sub

    Private Sub Export2DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2DToolStripMenuItem.Click
        Dim sd As New SaveFileDialog
        sd.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png"
        If sd.ShowDialog = Windows.Forms.DialogResult.OK Then
            cf3D.saveSlicka(sd.FileName)
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click, TSBCopy.Click
        If Me.Scena.SelectedObject.GetType Is GetType(ClassCA) Then
            'Dim mem As New System.IO.MemoryStream()
            'Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            'Dim obj As New ClassCA(Me.Scena.selectedCA.xPolja, Me.Scena.selectedCA.yPolja, Me.Scena.selectedCA.brojNivoa)
            Try
                'bin.Serialize(mem, obj)
                My.Computer.Clipboard.SetData("ClassCA", Me.Scena.selectedCA)
            Catch ex As Exception
                MsgBox("Your object cannot be serialized." + " The reason is: " + ex.ToString())
            End Try
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click, TSBPaste.Click
        If My.Computer.Clipboard.ContainsData("ClassCA") And Not My.Computer.Clipboard.GetData("ClassCA") Is Nothing Then
            Dim CA As ClassCA
            Dim obj As Object = My.Computer.Clipboard.GetData("ClassCA")
            If obj.GetType Is GetType(ClassCA) Then
                CA = obj
                CA.AfterCopy()
                CA.Name = CA.Name + " copy"
                CA.xPosition += 20
                CA.yPosition += 20
                Me.Scena.CAList.Add(CA)
                Me.Scena.SelectedObject = CA
            End If
        End If
    End Sub
    Public Function copyScena(ByVal sourceScena As ClassScena) As ClassScena
        Dim scenaN As New ClassScena
        Me.fileSave("", True)
        scenaN = Me.openFile("", True)
        Return scenaN
    End Function
    Public Sub addUndoData()
        Me.undoData.Add(Me.copyScena(Me.Scena))
        If Me.undoData.Count > undoRedoLimit Then
            Me.undoData.RemoveAt(0)
        End If
    End Sub

    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click, TSBUndo.Click
        Try
            If Me.undoData.Count > 0 Then
                Me.redoData.Add(Me.copyScena(Me.Scena))
                If Me.redoData.Count > undoRedoLimit Then
                    Me.redoData.RemoveAt(0)
                End If
                Me.Scena = Me.undoData(Me.undoData.Count - 1)
                If dfCA.Visible Then dfCA.Close()
                Me.undoData.RemoveAt(Me.undoData.Count - 1)
                tfOsobine.scn = Me.Scena
                tfOsobine.PropertyGridUV.Refresh()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click, TSBRedo.Click
        Try
            If Me.redoData.Count > 0 Then
                Me.addUndoData()
                Me.Scena = Me.redoData(Me.redoData.Count - 1)
                If dfCA.Visible Then dfCA.Close()
                Me.redoData.RemoveAt(Me.redoData.Count - 1)
                tfOsobine.scn = Me.Scena
                tfOsobine.PropertyGridUV.Refresh()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Scena_propertyChanged() Handles Scena.propertyChanged
        cf3D.Timer1.Stop()
        Me.Cursor = Cursors.WaitCursor
        Me.addUndoData()
        cf3D.Timer1.Start()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Scena_SelectionChanged() Handles Scena.SelectionChanged
        If Me.Scena.SelectedObject.GetType Is GetType(ClassCA) Then
            Me.CAInspectorToolStripMenuItem_Click(New Object, New EventArgs)
        End If
    End Sub

    Private Sub NewLSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewLSToolStripMenuItem.Click, bNewLS.Click
        Dim ime As String = ""
        ime = InputBox("LS name:", , "New LS")
        If ime <> "" Then
            Me.Scena.LSList.Add(New ClassLS(ime))
            Try
                Me.Scena.SelectedObject = Me.Scena.LSList(Me.Scena.LSList.Count - 1)
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class
