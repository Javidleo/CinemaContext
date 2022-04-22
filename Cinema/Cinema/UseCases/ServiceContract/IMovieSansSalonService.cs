using System;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IMovieSansSalonService
    {
        Task Create(int movieId, int salonId, int sansId, Guid adminGuid);
    }
}