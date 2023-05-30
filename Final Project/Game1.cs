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
            _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        enum Screen
        {
            Title,
            Story,
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
        SpriteFont titleReportFont, generalTextFont;
        MouseState mouseState;
        KeyboardState keyboardState;

        Texture2D yourEscapePodTexture, cargoBayTexture, hallway1Texture, hallway2Texture, engineRoomTexture, logRoomTexture, commRoomTexture, bridgeTexture, titleScreen;
        Texture2D rectangleButton, storyPanel1, storyPanel2;
        Rectangle backgroundRect, startButtonRect, storyTextBox;
        int storyPanelCount = 0;

        protected override void Initialize()
        {
            backgroundRect = new Rectangle(0, 0, 1200, 600);
            startButtonRect = new Rectangle(350, 300, 150, 75);
            storyTextBox = new Rectangle(0, 0, 800, 800);
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

            rectangleButton = Content.Load<Texture2D>("rectangle");
            storyPanel1 = Content.Load<Texture2D>("storyPanel1");
            storyPanel2 = Content.Load<Texture2D>("storyPanel2");

            titleReportFont = Content.Load<SpriteFont>("1942Font");
            generalTextFont = Content.Load<SpriteFont>("hammerKeysFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();

            if (screen == Screen.Title)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && startButtonRect.Contains(mouseState.X, mouseState.Y))
                {
                    screen = Screen.Story;
                }
            }
            else if (screen == Screen.Story)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    storyPanelCount++;
                }
                if (storyPanelCount == 2)
                {
                    screen = Screen.Game;
                    currentRoom = Room.yourEscapePodRoom;
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleScreen, backgroundRect, Color.White);
                _spriteBatch.Draw(rectangleButton, startButtonRect, Color.Black);
                _spriteBatch.DrawString(titleReportFont, "The Xunari", new Vector2(240, 150), Color.Olive);
                _spriteBatch.DrawString(generalTextFont, "Start", new Vector2(393, 305), Color.Olive);
            }
            else if (screen == Screen.Story)
            {
                _spriteBatch.Draw(rectangleButton, backgroundRect, Color.Black);
                if (storyPanelCount == 0)
                {
                    _spriteBatch.Draw(storyPanel1, storyTextBox, Color.Black);
                }
                else if (storyPanelCount == 1)
                {
                    _spriteBatch.Draw(storyPanel2, storyTextBox, Color.Black);
                }
            }
            if (screen == Screen.Game)
            {
                if (currentRoom == Room.yourEscapePodRoom)
                {
                    _spriteBatch.Draw(yourEscapePodTexture, backgroundRect, Color.White);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}