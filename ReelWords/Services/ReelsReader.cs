using ReelWords.Gameplay;
using System.IO;
using System.Linq;

namespace ReelWords.Services
{
    public class ReelsReader
    {
        public Reel[] Read(string path)
        {
            var lines = File.ReadLines(path);
            var reels = lines.Select(ParseReel).ToArray();

            return reels;
        }

        private Reel ParseReel(string line)
        {
            var letters = line.Split().Select(char.Parse).ToArray();

            var reel = new Reel(letters);

            return reel;
        }
    }
}
