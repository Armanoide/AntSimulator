using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using LibModel.ManageObjets;
using LibModel.ManageEnvironment;

namespace AntManager.Tiles
{
    class TilePheromone : Tile
    {
        private List<Sprite> _listPheromoneSprite;
        private int _totalImage { get; }


        public TilePheromone()
        {
            _totalImage = 40;
            _listPheromoneSprite = new List<Sprite>();
            for(var i = 1; i <= _totalImage; i++)
            {
                var path = "Content/Pheromone/pheromone-" + i + ".png";
                Console.WriteLine(path);
                _listPheromoneSprite.Add(new Sprite(new Texture(path)));
            }
        }

        public void SetPheromones(List<Pheromone> list)
        {

          if (list.Count <= 0)
            {
                return;
            }

            Field =(Field)list.First().Position;
          var totalDurationField =  list.Select(p => p.Duration).Sum();

            Sprite = _listPheromoneSprite[0];
            // max 7 pheromone max duration 14 => 200x7 = 1400
            var index = (int)Math.Floor((double)(_totalImage * totalDurationField) / 1400) - 1;
            if (index >= 0)
            {
               Sprite = _listPheromoneSprite[index];
            }
        }
    }
}
