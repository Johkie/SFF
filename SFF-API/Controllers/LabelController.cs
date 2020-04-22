using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFF_API.Models;

using SFF_API.Services;

namespace SFF_API.Controllers
{
    [Route("api/rentals/")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelController(ILabelService labelService)
        {
            this._labelService = labelService;
        }

        [HttpGet("labels/{rentalId}")]
        [Produces("application/xml")]
        public async Task<ActionResult<LabelModel>> GetLabelForRentalOrder(int rentalId)
        {
            try
            {
                var label = await _labelService.GetXmlLabelFor(rentalId);
                return Ok(label);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
