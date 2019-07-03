using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain_Reaction
{
    [Serializable]
    public class SmallBall
    {
        //treba da se pojavuvaat na random lokacii
        public int radius; //test vrednost 
        //radiusot ne treba da im se menuva
        public Point Center { get; set; }
        public Color Color { get; set; } //predlog da se naprajt sekoe so razlicna boja
        public int State { get; set; } //za koja boja ke bide topceto, ke se dodeluva so random vrednost

        public double Angle { get; set; } //za vo koja nasoka ke se dvizi topceto
        public double Velocity {get; set;} //so koja brzina ke se dvizi

        private float velocityX;
        private float velocityY;
        
        public bool bigBall { get; set; } //bool promenliva za da znaeme dali topceto moze da predizvika kolizija so drugo malo topce za da se dobijat poeni

        public bool isHit { get; set; } //ako se dopiraat so golemata topka

        public SmallBall()
        {
            isHit = false;
            radius = 10; //pocetna vrednost za sekoe topce pri
            bigBall = false;
            Velocity = 10;
            Random r = new Random();
            Angle = r.NextDouble() * 2 * Math.PI;
            velocityX = (float)(Math.Cos(Angle) * Velocity);
            velocityY = (float)(Math.Sin(Angle) * Velocity);
            State = r.Next(3);
        }

        public void Draw(Graphics g)
        {
            if (State == 0)
            {
                Color = Color.Green;
            }
            else if (State == 1)
            {
                Color = Color.Blue;
            }
            else if (State == 2)
            {
                Color = Color.Red;
            }
            else
            {
                Color = Color.Black;
            }
            Brush b = new SolidBrush(Color);
            g.FillEllipse(b, Center.X - radius, Center.Y - radius, radius * 2, radius * 2);
            b.Dispose();
        }

        public void DrawFirst(Graphics g)
        {
            Brush b = new SolidBrush(Color.Black);
            g.FillEllipse(b, Center.X - radius, Center.Y - radius, radius * 2, radius * 2);
            b.Dispose();
        }

        /*public bool isColliding(SmallBall ball)
        {
            double distance = (Center.X - ball.Center.X) * (Center.X - ball.Center.X) + (Center.Y - ball.Center.Y) * (Center.Y - ball.Center.Y);
            bool smallBigImpact = this.bigBall && !ball.bigBall; //mora tekovnoto topce da bide golemo, a drugoto malo za da se dobijat poeni
            return distance <= (radius + ball.radius) * (radius + ball.radius) && smallBigImpact;
        }
        */

            public bool isColliding(BigBall ball)
        {
            double distance = (Center.X - ball.Center.X) * (Center.X - ball.Center.X) + (Center.Y - ball.Center.Y) * (Center.Y - ball.Center.Y);
            return distance <= (radius + ball.RADIUS) *(radius + ball.RADIUS);
        }


        public void Move(int left, int top, int width, int height)
        {
            if(!bigBall)
            {
                float nextX = Center.X + velocityX;
                float nextY = Center.Y + velocityY;
                if (nextX - radius <= left || nextX + radius >= width + left)
                {
                    velocityX = -velocityX;
                }
                if(nextY - radius <= top || nextY + radius >= height + top)
                {
                    velocityY = -velocityY;
                }
                Center = new Point((int)(Center.X + velocityX), (int)(Center.Y + velocityY));
            }

            
        }
    }
}
