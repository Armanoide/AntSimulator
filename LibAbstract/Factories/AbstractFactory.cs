using LibAbstract.ManageCharacters;
using LibAbstract.ManageEnvironment;
using LibAbstract.ManageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstract.Factories
{
    public abstract class AbstractFactory
    {

        public virtual AbstractEnvironment CreateEnvironment()
        {
            return null;
        }

        public virtual AbstractArea CreateArea()
        {
            return null;
        }

        public virtual AbstractAccess CreateAccess()
        {
            return null;
        }

        public virtual AbstractCharacter CreateCharacter()
        {
            return null;
        }

        public virtual AbstractObject CreateObject()
        {
            return null;
        }
    }
}
