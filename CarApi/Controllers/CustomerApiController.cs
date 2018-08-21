using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CarApi.Controllers
{
    [Route("[controller]")]
    public class CustomerApiController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<global::Models.Customer> Get()
        {
            return new global::Models.Customer[] { new global::Data.Customer().GetByName("*") };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public global::Models.Customer Get(string name)
        {
            return new global::Data.Customer().GetByName(name);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
