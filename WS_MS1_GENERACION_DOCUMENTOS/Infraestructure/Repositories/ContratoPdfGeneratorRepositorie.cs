using Infraestructure.Dto;
using Infrastructure.Dto;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Configuration;
using System.IO;

/// <summary>
/// Clase responsable de generar y guardar contratos legales en formato PDF.
/// Utiliza MigraDoc para el formato profesional del documento.
/// </summary>
public class ContratoPdfGeneratorRepositorie
{
    /// <summary>
    /// Genera un documento PDF legal con los datos proporcionados y lo guarda en el sistema de archivos.
    /// </summary>
    /// <param name="nombre">Nombre completo del firmante.</param>
    /// <param name="direccion">Dirección del firmante.</param>
    /// <param name="contenido">Texto del contrato con marcadores dinámicos.</param>
    /// <param name="rutaDestino">Ruta base donde se guardará el PDF.</param>
    /// <returns>Resultado de la operación incluyendo éxito, mensaje y ruta del archivo.</returns>
    public GeneracionPdfResultDto GuardarContratoLegal(string nombre, string direccion, string contenido, string rutaDestino)
    {
        var resultado = new GeneracionPdfResultDto();

        try
        {
            // Cargar plantilla de contrato con datos dinámicos
            string contenidoContrato = CargarTextoContrato(nombre, direccion);

            // Generar PDF desde el contenido
            byte[] pdfBytes = GenerarContratoPdf(nombre, direccion, contenidoContrato);

            // Guardar el PDF generado en disco
            string rutaFinal, mensajeError;
            string nombreArchivo = $"{nombre}_{DateTime.Now:yyyyMMddHHmmss}";

            bool ok = FileSaveHelper.GuardarPdfSeguro(pdfBytes, rutaDestino, nombreArchivo, out rutaFinal, out mensajeError);

            if (ok)
            {
                resultado.Exito = true;
                resultado.Mensaje = "PDF generado correctamente.";
                resultado.RutaGuardada = rutaFinal;
            }
            else
            {
                resultado.Exito = false;
                resultado.Mensaje = mensajeError;
            }
        }
        catch (Exception ex)
        {
            resultado.Exito = false;
            resultado.Mensaje = $"Error inesperado: {ex.Message}";
        }

        return resultado;
    }

    /// <summary>
    /// Genera el contenido PDF del contrato con formato profesional.
    /// </summary>
    /// <param name="nombre">Nombre del firmante.</param>
    /// <param name="direccion">Dirección del firmante.</param>
    /// <param name="contenidoDinamico">Contenido del contrato con formato.</param>
    /// <returns>PDF generado en bytes.</returns>
    public byte[] GenerarContratoPdf(string nombre, string direccion, string contenidoDinamico)
    {
        var doc = new Document();
        doc.Info.Title = "Contrato Legal";

        // Definir estilos generales del documento
        DefinirEstilos(doc);

        // Agregar sección principal con márgenes reducidos
        var sec = doc.AddSection();
        sec.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
        sec.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);
        sec.PageSetup.LeftMargin = Unit.FromCentimeter(2);
        sec.PageSetup.RightMargin = Unit.FromCentimeter(2);

        // Título centrado sin subrayado
        var titulo = sec.AddParagraph("CONTRATO DE APERTURA DE CUENTA DE AHORRO MEDIANTE ACEPTACIÓN ELECTRÓNICA");
        titulo.Style = "TituloContrato";

        // Introducción personalizada
        //var intro = sec.AddParagraph();
        //intro.Format.Alignment = ParagraphAlignment.Justify;
        //intro.AddText("Yo, ");
        //intro.AddFormattedText(nombre, TextFormat.Underline);
        //intro.AddText(" con domicilio en ");
        //intro.AddFormattedText(direccion, TextFormat.Underline);

        //// Cuerpo del contrato con formato dinámico
        ContratoFormatoHelper.DrawTextoConFormato(sec, contenidoDinamico);

        // Renderizar el documento PDF
        var renderer = new PdfDocumentRenderer();
        renderer.Document = doc;
        renderer.RenderDocument();

        using (var ms = new MemoryStream())
        {
            renderer.PdfDocument.Save(ms, false);
            return ms.ToArray();
        }
    }

    /// <summary>
    /// Define los estilos tipográficos reutilizables del documento.
    /// </summary>
    /// <param name="doc">Documento MigraDoc al que se le aplicarán estilos.</param>
    private void DefinirEstilos(Document doc)
    {
        // Estilo normal para el cuerpo
        var normal = doc.Styles["Normal"];
        normal.Font.Name = "Times New Roman";
        normal.Font.Size = 12;

        // Estilo para título sin subrayado
        var heading = doc.Styles.AddStyle("TituloContrato", "Normal");
        heading.Font.Bold = true;
        heading.Font.Size = 14;
        heading.ParagraphFormat.Alignment = ParagraphAlignment.Center;
        heading.ParagraphFormat.SpaceAfter = "0.5cm";
        heading.Font.Underline = Underline.Single;
    }

    /// <summary>
    /// Carga el texto base del contrato reemplazando marcadores por valores reales.
    /// </summary>
    /// <param name="nombre">Nombre del firmante.</param>
    /// <param name="direccion">Dirección del firmante.</param>
    /// <returns>Texto del contrato con valores dinámicos.</returns>
    private string CargarTextoContrato(string nombre, string direccion)
    {
        string texto = ContratoTextoBaseDto.ObtenerTexto();
        texto = texto.Replace("{NOMBRE}", nombre);
        texto = texto.Replace("{DIRECCION}", direccion);
        return texto;
    }
}
