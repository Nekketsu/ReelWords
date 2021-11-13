using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReelWords.Gameplay.Readers
{
    public class ScoresReader
    {
        public async Task<Dictionary<char, int>> ReadAsync(Stream stream)
        {
            var scores = new Dictionary<char, int>();

            using (var streamReader = new StreamReader(stream))
            {
                var line = await streamReader.ReadLineAsync();

                while (line != null)
                {
                    var lineSplit = line.Split();

                    var letter = char.Parse(lineSplit[0].ToUpper());
                    var score = int.Parse(lineSplit[1]);

                    scores.Add(letter, score);

                    line = await streamReader.ReadLineAsync();
                }
            }

            return scores;
        }
    }
}
