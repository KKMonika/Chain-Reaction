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
        /*public enum LEVELS
        {
            L1,
            L2,
            L3
        }*/
        
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

            nivoa.Add(nivo);
            currentLevel = nivo;
        }

        public bool daliIspolnuva()
        {
            if (currentLevel.currentLevel == BallsDoc.LEVELS.L1 && currentLevel.count >= currentLevel.needToHit())
            {
                changeLevel();
                return true;
            }
            else if (currentLevel.currentLevel == BallsDoc.LEVELS.L2 && currentLevel.count >= currentLevel.needToHit())
            {
                changeLevel();
                return true;
            }
            else if (currentLevel.currentLevel == BallsDoc.LEVELS.L3 && currentLevel.count >= currentLevel.needToHit())
            {
                changeLevel();
                return true;
            }
            else return false;
        }


        




    }
}
