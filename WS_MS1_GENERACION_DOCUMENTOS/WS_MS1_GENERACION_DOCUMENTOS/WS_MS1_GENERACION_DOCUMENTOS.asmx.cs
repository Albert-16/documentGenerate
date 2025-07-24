using Application;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WS_MS1_GENERACION_DOCUMENTOS
{
    /// <summary>
    /// Summary description for WS_MS1_GENERACION_DOCUMENTOS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WS_MS1_GENERACION_DOCUMENTOS : System.Web.Services.WebService
    {

        [WebMethod(Description = "Genera un archivo TXT con el contenido proporcionado.")]
        public string GenerateTxt(string content)
        {
            try
            {
                string basePath = Server.MapPath("~/TempFiles/");

                // Instanciar el writer desde infraestructura
                IDocumentWriter writer = new TxtDocumentWriter();

                // Instanciar el caso de uso desde application
                var useCase = new GenerateTxtUseCase(writer);

                // Ejecutar la generación
                string filePath = useCase.Execute(content, basePath);

                return $"Archivo generado exitosamente: {filePath}";
            }
            catch (Exception ex)
            {
                return $"Error al generar el archivo: {ex.Message}";
            }
        }
    }
}
