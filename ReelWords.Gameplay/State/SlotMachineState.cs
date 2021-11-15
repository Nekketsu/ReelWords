using System;

namespace ReelWords.Gameplay.State
{
    public class SlotMachineState
    {
        public SlotState[] Slots { get; set; } = Array.Empty<SlotState>();
    }
}
