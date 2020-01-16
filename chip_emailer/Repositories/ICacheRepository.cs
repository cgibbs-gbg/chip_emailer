using ChipEmailer.Models;
using System.Collections.Generic;

namespace ChipEmailer.Repositories
{
    public interface ICacheRepository : IBaseRepository
    {
        IEnumerable<NotificationType> NotificationTypes { get; }
    }
}
