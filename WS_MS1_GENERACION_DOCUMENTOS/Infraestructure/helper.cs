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
        private double _altoMaximoPorPagina;

        public PdfTextoAutoHelper(PdfDocument document, double margin = 50)
        {
            _document = document;
            _margin = margin;
            AgregarPagina();
        }

        public void DrawParrafo(string texto, XFont font, XBrush brush, double espacioDespues = 20)
        {
            double alturaDisponible = _altoMaximoPorPagina - _y;

            // Medir altura total del párrafo
            var tamañoParrafo = MedirTexto(texto, font, _ancho);

            // Si no cabe completo, dividir en bloques que sí caben
            if (tamañoParrafo.Height > alturaDisponible)
            {
                var fragmentos = DividirTextoPorAltura(texto, font, _ancho, alturaDisponible);

                foreach (var bloque in fragmentos)
                {
                    if (_y + MedirTexto(bloque, font, _ancho).Height > _altoMaximoPorPagina)
                        AgregarPagina();

                    _formatter.DrawString(bloque, font, brush, new XRect(_x, _y, _ancho, _altoMaximoPorPagina - _y));
                    _y += MedirTexto(bloque, font, _ancho).Height + espacioDespues;
                }
            }
            else
            {
                _formatter.DrawString(texto, font, brush, new XRect(_x, _y, _ancho, tamañoParrafo.Height));
                _y += tamañoParrafo.Height + espacioDespues;
            }
        }

        private XSize MedirTexto(string texto, XFont font, double ancho)
        {
            var tf = new XTextFormatter(_gfx);
            var dummyRect = new XRect(0, 0, ancho, double.MaxValue);
            tf.DrawString(texto, font, XBrushes.Transparent, dummyRect); // invisible
            return _gfx.MeasureString(texto, font);
        }

        private List<string> DividirTextoPorAltura(string texto, XFont font, double ancho, double alturaDisponible)
        {
            var lineas = texto.Split(new[] { '\n' }, StringSplitOptions.None);
            var bloques = new List<string>();
            string bloqueActual = "";
            double alturaActual = 0;

            foreach (var linea in lineas)
            {
                string nuevoBloque = string.IsNullOrEmpty(bloqueActual) ? linea : bloqueActual + "\n" + linea;
                var altura = MedirTexto(nuevoBloque, font, ancho).Height;

                if (altura > alturaDisponible)
                {
                    bloques.Add(bloqueActual);
                    bloqueActual = linea;
                    alturaActual = MedirTexto(linea, font, ancho).Height;
                }
                else
                {
                    bloqueActual = nuevoBloque;
                    alturaActual = altura;
                }
            }

            if (!string.IsNullOrWhiteSpace(bloqueActual))
                bloques.Add(bloqueActual);

            return bloques;
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
