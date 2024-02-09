namespace WordFinder.Models
{
    public class WordQuery
    {
        public int Index { get; set; }
        public int CharCount { get; set; }
        public bool IsComplete { get; set; }
        public char lastCharFound { get; set; }
    }
}
