using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.ViewModels;

namespace UseCases.ServiceContract
{
    public interface IMovieSansSalonService
    {
        Task Create(int movieId, int salonId, int sansId, Guid adminGuid, string adminFullName, DateTime premiereDate);
        Task<List<GetMovieByCityViewModel>> GetMovieByCity(int movieId, int cityId);
        Task<List<GetMovieByCityViewModel>> GetMovieByCity(int movieId, int cityId, DateTime premiereDate);
        Task<List<GetMovieByCityViewModel>> GetAll();
    }
}