using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace investment_backend.Portfolio
{
    public class Transaction
    {
        
        public double TransactionId { get; set; }

        public string Currency { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime ValueDate { get; set; }

        public string TypeDescription { get; set; }

        public double Amount { get; set; }
    }
}
