using ReelWords.Gameplay;
using ReelWords.Gameplay.Services;
using System.Threading.Tasks;

namespace ReelWordsTests.Mocks
{
    public class InputServiceMock : IInputService
    {
        private readonly char[] keys;
        private int index;

        public InputServiceMock(char[] keys)
        {
            this.keys = keys;
            index = 0;
        }

        public Task<char> ReadKeyAsync()
        {
            if (index < keys.Length)
            {
                var key = keys[index];
                index++;

                return Task.FromResult(key);
            }

            return Task.FromResult(Input.Escape);
        }
    }
}
