using MigraDoc.DocumentObjectModel;
using System;
using System.Text.RegularExpressions;

public static class ContratoFormatoHelper
{
    public static void DrawTextoConFormato(Section sec, string texto)
    {
        // Divide en párrafos
        string[] parrafos = texto.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var linea in parrafos)
        {
            var parrafo = sec.AddParagraph();
            parrafo.Format.SpaceAfter = "0.2cm";

            // Si el párrafo es muy corto, se alinea a la izquierda
            if (linea.Trim().Length < 60)
                parrafo.Format.Alignment = ParagraphAlignment.Left;
            else
                parrafo.Format.Alignment = ParagraphAlignment.Justify;

            // Analiza formato especial **negrita**, __subrayado__, //itálica//
            string pattern = @"(\*\*(.*?)\*\*|__([^_]+)__|\/\/(.*?)\/\/)";
            var matches = Regex.Matches(linea, pattern);

            int lastIndex = 0;
            foreach (Match match in matches)
            {
                // Agrega texto normal antes del match
                if (match.Index > lastIndex)
                {
                    string normalText = linea.Substring(lastIndex, match.Index - lastIndex);
                    parrafo.AddText(normalText);
                }

                string formato = match.Value;
                string contenidoPlano = match.Groups[2].Value != "" ? match.Groups[2].Value :
                                        match.Groups[3].Value != "" ? match.Groups[3].Value :
                                        match.Groups[4].Value;

                var textoFormateado = parrafo.AddFormattedText(contenidoPlano);

                if (match.Value.StartsWith("**")) textoFormateado.Bold = true;
                if (match.Value.StartsWith("__")) textoFormateado.Underline = Underline.Single;
                if (match.Value.StartsWith("//")) textoFormateado.Italic = true;

                lastIndex = match.Index + match.Length;
            }

            // Agrega texto restante después del último match
            if (lastIndex < linea.Length)
                parrafo.AddText(linea.Substring(lastIndex));
        }
    }
}
