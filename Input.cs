namespace Minesweeper
{
    /// <summary>
    /// A struct representing a piece of input gathered from the user during one game loop call
    /// </summary>
    internal struct Input
    {
        /// <summary>
        /// Position of the affected tile
        /// </summary>
        public Position tilePos;
        /// <summary>
        /// A command to be executed on the chosen tile
        /// </summary>
        public CommandType command;

        public Input(Position tilePos, CommandType command)
        {
            this.tilePos = tilePos;
            this.command = command;
        }
    }

    internal class CommandMapping
    {
        public CommandType command;
        public string commandCode;

        public CommandMapping(CommandType command, string commandCode)
        {
            this.command = command;
            this.commandCode = commandCode;
        }
    }

    public enum CommandType
    {
        Mark,
        Dig
    }
}
