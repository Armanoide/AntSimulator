using LibModel.ManageEnvironment;
using SFML.Graphics;
using SFML.System;

namespace AntManager
{
    public abstract class Tile : Drawable
    {
        public Field Field { get; set; }


        public Sprite Sprite;

        public Tile()
        {
        }

        public virtual void SetField(Field Field)
        {
            this.Field = Field;
        }

        public int GetSizeX()
        {
            if (Sprite != null && Sprite.Texture != null)
            {
                return (int)Sprite.Texture.Size.X;
            }
            return 0;
        }

        public int GetSizeY()
        {
            if (Sprite != null && Sprite.Texture != null)
            {
                return (int)Sprite.Texture.Size.Y;
            }
            return 0;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            if (Sprite == null || Sprite.Texture == null || Field == null) return;

            //screen.x = (map.x - map.y) * TILE_WIDTH_HALF;
            //screen.y = (map.x + map.y) * TILE_HEIGHT_HALF;

            Sprite.Position = new Vector2f(
                (Field.X - Field.Y) * (Sprite.Texture.Size.X / 2.0f) ,
                (Field.X + Field.Y) * (Sprite.Texture.Size.Y / 2.0f)
                );
            /*
             float offsetX = 0;
             if (Field.X % 2 == 0)
             {
                 offsetX = Sprite.Texture.Size.X / 2.0f;

             }


            Sprite.Position = new Vector2f(
                 (Field.Y * Sprite.Texture.Size.X) + offsetX,
                 (Field.X * Sprite.Texture.Size.Y / 2.0f));

            */
            target.Draw(Sprite);
        }
    }
}