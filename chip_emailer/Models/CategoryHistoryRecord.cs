using System;
using System.Collections.Generic;
using System.Text;

namespace ChipEmailer.Models
{
    public class CategoryHistoryRecord
    {
        public string Kit { get; set; }
        public string Category { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Removed { get; set; }
    }
}
