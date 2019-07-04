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

        public bool isActive() //ako nema aktivni/zgolemeni topcinja, da prestane move
        {
            foreach(SmallBall ball in balls)
            {
                if (ball.isHit)
                    return true;
            }
            //ako foreach ne vratil true, znaci nema aktivni mali topcinja
            return bigBall != null; //edinstvena proverka ostanata e dali bigBall e aktivna
        }

        // ja prepraviv CheckCollisons
        public void checkCollisions()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i+1; j < balls.Count; j++)
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

            for (int i = balls.Count - 1; i >= 0; i--)
            {
                if (balls[i].isHit && !balls[i].decreaseFlag) //ako e pogodeno topceto, zgolemuvaj mu go radiusot
                {
                    balls[i].increaseRadius();
                }

                if (balls[i].radius == SmallBall.MAX_RADIUS) //ova znaci deka vekje stanalo golemo topceto
                {
                    balls[i].radiusCounter++;
                    balls[i].decreaseFlag = true;
                }

                if (balls[i].radiusCounter > 30) //ako vekje podolgo vreme topceto e golemo, vreme e da pocne da se namulva
                {
                    balls[i].decreaseRadius();
                }

                if (balls[i].radius <= 0) //ako radiusot mu e 0 ili pomal, znaci deka treba da se otstrani od listata, inaku i so radius 1/0 moze da ima collision so drugo topce
                {
                    balls.RemoveAt(i);
                }

            }


            //Istata logika kako pogore, samo primeneta nad bigBall
            if (bigBall != null)
            {
                if (!bigBall.decreaseFlag)
                {
                    bigBall.increaseRadius();
                }

                if (bigBall.RADIUS == BigBall.MAX_RADIUS)
                {
                    bigBall.radiusCounter++;
                    bigBall.decreaseFlag = true;
                }

                if (bigBall.radiusCounter > 30)
                {
                    bigBall.decreaseRadius();
                }

                if (bigBall.RADIUS <= 0)
                {
                    bigBall = null;
                }
            }
            
        }
        public void nextLevel()
        {
            if(count == 10)
            {
                //bigBall.changeRadius();
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
