using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace User1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p = new Pen(new SolidBrush(Color.Red), 15);

        System.Timers.Timer tim = new System.Timers.Timer();
        object locker = new();

        Socket tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
        }

        private async void ConnectionButton_Click(object sender, EventArgs e)
        {
            this.Click += new EventHandler(Form1_Paint);
            button1.Enabled = false;
            await tcpClient.ConnectAsync("127.0.0.1", 80);
            tim.Elapsed += Timer;
            tim.Interval = 100;
            tim.Start();
        }

        private void Form1_Paint(object sender, EventArgs e)
        {
            MouseEventArgs mouseArgs = e as MouseEventArgs;
            Rectangle r = new Rectangle(mouseArgs.Location.X, mouseArgs.Location.Y, 10, 10);
            g.DrawEllipse(p, r);
            string cord = $"{mouseArgs.Location.X} {mouseArgs.Location.Y} \n";
            byte[] buffer = Encoding.UTF8.GetBytes(cord);
            tcpClient.Send(buffer);      
        }

        private void ServerDraw(string cord)
        {
            var X = Convert.ToInt32(cord.Split(' ')[0]);
            var Y = Convert.ToInt32(cord.Split(' ')[1]);
            Point point = new Point(X, Y);
            Rectangle r = new Rectangle(point.X,point.Y, 10,10);
            g.DrawEllipse(p, r);
        }

        public void GetServerInfo()
        {
            lock (locker)
            {
                var response = new List<byte>();
                byte[] bytesRead = new byte[1];
                while (true)
                {
                    var count = tcpClient.Receive(bytesRead);
                    // смотрим, если считанный байт представл€ет конечный символ, выходим
                    if (count == 0 || bytesRead[0] == '\n') break;
                    // иначе добавл€ем в буфер
                    response.Add(bytesRead[0]);
                }
                ServerDraw(Encoding.UTF8.GetString(response.ToArray()));
            }
        }
        public void Timer(Object source, ElapsedEventArgs e)
        {
            GetServerInfo();
        }

    }
}



