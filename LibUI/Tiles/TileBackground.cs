using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using LibModel.ManageEnvironment;

namespace AntManager
{
    class TileBackground : Tile
    {
        public TileBackground()
        {
            this.Field = new Field(.025f, -0.5f);
            //Sprite = new Sprite(new Texture("Content/bg.png"));
        }
    }
}
