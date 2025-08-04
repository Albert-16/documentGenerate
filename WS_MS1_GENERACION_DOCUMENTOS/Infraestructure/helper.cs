using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;

namespace Infraestructure.Helper
{
    public class PdfTextoAutoHelper
    {
        private readonly PdfDocument _document;
        private XGraphics _gfx;
        private XTextFormatter _formatter;
        private PdfPage _currentPage;
        private double _x, _y, _margin, _ancho;
        private readonly double _altoMaximoPorPagina;

        public PdfTextoAutoHelper(PdfDocument document, double margin = 50)
        {
            _document = document;
            _margin = margin;
            AgregarPagina();
        }

        public void DrawParrafo(string texto, XFont font, XBrush brush, double espacioDespues = 20)
        {
            var lineHeight = font.GetHeight();
            var lineas = RomperEnLineas(texto, font);

            foreach (string linea in lineas)
            {
                if (_y + lineHeight > _altoMaximoPorPagina)
                    AgregarPagina();

                _formatter.DrawString(linea, font, brush, new XRect(_x, _y, _ancho, lineHeight));
                _y += lineHeight;
            }

            _y += espacioDespues;
        }

        public void DrawTitulo(string texto, XFont font, XBrush brush, XStringFormat formato, double alto = 30, double espacioDespues = 20)
        {
            if (_y + alto > _altoMaximoPorPagina)
                AgregarPagina();

            _gfx.DrawString(texto, font, brush, new XRect(_x, _y, _ancho, alto), formato);
            _y += alto + espacioDespues;
        }

        private void AgregarPagina()
        {
            _currentPage = _document.AddPage();
            _gfx = XGraphics.FromPdfPage(_currentPage);
            _formatter = new XTextFormatter(_gfx) { Alignment = XParagraphAlignment.Justify };
            _x = _margin;
            _y = _margin;
            _ancho = _currentPage.Width - 2 * _margin;
            _altoMaximoPorPagina = _currentPage.Height - _margin;
        }

        private List<string> RomperEnLineas(string texto, XFont font)
        {
            var resultado = new List<string>();
            string[] parrafos = texto.Split('\n');

            foreach (string parrafo in parrafos)
            {
                string[] palabras = parrafo.Split(' ');
                string lineaActual = "";

                foreach (string palabra in palabras)
                {
                    string posible = string.IsNullOrEmpty(lineaActual)
                        ? palabra
                        : lineaActual + " " + palabra;

                    double anchoTexto = _gfx.MeasureString(posible, font).Width;

                    if (anchoTexto <= _ancho)
                    {
                        lineaActual = posible;
                    }
                    else
                    {
                        resultado.Add(lineaActual);
                        lineaActual = palabra;
                    }
                }

                if (!string.IsNullOrWhiteSpace(lineaActual))
                    resultado.Add(lineaActual);

                resultado.Add(""); // salto de línea entre párrafos
            }

            return resultado;
        }
    }
}
