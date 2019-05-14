using Store.Domain.Entities;

namespace Store.Domain.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}