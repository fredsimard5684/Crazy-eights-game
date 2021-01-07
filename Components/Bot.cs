using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using inf1035_crazy_eights.Board.RuleSet;

namespace inf1035_crazy_eights.Components
{
    /*
     * Summary: a class Bot that inherits from the player class. A bot in this case is a Player with an automatic analyse attribute
     */
    public class Bot : Player
    {
        //Constructor
        public Bot(string firstName, string lastName, int id) : base(firstName, lastName, id)
        {
        }

        /*
         * Summary: this method allows to get the available cards in the player's hand
         * param name="currentCard": the card on the top of the deck
         * return: the list of availables cards
         */
        private List<Card> AvailableCards(Card currentCard)
        {
            return ShowHand.Cast<Card>()
                .Where(c => c.CompareType(currentCard) || c.CompareValue(currentCard) || RuleHandler.IsWildCard(c)).ToList();
        }

        /*
        * Summary: this method allows to check if the available cards contains any special card
        * param name="list": the list of available cards
        * param name="card": a reference of an object Card that will get change regardless of the return state
        * return: true if the card has been changed to an other card object(not null)
        */
        private bool HasSpecialCard(List<Card> list, ref Card? card)
        {
            var playableCards = new List<Card>();
            list.ForEach(c =>
            {
                if (RuleHandler.IsSpecialCard(c)) playableCards.Add(c);
            });
            if (playableCards.Count == 0) return false;
            card = ChooseBestSpecialCard(playableCards);
            return card != null;
        }

        /*
        * Summary: this method allows to choose what the best special card is in the list of special card
        * param name="list": the list of special cards
        * return: the best card to play in this situation
        */
        private Card ChooseBestSpecialCard(List<Card> list)
        {
            int[] countTypeInHand = CountTypeInHand();
            Card? bestCard = null;
            int highestValue = 0;
            list.ForEach(c =>
            {
                int currentValue = countTypeInHand[c.GetCardType];
                if (currentValue > highestValue)
                {
                    highestValue = currentValue;
                    bestCard = c;
                }
            });
            return ((Card) bestCard);
        }

        /*
        * Summary: this method allows to check if the available cards contains any card with the same values as the card on top of the deck
        * param name="list": the list of available cards
        * param name="currentCard": the card on top of the deck
        * param name="card": a reference of an object Card that will get change regardless of the return state
        * return: true if the card has been changed to an other card object(not null)
        */
        private bool HasSameValue(List<Card> list, Card currentCard, ref Card? card)
        {
            var playableCards = new List<Card>();
            list.ForEach(c =>
            {
                if (c.CompareValue(currentCard))
                    if (!RuleHandler.IsSpecialCard(c))
                        playableCards.Add(c);
            });
            if (playableCards.Count == 0) return false;
            if (IsCurrentTypeBest(currentCard.GetCardType)) return false;
            card = ChooseBestValueCard(playableCards);
            return card != null;
        }

        /*
        * Summary: this method allows to check if the available cards contains any wildcards
        * param name="list": the list of available cards
        * param name="currentCard": the card on top of the deck
        * param name="card": a reference of an object Card that will get change regardless of the return state
        * return: true if the card has been changed to an other card object(not null)
        */
        private bool HasWildCard(List<Card> list, Card currentCard, ref Card? card)
        {
            var playableCards = new List<Card>();
            list.ForEach(c =>
            {
                if (RuleHandler.IsWildCard(c)) playableCards.Add(c);
            });
            if (playableCards.Count == 0) return false;
            if (IsCurrentTypeBest(currentCard.GetCardType)) return false;
            card = ChooseBestValueCard(playableCards);
            return card != null;
        }

        /*
        * Summary: this method allows to choose what the best value card is in the list of value card
        * param name="list": the list of value card
        * return: the best card to play in this situation
        */
        private Card? ChooseBestValueCard(IEnumerable<Card> list)
        {
            Card? card = null;
            foreach (Card c in list)
            {
                if (IsCurrentTypeBest(c.GetCardType))
                {
                    card = c;
                    break;
                }
            }

            return card;
        }

