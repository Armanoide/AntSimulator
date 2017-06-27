using LibAbstract.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstract.ManageCharacters;
using LibModel.ManageCharacters;

namespace LibModel.Factories
{
    public class FactoryAntQueen: AbstractFactory
    {
        public override AbstractCharacter CreateCharacter()
        {
            return new AntQueen();
        }
    }
}
