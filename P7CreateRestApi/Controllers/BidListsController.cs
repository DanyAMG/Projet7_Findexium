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
    public class BidListsController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public BidListsController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/BidLists
        [HttpGet]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<IEnumerable<BidList>>> GetBids()
        {
            return await _context.Bids.ToListAsync();
        }

        // GET: api/BidLists/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<BidList>> GetBidList(int id)
        {
            var bidList = await _context.Bids.FindAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            return bidList;
        }

        // PUT: api/BidLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<IActionResult> PutBidList(int id, BidList bidList)
        {
            if (id != bidList.BidListId)
            {
                return BadRequest();
            }

            _context.Entry(bidList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidListExists(id))
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

        // POST: api/BidLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<ActionResult<BidList>> PostBidList(BidList bidList)
        {
            _context.Bids.Add(bidList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBidList", new { id = bidList.BidListId }, bidList);
        }

        // DELETE: api/BidLists/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBidList(int id)
        {
            var bidList = await _context.Bids.FindAsync(id);
            if (bidList == null)
            {
                return NotFound();
            }

            _context.Bids.Remove(bidList);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool BidListExists(int id)
        {
            return _context.Bids.Any(e => e.BidListId == id);
        }
    }
}
