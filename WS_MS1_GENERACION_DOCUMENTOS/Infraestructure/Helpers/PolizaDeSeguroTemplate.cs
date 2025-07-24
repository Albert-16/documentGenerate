using Application;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;

namespace Infraestructure.Helpers
{
    public class PolizaDeSeguroTemplate : IDocumentWriter
    {
        private readonly int Width = 2480; // A4 a 300 DPI
        private readonly int Height = 3508;
        private readonly int Margin = 80;
        private Bitmap _bitmap;
        private Graphics _gfx;
        private int _y;

        public string GenerarPolizaPdf(string rutaDestino)
        {
            _bitmap = new Bitmap(Width, Height);
            _gfx = Graphics.FromImage(_bitmap);
            _gfx.Clear(Color.White);
            _gfx.SmoothingMode = SmoothingMode.AntiAlias;
            _gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _y = Margin;

            DibujarEncabezado();
            DibujarCuadroDatosGenerales();
            DibujarSeccion("DATOS DEL ASEGURADO", new[]
            {
                "Nombre: Carlos Alberto",
                "Dirección: Residencial San Esteban",
                "Correo: carlos@email.com",
                "Edad: 30 años",
                "Teléfono: 9999-9999"
            });

            DibujarSeccion("PROGRAMA DE COBERTURAS", new[]
            {
                "Cobertura 1: Fallecimiento por cualquier causa",
                "Cobertura 2: Invalidez total y permanente",
                "Cobertura 3: Gastos funerarios"
            });

            DibujarPiePagina();

            var nombreArchivo = $"PolizaSimulada_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var rutaImagen = Path.Combine(rutaDestino, nombreArchivo);
            _bitmap.Save(rutaImagen, ImageFormat.Png);

            var rutaPdf = Path.Combine(rutaDestino, Path.GetFileNameWithoutExtension(nombreArchivo) + ".pdf");
            ExportarImagenComoPdf(rutaImagen, rutaPdf);
            File.Delete(rutaImagen);

            return rutaPdf;
        }

        private void DibujarEncabezado()
        {
            var rojoDavivienda = Color.FromArgb(206, 18, 18);
            _gfx.FillRectangle(new SolidBrush(rojoDavivienda), 0, 0, Width, 120);
            _gfx.DrawString("SEGURO DAVIDA PROTECCIÓN FAMILIA", new Font("Arial", 22, FontStyle.Bold), Brushes.White, Margin, 40);
            _y = 150;

            _gfx.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy}", new Font("Arial", 14), Brushes.Black, Width - 400, _y);
            _y += 40;
        }

        private void DibujarCuadroDatosGenerales()
        {
            _gfx.FillRectangle(Brushes.LightGray, Margin, _y, Width - 2 * Margin, 40);
            _gfx.DrawString("DATOS GENERALES", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, Margin + 10, _y + 8);
            _y += 60;

            string[] pares =
            {
                "Póliza: 20250723", "Canal Venta: Internet",
                "Empleado: Carlos", "Centro Costo: IT",
                "Cliente: Juan Pérez", "Gerente: Pedro Díaz",
                "Vig. Desde: 01/01/2025", "Vig. Hasta: 31/12/2025",
                "Suma Asegurada: L. 500,000", "Forma Pago: Anual"
            };

            for (int i = 0; i < pares.Length; i += 2)
            {
                _gfx.DrawString(pares[i], new Font("Arial", 14), Brushes.Black, Margin, _y);
                _gfx.DrawString(pares[i + 1], new Font("Arial", 14), Brushes.Black, Width / 2, _y);
                _y += 35;
            }

            _y += 20;
        }

        private void DibujarSeccion(string titulo, string[] lineas)
        {
            var color = Color.FromArgb(255, 99, 132);
            _gfx.FillRectangle(new SolidBrush(color), Margin, _y, Width - 2 * Margin, 40);
            _gfx.DrawString(titulo, new Font("Arial", 16, FontStyle.Bold), Brushes.White, Margin + 10, _y + 8);
            _y += 60;

            foreach (var linea in lineas)
            {
                _gfx.DrawString(linea, new Font("Arial", 14), Brushes.Black, Margin + 10, _y);
                _y += 30;
            }

            _y += 20;
        }

        private void DibujarPiePagina()
        {
            _gfx.DrawString("COBERTURA BÁSICA:", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, Margin, _y);
            _y += 30;
            _gfx.DrawString("• Básica por fallecimiento\n• Gastos funerarios\n• Invalidez total y permanente (PASIT)", new Font("Arial", 13), Brushes.Black, Margin, _y);
        }

        private void ExportarImagenComoPdf(string imagenPath, string pdfPath)
        {
            using (var printDoc = new PrintDocument())
            {
                printDoc.PrintPage += (sender, e) =>
                {
                    using (var img = Image.FromFile(imagenPath))
                    {
                        e.Graphics.DrawImage(img, e.MarginBounds);
                    }
                };

                printDoc.PrinterSettings.PrintToFile = true;
                printDoc.PrinterSettings.PrintFileName = pdfPath;
                printDoc.PrintController = new StandardPrintController();
                printDoc.Print();
            }
        }
    }
}
