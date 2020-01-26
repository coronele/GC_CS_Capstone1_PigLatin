using System;
using System.Text;

namespace Capstone___Pig_Latin
{
    class Program
    {
        static void Main(string[] args)
        {
            string userPhrase;
            bool continueProg;
            
            // Shows program title
            ShowTitle("Pig Latin Generator");

            // Main program - Looping based on status of "continueProg"
            do
            {
                // User prompt and input
                userPhrase = GetUserInput("Please enter a word/phrase to convert into Pig Latin: ");

                // Process user supplied input.  Turn that stuff into Pig Latin!
                if (userPhrase=="")
                {
                    SetOutputColor();
                    Console.WriteLine("You did not enter a string to translate!\n");
                }
                else
                {
                    PigLatinTranslate(userPhrase);
                }

                // Prompt user to translate again
                continueProg = TryAgain("Would you like to translate another word/phrase? [y/n] ");
            }
            while (continueProg);
        }

        public static string GetUserInput(string message)
        {
            // Allows for program prompt and user input (string)
            SetOutputColor();
            Console.WriteLine(message);
            SetInputColor();
            string input = Console.ReadLine();
            return input;
        }
        public static void SetInputColor()
        {
            // Method for setting colors for user inputted text
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void SetOutputColor()
        {
            // Method for setting colors for default display text
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void ShowTitle(string title)
        {
            // This method simply shows the title
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{title} \n\n");
        }

        public static bool TryAgain(string message)
        {
            // Method for running program again.  Passes back to do while loop in main.
            string userChoice = GetUserInput(message).ToLower();
            bool continueProgram = true;
            while ((userChoice != "y") && (userChoice != "n"))
            {
                userChoice = GetUserInput("Please enter 'y' or 'n'. [y/n] ");
            }
            if (userChoice == "y")
                {
                continueProgram = true;
                }
            else
            {
                continueProgram = false;
            }
            return continueProgram;
        }

        public static void PigLatinTranslate(string userPhrase)
        {
            string workPhrase, newWord, nextWordPart;
            string[] tempPhrase;
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            
            StringBuilder pigLatinPhrase = new StringBuilder();
            StringBuilder wordBuilder = new StringBuilder();

            // Convert phrase to lowercase
            workPhrase = userPhrase.ToLower();  // string to preserve original user text
            tempPhrase = workPhrase.Split();    // splits phrase into words


            for (int i=0; i < tempPhrase.Length; i++)
            {
                if (tempPhrase[i].IndexOfAny(vowels)==0)
                {
                    // First letter is a vowel
                    newWord = $"{tempPhrase[i]}way";
                }
                else
                {
                    // First letter is a consonant
                    wordBuilder.Clear();
                    
                    // Creates new starting at the first vowel and stores into wordBuilder
                    wordBuilder.Append(tempPhrase[i].Substring(tempPhrase[i].IndexOfAny(vowels)));
                    
                    // string to capture newly created word part. Will be used to use Length property.
                    nextWordPart = wordBuilder.ToString();

                    // Add into WordBuilder the first part of the original word from the first character to
                    // to the character before the first vowel. (Length of original word - Length of the stored word part)
                    wordBuilder.Append(tempPhrase[i].Substring(0,(tempPhrase[i].Length - nextWordPart.Length)));
                    
                    //  Add the "ay".  Because it's Pig Latin. Those are the rules.
                    wordBuilder.Append("ay");

                    // Convert wordBuilder back into string newWord
                    newWord = wordBuilder.ToString();
                }

                // Add a space before words besides the first one
                if (i > 0)
                {
                    pigLatinPhrase.Append(" ");
                }
                
                // Add newWord as part of the Pig Latin Phrase
                pigLatinPhrase.Append(newWord);
            }

            // Output to user
            Console.WriteLine($"\nThe Pig Latin translation of \"{userPhrase}\" is: \"{pigLatinPhrase}\"");
        }

    }
}
