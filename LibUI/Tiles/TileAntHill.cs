using SFML.Graphics;
using SFML.System;
using LibModel.ManageEnvironment;

namespace AntManager
{
    public class TileAntHill : Tile
    {

        public TileAntHill() 
        {
            Sprite = new Sprite(new Texture("Content/Grass/hole.png"));
        }

    }
}