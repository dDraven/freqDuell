using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net.WebSockets;

namespace Family_Duell
{
    public class TcpConnect
    {
        TcpClient client;
        TcpListener listener;

        public delegate void RefreshResults(string text);
        public event RefreshResults eventRefreshResults;




        public TcpConnect()
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(S.server, S.port);
                client = new TcpClient();
                client.Connect(endpoint);

                Thread listenThread = new Thread(Listening);
                listenThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Listening()
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            
            string text = reader.ReadLine();


            eventRefreshResults(text);

            Listening();

        }

        
    }
}
