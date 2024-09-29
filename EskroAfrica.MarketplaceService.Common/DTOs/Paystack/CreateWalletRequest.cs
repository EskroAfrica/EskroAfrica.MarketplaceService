using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Common.DTOs.Paystack
{
    public class CreateWalletRequest
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string preferred_bank { get; set; }
        public string country { get; set; } = "NG";
    }
}
