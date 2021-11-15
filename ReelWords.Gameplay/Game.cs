using ReelWords.Gameplay.Services;
using ReelWords.Gameplay.State;
using System.Linq;
using System.Threading.Tasks;

namespace ReelWords.Gameplay
{
    public class Game
    {
        private readonly SlotMachine slotMachine;

        private readonly IDrawService drawService;
        private readonly IInputService inputService;

        private readonly Input input;

        private readonly GameState state;

        public Game(SlotMachine slotMachine, IInputService inputService, IDrawService drawService)
        {
            this.slotMachine = slotMachine;
            this.inputService = inputService;
            this.drawService = drawService;

            input = new Input(slotMachine.Length);

            state = new GameState();
        }

        public async Task RunAsync()
        {
            Initialize();
            Draw();

            while (state.IsPlaying)
            {
                await UpdateAsync();
                Draw();
            }
        }

        private void Initialize()
        {
            slotMachine.Shuffle();

            state.IsPlaying = true;

            UpdateSlotMachineState();
            UpdateInputState();
        }

        private async Task UpdateAsync()
        {
            var key = await inputService.ReadKeyAsync();

            if (key == Input.Enter)
            {
                ManageEnter();
            }
            else if (key == Input.Escape)
            {
                ManageEscape();
            }
            else
            {
                ManageKeyPress(key);
            }
        }

        private void Draw()
        {
            drawService.Draw(state);
        }

        private void ManageEnter()
        {
            var indices = input.Indices;

            if (indices.Length <= 0) { return; }

            state.Word = new string(slotMachine.GetLetters(indices));
            state.IsWordValid = slotMachine.IsValid(state.Word);

            if (state.IsWordValid == true)
            {
                state.WordPoints = slotMachine.GetScore(indices);
                state.Score += state.WordPoints;
            }

            slotMachine.Next(indices);
            input.Clear();

            UpdateSlotMachineState();
            UpdateInputState();
        }

        private void ManageEscape()
        {
            state.IsPlaying = false;
        }

        private void ManageKeyPress(char key)
        {
            input.Update(key);

            state.IsWordValid = null;
            UpdateInputState();
        }

        private void UpdateSlotMachineState()
        {
            state.SlotMachine.Slots = slotMachine.Letters
                .Zip(slotMachine.Points)
                .Select((items, index) => new SlotState
                {
                    Letter = items.First,
                    Key = (char)(Input.FirstLetter + index),
                    Points = items.Second
                }).ToArray();
        }

        private void UpdateInputState()
        {
            var points = slotMachine.Points;
            var letters = slotMachine.Letters;

            state.Input.Slots = input.Indices
                .Select(index => new SlotState
                {
                    Letter = letters[index],
                    Key = (char)(Input.FirstLetter + index),
                    Points = points[index]
                }).ToArray();
        }
    }
}
