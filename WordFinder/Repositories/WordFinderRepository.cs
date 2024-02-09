using System.Linq;
using System.Text.RegularExpressions;
using WordFinder.DTO;
using WordFinder.Interfaces;
using WordFinder.Models;

namespace WordFinder.Repositories
{
    public class WordFinderRepository : WordFinderInterface
    {
        private IEnumerable<string> _matrix;

        public WordFinderRepository()
        {
            _matrix = new List<string>();
        }

        public void SetMatrix(IEnumerable<string> matrix)
        {
            _matrix = matrix;
        }

        public IEnumerable<WordFinderDTO> FindAll(IEnumerable<string> wordstream)
        {
            wordstream = wordstream.Distinct().ToList();
            List<WordResult> Result = wordstream.Select(word => new WordResult
            {
                Word = word,
                TotalCount = WordCount(word)
            }).ToList();

            return Result.Select(r => new WordFinderDTO { 
                Word = r.Word,
                Count = r.TotalCount
            }).OrderByDescending(r => r.Count).ToList();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            return FindAll(wordstream).Where(r => r.Count > 0).Select(r => r.Word).Take(10).ToList();
        }

        private int WordCount(string word)
        {
            int count = 0;
            word = word.ToUpper();
            List<WordQuery> queries = new List<WordQuery>();
            List<WordQuery> completedQueries = new List<WordQuery>();
            char firstLetter = word[0];
            foreach (string rowOriginal in _matrix)
            {
                string row = rowOriginal.ToUpper();
                // Adding horizontal coincidences
                count += Regex.Matches(row, word).Count;

                // Validating existing queries
                queries = queries.Where(query => word[query.CharCount] == row[query.Index]).Select(query => {
                    query.lastCharFound = word[query.CharCount];
                    query.IsComplete = (query.CharCount + 1 == word.Length);
                    query.CharCount += 1;
                    return query;
                }).ToList();

                // Adding completed to completedQueries array
                completedQueries.AddRange(queries.Where(q => q.IsComplete).ToList());
                // Removing completed queries from queries array
                queries = queries.Where(q => !q.IsComplete).ToList();

                // Searching new coincidences from the first letter
                List<WordQuery> firstLetterCoincidences = row.Select((ch, i) => new WordQuery
                {
                    CharCount = 1,
                    Index = i,
                    lastCharFound = ch,
                    IsComplete = false
                }).Where(wq => wq.lastCharFound == firstLetter).ToList();

                queries.AddRange(firstLetterCoincidences);
            }

            count += completedQueries.Count();
            return count;
        }

        public bool IsMatrixValid(IEnumerable<string> matrix)
        {
            /* This matrix validation include:
             * - matrix is not empty
             * - matrix size does not exceed 64x64
             * - all strings contain the same number of characters
            */
            if (matrix == null || !matrix.Any() || matrix.Count() > 64 || matrix.Where(s => s.Length > 64).Any() || matrix.Select(s => s.Length).Distinct().Count() > 1)
            {
                return false;
            }
            return true;
        }
    }
}
