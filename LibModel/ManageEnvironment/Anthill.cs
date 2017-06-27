using LibAbstract.ManageCharacters;
using LibAbstract.ManageEnvironment;
using LibModel.ManageCharacters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LibModel.ManageObjets;
using LibAbstract.ManageObjects;
using Newtonsoft.Json.Linq;

namespace LibModel.ManageEnvironment
{
    public class Anthill : AbstractEnvironment
    {
        private double _lastTimeAntFighterCanBeat;
        private double _lastTimeEggCreate;
        private double _lastTimeEggHeach;
        private double _lastTimeRemoveDead;
        private double _lastTimeHealthCare;

        public Anthill()
        {
            _lastTimeEggCreate = -1000;
            _lastTimeEggHeach = -1000;
            _lastTimeRemoveDead = -1000;
            _lastTimeAntFighterCanBeat = -1000;
        }

        public override void AddCharacter(AbstractCharacter character)
        {
            base.AddCharacter(character);
            if (character is IObserverSun)
            {
                Sun.Instance.AddObserver((IObserverSun)character);
            }
        }

        public override void RemoveCharater(AbstractCharacter character)
        {
            base.RemoveCharater(character);
            if (character is IObserverSun)
            {
                Sun.Instance.RemoveObserver((IObserverSun)character);
            }
        }

        override public void MoveCharacter(AbstractCharacter character, AbstractArea source, AbstractArea destination)
        {
            source.RemoveCharacter(character);
            destination.AddCharcater(character);

        }

        public bool IsCharacterFromAnthill(AbstractCharacter character)
        {
            foreach (AbstractCharacter c in ListCharacter)
            {
                if (c == character)
                {
                    return true;
                }
            }
            return false;
        }

