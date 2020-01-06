using System;
using System.Collections.Generic;
using System.Text;

namespace ChipEmailer.Models
{
    public class CollatedKit
    {
        public ReceivedKit ReceivedKit { get; set; }
        public bool IsBundled { get; set; }
        public IList<string> BundleList { get; set; }
    }
}
