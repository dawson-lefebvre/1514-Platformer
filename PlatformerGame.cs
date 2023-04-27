using DMIT1514_Lab06_Platformer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PlatformerGame
{
    public class PlatformerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int GameScale = 4;

        Texture2D playerTexture;
        Texture2D tile;
        Actor player;
        bool isGrounded;

        List<Collider> groundPlatforms = new List<Collider>();
        int groundTileCount = 10;

        int ascendingTileCount = 6;

        Texture2D coinText;
        List<Collectable> coins = new List<Collectable>();

        Transform platTransform;
        Transform playerTransform;

        Point playerSpawn;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            Window.Title = "Platformer Game";
            _graphics.PreferredBackBufferWidth = 360 * GameScale;
            _graphics.PreferredBackBufferHeight = 240 * GameScale;
            _graphics.ApplyChanges();

            playerSpawn = new(20 * GameScale, 20 * GameScale);
            playerTransform = new Transform(playerSpawn.ToVector2(), 0, GameScale);
            platTransform = new Transform(new Vector2(20 * GameScale, 200 * GameScale), 0, GameScale);

            player = new Actor(this, playerTransform, playerTexture);
            for (int colldierIndex = 0; colldierIndex < groundTileCount; colldierIndex++)
            {
                Collider newPlatform = new Collider(this, platTransform, tile);
                newPlatform.rectangle.Location = new Point(colldierIndex * newPlatform.rectangle.Width, _graphics.PreferredBackBufferHeight - (20 * GameScale));
                newPlatform.transform.SyncRect(newPlatform.rectangle);
                groundPlatforms.Add(newPlatform);
            }

            for (int platformIndex = 1; platformIndex <= ascendingTileCount; platformIndex++)
            {
                Collider newPlatform1 = new Collider(this, platTransform, tile);
                newPlatform1.rectangle.Location = new Point(platformIndex * newPlatform1.rectangle.Width + ((40 * (platformIndex)) * GameScale), _graphics.PreferredBackBufferHeight - (20 * GameScale) * platformIndex);
                newPlatform1.transform.SyncRect(newPlatform1.rectangle);
                groundPlatforms.Add(newPlatform1);

                Collectable coin1 = new Collectable(this, platTransform, coinText);
                coin1.rectangle.Location = new Point(platformIndex * newPlatform1.rectangle.Width + ((40 * (platformIndex)) * GameScale), newPlatform1.rectangle.Top - (2 * GameScale) - coin1.rectangle.Height);
                coin1.transform.SyncRect(coin1.rectangle);
                coins.Add(coin1);

                Collider newPlatform2 = new Collider(this, platTransform, tile);
                newPlatform2.rectangle.Location = new Point(platformIndex * newPlatform2.rectangle.Width + ((40 * (platformIndex)) * GameScale) + newPlatform2.rectangle.Width, _graphics.PreferredBackBufferHeight - (20 * GameScale) * platformIndex);
                newPlatform2.transform.SyncRect(newPlatform2.rectangle);
                newPlatform2.transform.SyncRect(newPlatform2.rectangle);
                groundPlatforms.Add(newPlatform2);

                Collectable coin2 = new Collectable(this, platTransform, coinText);
                coin2.rectangle.Location = new Point(platformIndex * newPlatform2.rectangle.Width + ((40 * (platformIndex)) * GameScale), newPlatform2.rectangle.Top - (2 * GameScale) - coin2.rectangle.Height);
                coin2.transform.SyncRect(coin2.rectangle);
                coins.Add(coin2);
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("little_guy");
            tile = Content.Load<Texture2D>("platform");
            coinText = Content.Load<Texture2D>("heart");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //player.transform.SetPosition(Mouse.GetState().X,Mouse.GetState().Y);
                player.SetVelocity(0, 0);
                player.rectangle.Location = Mouse.GetState().Position;
            }

            foreach (Collider collider in groundPlatforms)
            {
                isGrounded = collider.ProcessCollision(player);
                if (isGrounded)
                {
                    break;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (player.InBoundsLeft())
                {
                    player.SetVelocity(-5, player.velocity.Y);
                }
                else
                {
                    player.SetVelocity(0, player.velocity.Y);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (player.InBoundsRight(_graphics))
                {
                    player.SetVelocity(5, player.velocity.Y);
                }
            }
            else
            {
                player.SetVelocity(0, player.velocity.Y);
            }

            player.InBoundsBottom(_graphics);

            if (player.rectangle.Top > _graphics.PreferredBackBufferHeight)
            {
                player.rectangle.Location = playerSpawn;
            }

            Debug.WriteLine(player.rectangle.Location);

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && isGrounded)
            {
                player.SetVelocity(player.velocity.X, -20);
                isGrounded = false;
            }
            // TODO: Add your update logic here

            foreach (Collectable coin in coins)
            {
                if(coin.currentState == Collectable.State.Enabled)
                {
                    coin.ProcessCollision(player);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}