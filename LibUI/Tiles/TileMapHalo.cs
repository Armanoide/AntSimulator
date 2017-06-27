using LibModel.ManageEnvironment;
using SFML.Graphics;
using SFML.System;

namespace AntManager
{
    public class TileMapHalo : Tile
    {
        public TileMapHalo()
        {
            Sprite = new Sprite(new Texture("Content/Grass/grass-halo.png"));            
        }

        override public void SetField(Field Field)
        {
            base.SetField(Field);            
        }

    }
}