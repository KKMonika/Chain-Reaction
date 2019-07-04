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

        public void changeLevel()
        {
            BallsDoc nivo = new BallsDoc();
            if (nivoa.ElementAt(nivoa.Count - 1).currentLevel == BallsDoc.LEVELS.L1)
                nivo.currentLevel = BallsDoc.LEVELS.L2;
            else if (nivoa.ElementAt(nivoa.Count - 1).currentLevel == BallsDoc.LEVELS.L2)
                nivo.currentLevel = BallsDoc.LEVELS.L3;

            nivoa.Add(nivo);
        }


        




    }
}
