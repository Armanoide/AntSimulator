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
    public class TileFood : Tile
    {
        private Sprite _allPiece;
        private Sprite _halfPiece;
        private Sprite _littlePiece;
        private Sprite _littleLittlePiece;

        public TileFood()
        {
            _allPiece = new Sprite(new Texture("Content/Food/food.png"));
            _halfPiece = new Sprite(new Texture("Content/Food/food-2.png"));
            _littlePiece = new Sprite(new Texture("Content/Food/food-3.png"));
            _littleLittlePiece = new Sprite(new Texture("Content/Food/food-4.png"));            
        }

        public void SetFood(Food food)
        {
            Field =(Field)food.Position;
            Sprite = _allPiece;
            if (food.Capacity / 2 >= food.GetRemaningPiece())
            {
                Sprite = _halfPiece;
            }
            if (food.Capacity / 3 >= food.GetRemaningPiece())
            {
                Sprite = _littlePiece;
            }
            if (food.Capacity / 4 >= food.GetRemaningPiece())
            {
                Sprite = _littleLittlePiece;
            }
        }


    }
}
