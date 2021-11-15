using ReelWords.Gameplay;
using ReelWords.Gameplay.Services;
using System;
using System.Threading.Tasks;

namespace ReelWords.Services
{
    public class InputService : IInputService
    {
        public Task<char> ReadKeyAsync()
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                return Task.FromResult(Input.Backspace);
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return Task.FromResult(Input.Enter);
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                return Task.FromResult(Input.Backspace);
            }

            return Task.FromResult(key.KeyChar);
        }
    }
}
