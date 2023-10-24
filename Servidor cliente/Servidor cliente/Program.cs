using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        string serverIp = "192.168.1.8";
        int serverPort = 80;
        bool continuar = true;

        do
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(serverIp, serverPort);
                Console.WriteLine("Conectado al servidor...");

                NetworkStream stream = client.GetStream();

                Console.Write("Escribe tu nombre: ");
                string nombre = Console.ReadLine();
                Console.Write("Escribe tus apellidos: ");
                string apellidos = Console.ReadLine();
                Console.Write("Escribe tu edad: ");
                string edad = Console.ReadLine();

                string dataToSend = nombre + "|" + apellidos + "|" + edad;
                byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                stream.Write(data, 0, data.Length);

                data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string response = Encoding.ASCII.GetString(data, 0, bytesRead);
                Console.WriteLine("Respuesta del servidor: " + response);

                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Write("¿Deseas seguir insertando datos? (si/no): ");
            string respuesta = Console.ReadLine().ToLower();

            if (respuesta != "si")
            {
                if (respuesta == "no")
                {
                    continuar = false;
                }
                else
                {
                    Console.WriteLine("Respuesta no válida. Saliendo del programa.");
                    continuar = false;
                }
            }

        } while (continuar);
    }
}
