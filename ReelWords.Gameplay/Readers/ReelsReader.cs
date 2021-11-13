using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReelWords.Gameplay.Readers
{
    public class ReelsReader
    {
        public async Task<Reel[]> ReadAsync(Stream stream)
        {
            var reels = new List<Reel>();

            using (var streamReader = new StreamReader(stream))
            {
                var line = await streamReader.ReadLineAsync();

                while (line != null)
                {
                    var reel = ParseReel(line);
                    reels.Add(reel);
                    line = await streamReader.ReadLineAsync();
                }
            }

            return reels.ToArray();
        }

        private Reel ParseReel(string line)
        {
            var letters = line.ToUpper().Split().Select(char.Parse).ToArray();

            var reel = new Reel(letters);

            return reel;
        }
    }
}
