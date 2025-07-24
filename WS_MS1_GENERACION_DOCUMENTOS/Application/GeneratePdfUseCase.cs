using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
  public class GeneratePdfUseCase
    {
        private readonly IDocumentWriter _writer;

        public GeneratePdfUseCase(IDocumentWriter writer)
        {
            _writer = writer;
        }

        public string Execute(string basePath)
        {
            return _writer.GenerarPolizaPdf(basePath);
        }
    }
}
