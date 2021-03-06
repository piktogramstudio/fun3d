Imports System.Math
Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports RMA.OpenNURBS
Public Class mf
    Public WithEvents Scena As New ClassScena
    Public saveFile As String = ""
    Public saveDataSet As New dsProjekat
    Dim currentFrame As Integer = 0
    Public xdfca, ydfca As Integer
    Public undoData As New Stack(Of IO.MemoryStream)
    Public undoCall As String = ""
    Public redoData As New Stack(Of IO.MemoryStream)
    Public redoCall As New Stack(Of String)
    Dim undoRedoLimit As Integer = 10
    Dim clipboardObject As Object = Nothing
    Dim ex, ey As Single
    Private Sub mf_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        cf3D.Timer1.Stop()
        cf3D.Dispose()
    End Sub
    Private Sub mf_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Console.SetOut(New ClassTextWriter(Me.rtbConsole))
        Console.WriteLine("Fun3D V " + My.Application.Info.Version.ToString)
        Console.WriteLine("Copyright © Bojan Mitrović 2008-2012.")
        Dim lic As String = My.Computer.FileSystem.ReadAllText("hello.txt")
        Console.WriteLine(lic)
        Console.WriteLine("Ready.")
        ydfca = Me.Height - dfCA.Height - 20
        xdfca = Me.Width - dfCA.Width - 20
        tfOsobine.PropertyGridUV.SelectedObject = Scena
        tfOsobine.Show(Me)
        Me.NewToolStripMenuItem_Click(sender, e)
        Dim fn As String
        'Me.Scena.backgroundColor = Color.White
        If My.Application.CommandLineArgs.Count > 0 Then
            fn = My.Application.CommandLineArgs(0)
            Me.Scena = mdTools.openFile(fn)
            Me.Scena.afterPaste(cf3D.device)
            tfOsobine.scn = Me.Scena
            Me.Scena.SelectedObject = Me.Scena
        End If
        cf3D.Timer1.Enabled = True
        addUndoData("")
    End Sub
    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click, NewToolStripButton.Click
        Me.P3D.Controls.Clear()
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
        cf3D.Dock = DockStyle.Fill
        cf3D.TopLevel = False
        cf3D.Show()
        Me.P3D.Controls.Add(cf3D)
        cf3D.Focus()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim sd As New SaveFileDialog
        sd.Filter = "Fun3D file (*.f3dx)|*.f3dx|Old Fun3D file (*.f3d)|*.f3d"
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

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click, OpenToolStripMenuItem.Click
        Dim opf As New OpenFileDialog
        If Me.saveFile = "" Then
            opf.InitialDirectory = My.Application.Info.DirectoryPath + "\samples"
        End If
        opf.Filter = "Fun3D file (*.f3dx)|*.f3dx|Old Fun3D file (*.f3d)|*.f3d|All files|*.*"
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fnm As String = opf.FileName
            Dim scn As ClassScena = openFile(fnm)
            If scn IsNot Nothing Then
                Me.Scena = openFile(fnm)
                Me.Scena.afterPaste(cf3D.device)
                tfOsobine.scn = Me.Scena
                Me.Scena.SelectedObject = Me.Scena
            End If
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
            tfNavigacija.NUDxcam.Value = CDec(xc)
            tfNavigacija.NUDycam.Value = CDec(yc)
            tfNavigacija.NUDzcam.Value = CDec(zc)
            tfNavigacija.NUDugaoX.Value = CDec(ax)
            tfNavigacija.NUDugaoY.Value = CDec(ay)
            tfNavigacija.NUDugaoZ.Value = CDec(az)
        Else
            tfNavigacija.Close()
        End If
    End Sub
    Private Sub fileSave(ByVal fileName As String, Optional ByVal tomem As Boolean = False)
       
        If fileName.Substring(fileName.Length - 4, 4) = "f3dx" Then
            Try
                Dim mem As New System.IO.FileStream(fileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                Dim obj As ClassScena = Me.Scena

                bin.Serialize(mem, obj)
                mem.Close()
                Me.saveFile = fileName
            Catch ex As Exception
                MsgBox("ERROR." + " The reason is: " + ex.ToString())
            End Try
        Else
            Dim saveString As String = ""
            Dim pl As ClassParametri
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
                .FillMode = CByte(Me.Scena.FillMode)
                .GridColor = Me.Scena.colorGrid.ToArgb.ToString
                .GridMaxValue = Me.Scena.gridMaxV
                .GridMinValue = Me.Scena.gridMinV
                .GridStep = Me.Scena.stepGrid
                .Lighting = Me.Scena.Lighting
                .Name = Me.Scena.Name
                .ShadeMode = CByte(Me.Scena.ShadeMode)
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
                    .LineColor = UV.LineColor.ToArgb.ToString
                    .Name = UV.Name
                    .UDensity = UV.Udens
                    .UMax = UV.maxU
                    .UMin = UV.minU
                    .VDensity = UV.Vdens
                    .VMax = UV.maxV
                    .VMin = UV.minV
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
                    For Each pl In UV.Parameters
                        Dim np As dsProjekat.parametriRow = Me.saveDataSet.parametri.NewparametriRow
                        np.pripadaUV = nuv.jb
                        np.Name = pl.Name
                        np.Value = pl.value
                        np.stepS = pl.sliderStep
                        np.maxS = pl.sliderMaximum
                        np.minS = CStr(pl.sliderMinimum)
                        Me.saveDataSet.parametri.AddparametriRow(np)
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
                    .LineColor = U.LineColor.ToArgb.ToString
                    .Name = U.Name
                    .UDensity = CInt(U.Udens)
                    .UMax = CSng(U.Umax)
                    .UMin = CSng(U.Umin)
                    .XFun = U.funX
                    .XPos = U.transform.tx
                    .XScale = U.transform.sx
                    .YFun = U.funY
                    .YPos = U.transform.ty
                    .YScale = U.transform.sy
                    .ZFun = U.funZ
                    .ZPos = U.transform.tz
                    .ZScale = U.transform.sz
                    .maxSU = CSng(U.sliderMaximumUmin)
                    .minSU = CSng(U.sliderMinimumUmin)
                    .stepSU = CSng(U.sliderStepUmin)
                    .maxSUm = CSng(U.sliderMaximumUmax)
                    .minSUm = CSng(U.sliderMinimumUmax)
                    .stepSUm = CSng(U.sliderStepUmax)
                    .Transparency = U.Transparency
                    Me.saveDataSet.U.AddURow(nu)
                    For Each pl In U.Parameters
                        Dim np As dsProjekat.parametriRow = Me.saveDataSet.parametri.NewparametriRow
                        np.pripadaU = nu.jb
                        np.Name = pl.Name
                        np.Value = pl.value
                        np.stepS = pl.sliderStep
                        np.maxS = pl.sliderMaximum
                        np.minS = CStr(pl.sliderMinimum)
                        Me.saveDataSet.parametri.AddparametriRow(np)
                    Next
                End With
            Next
            For Each CA In Me.Scena.CAList
                Dim nca As dsProjekat.CARow = Me.saveDataSet.CA.NewCARow
                With nca
                    .bojaKocke = CA.CubeColor.ToArgb
                    .bojaLinije = CA.LineColor.ToArgb
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
                    .transparency = CStr(CA.Transparency)
                    .xPolja = CA.columns
                    .yPolja = CA.rows
                    .xPos = CA.xPosition
                    .yPos = CA.yPosition
                    .zPos = CA.zPosition
                    .xRot = CA.xRotation
                    .yRot = CA.yRotation
                    .zRot = CA.zRotation
                End With
                Me.saveDataSet.CA.AddCARow(nca)
                Dim mval As Byte
                For Each mval In CA.matrices(0)
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
                    .Type = CByte(l.Type)
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
        End If
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

    Private Sub NewUVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonNewUV.Click, NewUVToolStripMenuItem.Click
        Dim naz As String = InputBox("UV Name:")
        If naz <> "" Then
            addUndoData("Object created")
            Dim noviUV As New ClassUV(naz, cf3D.device)
            Me.Scena.UVList.Add(noviUV)
            Me.Scena.SelectedObject = noviUV

        End If
    End Sub
    Public Sub addRedoData()
        Try
            Dim mem As New System.IO.MemoryStream()
            Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim obj As ClassScena = Me.Scena
            bin.Serialize(mem, obj)
            Me.redoData.Push(mem)
            If Me.redoData.Count > Me.undoRedoLimit Then
                Me.redoData.TrimExcess()
            End If
        Catch ex As Exception
            MsgBox("ERROR." + " The reason is: " + ex.ToString())
        End Try
    End Sub
    Public Sub addUndoData(ByVal undoCall As String)
        Try
            If undoCall <> "Redo" Then Me.redoData.Clear()
            Dim mem As New System.IO.MemoryStream()
            Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim obj As ClassScena = Me.Scena
            bin.Serialize(mem, obj)
            Me.undoData.Push(mem)
            Me.undoCall = undoCall
            If Me.undoData.Count > Me.undoRedoLimit Then
                Dim i As Integer
                Dim ns As New Stack(Of IO.MemoryStream)
                For i = 1 To Me.undoData.Count
                    ns.Push(Me.undoData.Pop)
                Next
                ns.Pop()
                For i = 1 To ns.Count
                    Me.undoData.Push(ns.Pop)
                Next
            End If
        Catch ex As Exception
            Console.WriteLine("ERROR." + " The reason is: " + ex.Message)
        End Try
    End Sub
    Private Sub DeleteSelectedUVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSelectedUVToolStripMenuItem.Click, ToolStripButtonDelete.Click
        If Me.Scena.SelectedObject.GetType IsNot GetType(ClassScena) Then
            addUndoData("Delete object")
        End If
        Select Case Me.Scena.SelectedObject.GetType.FullName
            Case GetType(ClassPacking).FullName
                Me.Scena.PackingList.Remove(CType(Me.Scena.SelectedObject, ClassPacking))
                Me.Scena.SelectedObject = Me.Scena
            Case GetType(ClassCrackedPoly).FullName
                Me.Scena.CrackedPolyList.Remove(CType(Me.Scena.SelectedObject, ClassCrackedPoly))
                Me.Scena.SelectedObject = Me.Scena
            Case GetType(ClassUV).FullName
                Me.Scena.UVList.Remove(CType(Me.Scena.SelectedObject, ClassUV))
                Me.Scena.SelectedObject = Me.Scena
        End Select
        ' TODO Finish delete
        Try
            Me.Scena.UList.Remove(CType(Me.Scena.SelectedObject, ClassU))
            Me.Scena.SelectedObject = Me.Scena
        Catch ex1 As Exception
            Try
                Me.Scena.CAList.Remove(CType(Me.Scena.SelectedObject, ClassCA))
                Me.Scena.SelectedObject = Me.Scena
            Catch ex2 As Exception
                Try
                    Me.Scena.LSList.Remove(CType(Me.Scena.SelectedObject, ClassLS))
                    Me.Scena.SelectedObject = Me.Scena
                Catch ex3 As Exception
                    Try
                        Me.Scena.MeshList.Remove(CType(Me.Scena.SelectedObject, ClassMesh))
                        Me.Scena.SelectedObject = Me.Scena
                    Catch ex4 As Exception
                        Try
                            Me.Scena.ISOList.Remove(CType(Me.Scena.SelectedObject, ClassISO))
                            Me.Scena.SelectedObject = Me.Scena
                        Catch ex5 As Exception

                        End Try
                    End Try
                End Try
            End Try
        End Try
    End Sub

    Private Sub NewPredefinedUVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonUVp.Click, NewPredefinedUVToolStripMenuItem.Click
        If dfPredefined.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                addUndoData("Object created")
                Dim UVC As ClassUV
                Dim UC As ClassU
                Dim ISO As ClassISO
                Dim CA As ClassCA
                Dim LS As ClassLS
                Dim CM As ClassMesh
                For Each UVC In dfPredefined.Scena.UVList
                    Me.Scena.SelectedObject = UVC
                    Me.CopyToolStripMenuItem_Click(sender, e)
                    Me.PasteToolStripMenuItem_Click(sender, e)
                Next
                For Each UC In dfPredefined.Scena.UList
                    Me.Scena.SelectedObject = UC
                    Me.CopyToolStripMenuItem_Click(sender, e)
                    Me.PasteToolStripMenuItem_Click(sender, e)
                Next
                For Each ISO In dfPredefined.Scena.ISOList
                    Me.Scena.SelectedObject = ISO
                    Me.CopyToolStripMenuItem_Click(sender, e)
                    Me.PasteToolStripMenuItem_Click(sender, e)
                Next
                For Each CA In dfPredefined.Scena.CAList
                    Me.Scena.SelectedObject = CA
                    Me.CopyToolStripMenuItem_Click(sender, e)
                    Me.PasteToolStripMenuItem_Click(sender, e)
                Next
                For Each CM In dfPredefined.Scena.MeshList
                    Me.Scena.SelectedObject = CM
                    Me.CopyToolStripMenuItem_Click(sender, e)
                    Me.PasteToolStripMenuItem_Click(sender, e)
                Next
                For Each LS In dfPredefined.Scena.LSList
                    Me.Scena.SelectedObject = LS
                    Me.CopyToolStripMenuItem_Click(sender, e)
                    Me.PasteToolStripMenuItem_Click(sender, e)
                Next
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
                If si > (Me.Scena.UVList.Count + Me.Scena.UList.Count + Me.Scena.CAList.Count + Me.Scena.LSList.Count + Me.Scena.ISOList.Count + Me.Scena.MeshList.Count - 1) Then
                    Me.Scena.SelectedObject = Me.Scena.CrackedPolyList(si - Me.Scena.MeshList.Count - Me.Scena.ISOList.Count - Me.Scena.LSList.Count - Me.Scena.CAList.Count - Me.Scena.UList.Count - Me.Scena.UVList.Count)
                ElseIf si > (Me.Scena.UVList.Count + Me.Scena.UList.Count + Me.Scena.CAList.Count + Me.Scena.LSList.Count + Me.Scena.ISOList.Count - 1) Then
                    Me.Scena.SelectedObject = Me.Scena.MeshList(si - Me.Scena.ISOList.Count - Me.Scena.LSList.Count - Me.Scena.CAList.Count - Me.Scena.UList.Count - Me.Scena.UVList.Count)
                ElseIf si > (Me.Scena.UVList.Count + Me.Scena.UList.Count + Me.Scena.CAList.Count + Me.Scena.LSList.Count - 1) Then
                    Me.Scena.SelectedObject = Me.Scena.ISOList(si - Me.Scena.LSList.Count - Me.Scena.CAList.Count - Me.Scena.UList.Count - Me.Scena.UVList.Count)
                ElseIf si > (Me.Scena.UVList.Count + Me.Scena.UList.Count + Me.Scena.CAList.Count - 1) Then
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
        mfHelp.Show(Me)
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        Dim sf As New SaveFileDialog
        sf.Filter = "JavaView (*.jvx)|*.jvx|AutoCAD (*.dxf)|*.dxf|Rhinoceros (*.3DM)|*.3DM"
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
                    Dim CAE As ClassCA
                    Dim CP As ClassCrackedPoly
                    Dim a, b, c, d, u, v As Single
                    Dim i As Integer
                    For Each CP In Me.Scena.CrackedPolyList
                        saveString += "<geometry name=""" + CP.Name + """>" + Chr(13)
                        CP.refreshBuffer()
                        saveString += "<pointSet point=""hide"" dim=""3"">" + Chr(13)
                        saveString += "<points num=""" + CP.triangles.Count.ToString + """>" + Chr(13)
                        For i = 0 To CP.triangles.Count - 1
                            saveString += "<p>" + CP.triangles(i).X.ToString.Replace(",", ".") + " " + CP.triangles(i).Y.ToString.Replace(",", ".") + " " + CP.triangles(i).Z.ToString.Replace(",", ".") + "</p>" + Chr(13)
                        Next
                        saveString += "<thickness>2.0</thickness>" + Chr(13)
                        saveString += "<color type=""rgb"">255 0 0</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</points>" + Chr(13)
                        saveString += "</pointSet>" + Chr(13)
                        Dim maxI As Integer = CP.triangles.Count - 1
                        saveString += "<faceSet face=""show"" edge=""show"">" + Chr(13)
                        saveString += "<faces num=""" + (CP.triangles.Count / 3).ToString + """>" + Chr(13)
                        For i = 0 To CP.triangles.Count - 1 Step 3
                            a = i
                            b = i + 1
                            c = i + 2
                            saveString += "<f>" + a.ToString + " " + b.ToString + " " + c.ToString + " " + "</f>" + Chr(13)

                        Next
                        saveString += "<color type=""rgb"">" + CP.color.R.ToString + " " + CP.color.G.ToString + " " + CP.color.B.ToString + "</color>" + Chr(13)
                    saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                    saveString += "</faces>" + Chr(13)
                    saveString += "</faceSet>" + Chr(13)
                        saveString += "<material><transparency visible=""show"">" + (1 - CP.transparency / 255).ToString.Replace(",", ".") + "</transparency></material>" + Chr(13)
                    saveString += "</geometry>" + Chr(13)
                    Next

                    For Each UVE In Me.Scena.UVList
                        saveString += "<geometry name=""" + UVE.Name + """>" + Chr(13)
                        UVE.refreshBuffer()
                        saveString += "<pointSet point=""hide"" dim=""3"">" + Chr(13)
                        saveString += "<points num=""" + UVE.vBuffer.Count.ToString + """>" + Chr(13)
                        For i = 0 To UVE.vBuffer.Count - 1
                            saveString += "<p>" + UVE.vBuffer(i).X.ToString.Replace(",", ".") + " " + UVE.vBuffer(i).Y.ToString.Replace(",", ".") + " " + UVE.vBuffer(i).Z.ToString.Replace(",", ".") + "</p>" + Chr(13)
                        Next
                        saveString += "<thickness>2.0</thickness>" + Chr(13)
                        saveString += "<color type=""rgb"">255 0 0</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</points>" + Chr(13)
                        saveString += "</pointSet>" + Chr(13)
                        Dim maxI As Integer = UVE.vBuffer.Count - 1
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
                        saveString += "<pointSet point=""hide"" dim=""3"">" + Chr(13)
                        saveString += "<points num=""" + UE.tgeom.vb.Length.ToString + """>" + Chr(13)
                        For i = 0 To UE.tgeom.vb.Length - 1
                            saveString += "<p>" + UE.tgeom.vb(i).X.ToString.Replace(",", ".") + " " + UE.tgeom.vb(i).Y.ToString.Replace(",", ".") + " " + UE.tgeom.vb(i).Z.ToString.Replace(",", ".") + "</p>" + Chr(13)
                        Next
                        saveString += "<thickness>2.0</thickness>" + Chr(13)
                        saveString += "<color type=""rgb"">255 0 0</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</points>" + Chr(13)
                        saveString += "</pointSet>" + Chr(13)
                        Dim maxI As Integer = UE.tgeom.vb.Length - 1
                        saveString += "<lineSet line=""show"">" + Chr(13)
                        saveString += "<lines num=""" + (CDbl(UE.Udens) - 1).ToString + """>" + Chr(13)
                        For u = 0 To CSng(UE.Udens) - 1
                            a = u
                            b = u + 1
                            If a > maxI Or b > maxI Then Exit For
                            saveString += "<l>" + a.ToString + " " + b.ToString + "</l>" + Chr(13)
                        Next
                        saveString += "<color type=""rgb"">" + UE.LineColor.R.ToString + " " + UE.LineColor.G.ToString + " " + UE.LineColor.B.ToString + "</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</lines>" + Chr(13)
                        saveString += "</lineSet>" + Chr(13)
                        saveString += "<material><transparency visible=""show"">" + (1 - UE.Transparency / 255).ToString.Replace(",", ".") + "</transparency></material>" + Chr(13)
                        saveString += "</geometry>" + Chr(13)
                    Next
                    For Each CAE In Me.Scena.CAList
                        saveString += "<geometry name=""" + CAE.Name + """>" + Chr(13)
                        CAE.refreshBuffer()
                        saveString += "<pointSet point=""hide"" dim=""3"">" + Chr(13)
                        saveString += "<points num=""" + CAE.DXFBuffer.Count.ToString + """>" + Chr(13)
                        For i = 0 To CAE.DXFBuffer.Count - 1
                            saveString += "<p>" + CAE.DXFBuffer(i).X.ToString.Replace(",", ".") + " " + CAE.DXFBuffer(i).Y.ToString.Replace(",", ".") + " " + CAE.DXFBuffer(i).Z.ToString.Replace(",", ".") + "</p>" + Chr(13)
                        Next
                        saveString += "<thickness>2.0</thickness>" + Chr(13)
                        saveString += "<color type=""rgb"">255 0 0</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</points>" + Chr(13)
                        saveString += "</pointSet>" + Chr(13)
                        saveString += "<faceSet face=""show"" edge=""show"">" + Chr(13)
                        saveString += "<faces num=""" + (CAE.DXFBuffer.Count / 4).ToString + """>" + Chr(13)
                        For i = 0 To CAE.DXFBuffer.Count - 1 Step 4
                            a = i
                            b = i + 1
                            c = i + 2
                            d = i + 3
                            saveString += "<f>" + a.ToString + " " + b.ToString + " " + c.ToString + " " + d.ToString + "</f>" + Chr(13)
                        Next
                        saveString += "<color type=""rgb"">" + CAE.CubeColor.R.ToString + " " + CAE.CubeColor.G.ToString + " " + CAE.CubeColor.B.ToString + "</color>" + Chr(13)
                        saveString += "<colorTag type=""rgb"">255 0 255</colorTag>" + Chr(13)
                        saveString += "</faces>" + Chr(13)
                        saveString += "</faceSet>" + Chr(13)
                        saveString += "<material><transparency visible=""show"">" + (1 - CAE.Transparency / 255).ToString.Replace(",", ".") + "</transparency></material>" + Chr(13)
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
                    Dim ISO As ClassISO
                    Dim CP As ClassCrackedPoly
                    Dim a, b, c, d, u, v As Single
                    For Each CP In Me.Scena.CrackedPolyList
                        CP.refreshBuffer()
                        Dim indice As Integer
                        For indice = 0 To CP.triangles.Count - 1 Step 3
                            PrintLine(1, 0)
                            PrintLine(1, "3DFACE")
                            PrintLine(1, 8)
                            PrintLine(1, CP.Name)
                            ' vertex 1
                            PrintLine(1, 10)
                            PrintLine(1, Round(CP.triangles(indice).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 20)
                            PrintLine(1, Round(CP.triangles(indice).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 30)
                            PrintLine(1, Round(CP.triangles(indice).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            ' vertex 2
                            PrintLine(1, 11)
                            PrintLine(1, Round(CP.triangles(indice + 1).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 21)
                            PrintLine(1, Round(CP.triangles(indice + 1).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 31)
                            PrintLine(1, Round(CP.triangles(indice + 1).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            ' vertex 2
                            PrintLine(1, 12)
                            PrintLine(1, Round(CP.triangles(indice + 1).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 22)
                            PrintLine(1, Round(CP.triangles(indice + 1).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 32)
                            PrintLine(1, Round(CP.triangles(indice + 1).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            ' vertex 3
                            PrintLine(1, 13)
                            PrintLine(1, Round(CP.triangles(indice + 2).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 23)
                            PrintLine(1, Round(CP.triangles(indice + 2).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 33)
                            PrintLine(1, Round(CP.triangles(indice + 2).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                        Next
                    Next

                    For Each ISO In Me.Scena.ISOList
                        ISO.refreshBuffer()
                        Dim indice As Integer
                        For indice = 0 To ISO.iBuffer.Count - 1 Step 3
                            PrintLine(1, 0)
                            PrintLine(1, "3DFACE")
                            PrintLine(1, 8)
                            PrintLine(1, ISO.Name)
                            ' vertex 1
                            PrintLine(1, 10)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 20)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 30)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            ' vertex 2
                            PrintLine(1, 11)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 1)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 21)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 1)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 31)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 1)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            ' vertex 2
                            PrintLine(1, 12)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 1)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 22)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 1)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 32)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 1)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            ' vertex 3
                            PrintLine(1, 13)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 2)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 23)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 2)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 33)
                            PrintLine(1, Round(ISO.vBuffer(ISO.iBuffer(indice + 2)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                        Next
                    Next
                    For Each UVE In Me.Scena.UVList
                        UVE.refreshBuffer()
                        Dim maxI As Integer = UVE.vBuffer.Count - 1
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
                                PrintLine(1, Round(UVE.vBuffer(CInt(a)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 20)
                                PrintLine(1, Round(UVE.vBuffer(CInt(a)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 30)
                                PrintLine(1, Round(UVE.vBuffer(CInt(a)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 11)
                                PrintLine(1, Round(UVE.vBuffer(CInt(b)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 21)
                                PrintLine(1, Round(UVE.vBuffer(CInt(b)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 31)
                                PrintLine(1, Round(UVE.vBuffer(CInt(b)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 12)
                                PrintLine(1, Round(UVE.vBuffer(CInt(c)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 22)
                                PrintLine(1, Round(UVE.vBuffer(CInt(c)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 32)
                                PrintLine(1, Round(UVE.vBuffer(CInt(c)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 13)
                                PrintLine(1, Round(UVE.vBuffer(CInt(d)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 23)
                                PrintLine(1, Round(UVE.vBuffer(CInt(d)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 33)
                                PrintLine(1, Round(UVE.vBuffer(CInt(d)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            Next
                        Next
                    Next
                    For Each UE In Me.Scena.UList
                        UE.refreshBuffer()
                        Dim maxI As Integer = UE.tgeom.vb.Length - 1
                        For u = 0 To CSng(UE.Udens) - 1
                            a = u
                            b = u + 1
                            If a > maxI Or b > maxI Then Exit For
                            PrintLine(1, 0)
                            PrintLine(1, "LINE")
                            PrintLine(1, 8)
                            PrintLine(1, UE.Name)
                            PrintLine(1, 10)
                            PrintLine(1, Round(UE.tgeom.vb(CInt(a)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 20)
                            PrintLine(1, Round(UE.tgeom.vb(CInt(a)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 30)
                            PrintLine(1, Round(UE.tgeom.vb(CInt(a)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 11)
                            PrintLine(1, Round(UE.tgeom.vb(CInt(b)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 21)
                            PrintLine(1, Round(UE.tgeom.vb(CInt(b)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            PrintLine(1, 31)
                            PrintLine(1, Round(UE.tgeom.vb(CInt(b)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                        Next
                    Next
                    For Each CA In Me.Scena.CAList
                        If CA.Shape = shapes.Mesh Then
                            Dim caMesh As ClassMesh
                            For Each caMesh In CA.meshBuffer
                                Dim indice As Integer
                                For indice = 0 To caMesh.ibf.Count - 1 Step 3
                                    PrintLine(1, 0)
                                    PrintLine(1, "3DFACE")
                                    PrintLine(1, 8)
                                    PrintLine(1, CA.Name)
                                    ' vertex 1
                                    PrintLine(1, 10)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    PrintLine(1, 20)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    PrintLine(1, 30)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    ' vertex 2
                                    PrintLine(1, 11)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 1)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    PrintLine(1, 21)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 1)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    PrintLine(1, 31)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 1)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    ' vertex 2
                                    PrintLine(1, 12)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 1)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    PrintLine(1, 22)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 1)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    PrintLine(1, 32)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 1)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    ' vertex 3
                                    PrintLine(1, 13)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 2)).X, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    PrintLine(1, 23)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 2)).Y, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                    PrintLine(1, 33)
                                    PrintLine(1, Round(caMesh.vbfo(caMesh.ibf(indice + 2)).Z, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                Next
                            Next
                        Else
                            Dim c1, cc As Integer
                            c1 = CA.DXFBuffer.Count - 5
                            For cc = 0 To c1 Step 4

                                PrintLine(1, 0)
                                PrintLine(1, "3DFACE")
                                PrintLine(1, 8)
                                PrintLine(1, CA.Name)
                                PrintLine(1, 10)
                                PrintLine(1, Round(CA.DXFBuffer(cc).X + CA.xPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 20)
                                PrintLine(1, Round(CA.DXFBuffer(cc).Y + CA.yPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 30)
                                PrintLine(1, Round(CA.DXFBuffer(cc).Z + CA.zPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 11)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 1).X + CA.xPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 21)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 1).Y + CA.yPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 31)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 1).Z + CA.zPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 12)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 2).X + CA.xPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 22)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 2).Y + CA.yPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 32)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 2).Z + CA.zPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 13)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 3).X + CA.xPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 23)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 3).Y + CA.yPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                                PrintLine(1, 33)
                                PrintLine(1, Round(CA.DXFBuffer(cc + 3).Z + CA.zPosition, 3, MidpointRounding.ToEven).ToString.Replace(",", "."))
                            Next
                        End If
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
                Case "3DM"
                    Dim filename As String
                    Dim fp As OnFileHandle
                    Dim rc As Boolean
                    Dim error_log As New OnTextLog(System.Console.Error)
                    Dim model As New OnXModel()
                    filename = sf.FileName
                    fp = OnUtil.OpenFile(filename, "wb")
                    Dim archive As OnBinaryArchive = New OnBinaryFile(IOn.archive_mode.write3dm, fp)
                    Dim vBuffer() As Vector3
                    'Cracked Poly Export
                    Dim CP As ClassCrackedPoly
                    For Each CP In Me.Scena.CrackedPolyList
                        ReDim vBuffer(CP.triangles.Count - 1)
                        Dim i As Integer
                        For i = 0 To CP.triangles.Count - 1
                            vBuffer(i) = CP.triangles(i).Position
                        Next
                        Dim iBuffer() As Int32
                        ReDim iBuffer(CP.triangles.Count - 1)
                        For i = 0 To CP.triangles.Count - 1
                            iBuffer(i) = i
                        Next
                        rc = mdTools.WriteMeshRhino(vBuffer, iBuffer, model, False, True)
                        'mdTools.WriteBrepMesh(vBuffer, iBuffer, model)
                    Next
                    'Packing export
                    Dim PC As ClassPacking
                    For Each PC In Me.Scena.PackingList
                        Dim CM As ClassMesh
                        For Each CM In PC.cMesh
                            ReDim vBuffer(CM.vbf.Count - 1)
                            Dim i As Integer
                            For i = 0 To CM.vbf.Count - 1
                                vBuffer(i) = CM.vbf(i).Position
                            Next
                            rc = mdTools.WriteMeshRhino(vBuffer, CM.ibf.ToArray, model, False, True)
                        Next
                    Next
                    model.Write(archive, 4)
                    OnUtil.CloseFile(fp)
                    If (rc) Then
                        Console.Out.WriteLine("Successfully wrote " + filename)
                    Else
                        Console.Out.WriteLine("Errors while writing " + filename)
                    End If
            End Select
        End If
    End Sub

    Private Sub NewUCurveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click, NewUCurveToolStripMenuItem.Click
        Dim naz As String = InputBox("U Name:")

        If naz <> "" Then
            addUndoData("Object created")
            Dim noviU As New ClassU(naz)
            Me.Scena.UList.Add(noviU)
            Me.Scena.SelectedObject = noviU

        End If
    End Sub

    Private Sub ImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportToolStripMenuItem.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "X Files (*.x)|*.x|3D Studio (*.3ds)|*.3ds"
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            Select Case opf.FileName.Split(CChar("."))(opf.FileName.Split(CChar(".")).Length - 1)
                Case "x", "X"
                    Dim sModel As String
                    Dim mtrlBuffer() As ExtendedMaterial = Nothing
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
                    Dim lst As New List(Of String)


                    ModuleImport3DS.AzzeraVar()
                    ModuleImport3DS.ReadFile(opf.FileName, True, lst)

                    Dim I, j, k, ic As Integer
                    Dim vbf As New List(Of Vector3)
                    Dim ibf As New List(Of Integer)
                    Dim wsolv As ModuleImport3DS.tVertici
                    ic = 0
                    For I = 1 To ModuleImport3DS.nSolidi
                        vbf.Clear()
                        ibf.Clear()
                        For k = 0 To ModuleImport3DS.solids(I).nVerts - 1
                            wsolv = ModuleImport3DS.solids(I).Verts(k)
                            vbf.Add(New Vector3(wsolv.X, wsolv.Y, wsolv.Z))
                        Next
                        For k = 0 To ModuleImport3DS.solids(I).nSegs - 1
                            For j = 0 To ModuleImport3DS.solids(I).Segs(k).nVertsi - 1
                                ibf.Add(ModuleImport3DS.solids(I).Segs(k).Vertsi(j))
                                ic += 1
                            Next
                        Next
                        Dim cm As New ClassMesh()
                        cm.vbfo = vbf
                        cm.ibf = ibf
                        cm.refreshBuffer(cf3D.device)
                        Me.Scena.MeshList.Add(cm)
                    Next
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
        dfCA.CA = New ClassCA(cf3D.device, 6, 6, 3)
        If dfCA.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not Me.Scena.CAList.Contains(dfCA.CA) Then
                addUndoData("Object created")
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
            dfCA.CA = CType(Me.Scena.SelectedObject, ClassCA)
            dfCA.Show(Me)
        Else
            MsgBox("Select CA first!")
        End If
    End Sub

    Private Sub Export2DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2DToolStripMenuItem.Click
        If dfImage.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim sd As New SaveFileDialog
            sd.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png"
            If sd.ShowDialog = Windows.Forms.DialogResult.OK Then
                System.Threading.Thread.Sleep(30)
                cf3D.saveSlicka(sd.FileName, CInt(dfImage.TBWidth.Text), CInt(dfImage.TBHeight.Text))
            End If
        End If
    End Sub

    Public Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click, TSBCopy.Click
        If Not Me.Scena.SelectedObject.GetType Is GetType(ClassScena) Then
            Dim mem As New System.IO.MemoryStream()
            Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim obj As Object = Me.Scena.SelectedObject
            Try
                bin.Serialize(mem, obj)
                My.Computer.Clipboard.SetData(obj.GetType.Name, obj)
                'Me.clipboardObject = Me.Scena.SelectedObject
            Catch ex As Exception
                MsgBox("Your object cannot be serialized." + " The reason is: " + ex.ToString())
            End Try
        End If
    End Sub

    Public Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click, TSBPaste.Click
        ' paste ClassU
        If My.Computer.Clipboard.ContainsData(GetType(ClassU).Name) Then
            addUndoData("Object pasted")
            Dim obj As ClassU = CType(My.Computer.Clipboard.GetData(GetType(ClassU).Name), ClassU)
            obj.afterPaste(cf3D.device)
            Me.Scena.UList.Add(obj)
            Exit Sub
        End If
        ' paste ClassUV
        If My.Computer.Clipboard.ContainsData(GetType(ClassUV).Name) Then
            addUndoData("Object pasted")
            Dim obj As ClassUV = CType(My.Computer.Clipboard.GetData(GetType(ClassUV).Name), ClassUV)
            obj.afterPaste(cf3D.device)
            Me.Scena.UVList.Add(obj)
            Exit Sub
        End If
        ' paste ClassMesh
        If My.Computer.Clipboard.ContainsData(GetType(ClassMesh).Name) Then
            addUndoData("Object pasted")
            Dim obj As ClassMesh = CType(My.Computer.Clipboard.GetData(GetType(ClassMesh).Name), ClassMesh)
            obj.afterPaste(cf3D.device)
            Me.Scena.MeshList.Add(obj)
            Exit Sub
        End If
        ' paste ClassCA
        If My.Computer.Clipboard.ContainsData(GetType(ClassCA).Name) Then
            addUndoData("Object pasted")
            Dim obj As ClassCA = CType(My.Computer.Clipboard.GetData(GetType(ClassCA).Name), ClassCA)
            obj.afterPaste(cf3D.device)
            Me.Scena.CAList.Add(obj)
            Exit Sub
        End If
        ' paste ClassISO
        If My.Computer.Clipboard.ContainsData(GetType(ClassISO).Name) Then
            addUndoData("Object pasted")
            Dim obj As ClassISO = CType(My.Computer.Clipboard.GetData(GetType(ClassISO).Name), ClassISO)
            obj.afterPaste(cf3D.device)
            Me.Scena.ISOList.Add(obj)
            Exit Sub
        End If
        ' paste ClassLS
        If My.Computer.Clipboard.ContainsData(GetType(ClassLS).Name) Then
            addUndoData("Object pasted")
            Dim obj As ClassLS = CType(My.Computer.Clipboard.GetData(GetType(ClassLS).Name), ClassLS)
            obj.afterPaste(cf3D.device)
            Me.Scena.LSList.Add(obj)
            Exit Sub
        End If
        ' paste ClassCrackedPoly
        If My.Computer.Clipboard.ContainsData(GetType(ClassCrackedPoly).Name) Then
            addUndoData("Object pasted")
            Dim obj As ClassCrackedPoly = CType(My.Computer.Clipboard.GetData(GetType(ClassCrackedPoly).Name), ClassCrackedPoly)
            obj.afterPaste(cf3D.device)
            Me.Scena.CrackedPolyList.Add(obj)
            Exit Sub
        End If
        ' paste ClassPacking
        If My.Computer.Clipboard.ContainsData(GetType(ClassPacking).Name) Then
            addUndoData("Object pasted")
            Dim obj As ClassPacking = CType(My.Computer.Clipboard.GetData(GetType(ClassPacking).Name), ClassPacking)
            obj.afterPaste(cf3D.device)
            Me.Scena.PackingList.Add(obj)
            Exit Sub
        End If
    End Sub
    
    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click, TSBUndo.Click
        Try
            Dim mem As IO.MemoryStream = Me.undoData.Pop
            Dim b As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            Dim undoCall As String = Me.undoCall
            Select Case undoCall
                Case "Property value changed"
                    mem = Me.undoData.Pop
                    Me.undoCall = ""
            End Select
            mem.Seek(0, IO.SeekOrigin.Begin)
            addRedoData()
            Me.Scena = CType(b.Deserialize(mem), ClassScena)
            Me.Scena.afterPaste(cf3D.device)
            mem.Close()
            tfOsobine.PropertyGridUV.SelectedObject = Me.Scena.SelectedObject
            tfOsobine.PropertyGridUV.Refresh()
            cf3D.Refresh()
            'MsgBox(undoCall)
        Catch ex As Exception
            'console.writeline(ex.Message)
        End Try
    End Sub

    Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click, TSBRedo.Click
        Try
            Dim mem As IO.MemoryStream = Me.redoData.Pop
            Dim b As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            mem.Seek(0, IO.SeekOrigin.Begin)
            addUndoData("Redo")
            Me.Scena = CType(b.Deserialize(mem), ClassScena)
            Me.Scena.afterPaste(cf3D.device)
            tfOsobine.PropertyGridUV.SelectedObject = Me.Scena.SelectedObject
            tfOsobine.PropertyGridUV.Refresh()
            cf3D.Refresh()
        Catch ex As Exception
            'console.writeline(ex.Message)
        End Try
    End Sub

    Private Sub Scena_progress(ByVal p As Integer, ByVal m As String) Handles Scena.progress
        Me.PB1.Value = p
        Me.LStatus.Text = m
        Me.StatusStrip1.Refresh()
    End Sub

    Private Sub Scena_progressEnd() Handles Scena.progressEnd
        'Me.StatusStrip1.Visible = False
        Me.LMessage.Visible = False
        Me.LStatus.Text = "Ready."
        Me.PB1.Visible = False
    End Sub

    Private Sub Scena_progressStart() Handles Scena.progressStart
        'Me.StatusStrip1.Visible = True
        Me.LMessage.Visible = True
        Me.LStatus.Visible = True
        Me.PB1.Visible = True
        Me.LMessage.Text = "Please wait... "
        Me.LStatus.Text = "Status: Creating..."
        Me.StatusStrip1.Refresh()
    End Sub



    Private Sub Scena_SelectionChanged() Handles Scena.SelectionChanged
        If dfCA.Visible Then dfCA.Close()
        If tfUCurveEq.Visible Then tfUCurveEq.Close()
        If Me.Scena.SelectedObject.GetType Is GetType(ClassCA) Then
            Me.CAInspectorToolStripMenuItem_Click(New Object, New EventArgs)
        End If
        If Me.Scena.SelectedObject.GetType Is GetType(ClassU) Then
            tfUCurveEq.StartPosition = FormStartPosition.Manual
            tfUCurveEq.Top = Me.P3D.Top + 40
            tfUCurveEq.Left = Me.P3D.Left + 10
            tfUCurveEq.Show(Me)
        End If
    End Sub

    Private Sub NewLSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bNewLS.Click, NewLSToolStripMenuItem.Click
        Dim ime As String = ""
        ime = InputBox("LS name:", , "New LS")
        If ime <> "" Then
            addUndoData("Object created")
            Me.Scena.LSList.Add(New ClassLS(ime))

            Try
                Me.Scena.SelectedObject = Me.Scena.LSList(Me.Scena.LSList.Count - 1)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ExportMovieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportMovieToolStripMenuItem.Click
        ' VIDEO EXPORT
        '====================================================
        '        Dim sf As New SaveFileDialog
        '        Dim w, h, fps As Integer
        '        Dim cdc As AviClasses.cVideoHandler
        '        sf.Filter = "Video (*.avi)|*.avi"
        '        If dfVideo.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            w = dfVideo.w
        '            h = dfVideo.h
        '            cdc = dfVideo.vhan
        '            fps = dfVideo.fps
        '        Else
        '            Exit Sub
        '        End If
        '        Me.StatusStrip1.Visible = True
        '        If sf.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            Dim nazivFajla = sf.FileName
        '            ' Create an instance of the AVI Creator:
        '            Dim cAVI As New AviClasses.cAVICreator
        '
        '            ' 1. Setting General Output Options
        '            ' Bits/pixel in images:
        '            cAVI.bitsPerPixel = 24
        '            ' MPEG4 video handler has the FourCC identifier MPG4:
        '            cAVI.VideoHandlerFourCC = cdc.FourCC
        '            ' Each frame displayed for 25ms
        '            cAVI.FrameDuration = Int(1000 / fps)
        '            ' AVI name:
        '            cAVI.Name = "My Movie"
        '            ' Output file name:
        '            cAVI.Filename = nazivFajla
        '            cAVI.Height = h
        '            cAVI.Width = w
        '            'Create BMP Frames
        '            '-----------------
        '            If Not My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.Temp & "\fun3d") Then
        '                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.Temp & "\fun3d")
        '            Else
        '                My.Computer.FileSystem.DeleteDirectory(My.Computer.FileSystem.SpecialDirectories.Temp & "\fun3d", FileIO.DeleteDirectoryOption.DeleteAllContents)
        '                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.Temp & "\fun3d")
        '            End If
        '            Dim ax, ay, az, xc, yc, zc As Single
        '            Dim prolazi As Integer = 30
        '            Dim prolaz As Integer = 0
        '            Dim fc As Integer = 0
        '            Dim i As Integer
        '            Dim ukupnoF As Integer = prolazi * (Me.Scena.SceneFrames.Count - 1)
        '            Me.PB1.Value = 0
        '            Me.LMessage.Text = "Please wait... "
        '            Me.LStatus.Text = "Status: Rendering images..."
        '            Me.StatusStrip1.Refresh()
        '            If Me.Scena.SceneFrames.Count > 1 Then
        '                For i = 0 To Me.Scena.SceneFrames.Count - 2
        '                    While prolazi > prolaz
        '                        System.Threading.Thread.Sleep(30)
        '                        ax = Me.Scena.SceneFrames(i).ax + (Me.Scena.SceneFrames(i + 1).ax - Me.Scena.SceneFrames(i).ax) * prolaz / prolazi
        '                        ay = Me.Scena.SceneFrames(i).ay + (Me.Scena.SceneFrames(i + 1).ay - Me.Scena.SceneFrames(i).ay) * prolaz / prolazi
        '                        az = Me.Scena.SceneFrames(i).az + (Me.Scena.SceneFrames(i + 1).az - Me.Scena.SceneFrames(i).az) * prolaz / prolazi
        '                        xc = Me.Scena.SceneFrames(i).xc + (Me.Scena.SceneFrames(i + 1).xc - Me.Scena.SceneFrames(i).xc) * prolaz / prolazi
        '                        yc = Me.Scena.SceneFrames(i).yc + (Me.Scena.SceneFrames(i + 1).yc - Me.Scena.SceneFrames(i).yc) * prolaz / prolazi
        '                        zc = Me.Scena.SceneFrames(i).zc + (Me.Scena.SceneFrames(i + 1).zc - Me.Scena.SceneFrames(i).zc) * prolaz / prolazi
        '                        cf3D.angleX = ax
        '                        cf3D.angleY = ay
        '                        cf3D.angleZ = az
        '                        cf3D.xcam = xc
        '                        cf3D.ycam = yc
        '                        cf3D.zcam = zc
        '                        cf3D.saveSlicka(My.Computer.FileSystem.SpecialDirectories.Temp & "\fun3d\" + fc.ToString("000000") + ".bmp", w, h)
        '                        prolaz += 1
        '                        fc += 1
        '                        Me.PB1.Value = Int(50 * fc / ukupnoF)
        '                    End While
        '                    prolaz = 0
        '                Next
        '            End If
        '            '-----------------
        '
        '            '2. Creating an AVI Stream to Write To
        '            ' Variables to allow files in the directory to be
        '            ' enumerated
        '            Dim sBaseDir As String
        '            sBaseDir = My.Computer.FileSystem.SpecialDirectories.Temp & "\fun3d\"
        '            Dim sDir As String
        '            sDir = Dir(sBaseDir & "*.bmp")
        '            Dim cB As New AviClasses.cBmp
        '            cB.Load(sBaseDir & sDir)
        '            ' Create the file stream to write to:
        '            cAVI.StreamCreate(cB)
        '            Me.LStatus.Text = "Status: Creating video..."
        '            Me.StatusStrip1.Refresh()
        '            '3. Adding bitmaps and closing the stream
        '            fc = 0
        '            Do
        '                ' Get next file:
        '                sDir = Dir()
        '                ' If there was a next file:
        '                If (Len(sDir) > 0) Then
        '                    ' Read the image:
        '                    cB.Load(sBaseDir & sDir)
        '                    ' Add it as a new frame to the stream
        '                    cAVI.StreamAdd(cB)
        '                    fc += 1
        '                    Me.PB1.Value = 50 + Int(40 * fc / ukupnoF)
        '                End If
        '            Loop While Len(sDir) > 0
        '            ' Ensure that the stream is closed.
        '            cAVI.StreamClose()
        '            Me.PB1.Value = 100
        '        End If
        '        Me.StatusStrip1.Visible = False
        '======================================================
    End Sub

    Private Sub RenderStatePropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderStatePropertiesToolStripMenuItem.Click
        tfOsobine.PropertyGridUV.SelectedObject = cf3D.device.RenderState
        tfOsobine.PropertyGridUV.Refresh()
        Try
            tfOsobine.Show(Me)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NewISOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBNewISO.Click, NewISOToolStripMenuItem.Click
        Dim naz As String = InputBox("ISO Name:")
        If naz <> "" Then
            addUndoData("Object created")
            Dim noviISO As New ClassISO(naz, cf3D.device)
            Me.Scena.ISOList.Add(noviISO)
            Me.Scena.SelectedObject = noviISO

        End If
    End Sub

    Private Sub BClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.ExitToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub BMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub BMax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub PTitle_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ex = e.X
        ey = e.Y
    End Sub

    Private Sub PTitle_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.SetDesktopLocation(CInt(Me.Location.X - ex + e.X), CInt(Me.Location.Y - ey + e.Y))
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub NURBSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NURBSToolStripMenuItem.Click

    End Sub

    Private Sub tsbNewCracking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbNewCracking.Click, NewCrackingStructureToolStripMenuItem.Click
        Dim naz As String = InputBox("Cracking Name:")
        If naz <> "" Then
            addUndoData("Object created")
            Dim noviCS As New ClassCrackedPoly(naz, cf3D.device)
            Me.Scena.CrackedPolyList.Add(noviCS)
            Me.Scena.SelectedObject = noviCS
            cf3D.getPoints(noviCS.PolygonPoints, AddressOf noviCS.refreshBufferMI)

        End If
    End Sub

    Private Sub TSBNewPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBNewPacking.Click, NewPackingStructureToolStripMenuItem.Click
        Dim naz As String = InputBox("Packing Name:")
        If naz <> "" Then
            addUndoData("Object created")
            Dim noviPC As New ClassPacking(naz, cf3D.device)
            Me.Scena.PackingList.Add(noviPC)
            Me.Scena.SelectedObject = noviPC
            Me.Scena.selectedFun3DObject = noviPC
        End If
    End Sub

    Private Sub LightsPropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LightsPropertiesToolStripMenuItem.Click
        tfOsobine.PropertyGridUV.SelectedObject = cf3D.device.Lights(0)
        tfOsobine.PropertyGridUV.Refresh()
        Try
            tfOsobine.Show(Me)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ConsoleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConsoleToolStripMenuItem.Click
        Me.rtbConsole.Visible = Me.ConsoleToolStripMenuItem.Checked
    End Sub

    Private Sub CalculateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculateToolStripMenuItem.Click
        Dim s As String = InputBox("Equation:", "Expression is not valid")
        Try
            Console.WriteLine("RESULT IS: " + mdTools.Evaluate(s).ToString)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub
End Class
