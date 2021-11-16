using ReelWords.Gameplay.Services;
using ReelWords.Gameplay.State;

namespace ReelWordsTests.Mocks
{
    public class DrawServiceMock : IDrawService
    {
        public GameState GameState { get; private set; }

        public void Draw(GameState state)
        {
            GameState = state;
        }
    }
}