        private void ProcessSimulate()
        {
            var isAntFighterCanBeat = false;
            var decreaseLife = false;
            var isTimeCleanDeath = false;

            if (TotalTime - _lastTimeHealthCare >= 2000)
            {
                _lastTimeHealthCare = TotalTime;
                decreaseLife = true;
            }


            if (TotalTime - _lastTimeAntFighterCanBeat >= 1000)
            {
                _lastTimeAntFighterCanBeat = TotalTime;
                isAntFighterCanBeat = true;
            }

            if (TotalTime - _lastTimeRemoveDead >= (60000 * 1) / 2)
            {
                _lastTimeRemoveDead = TotalTime;
                isTimeCleanDeath = true;

                List<AbstractCharacter> ListDead = ListCharacter.Where(c => c.Death == true || c.Life <= 0).ToList();
                ListCharacter.RemoveAll(c => c.Death == true || c.Life <= 0);
                foreach (AbstractCharacter c in ListDead)
                {
                    c.Position.RemoveCharacter(c);
                }
            }

            if (TotalTime - _lastTimeEggCreate >= (60000 * 1) / 2)
            {
                _lastTimeEggCreate = TotalTime;
                ((AntQueen)ListCharacter.FindLast(c => c.GetType() == typeof(AntQueen)))?.CreateEgg(this);
            }

            if (TotalTime - _lastTimeEggHeach >= (60000 * 1) / 2)
            {
                _lastTimeEggHeach = TotalTime;
                List<AbstractObject> eggs = ListObject.Where(o => o.GetType() == typeof(Egg)).ToList();

                foreach (AbstractObject egg in eggs)
                {
                    AddCharacter(((Egg)egg).HeachCharacter());
                    ListObject.Remove(egg);
                }

            }



            foreach (AbstractCharacter c in ListCharacter)
            {

                if (c.GetType() == typeof(AntFighter))
                {
                    ((AntFighter)c).CanBeat = isAntFighterCanBeat;
                }

                if (c.Death == true)
                {
                    continue;
                }

                if (c.Life <= 0)
                {
                    c.Death = true;
                    continue;
                }

                if (decreaseLife && !Sun.Instance.IsNight)
                {
                    c.Life -= 1;
                }



                List<AbstractArea> choices = new List<AbstractArea>();
                int maxByField = 200;

                // Home
                choices.Add(Position);

                // CURRENT POSITION
                choices.Add(Areas[(int)Math.Round(c.Position.Y), (int)Math.Round(c.Position.X)]);


                // UP-LEFT
                if (Math.Round(c.Position.X) - 1 >= 0 && Math.Round(c.Position.Y) - 1 >= 0)
                {
                    //if (Areas[(int)Math.Round(c.Position.X) - 1, (int)Math.Round(c.Position.Y) - 1].ListCharacters.Count <= maxByField)
                    {
                        choices.Add(Areas[(int)Math.Round(c.Position.Y) - 1, (int)Math.Round(c.Position.X) - 1]);
                    }
                }

                // UP-RIGHT
                if (Math.Round(c.Position.X) + 1 < WidthWorld && Math.Round(c.Position.Y) - 1 >= 0)
                {
                    //if (Areas[(int)Math.Round(c.Position.X) + 1, (int)Math.Round(c.Position.Y) - 1].ListCharacters.Count <= maxByField)
                    {
                        choices.Add(Areas[(int)Math.Round(c.Position.Y) - 1, (int)Math.Round(c.Position.X) + 1]);
                    }
                }


                // DOWN-RIGHT
                if (Math.Round(c.Position.X) + 1 < WidthWorld && Math.Round(c.Position.Y) + 1 < HeightWorld)
                {
                    //if (Areas[(int)Math.Round(c.Position.X) + 1, (int)Math.Round(c.Position.Y) + 1].ListCharacters.Count <= maxByField)
                    {
                        choices.Add(Areas[(int)Math.Round(c.Position.Y) + 1, (int)Math.Round(c.Position.X) + 1]);
                    }
                }

                // DOWN-LEFT
                if (Math.Round(c.Position.X) - 1 >= 0 && Math.Round(c.Position.Y) + 1 < HeightWorld)
                {
                    //if (Areas[(int)Math.Round(c.Position.X) - 1, (int)Math.Round(c.Position.Y) + 1].ListCharacters.Count <= maxByField)
                    {
                        choices.Add(Areas[(int)Math.Round(c.Position.Y) + 1, (int)Math.Round(c.Position.X) - 1]);
                    }
                }



                // RIGHT
                if (Math.Round(c.Position.X) + 1 < WidthWorld)
                {
                    //if (Areas[(int)Math.Round(c.Position.X) + 1, (int)Math.Round(c.Position.Y)].ListCharacters.Count <= maxByField)
                    {
                        choices.Add(Areas[(int)Math.Round(c.Position.Y), (int)Math.Round(c.Position.X) + 1]);
                    }
                }

                //LEFT
                if (Math.Round(c.Position.X) - 1 >= 0)
                {
                    //if (Areas[(int)Math.Round(c.Position.X) - 1, (int)Math.Round(c.Position.Y)].ListCharacters.Count <= maxByField)
                    {
                        choices.Add(Areas[(int)Math.Round(c.Position.Y), (int)Math.Round(c.Position.X) - 1]);
                    }
                }


                // DOWN
                if (Math.Round(c.Position.Y) + 1 < HeightWorld)
                {
                    //if (Areas[(int)Math.Round(c.Position.X), (int)Math.Round(c.Position.Y) + 1].ListCharacters.Count <= maxByField)
                    {
                        choices.Add(Areas[(int)Math.Round(c.Position.Y) + 1, (int)Math.Round(c.Position.X)]);
                    }
                }

                // UP
                if (Math.Round(c.Position.Y) - 1 >= 0)
                {
                    //if (Areas[(int)Math.Round(c.Position.X), (int)Math.Round(c.Position.Y) - 1].ListCharacters.Count <= maxByField)
                    {
                        choices.Add(Areas[(int)Math.Round(c.Position.Y) - 1, (int)Math.Round(c.Position.X)]);
                    }
                }

                Field newPosition = (Field)c.ChoiceNextArea(choices);
                if (newPosition != null)
                {

                     //                   new Thread(() =>
                     //                   {
                     //                       Thread.CurrentThread.IsBackground = true;
                    this.MoveCharacter(c, c.Position, newPosition);
                     //                    }).Start();


                }
            }

        }

        override public void Simulate()
        {
            ProcessSimulate();
        }

        public JObject ToJson()
        {
            JArray queens = new JArray();
            JArray fighters = new JArray();
            JArray pickers = new JArray();

            foreach(AntQueen q in ListCharacter.Where(c => c.GetType() == typeof(AntQueen)).ToList())
            {
                queens.Add(q.ToJson());
            }

            foreach (AntPicker p in ListCharacter.Where(c => c.GetType() == typeof(AntPicker)).ToList())
            {
                pickers.Add(p.ToJson());
            }

            foreach (AntFighter f in ListCharacter.Where(c => c.GetType() == typeof(AntFighter)).ToList())
            {
                fighters.Add(f.ToJson());
            }

            return new JObject(
                 new JProperty("position",(this.Position as Field).toJson()),
                 new JProperty("queens", queens),
                 new JProperty("fighters", fighters),
                 new JProperty("pickers", pickers),
                 new JProperty("_lastTimeAntFighterCanBeat", this._lastTimeAntFighterCanBeat),
                 new JProperty("_lastTimeEggCreate", this._lastTimeEggCreate),
                 new JProperty("_lastTimeEggHeach", this._lastTimeEggHeach),
                 new JProperty("_lastTimeRemoveDead", this._lastTimeRemoveDead),
                 new JProperty("_lastTimeHealthCare", this._lastTimeHealthCare)
                 );
        }

    }

}
