using inf1035_crazy_eights.Components;

namespace inf1035_crazy_eights.Board.RuleSet
{
    /*
    * Summary: This class define what will happen if a skip behavior happens
    */
    public class SkipTurn : IRuleSet
    {
        public void Execute(CardGame cardGame, int current)
        {
            int nextPlayer = cardGame.NextPlayer(current);
            cardGame.boardState = CardGame.BoardState.SKIP_TURN;
            Log(cardGame, nextPlayer);
        }

        //Display what happen
        private static void Log(CardGame cardGame, int current)
        {
            ConsoleHelper.PrintString(
                $"{cardGame.Players[current] as Player} will skip his turn since a skip card has been played");
        }

        public bool isEnable()
        {
            return true;
        }

        public bool enableBy(Card? card)
        {
            return (card != null && ((Card)card).CompareValue(Card.Cards.QUEEN));
        }
    }
}
