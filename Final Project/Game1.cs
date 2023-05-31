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
            dockingBayRoom,
            escapePodBayRoom,
            residenceRoom1,
            residenceRoom2,
            securityRoom,
            cargoBayRoom,
            hallway1Room,
            hallway2Room,
            medBayRoom,
            engineRoom,
            reactorRoom,
            logRoom,
            commRoom,
            elevatorRoom,
            captainsQuartersRoom,
            bridgeRoom,
        }


        Screen screen;
        Room currentRoom;
        SpriteFont titleReportFont, generalTextFont;
        MouseState newMouseState, oldMouseState;
        KeyboardState keyboardState;

        Texture2D yourEscapePodTexture, dockingBayTexture, cargoBayTexture, hallway1Texture, hallway2Texture, residenceRoom1Texture, residenceRoom2Texture, messHallRoomTexture, securityRoomTexture, engineRoomTexture, reactorRoomTexture, logRoomTexture, commRoomTexture, elevatorRoomTexture, captainsQuartersRoomTexture, bridgeTexture, titleScreen;
        Texture2D rectangleButtonTexture, circleIconTexture, storyPanel1, storyPanel2, closeButtonTexture, xunariMapIconTexture;
        Rectangle backgroundRect, startButtonRect, storyTextBox, mapButtonRect, closeButtonRect, escapePodMapIconRect, xunariMapRect, miniMapRect, miniMapCurrentRoomIcon;
        int storyPanelCount = 0, iconBlinkCounter;
        float timeStamp, elapsedTimeSec;
        bool travelToXunariPrompt = false;

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1275;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            backgroundRect = new Rectangle(0, 0, 1275, 1000);
            startButtonRect = new Rectangle(520, 515, 220, 110);
            mapButtonRect = new Rectangle(1115, 855, 150, 130);
            storyTextBox = new Rectangle(250, 50, 800, 800);
            closeButtonRect = new Rectangle(1610, 0, 50, 50);
            escapePodMapIconRect = new Rectangle(700, 810, 20, 20);
            xunariMapRect = new Rectangle(575, 360, 150, 300);
            storyPanelCount = 0;
            screen = Screen.Title;

            base.Initialize();


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // ROOM BACKGROUND TEXTURES
            yourEscapePodTexture = Content.Load<Texture2D>("yourEscapePodRoom");
            dockingBayTexture = Content.Load<Texture2D>("dockingBayRoom");
            cargoBayTexture = Content.Load<Texture2D>("cargoBayRoom");
            hallway1Texture = Content.Load<Texture2D>("hallwayRoom1");
            hallway2Texture = Content.Load<Texture2D>("hallwayRoom2");
            residenceRoom1Texture = Content.Load<Texture2D>("residenceRoom1");
            residenceRoom2Texture = Content.Load<Texture2D>("residenceRoom2");
            messHallRoomTexture = Content.Load<Texture2D>("messHallRoom");
            securityRoomTexture = Content.Load<Texture2D>("securityRoom");
            engineRoomTexture = Content.Load<Texture2D>("engineRoom");
            reactorRoomTexture = Content.Load<Texture2D>("reactorRoom");
            logRoomTexture = Content.Load<Texture2D>("logRoom");
            commRoomTexture = Content.Load<Texture2D>("commRoom");
            elevatorRoomTexture = Content.Load<Texture2D>("elevatorRoom");
            captainsQuartersRoomTexture = Content.Load<Texture2D>("captainsQuartersRoom");
            bridgeTexture = Content.Load<Texture2D>("bridgeRoom");
            titleScreen = Content.Load<Texture2D>("titleScreen");

            // ICONS, BUTTONS, & CURSORS
            rectangleButtonTexture = Content.Load<Texture2D>("rectangle");
            circleIconTexture = Content.Load<Texture2D>("circle");
            closeButtonTexture = Content.Load<Texture2D>("closeButton");
            storyPanel1 = Content.Load<Texture2D>("storyPanel1");
            storyPanel2 = Content.Load<Texture2D>("storyPanel2");
            xunariMapIconTexture = Content.Load<Texture2D>("xunariMapIcon");

            // FONTS
            titleReportFont = Content.Load<SpriteFont>("1942Font");
            generalTextFont = Content.Load<SpriteFont>("hammerKeysFont");
        }


        // //////////////////////////////////////////////
        // ////////////////////////////////////////////// UPDATE
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            newMouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

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
                if (currentRoom == Room.travelling)
                {
                    elapsedTimeSec = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;
                    if (elapsedTimeSec >= 4)
                    {
                        currentRoom = Room.dockingBayRoom;
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
                    timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                    travelToXunariPrompt = false;
                }
            }

            oldMouseState = newMouseState;
            base.Update(gameTime);
        }


        // ////////////////////////////////////////
        // //////////////////////////////////////// DRAW
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleScreen, backgroundRect, Color.White);
                _spriteBatch.Draw(rectangleButtonTexture, startButtonRect, Color.Black);
                _spriteBatch.DrawString(titleReportFont, "The Xunari", new Vector2(387, 400), Color.Olive);
                _spriteBatch.DrawString(generalTextFont, "Start", new Vector2(580, 525), Color.Olive);
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
                    _spriteBatch.DrawString(generalTextFont, "Travel to The Xunari? [ENTER]", new Vector2(440, 650), Color.Olive);
                }
                iconBlinkCounter++;
            }
            else if (screen == Screen.Game)
            {
                if (currentRoom == Room.yourEscapePodRoom)
                {
                    _spriteBatch.Draw(yourEscapePodTexture, backgroundRect, Color.White);

                    _spriteBatch.Draw(rectangleButtonTexture, mapButtonRect, Color.Black);
                    _spriteBatch.DrawString(generalTextFont, "MAP", new Vector2(1155, 885), Color.Olive);
                }
                else if (currentRoom == Room.travelling)
                {

                }
                else if (currentRoom == Room.dockingBayRoom)
                {
                    _spriteBatch.Draw(dockingBayTexture, backgroundRect, Color.White);
                }

            }
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}