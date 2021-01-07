using inf1035_crazy_eights.Components;

namespace inf1035_crazy_eights.Board.RuleSet
{
    /*
    * Summary: This class define what will happen if a drawPenality call is made
    */
    public class DrawPenality : IRuleSet
    {
        public void Execute(CardGame cardGame, int current)
        {
            DrawCardPenality(cardGame, current);
            cardGame.boardState = CardGame.BoardState.SKIP_TURN;
        }

        /*
        * Summary: this method makes a player draw 2 additional cards
        * param name ="cardGame": A CardGame object
        * param name= "current": the current player turn
        */
        private static void DrawCardPenality(CardGame cardGame, int current)
        {
            Player player = ((Player) cardGame.Players[cardGame.NextPlayer(current)]);
            Log(player);
            for (int i = 0; i < 2; i++)
            {
                cardGame.giveCardToPlayer(player);
                ConsoleHelper.PrintString("---------------------------");
            }
        }

        private static void Log(Player player)
        {
            ConsoleHelper.PrintString(
                $"{player.ToString()} needs to draw some cards since the last player played a penality card(add +2 card). He will also skip his turn",
                1000);
            ConsoleHelper.PrintString($"Cards before draw: {player.CardsLeft()}", 1000);
            ConsoleHelper.PrintString($"Cards After draw: {player.CardsLeft() + 2}", 1000);
        }

        public bool isEnable()
        {
            return true;
        }

        public bool enableBy(Card? card)
        {
        return (card != null && ((Card)card).CompareValue(Card.Cards.TWO));
        }
    }
}
