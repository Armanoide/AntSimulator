using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Linq;
using LibAbstract.ManageCharacters;
using LibModel.ManageCharacters;
using LibModel.ManageEnvironment;
using AntManager.Tiles;
using LibAbstract.ManageObjects;
using LibModel.ManageObjets;
using System.Threading;
using System.Diagnostics;
using AntManager.Panels;
using LibModel;

namespace AntManager
{
    public class Simulator : Drawable
    {
        private int Width { get; }
        private int Height { get; }
        public World World { get; }

        private Sprite _spriteBg;

        private PanelTop _panelTop;

        private PanelBottom _panelBottom;

        private Field _fieldSelected;

        private TilePheromone _tilePheromone;

        private TileBackground _tileBg;

        private TileMapHalo _tileMapHalo;

        private TileFood _tileChickenMeet;

        private TileMap _tileMap;

        private TileAnt _tileAnt;

        private TileAntHill _tileAntHill;

        private TileTree _tileTree;

        private TileAntFighter _tileAntFighter;

        private TileAntQueen _tileAntQueen;

        private double _lastTimeSimulate;

        private OptionSimulator _option;

        private Stopwatch _clock;

        private readonly RenderWindow _app;

        private bool _didSelectField;

        private bool _modeZoom;
        private View _view;
        public bool Active;
        private bool _isControlActive;


        public Simulator(RenderWindow app, int dimmension)
        {
            Height = dimmension;
            Width = dimmension;
            _clock = new Stopwatch();
            _lastTimeSimulate = -10000;
            _tilePheromone = new TilePheromone();
            _tileAntHill = new TileAntHill();
            _tileAnt = new TileAnt();
            _tileAntQueen = new TileAntQueen();
            _tileAntFighter = new TileAntFighter();
            _tileBg = new TileBackground();
            _tileMapHalo = new TileMapHalo();
            _tileMap = new TileMap();
            _tileChickenMeet = new TileFood();
            World = new World(dimmension, dimmension);
            _panelBottom = new PanelBottom(app);
            _panelTop = new PanelTop(app);
            Active = false;
            _didSelectField = false;
            _modeZoom = false;
            _app = app;
            _option = new OptionSimulator();

            var view = _app.GetView();
            view.Center = new Vector2f(0, dimmension * 25);
            //            var size = new Vector2f(view.Size.X * 2.0f, view.Size.Y * 2.0f);
            //            view.Size = size;
            _app.SetView(view);


            _app.KeyReleased += OnKeyRelead;
            _app.KeyPressed += OnKeyPressed;
            _app.MouseButtonPressed += OnMouseButtonPressed;
            _app.MouseButtonReleased += OnMouseButtonReleased;

            _clock.Start();
            _app.SetFramerateLimit(160);
        }

