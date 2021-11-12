using ReelWords.Gameplay;
using ReelWords.Services;
using System.IO;

namespace ReelWords
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool playing = true;

            //while (playing)
            //{
            //    string input = Console.ReadLine();

            //    // TODO:  Run game logic here using the user input string

            //    // TODO:  Create simple unit tests to test your code in the ReelWordsTests project,
            //    // don't worry about creating tests for everything, just important functions as
            //    // seen for the Trie tests

            //}

            var slotMachine = ReadSlotMachine();
            var game = new Game(slotMachine);

            game.Run();
        }

        private static SlotMachine ReadSlotMachine()
        {
            const string resourcesPath = "Resources";
            const string dictionaryPath = "american-english-large.txt";
            const string reelsPath = "reels.txt";
            const string scoresPath = "scores.txt";

            var dictionaryReader = new DictionaryReader();
            var reelsReader = new ReelsReader();
            var scoresReader = new ScoresReader();

            var dictionary = dictionaryReader.Read(Path.Combine(resourcesPath, dictionaryPath));
            var reels = reelsReader.Read(Path.Combine(resourcesPath, reelsPath));
            var scores = scoresReader.Read(Path.Combine(resourcesPath, scoresPath));

            var slotMachine = new SlotMachine(reels, scores, dictionary);

            return slotMachine;
        }
    }
}