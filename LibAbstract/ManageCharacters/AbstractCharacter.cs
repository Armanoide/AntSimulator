

using LibAbstract.ManageEnvironment;
using LibAbstract.ManageObjects;
using RandomNameGenerator;
using System;
using System.Collections.Generic;

namespace LibAbstract.ManageCharacters
{
    public abstract class AbstractCharacter
    {
        public double Life { get; set; }

        public string SimpleName { get; set; }

        public string Name { get; set; }

        public bool Death { get; set; }

        public List<AbstractObject> ListObject;

        private static int lastRand = -1;

        public virtual AbstractArea Position {  get; set; }

        protected AbstractArea _position;

        private static Random _rand = new Random(DateTime.Now.Millisecond);

        public AbstractCharacter()
        {
            ListObject = new List<AbstractObject>();
            Name = NameGenerator.GenerateLastName();
            Name = new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(Name.ToLower());
            SimpleName = Name;
            Life = 100;
            Death = false;
        }


        public int RandInt(int min, int max)
        {
            int t = _rand.Next(min, max);

            do
            {
                t = _rand.Next(min, max);
            } while (t == lastRand);

            lastRand = t;

            return t;
        }

        abstract public AbstractArea ChoiceNextArea(List<AbstractArea> listArea);

        override public string ToString()
        {
            return Name;
        }
    }
}
