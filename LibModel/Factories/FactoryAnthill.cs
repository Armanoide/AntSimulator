using LibAbstract.Factories;
using LibAbstract.ManageCharacters;
using LibAbstract.ManageEnvironment;
using LibAbstract.ManageObjects;
using LibModel.ManageCharacters;
using LibModel.ManageEnvironment;
using LibModel.ManageObjets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.Factories
{
    public class FactoryAnthill : AbstractFactory
    {
        public Field Position { get; set; }
        private AntQueen _queen;

        private Field[,] _map;
        private int _widthWorld;
        private int _heightWorld;
        private Stack _stackAnt;
        private Stack _stackObjet;

        public FactoryAnthill(Field[,] map, int widthWorld, int heightWorld)
            : this(map, widthWorld, heightWorld, null)
        {

        }

        public FactoryAnthill(Field[,] map, int widthWorld, int heightWorld, AntQueen queen)
        {
            _queen = queen;
            FactoryAntQueen factoryQueen = new FactoryAntQueen();
            FactoryAntPicker factoryAntPicker = new FactoryAntPicker();
            FactoryFood factoryFood = new FactoryFood();
            FactoryAntFighter factoryAntFighter = new FactoryAntFighter();

            _heightWorld = heightWorld;
            _widthWorld = widthWorld;
            _map = map;
            _stackObjet = new Stack();
            _stackAnt = new Stack();
            _stackAnt.Push(queen ?? factoryQueen.CreateCharacter());
            for (var i = 0; i < 5; i++)
            {
                _stackAnt.Push(factoryAntFighter.CreateCharacter());
            }
            for (var i = 0; i < 20; i++)
            {
                _stackAnt.Push(factoryAntPicker.CreateCharacter());
            }
            for (var i = 0; i < 450; i++)
            {
                _stackObjet.Push(factoryFood.CreateObject());
            }
        }

        public override AbstractAccess CreateAccess()
        {
            return null;
        }

        public override AbstractArea CreateArea()
        {
            Field positionNewAnthill = null;
            int maxTry = 10;

            do
            {
                Random r = new Random();
                int indexW = r.Next(0, _widthWorld);
                int indexH = r.Next(0, _heightWorld);
                positionNewAnthill = _map[indexH, indexW];
                maxTry--;

            } while (positionNewAnthill.Environment != null && maxTry > 0);

            return positionNewAnthill;
        }

        public override AbstractCharacter CreateCharacter()
        {
            if (_stackAnt.Count > 0)
            {
                AbstractCharacter ant = (AbstractCharacter)_stackAnt.Pop();
                return ant;

            }
            return null;
        }

        public override AbstractEnvironment CreateEnvironment()
        {
            var anthill = new Anthill()
            {
                HeightWorld = _heightWorld,
                WidthWorld = _widthWorld
            };
            return anthill;
        }

        public override AbstractObject CreateObject()
        {
            if (_stackObjet.Count > 0)
            {
                AbstractObject obj = (AbstractObject)_stackObjet.Pop();
                return obj;
            }
            return null;
        }
    }
}
