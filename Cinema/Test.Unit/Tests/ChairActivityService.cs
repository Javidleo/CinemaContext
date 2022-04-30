using DomainModel.Domain;
using DomainModel.Validation;
using NEGSO.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;

namespace Test.Unit.Tests
{
    public class ChairActivityService : IChairActivityService
    {
        private readonly IChairRepository _chairRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IChairactivityRepository _chairActivityRepository;
        private readonly ChairActivityValidator _validtaor;
        public ChairActivityService(IChairRepository chairRepository, IAdminRepository adminRepository
                    , IChairactivityRepository chairActivityRepository, ISalonRepository salonRepository)
        {
            _chairRepository = chairRepository;
            _salonRepository = salonRepository;
            _adminRepository = adminRepository;
            _chairActivityRepository = chairActivityRepository;
            _validtaor = new ChairActivityValidator();
        }

        public Task Deactivate(int chairId, string description, DateTime startDate, Guid adminGuid, string adminFullName)
        {
            if (!_chairRepository.DoesExist(chairId))
                throw new NotFoundException("chair not found");

            if (!_adminRepository.DoesExist(adminGuid))
                throw new NotFoundException("admin not found");

            var chairActivity = ChairActivity.Create(chairId, startDate, startDate.ToPersianDate(), description, adminGuid, adminFullName);

            if (!_validtaor.Validate(chairActivity).IsValid)
                throw new NotAcceptableException("invalid chairActivity");

            _chairActivityRepository.Add(chairActivity);
            return Task.CompletedTask;
        }

        public Task Deactivate(List<int> chairIdList, string description, DateTime startDate, Guid adminGuid,
            string adminFullName)
        {
            if (!_adminRepository.DoesExist(adminGuid))
                throw new NotFoundException("admin not found");

            if (!_chairRepository.DoesExist(chairIdList))
                throw new NotFoundException("invalid chairId");

            List<ChairActivity> chairActivityList = new();
            foreach(var id in chairIdList)
            {
                var chairActivity = ChairActivity.Create(id, startDate, startDate.ToPersianDate(), description, adminGuid, adminFullName);

                chairActivityList.Add(chairActivity);
            }
            _chairActivityRepository.Add(chairActivityList);
            return Task.CompletedTask;
        }
    }
}