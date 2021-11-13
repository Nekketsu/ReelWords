using System.Collections.Generic;

namespace ReelWords.Tries
{
    public class Node
    {
        public Dictionary<char, Node> Children { get; set; }

        public Node()
        {
            Children = new Dictionary<char, Node>();
        }
    }
}
