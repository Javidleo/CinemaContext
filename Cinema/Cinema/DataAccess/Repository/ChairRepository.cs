using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{

    public class ChairRepository : IChairRepository
    {
        private readonly CinemaContext _context;

        public ChairRepository(CinemaContext context)
        => _context = context;

        public void Add(List<Chair> chairs)
        {
            _context.Chair.AddRange(chairs);
            _context.SaveChanges();
        }

        public List<Chair> FindBySalon(int salonId)
        => _context.Chair.Include(i => i.Salon).Where(i => i.SalonId == salonId && i.InUse == false).ToList();

        public bool DoesExist(int id)
        => _context.Chair.Any(i => i.Id == id);

        public bool DoesExist(List<int> chairIdList)
        => chairIdList.All(i=> _context.Chair.Select(c=> c.Id).Contains(i));

        public Chair FindWithParents(int id)
        => _context.Chair.Include(i => i.Salon).ThenInclude(i => i.Cinema).FirstOrDefault(i => i.Id == id);

    }
}
