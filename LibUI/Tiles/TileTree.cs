using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace AntManager.Tiles
{
    public class TileTree: Tile
    {

        public TileTree()
        {
            Sprite = new Sprite(new Texture("Content/tree.png"));
        }
    }
}
