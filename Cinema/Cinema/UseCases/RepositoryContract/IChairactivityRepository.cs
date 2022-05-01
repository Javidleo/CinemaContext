using DomainModel.Domain;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IChairactivityRepository
    {
        void Add(ChairActivity chairActivity);
        void Add(List<ChairActivity> chairActivityList);
    }
}