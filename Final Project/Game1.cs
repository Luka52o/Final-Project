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
            IsMouseVisible = false;
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
            messHallRoom,
            securityRoom,
            cargoBayRoom,
            hallwayARoom,
            hallwayBRoom,
            hallwayCRoom,
            hallwayDRoom,
            hallwayERoom,
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
        Room currentRoom, prevRoom; // prevRoom will be used to decide which end of a hallway the rooms should be based on where the player is coming from
        SpriteFont titleReportFont, generalTextFont;
        MouseState newMouseState, oldMouseState;
        KeyboardState keyboardState;

        Texture2D yourEscapePodTexture, dockingBayTexture, escapePodBayTexture, cargoBayTexture, hallway1Texture, hallway2Texture, residenceRoom1Texture, residenceRoom2Texture, messHallRoomTexture, securityRoomTexture, medBayRoomTexture, engineRoomTexture, reactorRoomTexture, logRoomTexture, commRoomTexture, elevatorRoomTexture, captainsQuartersRoomTexture, bridgeTexture, titleScreen;
        Texture2D rectangleButtonTexture, circleIconTexture, storyPanel1, storyPanel2, closeButtonTexture, xunariMapIconTexture, miniMapTexture, miniMapCurrentRoomTexture, moveCursorLeft, moveCursorRight, moveCursorUp, moveCursorDown, circleCursor;
        Texture2D DBmove1, DBmove2, DBmove3;

        Rectangle backgroundRect, startButtonRect, storyTextBox, mapButtonRect, closeButtonRect, escapePodMapIconRect, xunariMapRect, miniMapRect, miniMapCurrentRoomRect, moveRoomRect1, moveRoomRect2, moveRoomRect3, moveRoomRect4;
        int storyPanelCount = 0, iconBlinkCounter;
        float timeStamp, elapsedTimeSec;
        bool travelToXunariPrompt = false, onXunari = false;

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1275;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            backgroundRect = new Rectangle(0, 0, 1275, 1000);

            startButtonRect = new Rectangle(520, 515, 220, 110);
            mapButtonRect = new Rectangle(1115, 855, 150, 130);
            closeButtonRect = new Rectangle(1610, 0, 50, 50);

            storyTextBox = new Rectangle(250, 50, 800, 800);

            moveRoomRect1 = new Rectangle();
            moveRoomRect2 = new Rectangle();
            moveRoomRect3 = new Rectangle();
            moveRoomRect4 = new Rectangle();

            escapePodMapIconRect = new Rectangle(700, 810, 20, 20);
            xunariMapRect = new Rectangle(575, 360, 150, 300);
            miniMapRect = new Rectangle(1000, 705, 275, 275);
            miniMapCurrentRoomRect = new Rectangle();

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
            escapePodBayTexture = Content.Load<Texture2D>("escapePodBayRoom");
            cargoBayTexture = Content.Load<Texture2D>("cargoBayRoom");
            hallway1Texture = Content.Load<Texture2D>("hallwayRoom1");
            hallway2Texture = Content.Load<Texture2D>("hallwayRoom2");
            residenceRoom1Texture = Content.Load<Texture2D>("residenceRoom1");
            residenceRoom2Texture = Content.Load<Texture2D>("residenceRoom2");
            messHallRoomTexture = Content.Load<Texture2D>("messHallRoom");
            securityRoomTexture = Content.Load<Texture2D>("securityRoom");
            medBayRoomTexture = Content.Load<Texture2D>("medBayRoom");
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
            miniMapTexture = Content.Load<Texture2D>("miniMap");
            miniMapCurrentRoomTexture = Content.Load<Texture2D>("rectangle");
            moveCursorLeft = Content.Load<Texture2D>("moveCursorL");
            moveCursorRight = Content.Load<Texture2D>("moveCursorR");
            moveCursorUp = Content.Load<Texture2D>("moveCursorU");
            moveCursorDown = Content.Load<Texture2D>("moveCursorD");
            circleCursor = Content.Load<Texture2D>("circleCursor");

            // FONTS
            titleReportFont = Content.Load<SpriteFont>("1942Font");
            generalTextFont = Content.Load<SpriteFont>("hammerKeysFont");

            // DEBUG
            DBmove1 = Content.Load<Texture2D>("DBmoveRoom1");
            DBmove2 = Content.Load<Texture2D>("DBmoveRoom2");
            DBmove3 = Content.Load<Texture2D>("DBmoveRoom3");
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
                if (newMouseState.LeftButton == ButtonState.Pressed && startButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
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
                    if (newMouseState.LeftButton == ButtonState.Pressed && mapButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                    {
                        screen = Screen.EscapePodMap;
                    }
                }

                else if (currentRoom == Room.dockingBayRoom)
                    UpdateDockingBay();

                else if (currentRoom == Room.escapePodBayRoom)
                    UpdateEscapePodBay();

                else if (currentRoom == Room.hallwayARoom)
                    UpdateHallwayA();

                else if (currentRoom == Room.hallwayBRoom)
                    UpdateHallwayB();

                else if (currentRoom == Room.residenceRoom1)
                    UpdateResidence1();

                else if (currentRoom == Room.cargoBayRoom)
                    UpdateCargoBay();

                else if (currentRoom == Room.messHallRoom)
                    UpdateMessHall();

                else if (currentRoom == Room.residenceRoom2)
                    UpdateResidence2();

                else if (currentRoom == Room.securityRoom)
                    UpdateSecurityRoom();

                else if (currentRoom == Room.engineRoom)
                    UpdateEngineRoom();

                else if (currentRoom == Room.reactorRoom)
                    UpdateReactorRoom();

                else if (currentRoom == Room.commRoom)
                    UpdateCommRoom();

                else if (currentRoom == Room.hallwayCRoom)
                    UpdateHallwayC();

                else if (currentRoom == Room.logRoom)
                    UpdateLogRoom();

                else if (currentRoom == Room.hallwayDRoom)
                    UpdateHallwayD();

                else if (currentRoom == Room.medBayRoom)
                    UpdateMedBay();

                else if (currentRoom == Room.travelling)
                {
                    elapsedTimeSec = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;
                    if (elapsedTimeSec >= 4)
                    {
                        currentRoom = Room.dockingBayRoom;
                        onXunari = true;
                    }
                }
            }
            else if (screen == Screen.EscapePodMap)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && xunariMapRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    travelToXunariPrompt = true;
                }
                if (travelToXunariPrompt && keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Game;
                    currentRoom = Room.dockingBayRoom; // change this back to Room.travelling when done debugging
                    onXunari = true; // and delete this line
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

            if (onXunari)
            {
                iconBlinkCounter++;
            }

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

                else if (currentRoom == Room.dockingBayRoom)
                {
                    _spriteBatch.Draw(dockingBayTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.escapePodBayRoom)
                {
                    _spriteBatch.Draw(escapePodBayTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorRight, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.hallwayARoom)
                {
                    _spriteBatch.Draw(hallway1Texture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.hallwayBRoom)
                {
                    _spriteBatch.Draw(hallway1Texture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.residenceRoom1)
                {
                    _spriteBatch.Draw(residenceRoom1Texture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorRight, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    RunMapBlinker();
                }

                else if (currentRoom == Room.cargoBayRoom)
                {
                    _spriteBatch.Draw(cargoBayTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorRight, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorLeft, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.messHallRoom)
                {
                    _spriteBatch.Draw(messHallRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorRight, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorLeft, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.residenceRoom2)
                {
                    _spriteBatch.Draw(residenceRoom2Texture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.engineRoom)
                {
                    _spriteBatch.Draw(engineRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorRight, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorLeft, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.securityRoom)
                {
                    _spriteBatch.Draw(securityRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorRight, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorLeft, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.reactorRoom)
                {
                    _spriteBatch.Draw(reactorRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorLeft, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.hallwayDRoom)
                {
                    _spriteBatch.Draw(hallway1Texture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.commRoom)
                {
                    _spriteBatch.Draw(commRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorLeft, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.hallwayCRoom)
                {
                    _spriteBatch.Draw(hallway1Texture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.logRoom)
                {
                    _spriteBatch.Draw(logRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);

                    RunMapBlinker();
                }

                else if (currentRoom == Room.medBayRoom)
                {
                    _spriteBatch.Draw(medBayRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    
                    RunMapBlinker();
                }
            }

            //_spriteBatch.Draw(DBmove1, moveRoomRect1, Color.White);
            //_spriteBatch.Draw(DBmove2, moveRoomRect2, Color.White);
            //_spriteBatch.Draw(DBmove3, moveRoomRect3, Color.White);

            if (!moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && !moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && !moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && !moveRoomRect4.Contains(newMouseState.X, newMouseState.Y))
            {
                _spriteBatch.Draw(circleCursor, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void RunMapBlinker()
        {
            if (iconBlinkCounter < 60)
            {
                _spriteBatch.Draw(miniMapCurrentRoomTexture, miniMapCurrentRoomRect, Color.Black);
            }
            else if (iconBlinkCounter > 60 && iconBlinkCounter < 120)
            {
                _spriteBatch.Draw(miniMapCurrentRoomTexture, miniMapCurrentRoomRect, Color.DarkRed);
            }
            else if (iconBlinkCounter == 120)
                iconBlinkCounter = 0;
        }

        public void UpdateDockingBay()
        {
            miniMapCurrentRoomRect = new Rectangle(1024, 942, 234, 31);
            moveRoomRect1 = new Rectangle(500, 400, 300, 500);
            moveRoomRect2 = new Rectangle(); // when room jumping is done, these blank rectangles can all be removed since they wont do anything when clicked anyways
            moveRoomRect3 = new Rectangle();

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.escapePodBayRoom;
            }
        }

        public void UpdateEscapePodBay()
        {
            miniMapCurrentRoomRect = new Rectangle(1032, 895, 30, 30);
            moveRoomRect1 = new Rectangle(270, 150, 300, 500);
            moveRoomRect2 = new Rectangle(950, 200, 300, 500);
            moveRoomRect3 = new Rectangle(500, 780, 500, 215);
            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayARoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayBRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.dockingBayRoom;
            }
        }

        public void UpdateHallwayA()
        {
            miniMapCurrentRoomRect = new Rectangle(1040, 840, 14, 49);
            moveRoomRect1 = new Rectangle(585, 150, 300, 500);
            moveRoomRect2 = new Rectangle();
            moveRoomRect3 = new Rectangle(500, 780, 500, 215);

            if (prevRoom == Room.escapePodBayRoom)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.residenceRoom1;
                }
                else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.escapePodBayRoom;
                }
            }
            else if (prevRoom == Room.residenceRoom1)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.escapePodBayRoom;
                }
                else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.residenceRoom1;
                }
            }
            
        }

        public void UpdateHallwayB()
        {
            miniMapCurrentRoomRect = new Rectangle(1069, 903, 49, 14);
            moveRoomRect1 = new Rectangle(720, 150, 300, 500);
            moveRoomRect2 = new Rectangle();
            moveRoomRect3 = new Rectangle(500, 780, 500, 215);

            if (prevRoom == Room.escapePodBayRoom)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.cargoBayRoom;
                }
                else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.escapePodBayRoom;
                }
            }
            else if (prevRoom == Room.cargoBayRoom)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.escapePodBayRoom;
                }
                else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.cargoBayRoom;
                }
            }
        }

        public void UpdateCargoBay()
        {
            miniMapCurrentRoomRect = new Rectangle(1126, 895, 30, 30);
            moveRoomRect1 = new Rectangle(950, 190, 300, 500); // right side long
            moveRoomRect2 = new Rectangle(30, 180, 300, 500); // left side long
            moveRoomRect3 = new Rectangle();
            // moveRoomRect3 = new Rectangle(375, 400, 500, 215); // middle wide

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.engineRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayBRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.logRoom;
            }
        }

        public void UpdateEngineRoom()
        {
            miniMapCurrentRoomRect = new Rectangle(1170, 895, 30, 30);
            moveRoomRect1 = new Rectangle(820, 150, 300, 500);
            moveRoomRect2 = new Rectangle(30, 180, 300, 500);
            moveRoomRect3 = new Rectangle();

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.reactorRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.cargoBayRoom;
            }
        }

        public void UpdateReactorRoom()
        {
            miniMapCurrentRoomRect = new Rectangle(1215, 895, 30, 30);
            moveRoomRect1 = new Rectangle(375, 100, 500, 215); // top wide
            moveRoomRect2 = new Rectangle(30, 180, 300, 500); // left side long
            moveRoomRect3 = new Rectangle();

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.commRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.engineRoom;
            }
        }

        public void UpdateResidence1()
        {
            miniMapCurrentRoomRect = new Rectangle(1032, 807, 30, 30);
            moveRoomRect1 = new Rectangle(920, 150, 300, 500);
            moveRoomRect2 = new Rectangle();
            moveRoomRect3 = new Rectangle(500, 780, 500, 215);

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.messHallRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayARoom;
            }
        }

        public void UpdateMessHall()
        {
            miniMapCurrentRoomRect = new Rectangle(1069, 807, 50, 84);
            moveRoomRect1 = new Rectangle(920, 150, 300, 500);
            moveRoomRect2 = new Rectangle(30, 180, 300, 500); // left side long
            moveRoomRect3 = new Rectangle(375, 100, 500, 215); // top wide

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.securityRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.residenceRoom1;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.residenceRoom2;
            }
        }

        public void UpdateResidence2()
        {
            miniMapCurrentRoomRect = new Rectangle(1079, 767, 30, 30);
            moveRoomRect1 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect2 = new Rectangle();
            moveRoomRect3 = new Rectangle();

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.messHallRoom;
            }
        }

        public void UpdateCommRoom()
        {
            miniMapCurrentRoomRect = new Rectangle(1217, 849, 30, 30);
            moveRoomRect1 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect2 = new Rectangle(30, 180, 300, 500); // left side long
            moveRoomRect3 = new Rectangle(375, 100, 500, 215); // top wide

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.reactorRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayCRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.medBayRoom;
            }
        }

        public void UpdateHallwayC()
        {
            miniMapCurrentRoomRect = new Rectangle(1161, 858, 49, 14);
            moveRoomRect1 = new Rectangle(720, 150, 300, 500);
            moveRoomRect2 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect3 = new Rectangle();

            if (prevRoom == Room.commRoom)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.logRoom;
                }
                else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.commRoom;
                }
            }
            else if (prevRoom == Room.logRoom)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.commRoom;
                }
                else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.logRoom;
                }
            }
        }

        public void UpdateLogRoom()
        {
            miniMapCurrentRoomRect = new Rectangle(1126, 850, 30, 30);
            moveRoomRect1 = new Rectangle(920, 150, 300, 500); // left side long
            moveRoomRect2 = new Rectangle(375, 100, 500, 215); // top wide
            moveRoomRect3 = new Rectangle();

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayCRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.securityRoom;
            }
        }

        public void UpdateMedBay()
        {
            miniMapCurrentRoomRect = new Rectangle(1216, 806, 30, 30);
            moveRoomRect1 = new Rectangle(30, 180, 300, 500); // left side long
            moveRoomRect2 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect3 = new Rectangle(950, 190, 300, 500); // right side long


            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayDRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.commRoom;
            }
            //else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            //{
            //    prevRoom = currentRoom;
            //    currentRoom = Room.elevatorRoom;
            //}
        }

        public void UpdateHallwayD()
        {
            miniMapCurrentRoomRect = new Rectangle(1162, 815, 49, 14);
            moveRoomRect1 = new Rectangle(720, 150, 300, 500);
            moveRoomRect2 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect3 = new Rectangle();

            if (prevRoom == Room.securityRoom)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.medBayRoom;
                }
                else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.securityRoom;
                }
            }
            else if (prevRoom == Room.medBayRoom)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.securityRoom;
                }
                else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    prevRoom = currentRoom;
                    currentRoom = Room.medBayRoom;
                }
            }
        }

        public void UpdateSecurityRoom()
        {
            miniMapCurrentRoomRect = new Rectangle(1126, 807, 30, 30);
            moveRoomRect1 = new Rectangle(950, 190, 300, 500); // right side long
            moveRoomRect2 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect3 = new Rectangle(30, 180, 300, 500); // left side long
            moveRoomRect4 = new Rectangle(); // will need to do this, drawing is wrong

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayDRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.logRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.messHallRoom;
            }
        }
    }
}