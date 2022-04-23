using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.ViewModels;

namespace UseCases.ServiceContract
{
    public interface IMovieSansSalonService
    {
        Task Create(int movieId, int salonId, int sansId, Guid adminGuid);
        Task<List<GetMovieByCityViewModel>> GetMovieByCity(int movieId, int cityId);
        Task<List<GetMovieByCityViewModel>> GetMovieByCity(int movieId, int cityId, DateTime premiereDate);
    }
}