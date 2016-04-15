Module Main

    Public Tests As New List(Of Object)

    Public Sub Main()
        System.Windows.Forms.Application.EnableVisualStyles()

        Tests.Add(New Test_0_1)
        Tests.Add(New Test_0_1_Font)
        Tests.Add(New Test_0_2)
        Tests.Add(New Test_0_3)
        Tests.Add(New Test_0_5_PDF)
        Tests.Add(New Test_4_23_Rect)

        Application.Run(New FmTest)
    End Sub

End Module
