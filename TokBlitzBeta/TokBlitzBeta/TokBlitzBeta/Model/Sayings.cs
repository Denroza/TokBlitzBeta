using System;
using System.Collections.Generic;
using System.Text;

namespace TokBlitzBeta.Model
{
    public class Sayings
    {
        public int id { get; set; }
        public string TheSaying { get; set; }
        public string Category { get; set; }
        public string SayingSource { get; set; }
        public int WordCount { get; set; }
        public int MaxChar { get; set; }
    }
}
