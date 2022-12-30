using Space.Objects;

namespace Space
{
    public partial class Form1 : Form
    {
        Ship ship;
        Shield shield;
        ShieldTimer sht = new ShieldTimer();
        List<Meteor> meteors = new List<Meteor>();

        int shieldduration = 10;
        int shieldCd = 15;
        bool IsShieldReady = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            btn_start.Visible = false;
            btn_start.Enabled = false;
            ship = new Ship(new Point(450,500),this);
            shield = new Shield(this);            
            Controls.Add(ship.Draw());
            Controls.Add(sht.GetShield());
            Controls.Add(sht.Shield_Cd_Label);
            for(int i = 0; i < 10; i++)
            {
                var meteor = new Meteor(new Point(new Random().Next(0, 900), new Random().Next(0, 300)));
                meteors.Add(meteor);
                Controls.Add(meteor.GetMeteor());

                meteor.Draw();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Down):
                    ship.MoveDown();
                    shield.Draw(ship,IsShieldReady);
                    break;
                
                case Keys.Up:
                    ship.MoveUp();
                    shield.Draw(ship, IsShieldReady);
                    break;
                
                case Keys.Right:                    
                    ship.MoveRight();
                    shield.Draw(ship, IsShieldReady);
                    break;
                
                case Keys.Left:
                    ship.MoveLeft();
                    shield.Draw(ship, IsShieldReady);
                    break;

                case Keys.Space:
                    ship.Fire();
                    break;

                case Keys.ShiftKey:
                    if(IsShieldReady)
                    {
                        sht.Set_shield_Visible_false();
                        shield.Draw(ship, !IsShieldReady);
                        sht.Shield_Cd_Label.Text = shieldduration.ToString();
                        ShieldTimer.Enabled = true;
                        IsShieldReady = false;
                    }
                    else
                    {
                        break;
                    }
                                      
                    break;

                case Keys.Escape:
                    this.Close();
                    break;                

                default:
                    MessageBox.Show(e.KeyCode.ToString());
                    break;               
            }
        }

        private void ShieldTimer_Tick(object sender, EventArgs e)
        {
            if (sht.Shield_Cd_Label.Text == "Ready")
            {
                shield.SetVisible_false();
                ShieldTimer.Enabled = false;
                IsShieldReady = true;
            }
            else if(!IsShieldReady)  
            { 
                sht.Start_Duration(ref shieldduration,ref shieldCd);
            }
            
        }
    }
}