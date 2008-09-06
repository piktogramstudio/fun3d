Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

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
End Module
