using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstract.ManageEnvironment
{
    public abstract class AbstractAccess
    {

       AbstractArea Start { get; }
       AbstractArea End { get; }
        public AbstractAccess(AbstractArea start, AbstractArea end)
        {
            this.Start = start;
            this.End = end;
        }

       public bool IsInAccess(AbstractArea area)
        {
            if((area.Y >= Start.Y && area.Y <= End.Y) 
                && area.X >= Start.X && area.Y < End.X)
            {
                return true;
            }
            return false;
        }
    }
}
