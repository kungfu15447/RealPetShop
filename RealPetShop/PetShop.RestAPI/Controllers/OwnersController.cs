using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;

namespace PetShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET api/pet
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] Filter filter)
        {
            try
            {
                if (filter.CurrentPage == 0 || filter.ItemsPrPage == 0)
                {
                    return _ownerService.GetAllOwners(null);
                }
                return _ownerService.GetAllOwners(filter);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        // GET api/pet/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            return _ownerService.GetOwner(id);
        }

        // POST api/pet
        [HttpPost]
        public void Post([FromBody] Owner owner)
        {
            _ownerService.CreateOwner(owner);
        }

        // PUT api/pet/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Owner updatedOwner)
        {
            Owner toBeUpdated = _ownerService.GetOwner(id);
            _ownerService.UpdateOwner(toBeUpdated, updatedOwner);
        }

        // DELETE api/pet/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Owner owner = _ownerService.GetOwner(id);
            _ownerService.DeleteOwner(owner);
        }
    }
}