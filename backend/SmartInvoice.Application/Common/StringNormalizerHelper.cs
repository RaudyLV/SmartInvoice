using System.Text.RegularExpressions;

namespace SmartInvoice.Application.Common
{
    public static class StringNormalizerHelper
    {
        public static string NormalizeSearchTerm(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string normalized = input.ToLowerInvariant();

            //1. Reemplaza guiones, barras, etc por un espacio
            normalized = Regex.Replace(normalized, @"[-/]", " ");

            //2, Reemplaza todo lo que no sea un numero o una letra o espacio
            normalized = Regex.Replace(normalized, @"[^a-z0-9\s]", "");

            //3. Reemplaza 2 o mas espacios en blanco y deja solo uno de diferencia
            //tambien corta los espacios al final y principio
            normalized = Regex.Replace(normalized, @"\s+", " ").Trim();

            return normalized;
        }
    }
}