namespace Domain
{
    public class GameState
    {
        public int GameStateId { get; set; }
        public int? SaveGameId { get; set; } // is null if new game
        public string Board { get; set; }
        public bool PlayerOneMove { get; set; }
    }
}