using System;

namespace inf1035_crazy_eights.Board
{
    public enum PlayerEventTypes
    {
        START_OF_TURN,
        PLAYER_TOOK_CARD,
        PLAYER_PLAY_CARD,
        WINNER,
        END_OF_TURN
    }

    public class PlayerEventArgs : EventArgs
    {
        public int playerCardLeft { get; set; }
        public string playerName { get; set; }
        public PlayerEventTypes eventType { get; set; }
        public int playerId { get; set; }
        public DateTime at { get; set; }
    }
}
