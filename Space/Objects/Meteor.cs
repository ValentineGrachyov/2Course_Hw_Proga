using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Objects
{
    internal class Meteor
    {
        private PictureBox _body = new PictureBox();
        public Point _location;
        public int HP { get; set; }
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 100;

        public Meteor(Point location)
        {
            _location = location;
            Initializeobject();
        }

        private void Initializeobject()
        {
            _body.Image = Image.FromFile(Path.Join(Directory.GetCurrentDirectory(), @"/Images/meteor.png"));

            _body.Location = _location;
            _body.SizeMode = PictureBoxSizeMode.StretchImage;
            _body.Width = Width;
            _body.Height = Height;
        }

        public void Draw()
        {            
            _body.Location = new Point(new Random().Next(0,1000) , new Random().Next(0, 300));
        }

        public PictureBox GetMeteor()
        {
            return _body;
        }   

    }
}
