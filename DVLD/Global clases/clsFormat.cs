using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD.Classes
{
    public class clsFormat
    {
        public static string DateToShort(DateTime Dt1)
        {
            
            return Dt1.ToString("dd/MMM/yyyy");
        } 

        public static string DateToString(DateTime dt1)
        {
            return dt1.ToString("ddd/MM/yyyy");
        }
        public static DateTime DateTime1(string ST1)
        {
            return DateTime.Parse(ST1);
        }




    }
}
