using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public class Player
    {
        public Player (string name, int beginningBalance) //this is a constructor that goes on top of the class
        {
            Hand = new List<Card>();
            Balance = beginningBalance;
            Name = name;
        }

        private List<Card> _hand = new List<Card>();
        public List<Card> Hand { get { return _hand; } set { _hand = value; } }
        public int Balance { get; set; }
        public string Name { get; set; }
        public bool IsActivelyPlaying { get; set; }
        public bool Stay { get; set; }  //might be better to create a twentyonegame player that inherits player and have a specific property "Stay".

        public bool Bet(int amount) //here we place a bet method within the player class, because we want to avoid having unrelated methods/logic in dfferent classes. The player is doing the logic so we should keep that logic to that entity
        {
            if (Balance - amount < 0)
            {
                Console.WriteLine("You do not have enough to place a bet that size.");
                return false; //means the bet didnt work.
            }
            else
            {
                Balance -= amount; //Same as (Balance = Balance - amount)
                return true;
            }
        }
        public static Game operator +(Game game, Player player)
        {
            game.Players.Add(player);
            return game;
        }

        public static Game operator -(Game game, Player player)
        {
            game.Players.Remove(player);
            return game;
        }
        //since we are adding this operator overload to the class Game, this means it can be used in other games such as Solitaire, poker etc...This is an example of Polymorphism
    }
}
