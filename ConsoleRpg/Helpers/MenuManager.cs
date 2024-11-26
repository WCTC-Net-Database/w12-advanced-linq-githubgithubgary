namespace ConsoleRpg.Helpers;

public class MenuManager
{
    private readonly OutputManager _outputManager;

    public MenuManager(OutputManager outputManager)
    {
        _outputManager = outputManager;
    }

    public int ShowMainMenu()
    {
        _outputManager.Clear();
        _outputManager.WriteLine("Welcome to the RPG Game!", ConsoleColor.Yellow);
        _outputManager.WriteLine("1. Start Game", ConsoleColor.Cyan);
        _outputManager.WriteLine("0. Exit", ConsoleColor.Cyan);
        _outputManager.Display();

        return HandleMainMenuInput();
    }

    private int HandleMainMenuInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    _outputManager.WriteLine("Starting game...", ConsoleColor.Green);
                    _outputManager.Display();
                    return 1;
                case "0":
                    _outputManager.WriteLine("Exiting game...", ConsoleColor.Red);
                    _outputManager.Display();
                    //Environment.Exit(0);
                    return 0;
                default:
                    _outputManager.WriteLine("Invalid selection. Please choose 0 or 1.", ConsoleColor.Red);
                    _outputManager.Display();
                    break;
            }
        }
    }
    public int ShowInventoryManagementMenu()
    {
        _outputManager.Clear();
        _outputManager.WriteLine("\nInventory Management:", ConsoleColor.Yellow);
        _outputManager.WriteLine("1. Search for item by name", ConsoleColor.Cyan);
        _outputManager.WriteLine("2. List items by type", ConsoleColor.Cyan);
        _outputManager.WriteLine("3. Sort items", ConsoleColor.Cyan);
        _outputManager.WriteLine("0. Previous Menu", ConsoleColor.Cyan);
        _outputManager.Display();

        return HandleInventoryManagmentMenuInput();
    }
    private int HandleInventoryManagmentMenuInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    _outputManager.WriteLine("Search by Name...", ConsoleColor.Green);
                    _outputManager.Display();
                    return 1;
                case "2":
                    _outputManager.WriteLine("List by Type...", ConsoleColor.Green);
                    _outputManager.Display();
                    return 2;
                case "3":
                    _outputManager.WriteLine("Sort Items...", ConsoleColor.Green);
                    _outputManager.Display();
                    return 3;
                case "0":
                    _outputManager.WriteLine("Return to previous menu...", ConsoleColor.Red);
                    _outputManager.Display();
                    return 0;
                default:
                    _outputManager.WriteLine("Invalid selection. Please choose 0-3", ConsoleColor.Red);
                    _outputManager.Display();
                    break;
            }
        }
    }
    public int ShowSortItemMenu()
    {
        _outputManager.Clear();
        _outputManager.WriteLine("\nSort Item Menu:", ConsoleColor.Yellow);
        _outputManager.WriteLine("1. by Name", ConsoleColor.Cyan);
        _outputManager.WriteLine("2. by Attack Value", ConsoleColor.Cyan);
        _outputManager.WriteLine("3. by Defense Value", ConsoleColor.Cyan);
        _outputManager.WriteLine("0. Previous Menu", ConsoleColor.Cyan);
        _outputManager.Display();

        return HandleSortItemMenuInput();
    }
    private int HandleSortItemMenuInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    _outputManager.WriteLine("Sort Item by Name...", ConsoleColor.Green);
                    _outputManager.Display();
                    return 1;
                case "2":
                    _outputManager.WriteLine("Sort Item by Attach Value...", ConsoleColor.Green);
                    _outputManager.Display();
                    return 2;
                case "3":
                    _outputManager.WriteLine("Sort Item by Defense Value...", ConsoleColor.Green);
                    _outputManager.Display();
                    return 3;
                case "0":
                    _outputManager.WriteLine("Return to previous menu...", ConsoleColor.Red);
                    _outputManager.Display();
                    return 0;
                default:
                    _outputManager.WriteLine("Invalid selection. Please choose 0-3", ConsoleColor.Red);
                    _outputManager.Display();
                    break;
            }
        }
    }
    public int ShowPlayerMaintenanceMenu()
    {
        _outputManager.Clear();
        _outputManager.WriteLine("\nPlayer Maintenance Menu:", ConsoleColor.Yellow);
        _outputManager.WriteLine("1. Adjust Weight allowance", ConsoleColor.Cyan);
        _outputManager.WriteLine("2. Add an Item", ConsoleColor.Cyan);
        _outputManager.WriteLine("0. Previous Menu", ConsoleColor.Cyan);
        _outputManager.Display();

        return HandlePlayerMaintenanceMenuInput();
    }
    private int HandlePlayerMaintenanceMenuInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return 1;
                case "2":
                    return 2;
                case "0":
                    _outputManager.WriteLine("Return to previous menu...", ConsoleColor.Red);
                    _outputManager.Display();
                    return 0;
                default:
                    _outputManager.WriteLine("Invalid selection. Please choose 0-2", ConsoleColor.Red);
                    _outputManager.Display();
                    break;
            }
        }
    }
}
