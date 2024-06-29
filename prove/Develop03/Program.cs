using System;
using System.Collections.Generic;
using System.Linq;

class Reference
{
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int VerseStart { get; private set; }
    public int? VerseEnd { get; private set; }

    public Reference(string book, int chapter, int verseStart, int? verseEnd = null)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verseStart;
        VerseEnd = verseEnd;
    }

    public override string ToString()
    {
        if (VerseEnd.HasValue)
        {
            return $"{Book} {Chapter}:{VerseStart}-{VerseEnd}";
        }
        else
        {
            return $"{Book} {Chapter}:{VerseStart}";
        }
    }
}

class Word
{
    public string Text { get; private set; }
    public bool Hidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        Hidden = false;
    }

    public void Hide()
    {
        Hidden = true;
    }

    public override string ToString()
    {
        return Hidden ? "_____" : Text;
    }
}

class Scripture
{
    public Reference Reference { get; private set; }
    private List<Word> Words { get; set; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int count = 3)
    {
        var wordsToHide = Words.Where(word => !word.Hidden).ToList();
        if (wordsToHide.Count > 0)
        {
            var random = new Random();
            foreach (var word in wordsToHide.OrderBy(x => random.Next()).Take(count))
            {
                word.Hide();
            }
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(word => word.Hidden);
    }

    public override string ToString()
    {
        var wordsStr = string.Join(" ", Words);
        return $"{Reference}\n{wordsStr}";
    }
}

class Program
{
    private Scripture scripture;

    static void Main(string[] args)
    {
        var program = new Program();
        program.Run();
    }

    public void ClearScreen()
    {
        Console.Clear();
    }

    public void Run()
    {
        var reference = new Reference("John", 3, 16);
        var text = "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.";
        scripture = new Scripture(reference, text);

        while (true)
        {
            ClearScreen();
            Console.WriteLine(scripture);
            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("All words are hidden. Program will now exit.");
                break;
            }

            Console.Write("Press Enter to continue or type 'quit' to exit: ");
            var userInput = Console.ReadLine();
            if (userInput.ToLower() == "quit")
            {
                break;
            }
            else
            {
                scripture.HideRandomWords();
            }
        }
    }
}
