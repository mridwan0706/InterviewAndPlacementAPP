using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementsController : BaseController<Placement, PlacementRepository>
    {
        private PlacementRepository placementRepository;
        public PlacementsController(PlacementRepository placementRepository) : base (placementRepository)
        {
            this.placementRepository = placementRepository;
        }

        [HttpGet("Sites")]
        public IActionResult GetPlacementSites()
        {
            var sites = placementRepository.GetPlacementSites().Result;
            return Ok(sites);
        }

        [HttpGet("Site/{id}")]
        public IActionResult DetailPlacementSite(int Id)
        {
            var detail = placementRepository.DetailPlacementSite(Id).Result;
            return Ok(detail);
        }

        [HttpGet("History/{empId}")]
        public IActionResult HistoryUser(string empId)
        {
            var placement = placementRepository.HistoryUser(empId).Result;
            return Ok(placement);
        }
    }
}