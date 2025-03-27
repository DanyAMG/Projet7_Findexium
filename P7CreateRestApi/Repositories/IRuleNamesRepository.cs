using Dot.Net.WebApi.Controllers;

namespace P7CreateRestApi.Repositories
{
    public interface IRuleNamesRepository
    {
        Task<IEnumerable<RuleName>> GetAllRuleNamesAsync();
        Task<RuleName> GetRuleNameByIdAsync(int id);
        Task<RuleName> CreateRuleNameAsync(RuleName ruleName);
        Task<RuleName> UpdateRuleNameAsync(RuleName ruleName);
        Task DeleteRuleNameAsync(int id);
        Task<bool> RuleNameExistsAsync(int id);
    }
}
