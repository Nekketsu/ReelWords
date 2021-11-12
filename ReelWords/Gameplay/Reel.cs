using System;

namespace ReelWords.Gameplay
{
    public class Reel
    {
        private readonly char[] letters;
        private int index;

        private readonly Random random;

        public Reel(char[] letters)
        {
            this.letters = letters;
            index = 0;

            random = new Random();
        }

        public char Letter => letters[index];

        public void Shuffle()
        {
            index = random.Next(letters.Length);
        }

        public void Next()
        {
            index = (index + 1) % letters.Length;
        }

        public override string ToString()
        {
            return Letter.ToString();
        }
    }
}
