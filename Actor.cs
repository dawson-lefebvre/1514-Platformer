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
    public class Actor : GameObject
    {
        internal Vector2 velocity;

        public Actor(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {
            velocity = new Vector2(0, 0);
            this.transform = base.transform;
            this.texture = base.texture;

            this.rectangle = this.texture.Bounds;
            this.rectangle = new Rectangle(rectangle.Location, new Point(rectangle.Width * (int)transform._scale, rectangle.Height * (int)transform._scale));
            velocity.Y += 1;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Y += 1;
            rectangle.Offset(velocity);
            transform.SyncRect(rectangle);
        }

        internal void Land(Rectangle landSurface)
        {
            rectangle.Y = (int)(landSurface.Top - rectangle.Height) + 1;
            velocity.Y = 0;
            transform.SyncRect(rectangle);
        }


        internal void StandOn(Rectangle standSurface)
        {
            velocity.Y -= 1;
        }

        public void SetVelocity(float x, float y)
        {
            velocity = new Vector2(x, y);
        }

        public void AddVelocity(int x, int y)
        {
            velocity.X += x;
            velocity.Y += y;
        }

        public bool InBoundsLeft()
        {
            if(rectangle.Left > 0 + rectangle.Width / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InBoundsRight(GraphicsDeviceManager graphics)
        {
            if (rectangle.Right <= graphics.PreferredBackBufferWidth - rectangle.Width / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InBoundsTop()
        {
            if (rectangle.Top >= 0)
            {
                return true;
            }
            else
            {
                rectangle.Y = 0;
                return false;
            }
        }

        public bool InBoundsBottom(GraphicsDeviceManager graphics)
        {
            if (rectangle.Bottom <= graphics.PreferredBackBufferHeight - rectangle.Height / 2)
            {
                return true;
            }
            else
            {
                velocity.Y = 0;
                return false;
            }
        }
    }
}
