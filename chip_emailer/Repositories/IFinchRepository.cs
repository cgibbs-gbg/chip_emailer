using ChipEmailer.Models;
using System.Collections.Generic;
using System.Data;

namespace ChipEmailer.Repositories
{
    public interface IFinchRepository : IBaseRepository
    {
        int NextOrderId();
        int? CreateOrder(string grcNumber, string marker, string batch, int priority = 20, IDbTransaction transaction = null);
        FinchOrder GetOrderById(int orderId, IDbTransaction transaction = null);
        IEnumerable<FinchOrder> GetPendingOrders(string grcNumber, IDbTransaction transaction = null);
        IEnumerable<RegisteredSampleStoreTube> GetSampleStoreTubesByKitNumber(string kitNumber, IDbTransaction transaction = null);
        IEnumerable<RegisteredSampleStoreTube> GetSampleStoreTubesByGrcNumber(string grcNumber, IDbTransaction transaction = null);
        IEnumerable<FinchOrder> GetPastDateChips(IDbTransaction transaction = null);
    }
}
