
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Test.Integration
{
    public class ContextOptionBuilderGenerator
    {
        private string _connectionString = "Server=DESKTOP-MONHQ70;Database=bookdb;Trusted_Connection=True;";
        
        public ContextOptionBuilderGenerator WithConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public DbContextOptionsBuilder<CinemaContext> Build()
        {
            var optionBuilder = new DbContextOptionsBuilder<CinemaContext>();
            optionBuilder.UseSqlServer(_connectionString);
            return optionBuilder;
        }
    }
}
