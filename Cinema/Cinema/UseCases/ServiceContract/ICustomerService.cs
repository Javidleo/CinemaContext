using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface ICustomerService
    {
        Task Create(string name, string family, string email);

    }
}