using System;
using System.Collections;
using inf1035_crazy_eights.Board.RuleSet;
using inf1035_crazy_eights.Components;
using inf1035_crazy_eights.Helper;

namespace inf1035_crazy_eights.Board
{
    /*
    * This class defines the game loop and how the game is going to be play
    */
    public class CardGame
    {
        //Member variables
        private readonly CardDeck _discardDeck;
        private readonly RuleHandler _ruleHandle;
        public event EventHandler<PlayerEventArgs> PlayersEvent;

        //Getters and Setters
        public ArrayList Players { get; }
        public CardDeck DrawDeck { get; }
        public bool IsClockwise { get; set; }

        public enum BoardState
        {
            DEFAULT,
            SKIP_TURN,
        }

        public BoardState boardState { get; set; }


        //Constructor
        public CardGame()
        {
            DrawDeck = new CardDeck(false);
            _discardDeck = new CardDeck(true);
            Players = new ArrayList();
            boardState = BoardState.DEFAULT;
            _ruleHandle = new RuleHandler(this);
            IsClockwise = true;
        }

        public void RegisterPlayer(Player player)
        {
            Players.Add(player);
            player.RegistredToGame(this);
        }

        public int NextPlayer(int current)
        {
            return IsClockwise
                ? MathHelper.Modulo(++current, Players.Count)
                : MathHelper.Modulo(--current, Players.Count);
        }

        /*
        * Summary: Distributes the card equally to each player in the game
        * param name="numberOfCard": this parameters takes in the number of cards that each player need to start with in their hands
        */
        public void DistributeCards(int numberOfCard)
        {
            for (int i = 0; i < numberOfCard; i++)
            {
                foreach (Player player in Players)
                {
                    giveCardToPlayer(player);
                }
            }
        }


        public void giveCardToPlayer(Player player)
        {
            this.ChekForReshuffle();
            player.AddCard(DrawDeck.Pick());

            EmitPlayerEvent(GenerateEvent(
                player,
                PlayerEventTypes.PLAYER_TOOK_CARD
            ));
        }

        /*
        * Summary: this method checks if a deck need to be reshuffle
        */
        private void ChekForReshuffle()
        {
            if (DrawDeck.CanPick()) return;
            ConsoleHelper.PrintString("The deck is empty, the deck is being reshuffle to 'reset' the game deck", 0);
            Card? last = _discardDeck.Pop();
            DrawDeck.Merge(_discardDeck);
            if (last != null) _discardDeck.AddCard((Card)last);

        }

        //Use an event
        private void EmitPlayerEvent(PlayerEventArgs e)
        {
            PlayersEvent?.Invoke(this, e);
        }

        /*
        * Summary: this method is the game loop. It will change the player's turn according to the board state and the rules.
        * This is where most of the program will be. It will quit this method when a player has won the game.
        */
        public void Play()
        {
            Random random = new Random();
            bool finish = false;

            int playerTurn = random.Next(Players.Count);

            while (!finish)
            {
                Player currentPlayer = (Player)Players[playerTurn];

                if (boardState == BoardState.SKIP_TURN)
                {
                    boardState = BoardState.DEFAULT;
                    playerTurn = NextPlayer(playerTurn);
                    continue;
                }

                EmitPlayerEvent(GenerateEvent(
                    currentPlayer,
                    PlayerEventTypes.START_OF_TURN
                ));

                ConsoleHelper.PrintList(currentPlayer.ShowHand);
                Card? playedCard = currentPlayer.Play(_discardDeck.Peek(),
                    (((Player)Players[NextPlayer(playerTurn)]).id));

                if (playedCard != null)
                {
                    currentPlayer.RemoveCard((Card)playedCard);
                    _discardDeck.AddCard((Card)playedCard);
                    ConsoleHelper.PrintString($"\nPlayed Card: {playedCard}", 5000);
                    ConsoleHelper.PrintString("Cards left: " + currentPlayer.CardsLeft(), 1000);

                    EmitPlayerEvent(GenerateEvent(
                    currentPlayer,
                        PlayerEventTypes.PLAYER_PLAY_CARD
                    ));

                    if (!currentPlayer.HasCardLeft())
                    {
                        ConsoleHelper.PrintString($"Played no more card left:: {currentPlayer.CardsLeft()}", 1000);

                        var finisher = currentPlayer;
                        finish = true;
                        EmitPlayerEvent(GenerateEvent(
                            finisher,
                            PlayerEventTypes.WINNER
                        ));
                        break;
                    }
                }
                _ruleHandle.Handle(playedCard, playerTurn);

                ConsoleHelper.PrintString($"Cards left in the deck: {DrawDeck.CardsCount()}", 1000);
                EmitPlayerEvent(GenerateEvent(
                    PlayerEventTypes.END_OF_TURN
                ));

                playerTurn = NextPlayer(playerTurn);
            }
        }

        /*
        * Summary: generate the player event by creating a new PlayerArg event
        * param name="type": the type of event
        * return: a list of arguments in the PlayerEventArgs class
        */
        private PlayerEventArgs GenerateEvent(PlayerEventTypes type)
        {
            PlayerEventArgs args = new PlayerEventArgs { eventType = type };
            return args;
        }

        /*
        * Summary: generate the player event by creating a new PlayerArg event
        * param name="player": an object Player
        * param name="id": the player id
        * param name="type": the type of event
        * return: a list of arguments in the PlayerEventArgs class
        */
        private PlayerEventArgs GenerateEvent(Player player, PlayerEventTypes type)
        {
            PlayerEventArgs args = new PlayerEventArgs
            {
                playerName = player.ToString(),
                playerId = player.id,
                playerCardLeft = player.CardsLeft(),
                at = DateTime.Now,
                eventType = type
            };
            return args;
        }
    }
}
