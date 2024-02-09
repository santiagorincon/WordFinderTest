using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WordFinder.DTO;
using WordFinder.Interfaces;

namespace WordFinder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordFinderController : ControllerBase
    {
        private readonly WordFinderInterface _wordFinderRepo;

        public WordFinderController(WordFinderInterface wordFinderRepo)
        {
            _wordFinderRepo = wordFinderRepo;
        }

        [HttpPost("findAll")]
        public IActionResult FindAll([FromBody] WordFinderRequestDTO wordFinderRequest)
        {
            using (var scope = new TransactionScope())
            {
                if (!_wordFinderRepo.IsMatrixValid(wordFinderRequest.matrix))
                {
                    return BadRequest("Matrix is not valid");
                }
                _wordFinderRepo.SetMatrix(wordFinderRequest.matrix);
                IEnumerable<WordFinderDTO> coincidences = _wordFinderRepo.FindAll(wordFinderRequest.wordstream);
                scope.Complete();
                return Ok(coincidences);
            }
        }

        [HttpPost("findTop10")]
        public IActionResult FindTop10([FromBody] WordFinderRequestDTO wordFinderRequest)
        {
            using (var scope = new TransactionScope())
            {
                if (!_wordFinderRepo.IsMatrixValid(wordFinderRequest.matrix))
                {
                    return BadRequest("Matrix is not valid");
                }
                _wordFinderRepo.SetMatrix(wordFinderRequest.matrix);
                IEnumerable<string> coincidences = _wordFinderRepo.Find(wordFinderRequest.wordstream);
                scope.Complete();
                return Ok(coincidences);
            }
        }
    }
}