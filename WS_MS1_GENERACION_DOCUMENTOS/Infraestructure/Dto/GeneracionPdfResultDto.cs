using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Dto
{
    public class GeneracionPdfResultDto
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string RutaGuardada { get; set; }
    }

}
