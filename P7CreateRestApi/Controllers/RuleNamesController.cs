using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleNamesController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public RuleNamesController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/RuleNames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetRules()
        {
            return await _context.Rules.ToListAsync();
        }

        // GET: api/RuleNames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleName>> GetRuleName(int id)
        {
            var ruleName = await _context.Rules.FindAsync(id);

            if (ruleName == null)
            {
                return NotFound();
            }

            return ruleName;
        }

        // PUT: api/RuleNames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuleName(int id, RuleName ruleName)
        {
            if (id != ruleName.Id)
            {
                return BadRequest();
            }

            _context.Entry(ruleName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleNameExists(id))
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

        // POST: api/RuleNames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuleName>> PostRuleName(RuleName ruleName)
        {
            _context.Rules.Add(ruleName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuleName", new { id = ruleName.Id }, ruleName);
        }

        // DELETE: api/RuleNames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            var ruleName = await _context.Rules.FindAsync(id);
            if (ruleName == null)
            {
                return NotFound();
            }

            _context.Rules.Remove(ruleName);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuleNameExists(int id)
        {
            return _context.Rules.Any(e => e.Id == id);
        }
    }
}
