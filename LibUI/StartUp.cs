using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace AntManager
{
    public class StartUp: Drawable
    {

        private readonly Sprite _spriteBg;
        private readonly Text _title;
        private readonly Text _newSim;
        private readonly Text _loadSim;
        private readonly Text _options;
        private readonly Text _exit;
        private readonly RenderWindow _app;
        private readonly Font _font;
        private Simulator _simulator;
        private bool _isListenerEvent;

        public bool Active;

        public StartUp(RenderWindow app)
        {

            Active = false;
            _app = app;
            _isListenerEvent = true;
            _font = new Font("Content/Fonts/KeepCalm-Medium.ttf");
            _spriteBg = new Sprite(new Texture("Content/bg-startup.png"));
            this.SetListenerEvent(true);
            _title = new Text
            {
                Font = new Font("Content/Fonts/vinilo.ttf"),
                DisplayedString = "Ant Simulator",
                CharacterSize = 70,
                Color = Color.White,
                Style = Text.Styles.Bold
            };
            _title.Position = new Vector2f(_app.Size.X / 2.0f  - _title.GetGlobalBounds().Width / 2.0f, 20);

            _newSim = CreateTextAboveText("Nouvelle simulation".ToUpper(), _title, 100);
            _loadSim = CreateTextAboveText("Charger une partie".ToUpper(), _newSim, 10);
            _options = CreateTextAboveText("Options".ToUpper(), _loadSim, 10);
            _exit = CreateTextAboveText("Quitter".ToUpper(), _options, 10);

        }

        private void SetListenerEvent(bool value)
        {
            _isListenerEvent = value;
            if (value)
            {
                _app.MouseMoved += OnMouseMoved;
                _app.MouseButtonPressed += OnMouseButtonPressed;

            } else
            {
                _app.MouseMoved -= OnMouseMoved;
                _app.MouseButtonPressed -= OnMouseButtonPressed;

            }
        }

        public bool IsPointOverSprite(Vector2f position, Text text)
        {
            return	(position.X < text.Position.X + text.GetGlobalBounds().Width) && (text.Position.X < position.X) &&
                  	(position.Y < text.Position.Y + text.GetGlobalBounds().Height) && (text.Position.Y < position.Y);
        }

        public bool IsPointOverSprite(Vector2f position, Sprite sprite)
        {
            return	(position.X < sprite.Position.X + sprite.GetGlobalBounds().Width) && (sprite.Position.X < position.X) &&
                  	(position.Y < sprite.Position.Y + sprite.GetGlobalBounds().Height) && (sprite.Position.Y < position.Y);
        }


        private Text CreateText(string str, float y)
        {
            var text = new Text
            {
                Font = _font,
                DisplayedString = str,
                CharacterSize = 25,
                Color = new Color(0, 0, 0),
                Style = Text.Styles.Bold
            };
            text.Position = new Vector2f(_app.Size.X / 2.0f  - text.GetGlobalBounds().Width / 2.0f, y);
            return text;
        }

        private Text CreateTextAboveText(string str, Text above, float margin)
        {
            var y = above.GetGlobalBounds().Height + above.Position.Y + margin + 25;
            var text = CreateText(str, y);
            return text;
        }


        public void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            if (!Active) return;
            _newSim.Color = Color.Black;
            _exit.Color = Color.Black;
            _options.Color = Color.Black;
            _loadSim.Color = Color.Black;

            if (IsPointOverSprite(new Vector2f(e.X, e.Y), _newSim))
            {
                _newSim.Color = Color.Red;
            }
            if (IsPointOverSprite(new Vector2f(e.X, e.Y), _exit))
            {
                _exit.Color = Color.Red;
            }
            if (IsPointOverSprite(new Vector2f(e.X, e.Y), _options))
            {
                _options.Color = Color.Red;
            }
            if (IsPointOverSprite(new Vector2f(e.X, e.Y), _loadSim))
            {
                _loadSim.Color = Color.Red;
            }

        }

        public void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if (IsPointOverSprite(new Vector2f(e.X, e.Y), _exit))
                {
                    _app.Close();
                }
                if (IsPointOverSprite(new Vector2f(e.X, e.Y), _newSim))
                {
                    _simulator = new Simulator(_app, 40) {Active = true};
                    Active = false;
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_simulator != null)
            {
                target.Draw(_simulator);
            }

            if (!Active)
            {
                if (_isListenerEvent)
                {
                    SetListenerEvent(false);
                }
                return;
            }
            target.Draw(_spriteBg);
            target.Draw(_title);
            target.Draw(_newSim);
            target.Draw(_loadSim);
            target.Draw(_options);
            target.Draw(_exit);
        }
    }
}