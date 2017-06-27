using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstract.ManageEnvironment;
using LibAbstract.ManageCharacters;
using LibModel.ManageObjets;
using LibModel.ManageEnvironment;

namespace LibModel.ManageCharacters
{
    public class AntFighter : Character
    {

        public bool CanBeat { get; set; }
        public bool IsFighting { get; set; }

        public AntFighter()
        {
            CanBeat = false;
            IsFighting = false;
            Name = "[Fighter]" + Name;
        }

        public override AbstractArea ChoiceNextArea(List<AbstractArea> listArea)
        {
            var home = (Anthill)listArea[0].Environment;
            var CurrentPosition = listArea[1];
            IsFighting = false;

            if (CurrentPosition == home.Position)
            {
                while (Life <= 90)
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
            }


            if (CurrentPosition == home.Position
                && CurrentPosition.ListCharacters.Where(c => c.GetType() == typeof(AntFighter)).ToList().Count < 1)
            {
                //IsGoingHome = true;
            }
            else
            {
                IsGoingHome = false;
            }

            if (Life < 20)
            {
                IsGoingHome = true;
            }

            if (!IsGoingHome)
            {

            AbstractArea foundEnemyPosition = null;
            foreach (AbstractArea area in listArea)
            {
                var listCharacters = area.ListCharacters;
                foreach (AbstractCharacter c in listCharacters)
                {
                    if (!home.IsCharacterFromAnthill(c) && c.Life > 0 
                        && AbstractArea.GetDistance(c.Position, Position) <= 1)
                    {
                        List<AbstractArea> l = new List<AbstractArea>
            {
                foundEnemyPosition
            };

                        foundEnemyPosition = area;
                        if (CanBeat && Life > 0)
                        {
                            IsFighting = true;
                            UpdateDirection((Field)area);
                            c.Life -= 5;
                            c.Life = Math.Round(c.Life, 2);
                        }
                        return base.ChoiceNextArea(l);
                    }
                }
            }
            }

            IsStoping = false;
            return base.ChoiceNextArea(listArea);
        }
    }
}
