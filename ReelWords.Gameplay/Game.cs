using ReelWords.Gameplay.Services;
using ReelWords.Gameplay.State;
using System.Threading.Tasks;

namespace ReelWords.Gameplay
{
    public class Game
    {
        private readonly SlotMachine slotMachine;

        private readonly IDrawManager drawManager;
        private readonly IInputManager inputManager;


        private readonly Input input;

        private readonly GameState state;

        public Game(SlotMachine slotMachine, IInputManager inputManager, IDrawManager drawManager)
        {
            this.slotMachine = slotMachine;
            this.inputManager = inputManager;
            this.drawManager = drawManager;

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
            var key = await inputManager.ReadKeyAsync();

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
            drawManager.Draw(state);
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
            UpdateInputState();
        }

        private void UpdateSlotMachineState()
        {
            state.SlotMachineState.Points = slotMachine.Points;
            state.SlotMachineState.Letters = slotMachine.Letters;
        }

        private void UpdateInputState()
        {
            state.InputState.Indices = input.Indices;
            state.InputState.Letters = slotMachine.GetLetters(state.InputState.Indices);
        }
    }
}
