using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using TestApplication.Classes;
using TestApplication.Models;
using TestApplication.Repository;

namespace TestApplication.Services
{
    public class MainService : IMainService
    {
        private readonly MainConfig _mainConfig;
        private readonly IRepository<Suggest> _suggestRepository;
        private readonly ILogger _logger;

        /// <summary>
        /// DI
        /// </summary>
        /// <param name="mainConfig"></param>
        public MainService(IOptions<MainConfig> mainConfig, IRepository<Suggest> suggestRepository, ILogger<MainService> logger)
        {
            _mainConfig = mainConfig.Value;
            _suggestRepository = suggestRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Вернет подсказки для пользовательского ввода (с дефолтным количеством)
        /// </summary>
        /// <param name="input">Строка по которой ведется поиск</param>
        /// <param name="maxCount">Выводимое количество подсказок</param>
        public IEnumerable<Suggest> Suggest(SuggestRequest input)
        {
            if (input == null)
                throw new NullReferenceException("SuggestRequest is null");

            return Suggest(input, _mainConfig.MaxSuggestCount);
        }

        /// <summary>
        /// Вернет подсказки для пользовательского ввода (с переданным количеством)
        /// </summary>
        /// <param name="input">Строка по которой ведется поиск</param>
        /// <param name="suggestCount">Выводимое количество подсказок</param>
        /// <returns></returns>
        private IEnumerable<Suggest> Suggest(SuggestRequest input, int suggestCount)
        {
            _logger.LogInformation($"Query={input.Query}");

            IEnumerable<Suggest> result = null;
            if (!string.IsNullOrEmpty(input.Query))
                result = _suggestRepository.FindByInput(input.Query, suggestCount);
            else
                result = new List<Suggest>();

            return result;
        }
    }
}
