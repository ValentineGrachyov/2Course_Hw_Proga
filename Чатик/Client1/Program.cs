using System.Net.Sockets;
using System.Text;

// слова для отправки для получения перевода
var words = new string[] { "red", "yellow", "blue" };

using var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    await tcpClient.ConnectAsync("127.0.0.1", 8888);
    // буфер для входящих данных
    var response = new List<byte>();
    foreach (var word in words)
    {
        // считыванием строку в массив байт
        // при отправке добавляем маркер завершения сообщения - \n
        byte[] data = Encoding.UTF8.GetBytes(Console.ReadLine() + '\n');
        // отправляем данные
         tcpClient.Send(data);

        // буфер для считывания одного байта
        var bytesRead = new byte[1];
        // считываем данные до конечного символа
        while (true)
        {
            var count = tcpClient.Receive(bytesRead);
            // смотрим, если считанный байт представляет конечный символ, выходим
            if (count == 0 || bytesRead[0] == '\n') break;
            // иначе добавляем в буфер
            response.Add(bytesRead[0]);
        }
        var translation = Encoding.UTF8.GetString(response.ToArray());
        Console.WriteLine($"Слово {word}: {translation}");
        response.Clear();
    }

    // отправляем маркер завершения подключения - END
     tcpClient.Send(Encoding.UTF8.GetBytes("END\n"));
    Console.WriteLine("Все сообщения отправлены");
    Console.Read();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}












//// данные для отправки
//string name = "iPhone 14";
//string company = "Apple";
//int count = 2;
//decimal price = 145890.34m;

//using TcpClient tcpClient = new TcpClient();
//await tcpClient.ConnectAsync("127.0.0.1", 8888);
//// получаем NetworkStream для взаимодействия с сервером
//var stream = tcpClient.GetStream();
//// создаем BinaryReader для чтения данных
//using var binaryReader = new BinaryReader(stream);
//// создаем BinaryWriter для отправки данных
//using var binaryWriter = new BinaryWriter(stream);

//// отправляем данные товара
//binaryWriter.Write(name);
//binaryWriter.Write(company);
//binaryWriter.Write(count);
//binaryWriter.Write(price);
//binaryWriter.Flush();

//// считываем сгенерированный на сервере id нового товара
//var id = binaryReader.ReadString();
//Console.WriteLine($"Id нового товара: {id}");
//Console.ReadLine();


//// Клиент на сокете
//using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//try
//{
//    await socket.ConnectAsync("127.0.0.1", 8888);
//    Console.WriteLine("Подключение к "+ socket.RemoteEndPoint);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.ToString());
//}


// Для TCP-клиент 
//string host = "127.0.0.1";
//int port = 8888;
//using TcpClient client = new TcpClient();
//Console.Write("Введите свое имя: ");
//string? userName = Console.ReadLine();
//Console.WriteLine($"Добро пожаловать, {userName}");
//StreamReader? Reader = null;
//StreamWriter? Writer = null;

//try
//{
//    client.Connect(host, port); //подключение клиента
//    Reader = new StreamReader(client.GetStream());
//    Writer = new StreamWriter(client.GetStream());
//    if (Writer is null || Reader is null) return;
//    // запускаем новый поток для получения данных
//    Task.Run(() => ReceiveMessageAsync(Reader));
//    // запускаем ввод сообщений
//    await SendMessageAsync(Writer);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//Writer?.Close();
//Reader?.Close();

//// отправка сообщений
//async Task SendMessageAsync(StreamWriter writer)
//{
//    // сначала отправляем имя
//    await writer.WriteLineAsync(userName);
//    await writer.FlushAsync();
//    Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");

//    while (true)
//    {
//        string? message = Console.ReadLine();
//        await writer.WriteLineAsync(message);
//        await writer.FlushAsync();
//    }
//}
//// получение сообщений
//async Task ReceiveMessageAsync(StreamReader reader)
//{
//    while (true)
//    {
//        try
//        {
//            // считываем ответ в виде строки
//            string? message = await reader.ReadLineAsync();
//            // если пустой ответ, ничего не выводим на консоль
//            if (string.IsNullOrEmpty(message)) continue;
//            Print(message);//вывод сообщения
//        }
//        catch
//        {
//            break;
//        }
//    }
//}
//// чтобы полученное сообщение не накладывалось на ввод нового сообщения
//void Print(string message)
//{
//    if (OperatingSystem.IsWindows())    // если ОС Windows
//    {
//        var position = Console.GetCursorPosition(); // получаем текущую позицию курсора
//        int left = position.Left;   // смещение в символах относительно левого края
//        int top = position.Top;     // смещение в строках относительно верха
//        // копируем ранее введенные символы в строке на следующую строку
//        Console.MoveBufferArea(0, top, left, 1, 0, top + 1);
//        // устанавливаем курсор в начало текущей строки
//        Console.SetCursorPosition(0, top);
//        // в текущей строке выводит полученное сообщение
//        Console.WriteLine(message);
//        // переносим курсор на следующую строку
//        // и пользователь продолжает ввод уже на следующей строке
//        Console.SetCursorPosition(left, top + 1);
//    }
//    else Console.WriteLine(message);
//}