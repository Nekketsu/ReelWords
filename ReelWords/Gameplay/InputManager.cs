using System;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Gameplay
{
    public class InputManager
    {
        const char firstLetter = '1';

        private readonly List<int> input;
        private readonly bool[] isIndexSelected;

        public int Length { get; }
        public int[] Indices => input.ToArray();


        public InputManager(int length)
        {
            input = new List<int>(length);
            isIndexSelected = new bool[length];

            Length = length;
        }

        public void Update(ConsoleKeyInfo key)
        {
            if (key.KeyChar >= firstLetter && key.KeyChar <= firstLetter + Length - 1)
            {
                AddKey(key);
            }
            else if (key.Key == ConsoleKey.Backspace)
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
            return $"{string.Join(' ', Indices.Select(index => (char)(index + firstLetter)))}";
        }


        private void AddKey(ConsoleKeyInfo key)
        {
            var index = key.KeyChar - firstLetter;

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
