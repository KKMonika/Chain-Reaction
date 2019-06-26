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
        public List<SmallBall> smallBalls { get; set; }
        public List<BigBall> bigBalls { get; set; }
        public BallsDoc()
        {
            smallBalls = new List<SmallBall>();
            bigBalls = new List<BigBall>();
        }

        public void Draw(Graphics g)
        {
            foreach(SmallBall ball in smallBalls)
            {
                ball.Draw(g);
            }
        }

        public void AddBall(Point position)
        {
            SmallBall b = new SmallBall();
            b.Center = position;
            smallBalls.Add(b);
        }

        public void MoveBalls(int left, int top, int width, int height)
        {
            foreach(SmallBall ball in smallBalls)
            {
                ball.Move(left, top, width, height);
            }
        }

        //funkcija checkColisions vo glavnata forma
        /*void checkCollisions()
        {
            for(int i=0; i<smallBalls.Count; i++)
            {
                for(int j= i + 1; j<smallBalls.Count; j++)
                {
                    if (i != j && smallBalls[i].isColliding(smallBalls[j]))
                        {

                    }
                }
            }
        }*/
    }
}
