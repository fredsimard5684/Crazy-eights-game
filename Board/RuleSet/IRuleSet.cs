using inf1035_crazy_eights.Components;

namespace inf1035_crazy_eights.Board.RuleSet
{
    /*
    * Summary: an interface that contains a base for the rule set
    */
    public interface IRuleSet
    {
        /*
        * Summary: this method will be implemented by a bunch of class so that every behavior can change the board state differently.
        *          It will apply some rules and change the board according to that
        * param name="cardGame": A card game object
        * param name="turn": the current board index turn
        * return: the result of the board state.
        */
        void Execute(CardGame cardGame, int turn);
        bool isEnable();
        bool enableBy(Card? card);
    }
}
