using System.Data;

namespace DapperCRUD.Data
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
