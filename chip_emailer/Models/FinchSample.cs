namespace ChipEmailer.Models
{
    public class FinchSample
    {
        // Column: sample_id
        public int SampleId { get; set; }

        // Column: req_id
        public int? ReqId { get; set; }

        // Column: label
        public string Label { get; set; }

        // Column: label_tags
        public string LabelTags { get; set; }

        // Column: status
        public string Status { get; set; }

        // Column: folder
        public string Folder { get; set; }

        // Column: cmnt
        public string Comment { get; set; }

        // Column: template_name
        public string TemplateName { get; set; }

        // Column: primer_name
        public string PrimerName { get; set; }

        // Column: chemistry
        public string Chemistry { get; set; }

        // Column: vector
        public string Vector { get; set; }

        // Column: direction
        public string Direction { get; set; }

        // Column: insert_size
        public int? InsertSize { get; set; }

        // Column: vec_dbk
        public int? VecDbk { get; set; }

        // Column: src_pos
        public string SourcePosition { get; set; }

        // Column: dst_pos
        public string DestinationPosition { get; set; }

        // Column: log_id
        public int? LogId { get; set; }
    }
}
