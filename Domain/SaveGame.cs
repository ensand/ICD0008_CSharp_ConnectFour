using System;

namespace Domain
{
    public class SaveGame
    {
        public int SaveGameId { get; set; }
        public string SaveGameName { get; set; }
        public string Board { get; set; } // will convert CellState[][] to string when saving, and do vice versa when loading
        public bool PlayerOneMove { get; set; }
        // public DateTime SaveCreationDateTime { get; set; }

        public override string ToString()
        {
            return $"{SaveGameName} ({SaveGameId}): {Board}";
        }
    }
}