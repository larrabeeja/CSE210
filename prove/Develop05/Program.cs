using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EternalQuest
{
    // Base Goal class
    public abstract class Goal
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public bool Completed { get; protected set; }

        protected Goal(string name, int points)
        {
            Name = name;
            Points = points;
            Completed = false;
        }

        public abstract int RecordEvent();
        public abstract string GetGoalType();
        public void SetCompleted(bool completed)
        {
            Completed = completed;
        }
        public override string ToString()
        {
            return $"{Name} - {Points} points";
        }
    }

    // SimpleGoal class
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, int points) : base(name, points) { }

        public override int RecordEvent()
        {
            if (!Completed)
            {
                Completed = true;
                return Points;
            }
            return 0;
        }

        public override string GetGoalType()
        {
            return "SimpleGoal";
        }
    }

    // EternalGoal class
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points) { }

        public override int RecordEvent()
        {
            return Points;
        }

        public override string GetGoalType()
        {
            return "EternalGoal";
        }
    }

    // ChecklistGoal class
    public class ChecklistGoal : Goal
    {
        public int Target { get; set; }
        public int CurrentCount { get; set; }

        public ChecklistGoal(string name, int points, int target) : base(name, points)
        {
            Target = target;
            CurrentCount = 0;
        }

        public override int RecordEvent()
        {
            if (CurrentCount < Target)
            {
                CurrentCount++;
                if (CurrentCount == Target)
                {
                    Completed = true;
                    return Points + 500; // Bonus points
                }
                return Points;
            }
            return 0;
        }

        public override string ToString()
        {
            return $"{Name} - {Points} points each, Completed {CurrentCount}/{Target}";
        }

        public override string GetGoalType()
        {
            return "ChecklistGoal";
        }
    }

    // GoalTracker class
    public class GoalTracker
    {
        private List<Goal> goals;
        public int Score { get; private set; }

        public GoalTracker()
        {
            goals = new List<Goal>();
            Score = 0;
        }

        public void AddGoal(Goal goal)
        {
            goals.Add(goal);
        }

        public void RecordEvent(string goalName)
        {
            foreach (var goal in goals)
            {
                if (goal.Name == goalName)
                {
                    Score += goal.RecordEvent();
                    break;
                }
            }
        }

        public void DisplayGoals()
        {
            foreach (var goal in goals)
            {
                string status = goal.Completed ? "[X]" : "[ ]";
                Console.WriteLine($"{status} {goal}");
            }
        }

        public void Save(string filename)
        {
            var data = new SaveData
            {
                Score = Score,
                Goals = new List<GoalData>()
            };
            foreach (var goal in goals)
            {
                var goalData = new GoalData
                {
                    GoalType = goal.GetGoalType(),
                    Name = goal.Name,
                    Points = goal.Points,
                    Completed = goal.Completed
                };
                if (goal is ChecklistGoal checklistGoal)
                {
                    goalData.Target = checklistGoal.Target;
                    goalData.CurrentCount = checklistGoal.CurrentCount;
                }
                data.Goals.Add(goalData);
            }
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filename, jsonString);
        }

        public void Load(string filename)
        {
            string jsonString = File.ReadAllText(filename);
            var data = JsonSerializer.Deserialize<SaveData>(jsonString);
            Score = data.Score;
            goals = new List<Goal>();

            foreach (var goalData in data.Goals)
            {
                Goal goal;
                if (goalData.GoalType == "SimpleGoal")
                {
                    goal = new SimpleGoal(goalData.Name, goalData.Points);
                }
                else if (goalData.GoalType == "EternalGoal")
                {
                    goal = new EternalGoal(goalData.Name, goalData.Points);
                }
                else if (goalData.GoalType == "ChecklistGoal")
                {
                    goal = new ChecklistGoal(goalData.Name, goalData.Points, goalData.Target)
                    {
                        CurrentCount = goalData.CurrentCount
                    };
                }
                else
                {
                    throw new Exception("Unknown goal type");
                }

                goal.SetCompleted(goalData.Completed); // Use the new method to set the completed status
                goals.Add(goal);
            }
        }

        public void DisplayScore()
        {
            Console.WriteLine($"Score: {Score}");
        }
    }

    // SaveData class for serialization
    public class SaveData
    {
        public int Score { get; set; }
        public List<GoalData> Goals { get; set; }
    }

    // GoalData class for serialization
    public class GoalData
    {
        public string GoalType { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public bool Completed { get; set; }
        public int Target { get; set; }
        public int CurrentCount { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GoalTracker tracker = new GoalTracker();

            tracker.AddGoal(new SimpleGoal("Run a marathon", 1000));
            tracker.AddGoal(new EternalGoal("Read scriptures", 100));
            tracker.AddGoal(new ChecklistGoal("Attend temple", 50, 10));

            tracker.RecordEvent("Read scriptures");
            tracker.RecordEvent("Attend temple");
            tracker.RecordEvent("Attend temple");
            tracker.RecordEvent("Run a marathon");

            tracker.DisplayGoals();
            tracker.DisplayScore();

            tracker.Save("goals.json");
            tracker.Load("goals.json");
            tracker.DisplayGoals();
            tracker.DisplayScore();
        }
    }
}
