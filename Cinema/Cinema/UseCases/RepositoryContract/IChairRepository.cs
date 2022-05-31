using DomainModel.Domain;
using System.Collections.Generic;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface IChairRepository : IBaseRepository<Chair>
    {
        Chair FindWithParents(int id);
        bool DoesExist(int id);
        bool DoesExist(List<int> chairIdList);
        List<Chair> FindBySalon(int salonId);
    }
}