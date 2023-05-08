using DapperCRUD.Models;

namespace DapperCRUD.Repository
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAllAsync();
        Task<Branch> GetByIdAsync(Int64 id);
        Task Create(Branch _Branch);
        Task Update(Branch _Branch);
        Task Delete(Int64 id);
    }
}
