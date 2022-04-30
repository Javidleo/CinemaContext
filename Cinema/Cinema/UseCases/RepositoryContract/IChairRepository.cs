﻿using DomainModel.Domain;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IChairRepository
    {
        void Add(List<Chair> chairs);
        Chair FindWithParents(int id);
        bool DoesExist(int id);
        bool DoesExist(List<int> chairIdList);
        List<Chair> FindBySalon(int salonId);
    }
}