using HVVEDA_HFT_2021221.Logic;
using HVVEDA_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HVVEDA_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CleanerController : ControllerBase
    {

        ICleanerLogic cl;

        public CleanerController(ICleanerLogic cl)
        {
            this.cl = cl;
        }

        // GET: /Cleaner
        [HttpGet]
        public IEnumerable<Cleaner> Get()
        {
            return cl.ReadAll();
        }

        // GET /Cleaner/5
        [HttpGet("{id}")]
        public Cleaner Get(int id)
        {
            return cl.GetCleanerById(id);
        }

        // POST /Cleaner
        [HttpPost]
        public void Post([FromBody] Cleaner value)
        {
            cl.AddNewCleaner(value);
        }

        // PUT /Cleaner/5
        [HttpPut("{id}")]
        public void Put([FromBody] Cleaner value)
        {
            cl.UpdateCleaner(value);
        }

        // DELETE /Cleaner/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cl.DeleteCleaner(id);
        }
    }
}
