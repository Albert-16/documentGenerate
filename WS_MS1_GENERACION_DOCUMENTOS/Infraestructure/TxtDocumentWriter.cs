using System;
using System.IO;
using Application;

namespace Infrastructure
{
    public class TxtDocumentWriter : IDocumentWriter
    {
        public string SaveTxt(string content, string basePath)
        {
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string fileName = $"DOC_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string fullPath = Path.Combine(basePath, fileName);

            File.WriteAllText(fullPath, content);
            return fullPath;
        }
    }
}
