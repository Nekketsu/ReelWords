using ReelWords.Tries;
using System.IO;
using System.Threading.Tasks;

namespace ReelWords.Gameplay.Readers
{
    public class DictionaryReader
    {
        public async Task<Trie> ReadAsync(Stream stream)
        {
            var trie = new Trie();

            using (var streamReader = new StreamReader(stream))
            {
                var word = await streamReader.ReadLineAsync();

                while (word != null)
                {
                    trie.Insert(word);
                    word = await streamReader.ReadLineAsync();
                }
            }

            return trie;
        }
    }
}
