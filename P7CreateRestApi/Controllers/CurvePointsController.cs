using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurvePointsController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public CurvePointsController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/CurvePoints
        [HttpGet]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetCurvePoints()
        {
            return await _context.CurvePoints.ToListAsync();
        }

        // GET: api/CurvePoints/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<CurvePoint>> GetCurvePoint(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            return curvePoint;
        }

        // PUT: api/CurvePoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<IActionResult> PutCurvePoint(int id, CurvePoint curvePoint)
        {
            if (id != curvePoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(curvePoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurvePointExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CurvePoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<ActionResult<CurvePoint>> PostCurvePoint(CurvePoint curvePoint)
        {
            _context.CurvePoints.Add(curvePoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurvePoint", new { id = curvePoint.Id }, curvePoint);
        }

        // DELETE: api/CurvePoints/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if (curvePoint == null)
            {
                return NotFound();
            }

            _context.CurvePoints.Remove(curvePoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurvePointExists(int id)
        {
            return _context.CurvePoints.Any(e => e.Id == id);
        }
    }
}
