using ReelWords.Gameplay;
using ReelWords.Tries;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ReelWordsTests
{
    public class SlotMachineTests
    {
        private char[][] letters;
        private Dictionary<char, int> scores;
        private Trie trie;

        public SlotMachineTests()
        {
            letters = new[]
            {
                new[] { 'u', 'd', 'x', 'c', 'l', 'a', 'e' },
                new[] { 'e', 'y', 'v', 'p', 'q', 'y', 'n' },
                new[] { 'i', 'l', 'o', 'w', 'm', 'g', 'n' },
                new[] { 'a', 'n', 'd', 'i', 's', 'e', 'v' },
                new[] { 'a', 'n', 'j', 'a', 'e', 't', 'b' },
                new[] { 'a', 'b', 'w', 't', 'd', 'o', 'h' }
            };

            scores = new Dictionary<char, int>
            {
                ['a'] = 1, ['b'] = 3, ['c'] = 3, ['d'] = 2, ['e'] = 1,
                ['f'] = 4, ['g'] = 2, ['h'] = 4, ['i'] = 1, ['j'] = 8,
                ['k'] = 5, ['l'] = 1, ['m'] = 3, ['n'] = 1, ['o'] = 1,
                ['p'] = 3, ['q'] = 1, ['r'] = 1, ['s'] = 1, ['t'] = 1,
                ['u'] = 1, ['v'] = 4, ['w'] = 4, ['x'] = 8, ['y'] = 4,
                ['z'] = 10
            };

            trie = new Trie();
            trie.Insert("river");
            trie.Insert("dog");
            trie.Insert("cat");
        }

        private SlotMachine GetSlotMachine()
        {
            var reels = letters.Select(reelLetters => new Reel(reelLetters)).ToArray();
            var slotMachine = new SlotMachine(reels, scores, trie);

            return slotMachine;
        }

        [Fact]
        public void Length_ShouldReturnNumberOfReels()
        {
            var slotMachine = GetSlotMachine();

            Assert.Equal(letters.Length, slotMachine.Length);
        }

        [Fact]
        public void Letters_ShouldReturnSelectedLetters()
        {
            var slotMachine = GetSlotMachine();

            var letters = this.letters.Select(letter => letter[0]).ToArray();

            Assert.All(letters.Zip(slotMachine.Letters), item => Assert.Equal(item.First, item.Second));
        }

        [Fact]
        public void Points_ShouldHaveExpectedValues()
        {
            var slotMachine = GetSlotMachine();

            var points = letters.Select(letter => scores[letter[0]]).ToArray();

            Assert.All(points.Zip(slotMachine.Points), item => Assert.Equal(item.First, item.Second));
        }

        [Theory]
        [InlineData(new int[0], new [] { 'u', 'e', 'i', 'a', 'a', 'a' })]
        [InlineData(new [] { 0 }, new[] { 'd', 'e', 'i', 'a', 'a', 'a' })]
        [InlineData(new[] { 2, 3, 5 }, new[] { 'u', 'e', 'l', 'n', 'a', 'b' })]
        [InlineData(new [] { 0, 1, 2, 3, 4, 5 }, new [] { 'd', 'y', 'l', 'n', 'n', 'b' })]
        public void Next_ShouldMoveNextIndices(int[] indices, char[] expectedLetters)
        {
            var slotMachine = GetSlotMachine();

            slotMachine.Next(indices);

            Assert.All(expectedLetters.Zip(slotMachine.Letters), item => Assert.Equal(item.First, item.Second));
        }

        [Theory]
        [InlineData(new int[0], 0)]
        [InlineData(new[] { 0 }, 1)]
        [InlineData(new[] { 2, 3, 5 }, 3)]
        [InlineData(new[] { 0, 1, 2, 3, 4, 5 }, 6)]
        public void GetScore_ShouldCalculteScoreOfSelectedIndices(int[] indices, int expectedScore)
        {
            var slotMachine = GetSlotMachine();

            var score = slotMachine.GetScore(indices);

            Assert.Equal(expectedScore, score);
        }

        [Theory]
        [InlineData(new int[0], new char[0])]
        [InlineData(new[] { 0 }, new[] { 'u' })]
        [InlineData(new[] { 2, 3, 5 }, new[] { 'e', 'a', 'a', })]
        [InlineData(new[] { 0, 1, 2, 3, 4, 5 }, new char[] { 'u', 'e', 'i', 'e', 'a', 'a' })]
        public void GetLetters_ShouldReturnLettersInSelectedIndices(int[] indices, char[] expectedLetters)
        {
            var slotMachine = GetSlotMachine();

            var letters = slotMachine.GetLetters(indices);

            Assert.All(expectedLetters.Zip(letters), item => Assert.Equal(item.First, item.Second));
        }

        [Theory]
        [InlineData("river", true)]
        [InlineData("dragon", false)]
        [InlineData("dog", true)]
        [InlineData("plane", false)]
        [InlineData("cat", true)]
        public void IsValid_ShouldReturnWheterAWordIsValidOrNot(string word, bool expectedValue)
        {
            var slotMachine = GetSlotMachine();

            var isValid = slotMachine.IsValid(word);

            Assert.Equal(expectedValue, isValid);
        }
    }
}
