using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using TCP_Socket;


 Socket tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
 System.Timers.Timer timer = new System.Timers.Timer(10000);

try
{
    tcpListener.Bind(new IPEndPoint(IPAddress.Any, 80));
    tcpListener.Listen(4);    // запускаем сервер    
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");

    Console.WriteLine("Количество подключенных игроков");
    //string str = Console.ReadLine();
    string s = "2";

    // получаем подключение и сохраняем его в лист сокетов  

    List<Socket> sockets = new List<Socket>();

    switch (s)
    {
        case "1":
            sockets.Add(await tcpListener.AcceptAsync());
            break;
        case "2":
            sockets.Add((await tcpListener.AcceptAsync()));
            sockets.Add((await tcpListener.AcceptAsync()));
            break;
        case "3":
            sockets.Add((await tcpListener.AcceptAsync()));
            sockets.Add((await tcpListener.AcceptAsync()));
            sockets.Add((await tcpListener.AcceptAsync()));
            break;
        case "4":
            sockets.Add((await tcpListener.AcceptAsync()));
            sockets.Add((await tcpListener.AcceptAsync()));
            sockets.Add((await tcpListener.AcceptAsync()));
            sockets.Add((await tcpListener.AcceptAsync()));
            break;
    }

    var bl = new ServerRequest(sockets);
    

    foreach (var el in sockets) { Console.WriteLine(el.RemoteEndPoint.ToString()); }
    while(true)
    {
            await Task.Run(() => bl.ServerWork(0));
            await Task.Run(() => bl.ServerWork(1));
    }
    
    
       
        
    
        
                
                
            
    
     
    
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
    
   

    

