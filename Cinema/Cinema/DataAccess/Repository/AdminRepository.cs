using DomainModel.Domain;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CinemaContext _context;
        public AdminRepository(CinemaContext context)
        => _context = context;

        public bool DoesExist(int id)
        => _context.Admin.Any(i => i.Id == id);
        
        public Admin FindByEmail(string email)
        => _context.Admin.FirstOrDefault(i => i.Email == email);

        public Admin FindByUserName(string userName)
        => _context.Admin.FirstOrDefault(i => i.UserName == userName);
    }
}
