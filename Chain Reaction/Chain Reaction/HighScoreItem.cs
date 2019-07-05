using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain_Reaction
{
    [Serializable]
    public class HighScoreItem : IComparable<HighScoreItem>
    {
        
        public string player { get; set; }
      
        public int points { get; set; }

        public int CompareTo(HighScoreItem other)
        {
            return this.points.CompareTo(other.points);
        }
        public bool Equals(HighScoreItem other)
        {
            return this.player.Equals(other.player) && this.points.Equals(other.points);
        }
    }
}
