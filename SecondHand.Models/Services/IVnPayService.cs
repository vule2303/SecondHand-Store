using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Models.Models.Domain;
using SecondHand.Models.Domain;

namespace SecondHand.Utility.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(Order model, HttpContext context);
        PaymentDetail PaymentExecute(IQueryCollection collections);
    }
}
