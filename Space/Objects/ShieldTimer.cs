using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Objects
{
    public class ShieldTimer
    {
        private PictureBox _body = new PictureBox();
        private Point _lbPoint = new Point(12, 500);

        public Label Shield_Cd_Label = new Label();
        
        public Point _location = new Point(12,550);
        public int Width { get; set; } = 70;
        public int Height { get; set; } = 70;

        public ShieldTimer()
        {
            Initializeobject();
        }
        private void Initializeobject()
        {
            _body.Image = Image.FromFile(Path.Join(Directory.GetCurrentDirectory(), @"/Images/shield.png"));
            
            _body.Location = _location;
            _body.SizeMode = PictureBoxSizeMode.StretchImage;
            _body.Width = Width;
            _body.Height = Height;

            Shield_Cd_Label.Location = _lbPoint;
            Shield_Cd_Label.Text = "Ready";
        }

        public void Set_shield_Visible_false()
        {
            _body.Visible = false;
        }
        
        public void Start_Duration(ref int dur, ref int cd)
        {            
            if(dur == 0)
            {
                _body.Visible = true;                
                Start_Couldaun(ref cd);
                dur = 10;

            }
            else
            {
                Shield_Cd_Label.Text = dur.ToString();
                dur--;
            }
        }

        private void Start_Couldaun(ref int cd)
        {
            if (cd == 0)
            {
                Shield_Cd_Label.Text = "Ready";
                cd = 15;
            }
            else
            {
                Shield_Cd_Label.Text = "Перезарядка" + cd.ToString();
                cd--;
            }
        }

        public PictureBox GetShield()
        {  
            return _body;
        }
    }
}
