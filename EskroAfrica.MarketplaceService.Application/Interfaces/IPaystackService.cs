using EskroAfrica.MarketplaceService.Common.DTOs.Paystack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EskroAfrica.MarketplaceService.Application.Interfaces
{
    public interface IPaystackService
    {
        Task<PaystackResponse<InitiateTransactionResponse>> InitiateTransaction(InitiateTransactionRequest request);
        Task<PaystackResponse<VerifyTransactionResponse>> VerifyTransaction(string reference);
    }
}
