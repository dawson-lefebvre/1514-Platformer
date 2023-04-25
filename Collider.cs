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
    public class Collider : GameObject
    {
        public enum ColliderType
        {
            left, Right, Top, Bottom
        }

        protected ColliderType type;

        public Collider(Game game, Transform transform, Texture2D texture) : base(game, transform, texture)
        {
            this.transform = base.transform;
            this.texture = base.texture;
            this.rectangle = this.texture.Bounds;

            this.rectangle.Location = this.transform._position.ToPoint();
            this.rectangle = new Rectangle(rectangle.Location, new Point(rectangle.Width * (int)transform._scale, rectangle.Height * (int)transform._scale));

            type = ColliderType.Top;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        internal bool ProcessCollision(Actor actor)
        {
            bool grounded = false;
            if (rectangle.Intersects(actor.rectangle))
            {
                if (actor.rectangle.Bottom > rectangle.Top && actor.rectangle.Bottom < rectangle.Bottom)
                {
                    grounded = true;
                    actor.Land(rectangle);
                    actor.StandOn(rectangle);
                }
                else if (actor.rectangle.Top < rectangle.Bottom && actor.rectangle.Top > rectangle.Top)
                {
                    actor.rectangle.Y = (int)(rectangle.Bottom) - 1;
                    actor.velocity.Y = 0;
                    actor.transform.SyncRect(rectangle);
                }
                else if (actor.rectangle.Right > rectangle.Left && actor.rectangle.Right < rectangle.Right)
                {
                    actor.rectangle.X = (int)(rectangle.Left - rectangle.Width) + 1;
                    actor.velocity.X = 0;
                    actor.transform.SyncRect(rectangle);
                }
                else if (actor.rectangle.Left < rectangle.Right && actor.rectangle.Left > rectangle.Left)
                {
                    actor.rectangle.X = (int)(rectangle.Right) + 1;
                    actor.velocity.X = 0;
                    actor.transform.SyncRect(rectangle);
                }
            }
            return grounded;
        }
    }
}
