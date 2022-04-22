using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface ISalonService
    {
        Task Create(int cinemaId, string name, int capacity);
    }
}