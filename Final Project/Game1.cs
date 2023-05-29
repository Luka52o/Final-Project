using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        enum Screen
        {
            Title,
            Game,
            EscapePodMap,
        }
        enum Room
        {
            yourEscapePodRoom,
            cargoBayRoom,
            hallway1Room,
            hallway2Room,
            engineRoom,
            logRoom,
            commRoom,
            bridgeRoom,
        }


        Screen screen;
        Room currentRoom;
        Texture2D yourEscapePodTexture, cargoBayTexture, hallway1Texture, hallway2Texture, engineRoomTexture, logRoomTexture, commRoomTexture, bridgeTexture, titleScreen;
        Rectangle backgroundRect;

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1366;
            _graphics.PreferredBackBufferHeight = 768;

            backgroundRect = new Rectangle(0, 0, 1366, 768);

            screen = Screen.Title;

            base.Initialize();


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            yourEscapePodTexture = Content.Load<Texture2D>("yourEscapePodRoom");
            cargoBayTexture = Content.Load<Texture2D>("cargoBayRoom");
            hallway1Texture = Content.Load<Texture2D>("hallwayRoom1");
            hallway2Texture = Content.Load<Texture2D>("hallwayRoom2");
            engineRoomTexture = Content.Load<Texture2D>("engineRoom");
            logRoomTexture = Content.Load<Texture2D>("logRoom");
            commRoomTexture = Content.Load<Texture2D>("commRoom");
            bridgeTexture = Content.Load<Texture2D>("bridgeRoom");
            titleScreen = Content.Load<Texture2D>("titleScreen");


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleScreen, backgroundRect, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}