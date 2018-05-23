using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casino.Interfaces;

namespace Casino.TwentyOne
{
    public class TwentyOneGame : Game, IWalkAway
    {
        public TwentyOneDealer Dealer { get; set; }
        public override void Play() //going to encompass 1 hand in twenty one, after the hand it will ask the user where he/she wants to continue
        {
            Dealer = new TwentyOneDealer(); //we name the twentyonedealer "Dealer".
            foreach (Player player in Players) //allows for multiple players, Players is a list property from the Game class
            {
                player.Hand = new List<Card>(); //we want their hand to be blank (reset)
                player.Stay = false;
            }
            Dealer.Hand = new List<Card>(); //we also want the dealers hand to be a new hand, again we want to reset the dealers hand with a new list to ensure nothing is carried over
            Dealer.Stay = false; 
            Dealer.Deck = new Deck();//we want to create a new deck. If we didn't do a new deck then we would get a partial deck every round, so each round will end up having an affect on the next round.
            Dealer.Deck.Shuffle();

            foreach (Player player in Players) // Now we iterate through every player in the list Players for their bet
            {
                bool validAnswer = false;
                int bet = 0;
                while (!validAnswer)
                {
                    Console.WriteLine("Place your bet!");
                    validAnswer = int.TryParse(Console.ReadLine(), out bet);
                    if (!validAnswer) Console.WriteLine("Please enter digits only, no decimals.");
                }
                if (bet < 0)
                {
                    throw new FraudException("Security! Kick this person out.");
                }
                bool successfullyBet = player.Bet(bet); 
                if (successfullyBet == false)
                {
                    return; //what return does in void doesn't return anythin. We are just saying to end this method. It will go back to the while loop in the main program. If the user is actively playing still (true), then it will hit the PLay() method again and ask for a bet.
                }
                Bets[player] = bet; //here we have entered a new entry into the dictionary

            }
            for (int i = 0; i < 2; i++) //This form loop is for the dealing of the cards. We will give everyone a card and then give the dealer a card, and then repeat. For simplicity both cards will be face up.
            {
                Console.WriteLine("Dealing...");
                foreach (Player player in Players)
                {
                    Console.Write("{0}: ", player.Name); //Console.Write() is so that the next thing will not be on a new line (doesn't automatically press enter).
                    Dealer.Deal(player.Hand); //the method Deal from the class Dealer, is taking in the player's Hand as a parameter.
                    if (i == 1) //asking if it is the second round
                    {
                        bool blackJack = TwentyOneRules.CheckForBlackJack(player.Hand); //we create a boolean for CheckForBlackJack method
                        if (blackJack)//if the boolean is true essentially
                        {
                            Console.WriteLine("Blackjack! {0} wins {1}", player.Name, Bets[player]);//return the value from the Dictionary Bets
                            player.Balance = player.Balance + Convert.ToInt32((Bets[player] * 1.5) + Bets[player]); //Bets[player] is a dictionary property from Game class that takes in player as a parameter
                            return;
                        }
                    }
                }
                Console.Write("Dealer: ");
                Dealer.Deal(Dealer.Hand);
                if (i == 1)
                {
                    bool blackJack = TwentyOneRules.CheckForBlackJack(Dealer.Hand);
                    if (blackJack) //checking if dealer has a blackjack hand
                    {
                        Console.WriteLine("Dealer has Blackjack! Everyone loses!");
                        foreach (KeyValuePair<Player, int> entry in Bets) //here we iterate through a dictionary named "Bets" from the class Game, for each dictionary item (player and integer pair), which we will name entry we do the following:
                        {// btw each item in the dictionary "Bet" is given the name entry
                            Dealer.Balance = Dealer.Balance + entry.Value; //here we assign the dealer's balance to everything in the dictionary pair
                        }
                        return; //without this it will ask to hit or stay even if the dealer got blackjack
                    }
                }
            }
            foreach (Player player in Players) //now we begin designing our hit/stay for our players! (we still have to do it for our dealer after)
            {
                while (!player.Stay) // same as player.Stay == false???
                {
                    Console.WriteLine("Your cards are: ");
                    foreach (Card card in player.Hand)
                    {
                        Console.Write("{0} ", card.ToString());
                    }
                    Console.WriteLine("\n\nHit or Stay?");
                    string answer = Console.ReadLine().ToLower(); //take in user output and put it in lower case...
                    if (answer == "stay") //if they choose to stay than player.Stay become true, and the loop is broken
                    {
                        player.Stay = true;
                        break;
                    }
                    else if (answer == "hit") 
                    {
                        Dealer.Deal(player.Hand); //here the dealer implements from method Deal() from the class Dealer, and makes you oh the player's hand as a parameter
                    }
                    bool busted = TwentyOneRules.IsBusted(player.Hand); //passes in player.Hand as a parameter
                    if (busted) //if busted has a value of True
                    {
                        Dealer.Balance = Dealer.Balance + Bets[player]; //increase dealer balance with bet
                        Console.WriteLine("{0} Busted! You lose your bet of {1}. Your balance is {2}.", player.Name, Bets[player], player.Balance);
                        Console.WriteLine("Do you want to play again?");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya")
                        {
                            player.IsActivelyPlaying = true;
                            return;
                        }
                        else
                        {
                            player.IsActivelyPlaying = false; //ends game!
                            return;//return will end the void function
                        }
                    }
                }
            }
            Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand); //now we do everything for the dealers side. Here we check to see whether the Dealer's hand is busted.
            Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand); //ShouldDealerStay is another custome rule/methos we must create that is very specific to Twenty One. Dealer.Stay is a boolean property within the  TwentyOneDealerClass the inherits fromt the dealer class
            while (!Dealer.Stay && !Dealer.isBusted) //this states, while dealer is not staying (false) AND has not busted (fasse)
            {
                Console.WriteLine("Dealer is hitting...");
                Dealer.Deal(Dealer.Hand); //here the the method "Deal" from the class Dealer is being implemented, taking in the Dealer.Hand as a parameter
                Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand); //with the new Dealer.Hand, because of Dealer.Deal, isBusted is a boolean property that checkts to see if the dealer has busted, and will change to the updated boolean
                Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand); //here we check for the method ShouldDealerStay, and assign to the boolean variable/property "Stay"
                //if either isbusted or stay is true, then the while loop breaks
            }
            if (Dealer.Stay) //if dealer decides to stay (true value)
            {
                Console.WriteLine("Dealer is staying.");
            }
            if (Dealer.isBusted) // if Dealers.IsBusted == True
            {
                Console.WriteLine("Dealer busted!");
                foreach (KeyValuePair<Player, int> entry in Bets) //if the dealer busts, all players must get all the winnings, as recorded within the dictionary "Bets"
                {
                    Console.WriteLine("{0} won {1}", entry.Key.Name, entry.Value); //you access the dictionary values for name and bets by making use of "entry.Key"
                    Players.Where(x => x.Name == entry.Key.Name).First().Balance += (entry.Value * 2); //Here we make use of a lambda function where x is each element in a list created by "Where". For any elements who's name matches entry,KeyName, the First() method must grab it from the list. Even though there is only one player. It then adds the corresponding value to the balance
                    Dealer.Balance = Dealer.Balance - entry.Value; //this is what happens to the Dealer's balance when the dealer busts
                }
                return;
            }
            foreach (Player player in Players) //Now we have to build scenerios in which neither the player nor the dealer bust and both have chosen to stay. In this case you have 3 scenerios. Booleans are structs, meaning they are value types and cannot be null. Therefore, you can only have two options with them...(True or false). In this case we make use of a feature in the .Net Framework that allows a boolean to have a null value, and thus have 3 options!

            {
                bool? playerWon = TwentyOneRules.CompareHands(player.Hand, Dealer.Hand);
                if (playerWon == null)
                {
                    Console.WriteLine("Push! No one wins.");
                    player.Balance = player.Balance + Bets[player];
                }
                else if (playerWon == true)
                {
                    Console.WriteLine("{0} won {1}!", player.Name, Bets[player]);
                    player.Balance = player.Balance + (Bets[player] * 2); //they get back their bet and winnings
                    Dealer.Balance = Dealer.Balance - Bets[player];
                }
                else
                {
                    Console.WriteLine("Dealer wins {0}", Bets[player]); //since the dealer win, it gets the player balance added to its balance
                    Dealer.Balance = Dealer.Balance + Bets[player];
                }
                Console.WriteLine("Play again?"); //here the round is over so we check to see if the user wants to stay within the game
                string answer = Console.ReadLine().ToLower();
                if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya")
                {
                    player.IsActivelyPlaying = true;
                }
                else
                {
                    player.IsActivelyPlaying = false;
                }
            }
            
        }

        public override void ListPlayers()
        {
            Console.WriteLine("21 Players");
            base.ListPlayers();
        }

        public void WalkAway(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
