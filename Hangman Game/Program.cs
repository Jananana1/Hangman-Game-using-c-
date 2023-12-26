using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

class HangmanGame
{
    class ScoreboardEntry
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public DateTime Timestamp { get; set; }
    }

    private static List<ScoreboardEntry> scoreboard = new List<ScoreboardEntry>();

    static void Main()
    {
        Console.Title = "Hangman Game";
        RunMainMenu();
    }

    private static void RunMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        string prompt = @"
          _____                    _____                    _____                    _____                    _____                    _____                    _____          
         /\    \                  /\    \                  /\    \                  /\    \                  /\    \                  /\    \                  /\    \         
        /::\____\                /::\    \                /::\____\                /::\    \                /::\____\                /::\    \                /::\____\        
       /:::/    /               /::::\    \              /::::|   |               /::::\    \              /::::|   |               /::::\    \              /::::|   |        
      /:::/    /               /::::::\    \            /:::::|   |              /::::::\    \            /:::::|   |              /::::::\    \            /:::::|   |        
     /:::/    /               /:::/\:::\    \          /::::::|   |             /:::/\:::\    \          /::::::|   |             /:::/\:::\    \          /::::::|   |        
    /:::/____/               /:::/__\:::\    \        /:::/|::|   |            /:::/  \:::\    \        /:::/|::|   |            /:::/__\:::\    \        /:::/|::|   |        
   /::::\    \              /::::\   \:::\    \      /:::/ |::|   |           /:::/    \:::\    \      /:::/ |::|   |           /::::\   \:::\    \      /:::/ |::|   |        
  /::::::\    \   _____    /::::::\   \:::\    \    /:::/  |::|   | _____    /:::/    / \:::\    \    /:::/  |::|___|______    /::::::\   \:::\    \    /:::/  |::|   | _____  
 /:::/\:::\    \ /\    \  /:::/\:::\   \:::\    \  /:::/   |::|   |/\    \  /:::/    /   \:::\ ___\  /:::/   |::::::::\    \  /:::/\:::\   \:::\    \  /:::/   |::|   |/\    \ 
/:::/  \:::\    /::\____\/:::/  \:::\   \:::\____\/:: /    |::|   /::\____\/:::/____/  ___\:::|    |/:::/    |:::::::::\____\/:::/  \:::\   \:::\____\/:: /    |::|   /::\____\
\::/    \:::\  /:::/    /\::/    \:::\  /:::/    /\::/    /|::|  /:::/    /\:::\    \ /\  /:::|____|\::/    / ~~~~~/:::/    /\::/    \:::\  /:::/    /\::/    /|::|  /:::/    /
 \/____/ \:::\/:::/    /  \/____/ \:::\/:::/    /  \/____/ |::| /:::/    /  \:::\    /::\ \::/    /  \/____/      /:::/    /  \/____/ \:::\/:::/    /  \/____/ |::| /:::/    / 
          \::::::/    /            \::::::/    /           |::|/:::/    /    \:::\   \:::\ \/____/               /:::/    /            \::::::/    /           |::|/:::/    /  
           \::::/    /              \::::/    /            |::::::/    /      \:::\   \:::\____\                /:::/    /              \::::/    /            |::::::/    /   
           /:::/    /               /:::/    /             |:::::/    /        \:::\  /:::/    /               /:::/    /               /:::/    /             |:::::/    /    
          /:::/    /               /:::/    /              |::::/    /          \:::\/:::/    /               /:::/    /               /:::/    /              |::::/    /     
         /:::/    /               /:::/    /               /:::/    /            \::::::/    /               /:::/    /               /:::/    /               /:::/    /      
        /:::/    /               /:::/    /               /:::/    /              \::::/    /               /:::/    /               /:::/    /               /:::/    /       
        \::/    /                \::/    /                \::/    /                \::/____/                \::/    /                \::/    /                \::/    /        
         \/____/                  \/____/                  \/____/                                           \/____/                  \/____/                  \/____/         
                                                                                                                                                                               
