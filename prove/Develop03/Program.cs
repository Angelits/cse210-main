//For exceeding the requirements I added a bonus system so the user can receive even more points than estimated to have a sense of accomplishment
//and extra recognition
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent(ref int score);
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent(ref int score)
    {
        if (!_isComplete)
        {
            _isComplete = true;
            score += _points + 20;
            Console.WriteLine($"Congratulations you accomplished another goal +{_points + 20}pts");
        }
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDetailsString() => $"{GetStringRepresentation()} (Completed: {_isComplete})";

    public override string GetStringRepresentation() => $"{_shortName}: {_description}";
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent(ref int score)
    {
        score += _points;
        Console.WriteLine($"You earned {_points} points for recording your eternal goal.");
    }

    public override bool IsComplete() => false; 

    public override string GetDetailsString() => GetStringRepresentation();

    public override string GetStringRepresentation() => $"{_shortName}: {_description}";
}

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus) : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent(ref int score)
    {
        if (_amountCompleted < _target)
        {
            _amountCompleted++;
            score += _points;
            Console.WriteLine($"You earned {_points} points for recording your checklist goal.");
            if (_amountCompleted == _target)
            {
                score += _bonus;
                Console.WriteLine($"Congratulations! You've completed the checklist goal and earned a bonus of {_bonus} points!");
            }
        }
    }

    public override bool IsComplete() => _amountCompleted >= _target;

    public override string GetDetailsString() => $"{GetStringRepresentation()} (Completed: {_amountCompleted}/{_target})";

    public override string GetStringRepresentation() => $"{_shortName}: {_description}";
}

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;

    public GoalManager()
    {
        _score = 0;
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Welcome! Menu Options:(for every goal completed you get a bonus of 20pts! multiplying your effort)");
            Console.WriteLine("1. Create a new Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    SaveGoals("goals.json");
                    break;
                case "4":
                    LoadGoals("goals.json");
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    Console.WriteLine("Goodbye! come back soon and earn more personal points");
                    return; 
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"Current Score: {_score}");
    }

    public void ListGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            var goal = _goals[i];
            string status = goal.IsComplete() ? "[X]" : "[ ]";
            Console.WriteLine($"{i + 1}. {status} {goal.GetStringRepresentation()}");
        }
        DisplayPlayerInfo(); 
    }

    public void CreateGoal()
    {
        Console.WriteLine("Enter goal type (simple, eternal, checklist): ");
        var type = Console.ReadLine().ToLower();

        Console.WriteLine("Enter goal name: ");
        var name = Console.ReadLine();

        Console.WriteLine("Enter goal description: ");
        var description = Console.ReadLine();

        Console.WriteLine("Enter points for the goal: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "simple")
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == "eternal")
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (type == "checklist")
        {
            Console.WriteLine("Enter target amount: ");
            int target = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter bonus points for completion: ");
            int bonus = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("Enter the name of the goal you completed: ");
        var name = Console.ReadLine();

        var goal = _goals.Find(g => g.GetStringRepresentation().Contains(name));
        if (goal != null)
        {
            goal.RecordEvent(ref _score);
            Console.WriteLine($"Current Score: {_score}"); 
        }
        else
        {
            Console.WriteLine("Goal not found.");
        }
    }

    public void SaveGoals(string filename)
    {
        var json = JsonSerializer.Serialize(_goals);
        File.WriteAllText(filename, json);
        Console.WriteLine("Goals saved.");
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            var json = File.ReadAllText(filename);
            _goals = JsonSerializer.Deserialize<List<Goal>>(json);
            Console.WriteLine("Goals loaded.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}
