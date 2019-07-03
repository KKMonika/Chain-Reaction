using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain_Reaction
{
    [Serializable]
   public class BigBall
    {
        public int initialRadius;
        public int RADIUS;
        public Point Center { get; set; }
        public Color Color { get; set; }

        public bool isSet  { get; set; } // dali sme postavile topka

        public BigBall(Point center, Color color)
        {
            Center = center;
            Color = color;
            initialRadius = 30; //test moze i da e promeni, a ponataka moze da i dodademe i ramka okolu
            RADIUS = 30;
            isSet = false;
        }

        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color);
            g.FillEllipse(b, Center.X - RADIUS, Center.Y - RADIUS, RADIUS * 2, RADIUS * 2);
            b.Dispose();
        }
        public bool Touches(SmallBall ball)
        {
            return (ball.Center.X - Center.X) * (ball.Center.X - Center.X) + (ball.Center.Y - Center.Y) * (ball.Center.Y - Center.Y) <= RADIUS * RADIUS;

        }
        public void changeRadius()
        {
            RADIUS += 5;
        }
        
    }
}
