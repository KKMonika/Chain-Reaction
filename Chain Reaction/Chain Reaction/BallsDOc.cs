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
        public enum LEVELS
        {
            L1,
            L2,
            L3,
            L4,
            L5,
            L6,
            L7
        }
        public List<SmallBall> balls { get; set; }
        public BigBall bigBall { get; set; }
        public int count { get; set; } //kolku topcinja se pogodeni
       // public int needToHit { get; set; } // kolku topcinja treba da se pogodat za da bide pominato nivoto
        public LEVELS currentLevel;
        public bool hasClicked { get; set; }  //se menuva vo true samo pri klik na pocetokot vo formata, posle toa pri sekoj sleden klik nema da se kreira novo topce
        public int poeniOdTekovnoNivo;
        private Font font;
        public BallsDoc()
        {
            hasClicked = false;
            balls = new List<SmallBall>();
            count = 0;
            poeniOdTekovnoNivo = 0;
            font = new Font("Ariel", 10);
        }

        public void Draw(Graphics g)
        {
            foreach(SmallBall ball in balls)
            {
                ball.Draw(g, font);
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
                        balls[i].Points = 100;
                        poeniOdTekovnoNivo += balls[i].Points;
                    }

                    if (!balls[j].isHit && balls[j].isCollidingBig(bigBall))
                    {
                        count++;
                        balls[j].isHit = true;
                        balls[j].Points = 100;
                        poeniOdTekovnoNivo += balls[j].Points;
                    }

                    else if (balls[i].isCollidingSmall(balls[j]) && balls[i].isHit && !balls[j].isHit)
                    {
                        count++;
                        balls[j].isHit = true;
                        balls[j].Points = balls[i].Points * 2;
                        poeniOdTekovnoNivo += balls[j].Points;
                    }

                    else if (balls[j].isCollidingSmall(balls[i]) && balls[j].isHit && !balls[i].isHit)
                    {
                        count++;
                        balls[i].isHit = true;
                        balls[i].Points = balls[j].Points * 2;
                        poeniOdTekovnoNivo += balls[i].Points;
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

        /*public int poeniOdTekovnoNivo()
        {
            int sum = 0;
            foreach(SmallBall s in balls)
            {
                if (s.isHit)
                    sum += s.Points;
            }
            return sum;
        }*/

        public int needToHit()
        {
            switch (currentLevel)
            {
                case LEVELS.L1:
                    return 3;
                case LEVELS.L2:
                    return 5;
                case LEVELS.L3:
                    return 10;
                case LEVELS.L4:
                    return 13;
                case LEVELS.L5:
                    return 18;
                case LEVELS.L6:
                    return 27;
                case LEVELS.L7:
                    return 32;
                default:
                    return 0;
            }

        }

        public int maxBalls()
        {
            switch (currentLevel)
            {
                case LEVELS.L1:
                    return 10;
                case LEVELS.L2:
                    return 15;
                case LEVELS.L3:
                    return 20;
                case LEVELS.L4:
                    return 25;
                case LEVELS.L5:
                    return 30;
                case LEVELS.L6:
                    return 35;
                case LEVELS.L7:
                    return 40;
                default:
                    return 0;
            }
        }
      


        

       
    }
}
