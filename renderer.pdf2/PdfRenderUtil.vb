Imports System.Text.RegularExpressions
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports jp.co.systembase.report.component

Public Module PdfRenderUtil

    Public Function GetColor(ByVal v As String) As BaseColor
        If Not String.IsNullOrEmpty(v) Then
            If v.StartsWith("#") Then
                Dim _v As String = v.Substring(1).ToLower
                For i As Integer = 0 To 5
                    If "0123456789abcdef".IndexOf(_v(i)) < 0 Then
                        Return Nothing
                    End If
                Next
                Return New BaseColor(Convert.ToInt32(_v.Substring(0, 2), 16), _
                                     Convert.ToInt32(_v.Substring(2, 2), 16), _
                                     Convert.ToInt32(_v.Substring(4, 2), 16))
            Else
                If Array.IndexOf(RenderUtil.COLOR_NAMES, v.ToLower) >= 0 Then
                    Dim c As System.Drawing.Color = System.Drawing.Color.FromName(v)
                    Return New BaseColor(c.R, c.G, c.B)
                End If
            End If
        End If
        Return Nothing
    End Function

End Module