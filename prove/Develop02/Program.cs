using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    // Entry class representing a single journal entry
    class Entry
    {
        public string Date { get; }  // Read-only property
        public string Prompt { get; }
        public string Response { get; }

        public Entry(string date, string prompt, string response)
        {
            Date = date;   // Set in the constructor
            Prompt = prompt;
            Response = response;
        }

        public override string ToString()
        {
            return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
        }
    }

    // Journal class managing the list of entries and file operations
    class Journal
    {
        private List<Entry> entries;
        private string[] prompts = {
            "What miracles did you see today?",
            "If you had to change one thing about today, what would that be?",
            "What made today a day worth living?",
            "What is one new thing you learned today?"
        };

        public Journal()
        {
            entries = new List<Entry>();
        }

        public void AddEntry()
        {
            // Select a random prompt
            Random random = new Random();
            string randomPrompt = prompts[random.Next(prompts.Length)];

            Console.WriteLine($"Prompt: {randomPrompt}");
            Console.WriteLine("Enter your response:");
            string response = Console.ReadLine();

            Entry entry = new Entry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), randomPrompt, response);
            entries.Add(entry);
            Console.WriteLine("Entry added successfully!\n");
        }

        public void DisplayEntries()
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("No entries found.\n");
            }
            else
            {
                foreach (var entry in entries)
                {
                    Console.WriteLine(entry.ToString());
                }
            }
        }

        public void SaveToFile(string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    foreach (var entry in entries)
                    {
                        writer.WriteLine($"Date: {entry.Date}");
                        writer.WriteLine($"Prompt: {entry.Prompt}");
                        writer.WriteLine($"Response: {entry.Response}");
                        writer.WriteLine(); // Empty line between entries
                    }
                }
                Console.WriteLine($"Journal saved to {filename}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving journal: {ex.Message}\n");
            }
        }

        public void LoadFromFile(string filename)
        {
            try
            {
                entries.Clear(); // Clear existing entries

                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    string date = "";
                    string prompt = "";
                    string response = "";

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Date: "))
                        {
                            date = line.Substring("Date: ".Length);
                        }
                        else if (line.StartsWith("Prompt: "))
                        {
                            prompt = line.Substring("Prompt: ".Length);
                        }
                        else if (line.StartsWith("Response: "))
                        {
                            response = line.Substring("Response: ".Length);
                        }
                        else if (line.Trim() == "") // Empty line indicates end of an entry
                        {
                            Entry entry = new Entry(date, prompt, response);
                            entries.Add(entry);

                            // Reset variables for next entry
                            date = "";
                            prompt = "";
                            response = "";
                        }
                    }

                    // Add the last entry if not already added (in case file does not end with an empty line)
                    if (!string.IsNullOrWhiteSpace(date) && !string.IsNullOrWhiteSpace(prompt) && !string.IsNullOrWhiteSpace(response))
                    {
                        Entry entry = new Entry(date, prompt, response);
                        entries.Add(entry);
                    }
                }
                Console.WriteLine($"Journal loaded from {filename}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading journal: {ex.Message}\n");
            }
        }
    }

    // Program class for menu-driven user interface
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            string filename = "journal.txt"; // Default filename

            while (true)
            {
                Console.WriteLine("Journal Menu:");
                Console.WriteLine("1. Write a New Entry");
                Console.WriteLine("2. Display Journal");
                Console.WriteLine("3. Save Journal to File");
                Console.WriteLine("4. Load Journal from File");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("Enter the filename to save the journal (default: journal.txt): ");
                        string saveFilename = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(saveFilename))
                            filename = saveFilename;
                        journal.SaveToFile(filename);
                        break;
                    case "4":
                        Console.Write("Enter the filename to load the journal (default: journal.txt): ");
                        string loadFilename = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(loadFilename))
                            filename = loadFilename;
                        journal.LoadFromFile(filename);
                        break;
                    case "5":
                        Console.WriteLine("Exiting the journal. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }
    }
}
