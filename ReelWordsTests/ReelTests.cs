using ReelWords.Gameplay;
using Xunit;

namespace ReelWordsTests
{
    public class ReelTests
    {
        public static object[][] letters = new[]
        {
            new [] { new[] { 'u', 'd', 'x', 'c', 'l', 'a', 'e' } },
            new [] { new[] { 'e', 'y', 'v', 'p', 'q', 'y', 'n' } },
            new [] { new[] { 'i', 'l', 'o', 'w', 'm', 'g', 'n' } },
            new [] { new[] { 'a', 'n', 'd', 'i', 's', 'e', 'v' } },
            new [] { new[] { 'a', 'n', 'j', 'a', 'e', 't', 'b' } },
            new [] { new[] { 'a', 'b', 'w', 't', 'd', 'o', 'h' } }
        };

        [Theory, MemberData(nameof(letters))]
        public void ShowsFirstLetter_WhenInitialized(char[] letters)
        {
            var reel = new Reel(letters);

            Assert.Equal(letters[0], reel.Letter);
        }

        [Theory, MemberData(nameof(letters))]
        public void Next_ShouldMoveThroughAllLetters(char[] letters)
        {
            var reel = new Reel(letters);

            foreach (var letter in letters)
            {
                Assert.Equal(letter, reel.Letter);
                reel.Next();
            }
        }

        [Theory, MemberData(nameof(letters))]
        public void Next_ShouldRestart_WhenVisitedAllLetters(char[] letters)
        {
            var reel = new Reel(letters);

            foreach (var letter in letters)
            {
                reel.Next();
            }

            Assert.Equal(letters[0], reel.Letter);
        }
    }
}
