using Application;
using Infraestructure.Helpers;
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

        [WebMethod(Description = "Genera una póliza en formato PDF simulada.")]
        public string GenerarPoliza()
        {
            try
            {
                string basePath = Server.MapPath("~/TempFiles/");
                IDocumentWriter writer = new PolizaDeSeguroTemplate();
                var useCase = new GeneratePdfUseCase(writer);
                string rutaFinal = useCase.Execute(basePath);
                return $"PDF generado exitosamente: {rutaFinal}";
            }
            catch (Exception ex)
            {
                return $"Error al generar el PDF: {ex.Message}";
            }
        }

    }
}
