using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Drawing;

namespace Family_Duell
{
    static class S
    {
        public static IPAddress server = IPAddress.Parse("192.168.178.38");
        public static int port = 4713;

        public static Color backcolor = System.Drawing.ColorTranslator.FromHtml("#7f8080");

        public static Color rowcolor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        public static Color leftcolor = System.Drawing.ColorTranslator.FromHtml("#524595");

        public static Color rightcolor = System.Drawing.ColorTranslator.FromHtml("#DFB24F");

        public static TcpConnect client;

    }
}
