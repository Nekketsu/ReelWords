using ReelWords.Gameplay;
using ReelWords.Gameplay.Readers;
using ReelWords.Services;
using System.IO;
using System.Threading.Tasks;

namespace ReelWords
{
    class Program
    {
        static async Task Main(string[] args)
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

            var slotMachine = await ReadSlotMachineAsync();

            var inputManager = new InputManager();
            var drawManager = new DrawManager();

            var game = new Game(slotMachine, inputManager, drawManager);

            await game.RunAsync();
        }

        private static async Task<SlotMachine> ReadSlotMachineAsync()
        {
            const string resourcesPath = "Resources";
            const string dictionaryPath = "american-english-large.txt";
            const string reelsPath = "reels.txt";
            const string scoresPath = "scores.txt";

            var dictionaryReader = new DictionaryReader();
            var reelsReader = new ReelsReader();
            var scoresReader = new ScoresReader();

            using (var dictionaryStream = File.OpenRead(Path.Combine(resourcesPath, dictionaryPath)))
            using (var reelsStream = File.OpenRead(Path.Combine(resourcesPath, reelsPath)))
            using (var scoresStream = File.OpenRead(Path.Combine(resourcesPath, scoresPath)))
            {
                var dictionary = await dictionaryReader.ReadAsync(dictionaryStream);
                var reels = await reelsReader.ReadAsync(reelsStream);
                var scores = await scoresReader.ReadAsync(scoresStream);

                var slotMachine = new SlotMachine(reels, scores, dictionary);

                return slotMachine;
            }
        }
    }
}