Main Menu: (Use the arrow keys to make your selection, then press the ENTER key.)";
        string[] options = { "Play", "How to Play", "Scoreboard", "About", "Exit" };
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {

            case 0:
                RunFirstChoice();
                break;
            case 1:
                DisplayHowToPlay();
                break;
            case 2:
                HangmanDisplayLeaderboard();
                break;
            case 3:
                AboutGame();
                break;
            case 4:
                ExitGame();
                break;
        }
    }

    private static void ExitGame()
    {
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }

    private static void AboutGame()
    {
        Console.Clear();
        Console.WriteLine("Developers: Janna Abbey Santos, Ella Mae Alad, and Mark Angelo Abangan");
        Thread.Sleep(1000);
        Console.WriteLine("This game was created on Novemeber 20, 2023 and finished on December 14, 2023");
        Thread.Sleep(1000);
        Console.WriteLine("      © All rights reserved 2023.      ");
        Console.WriteLine("PRESS ANY KEY TO CONTINUE");
        Console.ReadKey();
        Console.Clear(); // Clear the console after reading instructions
        RunMainMenu();
    }



    private static void RunFirstChoice()
    {
        Console.ResetColor();
        Console.Clear();
        Console.WriteLine("Welcome to Hangman!");
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        string[] unknownWords = { "programming", "computer", "break", "programmer", "code", "repetition", "variable", "data", "array", "flowchart", "modulus", "interpreter", "pseudocode", "comments", "keywords", "flowlines", "terminal", "Process", "assignment", "method" };
        string[] wordDescriptions = { "The process of writing code for software", "An electronic device for processing data", "type of execution that jumps out of loop", "Someone who writes code", "Instructions for a computer program", "a control structure that needs to execute repeatedly", "A storage location with a name", "Information processed by a computer", "multiple values at the same time", "A graphical representation of an algorithm.", "Often denoted by the % operator", "It translates the source code line by line.", "Also known as 'false code'.", "Use for explaining codes.", "Also known as 'reserved words'.", "use to connect a flowchart.", "Shows the beginning & end of flowchart.", "A symbol in flowchart that performs calculations.", "An operator that has a symbol of '='.", "Available or located inside the class." };

        bool playAgain = true;

        do
        {
            Console.Clear();
            string unknownWord = GetRandomWord(unknownWords);
            int wordList = Array.IndexOf(unknownWords, unknownWord);
            string wordDescription = wordDescriptions[wordList];

            char[] guessedLetters = new char[unknownWord.Length];
            for (int i = 0; i < guessedLetters.Length; i++)
            {
                guessedLetters[i] = '_';
            }

            int attempts = 6;

            Console.Write("Hello ");
            Thread.Sleep(1000);
            Console.Write($"{name}, ");
            Thread.Sleep(500);
            Console.Write("Welcome ");
            Thread.Sleep(500);
            Console.Write("to ");
            Thread.Sleep(500);
            Console.Write("Hangman ");
            Thread.Sleep(500);
            Console.WriteLine("Game! ");
            Thread.Sleep(500);


            while (attempts > 0)
            {
                Console.WriteLine($"\nCurrent Word: {new string(guessedLetters)}");
                Console.WriteLine($"Description: {wordDescription}");
                DisplayHangman(attempts);

                Console.Write("Guess a letter: ");
                char guess = Console.ReadLine().ToLower()[0];

                bool correctGuess = false;
                for (int i = 0; i < unknownWord.Length; i++)
                {
                    if (unknownWord[i] == guess)
                    {
                        guessedLetters[i] = guess;
                        correctGuess = true;
                    }
                }

                if (new string(guessedLetters) == unknownWord)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\aCongratulations! {name}, You guessed the word: {unknownWord}");
                    Console.ResetColor();
                    break;
                }

                if (!correctGuess)
                {
                    attempts--;
                    Console.WriteLine($"Incorrect guess! Attempts left: {attempts}");
                }

                if (attempts == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("   +---+");
                    Console.WriteLine("   O   |");
                    Console.WriteLine("  /|\\  |");
                    Console.WriteLine("  / \\  |");
                    Console.WriteLine($"\nSorry, you ran out of attempts. The correct word was: {unknownWord}");
                    Console.ResetColor();
                }
            }
            ScoreboardEntry newEntry = new ScoreboardEntry
            {
                PlayerName = name,
                Score = attempts * 10, // Adjust this scoring mechanism as needed
                Timestamp = DateTime.Now
            };

            scoreboard.Add(newEntry);

            Console.Write("Do you want to play again? (y/n): ");
            playAgain = GetValidYesNoInput();
        } while (playAgain);

        Console.Clear(); // Clear the console after finishing the game
        RunMainMenu();
    }

    private static void HangmanDisplayLeaderboard()
    {
        Console.Clear();
        Console.WriteLine("Scoreboard:");
        DisplayScores();

        Console.WriteLine("\nPress any key to return to the menu.");
        Console.ReadKey();
        RunMainMenu(); // Return to the main menu
    }

    private static void DisplayScores()
    {
        scoreboard.Sort((x, y) => y.Score.CompareTo(x.Score));

        foreach (var entry in scoreboard)
        {
            Console.WriteLine($"{entry.PlayerName} - {entry.Score} points - {entry.Timestamp}");
        }
    }

    private static void DisplayHowToPlay()
    {
        Console.Clear();
        Console.WriteLine("How to Play Hangman:");
        Console.WriteLine("1. A secret word will be chosen.");
        Console.WriteLine("2. Guess letters to reveal the word.");
        Console.WriteLine("3. You have 6 incorrect guesses before losing.");
        Console.WriteLine("4. Win by guessing the entire word correctly.");
        Console.WriteLine("\nPress any key to return to the menu.");
        Console.ReadKey();
        Console.Clear(); // Clear the console after reading instructions
        RunMainMenu();
    }

    private static void DisplayHangman(int attempts)
    {
        Console.WriteLine();
        switch (attempts)
        {
            case 0:
                Console.WriteLine("   +---+");
                Console.WriteLine("   O   |");
                Console.WriteLine("  /|\\  |");
                Console.WriteLine("  / \\  |");
                Console.WriteLine("      ===");
                break;
            case 1:
                Console.WriteLine("   +---+");
                Console.WriteLine("   O   |");
                Console.WriteLine("  /|\\  |");
                Console.WriteLine("  /    |");
                Console.WriteLine("      ===");
                break;
            case 2:
                Console.WriteLine("   +---+");
                Console.WriteLine("   O   |");
                Console.WriteLine("  /|\\  |");
                Console.WriteLine("       |");
                Console.WriteLine("      ===");
                break;
            case 3:
                Console.WriteLine("   +---+");
                Console.WriteLine("   O   |");
                Console.WriteLine("  /|   |");
                Console.WriteLine("       |");
                Console.WriteLine("      ===");
                break;
            case 4:
                Console.WriteLine("   +---+");
                Console.WriteLine("   O   |");
                Console.WriteLine("   |   |");
                Console.WriteLine("       |");
                Console.WriteLine("      ===");
                break;
            case 5:
                Console.WriteLine("   +---+");
                Console.WriteLine("   O   |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("      ===");
                break;
            case 6:
                Console.WriteLine("   +---+");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("      ===");
                break;
        }
    }

    private static string GetRandomWord(string[] words)
    {
        Random random = new Random();
        int index = random.Next(0, words.Length);
        return words[index];
    }

    private static bool GetValidYesNoInput()
    {
        while (true)
        {
            string input = Console.ReadLine().ToLower();
            if (input == "y")
            {
                return true;
            }
            else if (input == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Do you want to play again? (y/n): ");
            }
        }
    }

    class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        private void DisplayOptions()
        {
            Console.WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix = (i == SelectedIndex) ? "* " : "  ";
                Console.WriteLine($"{prefix} << {currentOption} >>");
            }
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex = (SelectedIndex - 1 + Options.Length) % Options.Length;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex = (SelectedIndex + 1) % Options.Length;
                }
            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
// Developers: Janna Santos, Ella Alad, and Angelo Abangan