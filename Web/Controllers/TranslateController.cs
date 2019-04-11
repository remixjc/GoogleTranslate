using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;

namespace Web.Controllers
{            
    [Route("api/[controller]")]
    [ApiController]
    public class TranslateController : ControllerBase
    {
        private readonly INodeServices _services;
        public TranslateController(INodeServices services)
        {
            _services = services;
        }
            // GET: api/Translate
            [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Translate/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Translate
        [HttpPost]
        public async Task<string> Post([FromForm] string fr)
        {
            try
            {
                string result = await Common.Common.Translate(fr, "auto", Common.Common.Language.en_US.ToString(), _services);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // PUT: api/Translate/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
