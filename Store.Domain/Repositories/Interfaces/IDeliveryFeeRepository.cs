using Store.Domain.Entities;

namespace Store.Domain.Repositories.Interfaces
{
    public interface IDeliveryFeeRepository
    {
        decimal Get(string zipCode);
    }
}