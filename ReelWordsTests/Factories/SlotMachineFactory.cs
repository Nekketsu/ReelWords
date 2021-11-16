using ReelWords.Gameplay;
using ReelWords.Tries;
using System.Collections.Generic;
using System.Linq;

namespace ReelWordsTests.Factories
{
    public class SlotMachineFactory
    {
        public char[][] Letters { get; }
        public Dictionary<char, int> Scores { get; }
        public Trie Trie { get; }

        public SlotMachineFactory()
        {
            Letters = new[]
            {
                new[] { 'u', 'd', 'x', 'c', 'l', 'a', 'e' },
                new[] { 'e', 'y', 'v', 'p', 'q', 'y', 'n' },
                new[] { 'i', 'l', 'o', 'w', 'm', 'g', 'n' },
                new[] { 'a', 'n', 'd', 'i', 's', 'e', 'v' },
                new[] { 'a', 'n', 'j', 'a', 'e', 't', 'b' },
                new[] { 'a', 'b', 'w', 't', 'd', 'o', 'h' }
            };

            Scores = new Dictionary<char, int>
            {
                ['a'] = 1,
                ['b'] = 3,
                ['c'] = 3,
                ['d'] = 2,
                ['e'] = 1,
                ['f'] = 4,
                ['g'] = 2,
                ['h'] = 4,
                ['i'] = 1,
                ['j'] = 8,
                ['k'] = 5,
                ['l'] = 1,
                ['m'] = 3,
                ['n'] = 1,
                ['o'] = 1,
                ['p'] = 3,
                ['q'] = 1,
                ['r'] = 1,
                ['s'] = 1,
                ['t'] = 1,
                ['u'] = 1,
                ['v'] = 4,
                ['w'] = 4,
                ['x'] = 8,
                ['y'] = 4,
                ['z'] = 10
            };

            Trie = new Trie();
            Trie.Insert("uia");
            Trie.Insert("river");
            Trie.Insert("dog");
            Trie.Insert("cat");
        }

        public SlotMachine GetSlotMachine()
        {
            var reels = Letters.Select(reelLetters => new Reel(reelLetters)).ToArray();
            var slotMachine = new SlotMachine(reels, Scores, Trie);

            return slotMachine;
        }
    }
}
