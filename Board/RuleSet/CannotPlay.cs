using inf1035_crazy_eights.Components;

namespace inf1035_crazy_eights.Board.RuleSet
{
    /*
    * Summary: This class define what will happen if a draw call is made
    */
    public class CannotPlay : IRuleSet
    {
        public void Execute(CardGame cardGame, int current)
        {
            DrawCard(cardGame, current);
        }

        /*
         * Summary: this method makes a player draw an additional card
         * param name ="cardGame": A CardGame object
         * param name= "current": the current player turn
         */
        private static void DrawCard(CardGame cardGame, int current)
        {
            Player player = (Player)cardGame.Players[current];
            cardGame.giveCardToPlayer(player);
            Log(player.ToString(), player.CardsLeft());
        }

        //Display some information
        private static void Log(string name, int cardsLeft)
        {
            ConsoleHelper.PrintString($"{name} needs to draw a card since he played nothing", 0);
            ConsoleHelper.PrintString($"Cards before draw: {cardsLeft - 1}", 0);
            ConsoleHelper.PrintString($"Cards After draw: {cardsLeft}", 1000);
        }


        public bool isEnable()
        {
            return true;
        }

        public bool enableBy(Card? card)
        {
            return (card == null);
        }
    }
}
