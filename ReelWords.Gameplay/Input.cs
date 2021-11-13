using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Gameplay
{
    public class Input
    {
        public const char Backspace = (char)8;
        public const char Enter = (char)13;
        public const char Escape = (char)27;

        public const char FirstLetter = '1';

        private readonly List<int> input;
        private readonly bool[] isIndexSelected;

        public int Length { get; }
        public int[] Indices => input.ToArray();


        public Input(int length)
        {
            input = new List<int>(length);
            isIndexSelected = new bool[length];

            Length = length;
        }

        public void Update(char key)
        {
            const char backspace = (char)8;

            if (key >= FirstLetter && key <= FirstLetter + Length - 1)
            {
                AddKey(key);
            }
            else if (key == backspace)
            {
                RemoveKey();
            }
        }

        public void Clear()
        {
            input.Clear();
            Clear(isIndexSelected);
        }

        public override string ToString()
        {
            return $"{string.Join(' ', Indices.Select(index => (char)(index + FirstLetter)))}";
        }


        private void AddKey(char key)
        {
            var index = key - FirstLetter;

            if (!isIndexSelected[index])
            {
                input.Add(index);
                isIndexSelected[index] = true;
            }
        }

        private void RemoveKey()
        {
            if (input.Count > 0)
            {
                var index = input.Last();

                isIndexSelected[index] = false;
                input.RemoveAt(input.Count - 1);
            }
        }

        private void Clear(bool[] usedKeys)
        {
            for (var i = 0; i < usedKeys.Length; i++)
            {
                usedKeys[i] = false;
            }
        }
    }
}
