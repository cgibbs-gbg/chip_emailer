using ChipEmailer.Models;
using System.Collections.Generic;
using System.Data;

namespace ChipEmailer.Repositories
{
    public interface IFinchOrderRepository : IBaseRepository
    {
        IEnumerable<FinchOrder> GetAllOrders(
            IDbTransaction transaction = null
            );
        IEnumerable<FinchSample> GetFinchSamplesByChemistry(
            IEnumerable<string> chemistry,
            IDbTransaction transaction = null
            );
    }
}
