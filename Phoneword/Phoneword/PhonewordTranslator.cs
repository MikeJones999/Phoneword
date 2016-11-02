using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword
{
    public static class PhonewordTranslator
    {
        public static string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            raw = raw.ToUpperInvariant();

            var newNumber = new StringBuilder();
            foreach(var chr in raw)
            {
                if (" -0123456789".Contains(chr))
                {
                    newNumber.Append(chr);
                }
                else
                {
                    var result = TranslateToNumber(chr);
                    if(result != null)
                    {
                        newNumber.Append(result);
                    }
                    else
                    {
                        //possible bad character
                        return null;
                    }
                }
            }
            return newNumber.ToString();
        }


        static readonly string[] digits = {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        private static object TranslateToNumber(char c)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Contains(c))
                {
                    return 2 + i;
                }
             
            }

            return null;
        }

        static bool Contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }
    }
}
