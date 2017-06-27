using LibModel.ManageEnvironment;
using SFML.Graphics;
using SFML.System;

namespace AntManager
{
    public class TileMap : Tile
    {
        public TileMap()
        {
            Sprite = new Sprite(new Texture("Content/Grass/grass.png"));            
        }

        override public void SetField(Field Field)
        {
            base.SetField(Field);            
        }

    }
}