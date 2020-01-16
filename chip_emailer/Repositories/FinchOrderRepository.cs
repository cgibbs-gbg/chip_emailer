using ChipEmailer.Contexts;
using ChipEmailer.Models;
using System.Collections.Generic;
using System.Data;

namespace ChipEmailer.Repositories
{
    public class FinchOrderRepository : BaseRepository, IFinchOrderRepository
    {
        public FinchOrderRepository(IFinchDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<FinchOrder> GetAllOrders(
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
                    feo.order_date AS ""OrderDate"",
                    feo.req_id AS ""ReqId"",
                    feo.allele_id AS ""AlleleId"",
                    feo.batch as ""Batch""
                FROM finch.ftdna_edi_order feo
                LEFT JOIN finch.ftdna_edi_panel fep ON fep.marker = feo.marker
                ORDER BY order_date DESC
                ";

            return Query<FinchOrder>(
                query,
                transaction: transaction
                );
        }

        public IEnumerable<FinchSample> GetFinchSamplesByChemistry(
            IEnumerable<string> chemistry,
            IDbTransaction transaction = null
            )
        {
            var query = @"
                SELECT
                    s.sample_id AS ""SampleId"",
                    s.req_id AS ""ReqId"",
                    s.label AS ""Label"",
                    s.label_tags AS ""LabelTags"",
                    s.status AS ""Status"",
                    s.folder AS ""Folder"",
                    s.cmnt AS ""Comment"",
                    s.template_name AS ""TemplateName"",
                    s.primer_name AS ""PrimerName"",
                    s.chemistry AS ""Chemistry"",
                    s.vector AS ""Vector"",
                    s.direction AS ""Direction"",
                    s.insert_size AS ""InsertSize"",
                    s.vec_dbk AS ""VecDbk"",
                    s.src_pos AS ""SourcePosition"",
                    s.dst_pos AS ""DestinationPosition"",
                    s.log_id AS ""LogId""
                FROM finch.sample s
                ORDER BY sample_id
                ";

            return Query<FinchSample>(
                query,
                transaction: transaction
                );
        }
    }
}
