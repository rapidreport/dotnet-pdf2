Imports System.IO

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.pdf2

Public Class Test_0_3

    Public Overrides Function ToString() As String
        Return "0.3 バーコード"
    End Function

    Public Sub Run()
        Dim name As String = "test_0_3"

        Dim report As New Report(Json.Read("rrpt\" & name & ".rrpt"))
        report.GlobalScope.Add("time", Now)
        report.GlobalScope.Add("lang", "vb")
        report.Fill(DummyDataSource.GetInstance)

        Dim pages As ReportPages = report.GetPages()

        Using fs As New FileStream("out\" & name & ".pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            pages.Render(renderer)
        End Using

        MessageBox.Show(name & ".pdfを出力しました。")
    End Sub

End Class
