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
        public List<SmallBall> balls { get; set; }
        public bool hasClicked { get; set; }  //se menuva vo true samo pri klik na pocetokot vo formata, posle toa pri sekoj sleden klik nema da se kreira novo topce
        public BallsDoc()
        {
            hasClicked = false;
            balls = new List<SmallBall>();
        }

        public void Draw(Graphics g)
        {
            foreach(SmallBall ball in balls)
            {
                ball.Draw(g);
            }
        }

        public void AddBall(Point position)
        {
            SmallBall b = new SmallBall();
            b.Center = position;
            balls.Add(b);
        }

        public void MoveBalls(int left, int top, int width, int height)
        {
            foreach(SmallBall ball in balls)
            {
                ball.Move(left, top, width, height);
            }
        }

        //funkcija checkColisions vo glavnata forma
        public void checkCollisions()
        {
            for(int i=0; i<balls.Count; i++)
            {
                for(int j= i + 1; j<balls.Count; j++)
                {
                    if (i != j && balls[i].isColliding(balls[j]))
                    {
                        balls[j].bigBall = true;
                        //increaseRadius then decreaseRadius

                    }
                }
            }
        }
    }
}
