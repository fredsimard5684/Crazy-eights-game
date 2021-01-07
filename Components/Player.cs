using System.Collections;
using System;
using inf1035_crazy_eights.Board;
using System.Collections.Generic;


namespace inf1035_crazy_eights.Components
{
    /*
    * Summary: this class stores the player information. It is a model for a player
    */
    public abstract class Player
    {
        //Member variables
        public readonly int id;
        private readonly string _firstName;
        private readonly string _lastName;

        protected Dictionary<int, int> playersMemory;

        //Default constructor
        protected Player(string firstName, string lastName, int id)
        {
            _firstName = firstName;
            _lastName = lastName;
            this.id = id;

            this.playersMemory = new Dictionary<int, int>();
        }

        private void UpdateMemory(int playerId, int cardLeft)
        {
            Console.WriteLine($"\nPlayer {id} UPDATE MEMORY: Player {playerId} has {cardLeft} cards in hand.\n");
            if (!this.playersMemory.ContainsKey(playerId)) {
                this.playersMemory.Add(playerId, cardLeft);
                return;
            }

            this.playersMemory[playerId] = cardLeft;
        }

        //Getter: get the player's hand
        public ArrayList ShowHand { get; } = new ArrayList();


        /*
        * Summary: add a card at the top of the deck.
        * param name="card": take a card object that has a type and a value
        */
        public void AddCard(Card card)
        {
            ShowHand.Add(card);
        }

        /*
        * Summary: Check if there are cards left in the player's hand
        * return: true if the card's list is not empty
        */
        public bool HasCardLeft()
        {
            return ShowHand.Count > 0;
        }

        /*
        * Summary: Count the cards left in the player's hand
        * return: the number of cards in the player's hand
        */
        public int CardsLeft()
        {
            return ShowHand.Count;
        }

        /*
        * Summary: Remove a card from the player's hand
        * param name="card": the card object to remove from his hand
        */
        public void RemoveCard(Card card)
        {
            ShowHand.Remove(card);
        }

        public void RegistredToGame(CardGame cg) {
            cg.PlayersEvent += PlayerEventHappened;
        }

        private void PlayerEventHappened(object sender, PlayerEventArgs e)
        {
            if (e.playerId == this.id) return;

            switch (e.eventType)
            {
                case PlayerEventTypes.PLAYER_TOOK_CARD:
                    UpdateMemory(e.playerId, e.playerCardLeft);
                    break;
                case PlayerEventTypes.PLAYER_PLAY_CARD:
                    UpdateMemory(e.playerId, e.playerCardLeft);
                    break;
            }
        }

        /*
        * Summary: Overrides the default ToString method to display the firstname and lastname of the player
        * return: firstname and lastname
        */
        public override string ToString()
        {
            return $"{_firstName} {_lastName}";
        }

        public abstract Card? Play(Card? c, int cardLeft);
    }
}
