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
        public BigBall bigBall { get; set; }

        int count=0;
        bool levelChange = false; // dali da se smeni levelot
        int currentLevel = 1;
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

        // ja prepraviv CheckCollisons
        public void checkCollisions()
        {
            for(int i = 0; i < balls.Count; i++)
            {
                for(int j = i+1; j < balls.Count; j++)
                { 
                    if (!balls[i].isHit && balls[i].isCollidingBig(bigBall))
                    {
                        count++;
                        balls[i].isHit = true;
                    }

                    else if (balls[i].isCollidingSmall(balls[j]) && balls[i].isHit && !balls[j].isHit)
                    {
                        balls[j].isHit = true;
                    }

                    else if (balls[j].isCollidingSmall(balls[i]) && balls[j].isHit && !balls[i].isHit)
                    {
                        balls[i].isHit = true;
                    }
                }
            }
            for(int i = balls.Count - 1; i >= 0; i--)
            {
                if (balls[i].isHit)
                    balls[i].increaseRadius();
            }
        }
        public void nextLevel()
        {
            if(count == 10)
            {
                bigBall.changeRadius();
            }
        }
        public int poeni()
        {
            return count * 1000;
        }
        public int CurrentLevel()
        {
            if(poeni() == 10000)
            {
                currentLevel = 2;
            }
            if(poeni() == 500000)
            {
                currentLevel = 3;
            }
            return currentLevel;

        }

       
    }
}
