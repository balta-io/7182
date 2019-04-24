using Store.Domain.Entities;

namespace Store.Domain.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer Get(string document);
    }
}