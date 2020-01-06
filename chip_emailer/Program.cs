using ChipEmailer.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SnpCaller.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ChipEmailer
{
    public class ChipEmailer
    {
        private readonly ILogger<ChipEmailer> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICacheRepository _cacheRepo;
        private readonly IDbConnection _transactionManager;

        private const int COMMAND_TIMEOUT = 300;

        private const string SYSTEM_USER_GUID = "cdf3825a-efac-42a9-a4fc-bc41218565f6";

        private const int ENTITY_TYPE_SAMPLE = 1;
        private const int ENTITY_TYPE_TUBE = 2;
        private const int ENTITY_TYPE_RACK48 = 18;

        private const string STEP_RACK_SCAN_GUID = "e6fb2471-f611-4a85-bf0f-7d86563c0755";
        private const string STEP_CVIALS_GUID = "62cc947b-426b-4f33-8c2f-8e1953183171";

        private const int DATA_TYPE_STRING = 14;

        public ChipEmailer(
            ILogger<ChipEmailer> logger,
            IConfiguration configuration,
            ICacheRepository cacheRepo,
            IDbConnection transactionManager
            )
        {
            _logger = logger;
            _configuration = configuration;
            _cacheRepo = cacheRepo;
            _transactionManager = transactionManager;
        }

        private static IDictionary<string, int> PipelinePrecedence = new Dictionary<string, int>
        {
            { "BT", 0 },
            { "HG", 1 },
            { "DL", 2 },
            { "LN", 3 },
            { "MH", 4 },
            { "FF", 5 },
            { "VG", 6 },
            { "MT", 7 },
            { "YS", 8 },
            { "OT", 9 },
            { "NG", 10 },
            { "HH", 11 },
            { "CG", 12 },
            { "DR", 13 },
            { "DZ", 14 },
            { "MA", 15 },
            { "ME", 16 },
            { "UF", 17 },
        };

        private static IList<string> CoreApiKitPipelines = new List<string> { "BT", "HG", "DL", "LN", "HH", "CG", "DR", "DZ", "MA", "ME", "UF" };

        private CategoryHistoryRecord DeterminePrecedentCategory(List<CategoryHistoryRecord> categoryHistory)
        {
            CategoryHistoryRecord bestMatch = null;

            foreach (var item in categoryHistory)
            {
                if (bestMatch == null)
                {
                    bestMatch = item;
                    continue;
                }

                if (PipelinePrecedence[item.Category] < PipelinePrecedence[bestMatch.Category])
                {
                    bestMatch = item;
                }
            }

            return bestMatch;
        }

        private Dictionary<string, List<CategoryHistoryRecord>> ReadCategoryHistory()
        {
            if (!File.Exists(_configuration["Files:CategoryHistory"]))
            {
                return new Dictionary<string, List<CategoryHistoryRecord>>();
            }

            var fileContents = File.ReadAllText(_configuration["Files:CategoryHistory"]);
            var categoryHistory = JsonConvert.DeserializeObject<Dictionary<string, List<CategoryHistoryRecord>>>(fileContents);

            return categoryHistory;
        }

        public void Run()
        {
            //// Send notification for flagged error bin kits
            //if (flaggedKitsErrors.Count() > 0)
            //{
            //    _logger.LogInformation(string.Format("Detected {0} flagged kits", flaggedKitsErrors.Count()));
            //    try
            //    {
            //        var recipients = string.Join(",", _configuration.GetSection("Notifications:Email:Recipients").Get<string[]>());

            //        _logger.LogInformation(string.Format("Attempting to send notification email to {0}", recipients));
            //        var smtpClient = new SmtpClient
            //        {
            //            Host = _configuration["Notifications:Email:Hostname"],
            //            Port = int.Parse(_configuration["Notifications:Email:Port"]),
            //            EnableSsl = true,
            //            UseDefaultCredentials = false,
            //            DeliveryMethod = SmtpDeliveryMethod.Network,
            //            Credentials = new NetworkCredential(_configuration["Notifications:Email:Username"], _configuration["Notifications:Email:Password"])
            //        };
            //        using (var msg =
            //            new MailMessage(_configuration["Notifications:Email:Username"], recipients)
            //            {
            //                Subject = $"AutoSorter - Flagged Kit Detected in Error Bin",
            //                Body = $"Detected flagged kits in Error Bin:\r\n\r\n{string.Join("\r\n", flaggedKitsErrors)}"
            //            })
            //        {
            //            smtpClient.Send(msg);
            //            _logger.LogInformation("Notification email successfully sent");
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        _logger.LogInformation("Exception occurred while attempting to send email:\r\n{0}\r\n{1}", e.Message, e.StackTrace);
            //    }
            //}
        }

        private int CreateSystemEvent(IDbTransaction transaction)
        {
            string query = @"
                INSERT INTO Events (EventAt, UserId) VALUES (CURRENT_TIMESTAMP, @SystemUserGuid);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
                ";

            return transaction.Connection.Query<int>(query, new
            {
                SystemUserGuid = SYSTEM_USER_GUID,
            }, transaction).First();
        }
    }
}