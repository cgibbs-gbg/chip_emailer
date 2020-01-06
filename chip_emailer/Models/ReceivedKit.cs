using System;
using System.Collections.Generic;
using System.Text;

namespace ChipEmailer.Models
{
    public class ReceivedKit
    {
        public string KitNumber { get; set; }
        public DateTimeOffset ReceivedAt { get; set; }
        public string Pipeline { get; set; }
    }
}
