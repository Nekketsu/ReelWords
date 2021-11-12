using System.Collections.Generic;
using System.IO;

namespace ReelWords.Services
{
    public class ScoresReader
    {
        public Dictionary<char, int> Read(string path)
        {
            var scores = new Dictionary<char, int>();

            var lines = File.ReadLines(path);

            foreach (var line in lines)
            {
                var lineSplit = line.Split();

                var letter = char.Parse(lineSplit[0]);
                var score = int.Parse(lineSplit[1]);

                scores.Add(letter, score);
            }

            return scores;
        }
    }
}
