using LibAbstract.ManageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageObjets
{

    public class Pheromone : AbstractObject
    {
        public int Duration { get; set; }
        public Pheromone()
        {
            Duration = 200;
        }
    }
}
