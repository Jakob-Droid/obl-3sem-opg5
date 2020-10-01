using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLib.Models;
using Newtonsoft.Json;

namespace TCP_Server_IndeKlima
{
    public class Server
    {
        private static List<FanOutPut> _fanOutPuts = new List<FanOutPut>()
        {
            new FanOutPut(1, "en", 20, 40),
            new FanOutPut(2, "to", 21, 41),
            new FanOutPut(3, "tre", 22, 42),
            new FanOutPut(4, "fire", 23, 43),
            new FanOutPut(5, "fem", 24, 44)
        };

        public void Start()
        {
            TcpListener serverSideListener = new TcpListener(IPAddress.Any, 4646);
            serverSideListener.Start();
            while (true)
            {
                TcpClient socket = serverSideListener.AcceptTcpClient();
                Console.WriteLine("Server Activated");
                Task.Run(() => DoClient(socket));
            }
        }

        public void DoClient(TcpClient client)
        {
            using (client)
            {
                while (true)
                {
                    Stream ns = client.GetStream();
                    StreamReader sr = new StreamReader(ns);
                    StreamWriter sw = new StreamWriter(ns);
                    string Method;
                    string Data;
                    sw.WriteLine(
                        "Hej og velkommen til!\nDu skal indtaste den metode du har lyst til at bruge, Hentalle, Hent eller Gem");
                    sw.Flush();
                    Method = sr.ReadLine().ToLower();

                    sw.AutoFlush = true;

                    switch (Method.ToLower())
                    {
                        case "hentalle":
                            sw.WriteLine(JsonConverter(_fanOutPuts));
                            break;
                        case "hent":
                            sw.WriteLine("Indskriv venligst id'et på den data du skal bruge");
                            Data = sr.ReadLine();
                            if (int.TryParse(Data, out int n))
                            {
                                sw.WriteLine(JsonConverter(_fanOutPuts.Find(i => i.Id == n)));
                                break;
                            }

                            break;
                        case "gem":
                            sw.WriteLine("Du skal nu indtaste den data du skal gemme, Id, Navn, Temp og fugtighed");
                            string[] lines = sr.ReadLine().Split(" ");
                            _fanOutPuts.Add(new FanOutPut(Convert.ToInt32(lines[0]), lines[1],
                                Convert.ToDouble(lines[2]), Convert.ToDouble(lines[3])));
                            break;
                    }
                }
            }
        }


        public string JsonConverter<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
