using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Timers;

namespace TCP_Socket
{
    public class ServerRequest
    {
        public List<Socket> Sockets;
        public ServerRequest(List<Socket> socets)
        {
            Sockets = socets;
        }

        public void ServerWork(int i)
        {
            // буфер для накопления входящих данных
            var response = new List<byte>();
            // буфер для считывания одного байта
            var bytesRead = new byte[1];
            //считываем данные до конечного
            if(i == 0)
            {
                while (true)
                {
                    var count = Sockets[i].Receive(bytesRead);
                    // смотрим, если считанный байт представляет конечный символ, выходим
                    if (count == 0 || bytesRead[0] == '\n') break;
                    // иначе добавляем в буфер
                    response.Add(bytesRead[0]);
                }                
            }
            else if(i == 1)
            {
                while (true)
                {
                    var count = Sockets[i].Receive(bytesRead);
                    // смотрим, если считанный байт представляет конечный символ, выходим
                    if (count == 0 || bytesRead[0] == '\n') break;
                    // иначе добавляем в буфер
                    response.Add(bytesRead[0]);
                }
            }

            var cord = Encoding.UTF8.GetString(response.ToArray());

            Console.WriteLine($"Координата мышки от {Sockets[i].RemoteEndPoint} {cord}");

            if(i == 1)
            { Sockets[0].Send(Encoding.UTF8.GetBytes($"{cord} \n")); }
            else { Sockets[1].Send(Encoding.UTF8.GetBytes($"{cord} \n")); }            
            
            response.Clear();
        }
    }
}
