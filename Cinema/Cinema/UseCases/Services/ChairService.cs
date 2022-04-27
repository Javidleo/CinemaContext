using DomainModel.Domain;
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
        public Task Create(int rows, int count, int salonId)
        {
            if (!_salonRepostiory.DoesExist(salonId))
                throw new NotFoundException("salon not found");

            int chairCount = rows * count;

            List<Chair> chairList = new List<Chair>();

            byte number = 1;
            byte row = 1;

            for(int i =1; i <= chairCount; i++)
            {
                if (number > count)
                {
                    number = 1;
                    row++;

                }
                var chair = Chair.Create(salonId, number, row);
                chairList.Add(chair);
                number++;
            }

            _chairRepostiory.Add(chairList);
            return Task.CompletedTask;
        }
    }
}