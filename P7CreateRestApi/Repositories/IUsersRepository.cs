using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUSerAsync(int id);
        Task<bool> UserExistsAsync();
    }
}
