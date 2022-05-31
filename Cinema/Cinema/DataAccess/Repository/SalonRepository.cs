using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class SalonRepository :BaseRepository<Salon>, ISalonRepository
    {
        private readonly ICinemaContext _context;
        public SalonRepository(ICinemaContext context) : base(context)
        => _context = context;

        public bool DoesExist(int Id)
        => _context.Salon.Any(i => i.Id == Id);

        public Salon FindWithParents(int salonId)
        => _context.Salon.Include(i => i.Cinema).ThenInclude(i => i.CinemaActivities).FirstOrDefault(i => i.Id == salonId);
    }
}
