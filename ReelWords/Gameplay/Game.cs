using ReelWords.Gameplay.State;
using System;

namespace ReelWords.Gameplay
{
    public class Game
    {
        private readonly SlotMachine slotMachine;

        private readonly DrawManager drawManager;
        private readonly InputManager input;

        private readonly GameState state;

        public Game(SlotMachine slotMachine)
        {
            this.slotMachine = slotMachine;

            drawManager = new DrawManager();
            input = new InputManager(slotMachine.Length);

            state = new GameState();
        }

        public void Run()
        {
            Initialize();
            Draw();

            while (state.IsPlaying)
            {
                Update();
                Draw();
            }
        }

        private void Initialize()
        {
            slotMachine.Shuffle();

            state.IsPlaying = true;

            state.SlotMachineState.Points = slotMachine.Points;
            state.SlotMachineState.Letters = slotMachine.Letters;

            state.InputState.Indices = input.Indices;
            state.InputState.Letters = slotMachine.GetLetters(state.InputState.Indices);
        }

        private void Update()
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                ManageEnter();
            }
            else if (key.Key == ConsoleKey.Escape)
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

            UpdateInputState();

        }

        private void ManageEscape()
        {
            state.IsPlaying = false;
        }

        private void ManageKeyPress(ConsoleKeyInfo key)
        {
            input.Update(key);
            UpdateInputState();
        }

        private void UpdateInputState()
        {
            state.InputState.Indices = input.Indices;
            state.InputState.Letters = slotMachine.GetLetters(state.InputState.Indices);
        }
    }
}
