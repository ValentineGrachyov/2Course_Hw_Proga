using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Objects
{
    internal class Bullet
    {
        public int Damage { get; set; }
        public int Speed { get; set; } = 6;
        public int Width { get; set; } = 50;
        public int Height { get; set; } = 50;
        public PictureBox Body { get; set; } = new PictureBox();
        public Point StartLocationPoint { get; set; }

        public Bullet(Point startLocation)
        {
            StartLocationPoint = startLocation;
            InicializeObject();
        }

        private void InicializeObject()
        {
            Body.Image = Image.FromFile(Path.Join(Directory.GetCurrentDirectory(), @"/Images/lazer.png"));

            Body.Location = StartLocationPoint;
            Body.SizeMode = PictureBoxSizeMode.StretchImage;
            Body.Width = Width;
            Body.Height = Height;
        }
        public Point Draw()
        {                            
            Body.Location = new Point(Body.Location.X, Body.Location.Y - Speed); 
            return Body.Location;
        }
        public PictureBox GetObject()
        {
            return Body;
        }

    }
}
