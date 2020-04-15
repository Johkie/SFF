using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFF_API.Models;
using SFF_API.Context;
using Microsoft.EntityFrameworkCore;

namespace SFF_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {

        private readonly SFFEntitiesContext _context;

        public StudioController(SFFEntitiesContext context)
        {
            this._context = context;

            _context.Studios.Add(new StudioModel { StudioName = "Filmvetarna" });
            _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudioModel>>> GetMovies()
        {
            return await _context.Studios.ToListAsync();
        }

    }
}
