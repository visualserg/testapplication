using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Classes;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestController : ControllerBase
    {
        private readonly IMainService _mainService;

        /// <summary>
        /// DI
        /// </summary>
        /// <param name="mainService"></param>
        public SuggestController(IMainService mainService)
        {
            _mainService = mainService;
        }

        /// <summary>
        /// Вывод подсказок
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // GET api/values
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<string>> Suggest(SuggestRequest input)
        {
            var result = _mainService.Suggest(input);
            return result.Select(c => c.Text).ToList();
        }
    }
}
