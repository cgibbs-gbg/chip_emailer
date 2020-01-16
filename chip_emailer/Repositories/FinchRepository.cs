using ChipEmailer.Contexts;
using ChipEmailer.Models;
using ChipEmailer.Queries;
using System.Collections.Generic;
using System.Data;

namespace ChipEmailer.Repositories
{
    public class FinchRepository : BaseRepository, IFinchRepository
    {
        public FinchRepository(
            IChipEmailerDbContext dbContext
            )
            : base(dbContext)
        {
        }

        public int NextOrderId()
        {
            return QuerySingleOrDefaultAsync<int>("SELECT nextval('finch.ftdna_edi_order_seq')").Result;
        }

        public int? CreateOrder(
            string grcNumber,
            string marker,
            string batch,
            int priority = 20,
            IDbTransaction transaction = null
            )
        {
            var orderId = NextOrderId();

            var query = @"
                INSERT INTO finch.ftdna_edi_order (
                    order_id,
                    grc_number,
                    marker,
                    priority,
                    order_date,
                    batch
                ) VALUES (
                    @OrderId,
                    @GrcNumber,
                    @Marker,
                    @Priority,
                    LOCALTIMESTAMP,
                    @Batch
                )
                ";

            var count = Execute(query, new
            {
                OrderId = orderId,
                GrcNumber = grcNumber,
                Marker = marker,
                Priority = priority,
                Batch = batch
            },
            transaction: transaction
            );

            if (count > 0)
            {
                return orderId;
            }

            return null;
        }

        public FinchOrder GetOrderById(
            int orderId,
            IDbTransaction transaction = null
            )
        {
            var query = @"
                SELECT
                    feo.order_id AS ""OrderId"",
                    feo.grc_number AS ""GrcNumber"",
                    fep.panel AS ""Panel"",
                    feo.marker AS ""Marker"",
                    feo.priority AS ""Priority"",
                    CAST(feo.order_date as VARCHAR) AS ""OrderDate"",
                    feo.req_id AS ""ReqId"",
                    feo.allele_id AS ""AlleleId"",
                    feo.batch as ""Batch""
                FROM finch.ftdna_edi_order feo
                LEFT JOIN finch.ftdna_edi_panel fep ON fep.marker = feo.marker
                WHERE feo.order_id = @OrderId
                ";

            return QuerySingleOrDefaultAsync<FinchOrder>(
                query,
                new
                {
                    OrderId = orderId
                },
                transaction: transaction
                ).Result;
        }

        public IEnumerable<FinchOrder> GetPendingOrders(
            string grcNumber,
            IDbTransaction transaction = null
            )
        {
            var query = @"
                SELECT
                    feo.order_id AS ""OrderId"",
                    feo.grc_number AS ""GrcNumber"",
                    fep.panel AS ""Panel"",
                    feo.marker AS ""Marker"",
                    feo.priority AS ""Priority"",
                    CAST(feo.order_date as VARCHAR) AS ""OrderDate"",
                    feo.req_id AS ""ReqId"",
                    feo.allele_id AS ""AlleleId"",
                    feo.batch as ""Batch""
                FROM finch.ftdna_edi_order feo
                LEFT JOIN finch.ftdna_edi_panel fep ON fep.marker = feo.marker
                WHERE feo.grc_number = @GrcNumber
                ORDER BY order_date DESC
                ";

            return Query<FinchOrder>(
                query,
                new
                {
                    GrcNumber = grcNumber
                },
                transaction: transaction
                );
        }

        public IEnumerable<RegisteredSampleStoreTube> GetSampleStoreTubesByKitNumber(
            string kitNumber,
            IDbTransaction transaction = null
            )
        {
            var query = @"
                SELECT 
                    primer_name AS ""TubeBarcode"",
                    vector AS ""KitNumber"",
                    template_name AS ""GrcNumber"",
                    cmnt AS ""Comment""
                FROM finch.sample
                WHERE chemistry = 'SST'
                AND primer_name like 'FG%'
                AND vector = @KitNumber
                ";

            return Query<RegisteredSampleStoreTube>(
                query, 
                new { KitNumber = kitNumber },
                transaction: transaction
                );
        }

        public IEnumerable<RegisteredSampleStoreTube> GetSampleStoreTubesByGrcNumber(
            string grcNumber,
            IDbTransaction transaction = null
            )
        {
            var query = @"
                SELECT 
                    primer_name AS ""TubeBarcode"",
                    vector AS ""KitNumber"",
                    template_name AS ""GrcNumber"",
                    cmnt AS ""Comment""
                FROM finch.sample
                WHERE chemistry = 'SST'
                AND primer_name like 'FG%'
                AND template_name = @GrcNumber
                ";

            return Query<RegisteredSampleStoreTube>(
                query, 
                new { GrcNumber = grcNumber },
                transaction: transaction
                );
        }

        public IEnumerable<FinchOrder> GetPastDateChips(
          IDbTransaction transaction = null
          )
        {
            var query = QueryCache.GetQuery("FinchCheck.sql");
            return Query<FinchOrder>(
                query,
                transaction: transaction
            );
        }
    }

    public class RegisteredSampleStoreTube
    {
        public string TubeBarcode { get; set; }
        public string KitNumber { get; set; }
        public string GrcNumber { get; set; }
        public string Comment { get; set; }
        public bool IsFailed
        {
            get
            {
                return Comment == null ? false : Comment.Contains("FLD");
            }
        }
    }
}
