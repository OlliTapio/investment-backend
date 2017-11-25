using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace investment_backend.Portfolio
{
    public class Account
    {
        public Double AvailableBalance { get; set;}
        public Double BookedBalance { get; set; }
    }
}
