using System;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface ISalonActivityService
    {
        Task Deactivate(int salonId, string description, DateTime startDate, Guid adminGuid, string adminFullName);
    }
}