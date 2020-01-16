using System;

namespace ChipEmailer.Models
{
    public class FinchOrder
    {
        // Column: order_id
        public int OrderId { get; set; }

        // Column: grc_number
        public string GrcNumber { get; set; }

        // Column: panel
        public string Panel { get; set; }

        // Column: marker
        public string Marker { get; set; }

        // Column: priority
        public int Priority { get; set; }

        // Column: order_date
        public DateTime OrderDate { get; set; }

        // Column: req_id
        public int? ReqId { get; set; }

        // Column: allele_id
        public int? AlleleId { get; set; }

        // Column: batch
        public string Batch { get; set; }
    }
}
