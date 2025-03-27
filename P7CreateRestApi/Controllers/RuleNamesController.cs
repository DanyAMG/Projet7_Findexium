using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleNamesController : ControllerBase
    {
        private readonly IRuleNamesRepository _repository;

        public RuleNamesController(IRuleNamesRepository repository)
        {
            _repository = repository;
        }

        // GET: api/RuleNames
        [HttpGet]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetRules()
        {
            var rules = await _repository.GetAllRuleNamesAsync();

            if (rules == null || !rules.Any())
            {
                return NotFound();
            }

            return Ok(rules);
        }

        // GET: api/RuleNames/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<RuleName>> GetRuleName(int id)
        {
            var ruleName = await _repository.GetRuleNameByIdAsync(id);

            if (ruleName == null)
            {
                return NotFound();
            }

            return Ok(ruleName);
        }

        // PUT: api/RuleNames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<IActionResult> PutRuleName(int id, RuleName ruleNameDTO)
        {
            var existingRule = await _repository.GetRuleNameByIdAsync(id);

            if (existingRule == null)
            {
                return NotFound();
            }

            existingRule.Username = ruleNameDTO.Username;
            existingRule.Description = ruleNameDTO.Description;
            existingRule.JSon = ruleNameDTO.JSon;
            existingRule.Template = ruleNameDTO.Template;
            existingRule.SqlStr = ruleNameDTO.SqlStr;
            existingRule.SqlPart = ruleNameDTO.SqlPart;

            await _repository.UpdateRuleNameAsync(existingRule);
            return NoContent();
        }

        // POST: api/RuleNames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuleName>> PostRuleName(RuleName ruleNameDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ruleName = new RuleName
            {
                Username = ruleNameDTO.Username,
                Description = ruleNameDTO.Description,
                JSon = ruleNameDTO.JSon,
                Template = ruleNameDTO.Template,
                SqlStr = ruleNameDTO.SqlStr,
                SqlPart = ruleNameDTO.SqlPart
            };

            var createdRule = await _repository.CreateRuleNameAsync(ruleName);

            return CreatedAtAction("GetRuleName", new { id = createdRule.Id }, createdRule);
        }

        // DELETE: api/RuleNames/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            var ruleName = await _repository.GetRuleNameByIdAsync(id);

            if (ruleName == null)
            {
                return NotFound();
            }

            await _repository.DeleteRuleNameAsync(id);

            return NoContent();
        }
    }
}
