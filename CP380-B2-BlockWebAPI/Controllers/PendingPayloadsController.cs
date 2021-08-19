using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PendingPayloadsController : ControllerBase
    {
        // TODO
        private PendingPayloads _pending;
        public PendingPayloadsController(PendingPayloads payloads)
        {
            _pending = payloads;
        }

        [Route("/PendingPayloads")]
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(_pending.Payloads);
        }

        [Route("/AddPayload")]
        [HttpPost]
        public string Post(Payload payload)
        {
            _pending.Payloads.Add(payload);
            return JsonConvert.SerializeObject("Payload Added!");
        }
    }
}
