namespace Minesweeper.Gameplay
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

        /// <summary>
        /// Extracts a position from two axis strings and converts them from 1-based to 0-based integers
        /// </summary>
        /// <param name="posStringX">The X axis string</param>
        /// <param name="posStringY">The Y axis string</param>
        /// <param name="position">An out parameter for the position</param>
        /// <returns>True if the extracting was successful, otherwise false</returns>
        public static bool GetFromStrings(string posStringX, string posStringY, out Position position)
        {
            position = new();

            if (!GetAxisFromString(posStringX, out int posX) || !GetAxisFromString(posStringY, out int posY))
                return false;

            position = new(posX - 1, posY - 1);

            return true;
        }

        /// <summary>
        /// Extracts an axis value from a string
        /// </summary>
        /// <param name="s">The axis string</param>
        /// <param name="axis">An out parameter for the axis value</param>
        /// <returns>True if the extraction was successful, otherwise false</returns>
        private static bool GetAxisFromString(string s, out int axis)
        {
            axis = int.TryParse(s, out int pos) ? pos : -1;
            return axis != -1;
        }
    }
}