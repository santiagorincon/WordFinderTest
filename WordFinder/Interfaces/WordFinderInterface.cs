using WordFinder.DTO;

namespace WordFinder.Interfaces
{
    public interface WordFinderInterface
    {
        public bool IsMatrixValid(IEnumerable<string> matrix);
        public void SetMatrix(IEnumerable<string> matrix);
        public IEnumerable<string> Find(IEnumerable<string> wordstream);
        public IEnumerable<WordFinderDTO> FindAll(IEnumerable<string> wordstream);
    }
}
