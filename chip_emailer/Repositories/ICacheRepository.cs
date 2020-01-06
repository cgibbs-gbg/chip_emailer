using SnpCaller.Models;
using System.Collections.Generic;

namespace SnpCaller.Repositories
{
    public interface ICacheRepository : IBaseRepository
    {
        IEnumerable<NotificationType> NotificationTypes { get; }
    }
}
