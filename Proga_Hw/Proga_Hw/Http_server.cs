using System;
using System.Net;
using System.IO;
using System.Text;

namespace Proga_Hw
{
    public class Http_server
    {
        public void Start(string prefix)
        {
            var listener = new HttpListener();            
            listener.Prefixes.Add(prefix);
            listener.Start();
            Treatment(listener);
        }

        public void Treatment(HttpListener listener)
        {            
                Console.WriteLine("Ожидание подключений...");                
                // метод GetContext блокирует текущий поток, ожидая получение запроса 
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                // получаем объект ответа
                HttpListenerResponse response = context.Response;                
            //преобразуем объект
            try
                {
                    FileStream fstream = File.OpenRead(@"C:\Users\User\Desktop\Google_Home_Page\Google\Google.html");
                    byte[] buffer = new byte[fstream.Length];
                    fstream.Read(buffer, 0, buffer.Length);
                    string textFromFile = Encoding.Default.GetString(buffer);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Print Restart to restart server");
                    if (String.Compare(Console.ReadLine(), "Restart") == 0)
                    {
                        listener.Stop();
                        Start("http://localhost:8888/connection/");
                    }
                    else { Console.WriteLine("Incorrect message"); }
                }

            Console.WriteLine("If you want finish, Drop.\n Restart?");
            switch (Console.ReadLine())
                {
                    case "Drop": Finish(listener); break;
                    case "Restart": listener.Stop(); Start("http://localhost:8888/connection/"); break;
                    case "http://localhost:8887/connection/" : listener.Prefixes.Remove("http://localhost:8888/connection/");
                    listener.Prefixes.Add("http://localhost:8887/connection/"); listener.Stop(); Start("http://localhost:8887/connection/");
                    break;
                }



        }

        public void Finish( HttpListener listener)
        {            
            listener.Stop();
            Console.WriteLine("Обработка подключений завершена");
            Console.Read();
        }

    }
}