using LibAbstract.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstract.ManageObjects;
using LibModel.ManageObjets;
using LibAbstract.ManageCharacters;

namespace LibModel.Factories
{
    class FactoryEgg: AbstractFactory
    {

        private FactoryAntQueen _factoryAntQueen;
        private FactoryAntPicker _factoryAntPicker;
        private FactoryAntFighter _factoryAntFighter;

        public FactoryEgg()
        {
            _factoryAntFighter = new FactoryAntFighter();
            _factoryAntPicker = new FactoryAntPicker();
            _factoryAntQueen = new FactoryAntQueen();

        }

        public override AbstractObject CreateObject()
        {
            return new Egg();
        }

        private static Random _rand = new Random(DateTime.Now.Millisecond);

        public int RandInt(int min, int max)
        {
            int t = _rand.Next(min, max);

            return t;
        }


        public override AbstractCharacter CreateCharacter()
        {
            switch (RandInt(0, 50))
            {
                case 0:
                    return _factoryAntFighter.CreateCharacter();
                case 1:
                    return _factoryAntFighter.CreateCharacter();
                case 2:
                    return _factoryAntFighter.CreateCharacter();
                case 3:
                    return _factoryAntFighter.CreateCharacter();
                case 4:
                    return _factoryAntQueen.CreateCharacter();
                case 5:
                    return _factoryAntFighter.CreateCharacter();
                case 6:
                    return _factoryAntFighter.CreateCharacter();
                case 7:
                    return _factoryAntFighter.CreateCharacter();
                case 8:
                    return _factoryAntFighter.CreateCharacter();
                case 9:
                    return _factoryAntFighter.CreateCharacter();
                case 10:
                    return _factoryAntFighter.CreateCharacter();
                case 11:
                    return _factoryAntFighter.CreateCharacter();
                case 12:
                    return _factoryAntFighter.CreateCharacter();
                default:
                    return _factoryAntPicker.CreateCharacter();

            }
        }
    }
}
