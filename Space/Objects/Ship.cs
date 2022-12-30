using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Space.Objects
{
    internal class Ship
    {
        private PictureBox _body = new PictureBox();
        private Form _container;        
        private Point _startLocationPoint;
        private List<Bullet> _bullets = new List<Bullet>();
        private bool _isMoving = true;

        public int HP { get; set; }
        public int Speed { get; set; } = 30;
        public int Width { get; set; } = 50; //150
        public int Height { get; set; } = 50;
        public PictureBox Shield_Ship { get { return _body; } set { } }
        

        public Ship(Point startLocation, Form containerForm)
        {
            _container = containerForm;
            _startLocationPoint = startLocation;
            InicializeObject();
        }

        private void InicializeObject()
        {
            _body.Image = Image.FromFile(Path.Join(Directory.GetCurrentDirectory(), @"/Images/rocket.png"));
            _body.Location = _startLocationPoint;
            _body.SizeMode = PictureBoxSizeMode.StretchImage;
            _body.Width = Width;
            _body.Height = Height;
        }
        public PictureBox Draw()
        {
            return _body;
        }

        public  void MoveUp()
        {
            if (!(_body.Location.Y <= 0))
            {
                _body.Location = new Point(_body.Location.X , _body.Location.Y - Speed);
            }
            else _body.Location = new Point(_body.Location.X, _body.Location.Y);
        }
        public  void MoveDown()
        {
            if (!(_body.Location.Y == 590))
            {
                _body.Location = new Point(_body.Location.X, _body.Location.Y + Speed);
            }
            else _body.Location = new Point(_body.Location.X, _body.Location.Y);
        }
        public  void MoveLeft()
        {
            if (!(_body.Location.X == 0))
            {
                _body.Location = new Point(_body.Location.X - Speed, _body.Location.Y);
            }
            else _body.Location = new Point(_body.Location.X, _body.Location.Y);
        }
        public void MoveRight()
        {
            if (!(_body.Location.X == 1200))
            {
                _body.Location = new Point(_body.Location.X + Speed, _body.Location.Y);
            }
            else _body.Location = new Point(_body.Location.X, _body.Location.Y);
        }
        public async void Fire()
        {
            var bullet = new Bullet(_body.Location);
            Point bPos = new Point();

            while(bullet.Body.Location.Y > -100 )
            {
                await Task.Run(() =>
                {
                    bPos = BulletFall(bullet);
                    Thread.Sleep(2);
                });
                bullet.Body.Location = bPos;
                _container.Controls.Add(bullet.GetObject());                
            }
            _bullets.Add(bullet);
            _container.Controls.Remove(bullet.GetObject());

        }

        private Point BulletFall(Bullet bullet)
        {            
             return new Point(bullet.Body.Location.X, bullet.Body.Location.Y - bullet.Speed);
        }

    }
}
