using System;
using System.Collections.Generic;
using System.Text;

namespace TokBlitzBeta.Model
{
    public class ConvertedQAS
    {
        public int id { get; set; }
        public string tok_group { get; set; }
        public string Category { get; set; }
        public string primary_text { get; set; }
        public string secondary_text { get; set; }
        public int wordcount { get; set; }
        public int maxchar { get; set; }
    }
}
