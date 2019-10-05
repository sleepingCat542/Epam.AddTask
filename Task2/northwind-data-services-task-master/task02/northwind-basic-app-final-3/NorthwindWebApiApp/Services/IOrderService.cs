using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindWebApiApp.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<BriefOrderDescription>> GetOrdersAsync();

        Task<IEnumerable<BriefOrderVersion2Description>> GetExtendedOrdersAsync();

        Task<FullOrderDescription> GetOrderAsync(int orderId);
    }
}
