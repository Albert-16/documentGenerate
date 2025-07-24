using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IDocumentWriter
    {
      //  string SaveTxt(string content, string basePath);

        string GenerarPolizaPdf(string rutaDestino);

    }
}
// This interface defines a contract for saving text documents.