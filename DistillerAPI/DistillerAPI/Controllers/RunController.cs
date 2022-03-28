using DistillerAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace DistillerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunController : ControllerBase
    {

        private readonly IPDFTronService _pdfTronService;

        public RunController(IPDFTronService pdfTronService)
        {
            _pdfTronService = pdfTronService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            return _pdfTronService.Stuff();
        }
    }
}