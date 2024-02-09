using Microsoft.AspNetCore.Mvc;
using WordFinder.Controllers;
using WordFinder.DTO;
using WordFinder.Interfaces;
using WordFinder.Repositories;

namespace UnitTests
{
    public class WordFinderTest
    {
        private readonly WordFinderController _controller;
        private readonly WordFinderInterface _service;
        private WordFinderRequestDTO case1;
        private WordFinderRequestDTO case2;
        private WordFinderRequestDTO case3;

        public WordFinderTest()
        {
            _service = new WordFinderRepository();
            _controller = new WordFinderController(_service);
        }

        [SetUp]
        public void Setup()
        {
            case1 = new WordFinderRequestDTO {
                matrix = new List<string>{"abcdc","fgwio","chill","pqnsd","uvdxy"},
                wordstream = new List<string> { "cold", "snow", "chill", "wind" }
            };

            case2 = new WordFinderRequestDTO
            {
                matrix = new List<string> { "pugsvzyyenujbjelxrsy", "hcoldqoirstandyvoxjf", "yjnnjcgraaopbswgscfk", "iaaqxhspiorglniilism", "hggfdilsnowpcoldlzno", "oglxslxvhxxrbwimpyoy", "msacqlxwyvuaiaesbfws", "enxcloudvzwindmvrpys", "zozihiayzgdicgocqwjl", "zwjrncoldzdghmdgcizz", "bsiarongfpsrrksxongy", "epeiqlkwmckajdarltvz", "akhniduqohwidgnfdegc", "cvacxiqziifnuedirrlh", "hkcwhogwiltxkzyftcxz", "rzlxeqtillzhthmwqyga", "izotrlynhgtwudawujmr", "ghurqhudwtmenzskawcc", "kgdcrxgqkabveevhgksw", "hjkvykmgjwukwzvfcfwb" },
                wordstream = new List<string> { "cold", "snow", "rain", "wind", "chill", "cloud", "home", "beach", "sand", "winter", "fall", "spring" }
            };

            case3 = new WordFinderRequestDTO
            {
                matrix = new List<string> { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" },
                wordstream = new List<string> ()
            };
        }

        [Test]
        public void FindAll_SmallWordtream()
        {
            // Act
            var result = _controller.FindAll(case1);

            // Assert
            // Type validation
            Assert.IsInstanceOf<OkObjectResult>(result, "Service return OK");

            // Null validation for response
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult, "Response is not null");

            // Type validation for retorned data
            var model = okObjectResult.Value as IEnumerable<WordFinderDTO>;
            Assert.NotNull(model, "Response is in expected type");

            // It validates the retorned data with expected data
            Assert.That(model.Count(), Is.EqualTo(case1.wordstream.Count()), "Response include same amount of words as input wordstream");
            Assert.That(model.Where(m => m.Word == "cold").Select(m => m.Count).FirstOrDefault(), Is.EqualTo(1), "Word 'cold' must appear once");
        }

        [Test]
        public void FindAll_BigWordstream()
        {
            // Act
            var result = _controller.FindAll(case2);

            // Assert
            // Type validation
            Assert.IsInstanceOf<OkObjectResult>(result, "Service return OK");

            // Null validation for response
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult, "Response is not null");

            // Type validation for retorned data
            var model = okObjectResult.Value as IEnumerable<WordFinderDTO>;
            Assert.NotNull(model, "Response is in expected type");

            // It validates the retorned data with expected data
            Assert.That(model.Count(), Is.EqualTo(case2.wordstream.Count()), "Response include same amount of words as input wordstream");
            Assert.That(model.Where(m => m.Word == "cold").Select(m => m.Count).FirstOrDefault(), Is.EqualTo(5), "Word 'cold' must appear 5 times");
        }

        [Test]
        public void FindTop10_BigWordstream()
        {
            // Act
            var result = _controller.FindTop10(case2);

            // Assert
            // Type validation
            Assert.IsInstanceOf<OkObjectResult>(result, "Service return OK");

            // Null validation for response
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult, "Response is not null");

            // Type validation for retorned data
            var model = okObjectResult.Value as IEnumerable<string>;
            Assert.NotNull(model, "Response is in expected type");

            // It validates the retorned data with expected data
            Assert.That(model.Count(), Is.EqualTo(10), "Response include 10 top found words");
            Assert.That(model.FirstOrDefault(), Is.EqualTo("cold"), "Word 'cold' must appear first");
        }

        [Test]
        public void FindTop10_EmptyWordstream()
        {
            // Act
            var result = _controller.FindTop10(case3);

            // Assert
            // Type validation
            Assert.IsInstanceOf<OkObjectResult>(result, "Service return OK");

            // Null validation for response
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult, "Response is not null");

            // Type validation for retorned data
            var model = okObjectResult.Value as IEnumerable<string>;
            Assert.NotNull(model, "Response is in expected type");

            // It validates the retorned data with expected data
            Assert.That(model.Count(), Is.EqualTo(0), "Response is an empty array");
        }
    }
}