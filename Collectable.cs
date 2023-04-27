using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMIT1514_Lab06_Platformer
{
    public class Collectable : GameObject
    {
        public enum State 
        {
            Enabled, Disabled
        }

        public State currentState = State.Enabled;
        public Collectable(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {

        }

        public void ProcessCollision(Actor player)
        {
            if (player.rectangle.Intersects(rectangle))
            {
                currentState = State.Disabled;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //base.Draw(gameTime);
            if(currentState == State.Enabled)
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                spriteBatch.Draw(texture, transform._position, texture.Bounds, Color.White, transform._rotation, texture.Bounds.Center.ToVector2(), transform._scale, SpriteEffects.None, 0);
                spriteBatch.End();
            }
        }
    }
}
