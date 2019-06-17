using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain_Reaction
{
    [Serializable]
    public class SmallBalls
    {
        //treba da se pojavuvaat na random lokacii
        public static readonly int RADIUS = 10; //test vrednost 
        //radiusot ne treba da im se menuva
        public Point Center { get; set; }
        public Color Color { get; set; } //predlog da se naprajt sekoe so razlicna boja
        public int State { get; set; }
        public double Angle { get; set; }
        public double Velocity {get; set;}
        public float velocityX;
        public float velocityY;

        public bool isColided { get; set; } //ako se dopiraat so golemata topka

        public SmallBalls()
        {
           
            isColided = false;
            Velocity = 10;
            Random r = new Random();
            Angle = r.NextDouble() * 2 * Math.PI;
            velocityX = (float)(Math.Cos(Angle) * Velocity);
            velocityY = (float)(Math.Sin(Angle) * Velocity);
            State = r.Next(3);

        }

        public void Draw(Graphics g)
        {
            Brush b = null;
            if (State == 0)
            {
                b = new SolidBrush(Color.Green);
            }
            else if (State == 1)
            {
                b = new SolidBrush(Color.Blue);
            }
            else
            {
                b = new SolidBrush(Color.Red);
            }
            g.FillEllipse(b, Center.X - RADIUS, Center.Y - RADIUS, RADIUS * 2, RADIUS * 2);
            b.Dispose();
        }

        public bool isColiding(BigBall ball)
        {
            double d = (Center.X - ball.Center.X) * (Center.X - ball.Center.X) + (Center.Y - ball.Center.Y) * (Center.Y - ball.Center.Y);
            return d <= (2 * RADIUS) * (2 * RADIUS);
        }

        public void Move(int left, int top, int width, int height)
        {
            float nextX = Center.X + velocityX;
            float nextY = Center.Y + velocityY;
            if (nextX - RADIUS <= left || nextX + RADIUS >= width + left)
            {
                velocityX = -velocityX;
            }
            if(nextY - RADIUS <= top || nextY + RADIUS >= height + top)
            {
                velocityY = -velocityY;
            }
            Center = new Point((int)(Center.X + velocityX), (int)(Center.Y + velocityY));
        }
    }
}
