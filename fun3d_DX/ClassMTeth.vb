Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.DirectX


	Public Class ClassMTeth
		Public Class TRIANGLE
			Public p As Vector3() = New Vector3(2) {}
		End Class
		Public Class GRIDCELL
			Public p As Vector3() = New Vector3(7) {}
			Public val As Double() = New Double(7) {}
		End Class

    '   Converted from C code ref. http://local.wasp.uwa.edu.au/~pbourke/geometry/polygonise/
    '   Polygonising a scalar field
    '   Also known as: "3D Contouring", "Marching Cubes", "Surface Reconstruction" 
    '   by Paul Bourke (http://local.wasp.uwa.edu.au/~pbourke/geometry/)
    '   May 1994

'           Polygonise a tetrahedron given its vertices within a cube
'           This is an alternative algorithm to polygonisegrid.
'           It results in a smoother surface but more triangular facets.
'
'                              + 0
'                             /|\
'                            / | \
'                           /  |  \
'                          /   |   \
'                         /    |    \
'                        /     |     \
'                       +-------------+ 1
'                      3 \     |     /
'                         \    |    /
'                          \   |   /
'                           \  |  /
'                            \ | /
'                             \|/
'                              + 2
'
'           It's main purpose is still to polygonise a gridded dataset and
'           would normally be called 6 times, one for each tetrahedron making
'           up the grid cell.
'           Given the grid labelling as in PolygniseGrid one would call
'              PolygoniseTri(grid,iso,triangles,0,2,3,7);
'              PolygoniseTri(grid,iso,triangles,0,2,6,7);
'              PolygoniseTri(grid,iso,triangles,0,4,6,7);
'              PolygoniseTri(grid,iso,triangles,0,6,1,2);
'              PolygoniseTri(grid,iso,triangles,0,6,1,4);
'              PolygoniseTri(grid,iso,triangles,5,6,1,4);
'        

		Public Function PolygoniseTri(g As GRIDCELL, iso As Double, ByRef tri As TRIANGLE(), v0 As Integer, v1 As Integer, v2 As Integer, _
			v3 As Integer) As Integer
			Dim ntri As Integer = 0
			Dim triindex As Integer

			'
'               Determine which of the 16 cases we have given which vertices
'               are above or below the isosurface
'            

			triindex = 0
			If g.val(v0) < iso Then
				triindex = triindex Or 1
			End If
			If g.val(v1) < iso Then
				triindex = triindex Or 2
			End If
			If g.val(v2) < iso Then
				triindex = triindex Or 4
			End If
			If g.val(v3) < iso Then
				triindex = triindex Or 8
			End If

			' Form the vertices of the triangles for each case 

			Select Case triindex
				Case &H0, &Hf
					Exit Select
				Case &He, &H1
					tri(0).p(0) = VertexInterp(iso, g.p(v0), g.p(v1), g.val(v0), g.val(v1))
					tri(0).p(1) = VertexInterp(iso, g.p(v0), g.p(v2), g.val(v0), g.val(v2))
					tri(0).p(2) = VertexInterp(iso, g.p(v0), g.p(v3), g.val(v0), g.val(v3))
					ntri += 1
					Exit Select
				Case &Hd, &H2
					tri(0).p(0) = VertexInterp(iso, g.p(v1), g.p(v0), g.val(v1), g.val(v0))
					tri(0).p(1) = VertexInterp(iso, g.p(v1), g.p(v3), g.val(v1), g.val(v3))
					tri(0).p(2) = VertexInterp(iso, g.p(v1), g.p(v2), g.val(v1), g.val(v2))
					ntri += 1
					Exit Select
				Case &Hc, &H3
					tri(0).p(0) = VertexInterp(iso, g.p(v0), g.p(v3), g.val(v0), g.val(v3))
					tri(0).p(1) = VertexInterp(iso, g.p(v0), g.p(v2), g.val(v0), g.val(v2))
					tri(0).p(2) = VertexInterp(iso, g.p(v1), g.p(v3), g.val(v1), g.val(v3))
					ntri += 1
					tri(1).p(0) = tri(0).p(2)
					tri(1).p(1) = VertexInterp(iso, g.p(v1), g.p(v2), g.val(v1), g.val(v2))
					tri(1).p(2) = tri(0).p(1)
					ntri += 1
					Exit Select
				Case &Hb, &H4
					tri(0).p(0) = VertexInterp(iso, g.p(v2), g.p(v0), g.val(v2), g.val(v0))
					tri(0).p(1) = VertexInterp(iso, g.p(v2), g.p(v1), g.val(v2), g.val(v1))
					tri(0).p(2) = VertexInterp(iso, g.p(v2), g.p(v3), g.val(v2), g.val(v3))
					ntri += 1
					Exit Select
				Case &Ha, &H5
					tri(0).p(0) = VertexInterp(iso, g.p(v0), g.p(v1), g.val(v0), g.val(v1))
					tri(0).p(1) = VertexInterp(iso, g.p(v2), g.p(v3), g.val(v2), g.val(v3))
					tri(0).p(2) = VertexInterp(iso, g.p(v0), g.p(v3), g.val(v0), g.val(v3))
					ntri += 1
					tri(1).p(0) = tri(0).p(0)
					tri(1).p(1) = VertexInterp(iso, g.p(v1), g.p(v2), g.val(v1), g.val(v2))
					tri(1).p(2) = tri(0).p(1)
					ntri += 1
					Exit Select
				Case &H9, &H6
					tri(0).p(0) = VertexInterp(iso, g.p(v0), g.p(v1), g.val(v0), g.val(v1))
					tri(0).p(1) = VertexInterp(iso, g.p(v1), g.p(v3), g.val(v1), g.val(v3))
					tri(0).p(2) = VertexInterp(iso, g.p(v2), g.p(v3), g.val(v2), g.val(v3))
					ntri += 1
					tri(1).p(0) = tri(0).p(0)
					tri(1).p(1) = VertexInterp(iso, g.p(v0), g.p(v2), g.val(v0), g.val(v2))
					tri(1).p(2) = tri(0).p(2)
					ntri += 1
					Exit Select
				Case &H7, &H8
					tri(0).p(0) = VertexInterp(iso, g.p(v3), g.p(v0), g.val(v3), g.val(v0))
					tri(0).p(1) = VertexInterp(iso, g.p(v3), g.p(v2), g.val(v3), g.val(v2))
					tri(0).p(2) = VertexInterp(iso, g.p(v3), g.p(v1), g.val(v3), g.val(v1))
					ntri += 1
					Exit Select
			End Select

			Return (ntri)
		End Function
		Public Function VertexInterp(iso As Double, vect1 As Vector3, vect2 As Vector3, val1 As Double, val2 As Double) As Vector3
			Dim rv As New Vector3(vect1.X, vect1.Y, vect1.Z)
			Dim ISOlerp As Double = (iso - val1) / (val2 - val1)

			If Math.Abs(iso - val1) < 1E-05 Then
				Return (vect1)
			End If
			If Math.Abs(iso - val2) < 1E-05 Then
				Return (vect2)
			End If
			If Math.Abs(val1 - val2) < 1E-05 Then
				Return (vect1)
			End If
			rv.X = vect1.X + CSng(ISOlerp) * (vect2.X - vect1.X)
			rv.Y = vect1.Y + CSng(ISOlerp) * (vect2.Y - vect1.Y)
			rv.Z = vect1.Z + CSng(ISOlerp) * (vect2.Z - vect1.Z)


			Return rv
		End Function
	End Class