        private void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left && _didSelectField)
            {
                var c = _app.MapPixelToCoords(new Vector2i(e.X, e.Y));
                _fieldSelected = World.GetFieldCoordinate(_tileMap.GetSizeX(), _tileMap.GetSizeY(), c.X, c.Y);
                _didSelectField = false;
            }
        }

        private void SetPause(bool value)
        {
            _option.IsPausing = value;
            if (_option.IsPausing == true)
            {
                _clock.Stop();
            }
            else
            {
                _clock.Start();
                _lastTimeSimulate = -1000;
            }
        }

        private void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                _didSelectField = true;
            }
        }

        public void OnKeyRelead(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.LAlt || e.Code == Keyboard.Key.RAlt)
            {
                _modeZoom = false;
            }
            if (e.Code == Keyboard.Key.L)
            {
                _option.HideLife = !_option.HideLife;
            }
            if (e.Code == Keyboard.Key.N)
            {
                _option.HideName = !_option.HideName;
            }
            if ((e.Code == Keyboard.Key.LControl || e.Code == Keyboard.Key.LControl))
            {
                _isControlActive = false;
            }

            if (e.Code == Keyboard.Key.S && _isControlActive == true)
            {
                _isControlActive = false;
                Save s = new Save();
                s.SaveAs(World);
            }

        }


        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.LAlt || e.Code == Keyboard.Key.RAlt)
            {
                _modeZoom = true;
            }

            if ((e.Code == Keyboard.Key.LControl || e.Code == Keyboard.Key.LControl))
            {
                _isControlActive = true;
            }


            if (e.Code == Keyboard.Key.Space)
            {
                this.SetPause(!_option.IsPausing);
            }

            if (!_modeZoom && (e.Code == Keyboard.Key.Left || e.Code == Keyboard.Key.Right))
            {
                _view = _app.GetView();
                _view.Move(new Vector2f(e.Code == Keyboard.Key.Left ? -50 : 50, 0));
            }

            if (!_modeZoom && (e.Code == Keyboard.Key.Down || e.Code == Keyboard.Key.Up))
            {
                _view = _app.GetView();
                _view.Move(new Vector2f(0, e.Code == Keyboard.Key.Down ? 50 : -50));
            }

            if (_modeZoom && (e.Code == Keyboard.Key.Up || e.Code == Keyboard.Key.Down))
            {
                _view = _app.GetView();
                var size = new Vector2f(
                    e.Code == Keyboard.Key.Up ? _view.Size.X * 0.5f : _view.Size.X * 2.0f,
                    e.Code == Keyboard.Key.Up ? _view.Size.Y * 0.5f : _view.Size.Y * 2.0f
                    );
                _view.Size = size;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            
            if (!Active) return;

            if (!_option.IsPausing)
            {
                World.TotalTime = _clock.Elapsed.TotalMilliseconds;
            }

            if ((_clock.Elapsed.TotalMilliseconds - _lastTimeSimulate) >= 27)
            {
                _lastTimeSimulate = _clock.Elapsed.TotalMilliseconds;
                if (!_option.IsPausing)
                {
                    World.Simulate();
                }
            }

                 
            // Display map
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var field = World.Fields[y, x];
                    _tileMap.SetField(field);
                    target.Draw(_tileMap);

                    foreach (AbstractObject o in field.ListObject)
                    {
                        if (o.GetType() == typeof(Pheromone))
                        {
                            var pharomones = (from p in field.ListObject
                                              where p.GetType() == typeof(Pheromone)
                                              select p).ToList().ConvertAll<Pheromone>(p => (Pheromone)p);
                            _tilePheromone.SetPheromones(pharomones);
                            target.Draw(_tilePheromone);
                        }
                    }


                    var foods = field.ListObject.Where(s => s.GetType() == typeof(Food)).ToList();
                    if (foods.Count > 0)
                    {
                        _tileChickenMeet.SetFood((Food)foods[0]);
                        target.Draw(_tileChickenMeet);
                    }
                }
            }

            // Display halo map

            if (_fieldSelected != null)
            {
                _tileMapHalo.SetField(_fieldSelected);
                target.Draw(_tileMapHalo);
            }

         
            // Display anthill
            foreach (Anthill anthill in World.ListAnthill)
            {
                _tileAntHill.SetField((Field)anthill.Position);
                target.Draw(_tileAntHill);

                // Display characters of anthill
                foreach (AbstractCharacter c in anthill.ListCharacter)
                {
                    var isInAnthill = c.Position.Environment != null ? true : false;
                    if (isInAnthill/*anthill.Position.X == c.Position.X && anthill.Position.Y == c.Position.Y*/)
                    {
                        continue;
                    }
                    if (c.GetType() == typeof(AntFighter))
                    {
                        _tileAntFighter.SetAnt((AntFighter)c, _option);
                        target.Draw(_tileAntFighter);
                        continue;
                    }


                    if (c.GetType() == typeof(AntQueen))
                    {
                        _tileAntQueen.SetAnt((AntQueen)c, _option);
                        target.Draw(_tileAntQueen);
                        continue;
                    }

                    if (c.GetType() == typeof(AntPicker))
                    {
                        _tileAnt.SetAnt((AntPicker)c, _option);
                        target.Draw(_tileAnt);
                        continue;
                    }

                }
            }




            if (_view != null)
            {
                _app.SetView(_view);
                _view = null;
            }

            // Display NavBar Info
            if (_app.GetView().Size.X == 800 && _app.GetView().Size.Y == 600)
            {

                _panelBottom.SetFieldForDisplay(_fieldSelected);
                target.Draw(_panelBottom);

                _panelTop.Time = TimeSpan.FromMilliseconds(World.TotalTime);
                _panelTop.World = World;
                target.Draw(_panelTop);
            }

        }
    }
}