using GetecTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetecTask.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        [HttpGet("[action]")]
        public WordPresentation WordyFormatter(string input)
        {
            var formatter = new WordyFormatter();
            return formatter.Parse(input);
        }
    }
}
