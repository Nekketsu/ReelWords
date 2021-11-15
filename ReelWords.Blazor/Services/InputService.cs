using ReelWords.Gameplay.Services;

namespace ReelWords.Blazor.Services
{
    public class InputService : IInputService
    {
        private Pages.Index gamePage;
        private TaskCompletionSource<char>? taskCompletionSource;

        public InputService(Pages.Index gamePage)
        {
            this.gamePage = gamePage;

            this.gamePage.KeyPressed += GamePage_KeyPressed;
        }

        private void GamePage_KeyPressed(object? sender, char key)
        {
            taskCompletionSource?.SetResult(key);
        }

        public async Task<char> ReadKeyAsync()
        {
            taskCompletionSource = new TaskCompletionSource<char>();

            return await taskCompletionSource.Task;
        }
    }
}
