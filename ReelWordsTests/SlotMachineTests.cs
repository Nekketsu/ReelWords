using ReelWordsTests.Factories;
using System.Linq;
using Xunit;

namespace ReelWordsTests
{
    public class SlotMachineTests
    {
        SlotMachineFactory slotMachineFactory = new SlotMachineFactory();

        [Fact]
        public void Length_ShouldReturnNumberOfReels()
        {
            var slotMachine = slotMachineFactory.GetSlotMachine();

            Assert.Equal(slotMachineFactory.Letters.Length, slotMachine.Length);
        }

        [Fact]
        public void Letters_ShouldReturnCurrentSelectedLetters()
        {
            var slotMachine = slotMachineFactory.GetSlotMachine();

            var letters = slotMachineFactory.Letters.Select(letter => letter[0]).ToArray();

            Assert.All(letters.Zip(slotMachine.Letters), item => Assert.Equal(item.First, item.Second));
        }

        [Fact]
        public void Points_ShouldHaveExpectedValues()
        {
            var slotMachine = slotMachineFactory.GetSlotMachine();

            var points = slotMachineFactory.Letters.Select(letter => slotMachineFactory.Scores[letter[0]]).ToArray();

            Assert.All(points.Zip(slotMachine.Points), item => Assert.Equal(item.First, item.Second));
        }

        [Theory]
        [InlineData(new int[0], new[] { 'u', 'e', 'i', 'a', 'a', 'a' })]
        [InlineData(new[] { 0 }, new[] { 'd', 'e', 'i', 'a', 'a', 'a' })]
        [InlineData(new[] { 2, 3, 5 }, new[] { 'u', 'e', 'l', 'n', 'a', 'b' })]
        [InlineData(new[] { 0, 1, 2, 3, 4, 5 }, new[] { 'd', 'y', 'l', 'n', 'n', 'b' })]
        public void Next_ShouldMoveNextIndices(int[] indices, char[] expectedLetters)
        {
            var slotMachine = slotMachineFactory.GetSlotMachine();

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
            var slotMachine = slotMachineFactory.GetSlotMachine();

            var score = slotMachine.GetScore(indices);

            Assert.Equal(expectedScore, score);
        }

        [Theory]
        [InlineData(new int[0], new char[0])]
        [InlineData(new[] { 0 }, new[] { 'u' })]
        [InlineData(new[] { 2, 3, 5 }, new[] { 'i', 'a', 'a', })]
        [InlineData(new[] { 0, 1, 2, 3, 4, 5 }, new char[] { 'u', 'e', 'i', 'a', 'a', 'a' })]
        public void GetLetters_ShouldReturnLettersInSelectedIndices(int[] indices, char[] expectedLetters)
        {
            var slotMachine = slotMachineFactory.GetSlotMachine();

            var letters = slotMachine.GetLetters(indices);

            Assert.All(expectedLetters.Zip(letters), item => Assert.Equal(item.First, item.Second));
        }

        [Theory]
        [InlineData("river", true)]
        [InlineData("dragon", false)]
        [InlineData("dog", true)]
        [InlineData("plane", false)]
        [InlineData("cat", true)]
        public void IsValid_ShouldReturnWhetherAWordIsValidOrNot(string word, bool expectedValue)
        {
            var slotMachine = slotMachineFactory.GetSlotMachine();

            var isValid = slotMachine.IsValid(word);

            Assert.Equal(expectedValue, isValid);
        }
    }
}
