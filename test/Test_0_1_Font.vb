Imports System.IO

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.pdf2

Imports iTextSharp.text.pdf

Public Class Test_0_1_Font

    Public Overrides Function ToString() As String
        Return "0.1 フォント埋め込み"
    End Function

    Public Sub Run()
        Dim name As String = "test_0_1_Font"

        Dim setting As PdfRendererSetting = New PdfRendererSetting()
        setting.FontMap("gothic") = BaseFont.CreateFont("C:\Windows\Fonts\msgothic.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        setting.FontMap("mincho") = BaseFont.CreateFont("C:\Windows\Fonts\msmincho.ttc,0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)

        Dim report As New Report(Json.Read("rrpt\" & name & ".rrpt"))
        report.GlobalScope.Add("time", Now)
        report.GlobalScope.Add("lang", "vb")
        report.Fill(DummyDataSource.GetInstance)

        Dim pages As ReportPages = report.GetPages()

        Using fs As New FileStream("out\" & name & ".pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs, setting)
            pages.Render(renderer)
        End Using

        MessageBox.Show(name & ".pdfを出力しました。")
    End Sub

End Class
