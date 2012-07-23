Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
imports RMA.OpenNURBS 
Module mdTools
    Public Function GetNormalVector(ByVal v1 As Vector3, ByVal v2 As Vector3, ByVal v3 As Vector3) As Vector3
        Dim v01 As Vector3        'Vector from points 0 to 1
        Dim v02 As Vector3        'Vector from points 0 to 2
        Dim vNorm As Vector3
        v01 = Vector3.Subtract(v2, v1)
        v02 = Vector3.Subtract(v3, v1)
        vNorm = Vector3.Cross(v01, v02)
        vNorm = Vector3.Normalize(vNorm)
        Return vNorm
    End Function
    Public Function valueColor(ByVal minV As Single, ByVal maxV As Single, ByVal value As Single) As Color
        Dim nRGB() As Integer = {255, 255, 255, 255, 255, 255, 255, 255}
        Dim rv As Color = Nothing
        Dim colorInterval As Single = nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3) + nRGB(4) + nRGB(5) + nRGB(6) + nRGB(7)
        Dim interval As Single = maxV - minV
        Dim unknownColor As Single
        Dim valueInInterval As Single = value - minV
        unknownColor = (colorInterval / interval) * valueInInterval
        If unknownColor >= colorInterval Then
            rv = Color.FromArgb(255, 0, 0)
            Return rv
        End If
        If unknownColor >= (nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3) + nRGB(4) + nRGB(5) + nRGB(6)) Then
            Dim miniColorValue As Single = unknownColor - (nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3) + nRGB(4) + nRGB(5) + nRGB(6))
            rv = Color.FromArgb(255, CInt(127 - 127 * (miniColorValue / nRGB(7))), 0)
            Return rv
        End If
        If unknownColor >= (nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3) + nRGB(4) + nRGB(5)) Then
            Dim miniColorValue As Single = unknownColor - (nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3) + nRGB(4) + nRGB(5))
            rv = Color.FromArgb(255, CInt(255 - 128 * (miniColorValue / nRGB(6))), 0)
            Return rv
        End If
        If unknownColor >= (nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3) + nRGB(4)) Then
            Dim miniColorValue As Single = unknownColor - (nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3) + nRGB(4))
            rv = Color.FromArgb(CInt(127 + 128 * (miniColorValue / nRGB(5))), 255, 0)
            Return rv
        End If
        If unknownColor >= (nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3)) Then
            Dim miniColorValue As Single = unknownColor - (nRGB(0) + nRGB(1) + nRGB(2) + nRGB(3))
            rv = Color.FromArgb(CInt(128 * (miniColorValue / nRGB(4))), 255, 0)
            Return rv
        End If
        If unknownColor >= (nRGB(0) + nRGB(1) + nRGB(2)) Then
            Dim miniColorValue As Single = unknownColor - (nRGB(0) + nRGB(1) + nRGB(2))
            rv = Color.FromArgb(0, 255, CInt(127 - 127 * (miniColorValue / nRGB(3))))
            Return rv
        End If
        If unknownColor >= (nRGB(0) + nRGB(1)) Then
            Dim miniColorValue As Single = unknownColor - (nRGB(0) + nRGB(1))
            rv = Color.FromArgb(0, 255, CInt(255 - 128 * (miniColorValue / nRGB(2))))
            Return rv
        End If
        If unknownColor >= (nRGB(0)) Then
            Dim miniColorValue As Single = unknownColor - (nRGB(0))
            rv = Color.FromArgb(0, CInt(127 + 128 * (miniColorValue / nRGB(1))), 255)
            Return rv
        End If
        If unknownColor >= 0 Then
            Dim miniColorValue As Single = unknownColor - 0
            rv = Color.FromArgb(0, CInt(128 * (miniColorValue / nRGB(0))), 255)
            Return rv
        End If
        If unknownColor < 0 Then
            rv = Color.FromArgb(0, 0, 255)
        End If
        Return rv
    End Function
    Public Function angleBetween2Vectors(ByVal v1 As Vector3, ByVal v2 As Vector3) As Single
        Dim vdif As Vector3 = v2 - v1
        Return Geometry.RadianToDegree(CSng(Math.Acos(Vector3.Dot(Vector3.Normalize(vdif), Vector3.Normalize(New Vector3(1, 0, 0))))))
    End Function
    Public Function angleBetween2Vectors(ByVal center As Vector3, ByVal vc1 As Vector3, ByVal vc2 As Vector3) As Single
        Dim v1 As Vector3 = vc1 - center
        Dim v2 As Vector3 = vc2 - center
        Dim vdif As Vector3 = v2 - v1
        Return Geometry.RadianToDegree(CSng(Math.Acos(Vector3.Dot(Vector3.Normalize(v2), Vector3.Normalize(v1)))))
    End Function
    Public Function PointsOnSameSideOfLine(ByVal p1 As Vector3, ByVal p2 As Vector3, ByVal a As Vector3, ByVal b As Vector3) As Boolean
        Dim cp1, cp2 As Vector3
        cp1 = Vector3.Cross(b - a, p1 - a)
        cp2 = Vector3.Cross(b - a, p2 - a)
        If Vector3.Dot(cp1, cp2) >= 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function PointInTriangle(ByVal p As Vector3, ByVal a As Vector3, ByVal b As Vector3, ByVal c As Vector3) As Boolean
        If PointsOnSameSideOfLine(p, a, b, c) And PointsOnSameSideOfLine(p, b, a, c) And PointsOnSameSideOfLine(p, c, a, b) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function TrianglesIntersect(ByVal T1() As Vector3, ByVal T2() As Vector3) As Boolean
        Dim N2 As Vector3
        Dim d2, dt10, dt11, dt12 As Single
        Dim N1 As Vector3
        Dim d1, dt20, dt21, dt22 As Single
        Dim D As Vector3
        Dim p10, p11, p12 As Single
        Dim p20, p21, p22 As Single
        Dim t11, t12, t21, t22 As Single
        ' Plane equation of triangle 2 
        N2 = Vector3.Cross(T2(1) - T2(0), T2(2) - T2(0))
        d2 = Vector3.Dot(-N2, T2(0))
        ' Signed distance of triangle 1 vertices
        dt10 = Vector3.Dot(N2, T1(0)) - d2
        dt11 = Vector3.Dot(N2, T1(1)) - d2
        dt12 = Vector3.Dot(N2, T1(2)) - d2
        ' Not on plane and on same side
        If (dt10 < 0 And dt11 < 0 And dt12 < 0) Or (dt10 > 0 And dt11 > 0 And dt12 > 0) Then Return False
        ' Plane equation of triangle 1 
        N1 = Vector3.Cross(T1(1) - T1(0), T1(2) - T1(0))
        d1 = Vector3.Dot(-N1, T1(0))
        ' Signed distance of triangle 2 vertices
        dt20 = Vector3.Dot(N1, T2(0)) - d1
        dt21 = Vector3.Dot(N1, T2(1)) - d1
        dt22 = Vector3.Dot(N1, T2(2)) - d1
        ' Not on plane and on same side
        If (dt20 < 0 And dt21 < 0 And dt22 < 0) Or (dt20 > 0 And dt21 > 0 And dt22 > 0) Then Return False
        ' Co-planar triangles
        If dt10 = 0 And dt11 = 0 And dt12 = 0 And dt20 = 0 And dt21 = 0 And dt22 = 0 Then
            If PointInTriangle(T2(0), T1(0), T1(1), T1(2)) Then Return True
            If PointInTriangle(T2(1), T1(0), T1(1), T1(2)) Then Return True
            If PointInTriangle(T2(2), T1(0), T1(1), T1(2)) Then Return True
            If PointInTriangle(T1(0), T2(0), T2(1), T2(2)) Then Return True
            If PointInTriangle(T1(1), T2(0), T2(1), T2(2)) Then Return True
            If PointInTriangle(T1(2), T2(0), T2(1), T2(2)) Then Return True
        End If
        D = Vector3.Cross(N1, N2)
        p10 = Vector3.Dot(D, T1(0))
        p11 = Vector3.Dot(D, T1(1))
        p12 = Vector3.Dot(D, T1(2))
        p20 = Vector3.Dot(D, T2(0))
        p21 = Vector3.Dot(D, T2(1))
        p22 = Vector3.Dot(D, T2(2))
        ' Interval for triangle 1
        If (dt10 < 0 And dt11 < 0 And dt12 > 0) Or (dt10 > 0 And dt11 > 0 And dt12 < 0) Then
            t11 = p10 - (p12 - p10) * dt10 / (dt10 - dt12)
            t12 = p11 - (p12 - p11) * dt11 / (dt11 - dt12)
        End If
        If (dt10 < 0 And dt11 > 0 And dt12 < 0) Or (dt10 > 0 And dt11 < 0 And dt12 > 0) Then
            t11 = p10 - (p11 - p10) * dt10 / (dt10 - dt11)
            t12 = p12 - (p11 - p12) * dt12 / (dt12 - dt11)
        End If
        If (dt10 > 0 And dt11 < 0 And dt12 < 0) Or (dt10 < 0 And dt11 > 0 And dt12 > 0) Then
            t11 = p12 - (p10 - p12) * dt12 / (dt12 - dt10)
            t12 = p11 - (p10 - p11) * dt11 / (dt11 - dt10)
        End If
        ' Interval for triangle 2
        If (dt20 < 0 And dt21 < 0 And dt22 > 0) Or (dt20 > 0 And dt21 > 0 And dt22 < 0) Then
            t21 = p20 - (p22 - p20) * dt20 / (dt20 - dt22)
            t22 = p21 - (p22 - p21) * dt21 / (dt21 - dt22)
        End If
        If (dt20 < 0 And dt21 > 0 And dt22 < 0) Or (dt20 > 0 And dt21 < 0 And dt22 > 0) Then
            t21 = p20 - (p21 - p20) * dt20 / (dt20 - dt21)
            t22 = p22 - (p21 - p22) * dt22 / (dt22 - dt21)
        End If
        If (dt20 > 0 And dt21 < 0 And dt22 < 0) Or (dt20 < 0 And dt21 > 0 And dt22 > 0) Then
            t21 = p22 - (p20 - p22) * dt22 / (dt22 - dt20)
            t22 = p21 - (p20 - p21) * dt21 / (dt21 - dt20)
        End If
        If (t11 < t21 And t21 < t12) Or (t11 < t22 And t22 < t12) Or (t11 > t21 And t21 > t12) Or (t11 > t22 And t22 > t12) Then Return True
        Return False
    End Function
    Public Function ClassMeshesIntersect(ByVal m1 As ClassMesh, ByVal m2 As ClassMesh) As Boolean
        Dim i, ii As Integer
        Dim t1(2), t2(2) As Vector3
        For i = 0 To m1.ibf.Count - 1 Step 3
            t1(0) = m1.vbf(m1.ibf(i)).Position
            t1(1) = m1.vbf(m1.ibf(i + 1)).Position
            t1(2) = m1.vbf(m1.ibf(i + 2)).Position
            For ii = 0 To m2.ibf.Count - 1 Step 3
                t2(0) = m2.vbf(m2.ibf(ii)).Position
                t2(1) = m2.vbf(m2.ibf(ii + 1)).Position
                t2(2) = m2.vbf(m2.ibf(ii + 2)).Position
                If TrianglesIntersect(t1, t2) Then Return True
            Next
        Next
        Return False
    End Function
    Function WriteMeshRhino(ByVal vertexBuffer() As Vector3, ByVal indicesBuffer() As Int32, ByVal model As OnXModel, Optional ByVal quad As Boolean = False, Optional ByVal asSurf As Boolean = False) As Boolean
        Dim bHasVertexNormals As Boolean = False ' we will specify vertex normals
        Dim bHasTexCoords As Boolean = False    ' we will not specify texture coordinates
        Dim vertex_count As Integer = vertexBuffer.Length  ' 4 duplicates for different base normals
        Dim face_count As Integer = CInt(indicesBuffer.Length / 3) ' 4 triangle sides and a quad base
        Dim mesh As New OnMesh(face_count, vertex_count, bHasVertexNormals, bHasTexCoords)

        ' The SetVertex(), SetNormal(), SetTCoord() and SetFace() functions
        ' return true if successful and false if input is illegal.  It is
        ' a good idea to inspect this returned value.
        Dim i As Integer

        For i = 0 To vertex_count - 1
            mesh.SetVertex(i, vertexBuffer(i).X, vertexBuffer(i).Y, vertexBuffer(i).Z)
            'mesh.SetVertexNormal(0, 0.0, 0.0, 1.0)
        Next
        ' faces have vertices ordered counter-clockwise
        ' triangled mesh

        If Not quad Then
            For i = 0 To face_count - 1
                mesh.SetTriangle(i, indicesBuffer(3 * i + 2), indicesBuffer(3 * i + 1), indicesBuffer(3 * i))
            Next
        Else
            For i = 0 To face_count - 1
                mesh.SetQuad(i, indicesBuffer(4 * i + 3), indicesBuffer(4 * i + 2), indicesBuffer(4 * i + 1), indicesBuffer(4 * i))
            Next
        End If


        Dim ok As Boolean = False
        If (mesh.IsValid()) Then
            ' Most applications expect vertex normals.
            ' If they are not present, ComputeVertexNormals sets
            ' them by averaging face normals.
            If (Not mesh.HasVertexNormals()) Then mesh.ComputeVertexNormals()

            Dim mo As New OnXModel_Object()
            If asSurf Then
                mo.m_object = OnUtil.ON_BrepFromMesh(mesh.Topology())
            Else
                mo.m_object = mesh
            End If
            model.m_object_table.Append(mo)
            ok = True
        End If
        Return ok
    End Function
    Public Function MassCenter(ByVal shp As List(Of Vector3)) As Vector3
        Dim rv As Vector3 = Nothing
        Dim rv1 As Vector3 = Nothing
        Dim shpEnum As IEnumerator(Of Vector3) = shp.GetEnumerator()
        shpEnum.Reset()
        If shpEnum.MoveNext() Then
            rv = shpEnum.Current
            While shpEnum.MoveNext()
                rv.Add(shpEnum.Current)
            End While
            rv.Multiply(CSng(1 / shp.Count))
        End If
        Return rv
    End Function
    Public Function Evaluate(ByVal equation As String, Optional ByVal parameters() As ClassParametri = Nothing) As Single
        Dim rv As Single
        Dim p As ClassParametri
        Dim cd As String = ""
        Dim vba As New Microsoft.VisualBasic.VBCodeProvider()
        Dim cp As New CodeDom.Compiler.CompilerParameters()
        cd += "Imports Microsoft.DirectX" + vbCrLf
        cd += "Imports system.collections.generic" + vbCrLf
        cd += "Imports System.Math" + vbCrLf
        cd += "Namespace FlyAss" + vbCrLf
        cd += "Class EvaluateEq" + vbCrLf
        cd += "Public Function Evaluate() As Single" + vbCrLf
        cd += "Dim rv as Single" + vbCrLf
        If parameters IsNot Nothing Then
            For Each p In parameters
                cd += "Dim " + p.Name + " as Single = " + Str(p.value) + vbCrLf
            Next
        End If
        cd += "rv = " + equation + vbCrLf
        cd += "Return rv" + vbCrLf
        cd += "End Function" + vbCrLf
        cd += "End Class" + vbCrLf
        cd += "End Namespace" + vbCrLf
        ' Setup the Compiler Parameters  
        ' Add any referenced assemblies
        cp.ReferencedAssemblies.Add("system.dll")
        cp.ReferencedAssemblies.Add("system.xml.dll")
        cp.ReferencedAssemblies.Add("system.data.dll")
        cp.ReferencedAssemblies.Add("microsoft.directx.dll")
        cp.CompilerOptions = "/t:library"
        cp.GenerateInMemory = True
        Dim cr As CodeDom.Compiler.CompilerResults = vba.CompileAssemblyFromSource(cp, cd)
        If Not cr.Errors.Count <> 0 Then
            Dim exeins As Object = cr.CompiledAssembly.CreateInstance("FlyAss.EvaluateEq")
            Dim mi As Reflection.MethodInfo = exeins.GetType().GetMethod("Evaluate")

            rv = CType(mi.Invoke(exeins, Nothing), Single)
        Else
            Dim ce As CodeDom.Compiler.CompilerError
            For Each ce In cr.Errors
                'Console.WriteLine(cd)
                Console.WriteLine(ce.ErrorText)
                'Console.WriteLine("Line: " + ce.Line.ToString)
            Next
        End If
        cp.TempFiles.Delete()
        Return rv
    End Function
    Public Function openFile(ByVal fnm As String, Optional ByVal frommem As Boolean = False) As ClassScena
        If fnm.Substring(fnm.Length - 4, 4) = "f3dx" And Not frommem Then
            Dim rv As ClassScena = Nothing
            Dim mem As New IO.FileStream(fnm, IO.FileMode.Open)
            Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim obj As iFun3DScene
            Try
                obj = bin.Deserialize(mem)
                If obj.Fun3DFileVersion < 2 Then
                    Console.WriteLine("Old file version")
                    Return Nothing
                End If
                mf.saveFile = fnm
                rv = CType(obj, ClassScena)
            Catch ex As Exception
                Console.WriteLine("ERROR." + " The reason is: " + ex.ToString())
            End Try
            Try
                mem.Close()
            Catch ex As Exception
                Console.WriteLine("ERROR." + " The reason is: " + ex.ToString())
            End Try
            Return rv
        Else
            Dim rvalue As New ClassScena
            If Not frommem Then
                mf.saveDataSet.Clear()
                mf.saveDataSet.ReadXml(fnm)
            End If

            Try
                Dim os As dsProjekat.scenaRow = mf.saveDataSet.scena(0)
                With rvalue
                    .Ambient = Color.FromArgb(CInt(os.AmbientColor))
                    .backgroundColor = Color.FromArgb(CInt(os.BackgroundColor))
                    .colorGrid = Color.FromArgb(CInt(os.GridColor))
                    .FillMode = CType(os.FillMode, FillMode)
                    .gridMaxV = os.GridMaxValue
                    .gridMinV = os.GridMinValue
                    .stepGrid = os.GridStep
                    .Lighting = os.Lighting
                    .Name = os.Name
                    .ShadeMode = CType(os.ShadeMode, ShadeMode)
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
                    Dim UV As New ClassUV(cf3D.device)
                    With UV
                        .maxU = ouv.UMax
                        .maxV = ouv.VMax
                        .minU = ouv.UMin
                        .minV = ouv.VMin
                        .scaleX = ouv.XScale
                        .scaleY = ouv.YScale
                        .scaleZ = ouv.ZScale
                        .Udens = ouv.UDensity
                        .Vdens = ouv.VDensity
                        .XF = ouv.XFun
                        .YF = ouv.YFun
                        .ZF = ouv.ZFun
                        .LineColor = Color.FromArgb(CInt(ouv.LineColor))
                        .bojaPolja1 = Color.FromArgb(CInt(ouv.FieldColor1))
                        .bojaPolja2 = Color.FromArgb(CInt(ouv.FieldColor2))
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
                        .Parameters.Clear()
                        Dim op As dsProjekat.parametriRow
                        For Each op In ouv.GetparametriRows
                            Dim plAdd As New ClassParametri
                            plAdd.Name = "prmt" + op.jb.ToString + op.Name
                            plAdd.value = op.Value
                            plAdd.sliderMaximum = op.maxS
                            plAdd.sliderMinimum = CSng(op.minS)
                            plAdd.sliderStep = op.stepS
                            .Parameters.Add(plAdd)
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
                        .maxU = ou.UMax
                        .minU = ou.UMin
                        .Transform.sx = ou.XScale
                        .Transform.sy = ou.YScale
                        .Transform.sz = ou.ZScale
                        .Udens = CShort(ou.UDensity)
                        .funX = ou.XFun
                        .funY = ou.YFun
                        .funZ = ou.ZFun
                        .LineColor = Color.FromArgb(CInt(ou.LineColor))
                        .Transform.tx = ou.XPos
                        .Transform.ty = ou.YPos
                        .Transform.tz = ou.ZPos
                        .Name = ou.Name
                        .maxSU = ou.maxSU
                        .minSU = ou.minSU
                        .stepSU = ou.stepSU
                        .maxSUm = ou.maxSUm
                        .minSUm = ou.minSUm
                        .stepSUm = ou.stepSUm
                        Try
                            .Transparency = ou.Transparency
                        Catch ex As Exception

                        End Try
                        .Parameters.Clear()
                        Dim op As dsProjekat.parametriRow
                        For Each op In ou.GetparametriRows
                            Dim plAdd As New ClassParametri
                            plAdd.Name = "prmt" + op.jb.ToString + op.Name
                            plAdd.value = op.Value
                            plAdd.sliderMaximum = op.maxS
                            plAdd.sliderMinimum = CSng(op.minS)
                            plAdd.sliderStep = op.stepS
                            .Parameters.Add(plAdd)
                        Next
                    End With
                    U.refreshBuffer()
                    rvalue.UList.Add(U)
                Next
                Dim oca As dsProjekat.CARow
                For Each oca In os.GetCARows
                    Dim CA As New ClassCA(cf3D.device)
                    With CA
                        .CubeColor = Color.FromArgb(oca.bojaKocke)
                        .LineColor = Color.FromArgb(oca.bojaLinije)
                        .h = oca.h
                        .w = oca.w
                        .l = oca.l
                        .maxBC = oca.maxC
                        .minBC = oca.minC
                        .TurnOnCells = oca.turnOnC
                        .Name = oca.name
                        .nOfLevels = oca.nivoa
                        .Rule = oca.rule
                        .Space = oca.space
                        .Style = CType(oca.style, VisualStyles)
                        .Transparency = CByte(oca.transparency)
                        .xFields = oca.xPolja
                        .yFields = oca.yPolja
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
                        .Ambient = Color.FromArgb(CInt(ol.Ambient))
                        .Diffuse = Color.FromArgb(CInt(ol.Diffuse))
                        .Specular = Color.FromArgb(CInt(ol.Specular))
                        .Direction = ol.Direction
                        .Position = ol.Position
                        .Enabled = ol.Enabled
                        .Name = ol.Name
                        .Type = CType(ol.Type, LightType)
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
                mf.saveFile = fnm
                Dim UV1 As ClassUV
                For Each UV1 In rvalue.UVList
                    UV1.refreshBuffer()
                Next
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
            Return rvalue
        End If
    End Function
End Module
