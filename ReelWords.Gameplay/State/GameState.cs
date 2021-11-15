using System;

namespace ReelWords.Gameplay.State
{
    public class GameState
    {
        public bool IsPlaying { get; set; }

        public int Score { get; set; }
        public string Word { get; set; }
        public bool? IsWordValid { get; set; }
        public int WordPoints { get; set; }

        public SlotMachineState SlotMachine = new SlotMachineState();
        public InputState Input { get; set; } = new InputState();
    }
}
