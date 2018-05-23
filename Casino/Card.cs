using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public struct Card
    {
        
        public Suit Suit { get; set; }
        public Face Face { get; set; }

        public override string ToString() //within the class Card, we wanted to change the function of the ToString() method
        {
            return string.Format("{0} of {1}", Face, Suit); //now when we say card.ToString(); it pring the cards out in Face of Suit format (Ace of Hearts)
        }
        
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    public enum Face
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }


}
