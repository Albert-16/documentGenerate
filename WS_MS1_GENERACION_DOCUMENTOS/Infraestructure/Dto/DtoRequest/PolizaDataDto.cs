using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Dto.DtoRequest
{
    public class PolizaDataDto
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Poliza { get; set; } = "POL-20250723";
        public string Cliente { get; set; } = "Juan Pérez";
        public string CanalVenta { get; set; } = "Online";
        public string Categoria { get; set; } = "Oro";
        public string Ramo { get; set; } = "Vida";
        public string Ocupacion { get; set; } = "Ingeniero";
        public string SumaAsegurada { get; set; } = "$500,000";
        public string FormaPago { get; set; } = "Mensual";
        public string CuotaPagar { get; set; } = "$1,500";
        // Agrega los campos que se muestran en la imagen...
    }

}
