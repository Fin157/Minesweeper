namespace Minesweeper
{
    /// <summary>
    /// A class used for gathering keypress input from the user
    /// </summary>
    internal static class InputManager
    {
        private static CommandMapping[] commandMappings =
        {
            new(CommandType.Dig, "DIG"),
            new(CommandType.Mark, "MARK")
        };

        public static Input TakeInput()
        {
            // Syntax of one user input line: /(type of command) (tile x) (tile y)

            Console.Write("/");
            
            // Prevent receiving a null input
            string? command;
            do
            {
                command = Console.ReadLine();
            } while (command == null);

            string[] splitCommand = command.Split(' ');

            // Extract the command type from the whole command
            CommandType commandType;
            foreach (CommandMapping cm in commandMappings)
            {
                if (cm.commandCode == splitCommand[0])
                {
                    commandType = cm.command;
                    break;
                }
            }

            if (int.TryParse(splitCommand[1]))
        }

        private static bool ValidateCommand(out CommandType command)
        {

        }

        private static bool ValidatePosition()
        {

        }
    }
}
