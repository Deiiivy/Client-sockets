using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
      
        string ip = "127.0.0.1";
        int port = 8888; 

        while (true)
        {
           
            TcpClient client = new TcpClient();

            try
            {
                
                client.Connect(ip, port);
                Console.WriteLine("Conectado al servidor");

                
                NetworkStream stream = client.GetStream();

               
                Console.Write("Ingrese la operación matemática (o 'exit' para salir): ");
                string operation = Console.ReadLine();

                if (operation.ToLower() == "exit")
                    break;

             
                byte[] data = Encoding.ASCII.GetBytes(operation);
                stream.Write(data, 0, data.Length);


                data = new byte[256];
                StringBuilder response = new StringBuilder();
                int bytes = 0;

                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    if (bytes == 0) break; 
                    response.Append(Encoding.ASCII.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

               
                Console.WriteLine("Resultado: " + response.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("Desconectado del servidor");
            }
        }
    }
}
