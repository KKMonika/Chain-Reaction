using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain_Reaction
{
    [Serializable]
    public class BallsDoc
    {
        public List<SmallBalls> Balls { get; set; }
        public BallsDoc()
        {
            Balls = new List<SmallBalls>();

        }

        public void Draw(Graphics g)
        {
            foreach(SmallBalls ball in Balls)
            {
                ball.Draw(g);
            }
        }

        public void AddBall(Point position)
        {
            SmallBalls b = new SmallBalls();
            b.Center = position;
            Balls.Add(b);
        }

        public void MoveBalls(int left, int top, int width, int height)
        {
            foreach(SmallBalls ball in Balls)
            {
                ball.Move(left, top, width, height);
            }
        }

        //funkcija checkColisions vo glavnata forma
    }
}
