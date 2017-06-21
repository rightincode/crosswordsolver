using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crosswordsolver
{
    class Program
    {
        static string inputString;
        static int inputLength;

        static void Main(string[] args)
        {
            Console.WriteLine("Initializing");
            WordDictionary words = new WordDictionary();
            Console.WriteLine("Initializaiton Complete");

            do {

                PromptUser();

                if (inputLength == 0 || inputString.Length == 0)
                {
                    Console.WriteLine("Invalid input!");
                    Console.WriteLine("Hit the enter key to search again or 'Q' to quit");
                    continue;
                }

                List<string> matchedWords = words.FindMatch(inputLength, inputString);

                foreach (string matchedWord in matchedWords)
                {
                    Console.WriteLine(matchedWord);
                }
                Console.WriteLine("Search complete. Elapsed ticks for search: {0}", words.elapsedTicks);
                Console.WriteLine("Hit the enter key to search again or 'Q' to quit");

            } while (Console.ReadLine().ToUpper() != "Q");            
        }

        static void PromptUser()
        {
            Console.WriteLine("Welcome to the crossword solver.");
            Console.Write("Please enter the length (numerical value) of the word(s) you are searching: ");
            int.TryParse(Console.ReadLine(), out inputLength);

            Console.WriteLine("You are searching for word(s) with a length of {0}", inputLength);
            Console.WriteLine("Please enter the word pattern. Use '.' for matching 'any' letter in the specified position. (i.e. '.A.CE'");
            inputString = Console.ReadLine();
        }
    }
}
