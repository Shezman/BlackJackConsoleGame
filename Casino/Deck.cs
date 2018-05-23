using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Deck
    {   
        public Deck() //a constructor is a way to assign values immediately after instantiation //this is a property
        {
            Cards = new List<Card>(); //Instantiates the property "Cards" as an empty list of Cards. Refers to the property of that object that we have created
            
            for (int i = 0; i < 13; i++ ) //because there are 13 faces
            {
                for (int j = 0; j < 4; j++)  //because we want to fllop through each suit for each face. There are 4 suits
                {
                    Card card = new Card();
                    card.Face = (Face)i; //here we convert i into a face data type (this could be two, three, four, ace, jack etc..)
                    card.Suit = (Suit)j;//here we convert j into a suit data type(this could be clubs, spades, diamonds,hearts).
                    Cards.Add(card);
                }
            }
        

        }
        public List<Card> Cards { get; set; } //this is a property

        public void Shuffle(int times = 1) //we have an out variable here known as timesShuffled //this is a behavior
        {
            for (int i = 0; i < times; i++)
            {
                List<Card> TempList = new List<Card>(); //A temporary list to store your shuffled items
                Random random = new Random(); //this attains some form of randomity (although some argue that true randomity is impossible)

                while (Cards.Count > 0) //no longer have to preface card with deck
                {
                    int randomIndex = random.Next(0, Cards.Count); //this may return the same number, but it doesn't matter as it has been removed
                    TempList.Add(Cards[randomIndex]);
                    Cards.RemoveAt(randomIndex);
                }

                Cards = TempList; //"this" means it is refering to its own oject or to itself
            }

           
        }
    }
}
