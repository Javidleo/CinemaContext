using DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class SalonRepository : ISalonRepository
    {
        private readonly CinemaContext _context;
        public SalonRepository()
        => _context = new CinemaContext();

        public void Add(Salon salon)
        {
            _context.Salon.Add(salon);
            _context.SaveChanges();
        }

        public bool DoesExist(int Id)
        => _context.Salon.Any(i => i.Id == Id);

        public Salon FindWithParents(int salonId)
        => _context.Salon.Include(i => i.Cinema).ThenInclude(i => i.CinemaActivities).FirstOrDefault(i => i.Id == salonId);
    }
}
