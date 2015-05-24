using System;
using System.Collections.Generic;
using iosu.Entities;

namespace iosu.Models.View
{
    public class SelerDynamic
    {
        public String Selers { get; set; }

        public Dictionary<Months, long> Months { get; set; }
    }
}