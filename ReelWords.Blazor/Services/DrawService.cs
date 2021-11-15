using ReelWords.Gameplay.Services;
using ReelWords.Gameplay.State;

namespace ReelWords.Blazor.Services
{
    public class DrawService : IDrawService
    {
        private Pages.Index gamePage;

        public DrawService(Pages.Index gamePage)
        {
            this.gamePage = gamePage;
        }

        public void Draw(GameState state)
        {
            gamePage.GameState = state;

            gamePage.ForceStateHasChanged();
        }
    }
}
