Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
<System.Serializable()> _
Public Class ClassCA
    Public xPolja As Integer = 15
    Public yPolja As Integer = 15
    Public brojNivoa As Integer = 5
    Public praviloL As Byte = 54
    Public matrica As New List(Of Byte)
    Public matrice As New List(Of List(Of Byte))
    <System.NonSerialized()> _
    Public bufferT As New List(Of CustomVertex.PositionNormalTextured)
    Public w, h, l, razmak As Single
    Public bojaKocke As Integer = Color.Silver.ToArgb
    Public providnost As Byte = 255
    Public xpolozaj As Single = 0
    Public ypolozaj As Single = 0
    Public zpolozaj As Single = 0
    Public xrot As Single = 0
    Public yrot As Single = 0
    Public zrot As Single = 0
    Public minBC As Byte = 2
    Public maxBC As Byte = 3
    Public toLive As Byte = 3
    <System.NonSerialized()> _
    Public DXFBuffer As New List(Of CustomVertex.PositionColored)
    <System.NonSerialized()> _
    Public LineBuffer As New List(Of CustomVertex.PositionColored)
    Public selectedStyle As VisualStyles = VisualStyles.defaultStyle
    Dim conway As Boolean = True
    Dim ime As String = "New CA"
    Public bojaLinije As Integer = Color.Black.ToArgb
    Public Event bufferRefreshed()
    ' OSOBINE
    
    <Category("Meta")> _
    Public Property Name() As String
        Get
            Return ime
        End Get
        Set(ByVal value As String)
            ime = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property Style() As VisualStyles
        Get
            Return Me.selectedStyle
        End Get
        Set(ByVal value As VisualStyles)
            Me.selectedStyle = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property LineColor() As Color
        Get
            Return Color.FromArgb(Me.bojaLinije)
        End Get
        Set(ByVal value As Color)
            Me.bojaLinije = value.ToArgb
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Rule"), Browsable(False)> _
    Public Property MinimalCells() As Byte
        Get
            Return Me.minBC
        End Get
        Set(ByVal value As Byte)
            If value > 7 Or value < 0 Or value >= Me.maxBC Then
                MsgBox("Value must be between 0 and 7, and lower than MaximalCells Value")
            Else
                Me.minBC = value
                Me.createLevels()
                Me.refreshBuffer()
            End If
        End Set
    End Property
    <Category("Rule"), Browsable(False)> _
    Public Property MaximalCells() As Byte
        Get
            Return Me.maxBC
        End Get
        Set(ByVal value As Byte)
            If value > 8 Or value < 1 Or value <= Me.minBC Then
                MsgBox("Value must be between 1 and 8, and lower than MinimalCells Value")
            Else
                Me.maxBC = value
                Me.createLevels()
                Me.refreshBuffer()
            End If
        End Set
    End Property
    <Category("Rule"), Browsable(False)> _
    Public Property TurnOnCells() As Byte
        Get
            Return Me.toLive
        End Get
        Set(ByVal value As Byte)
            If value > 8 Or value < 0 Then
                MsgBox("Value must be between 0 and 8")
            Else
                Me.toLive = value
                Me.createLevels()
                Me.refreshBuffer()
            End If
        End Set
    End Property
    <Category("Matrix Definition"), Browsable(False)> _
    Public Property brojPoljaPoXosi() As Integer
        Get
            Return xPolja
        End Get
        Set(ByVal value As Integer)
            xPolja = value
            Me.generisiSlucajnuMatricu()
            createLevels()
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Matrix Definition"), Browsable(False)> _
    Public Property brojPoljaPoYosi() As Integer
        Get
            Return yPolja
        End Get
        Set(ByVal value As Integer)
            yPolja = value
            Me.generisiSlucajnuMatricu()
            createLevels()
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Matrix Definition"), Browsable(False)> _
    Public Property Levels() As Integer
        Get
            Return brojNivoa
        End Get
        Set(ByVal value As Integer)
            brojNivoa = value
            createLevels()
            Me.refreshBuffer()
        End Set
    End Property
    <System.ComponentModel.Browsable(False)> _
    Public Property Rule() As Byte
        Get
            Return praviloL
        End Get
        Set(ByVal value As Byte)
            praviloL = value
            Me.createLevels()
            Me.refreshBuffer()
        End Set
    End Property
    <Browsable(False)> _
    Public Property CellMatrix() As List(Of Byte)
        Get
            Return Me.matrice(0)
        End Get
        Set(ByVal value As List(Of Byte))
            Me.matrice(0) = value
        End Set
    End Property
    <Category("Geometry")> _
    Public Property width() As Single
        Get
            Return w
        End Get
        Set(ByVal value As Single)
            w = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Geometry")> _
    Public Property height() As Single
        Get
            Return h
        End Get
        Set(ByVal value As Single)
            h = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Geometry")> _
    Public Property lenght() As Single
        Get
            Return l
        End Get
        Set(ByVal value As Single)
            l = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Appearance")> _
    Public Property CubeColor() As Color
        Get
            Return Color.FromArgb(Me.bojaKocke)
        End Get
        Set(ByVal value As Color)
            Me.bojaKocke = value.ToArgb
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Appearance")> _
    Public Property Transparency() As Byte
        Get
            Return Me.providnost
        End Get
        Set(ByVal value As Byte)
            Me.providnost = value
        End Set
    End Property
    <Category("Geometry")> _
    Public Property Space() As Single
        Get
            Return razmak
        End Get
        Set(ByVal value As Single)
            razmak = value
            Me.refreshBuffer()
        End Set
    End Property
    <Category("Position")> _
    Public Property xPosition() As Single
        Get
            Return Me.xpolozaj
        End Get
        Set(ByVal value As Single)
            Me.xpolozaj = value
        End Set
    End Property
    <Category("Position")> _
    Public Property yPosition() As Single
        Get
            Return Me.ypolozaj
        End Get
        Set(ByVal value As Single)
            Me.ypolozaj = value
        End Set
    End Property
    <Category("Position")> _
    Public Property zPosition() As Single
        Get
            Return Me.zpolozaj
        End Get
        Set(ByVal value As Single)
            Me.zpolozaj = value
        End Set
    End Property
    <Category("Rotation")> _
    Public Property xRotation() As Single
        Get
            Return Me.xrot
        End Get
        Set(ByVal value As Single)
            Me.xrot = value
        End Set
    End Property
    <Category("Rotation")> _
    Public Property yRotation() As Single
        Get
            Return Me.yrot
        End Get
        Set(ByVal value As Single)
            Me.yrot = value
        End Set
    End Property
    <Category("Rotation")> _
    Public Property zRotation() As Single
        Get
            Return Me.zrot
        End Get
        Set(ByVal value As Single)
            Me.zrot = value
        End Set
    End Property
    ' METODE
    Public Sub New()
        w = 2
        h = 2
        l = 2
        razmak = 0.5
        Me.generisiSlucajnuMatricu()
        createLevels()
        Me.refreshBuffer()
    End Sub
    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal l1 As Integer)
        xPolja = x
        yPolja = y
        brojNivoa = l1
        w = 2
        h = 2
        l = 2
        razmak = 0.5
        Me.generisiSlucajnuMatricu()
        createLevels()
        Me.refreshBuffer()
    End Sub
    Public Sub generisiPraznuMatricu()
        Dim a, b As Integer
        Me.matrice.Clear()
        Me.matrice.Add(New List(Of Byte))
        For a = 0 To Me.xPolja - 1
            For b = 0 To Me.yPolja - 1
                Me.matrice(0).Add(0)
            Next
        Next
    End Sub
    Public Sub generisiSlucajnuMatricu()
        Dim a, b As Integer
        Me.matrice.Clear()
        Me.matrice.Add(New List(Of Byte))
        For a = 0 To Me.xPolja - 1
            For b = 0 To Me.yPolja - 1
                Randomize()
                Me.matrice(0).Add(CInt(Int((1 * Rnd()) + 0.35)))
            Next
        Next
    End Sub
    Public Sub refreshBuffer()
        Dim b As Byte
        Dim c1, c2, c3, c4, c5, c6, c7, c8 As CustomVertex.PositionNormalTextured
        Dim vertices(2) As CustomVertex.PositionNormalTextured
        Dim w1, l1, h1 As Single
        Dim n1, n2, n3, n4, n5, n6 As Vector3
        n1 = New Vector3(0, 0, 1)
        n2 = New Vector3(0, 1, 0)
        n3 = New Vector3(1, 0, 0)
        n4 = New Vector3(0, 0, -1)
        n5 = New Vector3(0, -1, 0)
        n6 = New Vector3(-1, 0, 0)
        w1 = w + razmak
        l1 = l + razmak
        h1 = h + razmak
        c1 = New CustomVertex.PositionNormalTextured
        c2 = New CustomVertex.PositionNormalTextured
        c3 = New CustomVertex.PositionNormalTextured
        c4 = New CustomVertex.PositionNormalTextured
        c5 = New CustomVertex.PositionNormalTextured
        c6 = New CustomVertex.PositionNormalTextured
        c7 = New CustomVertex.PositionNormalTextured
        c8 = New CustomVertex.PositionNormalTextured
        'c1.Color = bojaKocke
        'c2.Color = bojaKocke
        'c3.Color = bojaKocke
        'c4.Color = bojaKocke
        'c5.Color = bojaKocke
        'c6.Color = bojaKocke
        'c7.Color = bojaKocke
        'c8.Color = bojaKocke
        Me.bufferT.Clear()
        Me.DXFBuffer.Clear()
        Me.LineBuffer.Clear()
        Dim bi, mi, ni As Integer
        ni = 0
        For Each matrica In Me.matrice
            bi = 0 : mi = 0
            For Each b In matrica
                If b > 0 Then

                    c1.Position = New Vector3((mi - Int(mi / Me.xPolja) * Me.xPolja) * w1, Int(mi / Me.xPolja) * l1, ni * h1)
                    c2.Position = New Vector3(c1.X + w, c1.Y, c1.Z)
                    c3.Position = New Vector3(c1.X + w, c1.Y + l, c1.Z)
                    c4.Position = New Vector3(c1.X, c1.Y + l, c1.Z)
                    c5.Position = New Vector3(c1.X, c1.Y, c1.Z + h)
                    c6.Position = New Vector3(c1.X + w, c1.Y, c1.Z + h)
                    c7.Position = New Vector3(c1.X + w, c1.Y + l, c1.Z + h)
                    c8.Position = New Vector3(c1.X, c1.Y + l, c1.Z + h)

                    'line buffer 
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))

                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    Me.LineBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))

                    ' dxf buffer
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c1.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c5.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c2.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c6.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c3.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c7.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c8.Position, Me.bojaLinije))
                    Me.DXFBuffer.Add(New CustomVertex.PositionColored(c4.Position, Me.bojaLinije))
                    'f1
                    ' uv
                    c1.Tu = 0
                    c1.Tv = 0
                    c2.Tu = 0
                    c2.Tv = 1
                    c3.Tu = 1
                    c3.Tv = 1
                    c4.Tu = 1
                    c4.Tv = 0
                    Me.bufferT.Add(c3)
                    Me.bufferT.Add(c2)
                    Me.bufferT.Add(c1)

                    Me.bufferT.Add(c4)
                    Me.bufferT.Add(c3)
                    Me.bufferT.Add(c1)

                    'f2
                    ' uv
                    c1.Tu = 0
                    c1.Tv = 0
                    c2.Tu = 0
                    c2.Tv = 1
                    c6.Tu = 1
                    c6.Tv = 1
                    c5.Tu = 1
                    c5.Tv = 0
                    Me.bufferT.Add(c1)
                    Me.bufferT.Add(c2)
                    Me.bufferT.Add(c6)

                    Me.bufferT.Add(c1)
                    Me.bufferT.Add(c6)
                    Me.bufferT.Add(c5)

                    'f3
                    ' uv
                    c1.Tu = 0
                    c1.Tv = 0
                    c8.Tu = 1
                    c8.Tv = 1
                    c4.Tu = 1
                    c4.Tv = 0
                    c5.Tu = 0
                    c5.Tv = 1
                    Me.bufferT.Add(c1)
                    Me.bufferT.Add(c8)
                    Me.bufferT.Add(c4)

                    Me.bufferT.Add(c1)
                    Me.bufferT.Add(c5)
                    Me.bufferT.Add(c8)

                    'f4
                    ' uv
                    c7.Tu = 1
                    c7.Tv = 0
                    c3.Tu = 0
                    c3.Tv = 0
                    c2.Tu = 0
                    c2.Tv = 1
                    c6.Tu = 0
                    c6.Tv = 0
                    Me.bufferT.Add(c2)
                    Me.bufferT.Add(c3)
                    Me.bufferT.Add(c7)

                    Me.bufferT.Add(c6)
                    Me.bufferT.Add(c2)
                    Me.bufferT.Add(c7)

                    'f5
                    ' uv
                    c7.Tu = 0
                    c7.Tv = 0
                    c8.Tu = 0
                    c8.Tv = 1
                    c5.Tu = 1
                    c5.Tv = 1
                    c6.Tu = 1
                    c6.Tv = 0
                    Me.bufferT.Add(c7)
                    Me.bufferT.Add(c8)
                    Me.bufferT.Add(c5)

                    Me.bufferT.Add(c7)
                    Me.bufferT.Add(c5)
                    Me.bufferT.Add(c6)

                    'f6
                    ' uv
                    c7.Tu = 0
                    c7.Tv = 0
                    c8.Tu = 0
                    c8.Tv = 1
                    c4.Tu = 1
                    c4.Tv = 1
                    c3.Tu = 1
                    c3.Tv = 0
                    Me.bufferT.Add(c4)
                    Me.bufferT.Add(c8)
                    Me.bufferT.Add(c7)

                    Me.bufferT.Add(c3)
                    Me.bufferT.Add(c4)
                    Me.bufferT.Add(c7)
                    bi += 1
                End If
                mi += 1
            Next
            ni += 1
        Next
        RaiseEvent bufferRefreshed()
    End Sub
    Public Sub createLevels()
        Me.matrica = Me.matrice(0)
        Me.matrice.Clear()
        Me.matrice.Add(Me.matrica)
        Dim ln, endP As Integer
        Dim p0, p1, p2, p3, p4, p5, p6, p7 As Integer
        Try
            endP = Me.matrice(0).Count - 1
        Catch ex As Exception
            Exit Sub
        End Try
        For ln = 1 To Me.brojNivoa - 1
            Me.matrice.Add(New List(Of Byte))
            Dim b As Byte
            Dim mi As Integer
            mi = 0
            For Each b In Me.matrice(ln - 1)
                Dim k As Byte = 0
                p0 = mi - Me.xPolja - 1
                p1 = mi - Me.xPolja
                p2 = mi - Me.xPolja + 1
                p3 = mi - 1
                p4 = mi + 1
                p5 = mi + Me.xPolja - 1
                p6 = mi + Me.xPolja
                p7 = mi + Me.xPolja + 1
                Dim ukupnoZivih As Byte = 0
                If p0 >= 0 And p0 <= endP Then
                    If Me.matrice(ln - 1)(p0) > 0 Then
                        ukupnoZivih += 1
                        k = k Or 1
                    End If
                End If
                If p1 >= 0 And p1 <= endP Then
                    If Me.matrice(ln - 1)(p1) > 0 Then
                        ukupnoZivih += 1
                        k = k Or 2
                    End If
                End If
                If p2 >= 0 And p2 <= endP And Int(p2 / Me.xPolja) * xPolja <> p2 Then
                    If Me.matrice(ln - 1)(p2) > 0 Then
                        ukupnoZivih += 1
                        k = k Or 4
                    End If
                End If
                If p3 >= 0 And p3 <= endP And (p3 - Int(p3 / Me.xPolja) * xPolja) <> (Me.xPolja - 1) Then
                    If Me.matrice(ln - 1)(p3) > 0 Then
                        ukupnoZivih += 1
                        k = k Or 8
                    End If

                End If
                If p4 >= 0 And p4 <= endP And Int(p4 / Me.xPolja) * xPolja <> p4 Then
                    If Me.matrice(ln - 1)(p4) > 0 Then
                        ukupnoZivih += 1
                        k = k Or 16
                    End If
                End If
                If p5 >= 0 And p5 <= endP And (p5 - Int(p5 / Me.xPolja) * xPolja) <> (Me.xPolja - 1) Then
                    If Me.matrice(ln - 1)(p5) > 0 Then
                        ukupnoZivih += 1
                        k = k Or 32
                    End If
                End If
                If p6 >= 0 And p6 <= endP Then
                    If Me.matrice(ln - 1)(p6) > 0 Then
                        ukupnoZivih += 1
                        k = k Or 64
                    End If
                End If
                If p7 >= 0 And p7 <= endP Then
                    If Me.matrice(ln - 1)(p7) > 0 Then
                        ukupnoZivih += 1
                        k = k Or 128
                    End If
                End If
                If conway Then
                    If ukupnoZivih < Me.minBC Or ukupnoZivih > Me.maxBC Then
                        Me.matrice(ln).Add(0)
                    ElseIf ukupnoZivih = Me.toLive Then
                        Me.matrice(ln).Add(1)
                    Else
                        Me.matrice(ln).Add(b)
                    End If
                Else
                    If k And Me.Rule Then
                        Me.matrice(ln).Add(Not b)
                    Else
                        Me.matrice(ln).Add(0)
                    End If
                End If
                mi += 1
            Next
        Next
    End Sub
    Public Sub AfterCopy()
        Me.bufferT = New List(Of CustomVertex.PositionNormalTextured)
        Me.DXFBuffer = New List(Of CustomVertex.PositionColored)
        Me.LineBuffer = New List(Of CustomVertex.PositionColored)
        Me.refreshBuffer()
    End Sub
    Enum VisualStyles As Byte
        defaultStyle = 0
        Sketchy = 1
        FlatTransparent = 2
        GlassCube = 3
        GlassSky = 4
        GlassBlur = 5
        KohInor2B = 6
        Fluid = 7
    End Enum
End Class
