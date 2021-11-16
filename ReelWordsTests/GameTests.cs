using ReelWords.Gameplay;
using ReelWordsTests.Factories;
using ReelWordsTests.Mocks;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ReelWordsTests
{
    public class GameTests
    {
        SlotMachineFactory slotMachineFactory = new SlotMachineFactory();

        [Fact]
        public async Task ShouldShowInputKeys_WhenPressedEnter()
        {
            var keys = new[] { '1', '2', '3', Input.Enter };

            var slotMachine = slotMachineFactory.GetSlotMachine();
            var inputService = new InputServiceMock(keys);
            var drawService = new DrawServiceMock();
            var game = new Game(slotMachine, inputService, drawService);

            await game.RunAsync();

            Assert.All(keys.SkipLast(1).Zip(drawService.GameState.Input.Slots), item => Assert.Equal(item.First, item.Second.Key));
        }

        [Fact]
        public async Task ShouldShowInputWord_WhenPressedEnter()
        {
            var keys = new[] { '1', '2', '3', Input.Enter };

            var slotMachine = slotMachineFactory.GetSlotMachine();
            var inputService = new InputServiceMock(keys);
            var drawService = new DrawServiceMock();
            var game = new Game(slotMachine, inputService, drawService);

            var expectedWord = new string(slotMachine.GetLetters(new[] { 0, 1, 2 }));

            await game.RunAsync();

            Assert.Equal(expectedWord, drawService.GameState.Word);
        }

        [Fact]
        public async Task ShouldShowValidWord_WhenEnteredValidWord()
        {
            var keys = new[] { '1', '2', '3', Input.Enter };

            var slotMachine = slotMachineFactory.GetSlotMachine();
            var inputService = new InputServiceMock(keys);
            var drawService = new DrawServiceMock();
            var game = new Game(slotMachine, inputService, drawService);

            var word = new string(slotMachine.GetLetters(new[] { 0, 1, 2 }));
            if (!slotMachineFactory.Trie.Search(word)) { slotMachineFactory.Trie.Insert(word); }

            await game.RunAsync();

            Assert.True(drawService.GameState.IsWordValid);
        }

        [Fact]
        public async Task ShouldShowInvalidWord_WhenEnteredInvalidWord()
        {
            var keys = new[] { '1', '2', '3', Input.Enter };

            var slotMachine = slotMachineFactory.GetSlotMachine();
            var inputService = new InputServiceMock(keys);
            var drawService = new DrawServiceMock();
            var game = new Game(slotMachine, inputService, drawService);

            var word = new string(slotMachine.GetLetters(new[] { 0, 1, 2 }));
            if (slotMachineFactory.Trie.Search(word)) { slotMachineFactory.Trie.Delete(word); }

            await game.RunAsync();

            Assert.False(drawService.GameState.IsWordValid);
        }

        [Fact]
        public async Task ShouldShowCorrectScore_WhenEnteredValidWord()
        {
            var keys = new[] { '1', '2', '3', Input.Enter };

            var slotMachine = slotMachineFactory.GetSlotMachine();
            var inputService = new InputServiceMock(keys);
            var drawService = new DrawServiceMock();
            var game = new Game(slotMachine, inputService, drawService);

            var indices = new[] { 0, 1, 2 };
            var word = new string(slotMachine.GetLetters(indices));
            if (!slotMachineFactory.Trie.Search(word)) { slotMachineFactory.Trie.Insert(word); }
            var score = slotMachine.GetScore(indices);

            await game.RunAsync();

            Assert.Equal(score, drawService.GameState.Score);
        }

        [Fact]
        public async Task ShouldShowCorrectScore_WhenEnteredInvalidWord()
        {
            var keys = new[] { '1', '2', '3', Input.Enter };

            var slotMachine = slotMachineFactory.GetSlotMachine();
            var inputService = new InputServiceMock(keys);
            var drawService = new DrawServiceMock();
            var game = new Game(slotMachine, inputService, drawService);

            var word = new string(slotMachine.GetLetters(new[] { 0, 1, 2 }));
            if (slotMachineFactory.Trie.Search(word)) { slotMachineFactory.Trie.Delete(word); }

            await game.RunAsync();

            Assert.Equal(0, drawService.GameState.Score);
        }

        [Fact]
        public async Task ShouldShowCorrectWordPoints_WhenEnteredValidWord()
        {
            var keys = new[] { '1', '2', '3', Input.Enter };

            var slotMachine = slotMachineFactory.GetSlotMachine();
            var inputService = new InputServiceMock(keys);
            var drawService = new DrawServiceMock();
            var game = new Game(slotMachine, inputService, drawService);

            var indices = new[] { 0, 1, 2 };
            var word = new string(slotMachine.GetLetters(indices));
            if (!slotMachineFactory.Trie.Search(word)) { slotMachineFactory.Trie.Insert(word); }
            var points = slotMachine.GetScore(indices);

            await game.RunAsync();

            Assert.Equal(points, drawService.GameState.WordPoints);
        }

        [Fact]
        public async Task ShouldClearWordValidity_WhenTypingACharacter()
        {
            var keys = new[] { '1', '2', '3', Input.Enter, '1' };

            var slotMachine = slotMachineFactory.GetSlotMachine();
            var inputService = new InputServiceMock(keys);
            var drawService = new DrawServiceMock();
            var game = new Game(slotMachine, inputService, drawService);

            await game.RunAsync();

            Assert.Null(drawService.GameState.IsWordValid);
        }
    }
}
