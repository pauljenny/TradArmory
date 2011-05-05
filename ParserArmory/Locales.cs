using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserArmory
{
    public static class Locale
    {
        public static Dictionary<ClientLocale, string> localeDictionary = new Dictionary<ClientLocale, string>((int)ClientLocale.End)
        {
            {ClientLocale.French, "fr"},
            {ClientLocale.German, "de"},
            {ClientLocale.Spanish, "es"},
            {ClientLocale.Russian, "ru"}
        };
    }
}
