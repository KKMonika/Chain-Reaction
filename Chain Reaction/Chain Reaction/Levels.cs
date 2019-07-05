using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain_Reaction
{
    [Serializable]
    public class Levels
    {        
        public List<BallsDoc> nivoa;
        public BallsDoc currentLevel;

        public Levels()
        {
            nivoa = new List<BallsDoc>();
        }

        public void addBallsDoc()
        {
            BallsDoc nivo = new BallsDoc();
            nivo.currentLevel = BallsDoc.LEVELS.L1;
            nivoa.Add(nivo);
            currentLevel = nivo;

        }
        public void changeLevel()
        {
            BallsDoc nivo = new BallsDoc();
            if (nivoa.ElementAt(nivoa.Count - 1).currentLevel == BallsDoc.LEVELS.L1)
                nivo.currentLevel = BallsDoc.LEVELS.L2;
            else if (nivoa.ElementAt(nivoa.Count - 1).currentLevel == BallsDoc.LEVELS.L2)
                nivo.currentLevel = BallsDoc.LEVELS.L3;
            else if (nivoa.ElementAt(nivoa.Count - 1).currentLevel == BallsDoc.LEVELS.L3)
                nivo.currentLevel = BallsDoc.LEVELS.L4;
            else if (nivoa.ElementAt(nivoa.Count - 1).currentLevel == BallsDoc.LEVELS.L4)
                nivo.currentLevel = BallsDoc.LEVELS.L5;
            else if (nivoa.ElementAt(nivoa.Count - 1).currentLevel == BallsDoc.LEVELS.L5)
                nivo.currentLevel = BallsDoc.LEVELS.L6;
            else if (nivoa.ElementAt(nivoa.Count - 1).currentLevel == BallsDoc.LEVELS.L6)
                nivo.currentLevel = BallsDoc.LEVELS.L7;

            nivoa.Add(nivo);
            currentLevel = nivo;
        }

        public bool daliIspolnuva()
        {
            if (currentLevel.count >= currentLevel.needToHit())
            {
                changeLevel();
                return true;
            }
            else
                return false;
        }

        public int poeniOdSiteNivoa()
        {
            int sum = 0;
            foreach(BallsDoc nivo in nivoa)
            {
                sum += nivo.poeniOdTekovnoNivo;
            }
            return sum;
        }

        public int getLevel()
        {
            switch(currentLevel.currentLevel)
            {
                case BallsDoc.LEVELS.L1:
                    return 1;
                case BallsDoc.LEVELS.L2:
                    return 2;
                case BallsDoc.LEVELS.L3:
                    return 3;
                case BallsDoc.LEVELS.L4:
                    return 4;
                case BallsDoc.LEVELS.L5:
                    return 5;
                case BallsDoc.LEVELS.L6:
                    return 6;
                case BallsDoc.LEVELS.L7:
                    return 7;
                default:
                    return 0;
            }
        }




    }
}
