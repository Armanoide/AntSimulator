using LibAbstract.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstract.ManageObjects;
using LibModel.ManageObjets;

namespace LibModel.Factories
{
    public class FactoryFood: AbstractFactory
    {
        public override AbstractObject CreateObject()
        {
            return new Food(20);
        }
    }
}
