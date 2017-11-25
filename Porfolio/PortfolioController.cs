using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace investment_backend.Portfolio
{
    [Route("api/[controller]")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        // GET api/portfolio
        [HttpGet]
        public async Task<Account> Get()
        {
            var returnValue = await _portfolioService.GetAcccount();
            return returnValue;
        }

    }
}
