using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using WebVideoTransport.Models;
using YoutubeExplode.Videos;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace WebVideoTransport.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Play(InputData input)
        {
            if (input.Url.IsNullOrEmpty()) return BadRequest();
            var client = new YoutubeClient();

            var streamManifest = await client.Videos.Streams.GetManifestAsync(input.Url);// "https://www.youtube.com/watch?v=1La4QzGeaaQ");
            var streamInfo = streamManifest.GetMuxedStreams()
                .Where(s => s.Container == Container.Mp4).GetWithHighestVideoQuality();
            
            return Ok(streamInfo.Url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
