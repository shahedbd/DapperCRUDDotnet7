using Dapper;
using DapperCRUD.Data;
using DapperCRUD.Models;
using System.Data;
using static Dapper.SqlMapper;

namespace DapperCRUD.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IDapperContext _context;
        public BranchRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            var query = "SELECT * FROM " + typeof(Branch).Name;
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Branch>(query);
                return result.ToList();
            }
        }
        public async Task<Branch> GetByIdAsync(Int64 id)
        {
            var query = "SELECT * FROM " + typeof(Branch).Name + " WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Branch>(query, new { id });
                return result;
            }
        }
        public async Task Create(Branch _Branch)
        {
            var query = "INSERT INTO " + typeof(Branch).Name + " (Name, Description, CreatedDate,UpdatedDate) VALUES (@Name, @Description, @CreatedDate, @UpdatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", _Branch.Name, DbType.String);
            parameters.Add("Description", _Branch.Description, DbType.String);
            parameters.Add("CreatedDate", _Branch.CreatedDate, DbType.DateTime);
            parameters.Add("UpdatedDate", _Branch.UpdatedDate, DbType.DateTime);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task Update(Branch _Branch)
        {
            var query = "UPDATE " + typeof(Branch).Name + " SET Name = @Name, Description = @Description, UpdatedDate = @UpdatedDate WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", _Branch.Id, DbType.Int64);
            parameters.Add("Name", _Branch.Name, DbType.String);
            parameters.Add("Description", _Branch.Description, DbType.String);
            parameters.Add("UpdatedDate", _Branch.UpdatedDate, DbType.DateTime);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task Delete(Int64 id)
        {
            var query = "DELETE FROM " + typeof(Branch).Name + " WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
