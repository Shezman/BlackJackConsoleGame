using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public abstract class Game //generic naming conventions are best as they ensure you can resue is this class for perhaps another game
    {
        private List<Player> _players = new List<Player>();
        private Dictionary<Player, int> _bets = new Dictionary<Player, int>();

        public List<Player> Players { get { return _players; } set { _players = value; } } //list of player items in the list Players
        public string Name { get; set; } //the name of the game
        public Dictionary<Player, int> Bets { get { return _bets; } set { _bets = value; } } // we are calling this property bets, which is a dictionary of players and bets

        public abstract void Play();

        public virtual void ListPlayers() //this method should print a list of players that are playing the particular game
        {
            foreach (Player player in Players)
            {
                Console.WriteLine(player.Name);
            }
        }
    }

    
}
