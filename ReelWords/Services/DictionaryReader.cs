using ReelWords.Tries;
using System.IO;

namespace ReelWords.Services
{
    public class DictionaryReader
    {
        public Trie Read(string path)
        {
            var trie = new Trie();

            var words = File.ReadLines(path);

            foreach (var word in words)
            {
                trie.Insert(word);
            }

            return trie;
        }
    }
}
