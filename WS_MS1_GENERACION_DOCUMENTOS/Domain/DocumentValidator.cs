using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class DocumentValidator
    {
        public static bool IsValidContent(string content)
        {
            return !string.IsNullOrWhiteSpace(content) && content.Length <= 10000;
        }
    }
}
