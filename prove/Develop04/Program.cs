using System;
using System.Collections.Generic;
using System.Threading;

abstract class Activity
{
    protected int duration;

    public Activity(int duration)
    {
        this.duration = duration;
    }

    public void StartMessage(string name, string description)
    {
        Console.WriteLine($"Starting {name} Activity");
        Console.WriteLine(description);
        Console.WriteLine($"Duration: {duration} seconds");
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
    }

    public void EndMessage(string name)
    {
        Console.WriteLine("Good job!");
        Console.WriteLine($"You have completed the {name} activity for {duration} seconds.");
        ShowSpinner(3);
    }

    protected void ShowSpinner(int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    public abstract void PerformActivity();
}

class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base(duration) { }

    public override void PerformActivity()
    {
        StartMessage("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
        for (int i = 0; i < duration / 4; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner(2);
            Console.WriteLine("Breathe out...");
            ShowSpinner(2);
        }
        EndMessage("Breathing");
    }
}

class ReflectionActivity : Activity
{
    private static readonly List<string> Prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly List<string> Questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(int duration) : base(duration) { }

    public override void PerformActivity()
    {
        StartMessage("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
        Console.WriteLine(Prompts[new Random().Next(Prompts.Count)]);
        for (int i = 0; i < duration / 10; i++)
        {
            Console.WriteLine(Questions[new Random().Next(Questions.Count)]);
            ShowSpinner(10);
        }
        EndMessage("Reflection");
    }
}

class ListingActivity : Activity
{
    private static readonly List<string> Prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration) { }

    public override void PerformActivity()
    {
        StartMessage("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        Console.WriteLine(Prompts[new Random().Next(Prompts.Count)]);
        Console.WriteLine("Start listing items...");
        ShowSpinner(5);
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        List<string> items = new List<string>();
        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            items.Add(item);
        }
        Console.WriteLine($"You listed {items.Count} items.");
        EndMessage("Listing");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Quit");
            string choice = Console.ReadLine();

            Activity activity = null;

            if (choice == "1")
            {
                int duration = GetDuration();
                activity = new BreathingActivity(duration);
            }
            else if (choice == "2")
            {
                int duration = GetDuration();
                activity = new ReflectionActivity(duration);
            }
            else if (choice == "3")
            {
                int duration = GetDuration();
                activity = new ListingActivity(duration);
            }
            else if (choice == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            activity.PerformActivity();
        }
    }

    static int GetDuration()
    {
        Console.Write("Enter duration in seconds: ");
        return int.Parse(Console.ReadLine());
    }
}
