using LibAbstract.ManageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageEnvironment
{
    public class Path : AbstractAccess
    {
        public Path(AbstractArea start, AbstractArea end) : base(start, end)
        {
        }
    }
}
