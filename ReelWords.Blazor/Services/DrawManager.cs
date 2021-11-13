using ReelWords.Gameplay.Services;
using ReelWords.Gameplay.State;

namespace ReelWords.Blazor.Services
{
    public class DrawManager : IDrawManager
    {
        private Pages.Index gamePage;

        public DrawManager(Pages.Index gamePage)
        {
            this.gamePage = gamePage;
        }

        public void Draw(GameState state)
        {
            gamePage.Reels = state.SlotMachineState.Letters
                .Zip(state.SlotMachineState.Points).Select((items, index) => new Models.Reel
                {
                    Letter = items.First,
                    Key = (char)(Gameplay.Input.FirstLetter + index),
                    Points = items.Second
                }).ToArray();

            foreach (var index in state.InputState.Indices)
            {
                gamePage.Reels[index].IsSelected = true;
            }


            gamePage.Input = state.InputState.Letters.ToArray();

            gamePage.Score = state.Score;
            gamePage.Word = state.Word;
            gamePage.IsWordValid = state.IsWordValid;
            gamePage.WordPoints = state.WordPoints;

            gamePage.ForceStateHasChanged();
        }
    }
}
