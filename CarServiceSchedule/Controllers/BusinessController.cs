using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceSchedule.Business;
using CarServiceSchedule.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarServiceSchedule.Controllers
{
    [Route("api/[controller]")]
    public class BusinessController : Controller
    {
        private readonly ICarFeatureBusiness _carFeatureBusiness;
        private readonly IDemandBusiness _demandBusiness;

        public BusinessController(ICarFeatureBusiness carFeatureBusiness, IDemandBusiness demandBusiness)
        {
            _carFeatureBusiness = carFeatureBusiness;
            _demandBusiness = demandBusiness;
        }

        #region CarFeature
        // GET: api/values
        // [HttpGet]
        // [Route("~/CarFeature")]
        // public async Task<IEnumerable<CarFeature>> Get()
        // {
        //     var carFeatures = await _carFeatureBusiness.GetCarFeatures();
        //     return carFeatures;
        // }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

        #region Demand

        [HttpGet]
        [Route("~/DemandBusiness")]
        public Task<IEnumerable<Demand>> GetDemands()
        {
            var result = _demandBusiness.GetDemands();

            return result;
        }

        [HttpPost]
        [Route("~/DemandBusiness")]
        public async Task<ActionResult<Demand>> Create([FromBody] Demand demand)
        {
            var newDemand = await _demandBusiness.CreateDemand(demand);
            //return newDemand;

            // var result =  CreatedAtAction(nameof(Demand), new { id = newDemand.Id }, newDemand);
            var result =  CreatedAtAction(nameof(Create), new { id = newDemand?.Id }, newDemand); //Created action icin async await yapildi


            return result;
        }

        #endregion
    }
}
