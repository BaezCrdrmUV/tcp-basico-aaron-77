using System;

namespace tcp_com
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 9000;

            if(args.Length > 1)
            {
                // IP:puerto
                string[] tempArray = args[1].Split(':');
                try
                {
                    ip = tempArray[tempArray.Length - 2];
                    ip = ip.Replace("//", "");
                    port = Int32.Parse(tempArray[tempArray.Length - 1]);
                }
                catch (System.Exception) { }
            }

            if(args.Length == 0 || String.IsNullOrEmpty(args[0]) || args[0] == "server")
            {
                 TCPServer server = new TCPServer(ip, port, true);
                server.Listen();
               
            }
            else if(args[0] == "client")
            {
               Console.WriteLine("ARGUMENTOS ENVIADOS: "+args.Length);
              string name = "Cliente";
              if(!String.IsNullOrEmpty(args[1])){
                 try
                {
                    Console.WriteLine("si entro");
                    name = args[1];
                }
                catch (Exception) {
                    Console.WriteLine("Error al asigar el nombre");
                 }
              }
               

                // Ejecución de cliente
                TCPClient client = new TCPClient(ip, port, name);
                client.Chat();
            }
        }
    }
}
