//As an extra for exceeding the requirements I added a feedback prompt with the structure if, else if and else regarding the user experience //
using System;
using System.Collections.Generic;
using System.Linq;

class ScriptureStudy
{
    private Dictionary<string, string> scriptures;
    private HashSet<int> hiddenWords;
    private Random random;

    public ScriptureStudy()
    {
        scriptures = new Dictionary<string, string>
        {
            { "Alma 32:35", "Yea, because it is light and whatsoever is light is good" },
            { "John 3:16", "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life" },
            { "3 Nephi 18:20-21", "And whatsoever ye shall ask the Father in my name, which is right, believing that ye shall receive, behold it shall be given unto you. Pray in your families unto the Father, always in my name, that your wives and your children may be blessed" }
        };
        hiddenWords = new HashSet<int>();
        random = new Random();
    }

    public void StartSession()
    {
        var scriptureKeys = scriptures.Keys.ToList();
        var selectedScripture = scriptureKeys[random.Next(scriptureKeys.Count)];
        var scriptureText = scriptures[selectedScripture];

        Console.Clear();
        DisplayScripture(selectedScripture, scriptureText);

        while (true)
        {
            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");
            var input = Console.ReadLine();

            if (input?.ToLower() == "quit")
            {
                Console.WriteLine("Program exiting. Thank you for participating!");
                break; 
            }
            else if (hiddenWords.Count < scriptureText.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries).Length)
            {
                HideRandomWord(scriptureText);
                Console.Clear();
                DisplayScripture(selectedScripture, scriptureText);
            }
            else
            {
                Console.WriteLine("All the words are hidden now. Exiting.");
                break; 
            }
        }

        if (hiddenWords.Count < scriptureText.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries).Length)
        {
            ProvideFeedback();
        }
    }

    private void DisplayScripture(string reference, string text)
    {
        Console.WriteLine(reference);
        var words = text.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            if (hiddenWords.Contains(i))
            {
                Console.Write(new string('_', words[i].Length) + " ");
            }
            else
            {
                Console.Write(words[i] + " ");
            }
        }
        Console.WriteLine();
    }

    private void HideRandomWord(string text)
    {
        var words = text.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
        int wordIndex;
        do
        {
            wordIndex = random.Next(words.Length);
        } while (hiddenWords.Contains(wordIndex));

        hiddenWords.Add(wordIndex);
    }

    private void ProvideFeedback()
    {
        Console.WriteLine("\nThank you for participating in this scripture study session, was this method useful? (yes/no)");
        var feedback = Console.ReadLine()?.ToLower();

        if (feedback == "yes")
        {
            Console.WriteLine("Glad to hear that!!, have an excellent day!");
        }
        else if (feedback == "no")
        {
            Console.WriteLine("Understood, thank you for the feedback. Have a good day");
        }
        else
        {
            Console.WriteLine("Thank you for your response! Have a great day!");
        }
    }
}

class Program
{
    static void Main()
    {
        var scriptureStudy = new ScriptureStudy();
        scriptureStudy.StartSession();
    }
}