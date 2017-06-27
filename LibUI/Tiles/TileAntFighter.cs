using LibModel;
using LibModel.Enum;
using LibModel.ManageCharacters;
using LibModel.ManageEnvironment;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntManager.Tiles
{
    public class TileAntFighter : Tile
    {

        private bool _isFighting;
        private Direction _direction;
        protected Sprite[] _antUp;
        protected Sprite[] _antLeft;
        protected Sprite[] _antDown;
        protected Sprite[] _antRight;
        protected Sprite[] _antDownLeft;
        protected Sprite[] _antDownRight;
        protected Sprite[] _antUpLeft;
        protected Sprite[] _antUpRight;

        private Text _nameAnt;
        private Text _lifeAnt;
        private Random _rand;
        private bool _isDeath;
        private OptionSimulator _op;

        public TileAntFighter()
        {
            _rand = new Random();
            _antUp = new Sprite[3];
            _antLeft = new Sprite[3];
            _antDown = new Sprite[3];
            _antRight = new Sprite[3];
            _antDownLeft = new Sprite[3];
            _antDownRight = new Sprite[3];
            _antUpLeft = new Sprite[3];
            _antUpRight = new Sprite[3];

            _antUp[0] = new Sprite(new Texture("Content/AntFighter/ant-up.png"));
            _antUp[1] = new Sprite(new Texture("Content/AntFighter/ant-up-fight-1.png"));
            _antUp[2] = new Sprite(new Texture("Content/AntFighter/ant-up-fight-2.png"));
            _antLeft[0] = new Sprite(new Texture("Content/AntFighter/ant-left.png"));
            _antLeft[1] = new Sprite(new Texture("Content/AntFighter/ant-left-fight-1.png"));
            _antLeft[2] = new Sprite(new Texture("Content/AntFighter/ant-left-fight-2.png"));
            _antDown[0] = new Sprite(new Texture("Content/AntFighter/ant-down.png"));
            _antDown[1] = new Sprite(new Texture("Content/AntFighter/ant-down-fight-1.png"));
            _antDown[2] = new Sprite(new Texture("Content/AntFighter/ant-down-fight-2.png"));
            _antRight[0] = new Sprite(new Texture("Content/AntFighter/ant-right.png"));
            _antRight[1] = new Sprite(new Texture("Content/AntFighter/ant-right-fight-1.png"));
            _antRight[2] = new Sprite(new Texture("Content/AntFighter/ant-right-fight-2.png"));
            _antDownLeft[0] = new Sprite(new Texture("Content/AntFighter/ant-down-left.png"));
            _antDownLeft[1] = new Sprite(new Texture("Content/AntFighter/ant-down-left-fight-1.png"));
            _antDownLeft[2] = new Sprite(new Texture("Content/AntFighter/ant-down-left-fight-2.png"));
            _antDownRight[0] = new Sprite(new Texture("Content/AntFighter/ant-down-right.png"));
            _antDownRight[1] = new Sprite(new Texture("Content/AntFighter/ant-down-right-fight-1.png"));
            _antDownRight[2] = new Sprite(new Texture("Content/AntFighter/ant-down-right-fight-2.png"));
            _antUpLeft[0] = new Sprite(new Texture("Content/AntFighter/ant-up-left.png"));
            _antUpLeft[1] = new Sprite(new Texture("Content/AntFighter/ant-up-left-fight-1.png"));
            _antUpLeft[2] = new Sprite(new Texture("Content/AntFighter/ant-up-left-fight-2.png"));
            _antUpRight[0] = new Sprite(new Texture("Content/AntFighter/ant-up-right.png"));
            _antUpRight[1] = new Sprite(new Texture("Content/AntFighter/ant-up-right-fight-1.png"));
            _antUpRight[2] = new Sprite(new Texture("Content/AntFighter/ant-up-right-fight-2.png"));



            _nameAnt = new Text()
            {
                Color = Color.White,
                Font = new Font("Content/Fonts/KeepCalm-Medium.ttf"),
                Style = Text.Styles.Bold,
                CharacterSize = 10
            };

            _lifeAnt = new Text()
            {
                Color = Color.Green,
                Font = new Font("Content/Fonts/Kameron-Bold.ttf"),
                Style = Text.Styles.Bold,
                CharacterSize = 8
            };
        }

        public void SetAnt(AntFighter ant, OptionSimulator op)
        {
            _op = op;
            _direction = ant.Direction;
            Field = (Field)ant.Position;
            _isFighting = ant.IsFighting;
            _lifeAnt.DisplayedString = " +" + ant.Life.ToString();
            _nameAnt.DisplayedString = ant.Name;
            _isDeath = ant.Life <= 0;
            if (ant.Life <= 50)
            {
                _lifeAnt.Color = Color.Red;
            }
            else
            {
                _lifeAnt.Color = Color.Green;
            }

        }



        public override void Draw(RenderTarget target, RenderStates states)
        {
            int r = _isFighting && _isDeath == false ? _rand.Next(0, 2) : 0;

            switch (_direction)
            {
                case Direction.Left:
                    Sprite = _antLeft[r];
                    break;
                case Direction.Right:
                    Sprite = _antRight[r];
                    break;
                case Direction.Down:
                    Sprite = _antDown[r];
                    break;
                case Direction.DownRight:
                    Sprite = _antDownRight[r];
                    break;
                case Direction.DownLeft:
                    Sprite = _antDownLeft[r];
                    break;
                case Direction.UpLeft:
                    Sprite = _antUpLeft[r];
                    break;
                case Direction.UpRight:
                    Sprite = _antUpRight[r];
                    break;
                default:
                    Sprite = _antUp[r];
                    break;
            }
            base.Draw(target, states);

            _nameAnt.Position = new Vector2f(Sprite.Position.X, Sprite.Position.Y - Sprite.Texture.Size.Y / 2);
            _lifeAnt.Position = new Vector2f(_nameAnt.Position.X + _nameAnt.GetGlobalBounds().Width, _nameAnt.Position.Y);
            if (_op.HideName)
            {
                target.Draw(_nameAnt);
            }
            if (_op.HideLife)
            {
                target.Draw(_lifeAnt);
            }
        }
    }
}
