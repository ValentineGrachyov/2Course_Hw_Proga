using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Objects
{
    internal class Shield
    {
        private PictureBox _body = new PictureBox();
        private Form _form = new Form();

        public int Width { get; set; } = 100;
        public int Height { get; set; } = 100;

        public Shield( Form form)
        {
            _form = form;
            Initializeobject();
        }

        private void Initializeobject()
        {
            _body.Image = Image.FromFile(Path.Join(Directory.GetCurrentDirectory(), @"/Images/shield.png"));
            
            _body.SizeMode = PictureBoxSizeMode.StretchImage;
            _body.Width = Width;
            _body.Height = Height; 
        }

        public void Draw(Ship ship, bool isActive)
        {
            if (!isActive)
            {
                this._body.Visible = true;
                _body.Location = new Point(ship.Shield_Ship.Location.X - 20, ship.Shield_Ship.Location.Y - 25);
                _form.Controls.Add(this.GetShield());
            }            
        }

        public void SetVisible_false()
        {
            _body.Visible = false;
        }

        public PictureBox GetShield()
        {
            return _body;
        }
    }
}
