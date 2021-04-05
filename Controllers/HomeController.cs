using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext con)
        {
            _logger = logger;
            context = con;
        }

        public IActionResult Index(long? team, string teamName, int pageNum = 0)
        {
            int bowlersPerPage = 5;

            return View(new IndexViewModel
            {

                Bowlers = (context.Bowlers
                    .Where(x => x.TeamId == team || team == null)
                    .OrderBy(x => x)
                    .Skip((pageNum - 1) * bowlersPerPage)
                    .Take(bowlersPerPage)
                    .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = bowlersPerPage,
                    CurrentPage = pageNum,

                    TotalNumItems = (team == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == team).Count())
                },

                TeamName = teamName
            });
        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
