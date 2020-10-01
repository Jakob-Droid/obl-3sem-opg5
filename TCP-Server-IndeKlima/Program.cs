using System;

namespace TCP_Server_IndeKlima
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Server server = new Server();
            server.Start();
            Console.WriteLine("Server Startet!");


        }
    }
}
