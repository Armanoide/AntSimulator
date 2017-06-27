using LibAbstract.Factories;
using LibAbstract.ManageCharacters;
using LibAbstract.ManageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstract.ManageEnvironment
{
    public abstract class AbstractEnvironment
    {
        public double TotalTime { get; set; }  
        public int HeightWorld { get; set; }
        public int WidthWorld { get; set; }

        public List<AbstractCharacter> ListCharacter { get; private set; }

        public List<AbstractObject> ListObject { get; }

        public AbstractArea[,]Areas { get; set; }

        public List<AbstractAccess> ListAccess { get; }

        public AbstractArea Position { get; set; }

        public AbstractEnvironment()
        {
            TotalTime = 0;
            ListCharacter = new List<AbstractCharacter>();
            ListObject = new List<AbstractObject>();

        }

        virtual public void AddObject(AbstractObject absObject)
        {
            this.ListObject.Add(absObject);
        }

        virtual public void AddCharacter(AbstractCharacter character)
        {
            this.ListCharacter.Add(character);
            Position.AddCharcater(character);
            character.Position = Position;
        }

        virtual public void RemoveCharater(AbstractCharacter character)
        {
            this.ListCharacter.Remove(character);
            Position.RemoveCharacter(character);           
        }

        public void AddArea(AbstractArea area)
        {
            this.Position = area;
            area.AddEnvironment(this);
        }

        public void LoadEnvironment(AbstractFactory factory)
        {
            this.AddArea(factory.CreateArea());
            for (AbstractCharacter c = factory.CreateCharacter(); c != null; c = factory.CreateCharacter())
            {
                this.AddCharacter(c);
            }
            for (AbstractObject o = factory.CreateObject(); o != null; o = factory.CreateObject())
            {
                this.AddObject(o);
            }            
        }

        public virtual void LoadObjet(AbstractObject absObject)
        {
        }

        public virtual void LoadCharacter(AbstractCharacter character)
        {
        }

        public virtual void MoveCharacter(AbstractCharacter character, AbstractArea source, AbstractArea destination)
        {

        }

        public virtual void Simulate()
        {

        }


    }

}
