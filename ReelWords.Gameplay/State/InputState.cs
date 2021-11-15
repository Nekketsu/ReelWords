using System;

namespace ReelWords.Gameplay.State
{
    public class InputState
    {
        public SlotState[] Slots { get; set; } = Array.Empty<SlotState>();
    }
}
