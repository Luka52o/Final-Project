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
            Story,
            Game,
            EscapePodMap,
        }
        enum Room
        {
            yourEscapePodRoom,
            travelling,
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
        MouseState newMouseState, oldMouseState;
        KeyboardState keyboardState;

        Texture2D yourEscapePodTexture, cargoBayTexture, hallway1Texture, hallway2Texture, engineRoomTexture, logRoomTexture, commRoomTexture, bridgeTexture, titleScreen;
        Texture2D rectangleButtonTexture, circleIconTexture, storyPanel1, storyPanel2, closeButtonTexture, xunariMapIconTexture;
        Rectangle backgroundRect, startButtonRect, storyTextBox, mapButtonRect, closeButtonRect, escapePodMapIconRect, xunariMapRect;
        int storyPanelCount = 0, iconBlinkCounter;
        bool travelToXunariPrompt = false;

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            backgroundRect = new Rectangle(0, 0, 1920, 1080);
            startButtonRect = new Rectangle(810, 515, 300, 150);
            mapButtonRect = new Rectangle(1720, 955, 150, 75);
            storyTextBox = new Rectangle(380, -75, 1280, 1280);
            closeButtonRect = new Rectangle(1610, 0, 50, 50);
            escapePodMapIconRect = new Rectangle(1000, 1000, 20, 20);
            xunariMapRect = new Rectangle(840, 220, 334, 582);
            storyPanelCount = 0;
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

            rectangleButtonTexture = Content.Load<Texture2D>("rectangle");
            circleIconTexture = Content.Load<Texture2D>("circle");
            closeButtonTexture = Content.Load<Texture2D>("closeButton");
            storyPanel1 = Content.Load<Texture2D>("storyPanel1");
            storyPanel2 = Content.Load<Texture2D>("storyPanel2");
            xunariMapIconTexture = Content.Load<Texture2D>("xunariMapIcon");

            titleReportFont = Content.Load<SpriteFont>("1942Font");
            generalTextFont = Content.Load<SpriteFont>("hammerKeysFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            newMouseState = Mouse.GetState();

            if (screen == Screen.Title)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && startButtonRect.Contains(newMouseState.X, newMouseState.Y))
                {
                    screen = Screen.Story;
                }
            }
            else if (screen == Screen.Story)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && newMouseState.LeftButton != oldMouseState.LeftButton)
                {
                    storyPanelCount++;
                }
                if (storyPanelCount == 2)
                {
                    screen = Screen.Game;
                    currentRoom = Room.yourEscapePodRoom;
                }
            }
            else if (screen == Screen.Game)
            {
                if (currentRoom == Room.yourEscapePodRoom)
                {
                    if (newMouseState.LeftButton == ButtonState.Pressed && mapButtonRect.Contains(newMouseState.X, newMouseState.Y))
                    {
                        screen = Screen.EscapePodMap;
                    }
                }
            }
            else if (screen == Screen.EscapePodMap)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && xunariMapRect.Contains(newMouseState.X, newMouseState.Y))
                {
                    travelToXunariPrompt = true;
                }
                if (travelToXunariPrompt && keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Game;
                    currentRoom = Room.travelling;
                }
            }

            oldMouseState = newMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleScreen, backgroundRect, Color.White);
                _spriteBatch.Draw(rectangleButtonTexture, startButtonRect, Color.Black);
                _spriteBatch.DrawString(titleReportFont, "The Xunari", new Vector2(687, 400), Color.Olive);
                _spriteBatch.DrawString(generalTextFont, "Start", new Vector2(910, 555), Color.Olive);
            }
            else if (screen == Screen.Story)
            {
                if (storyPanelCount == 0)
                {
                    _spriteBatch.Draw(storyPanel1, storyTextBox, Color.White);
                }
                else if (storyPanelCount == 1)
                {
                    _spriteBatch.Draw(storyPanel2, storyTextBox, Color.White);
                }
            }
            else if (screen == Screen.EscapePodMap)
            {
                _spriteBatch.Draw(yourEscapePodTexture, backgroundRect, Color.White);

                _spriteBatch.Draw(rectangleButtonTexture, storyTextBox, Color.Black);
                _spriteBatch.Draw(closeButtonTexture, closeButtonRect, Color.White);
                _spriteBatch.Draw(xunariMapIconTexture, xunariMapRect, Color.White);
                if (iconBlinkCounter > 60)
                {
                    _spriteBatch.Draw(circleIconTexture, escapePodMapIconRect, Color.White);
                }
                else if (iconBlinkCounter < 60)
                {
                    _spriteBatch.Draw(circleIconTexture, escapePodMapIconRect, Color.DarkRed);
                }
                if (iconBlinkCounter == 120)
                    iconBlinkCounter = 0;

                if (travelToXunariPrompt == true)
                {
                    _spriteBatch.DrawString(generalTextFont, "Travel to The Xunari? [ENTER]", new Vector2(840, 850), Color.Olive);
                }
                iconBlinkCounter++;
            }
            else if (screen == Screen.Game)
            {
                if (currentRoom == Room.yourEscapePodRoom)
                {
                    _spriteBatch.Draw(yourEscapePodTexture, backgroundRect, Color.White);

                    _spriteBatch.Draw(rectangleButtonTexture, mapButtonRect, Color.Black);
                    _spriteBatch.DrawString(generalTextFont, "MAP", new Vector2(1780, 925), Color.Olive);
                }
                else if (currentRoom == Room.cargoBayRoom)
                {
                    _spriteBatch.Draw(cargoBayTexture, backgroundRect, Color.White);
                }

            }
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}