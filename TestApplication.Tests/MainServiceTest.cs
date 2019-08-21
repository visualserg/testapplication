using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApplication.Classes;
using TestApplication.Models;
using TestApplication.Repository;
using TestApplication.Services;
using Xunit;

namespace TestApplication.Tests
{
    public class MainServiceTest
    {
        private readonly IOptions<MainConfig> _mainConfig;
        private List<Suggest> _repositoryResult;
        private ILogger<MainService> _logger;

        public MainServiceTest()
        {
            _mainConfig = Options.Create(new MainConfig());
            _repositoryResult = new List<Suggest>();
            _logger = new Mock<ILogger<MainService>>().Object;
        }

        [Fact]
        public void ErrorIfSuggestNull()
        {
            var mockRepository = new Mock<IRepository<Suggest>>();

            mockRepository.Setup(x => x.FindByInput(It.IsAny<string>(), It.IsAny<int>())).Returns(_repositoryResult);

            var mainService = new MainService(_mainConfig, mockRepository.Object, _logger);

            Assert.Throws<NullReferenceException>(() => mainService.Suggest(null));
        }

        [Fact]
        public void IsQueryEmpty()
        {
            var mockRepository = new Mock<IRepository<Suggest>>();
            mockRepository.Setup(x => x.FindByInput(It.IsAny<string>(), It.IsAny<int>())).Returns(_repositoryResult);

            var mainService = new MainService(_mainConfig, mockRepository.Object, _logger);
            var result = mainService.Suggest(new SuggestRequest { Query = "" });
            Assert.Empty(result);
        }

        [Theory]
        [InlineData("При")]
        public void Contains(string input)
        {
            var mockRepository = new Mock<IRepository<Suggest>>();
            _repositoryResult.Add(new Suggest { Text = "Привет" });
            _repositoryResult.Add(new Suggest { Text = "Прикус" });

            mockRepository.Setup(x => x.FindByInput(It.IsAny<string>(), It.IsAny<int>())).Returns(_repositoryResult);

            var mainService = new MainService(_mainConfig, mockRepository.Object, _logger);
            var result = mainService.Suggest(new SuggestRequest { Query = input });

            Assert.All(result, item => Assert.Contains(input, item.Text));
        }
    }
}
