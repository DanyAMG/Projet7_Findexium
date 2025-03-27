using Microsoft.AspNetCore.Mvc;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Model;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidListsController : ControllerBase
    {
        private readonly IBidListRepository _repository;

        public BidListsController(IBidListRepository repository)
        {
            _repository = repository;
        }

        // GET: api/BidLists
        [HttpGet]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<IEnumerable<BidList>>> GetBids()
        {
            var bids = await _repository.GetAllBidsAsync();

            if (bids == null || !bids.Any())
            {
                return NotFound();
            }

            return Ok(bids);
        }

        // GET: api/BidLists/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<BidList>> GetBidList(int id)
        {
            var bidList = await _repository.GetBidListByIdAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            return Ok(bidList);
        }

        // PUT: api/BidLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<IActionResult> PutBidList(int id, BidListDTO bidListDTO)
        {
            var existingBidList = await _repository.GetBidListByIdAsync(id);

            if (existingBidList == null)
            {
                return NotFound();
            }

            existingBidList.Account = bidListDTO.Account;
            existingBidList.BidType = bidListDTO.BidType;
            existingBidList.BidQuantity = bidListDTO.BidQuantity;

            await _repository.UpdateBidListAsync(existingBidList);

            return NoContent();
        }

        // POST: api/BidLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<ActionResult<BidList>> PostBidList(BidListDTO bidListDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bidList = new BidList
            {
                Account = bidListDTO.Account,
                BidType = bidListDTO.BidType,
                BidQuantity = bidListDTO.BidQuantity,
            };
            var createdBidList = await _repository.CreateBidListAsync(bidList);
            return CreatedAtAction("GetBidList", new { id = createdBidList.BidListId }, createdBidList);
        }

        // DELETE: api/BidLists/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBidList(int id)
        {
            var bidList = await _repository.GetBidListByIdAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            await _repository.DeleteBidListAsync(id);
            return NoContent();
        }
    }
}
