using Microsoft.AspNetCore.Components;
using ReelWords.Blazor.Services;
using ReelWords.Gameplay;
using ReelWords.Gameplay.Readers;

namespace ReelWords.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private HttpClient HttpClient { get; set; }
        private Game game;

        public event EventHandler<char> KeyPressed;

        public Models.Reel[] Reels { get; set; }
        public char[] Input { get; set; }
        public int Score { get; set; }
        public string Word { get; set; }
        public bool? IsWordValid { get; set; }
        public int WordPoints { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var slotMachine = await ReadSlotMachineAsync();

            var inputManager = new InputManager(this);
            var drawManager = new DrawManager(this);

            game = new Game(slotMachine, inputManager, drawManager);

            await game.RunAsync();
        }

        public void ForceStateHasChanged()
        {
            StateHasChanged();
        }

        private void OnKeyPress(char key)
        {
            KeyPressed?.Invoke(this, key);
        }

        private void RemoveLetter()
        {
            KeyPressed?.Invoke(this, Gameplay.Input.Backspace);
        }

        private void SendWord()
        {
            KeyPressed?.Invoke(this, Gameplay.Input.Enter);
        }

        private async Task<SlotMachine> ReadSlotMachineAsync()
        {
            const string resourcesPath = "Resources";
            const string reelsPath = "reels.txt";
            const string scoresPath = "scores.txt";
            const string dictionaryPath = "american-english-large.txt";

            var reelsReader = new ReelsReader();
            var scoresReader = new ScoresReader();
            var dictionaryReader = new DictionaryReader();

            using var reelsStream = await HttpClient.GetStreamAsync(Path.Combine(resourcesPath, reelsPath));
            using var scoresStream = await HttpClient.GetStreamAsync(Path.Combine(resourcesPath, scoresPath));
            using var dictionaryStream = await HttpClient.GetStreamAsync(Path.Combine(resourcesPath, dictionaryPath));

            var reels = await reelsReader.ReadAsync(reelsStream);
            var scores = await scoresReader.ReadAsync(scoresStream);
            var dictionary = await dictionaryReader.ReadAsync(dictionaryStream);

            var slotMachine = new SlotMachine(reels, scores, dictionary);

            return slotMachine;
        }
    }
}
