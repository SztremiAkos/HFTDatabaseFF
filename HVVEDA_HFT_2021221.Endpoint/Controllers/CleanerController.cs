using HVVEDA_HFT_2021221.Endpoint.Services;
using HVVEDA_HFT_2021221.Logic;
using HVVEDA_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        IHubContext<SignalRHub> hub;

        public CleanerController(ICleanerLogic cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("CleanerCreated", value);
        }

        // PUT /Cleaner/5
        [HttpPut]
        public void Put([FromBody] Cleaner value)
        {
            cl.UpdateCleaner(value);
            this.hub.Clients.All.SendAsync("CleanerUpdated", value);
        }

        // DELETE /Cleaner/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var cleaner = this.cl.GetCleanerById(id);
            cl.DeleteCleaner(id);
            this.hub.Clients.All.SendAsync("CleanerDeleted", cleaner);
        }
    }
}
