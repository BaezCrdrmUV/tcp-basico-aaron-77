using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace tcp_com
{
    public class TCPClient
    {
        TcpClient client;
        string IP;
        int Port;
        string Username;

        public TCPClient(string ip, int port, string username)
        {
            try
            {
                client = new TcpClient();
                this.IP = ip;
                this.Port = port;
                this.Username = username;
            }
            catch (System.Exception)
            {
                
            }
        }

        public void Chat()
        {
            Console.WriteLine("IP: " + IP);
            Console.WriteLine("Port: " + Port.ToString());
            client.Connect(IP, Port);   
            Console.WriteLine("Conectado");

            while(true)
            {
                try
                {
                    string msg = Console.ReadLine();
                    Message newMessage = new Message(msg, Username);
                    newMessage.idMessage = Guid.NewGuid().ToString();
                  
                    string fecha =  DateTime.Now.ToString("dd-MM-yyyy");
                    newMessage.Date = fecha;
                    
                    if(newMessage.MessageString.StartsWith("-r")){
                        
                        newMessage.Type = "remover";
                    }else if(newMessage.MessageString.StartsWith("-e")){
                        
                        newMessage.Type = "editar";
                    }else if(newMessage.MessageString.StartsWith("-bt")){
                        newMessage.Type="buscarPorUsuario";
                    }else if(newMessage.MessageString.StartsWith("-b")){
                            newMessage.Type = "buscar";
                    }
                    
                    string jsonMessage = JsonConvert.SerializeObject(newMessage);
                    // Envío de datos
                    var stream = client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(jsonMessage);
                    Console.WriteLine("Enviando datos...");
                    stream.Write(data, 0, data.Length);

                    // Recepción de mensajes
                    byte[] package = new byte[1024];
                    stream.Read(package);
                    string serverMessage = Encoding.UTF8.GetString(package);
                    Console.WriteLine(serverMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error {0}", ex.Message);
                    break;
                }
            }
            Console.WriteLine("Desconectado");
            return;
        }
    }
}