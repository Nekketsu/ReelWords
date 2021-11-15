using Microsoft.AspNetCore.Components;
using ReelWords.Blazor.Services;
using ReelWords.Gameplay;
using ReelWords.Gameplay.Readers;
using ReelWords.Gameplay.State;

namespace ReelWords.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private HttpClient HttpClient { get; set; }
        private Game game;

        public event EventHandler<char> KeyPressed;

        public GameState GameState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var slotMachine = await ReadSlotMachineAsync();

            var inputService = new InputService(this);
            var drawService = new DrawService(this);

            game = new Game(slotMachine, inputService, drawService);

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
