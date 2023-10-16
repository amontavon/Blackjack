using static Blackjack.biz.Constants;

namespace Blackjack.biz.Cards
{
    public class BlackJackScorer
    {
        //Method determines the score. Most of this is for handling aces
        public static int GetScore(List<Card> cards, bool ignoreHiddenCards)
        {
            var possibleScoreTotals = new List<int>() { 0 }; //inital possible scores, starting with a possible score of 0

            for(int index = 0; index < cards.Count; index++)
            {
                var card = cards[index];

                if (ignoreHiddenCards || (!ignoreHiddenCards && !card.IsHidden)) //if we are calcuating the dealers face up score, and the current card is face down, ignore it
                {
                    //Logic for aces
                    if (card.Value == CardValue.Ace)
                    {
                        var updatedScores = new List<int>();
                        //Loop through each value and deplicate it. Working backwards to not run into duplicates added
                        while (possibleScoreTotals.Count > 0)
                        {
                            var score = possibleScoreTotals.Last();
                            possibleScoreTotals.Remove(score);

                            //Add both possible new scores
                            updatedScores.Add(score + 11);
                            updatedScores.Add(score + 1);
                        }

                        possibleScoreTotals = updatedScores;
                    }
                    else //Logic for all other cards
                    {
                        var updatedScores = new List<int>();
                        foreach (var score in possibleScoreTotals)
                        {
                            updatedScores.Add(score + card.GetCardPointValue());
                        }

                        possibleScoreTotals = updatedScores;
                    }
                }
            }

            if(possibleScoreTotals.Count == 1) //if there's only one possible score, return that
            {
                return possibleScoreTotals.Single();
            }

            var result = possibleScoreTotals.FindAll(x => x <= 21); //grab any valid scores
            if (result.Count != 0){ //if there's a good value
                return result.Max();
            }

            return possibleScoreTotals.Min(); //return the lowest value over 21
        }
    }
}
