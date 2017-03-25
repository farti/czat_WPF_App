using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Serwer
{
    class Serwer
    {
        static List<Socket> clients = new List<Socket>();       // lista klientów , czyli gniazd , czyli połaczeń

        static void Main(string[] args)
        {

            //TODO: 1. Utworzenie punktu nadsłuchiwania - IPEndPoint
            var endpoint = new IPEndPoint(IPAddress.IPv6Any, 2000);
            //TODO: 2. Utworzenie gniazda nadsłuchiwania - Socket
            var server = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  // ?????????????
            //TODO: 3. Przypisanie gniazda do adresu IP - Socket.Bind
            server.Bind(endpoint);
            //TODO: 4. Rozpoczęcie nadsłuchiwania - Socket.Listen
            server.Listen(10);
            //TODO: 5. W pętli oczekiwanie na połączenia - Socket.Accept
            while (true)
            {
                var client = server.Accept();
                clients.Add(client);
                var clientServe = new Task(          // 1) DEKLARACJAzadanie asynchroniczne klienta
                    () =>
                    {//  3)
                        while (true)
                        {
                            var data = new byte[70];    // nie znamy rozmiaru , na wyższą ocenę !!
                            client.Receive(data);   // oczekiwanie na dane
                            foreach (var c in clients)
                            {
                                if (c != client)  // eliminacja wysyłania do siebie
                                {
                                    //TODO: 6. Przesłanie danych klientowi - Socket.Send(ASCIIEncoding)
                                    c.Send(data);                             // c.sendfile do plikow !!!!!
                                }
                            }
                        }
                    });
                clientServe.Start();       // 2) URUCHOMIENIE   uruchomienie zadania
            }

        }
    }
}
