using System;
using System.Collections.Generic;
using System.Linq;

namespace ReelWords.Tries
{
    public class Trie
    {
        private readonly Node root;

        //private const char rootCharacter = '^'; // Not need of root character
        private const char endOfWordCharacter = '=';

        public Trie()
        {
            root = new Node();
        }

        public bool Search(string word)
        {
            var node = root;

            foreach (var c in word.ToLower())
            {
                if (!node.Children.TryGetValue(c, out var child))
                {
                    return false;
                }

                node = child;
            }

            var isEndOfWord = node.Children.ContainsKey(endOfWordCharacter);

            return isEndOfWord;
        }

        public void Insert(string word)
        {
            var node = root;

            foreach (var c in word.ToLower())
            {
                if (!node.Children.TryGetValue(c, out var child))
                {
                    child = new Node();
                    node.Children.Add(c, child);
                }

                node = child;
            }

            if (!node.Children.ContainsKey(endOfWordCharacter))
            {
                node.Children.Add(endOfWordCharacter, null); // Node not needed for end of word character
            }
        }

        public void Delete(string word)
        {
            var nodesToRemove = GetWordNodes(word.ToLower());

            if (nodesToRemove != null)
            {
                DeleteWordNodes(word, nodesToRemove);
            }
        }

        private Stack<Node> GetWordNodes(string word)
        {
            var wordNodes = new Stack<Node>();

            var node = root;
            wordNodes.Push(node);

            foreach (var c in word)
            {
                if (!node.Children.TryGetValue(c, out var child))
                {
                    return null;
                }

                node = child;
                wordNodes.Push(node);
            }

            var isEndOfWord = node.Children.ContainsKey(endOfWordCharacter);

            return isEndOfWord ? wordNodes : null;
        }

        private void DeleteWordNodes(string word, Stack<Node> nodes)
        {
            var tail = nodes.Pop();
            tail.Children.Remove(endOfWordCharacter);

            foreach (var c in word.Reverse())
            {
                var node = nodes.Pop();
                if (node.Children.Count == 1)
                {
                    node.Children.Remove(c);
                }
            }
        }
    }
}