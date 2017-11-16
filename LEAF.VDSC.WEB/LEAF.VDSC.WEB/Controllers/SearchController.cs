using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LEAF.VDSC.CORE.Services;

namespace LEAF.VDSC.WEB.Controllers
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        IElandmallService elandmallService;
        IGmarketService gmarketService;
        ITMonService tmonService;
        IWemakepriceService wemakepriceService;

        public SearchController(IElandmallService ElandmallService, IGmarketService GmarketService, ITMonService TMonService, IWemakepriceService WemakepriceService)
        {
            elandmallService = ElandmallService;
            gmarketService = GmarketService;
            tmonService = TMonService;
            wemakepriceService = WemakepriceService;
        }

        [HttpGet("elandmall")]
        public IActionResult GetElandmall(int discount = 50)
        {
            var results = elandmallService.SelectOverProducts(discount);
            return Ok(results);
        }

        [HttpGet("gmarket")]
        public IActionResult GetGmarket(int discount = 50)
        {
            var results = gmarketService.SelectOverProducts(discount);
            return Ok(results);
        }

        [HttpGet("tmon")]
        public IActionResult GetTMon(int discount = 50)
        {
            var results = tmonService.SelectOverProducts(discount);
            return Ok(results);
        }

        [HttpGet("wemakeprice")]
        public IActionResult GetWemakeprice(int discount = 50)
        {
            var results = wemakepriceService.SelectOverProducts(discount);
            return Ok(results);
        }

    }
}