        /*
        * Summary: this method allows to check if a card has the most number of card of the same type in the player hand
        * param name="type": the type of the card to evaluate
        * return: the best card to play in this situation
        */
        private bool IsCurrentTypeBest(int type)
        {
            int[] countTypeInHand = CountTypeInHand();
            int value = countTypeInHand[type];
            return countTypeInHand.All(count => count <= value);
        }

        /*
        * Summary: this method allows to check if the available cards contains any card of the same type
        * param name="list": the list of available cards
        * param name="currentCard": the card on top of the deck
        * param name="card": a reference of an object Card that will get change regardless of the return state
        * return: true if the card has been changed to an other card object(not null)
        */
        private static bool HasSameType(List<Card> list, Card currentCard, ref Card? card)
        {
            var playableCards = new List<Card>();
            list.ForEach(c =>
            {
                if (c.CompareType(currentCard))
                    if (!RuleHandler.IsSpecialCard(c))
                        playableCards.Add(c);
            });
            if (playableCards.Count == 0) return false;
            card = SelectRandomCard(playableCards);
            return card != null;
        }

        /*
        * Summary: this method allows to count thew number of card of each type in the player's hand
        * return: the list that contains each number of cards for each type
        */
        private int[] CountTypeInHand()
        {
            int[] typeCount = new int[4];
            foreach (Card card in ShowHand)
            {
                ++typeCount[card.GetCardType];
            }

            return typeCount;
        }

        /*
        * Summary: this method allows to pick any card excepts the one with very special effects
        * return: the card to play
        */
        private static Card IgnoreSpecialCard(IEnumerable list)
        {
            List<Card> newList = list.Cast<Card>().Where(c => !RuleHandler.IsSpecialCard(c))
                .ToList();
            Card card = SelectRandomCard(newList);
            return card;
        }

        /*
        * Summary: this method allows to select a random card from a given list
        * param name="list": a given list
        * return: the card to play randomly choose
        */
        private static Card SelectRandomCard(IList list)
        {
            Random rng = new Random();
            int pos = rng.Next(0, list.Count);
            Card card = (Card) list[pos];
            return card;
        }

        /*
        * Summary: this method allows to select chek if the next player has only one card left in his hand
        * param name="cardsLeft": number of cardsLeft in the next player's hand
        * return: true if the count is 1.
        */
        private bool NextPlayerHasOneCardLeft(int playerId)
        {
            return this.playersMemory[playerId] == 1;
        }

        /*
        * Summary: this method allows to play a card smartly. Make a decision that is better than just play a random card
        * param name="currentCard": the card on top of the deck that the player(bot) will check to play a card
        * param name="cardsLeft": the cards left in the next player hand. The player(bot) will take a look at the card count of the next player to make a choice based on that
        * return: the card to play
        */
        public override Card? Play(Card? currentCard, int nextPlayerId)
        {
            Card? card = null;
            if (currentCard == null)
            {
                card = IgnoreSpecialCard(ShowHand);
                return card;
            }

            List<Card> list = AvailableCards((Card) currentCard);
            switch (list.Count)
            {
                case 0:
                    return card;
                case 1:
                    card = list[0];
                    return card;
            }

            if (NextPlayerHasOneCardLeft(nextPlayerId))
                if (HasSpecialCard(list, ref card))
                    return card;
            if (HasSameValue(list, (Card) currentCard, ref card)) return card;
            if (HasWildCard(list, (Card) currentCard, ref card)) return card;
            if (HasSameType(list, (Card) currentCard, ref card)) return card;

            //If nothing was choose, meaning that nothing was optimal -> Select randomly
            card = SelectRandomCard(list);
            return card;
        }
    }
}
