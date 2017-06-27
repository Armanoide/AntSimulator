using LibAbstract.ManageCharacters;
using LibAbstract.ManageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstract.ManageEnvironment
{
    public abstract class AbstractArea
    {
        public float Y { get; }
        public float X { get; }
        public List<AbstractObject> ListObject { get; set; }
        public List<AbstractCharacter> ListCharacters { get; set; }
        public AbstractEnvironment Environment { get; set; }

        public AbstractArea(float x, float y)
        {
            this.X = x;
            this.Y = y; 
            ListCharacters = new List<AbstractCharacter>();
            ListObject = new List<AbstractObject>();
        }

        public void AddObject(AbstractObject obj)
        {
            ListObject.Add(obj);
            obj.Position = this;
        }
        

        public void AddEnvironment(AbstractEnvironment env)
        {
            this.Environment = env;
        }

        public void AddCharcater(AbstractCharacter character)
        {
            character.Position = this;
            ListCharacters.Add(character);
        }

        public void RemoveCharacter(AbstractCharacter character)
        {
                character.Position = null;
                ListCharacters.Remove(character);
        }

        public static int GetDistance(AbstractArea a, AbstractArea b)
        {

            return (int)Math.Round(Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2)));
        }
    }
}
