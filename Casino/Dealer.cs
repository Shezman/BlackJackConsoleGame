using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Casino
{
    public class Dealer
    {
        public string Name { get; set; }
        public Deck Deck { get; set; }
        public int Balance { get; set; }

        public void Deal (List<Card> Hand) //we are giving this dealer class the ability to deal. It takes an input parameter, a list of card named "hand"
        {
            Hand.Add(Deck.Cards.First()); //here we grab and add the first card to the hand that is passed into Deal()
            string card = string.Format(Deck.Cards.First().ToString() + "\n");
            Console.WriteLine(card); //print hand to console
            using (StreamWriter file = new StreamWriter(@"C:\Users\sherm\source\repos\TwentyOne\TwentyOne\log.txt", true))
            {
                file.WriteLine(DateTime.Now);
                file.WriteLine(card);
            }//once the closing brace has been hit, that all the resources is disposed of due to the using statement
            Deck.Cards.RemoveAt(0); //remove the hand from the deck
        }

        //people often ask why not have the dealer class inherit from the deck class? The reason, is that inheritance is a "is a relationship"
        //not a "has a relationship". For example, a twentyONeGame IS A game, but a Dealer HAS A Deck but IS NOT a Deck. This has to do with Martin Fowler's theory of composition over inheritance. If you have a choice between choosing to add a class as a property vs 

    }
}
