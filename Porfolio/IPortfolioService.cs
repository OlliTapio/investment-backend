using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace investment_backend.Portfolio
{
    public interface IPortfolioService
    {
        Task<Account> GetAcccount();
    }
}
