using System;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface ICinemaActivityService
    {
        Task Deactivate(int cinemaId, string description, DateTime startDate, Guid adminGuid
            , string adminFullName);
    }
}