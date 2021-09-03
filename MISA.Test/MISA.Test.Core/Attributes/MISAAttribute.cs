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
        public MISARequired()
        {

        }
    }

    /// <summary>
    /// Thuộc tính được xuất file excel
    /// </summary>
    /// CreatedBy: NHHoang (27/8/2021)
    public class MISAPropExport : Attribute
    {
        public readonly string Name;
        public MISAPropExport(string name)
        {
            this.Name = name;
        }
    }
}
