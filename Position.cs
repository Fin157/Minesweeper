namespace Minesweeper
{
    /// <summary>
    /// A struct representing a 2D position on the game map
    /// </summary>
    internal struct Position
    {
        /// <summary>
        /// The horizontal position on the map
        /// </summary>
        public int x;
        /// <summary>
        /// The vertical position on the map
        /// </summary>
        public int y;
        
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Combines this position with another one and returns the combined result
        /// </summary>
        /// <param name="other">The other position for this one to be combined with</param>
        /// <returns>The new position</returns>
        public Position Combine(Position other)
        {
            return new(x + other.x, y + other.y);
        }
    }
}
