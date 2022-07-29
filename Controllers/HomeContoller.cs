using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VposClientEntegrasyonDilara.Services;

namespace VposClientEntegrasyonDilara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration conf;
        private readonly VposServices vposServices;

        public HomeController(IConfiguration conf) //AutoWire
        {
            this.conf = conf;
            vposServices = new VposServices();
        }

        [HttpPost]
        public JsonResult Transaction(Sale datas)
        {
            return vposServices.transaction(datas);
        }
       
        [HttpDelete]

        public JsonResult Cancel(SaleCancel cancel)
        {
            return vposServices.cancel(cancel);
        }
        [HttpPut]

        public JsonResult Refund(SaleRefund refund)
        {
            return vposServices.refund(refund);
        }
    }
}
