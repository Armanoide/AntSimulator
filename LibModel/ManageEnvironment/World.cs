using LibAbstract.ManageCharacters;
using LibAbstract.ManageEnvironment;
using LibAbstract.ManageObjects;
using LibModel.Factories;
using LibModel.ManageCharacters;
using LibModel.ManageObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageEnvironment
{
    public class World : AbstractEnvironment
    {
        public Field[,] Fields { get; }

        public List<Anthill> ListAnthill { get; }

        public Sun Sun;
        private double _lastTimeDecreasePheromone;
        private double _lastTimeAddFood;

        public World(int width, int height)
        {
            // Create map
            WidthWorld = width;
            HeightWorld = height;

            Fields = new Field[WidthWorld, HeightWorld];

            for (var y = 0; y < HeightWorld; y++)
            {
                for (var x = 0; x < WidthWorld; x++)
                {
                    Fields[y, x] = new Field(x, y);
                }
            }

            //Create first anthill


            FactoryAnthill antFactory = new FactoryAnthill(Fields, width, height);
            var antHill = (Anthill)antFactory.CreateEnvironment();
            antHill.LoadEnvironment(antFactory);


            ListAnthill = new List<Anthill>
            {
                antHill,
            };
        
            _lastTimeAddFood = -10000;

            /*
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {

                FactoryAnthill antFactory2 = new FactoryAnthill(Fields, width, height);
                var antHill2 = (Anthill)antFactory.CreateEnvironment();
                antHill2.LoadEnvironment(antFactory2);
                ListAnthill.Add(antHill2);

                timer.Dispose();
            },
                        null, 1000, System.Threading.Timeout.Infinite); */

        }

        public Field GetFieldCoordinate(float widthField, float heightField, float x, float y)
        {
            // x = (screen.x / TILE_WIDTH_HALF + screen.y / TILE_HEIGHT_HALF) /2
            // y = (screen.y / TILE_HEIGHT_HALF - (screen.x / TILE_WIDTH_HALF)) /2
            int fieldX = (int)Math.Round((x / (widthField / 2.0f) + y / (heightField / 2.0f)) / 2.0f);
            int fieldY = (int)Math.Round((y / (heightField / 2.0f) - x / (widthField / 2.0f)) / 2.0f);
            if (fieldX - 1 >= 0 && fieldX - 1 < WidthWorld && fieldY >= 0 && fieldY < HeightWorld)
            {
                return Fields[fieldY, fieldX - 1];
                // QuckFix  : -1f 
                //return new Field(fieldX - 1f, fieldY + 0.0f);
            }
            return null;
        }

        override public void AddObject(AbstractObject obj)
        {

            Field positionNewObj = null;
            int maxTry = 10;

            do
            {
                Random r = new Random();
                int indexW = r.Next(0, WidthWorld);
                int indexH = r.Next(0, HeightWorld);
                positionNewObj = Fields[indexH, indexW];
                maxTry--;

            } while (positionNewObj.Environment != null && maxTry > 0
            && positionNewObj.ListObject.Count >= 1);
            if (positionNewObj != null)
            {
                base.AddObject(obj);
                positionNewObj.AddObject(obj);
                obj.Position = positionNewObj;

            }
        }

        override public void Simulate()
        {

            Sun.Instance.UpdateTime(TotalTime);

            if ((TotalTime - _lastTimeAddFood) >= 1000 && ListObject.Where(f => f.GetType() == typeof(Food)).ToList().Count < 80)
            {
                _lastTimeAddFood = TotalTime;
                AddObject(new Food(100));
            }

            List<AbstractObject> objectsToRemove = new List<AbstractObject>();

            foreach (AbstractObject obj in ListObject)
            {
                if (obj.GetType() == typeof(Food))
                {
                    Food food = (Food)obj;
                    if (food.GetRemaningPiece() == 0)
                    {
                        food.Position.ListObject.Remove(food);
                        objectsToRemove.Add(obj);
                    }
                }
            }
            ListObject.RemoveAll(food => objectsToRemove.Contains(food));


            ListAnthill.RemoveAll(a => a.ListCharacter.Count == 0);

            var countAnthill = ListAnthill.Count;
            for (var i = 0;  i < countAnthill; i++)
            {
                Anthill anthill = ListAnthill[i];
                if (anthill == null)
                {
                    break;
                }
                anthill.TotalTime = TotalTime;

                if (TotalTime - _lastTimeDecreasePheromone >= 20)
                {
                    _lastTimeDecreasePheromone = TotalTime;
                    for (var y = 0; y < HeightWorld; y++)
                    {
                        for (var x = 0; x < WidthWorld; x++)
                        {
                            var field = Fields[y, x];
                            foreach (AbstractObject o in field.ListObject)
                            {
                                if (o.GetType() == typeof(Pheromone))
                                {
                                    ((Pheromone)o).Duration--;
                                }
                            }
                            field.ListObject.RemoveAll(o => o.GetType() == typeof(Pheromone) && ((Pheromone)o).Duration <= 0);
                        }
                    }
                }
                var queens = anthill.ListCharacter.Where(c => c.GetType() == typeof(AntQueen)).ToList();
                if (queens.Count > 1)
                {
                    var queen = (AntQueen)queens.Last();
                    AbstractArea originalPositionQueen = queen.Position; 
                    queen.CreateAnthill(anthill, this);
                    queen.Position = originalPositionQueen;
                }
                anthill.Areas = Fields;
                anthill.TotalTime = TotalTime;
                anthill.Simulate();
            }
        }

    }
}
