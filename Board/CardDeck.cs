using System;
using System.Collections;
using inf1035_crazy_eights.Components;

namespace inf1035_crazy_eights.Board
{
    /*
    * Summary: this class allows to add the cards in a define deck.
    */
    public class CardDeck
    {
        //Members variables
        private const int NUMBER_OF_TYPE = 4;
        private const int NUMBER_OF_CARD = 13;
        private readonly ArrayList _cards = new ArrayList();

        //Getter: return the deck
        public ArrayList GetCards()
        {
            return this._cards;
        }

        /*
        * Summary: add a card at the top of the deck.
        * param name="card": take a card object that has a type and a value
        */
        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        /*
        * Summary: Count the cards in the deck.
        * return: the number of cards
        */
        public int CardsCount()
        {
            return _cards.Count;
        }

        /*
        * Summary: this constructor create the deck by setting directly the type and the value to the card object
        * param name="empty": this parameters allows to automaticly predifine the deck or not.
        */
        public CardDeck(bool empty)
        {
            if (empty) return;

            foreach (Card.Types type in Enum.GetValues(typeof(Card.Types)))
            {
                foreach (Card.Types val in Enum.GetValues(typeof(Card.Cards)))
                {
                    this.AddCard(new Card((int) type, (int)val));
                }
            }

            // for (int type = 0; type < Enum.GetNames(typeof(Card.Types)).Length; type++)
            // {
            //     for (int val = 0; val < Enum.GetNames(typeof(Card.Cards)).Length; val++)
            //     {
            //         this.AddCard(new Card(type, val));
            //     }
            // }
        }

        /*
        * Summary: this method allows to merge a deck with another deck
        * param name="newDeck": takes an existing CardDeck object.
        */
        public void Merge(CardDeck newDeck)
        {
            _cards.AddRange(newDeck.GetCards());
            int posOfLastCard = newDeck.GetCards().Count - 1;
            newDeck.GetCards().RemoveRange(0, posOfLastCard - 1);
        }

        /*
        * Summary: check if the deck is empty
        * return: true if the deck is not empty
        */
        public bool CanPick()
        {
            return this._cards.Count > 0;
        }

        public Card? Peek()
        {
            return _cards.Count > 0 ? (Card?) _cards[_cards.Count-1] : null;
        }

        /*
        * Summary: Pick a random card from the deck and remove it afterward
        * return: the card that has been randomly picked
        */
        public Card Pick()
        {
            Random r = new Random();

            int pos = r.Next(0, this._cards.Count);
            Card c = (Card) _cards[pos];
            _cards.RemoveAt(pos);
            return c;
        }

        public Card? Pop()
        {
            if (_cards.Count == 0) return null;

            int pos = _cards.Count - 1;
            Card c = (Card)_cards[pos];
            _cards.RemoveAt(pos);
            return c;
        }
    }
}
