using LibModel;
using LibModel.Enum;
using LibModel.ManageCharacters;
using LibModel.ManageEnvironment;
using SFML.Graphics;
using SFML.System;
using System;

namespace AntManager
{
    public class TileAnt: Tile
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

        private Sprite _antUpCarry;
        private Sprite _antLeftCarry;
        private Sprite _antDownCarry;
        private Sprite _antRightCarry;
        private Sprite _antDownLeftCarry;
        private Sprite _antDownRightCarry;
        private Sprite _antUpLeftCarry;
        private Sprite _antUpRightCarry;
        private bool _isCarry;
        private Text _nameAnt;
        private Text _lifeAnt;
        private OptionSimulator _op;
        public TileAnt()
        {
            _direction = Direction.Up;
            _antUp = new Sprite(new Texture("Content/AntPicker/ant-up.png"));
            _antLeft = new Sprite(new Texture("Content/AntPicker/ant-left.png"));
            _antDown = new Sprite(new Texture("Content/AntPicker/ant-down.png"));
            _antRight = new Sprite(new Texture("Content/AntPicker/ant-right.png"));
            _antDownLeft = new Sprite(new Texture("Content/AntPicker/ant-down-left.png"));
            _antDownRight = new Sprite(new Texture("Content/AntPicker/ant-down-right.png"));
            _antUpLeft = new Sprite(new Texture("Content/AntPicker/ant-up-left.png"));
            _antUpRight = new Sprite(new Texture("Content/AntPicker/ant-up-right.png"));


            _antUpCarry = new Sprite(new Texture("Content/AntPicker/ant-carry-up.png"));
            _antLeftCarry = new Sprite(new Texture("Content/AntPicker/ant-carry-left.png"));
            _antDownCarry = new Sprite(new Texture("Content/AntPicker/ant-carry-down.png"));
            _antRightCarry = new Sprite(new Texture("Content/AntPicker/ant-carry-right.png"));
            _antDownLeftCarry = new Sprite(new Texture("Content/AntPicker/ant-carry-down-left.png"));
            _antDownRightCarry = new Sprite(new Texture("Content/AntPicker/ant-carry-down-right.png"));
            _antUpLeftCarry = new Sprite(new Texture("Content/AntPicker/ant-carry-up-left.png"));
            _antUpRightCarry = new Sprite(new Texture("Content/AntPicker/ant-carry-up-right.png"));


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
                Font = new Font("Content/Fonts/KeepCalm-Medium.ttf"),
                Style = Text.Styles.Bold,
                CharacterSize = 8
            };
        }

        public void SetAnt(Character ant, OptionSimulator op)
        {

            _op = op;
            _isCarry = false;
            _direction = ant.Direction;
            Field = (Field)ant.Position;
            _lifeAnt.DisplayedString = " +" + ant.Life.ToString();
            _nameAnt.DisplayedString = ant.Name;
            if (ant.GetType() == typeof(AntPicker)
                && ((AntPicker)ant).ListObject.Count > 0)
            {
                _isCarry = true;
            }

            if (ant.Life <= 50)
            {
                _lifeAnt.Color = Color.Red;
            } else
            {
                _lifeAnt.Color = Color.Green;
            }

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {

            switch (_direction)
            {
                case Direction.Left:
                    Sprite = _isCarry ? _antLeftCarry : _antLeft;
                    break;
                case Direction.Right:
                    Sprite = _isCarry ? _antRightCarry : _antRight;
                    break;
                case Direction.Down:
                    Sprite = _isCarry ? _antDownCarry : _antDown;
                    break;
                case Direction.DownRight:
                    Sprite = _isCarry ? _antDownRightCarry : _antDownRight;
                    break;
                case Direction.DownLeft:
                    Sprite = _isCarry ? _antDownLeftCarry : _antDownLeft;
                    break;
                case Direction.UpLeft:
                    Sprite = _isCarry ? _antUpLeftCarry : _antUpLeft;
                    break;
                case Direction.UpRight:
                    Sprite = _isCarry ? _antUpRightCarry : _antUpRight;
                    break;
                default:
                    Sprite = _isCarry ? _antUpCarry : _antUp;
                    break;
            }
            base.Draw(target, states);

            _nameAnt.Position = new Vector2f(Sprite.Position.X, Sprite.Position.Y - Sprite.Texture.Size.Y/2);
            _lifeAnt.Position = new Vector2f(_nameAnt.Position.X + _nameAnt.GetGlobalBounds().Width , _nameAnt.Position.Y);
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