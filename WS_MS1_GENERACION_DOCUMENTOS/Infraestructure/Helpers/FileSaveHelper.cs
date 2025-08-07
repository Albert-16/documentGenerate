using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public static class FileSaveHelper
{
    public static bool GuardarPdfSeguro(byte[] contenido, string rutaCarpeta, string nombreArchivo, out string rutaFinal, out string mensajeError)
    {
        rutaFinal = string.Empty;
        mensajeError = string.Empty;

        try
        {
            if (contenido == null || contenido.Length == 0)
            {
                mensajeError = "El contenido del archivo PDF está vacío.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(rutaCarpeta) || string.IsNullOrWhiteSpace(nombreArchivo))
            {
                mensajeError = "La ruta o el nombre del archivo es inválido.";
                return false;
            }

            // Quitar caracteres inválidos
            nombreArchivo = RemoverCaracteresInvalidos(nombreArchivo);

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            rutaFinal = Path.Combine(rutaCarpeta, nombreArchivo + ".pdf");

            File.WriteAllBytes(rutaFinal, contenido);
            return true;
        }
        catch (Exception ex)
        {
            mensajeError = $"Error al guardar el archivo: {ex.Message}";
            return false;
        }
    }

    private static string RemoverCaracteresInvalidos(string input)
    {
        // Elimina tildes, ñ y caracteres especiales
        string limpio = Regex.Replace(input.Normalize(NormalizationForm.FormD), @"[^A-Za-z0-9_\- ]+", "");
        limpio = limpio.Replace(" ", "_");
        return limpio;
    }
}
