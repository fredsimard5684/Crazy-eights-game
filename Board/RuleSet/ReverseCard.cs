using inf1035_crazy_eights.Components;

namespace inf1035_crazy_eights.Board.RuleSet
{
    /*
    * Summary: This class define what will happen if a reverse call has been made
    */
    public class ReverseCard : IRuleSet
    {
        public void Execute(CardGame cardGame, int current)
        {
            Log();
            cardGame.IsClockwise = !cardGame.IsClockwise;
        }

        //Display what happen
        private static void Log()
        {
            ConsoleHelper.PrintString("A reverse card has been played, the board will be reverse...", 1000);
        }

        public bool isEnable()
        {
            return true;
        }

        public bool enableBy(Card? card)
        {
            return (card != null && ((Card)card).CompareValue(Card.Cards.ACE));
        }
    }
}
