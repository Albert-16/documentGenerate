using Infraestructure.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WS_MS1_GENERACION_DOCUMENTOS
{
    /// <summary>
    /// Summary description for WS_MS1_GENERACION_DOCUMENTOS
    /// </summary>
    [WebService(Namespace = "http://WS_MS1_GENERACION_DOCUMENTOS/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WS_MS1_GENERACION_DOCUMENTOS : System.Web.Services.WebService
    {

        [WebMethod(Description = "Generacion de Documentos")]
        public GeneracionPdfResultDto GenerarContratoLegal(string nombre, string direccion)
        {
            var resultado = new GeneracionPdfResultDto();

            try
            {
                var repositorio = new ContratoPdfGeneratorRepositorie();
                string rutaDestino = @"C:\ContratosGenerados";

                resultado = repositorio.GuardarContratoLegal(nombre, direccion, null, rutaDestino);
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = $"Error interno en el servicio: {ex.Message}";
                resultado.RutaGuardada = null;

                // Opcional: log en archivo o base de datos aquí
            }

            return resultado;
        }
    }
}
