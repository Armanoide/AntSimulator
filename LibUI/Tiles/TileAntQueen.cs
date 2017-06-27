using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using LibModel.Enum;
using LibModel.ManageCharacters;
using LibModel.ManageEnvironment;
using SFML.System;
using LibModel;

namespace AntManager.Tiles
{
    public class TileAntQueen: Tile
    {
        private Direction _direction;
        protected Sprite _antUp;
        protected Sprite _antLeft;
        protected Sprite _antDown;
        protected Sprite _antRight;
        protected Sprite _antDownLeft;
        protected Sprite _antDownRight;
        protected Sprite _antUpLeft;
        protected Sprite _antUpRight;

        private Text _nameAnt;
        private Text _lifeAnt;

        private OptionSimulator _op;

        public TileAntQueen()
        {
            _antUp = new Sprite(new Texture("Content/AntQueen/ant-queen-up.png"));
            _antLeft = new Sprite(new Texture("Content/AntQueen/ant-queen-left.png"));
            _antDown = new Sprite(new Texture("Content/AntQueen/ant-queen-down.png"));
            _antRight = new Sprite(new Texture("Content/AntQueen/ant-queen-right.png"));
            _antDownLeft = new Sprite(new Texture("Content/AntQueen/ant-queen-down-left.png"));
            _antDownRight = new Sprite(new Texture("Content/AntQueen/ant-queen-down-right.png"));
            _antUpLeft = new Sprite(new Texture("Content/AntQueen/ant-queen-up-left.png"));
            _antUpRight = new Sprite(new Texture("Content/AntQueen/ant-queen-up-right.png"));


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

        public void SetAnt(Character ant, OptionSimulator op)
        {
            _op = op;
            _direction = ant.Direction;
            Field = (Field)ant.Position;
            _lifeAnt.DisplayedString = " +" + ant.Life.ToString();
            _nameAnt.DisplayedString = ant.Name;

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

            switch (_direction)
            {
                case Direction.Left:
                    Sprite = _antLeft;
                    break;
                case Direction.Right:
                    Sprite = _antRight;
                    break;
                case Direction.Down:
                    Sprite = _antDown;
                    break;
                case Direction.DownRight:
                    Sprite = _antDownRight;
                    break;
                case Direction.DownLeft:
                    Sprite = _antDownLeft;
                    break;
                case Direction.UpLeft:
                    Sprite = _antUpLeft;
                    break;
                case Direction.UpRight:
                    Sprite = _antUpRight;
                    break;
                default:
                    Sprite = _antUp;
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
