using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Controllers;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository : IRuleNamesRepository
    {
        private readonly LocalDbContext _context;

        public RuleNameRepository(LocalDbContext context)   
        {
            _context = context;
        }

        public async Task<IEnumerable<RuleName>> GetAllRuleNamesAsync()
        {
            return await _context.Rules.ToListAsync();
        }

        public async Task<RuleName> GetRuleNameByIdAsync(int id)
        {
            return await _context.Rules.FindAsync(id);
        }

        public async Task<RuleName> CreateRuleNameAsync(RuleName ruleName)
        {
            _context.Rules.Add(ruleName);
            await _context.SaveChangesAsync();
            return ruleName;
        }

        public async Task<RuleName> UpdateRuleNameAsync(RuleName ruleName)
        {
            _context.Entry(ruleName).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return ruleName;
        }

        public async Task DeleteRuleNameAsync(int id)
        {
            var ruleName = await _context.Rules.FindAsync(id);
            if(ruleName != null)
            {
                _context.Rules.Remove(ruleName);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RuleNameExistsAsync(int id)
        {
            return await _context.Rules.AnyAsync(e => e.Id == id);
        }
    }
}
