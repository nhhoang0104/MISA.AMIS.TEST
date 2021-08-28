using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public readonly string Message;
        public MISARequired()
        {

        }
    }

    public class MISAPropExport : Attribute
    {
        public MISAPropExport()
        {
        }
    }
}
