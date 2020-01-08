using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace GameEngine
{
    public class Game
    {
        /// <summary>
        /// Game logic and database object
        /// </summary>
        
        public Guid GameId { get; set; }
        public string BoardString { get; set; }
        public string? SaveName { get; set; }
        public string SaveCreationDateTime { get; set; }
        [NotMapped] private int[][] Board { get; set; }
        public int BoardHeight { get; set; }
        public int BoardWidth { get; set; }
        public bool PlayerOneMove { get; set; }

        public Game()
        {
            Console.WriteLine("no args constructor initialized game");
        }
        
        public Game(GameSettings settings)
        {
            if (settings.BoardHeight < settings.MinSize || settings.BoardWidth < settings.MinSize)
                throw new ArgumentException("Board size has to be at least " + settings.MinSize + "x" + settings.MinSize + ".");
            
            if (settings.BoardHeight > settings.MaxSize || settings.BoardWidth > settings.MaxSize)
                throw new ArgumentException("Board size has to be less than " + settings.MaxSize + "x" + settings.MaxSize + ".");

            BoardHeight = settings.BoardHeight;
            BoardWidth = settings.BoardWidth;
            Board = InitializeBoard(settings.BoardHeight, settings.BoardWidth);
            SaveCreationDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            SerializeBoard(Board);
        }

        public void SetBoard(int[][] board)
        {
            Board = board;
            SerializeBoard(Board);
        }
        
        public int[][] GetBoard()
        {
            var result = InitializeBoard(BoardHeight, BoardWidth);
            Array.Copy(Board, result, Board.Length);
            return result;
        }

        public void Move(int x)
        {
            var posX = x - 1;
            var posY = GetLowestEmptyPos(posX);
            Board[posY][posX] = PlayerOneMove ? 1 : 2;
            SerializeBoard(Board);
            PlayerOneMove = !PlayerOneMove;
        }

        private int GetLowestEmptyPos(int posX)
        {
            for (var i = BoardHeight-1; i > -1; i--)
            {
                if (Board[i][posX] == 0)
                    return i;
            }

            return -1;
        }
        
        public bool IsGameDone()
        {
            for (var yIndex = 0; yIndex < BoardHeight; yIndex++)
            {
                for (var xIndex = 0; xIndex < BoardWidth; xIndex++)
                {
                    if (Board[yIndex][xIndex] == 0)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public bool IsColumnFull(int x)
        {
            var posX = x - 1;
            for (var i = 0; i < BoardHeight; i++)
            {
                if (Board[i][posX] == 0)
                    return false;
            }

            return true;
        }
        
        private int[][] InitializeBoard(int height, int width)
        {
            int[][] board = new int[height][];
            for (var i = 0; i < height; i++)
            {
                board[i] = new int[width];
            }
            return board;
        }

        public override string ToString()
        {
            return "BoardString: " + BoardString + ", saveName: " + SaveName + ", SaveCreationDateTime: " +
                   SaveCreationDateTime +
                   ", Board: " + Board + ", BoardHeight: " + BoardHeight + ", BoardWidth" + BoardWidth +
                   ", playeronemove: " + PlayerOneMove;
        }

        public void DeserializeBoard(string boardString)
        {
            Board = JsonSerializer.Deserialize<int[][]>(boardString);
        }

        public void SerializeBoard(int[][] board)
        {
            BoardString = JsonSerializer.Serialize(board);
        }
    }
}