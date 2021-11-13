using System.Threading.Tasks;

namespace ReelWords.Gameplay.Services
{
    public interface IInputManager
    {
        Task<char> ReadKeyAsync();
    }
}
