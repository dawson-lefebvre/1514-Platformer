using DMIT1514_Lab06_Platformer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
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
        Collider collider;
        bool inBlock;

        List<Collider> platforms = new List<Collider>();

        Transform platTransform;
        Transform playerTransform;


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
            playerTransform = new Transform(new Vector2(20 * GameScale, 0 * GameScale), 0, GameScale);
            platTransform = new Transform(new Vector2(20 * GameScale, 200 * GameScale), 0, GameScale);

            player = new Actor(this, playerTransform, playerTexture);
            collider = new Collider(this, platTransform, tile);
            platforms.Add(collider);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("little_guy");
            tile = Content.Load<Texture2D>("platform");
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

            foreach (Collider collider in platforms)
            {
                inBlock = collider.ProcessCollision(player);
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && !inBlock)
            {
                player.AddVelocity(0, -4);
            }
            // TODO: Add your update logic here

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