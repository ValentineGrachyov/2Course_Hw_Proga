using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HttpServer.Attributes;
using System.Reflection;
using System.Text.Json;

namespace HttpServer
{
    public class HttpServer : IDisposable
    {
        public ServerStatus Status = ServerStatus.Stop;
        
        private ServerSettings _serverSettings;
        private readonly HttpListener _httpListener;
        public HttpServer()
        {
            _serverSettings = ServerSettings.Deserialize();
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://localhost:" + _serverSettings.Port + "/");
        }

        public void Start()
        {
            if (Status == ServerStatus.Start)
            {
                Console.WriteLine("Сервер уже запущен!");
            }
            else
            {
                Console.WriteLine("Запуск сервера...");
                _httpListener.Start();
                Console.WriteLine("Ожидает подключения");
                Status = ServerStatus.Start;
            }

            Listening();
        }
        public void Stop()
        {
            if (Status == ServerStatus.Start)
            {
                _httpListener.Stop();
                Status = ServerStatus.Stop;
                Console.WriteLine("Обработка подключений завершена");
            }
            else
                Console.WriteLine("Сервер уже остановлен");
        }

        private async void Listening()
        {
            while (_httpListener.IsListening)
            {
                var context = await _httpListener.GetContextAsync();

                if (MethodHandler(context)) { Listening(); };

                MainSite(context.Request, context.Response);
            }

        }

        private bool MethodHandler(HttpListenerContext _httpContext)
        {
            // объект запроса
            HttpListenerRequest request = _httpContext.Request; 

            // объект ответа
            HttpListenerResponse response = _httpContext.Response;

            if (_httpContext.Request.Url.Segments.Length < 2) return false;

            string controllerName = _httpContext.Request.Url.Segments[1].Replace("/", "");

            string[] strParams = _httpContext.Request.Url
                                    .Segments
                                    .Skip(2)
                                    .Select(s => s.Replace("/", ""))
                                    .ToArray();

            var assembly = Assembly.GetExecutingAssembly();

            var controller = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HttpController)))
                                     .FirstOrDefault(c => c.Name.ToLower() == controllerName.ToLower());

            if (controller == null) return false;

            var test = typeof(HttpController).Name;
            var methods = controller.GetMethods().Where(t => t.GetCustomAttributes(true)
                                                 .Any(attr => attr.GetType().Name == $"Http{_httpContext.Request.HttpMethod}"));

            var method = methods.FirstOrDefault(x => _httpContext.Request.HttpMethod switch
                {
                    "GET" => x.GetCustomAttribute<HttpGET>().MethodURI == strParams[0],
                    "POST" => x.GetCustomAttribute<HttpPOST>().MethodURI == strParams[0],
                }) ;
                                                 

            if (method == null) return false;

            object[] queryParams = null;

            switch (strParams[0])
            {
                case "getaccount":
                    object[] t = new object[1] {Convert.ToInt32(strParams[1])};
                    queryParams = t;
                    break;
                case "getbyid":
                    object[] temp = new object[1] { Convert.ToInt32(strParams[1]) };
                    queryParams = temp;
                    break;
                case "saveaccount":
                    //колхоз, как красиво написать?? (чтобы не переименовывать переменную)
                    object[] temp1 = new object[2] { Convert.ToString(strParams[1]), Convert.ToString(strParams[2]) };
                    queryParams = temp1;
                    break;

            }

            var ret = method.Invoke(Activator.CreateInstance(controller), queryParams);

            response.ContentType = "Application/json";

            byte[] buffer = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(ret));
            response.ContentLength64 = buffer.Length;

            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();

            return true;
        }


        private void MainSite(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                byte[] buffer;

                var rawurl = request.RawUrl;

                buffer = Files.GetFile(rawurl.Replace("%20", " "));

                Files.GetExtension(ref response, "." + rawurl);

                if(buffer == null) { Show404(ref response, ref buffer); }

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                //закрываем поток
                output.Close();

                Listening();
            }
            catch
            {
                Console.WriteLine("Smth wrong");
                Stop();
            }

        }



        private void Show404(ref HttpListenerResponse response, ref byte[] buffer)
        {
            response.Headers.Set("Content-Type", "text/html");
            response.StatusCode = 404;
            response.ContentEncoding = Encoding.UTF8;
            string err = "<h1>4504<h1> <h2>The resource can not be found.<h2>";
            buffer = Encoding.UTF8.GetBytes(err);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
