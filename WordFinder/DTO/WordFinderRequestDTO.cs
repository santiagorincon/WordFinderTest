namespace WordFinder.DTO
{
    public class WordFinderRequestDTO
    {
        public IEnumerable<string> matrix { get; set; }
        public IEnumerable<string> wordstream { get; set; }
    }
}
