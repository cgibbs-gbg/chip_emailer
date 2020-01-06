using SnpCaller.Contexts.Interfaces;
using SnpCaller.Models;
using System;
using System.Collections.Generic;

namespace SnpCaller.Repositories
{
    public class CacheRepository : BaseRepository, ICacheRepository
    {
        private DateTime _lastRefreshed;
        private int _secondsBetweenRefresh = 1800;

        public CacheRepository(
            ISnpCallerDbContext dbContext
            )
            : base(dbContext)
        {
        }

        private IEnumerable<NotificationType> _notificationTypes;
        public IEnumerable<NotificationType> NotificationTypes
        {
            get
            {
                if (_isExpired())
                {
                    _loadCache();
                }
                return _notificationTypes;
            }
        }

        private bool _isExpired()
        {
            if (_lastRefreshed == null || _lastRefreshed.AddSeconds(_secondsBetweenRefresh) < DateTime.Now)
            {
                return true;
            }

            return false;
        }

        private void _loadCache()
        {
            _lastRefreshed = DateTime.Now;

            var sql = @"
                SELECT * FROM NotificationTypes;
                ";

            QueryMultiple(sql, (result) =>
            {
                _notificationTypes = result.Read<NotificationType>();
            });
        }

        public class KeyValuePairDto<TKey, TValue>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
        }
    }
}
