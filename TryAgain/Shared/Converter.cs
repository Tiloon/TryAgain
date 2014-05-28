using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared
{
    /*
     * J'utilise cette classe pour convertir un float en string et iversement.
     * "Pourquoi faire compliquer quand on peut faire du brainfuck"
     *          - Tilon, à propos du code de moi
     */
    public class Converter
    {
        public static String FloatToString(float f)
        {
            return Convert.ToBase64String(BitConverter.GetBytes(f));
        }

        public static float StringToFloat(String str)
        {
            return System.BitConverter.ToSingle(Convert.FromBase64String(str), 0);
        }
    }
}
