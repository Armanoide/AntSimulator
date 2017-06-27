using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel
{
    public class OptionSimulator
    {
        public bool HideName { get; set; }
        public bool IsPausing { get; set; }
        public bool HideLife { get; set; }

        public OptionSimulator()
        {
            HideName = false;
            IsPausing = false;
            HideLife = false;
        }

    }
}
