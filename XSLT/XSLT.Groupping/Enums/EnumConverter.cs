using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSLT.Enums
{
    public static class EnumConverter
    {
        public static OutputInput GetOutputInput(string tag)
        {
            if (tag == null) return OutputInput.Unknown;
            else if (tag == "output") return OutputInput.Output;
            else if (tag == "input") return OutputInput.Input;
            return OutputInput.Unknown;
        }
    }
}
