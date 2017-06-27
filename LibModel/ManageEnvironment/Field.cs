using LibAbstract.ManageEnvironment;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageEnvironment
{
    public class Field : AbstractArea
    {

        public Field(float x, float y) : base(x, y)
        {
        }

        public JObject toJson()
        {
            return new JObject(
                new JProperty("x", this.X),
                new JProperty("y", this.Y)
                );
        }
    }
}
