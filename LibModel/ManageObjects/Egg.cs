using LibAbstract.ManageCharacters;
using LibAbstract.ManageObjects;
using LibModel.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageObjets
{
    public class Egg: AbstractObject
    {
        private FactoryEgg _factoryEgg;

        public Egg()
        {
            _factoryEgg = new FactoryEgg();
        }


        public AbstractCharacter HeachCharacter()
        {
            return _factoryEgg.CreateCharacter();
        }
    }
}
