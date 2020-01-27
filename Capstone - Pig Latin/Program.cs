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
            
            // Exit program message
            SetOutputColor();
            Console.WriteLine("\n\nThank you for using the Pig Latin Translator!");
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

        public static bool isAllCaps (string checkString)
        {
            for (int i = 0; i < (checkString.Length); i++)
            {
                if (!Char.IsUpper(checkString[i]))
                {
                    // If a lowercase letter (or a non-uppercase letter) is found, isAllCaps is false
                    return false;
                }
            }
            // If this is run, all previous letters were capital letters.
            return true;
        }

        public static bool isTitle (string checkString)
        {
            // Check if first letter is caps
            if (!Char.IsUpper(checkString[0]))
            {
                return false;
            }
            
            // If first letter is caps, check if remaining letters are lowercase
            for (int i = 1; i < (checkString.Length); i++)
            {
                if(!Char.IsLower(checkString[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isAlpha (string checkString)
        {
            // Check if string only contains alpha characters
            for (int i = 0; i < checkString.Length; i++)
            {
                // Allow apostrophes
                if (checkString[i] == '\'')
                {

                }
                else if (!char.IsLetter(checkString[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool hasNoVowels (string checkString)
        {
            // This method created to account for certain words with no vowels (usually contain a "y")
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u'};

            for (int i=0; i < checkString.Length; i++)
            {
                if (checkString.IndexOfAny(vowels) == -1)
                {
                    return true;
                }
            }
            return false;
        }

         public static bool isNum (string checkString)
        {
            int intNum;
            double dblNum;
            bool isNumeric = true;

            // Check if string can be interpreted as an integer. If so, return true.
            if (isNumeric == int.TryParse(checkString, out intNum))
            {
                return isNumeric;
            }

            // Check if string can be interpreted as a double. If so, return true.
            else if (isNumeric == double.TryParse(checkString, out dblNum))
            {
                return isNumeric;
            }
            
            // String is not a number of any kind
            else
            {
                isNumeric = false;
            }

            return isNumeric;
        }

        public static void PigLatinTranslate(string userPhrase)
        {
            string workPhrase, newWord, nextWordPart, reTitle;
            string[] tempPhrase;
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u'};
            bool isTitleCase, isAllCapitals;
            
            StringBuilder pigLatinPhrase = new StringBuilder();
            StringBuilder wordBuilder = new StringBuilder();

            // Convert phrase to lowercase
            workPhrase = userPhrase;  // string to preserve original user text
            tempPhrase = workPhrase.Split();    // splits phrase into words

            // Process each word
            for (int i=0; i < tempPhrase.Length; i++)
            {
                if (isNum(tempPhrase[i]))
                {
                    // If "word" is all numeric, just pass on the number as is.
                    isAllCapitals = false;
                    isTitleCase = false;
                    newWord = tempPhrase[i];
                }
                else if (!isAlpha(tempPhrase[i]))
                {
                    // If "word" is not letters (apostrophes allowed), pass word as is
                    isAllCapitals = false;
                    isTitleCase = false;
                    newWord = tempPhrase[i];
                }
                else if (tempPhrase[i].IndexOfAny(vowels)==0)
                {
                    // First letter is a vowel
                    isAllCapitals = isAllCaps(tempPhrase[i]);
                    isTitleCase = isTitle(tempPhrase[i]);
                    tempPhrase[i] = tempPhrase[i].ToLower();  // convert to lowercase
                    newWord = $"{tempPhrase[i]}way";
                }
                else
                {
                    // First letter is a consonant
                    isAllCapitals = isAllCaps(tempPhrase[i]);
                    isTitleCase = isTitle(tempPhrase[i]);
                    tempPhrase[i] = tempPhrase[i].ToLower();  // convert to lowercase

                    wordBuilder.Clear();
                    
                    if (hasNoVowels(tempPhrase[i]))  // check to see if word has no vowels
                    {
                        newWord = $"{tempPhrase[i]}way";
                    }
                    else
                    {
                        // Creates new starting at the first vowel and stores into wordBuilder
                        wordBuilder.Append(tempPhrase[i].Substring(tempPhrase[i].IndexOfAny(vowels)));

                        // string to capture newly created word part. Will be used to use Length property.
                        nextWordPart = wordBuilder.ToString();

                        // Add into WordBuilder the first part of the original word from the first character to
                        // to the character before the first vowel. (Length of original word - Length of the stored word part)
                        wordBuilder.Append(tempPhrase[i].Substring(0, (tempPhrase[i].Length - nextWordPart.Length)));

                        //  Add the "ay".  Because it's Pig Latin. Those are the rules.
                        wordBuilder.Append("ay");

                        // Convert wordBuilder back into string newWord
                        newWord = wordBuilder.ToString();
                    }
                }

                // Restore original capitalization (Title case or all caps)
                if (isAllCapitals)
                {
                    newWord = newWord.ToUpper();
                }

                if (isTitleCase)
                {
                    reTitle = newWord.Substring(0, 1).ToUpper();
                    newWord = reTitle + newWord.Substring(1);
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
