namespace GameEngine
{
    public class GameSettings
    {
        public int BoardHeight { get; set; } = 6;
        public int BoardWidth { get; set; } = 8;
        public int MinSize { get; } = 4;
        public int MaxSize { get; } = 20;
    }
}