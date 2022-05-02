using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface ICustomerService
    {
        Task Create(string name, string family, string email,string password);
        Task Modify(int id,string name, string family, string email);
        Task ChangePassword(int id, string password);
    }
}