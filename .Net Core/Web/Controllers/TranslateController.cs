using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using static Web.Common.Common;

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
        public List<Dictionary<string, string>> Get()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> lang = new Dictionary<string, string>();
            var result = Enum.GetValues(typeof(Language)).OfType<Enum>();
            foreach (var item in result)
            {
                lang = new Dictionary<string, string>();
                lang.Add("key", item.ToString());
                lang.Add("value", Common.Common.GetDescription(item));
                list.Add(lang);
            }
            return list;
        }

        // GET: api/Translate/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Translate
        [HttpPost]
        public async Task<string> Post([FromForm] string fr, string lang, string tolang)
        {
            try
            {
                if (string.IsNullOrEmpty(lang)) lang = Request.Form["lang"];
                if (string.IsNullOrEmpty(tolang)) tolang = Request.Form["tolang"];
                string result = await Translate(fr, lang, tolang, _services);
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
