using System.Threading.Tasks;

namespace ReelWords.Gameplay.Services
{
    public interface IInputService
    {
        Task<char> ReadKeyAsync();
    }
}
