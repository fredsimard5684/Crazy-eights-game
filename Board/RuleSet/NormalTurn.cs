using inf1035_crazy_eights.Components;

namespace inf1035_crazy_eights.Board.RuleSet
{
    /*
    * Summary: This class define what will happen if no rule are apply
    */
    public class NormalTurn : IRuleSet
    {
        public void Execute(CardGame cardGame, int current)
        {
            return;
        }

        public bool isEnable() {
            return true;
        }

        public bool enableBy(Card? card) {
            return true;
        }
    }
}
