using System;
using System.Collections.Generic;

namespace WRM.App.ZscoreChecks.Queries.GetZscoresData
{
    public class ZscoresDTO
    {
        public List<DateTime> Timestamps { get; set; } = new List<DateTime>();
        public List<double> vals { get; set; } = new List<double>();
        public List<double> zScores { get; set; } = new List<double>();
    }
}
