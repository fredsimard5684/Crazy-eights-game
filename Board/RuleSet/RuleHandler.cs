using inf1035_crazy_eights.Components;
using System.Collections;
// using System.Collections.Generic;

namespace inf1035_crazy_eights.Board.RuleSet
{
    /*
     * This class implement a strategy pattern. It will allows to easily change the state of the board
     */
    public class RuleHandler
    {
        //Members variable
        // private IRuleSet _ruleSet;
        private readonly CardGame _cardGame;
        private ArrayList _rules;

        private static IRuleSet drawRule = new DrawPenality();
        private static IRuleSet reverseRule = new ReverseCard();
        private static IRuleSet skipRule = new SkipTurn();

        //Constructor
        public RuleHandler(CardGame cardGame)
        {
            _cardGame = cardGame;
            _rules = new ArrayList();
            _rules.Add(new CannotPlay());
            _rules.Add(skipRule);
            _rules.Add(drawRule);
            _rules.Add(reverseRule);

            // last
            _rules.Add(new NormalTurn());
        }

        /*
         * Summary: this method allows to set the correct rule set class
         * param name="card": the card on the deck
         */
        public void Handle(Card? card, int turn)
        {
            foreach (IRuleSet rule in _rules)
            {
                if (rule.isEnable() && rule.enableBy(card)) {
                    rule.Execute(_cardGame, turn);
                    return;
                }
            }
        }

        public static bool IsReverseCard(Card card)
        {
            return isRule(reverseRule, card);
        }

        public static bool IsDrawCard(Card card)
        {
            return isRule(drawRule, card);
        }

        public static bool IsWildCard(Card card)
        {
            return card.GetCardValue == (int) Card.Cards.JACK;
        }

        public static bool IsSkipCard(Card card)
        {
            return isRule(skipRule, card);
        }

        public static bool IsSpecialCard(Card card) {
            return IsReverseCard(card) || IsDrawCard(card) || IsSkipCard(card);
        }

        private static bool isRule(IRuleSet rule, Card card) {
            return rule.isEnable() && rule.enableBy(card);
        }
    }
}
