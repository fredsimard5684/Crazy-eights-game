namespace inf1035_crazy_eights.Components
{
    /*
    * Summary: this struct is defining what is a card and what value and type it has
    */
    public struct Card
    {
        //Members variables

        //Defining the card
        public enum Types {
            HEART,
            SPADE,
            CLUB,
            DIAMOND,
        }
        public enum Cards {
            ACE,
            TWO,
            THREE,
            FOUR,
            FIVE,
            SIX,
            SEVEN,
            EIGHT,
            NINE,
            TEN,
            JACK,
            QUEEN,
            KING
        }

        // Constructor: define the card type and the card value when initilize
        public Card(int type, int val)
        {
            this.GetCardValue = val;
            this.GetCardType = type;
        }

        //Getters
        public int GetCardType { get; }

        public int GetCardValue { get; }

        public bool CompareType(Card card)
        {
            return GetCardType == card.GetCardType;
        }

        public bool CompareValue(Card card)
        {
            return GetCardValue == card.GetCardValue;
        }

        public bool CompareValue(Cards type)
        {
            return GetCardValue == (int) type;
        }

        /*
        * Summary: Override the default ToString to display the cards information
        * return: Formated string
        */
        public override string ToString()
        {
            return $"{(Cards) GetCardValue} of {(Types) GetCardType}";
        }
    }
}
