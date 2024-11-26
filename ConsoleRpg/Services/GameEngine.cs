using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private readonly GameContext _context;
    private readonly MenuManager _menuManager;
    private readonly OutputManager _outputManager;

    private IPlayer _player;
    private IMonster _goblin;

    public GameEngine(GameContext context, MenuManager menuManager, OutputManager outputManager)
    {
        _menuManager = menuManager;
        _outputManager = outputManager;
        _context = context;
    }

    public void Run()
    {
        while (true)
        {
            int retval = _menuManager.ShowMainMenu();
            switch (retval)
            {
                case 1:
                    SetupGame();
                    continue;
                case 0:
                    //_outputManager.WriteLine("Exiting game...", ConsoleColor.Red);
                    //_outputManager.Display();
                    Environment.Exit(0);
                    break;
                default:
                    continue;
            }
        }
    }
    private void GameLoop()
    {
        while (true)
        {
            _outputManager.Clear();
            _outputManager.WriteLine("Choose an action:", ConsoleColor.Cyan);
            _outputManager.WriteLine("1. Player Attack");
            _outputManager.WriteLine("2. Player Inventory Management");
            _outputManager.WriteLine("3. Player Maintenance");
            _outputManager.WriteLine("0. Previous menu");

            _outputManager.Display();

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AttackCharacter();
                    continue;
                case "2":
                    _outputManager.WriteLine("Starting Inventory Managment...", ConsoleColor.Green);
                    _outputManager.Display();
                    InventoryManagment();
                    continue;
                case "3":
                    _outputManager.WriteLine("Starting Player Maintenance...", ConsoleColor.Green);
                    _outputManager.Display();
                    PlayerMaintenance();
                    continue;
                case "0":
                    _outputManager.WriteLine("Exiting menu...", ConsoleColor.Red);
                    _outputManager.Display();
                    //Environment.Exit(0);
                    return;
                    break;
                default:
                    _outputManager.WriteLine("Invalid selection. Please choose 0 - 3.", ConsoleColor.Red);
                    break;
            }
        }
    }
    private void InventoryManagment()
    {
        while (true)
        {
            int retval = _menuManager.ShowInventoryManagementMenu();
            switch (retval)
            {
                case 1:
                    // Search for item by name
                    SearchItemByName();
                    continue;
                case 2:
                    //Search for item by type
                    SearchItemByType();
                    continue;
                case 3:
                    SortItem();
                    continue;
                case 0:
                    return;
                default:
                    continue;
            }
        }
    }
    private void PlayerMaintenance()
    {
        while (true)
        {
            int retval = _menuManager.ShowPlayerMaintenanceMenu();
            switch (retval)
            {
                case 1:
                    // Search for item by name
                    AdjustWeightAllowance();
                    continue;
                case 2:
                    //Search for item by type
                    PlayerAddItem();
                    continue;
                case 0:
                    return;
                default:
                    continue;
            }
        }
    }

    private void SearchItemByName()
    {
        Random random = new Random();
        while (true)
        {
            _outputManager.Clear();
            _outputManager.WriteLine("What is the 'Precious' you are looking for my liege, 'Exit' to Quit search.");
            _outputManager.Display();

            var input = Console.ReadLine();
            if (input.ToLower() == "exit")
            {
                _outputManager.WriteLine("Returning to the previous menu.");
                _outputManager.Display();
                return;
            }
            else {
                _outputManager.WriteLine("I am looking my Lord/Lady, please give me a moment....");
                _outputManager.Display();

                int randomNumber = random.Next(100, 500);
                Thread.Sleep(randomNumber);

                if (_player.SearchItemByName(input) == false)
                {
                    _outputManager.WriteLine($"\nI am turely sorry but I could not find my, I mean your precious {input} amongst all your belongings.");
                    _outputManager.Display();
                    //input = Console.ReadLine();
                }
                else {
                    _outputManager.WriteLine($"My 'Precious' {input} there you are, now now!");
                    _outputManager.WriteLine("Any key to continue.");
                    _outputManager.Display();
                    input = Console.ReadLine();
                }
                _outputManager.WriteLine($"\nShall we try another 'Precious' perhaps?  Yes or No");
                _outputManager.Display();

                input = Console.ReadLine();
                if (input.ToLower() == "no")
                {
                    return;
                }
            }
        }
    }
    private void SearchItemByType()
    {
        List<string> types = _player.GetItemTypes();

        if (types != null && types.Count > 0)
        {
            // Some of this should be in the menu manager but ran out of time.
            while (true)
            {
                _outputManager.Clear();
                _outputManager.WriteLine($"Item Type Menu");
                _outputManager.Display();
                int cnt = 1;
                foreach (string type in types)
                {
                    _outputManager.WriteLine($"{cnt}. {type}");
                    _outputManager.Display();
                    cnt++;
                }

                _outputManager.WriteLine($"0. Previous menu");
                _outputManager.WriteLine($"Select a type to search by 1 - {types.Count()}");
                _outputManager.Display();
                var input = Console.ReadLine();
                int choice = Convert.ToInt32(input);

                // Should do a tryparse here but it was not working so I moved on

                if (choice != 0)
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine($"Here is a list of items of type '{types[choice-1]}'.");
                    _outputManager.Display();

                    _player.SearchItemByType(types[choice-1]);

                    _outputManager.WriteLine("Any key to continue.");
                    _outputManager.Display();
                    input = Console.ReadLine();
                }
                else
                {
                   _outputManager.WriteLine("Returning to the previous menu.");
                   _outputManager.Display();
                   return;
                }
            }
        }
    }

    private void SortItem()
    {
        while (true)
        {
            int retval = _menuManager.ShowSortItemMenu();
            switch (retval)
            {
                case 1:
                    // by name
                    SortItemsByName();
                    continue;
                case 2:
                    // by attack value
                    SortItemsByAttackValue();
                    continue;
                case 3:
                    // by Defense value
                    SortItemsByDefenseValue();
                    continue;
                case 0:
                    return;
                default:
                    continue;
            }
        }
    }

    void SortItemsByName()
    {
        _outputManager.Clear();
        _outputManager.WriteLine($"Here is a list of items sorted by Name");
        _outputManager.Display();

        _player.SortItemsByName();

        _outputManager.WriteLine("Any key to continue.");
        _outputManager.Display();
        var input = Console.ReadLine();
    }
    void SortItemsByAttackValue()
    {
        _outputManager.Clear();
        _outputManager.WriteLine($"Here is a list of items sorted by Attack Value then Name");
        _outputManager.Display();
        _player.SortItemsByAttackValue();

        _outputManager.WriteLine("Any key to continue.");
        _outputManager.Display();
        var input = Console.ReadLine();
    }
    void SortItemsByDefenseValue()
    {
        _outputManager.Clear();
        _outputManager.WriteLine($"Here is a list of items sorted by Defense Value then Name");
        _outputManager.Display();
        _player.SortItemsByDefenseValue();

        _outputManager.WriteLine("Any key to continue.");
        _outputManager.Display(); ;
        var input = Console.ReadLine();
    }

    private void AttackCharacter()
    {
        if (_goblin is ITargetable targetableGoblin)
        {
            _player.Attack(targetableGoblin);
            _player.UseAbility(_player.Abilities.First(), targetableGoblin);
        }
    }

    private void SetupGame()
    {
        _player = _context.Players.FirstOrDefault();
        _outputManager.WriteLine($"{_player.Name} has entered the game.", ConsoleColor.Green);

        // Load monsters into random rooms 
        LoadMonsters();

        // Pause before starting the game loop
        Thread.Sleep(500);
        GameLoop();
    }

    private void LoadMonsters()
    {
        _goblin = _context.Monsters.OfType<Goblin>().FirstOrDefault();
    }
    private void AdjustWeightAllowance()
    {
        _outputManager.Clear();
        
        _outputManager.WriteLine($"Players current weight allowance is {_player.GetWeightAllowance()}\n");
        _outputManager.WriteLine($"Enter any number to increase or decrease the players weight allow. ex Current = 5 enter 10 to increase or 3 to decrease.");
        _outputManager.Display();
        var input = Console.ReadLine();

        // should do some validations here but to expedite getting this turned in for now I need to just update the player and assume value is good.
        int value = Convert.ToInt32(input);
        _player.UpdateWeightAllowance(value);

        // Put it here for now just to force the udpate if I need to increase or decrease weight allowance for testing
        _context.SaveChanges();
    }
    private void PlayerAddItem()
    {
        _outputManager.Clear();
        _outputManager.WriteLine($"Enter the name of the item you want to add?");
        _outputManager.Display();
        var input = Console.ReadLine();
        if (input != null)
        {
            _player.AddItem(input);
            _outputManager.WriteLine($"Does nothing right now, any key to continue");
            _outputManager.Display();
            input = Console.ReadLine();

        }
    }
}
