using MISA.Test.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class ReportingCriteria
    {
        public TypeReport Type { get; set; }

        public ReportFollow ReportFollow { get; set; }

        public Int32? Year { get; set; }
    }
}
