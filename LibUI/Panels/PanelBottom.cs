using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using LibModel.ManageEnvironment;
using LibModel.ManageObjets;
using LibAbstract.ManageObjects;
using LibAbstract.ManageEnvironment;
using LibModel.ManageCharacters;

namespace AntManager.Panels
{
    public class PanelBottom : Drawable
    {
        private Sprite _imageFood;
        private Sprite _imagePanel;
        private Sprite _imagePanelNone;
        private Sprite _imageAntPicker;
        private Sprite _imageAntQueen;
        private Sprite _imageAntFighter;
        private Sprite _imageEgg;
        private Text _textCount;
        private RenderWindow _app;
        private Field _fieldSelected;
        private Font _font;

        public PanelBottom(RenderWindow app)
        {
            _app = app;
            _imageAntFighter = new Sprite(new Texture("Content/Panel/ant-fighter.png"));
           _imageFood = new Sprite(new Texture("Content/Panel/food.png"));
            _imagePanel = new Sprite(new Texture("Content/Panel/bar-bottom-5.png"));
            _imagePanelNone = new Sprite(new Texture("Content/Panel/bar-bottom-none.png"));
            _imageAntPicker = new Sprite(new Texture("Content/Panel/ant-picker.png"));
            _imageAntQueen = new Sprite(new Texture("Content/Panel/ant-queen.png"));
            _imageEgg = new Sprite(new Texture("Content/Panel/egg.png"));
            _font = new Font("Content/Fonts/Kameron-Bold.ttf");
            _textCount = new Text()
            {
                CharacterSize = 12,
                Font = _font
            };
        }

        public void SetFieldForDisplay(Field field)
        {
            _fieldSelected = field;
        }

        private Sprite GetImagePanel()
        {
            Sprite imagePanel = _imagePanelNone;
            if (_fieldSelected?.Environment != null)
            {
                imagePanel = _imagePanel;
            }
            return imagePanel;
        }

        private Vector2f GetNavBarStartCoordinate()
        {
            var imagePanel = GetImagePanel();
            return new Vector2f(_app.GetView().Center.X - (_app.Size.X / 2), _app.GetView().Center.Y + (_app.Size.Y / 2) - imagePanel.Texture.Size.Y);
        }

        public void DisplayInfoAntQueen(RenderTarget target, AbstractEnvironment env)
        {
            var startPos = GetNavBarStartCoordinate();

            _textCount.DisplayedString = env.ListCharacter.Where(c => c.Death == false && c.GetType() == typeof(AntQueen)).ToList().Count.ToString();
            _imageAntQueen.Position = new Vector2f(startPos.X + 300, startPos.Y + 20);
            _textCount.Position = new Vector2f(_imageAntQueen.Position.X + _imageAntQueen.Texture.Size.X / 2.0f - _textCount.GetGlobalBounds().Width / 2.0f + 5, _imageAntQueen.Position.Y + _imageAntQueen.GetGlobalBounds().Height - 20);
            target.Draw(_imageAntQueen);
            target.Draw(_textCount);
        }

        public void DisplayInfoAntPicker(RenderTarget target, AbstractEnvironment env)
        {
            var startPos = GetNavBarStartCoordinate();

            _textCount.DisplayedString = env.ListCharacter.Where(c => c.Death == false && c.GetType() == typeof(AntPicker)).ToList().Count.ToString();
            _imageAntPicker.Position = new Vector2f(startPos.X + 345, startPos.Y + 20);
            _textCount.Position = new Vector2f(_imageAntPicker.Position.X + _imageAntPicker.Texture.Size.X / 2.0f - _textCount.GetGlobalBounds().Width / 2.0f + 5, _imageAntPicker.Position.Y + _imageAntPicker.GetGlobalBounds().Height - 20);
            target.Draw(_imageAntPicker);
            target.Draw(_textCount);
        }

        public void DisplayInfoAntFood(RenderTarget target, AbstractEnvironment env)
        {
            var startPos = GetNavBarStartCoordinate();

            _textCount.DisplayedString = env.ListObject.Where(o => o.GetType() == typeof(Food)).ToList().Count.ToString();
            _imageFood.Position = new Vector2f(startPos.X + 475, startPos.Y + 20);
            _textCount.Position = new Vector2f(_imageFood.Position.X + _imageFood.Texture.Size.X / 2.0f - _textCount.GetGlobalBounds().Width / 2.0f + 5, _imageFood.Position.Y + _imageFood.GetGlobalBounds().Height - 20);
            target.Draw(_imageFood);
            target.Draw(_textCount);
        }

        public void DisplayInfoAntFighter(RenderTarget target, AbstractEnvironment env)
        {
            var startPos = GetNavBarStartCoordinate();

            _textCount.DisplayedString = env.ListCharacter.Where(o => o.GetType() == typeof(AntFighter)).ToList().Count.ToString();
            _imageAntFighter.Position = new Vector2f(startPos.X + 390, startPos.Y + 20);
            _textCount.Position = new Vector2f(_imageAntFighter.Position.X + _imageAntFighter.Texture.Size.X / 2.0f - _textCount.GetGlobalBounds().Width / 2.0f + 5, _imageAntFighter.Position.Y + _imageAntFighter.GetGlobalBounds().Height - 20);
            target.Draw(_imageAntFighter);
            target.Draw(_textCount);
        }


        public void DisplayInfoAntEgg(RenderTarget target, AbstractEnvironment env)
        {
            var startPos = GetNavBarStartCoordinate();

            _textCount.DisplayedString = env.ListObject.Where(o => o.GetType() == typeof(Egg)).ToList().Count.ToString();
            _imageEgg.Position = new Vector2f(startPos.X + 430, startPos.Y + 20);
            _textCount.Position = new Vector2f(_imageEgg.Position.X + _imageEgg.Texture.Size.X / 2.0f - _textCount.GetGlobalBounds().Width / 2.0f + 5, _imageEgg.Position.Y + _imageEgg.GetGlobalBounds().Height - 20);
            target.Draw(_imageEgg);
            target.Draw(_textCount);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_app.Position != null)
            {
                var startPos = GetNavBarStartCoordinate();
                var imagePanel = GetImagePanel();
                imagePanel.Position = startPos;


                AbstractObject firstObject = null;
                if (_fieldSelected != null && _fieldSelected.ListObject.Count > 0)
                {
                    firstObject  = _fieldSelected.ListObject.First();
                }

                target.Draw(imagePanel);

                if (_fieldSelected != null)
                {

                    if (_fieldSelected.Environment != null)
                    {
                        var env = _fieldSelected.Environment;

                        DisplayInfoAntQueen(target, env);
                        DisplayInfoAntPicker(target, env);
                        DisplayInfoAntFood(target, env);
                        DisplayInfoAntFighter(target, env);
                        DisplayInfoAntEgg(target, env);
                    }
                    else if(false == true)
                    {

                    }
                    else if (firstObject != null && firstObject.GetType() == typeof(Food))
                    {
                        var food = (Food)firstObject;
                        _textCount.DisplayedString = "+" + food.GetRemaningPiece().ToString();
                        _imageFood.Position = new Vector2f(startPos.X + 10, startPos.Y + 10);
                        _textCount.Position = new Vector2f(_imageFood.Position.X + _imageFood.Texture.Size.X / 2.0f - _textCount.GetGlobalBounds().Width / 2.0f, _imageFood.Position.Y + _imageFood.GetGlobalBounds().Height + 10);
                        target.Draw(_imageFood);
                        target.Draw(_textCount);

                    }

                }
            }
        }
    }
}
