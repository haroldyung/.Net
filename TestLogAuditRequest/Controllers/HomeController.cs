using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestLogAuditRequest.Attribute;
using TestLogAuditRequest.Models;
using TestLogAuditRequest.Models.TestAuditControllerRequest;

namespace TestLogAuditRequest.Controllers
{
    [ServiceFilter(typeof(AuditRequestLogAttribute))]
    public class HomeController: Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TestAuditControllerRequestContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, TestAuditControllerRequestContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return Forbid();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/Test/{abc}")]
        public IActionResult Test(int abc)
        {

            //throw new NullReferenceException();
            //return BadRequest();
            return Ok(abc);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}