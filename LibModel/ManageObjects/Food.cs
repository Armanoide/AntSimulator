using LibAbstract.ManageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageObjets
{
    public class Food : AbstractObject
    {

        public int Capacity { get; }

        private int _remaningPiece { get; set; }
        

        public Food(int totalPiece)
        {
            Capacity = totalPiece;
            _remaningPiece = totalPiece;
        }

        public Food TakeAPiece()
        {
            if (_remaningPiece >= 0)
            {
                this._remaningPiece -= 1;
                return new Food(1);
            }
            return null;
        }

        public int GetRemaningPiece()
        {
            return _remaningPiece;
        }

    }
}
