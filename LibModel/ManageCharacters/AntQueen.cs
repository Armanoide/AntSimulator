using LibAbstract.ManageCharacters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstract.ManageEnvironment;
using LibModel.Factories;
using LibModel.ManageObjets;
using LibAbstract.ManageObjects;
using LibModel.ManageEnvironment;
using Newtonsoft.Json.Linq;

namespace LibModel.ManageCharacters
{
    public class AntQueen: Character
    {

        private FactoryEgg _factoryEgg;
        private FactoryAnthill _factoryAnthill;


        public AntQueen(): base()
        {
            Name = "[Queen]" + Name;
            _factoryEgg = new FactoryEgg();
        }


        public void CreateEgg(AbstractEnvironment env)
        {
            List<AbstractObject> foods = env.ListObject.Where(o => o.GetType() == typeof(Food)).ToList();

            if (foods.Count > 500 && foods.Count - 500 > 200 && Position.X == env.Position.X && Position.Y == env.Position.Y)
            {
                for (var i = 0; i < 500; i++)
                {
                    AbstractObject food = foods[i];
                    ListObject.Remove(food);
                }

                for (int i = 0; i < 5; i++)
                {
                    env.AddObject(_factoryEgg.CreateObject());
                }

            }
        }

        public void CreateAnthill(Anthill home, World world)
        {
            var Queens = home.ListCharacter.Where(c => c.GetType() == typeof(AntQueen)).ToList();

            if (Queens.Count > 1 && Queens.Last() == this)
            {
                // limit max 3 anthill
                if (world.ListAnthill.Count <= 3)
                {
                    home.ListCharacter.Remove(this);
                    _factoryAnthill = new FactoryAnthill(world.Fields, home.WidthWorld, home.HeightWorld);
                    var newAnthill = (Anthill)_factoryAnthill.CreateEnvironment();
                    newAnthill.LoadEnvironment(_factoryAnthill);
                    world.ListAnthill.Add(newAnthill);

                } else
                {
                    home.ListCharacter.Remove(this);
                    this.Life = 0;
                }
            }

        }

        public override AbstractArea ChoiceNextArea(List<AbstractArea> listArea)
        {

            var homePosition = listArea[0];
            var home = listArea[0].Environment;


            if (homePosition.X == Position.X && homePosition.Y == Position.Y)
            {
                while (Life <= 80)
                {
                    var foods = (from f in home.ListObject
                                 where f.GetType() == typeof(Food)
                                 select f);

                    if (foods.Count() == 0)
                    {
                        break;
                    }
                    Life += ((Food)foods.First()).GetRemaningPiece();
                    home.ListObject.Remove(foods.First());

                }
            }

            IsGoingHome = true;


            return base.ChoiceNextArea(listArea);
        }

        override public JObject ToJson()
        {
            return base.ToJson();
        }

    }
}
