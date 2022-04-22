using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class ChairService : IChairService
    {
        private readonly IChairRepository _chairRepostiory;
        private readonly ISalonRepository _salonRepostiory;
        public ChairService(IChairRepository chairRepository, ISalonRepository salonRepository)
        {
            _chairRepostiory = chairRepository;
            _salonRepostiory = salonRepository;
        }
        public Task Create(int row, int count, int salonId)
        {
            if (!_salonRepostiory.DoesExist(salonId))
                throw new NotFoundException("salon not found");

            List<Chair> chairs = new List<Chair>();
            _chairRepostiory.Add(chairs);
            return Task.CompletedTask;
        }
    }
}