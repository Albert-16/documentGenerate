using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Dto
{
    public class ContratoComodatoDto
    {
        public string NombreAsegurado { get; set; }
        public string Cedula { get; set; }
        public string Lugar { get; set; }
        public string NombreRepresentante { get; set; }
        public DateTime FechaContrato { get; set; }
        public string DescripcionBien { get; set; }
    }

}
