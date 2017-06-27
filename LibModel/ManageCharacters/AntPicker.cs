using LibAbstract.ManageCharacters;
using LibAbstract.ManageEnvironment;
using LibAbstract.ManageObjects;
using LibModel.ManageEnvironment;
using LibModel.ManageObjets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageCharacters
{
    public class AntPicker : Character
    {

        public int MaxCapacityCarry { get; }
        private Field _isWalkingForFoodAtPosition;

        public AntPicker()
        {
            MaxCapacityCarry = 5;
            Name = "[Picker]" + Name;
        }

        override public AbstractArea ChoiceNextArea(List<AbstractArea> listArea)
        {
            var home = (Anthill)listArea[0].Environment;

            if (Life <= 25 && Life >= 10 || StayHome)
            {
                IsGoingHome = true;
            }

            if (Position.Environment?.GetType() == typeof(Anthill) && Position.Environment != home)
            {
                var homeEnemy = (Anthill)Position.Environment;
                IsStoping = true;
                var food = homeEnemy.ListObject.Count < 5 ? null : homeEnemy.ListObject.Where(o => o.GetType() == typeof(Food)).First();
                if (food != null || ListObject.Count < MaxCapacityCarry)
                {
                    homeEnemy.ListObject.Remove(food);
                    ListObject.Add(food);
                } else
                {
                    IsStoping = false;
                }
            }

            if (IsWalking && ListObject.Count > 0
                && (int)Math.Round(Position.X % 1) == 0
                && (int)Math.Round(Position.Y % 1) == 0)
            {
                var PositionCurrent = listArea[1];
                if (PositionCurrent.ListObject.Count < 7)
                {
                    PositionCurrent.AddObject(new Pheromone());
                }
                else
                {
                    ((Pheromone)(from p in PositionCurrent.ListObject
                                 where p.GetType() == typeof(Pheromone)
                                 select p).First()).Duration = 200;
                }

            }

            if (IsGoingHome == true && _position.X == home.Position.X && _position.Y == home.Position.Y)
            {
                home.ListObject.AddRange(ListObject);
                ListObject.RemoveAll(e => true == true);

                while (Life <= 100)
                {
                    var foods = (from f in home.ListObject
                                 where f.GetType() == typeof(Food)
                                 select f);

                    if (foods.Count() <= 10)
                    {
                        break;
                    }
                    Life += ((Food)foods.First()).GetRemaningPiece();
                    home.ListObject.Remove(foods.First());

                }

                _destination = null;
                IsGoingHome = false;
                IsWalking = false;
                _isWalkingForFoodAtPosition = null;
            }

            if (_isWalkingForFoodAtPosition != null)
            {

                if (_destination == null)
                {
                    _destination = _isWalkingForFoodAtPosition;
                }

                if (IsStoping)
                {
                    Food food = (Food)(from f in _isWalkingForFoodAtPosition.ListObject
                                       where f.GetType() == typeof(Food)
                                       select f).FirstOrDefault();

                    if (food != null && ListObject.Count < MaxCapacityCarry && food.GetRemaningPiece() > 0)
                    {
                        ListObject.Add(food.TakeAPiece());
                    }
                    else
                    {
                        IsGoingHome = true;
                        IsStoping = false;
                        _isWalkingForFoodAtPosition = null;
                    }
                }
                else if (Math.Abs(Math.Round(_isWalkingForFoodAtPosition.X - _position.X, 1)) <= 1
                    && Math.Abs(Math.Round(_isWalkingForFoodAtPosition.Y - _position.Y)) <= 1
                    && IsInCenterField())
                {
                    IsStoping = true;
                }
                else
                {
                }
            }
            else
            {
                for (var i = 1; i < listArea.Count && IsGoingHome == false; i++)
                {
                    var area = listArea[i];
                    foreach (AbstractObject obj in area.ListObject)
                    {
                        if (obj.GetType() == typeof(Food))
                        {
                            _isWalkingForFoodAtPosition = (Field)obj.Position;
                            _destination = obj.Position;
                            break;
                        }

                    }
                }
            }
            if (ListObject.Count > 0)
            {
                return base.ChoiceNextArea(listArea);
            }
            else if (Life <= 25)
            {
                return base.ChoiceNextArea(new List<AbstractArea>()
                    {
                    listArea[0]
                    });
            }
            else
            {
                var listPheromone = IsGoingHome ? new List<AbstractArea>() : GetAreaWithPheromone(listArea);
                if (listPheromone.Count > 0
                    && listPheromone.First().X != Position.X
                    && listPheromone.First().Y != Position.Y)
                {
                    return base.ChoiceNextArea(new List<AbstractArea>()
                    {
                        listPheromone.First()
                    });
                }
                else
                {
                   if (listPheromone.Count > 0)
                    {
                        IsGoingHome = false;
                        listArea.Remove(listArea[0]);
                    }

                    foreach (AbstractArea area in listPheromone)
                    {
                        listArea.Remove(area);
                    }

                    return base.ChoiceNextArea(listArea);
                }
            }

        }



        private List<AbstractArea> GetAreaWithPheromone(List<AbstractArea> list)
        {
            var homePosition = list[0];
            AbstractArea bestPosition = null;
            var bestDistance = 0;

            List<AbstractArea> listSelected = new List<AbstractArea>();

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                foreach (AbstractObject o in item.ListObject)
                {
                    if (o.GetType() == typeof(Pheromone))
                    {
                        var tmp = AbstractArea.GetDistance(homePosition, item);
                        if (tmp > bestDistance)
                        {
                            bestPosition = item;
                            bestDistance = tmp;
                        }
                        break;
                    }
                }
            }

            if (bestPosition != null)
            {
                listSelected.Add(bestPosition);
            }

            return listSelected;
        }

        override public JObject ToJson()
        {
            JObject baseC = base.ToJson();
            baseC.Merge(new JObject(
                new JProperty("_isWalkingForFoodAtPosition", this._isWalkingForFoodAtPosition),
                new JProperty("totalCarringFound", this.ListObject.Count())
                ));
            return baseC;
        }
    }
}