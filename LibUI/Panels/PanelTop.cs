using LibModel.ManageEnvironment;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntManager.Panels
{
    class PanelTop : Drawable
    {
        public TimeSpan Time { get; set; }
        public World World {get; set;}

        private Sprite _imagePanel;
        private RenderWindow _app;
        private Text _text;


        public PanelTop(RenderWindow app)
        {
            _app = app;
            _imagePanel = new Sprite(new Texture("Content/Panel/bar-top.png"));
            _text = new Text()
            {
                CharacterSize = 15,
                Color = Color.White,
                Font = new Font("Content/Fonts/Kameron-Bold.ttf")
            };

        }

        private Vector2f GetNavBarStartCoordinate()
        {
            return new Vector2f(_app.GetView().Center.X - (_app.Size.X / 2), _app.GetView().Center.Y - (_app.Size.Y / 2));
        }

      
    
        public void Draw(RenderTarget target, RenderStates states)
        {
            var startPos = GetNavBarStartCoordinate();
            _imagePanel.Position = startPos;
            target.Draw(_imagePanel);

            _text.Position = new Vector2f(startPos.X + 100 , _app.GetView().Center.Y - (_app.Size.Y / 2));
            _text.DisplayedString = World.ListAnthill.Count + " fourmilière(s)";
            target.Draw(_text);


            _text.DisplayedString = Sun.Instance.IsNight ?  "Status: Nuit" : "Status: Jour";
            _text.DisplayedString += "    Day: " + Sun.Instance.Day;
            _text.Position = new Vector2f(_app.GetView().Center.X + _app.GetView().Size.X / 2 - 280, _app.GetView().Center.Y - (_app.Size.Y / 2));
            target.Draw(_text);


            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                Time.Hours,
                Time.Minutes,
                Time.Seconds,
                Time.Milliseconds);
            _text.DisplayedString = answer;
            _text.Position = new Vector2f(_app.GetView().Center.X - _text.GetGlobalBounds().Width / 2, _app.GetView().Center.Y - (_app.Size.Y / 2));
            target.Draw(_text);

        }


    }
}
