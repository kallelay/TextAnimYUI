Imports System.Math
Module animations_declaration

    Function Animation_Linear(ByVal mStep, ByVal mCount) As Single
        Return mStep / mCount
    End Function


    Function Animation_SmoothCos(ByVal mStep, ByVal mCount) As Single
        Return (Sin(mStep / mCount * PI) + 1) / 2
    End Function

    Function Animation_SmoothCosBoomerang(ByVal mStep, ByVal mCount) As Single
        Return (Sin(mStep / mCount * 2 * PI) + 1) / 2
    End Function

    Function Animation_EaseInQuad(ByVal mStep, ByVal mCount) As Single
        Return (mStep / mCount) ^ 2
    End Function

    Function Animation_AccInSqrt(ByVal mStep, ByVal mCount) As Single
        Return Sqrt(mStep / mCount)
    End Function

    Function Animation_EaseOutQuad(ByVal mStep, ByVal mCount) As Single
        Return -(mStep / mCount) * (mStep / mCount - 2) ' oder gibt es auch diese Funktion, Quelle: https://www.gizma.com/easing/#quad1
        ' linDisp = (8 * mStep / Frames(LatestFrame).ImageCount) / Math.Sqrt(1 + (8 * mStep / Frames(LatestFrame).ImageCount) ^ 2) 'sigmoid function (eine der möglichen Variaten)
    End Function

    Function Animation_EaseInOutArctanAsSigmoid(ByVal mStep, ByVal mCount) As Single
        Dim t! = mStep / mCount
        Dim a! = 4.2
        Return (Math.Atan(t * 2 * a - a) / Math.Atan(a) + 1) / 2 'Sigmoid function, with parameterization element a 


        ' 'Not much "impact"! (interpolation)
        '(mStep / Frames(LatestFrame).ImageCount) ^ 2 * (1 - (mStep / Frames(LatestFrame).ImageCount)) - _
        '  (mStep / Frames(LatestFrame).ImageCount) * (mStep / Frames(LatestFrame).ImageCount - 2) * (mStep / Frames(LatestFrame).ImageCount)

    End Function
  


End Module
