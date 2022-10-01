using System;
using System.Net;
using System.IO;

namespace Proga_Hw
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Http_server();
            server.Start("http://localhost:8888/google/");
        }
    }
}
