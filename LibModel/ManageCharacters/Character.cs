using LibAbstract.ManageCharacters;
using LibAbstract.ManageEnvironment;
using LibModel.Enum;
using LibModel.ManageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModel.ManageCharacters
{
    public abstract class Character : AbstractCharacter, IObserverSun
    {
        public bool IsStoping { get; set; }
        public bool IsWalking { get; set; }
        public bool IsGoingHome { get; set; }
        public bool StayHome { get; set; }
        public Direction Direction { get; set; }


        protected AbstractArea _destination;

        override public AbstractArea Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                if (!IsWalking && _position != null && value != null
                     && (value.Y != _position.Y || value.X != _position.X))
                {
                    IsWalking = true;
                    _destination = value;
                } else
                {
                    _position = value;
                }
            }
        }


        public Character(): base()
        {
            IsWalking = false;
            IsGoingHome = false;
            StayHome = false;
        }


    
        public bool IsInCenterField()
        {
            int isX = (int)Math.Round(Position.X % 1);
            int isY = (int)Math.Round(Position.Y % 1);

            return (isX == 0 && isY == 0);
        }

        protected void UpdateDirection(Field destination)
        {
            Field source = (Field) Position;
            if (destination == null)
            {
                return;
            }

            if (destination.X > source.X && destination.Y == source.Y)
            {
                Direction = Direction.Right;
            }

            if (destination.X < source.X && destination.Y == source.Y)
            {
                Direction = Direction.Left;
            }

            if (destination.Y > source.Y && destination.X == source.X)
            {
                Direction = Direction.Down;
            }

            if (destination.Y < source.Y && destination.X == source.X)
            {
                Direction = Direction.Up;
            }

            if (destination.Y < source.Y && destination.X < source.X)
            {
                Direction = Direction.UpLeft;
            }

            if (destination.Y > source.Y && destination.X > source.X)
            {
                Direction = Direction.DownRight;
            }

            if (destination.Y < source.Y && destination.X > source.X)
            {
                Direction = Direction.UpRight;
            }

            if (destination.Y > source.Y && destination.X < source.X)
            {
                Direction = Direction.DownLeft;
            }

        }

        protected void Walk()
        {
            float step = 0.1f;

            var source = this.Position;

            if (_destination == null)
            {
                IsWalking = false;
                return;
            }               

            if (_destination.X > source.X && _destination.Y == source.Y)
            {
                Direction = Direction.Right;
                this.Position = new Field(source.X + step, source.Y);
            }

            if (_destination.X < source.X && _destination.Y == source.Y)
            {
                Direction = Direction.Left;
                this.Position = new Field(source.X - step, source.Y);
            }

            if (_destination.Y > source.Y && _destination.X == source.X)
            {
                Direction = Direction.Down;
                this.Position = new Field(source.X, source.Y + step);
            }

            if (_destination.Y < source.Y && _destination.X == source.X)
            {
                Direction = Direction.Up;
                this.Position = new Field(source.X, source.Y - step);
            }

            if (_destination.Y < source.Y && _destination.X < source.X)
            {
                Direction = Direction.UpLeft;
                this.Position = new Field(source.X - step, source.Y - step);
            }

            if (_destination.Y > source.Y && _destination.X > source.X)
            {
                Direction = Direction.DownRight;
                this.Position = new Field(source.X + step, source.Y + step);
            }

            if (_destination.Y < source.Y && _destination.X > source.X)
            {
                Direction = Direction.UpRight;
                this.Position = new Field(source.X + step, source.Y - step);
            }

            if (_destination.Y > source.Y && _destination.X < source.X)
            {
                Direction = Direction.DownLeft;
                this.Position = new Field(source.X - step, source.Y + step);
            }


            Random r = new Random();
            double magic = r.NextDouble();

            // to fix floating cf: 1.0000001 not eq 1.0
            this.Position = new Field((float)Math.Round((Decimal) Position.X, 1),
                (float)Math.Round((Decimal)Position.Y, 1));       
        }
       
        override public AbstractArea ChoiceNextArea(List<AbstractArea> listArea)
        {
            var positionHome = listArea[0];

            if (Life == 0)
            {
                return Position;
            }

            if (StayHome)
            {
                IsGoingHome = true;
                _destination = positionHome;
            }

            if (IsStoping)
            {
                return Position;
            }

            if (IsGoingHome && (_destination == null || _destination != positionHome))                
            {
                IsWalking = true;
                _destination = positionHome;
            }

            if (_destination != null)
            {
                if (this.Position.X == _destination.X && this.Position.Y == _destination.Y)
                {
                    _destination = null;
                    IsWalking = false;
                }
            }

            if (IsWalking)
            {
                Walk();
                return null;
            }


            var total = listArea.Count;
            if (total == 1)
            {
                return listArea.First();
            }
            if (total > 0)
            {
                return listArea[RandInt(1, total)];
            }
            return null;
        }

        public void NotifyIsDay()
        {
            StayHome = false;
            IsStoping = false;
            _destination = null;
        }

        public void NotifyIsNight()
        {
            _destination = null;
            StayHome = true;
        }
    }
}
