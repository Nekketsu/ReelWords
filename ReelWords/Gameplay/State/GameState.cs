namespace ReelWords.Gameplay.State
{
    public class GameState
    {
        public bool IsPlaying { get; set; }

        public int Score { get; set; }
        public string Word { get; set; }
        public bool? IsWordValid { get; set; }
        public int WordPoints { get; set; }

        public SlotMachineState SlotMachineState { get; set; } = new SlotMachineState();
        public InputState InputState { get; set; } = new InputState();
    }
}
