using ReelWords.Tries;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Gameplay
{
    public class SlotMachine
    {
        private readonly Reel[] reels;
        private readonly Dictionary<char, int> scores;
        private readonly Trie dictionary;

        public int Length => reels.Length;
        public char[] Letters => reels.Select(reel => reel.Letter).ToArray();
        public int[] Points => Letters.Select(letter => scores[letter]).ToArray();

        public SlotMachine(Reel[] reels, Dictionary<char, int> scores, Trie dictionary)
        {
            this.reels = reels;
            this.scores = scores;
            this.dictionary = dictionary;
        }

        public void Shuffle()
        {
            foreach (var reel in reels)
            {
                reel.Shuffle();
            }
        }

        public void Next(int[] indices)
        {
            foreach (var index in indices)
            {
                reels[index].Next();
            }
        }

        public int GetScore(int[] indices)
        {
            var score = indices.Select(index => Points[index]).Sum();
            return score;
        }

        public char[] GetLetters(int[] indices)
        {
            var letters = Letters;
            var word = indices.Select(index => letters[index]).ToArray();

            return word;
        }

        public bool IsValid(string word)
        {
            return dictionary.Search(word);
        }

        public override string ToString()
        {
            return string.Join(' ', Letters);
        }
    }
}
