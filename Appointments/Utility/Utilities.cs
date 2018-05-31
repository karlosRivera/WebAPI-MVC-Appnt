using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointments.Utility
{
    public static class Utilities
    {
        public static string RemoveJunkChar(string data)
        {
            string retdata = "";
            foreach (char c in data)
            {
                if (c == '%')
                {
                    retdata += "@";
                }
                else if (c == '4')
                {
                    retdata += "";
                }
                else if (c == '0')
                {
                    retdata += "";
                }
                else
                {
                    retdata += c;
                }

            }
            return retdata;
        }
    }
}