using System;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IMovieService
    {
        Task Create(Guid adminGuid,string adminFullName, string name, string director, string producer, DateTime publishDate, string baseMaleActorName
                    , string baseFemaleActorName, string supportedMaleActorName, string supportedFemaleActorName);
    }
}