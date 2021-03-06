﻿Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.io

Imports jp.co.systembase.report.renderer.pdf2.elementrenderer

Public Class PdfRendererSetting
    Implements ICloneable

    Public DummyElementRenderer As IElementRenderer
    Public DefaultFont As BaseFont = Nothing
    Public GaijiFont As BaseFont = Nothing
    Public ElementRendererMap As New Dictionary(Of String, IElementRenderer)
    Public FontMap As New Dictionary(Of String, BaseFont)
    Public ReplaceBackslashToYen As Boolean = False
    Public ShrinkFontSizeMin As Single
    Public UnderlineWidthCoefficient As Single

    Public Shared SkipInitialFontCreate As Boolean = False
    Private Shared _loaded As Boolean = False

    Public Sub New()
        If Not SkipInitialFontCreate Then
            If Not _loaded Then
                StreamUtil.AddToResourceSearch("iTextAsian.dll")
                StreamUtil.AddToResourceSearch(My.Application.Info.DirectoryPath & "\iTextAsian.dll")
                _loaded = True
            End If
        End If
        Me.DummyElementRenderer = New DummyRenderer
        Me.ElementRendererMap.Add("rect", New RectRenderer)
        Me.ElementRendererMap.Add("circle", New CircleRenderer)
        Me.ElementRendererMap.Add("line", New LineRenderer)
        Me.ElementRendererMap.Add("field", New FieldRenderer)
        Me.ElementRendererMap.Add("text", New TextRenderer)
        Me.ElementRendererMap.Add("barcode", New BarcodeRenderer)
        Me.ElementRendererMap.Add("image", New ImageRenderer)
        Me.ElementRendererMap.Add("subpage", New SubPageRenderer)
        If Not SkipInitialFontCreate Then
            Me.DefaultFont = BaseFont.CreateFont("HeiseiKakuGo-W5", "UniJIS-UCS2-H", BaseFont.NOT_EMBEDDED)
            Me.FontMap.Add("gothic", Me.DefaultFont)
            Me.FontMap.Add("mincho", BaseFont.CreateFont("HeiseiMin-W3", "UniJIS-UCS2-H", BaseFont.NOT_EMBEDDED))
        Else
            Me.DefaultFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED)
        End If
        Me.ShrinkFontSizeMin = 4.0F
        Me.UnderlineWidthCoefficient = 1.0F
    End Sub

    Private Sub New(ByVal setting As PdfRendererSetting)
        Me.DummyElementRenderer = setting.DummyElementRenderer
        For Each k As String In setting.ElementRendererMap.Keys
            Me.ElementRendererMap.Add(k, setting.ElementRendererMap(k))
        Next
        Me.DefaultFont = setting.DefaultFont
        For Each k As String In setting.FontMap.Keys
            Me.FontMap.Add(k, setting.FontMap(k))
        Next
        Me.ReplaceBackslashToYen = setting.ReplaceBackslashToYen
        Me.ShrinkFontSizeMin = setting.ShrinkFontSizeMin
    End Sub

    Public Function GetElementRenderer(ByVal key As String) As IElementRenderer
        If Not String.IsNullOrEmpty(key) AndAlso Me.ElementRendererMap.ContainsKey(key) Then
            Return Me.ElementRendererMap(key)
        Else
            Return Me.DummyElementRenderer
        End If
    End Function

    Public Function GetFont(ByVal key As String) As BaseFont
        If Not String.IsNullOrEmpty(key) AndAlso Me.FontMap.ContainsKey(key) Then
            Return Me.FontMap(key)
        Else
            Return Me.DefaultFont
        End If
    End Function

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New PdfRendererSetting(Me)
    End Function

End Class
