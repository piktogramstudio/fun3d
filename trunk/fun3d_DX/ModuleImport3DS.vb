' 3DS Import Module
' Code converted from VB6 code of
' 3DSViewer by Andrea Fontana found on
' http://www.freevbcode.com/ShowCode.asp?ID=3325

Module ModuleImport3DS
    Public FileName As String

    Public bDone As Boolean
    Public bRunning As Boolean

    Dim tPen As Long
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    Dim R As RECT
    Dim SizeX As Integer
    Dim SizeY As Integer
    Const pi = 3.14159265358979
    Public tHdc As Integer
    Public tBit As Integer
    Dim s3dStudioV As String
    Dim sMeshV As String
    Dim nPunti As Short
    Dim nFaccie As Short
    Dim xScarto As Double
    Dim yScarto As Double
    Public Structure POINTAPI
        Public X As Single
        Public Y As Single
    End Structure
    Public Structure tVertici
        Public nX As Single
        Public nY As Single
        Public nZ As Single
        Public tu As Single
        Public tv As Single
        Public X As Single
        Public Y As Single
        Public Z As Single
    End Structure
    Private Structure tVettore
        Public X As Single
        Public Y As Single
        Public Z As Single
    End Structure
    Public Structure tChunk
        Public Header As Integer
        Public Length As Integer
    End Structure
    Public Structure tChunkB
        Public Header As Short
        Public Length As Integer
    End Structure
    Public nFile As Short
    Dim Vertsi() As Short
    Dim nVertsi As Integer
    Public Structure tSeg
        Public mtrl As Short
        Public Vertsi() As Short
        Public nVertsi As Integer
        Public CullMode As Integer
        Public Shade As Integer
    End Structure
    Public Structure tSolido
        Public Verts() As tVertici
        Public nVerts As Short
        Public nSegs As Short
        Public Segs() As tSeg
    End Structure
    Dim lSegs(100) As tSeg
    Public nSolidi As Short
    Dim Solidi() As tSolido
    Dim lScale As Single
    Dim nSegs As Integer
    '
    '
    '
    '

    Public Property solids() As tSolido()
        Get
            Return Solidi
        End Get
        Set(ByVal value As tSolido())

        End Set
    End Property

    Public Function ReadStr(ByRef hm As Integer) As String
        Dim TmpChar As String
        Dim Ris As String = ""
        Dim b As Byte
        FileGet(nFile, b)
        TmpChar = Chr(b)
        hm = 0
        While b <> 0
            Ris = Ris & TmpChar
            FileGet(nFile, b)
            TmpChar = Chr(b)
            hm += 1
        End While
        Return Ris
    End Function

    Public Sub ReadFile(ByVal FileName As String, ByVal UseLst As Boolean, ByRef Lst As List(Of String))
        ModuleImport3DS.FileName = FileName
        Dim idx As Integer
        Dim tch As tChunk
        Dim I As Integer
        Try
            nFile = CShort(FreeFile())
            idx = 1
            FileOpen(nFile, FileName, OpenMode.Binary)
            AzzeraVar()
            While Not EOF(nFile)
                ReadChunk(idx, UseLst, tch, Lst)
            End While
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(FileLen(FileName).ToString)
        End Try
        FileClose(nFile)
        If UseLst Then
            Lst.Add(StrDup(100, "-"))
            Lst.Add("Number of Points: " + Str(nPunti))
            Lst.Add("Number of Polys: " + Str(nFaccie))
            Lst.Add("Number of Solids: " + Str(nSolidi))
        End If
        If UseLst Then
            If nSolidi > 1 Then
                For I = 1 To nSolidi
                    Lst.Add("Solid " + CStr(I) + ": " + CStr(Solidi(I).nVerts) + " Vertex.")
                Next
            End If
        End If
        Lst.ForEach(AddressOf Console.WriteLine)
    End Sub
    Public Sub ReadChunk(ByRef fPos As Integer, ByVal UseLst As Boolean, ByVal ptch As tChunk, ByRef Lst As List(Of String))
        Dim tch As tChunk
        Dim tchb As tChunkB
        Dim I As Integer
        Dim iV1, iV4 As Short
        Dim TmpInteger As Short
        Dim TmpLong As Integer
        Dim TmpSingle As Single
        Dim TmpStr As String
        Dim vt As ValueType = CType(tchb, ValueType)
        FileGet(nFile, vt, fPos)
        tchb = CType(vt, tChunkB)
        tch.Header = tchb.Header
        tch.Length = tchb.Length
        TmpStr = ""
        Select Case tch.Header
            'Versione di 3dStudio
            Case 2
                FileGet(nFile, TmpLong)
                TmpStr = CStr(TmpLong)
                s3dStudioV = TmpStr
                TmpStr = "3dStudio Version: " + TmpStr
                'Versione delle Mesh
            Case &H3D3E
                FileGet(nFile, TmpLong)
                TmpStr = CStr(TmpLong)
                sMeshV = TmpStr
                TmpStr = "Mesh Versione: " + TmpStr
                ' Scala
            Case &H100
                FileGet(nFile, TmpSingle)
                TmpStr = "Scale: " + CStr(TmpSingle)
                lScale = TmpSingle
                ' Oggetti
            Case &H4000
                Dim hm As Integer
                TmpStr = ReadStr(hm)
                TmpLong = fPos + hm + 7 'fPos + Len(TmpStr) + 7
                TmpStr = tch.Length & "* " & TmpStr
                nSegs = 0
                While TmpLong < (fPos + tch.Length)
                    ReadChunk(TmpLong, UseLst, tch, Lst)
                End While
                If nSolidi > 0 Then
                    If nSegs = 0 Then
                        Solidi(nSolidi).nSegs = 1
                        ReDim Solidi(nSolidi).Segs(1)
                        ReDim Solidi(nSolidi).Segs(0).Vertsi(nVertsi)
                        Solidi(nSolidi).Segs(0).nVertsi = nVertsi
                        For I = 0 To nVertsi - 1
                            Solidi(nSolidi).Segs(0).Vertsi(I) = Vertsi(I)
                        Next
                    Else
                        Solidi(nSolidi).nSegs = CShort(nSegs)
                        ReDim Solidi(nSolidi).Segs(nSegs)
                        For I = 0 To nSegs - 1
                            ReDim Solidi(nSolidi).Segs(I).Vertsi(lSegs(I).nVertsi)
                            Solidi(nSolidi).Segs(I) = lSegs(I)
                        Next
                    End If
                End If
                TmpStr = ""
            Case &H4110
                nSolidi = CShort(nSolidi + 1)
                ReDim Preserve Solidi(nSolidi)
                FileGet(nFile, TmpInteger)
                nPunti = nPunti + TmpInteger
                Solidi(nSolidi).nVerts = TmpInteger
                ReDim Solidi(nSolidi).Verts(TmpInteger)
                For I = 0 To TmpInteger - 1
                    FileGet(nFile, TmpSingle)
                    Solidi(nSolidi).Verts(I).X = TmpSingle
                    FileGet(nFile, TmpSingle)
                    Solidi(nSolidi).Verts(I).Y = TmpSingle
                    FileGet(nFile, TmpSingle)
                    Solidi(nSolidi).Verts(I).Z = TmpSingle
                Next

                ' Triangoli
            Case &H4120
                FileGet(nFile, TmpInteger)
                nFaccie = nFaccie + TmpInteger
                nVertsi = CInt(TmpInteger) * 3
                ReDim Vertsi(CInt(TmpInteger) * 3)
                For I = 0 To CInt(TmpInteger) * 3 - 1 Step 3
                    FileGet(nFile, Vertsi(I))
                    FileGet(nFile, Vertsi(I + 1))
                    FileGet(nFile, Vertsi(I + 2))
                    FileGet(nFile, iV4)

                    If Int(iV4 And 4) = 0 Then
                        iV1 = Vertsi(I)
                        Vertsi(I) = Vertsi(I + 1)
                        Vertsi(I + 1) = iV1
                    End If
                    If Int(iV4 And 2) = 0 Then
                        iV1 = Vertsi(I + 1)
                        Vertsi(I + 1) = Vertsi(I + 2)
                        Vertsi(I + 2) = iV1
                    End If
                    If Int(iV4 And 1) = 0 Then
                        iV1 = Vertsi(I)
                        Vertsi(I) = Vertsi(I + 2)
                        Vertsi(I + 2) = iV1
                    End If

                Next

                TmpLong = fPos + 8 + 8 * CInt(TmpInteger)
                Call ComputeNormals()
                While TmpLong < (fPos + tch.Length)
                    ReadChunk(TmpLong, UseLst, tch, Lst)
                End While
            Case &H4160
                '
            Case &H4170
                FileGet(nFile, TmpInteger)
                TmpStr = CStr(TmpInteger)
                For I = 1 To 21
                    FileGet(nFile, TmpSingle)
                    TmpStr = TmpStr & " " & CStr(TmpSingle)
                Next
                TmpStr = ""
                ' Varie sezioni
            Case &HA200, &HA204, &HA210, &HA220, &HA230, &H4100, &H1200, &H3000
                TmpLong = fPos + 6
                While TmpLong < (fPos + tch.Length)
                    ReadChunk(TmpLong, UseLst, tch, Lst)
                End While
            Case &H4D4D, &HAFFF, &H3D3D
                tch.Length = 6
            Case Else
                'Colori , effetti visivi, sezione Keyframer
                If tch.Header > &HA000 And tch.Header < &HA080 Then
                    TmpLong = fPos + 6
                Else
                    TmpStr = "* " & (tch.Length - 6)
                    TmpStr = ""
                End If
        End Select
        fPos = fPos + tch.Length
        If TmpStr <> "" Then
            If UseLst Then
                Lst.Add(TmpStr)
            End If
        End If
    End Sub
    Public Sub ComputeNormals()
        'hmm
    End Sub


    Public Sub AzzeraVar()
        ReDim Solidi(0)
        ReDim Vertsi(0)
        xScarto = 0
        yScarto = 0
        s3dStudioV = ""
        sMeshV = ""
        nSolidi = 0
        nVertsi = 0
        nPunti = 0
        nFaccie = 0
        nSegs = 0
        lScale = 0
    End Sub
End Module
