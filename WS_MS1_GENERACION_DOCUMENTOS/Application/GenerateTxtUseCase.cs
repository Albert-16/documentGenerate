using System;
using Domain;

namespace Application
{
    public class GenerateTxtUseCase
    {
        private readonly IDocumentWriter _writer;

        public GenerateTxtUseCase(IDocumentWriter writer)
        {
            _writer = writer;
        }

        public string Execute(string content, string basePath)
        {
            if (!DocumentValidator.IsValidContent(content))
                throw new ArgumentException("Contenido inválido.");

            return _writer.SaveTxt(content, basePath);
        }
    }
}
