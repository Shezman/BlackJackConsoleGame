using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.TwentyOne
{
    public class TwentyOneRules
    {
        private static Dictionary<Face, int> _cardValues = new Dictionary<Face, int>() //naming convention of a private class is to make use of an underscore
        {
            [Face.Two] = 2, //here we are creating a dictionary of all the cards and their values. This way we can quickly look up what the value of the card is. 
            [Face.Three] = 3,
            [Face.Four] = 4,
            [Face.Five] = 5,
            [Face.Six] = 6,
            [Face.Seven] = 7,
            [Face.Eight] = 8,
            [Face.Nine] = 9,
            [Face.Ten] = 10,
            [Face.Jack] = 10,
            [Face.Queen] =10,
            [Face.King] = 10,
            [Face.Ace] = 1 //for simplicity we will give it a value of 1 and create logic to add 10 later

        };
        private static int[] GetAllPossibleHandValues(List<Card> Hand) //will return an integer array of all possible values
        {
            int aceCount = Hand.Count(x => x.Face == Face.Ace); //each item on hand we are going to check if the card's face is equivalent to an ace. It will then return a count
            int[] result = new int[aceCount + 1]; //here we create an array named result. Within an array, you have to define how big it will be. In this case we define it as the count of aces + another card
            int value = Hand.Sum(x => _cardValues[x.Face]); //it looks at each item in the hand, and checks it fron the _cardValues dictionary get the face value and sums it up
            result[0] = value;
            if (result.Length == 1) //if there are no aces, then there can only be one possible value
            {
                return result;
            }
            //if the following for loop gets hit, it means result.length > 1
            for (int i = 1; i < result.Length; i++) //we now have to iterate through result.length in order to implement different values of ace.
            {
                value = value + (i * 10);
                result[i] = value;
            }
            return result;
        }
        public static bool CheckForBlackJack(List<Card> Hand)//takes in Hand list as paramter
        {
            int[] possibleValues = GetAllPossibleHandValues(Hand); //here we create an integer array (named possibleValues) of possible values by using the GetAllPossibleValues method, and passing the hand that we want to get values for.
            int value = possibleValues.Max();//assign the highest value in array of possible values to the int container value
            if (value == 21) return true;
            else return false; //here we do the if statement that actually checks to see if value is equal to 21 or not.
        }
        public static bool IsBusted(List<Card> Hand) //takes in Hand list as parameter
        {
            int value = GetAllPossibleHandValues(Hand).Min(); //get the lowest value in your hand, we only care about the minimum value, not the max
            if (value > 21) return true; //if the value of the card is abouve 21, it returns a true value (bust)
            else return false; //otherwise it returns a false value
        }
        public static bool ShouldDealerStay(List<Card> Hand) //method for dealer to choose to stay, once again parameter is a List od cards called "Hand"
        {
            int[] possibleHandValues = GetAllPossibleHandValues(Hand);
            foreach (int value in possibleHandValues) //regarding each item in the list "possibleHandValues
            {
                if (value > 16 && value < 22)
                {
                    return true;
                }
      
            }
            return false; //meaning the dealer should not stay (once again no else statement) Note that it is outside the foreach loop
        }

        public static bool? CompareHands(List<Card> PlayerHand, List<Card> DealerHand) //Now we have to build scenerios in which neither the player nor the dealer bust and both have chosen to stay. In this case you have 3 scenerios. Booleans are structs, meaning they are value types and cannot be null. Therefore, you can only have to options with them...(True or false). In this case we make use of a feature in the .Net Framework that allows a boolean to have a null value, and thus have 3 options!
        {
            int[] playerResults = GetAllPossibleHandValues(PlayerHand);
            int[] dealerResults = GetAllPossibleHandValues(DealerHand); //we place the possible values of 

            int playerScore = playerResults.Where(x => x < 22).Max(); //here we are saying for all player results, get me results that are less than 22, and find the max
            int dealerScore = dealerResults.Where(x => x < 22).Max();

            if (playerScore > dealerScore)
            {
                return true;
            }
            else if (playerScore < dealerScore)
            {
                return false;
            }
            else
            {
                return null;
            }
        }
    }
}
