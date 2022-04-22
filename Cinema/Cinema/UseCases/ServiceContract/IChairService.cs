using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IChairService
    {
        Task Create(int row, int count, int salonId);
    }
}