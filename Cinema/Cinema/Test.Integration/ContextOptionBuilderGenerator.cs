
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Test.Integration
{
    public class ContextOptionBuilderGenerator<T> where T : DbContext
    {
        private string _connectionString = "Server=.;Database=Cinema;Trusted_Connection=True;";
        
        public ContextOptionBuilderGenerator<T> WithConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public DbContextOptionsBuilder<T> Build()
        {
            var optionBuilder = new DbContextOptionsBuilder<T>();
            optionBuilder.UseSqlServer(_connectionString);
            return optionBuilder;
        }
    }
}
