using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Final_Project
{
    public enum Room
    {
        dockingBayRoom,   // 1
        escapePodBayRoom, // 2
        residenceRoom1,   // 3
        residenceRoom2,   // 4
        messHallRoom,     // 5
        securityRoom,     // 6
        cargoBayRoom,     // 7
        hallwayARoom,     // 8
        hallwayBRoom,     // 9
        hallwayCRoom,     // 10
        hallwayDRoom,     // 11
        hallwayERoom,     // 12
        medBayRoom,       // 13
        engineRoom,       // 14
        reactorRoom,      // 15
        logRoom,          // 16
        commRoom,         // 17
        yourEscapePodRoom,
        altEscapePodRoom,
        travelling,
        elevatorRoom,
        captainsQuartersRoom,
        bridgeRoom,
    }
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
            Hiding,
            end,
            outro,
            dead
        }
        

        enum Story
        {
            start,
            goToLogRoom,
            searchAltEscapePod,
            findKeyCard1,
            findKeyCard2,
            findKeyCard3,
            findKeyCard4,
            findKeyCard5,
            openBridge,
            end
        }

        Story activeTask;
        Screen screen;
        Song monsterHere, theXunari;
        SoundEffect heartbeatSlow, heartbeatMedium, heartbeatFast;
        SoundEffectInstance heartbeatSlowInstance, heartbeatMediumInstance, heartbeatFastInstance;
        Room currentRoom, prevRoom;
        Keys quickTimeLetter;
        SpriteFont titleReportFont, generalTextFont;
        MouseState newMouseState, oldMouseState;
        KeyboardState newKeyboardState, oldKeyboardState;
        public List<Rectangle> roomRects { get; set; } = new List<Rectangle>();
        public List<Room> rooms { get; set; } = new List<Room>();
        List<Keys> quicktimeLetters = new List<Keys>();

        Texture2D yourEscapePodTexture, altEscapePodTexture, dockingBayTexture, escapePodBayTexture, cargoBayTexture, hallway1Texture, hallway2Texture, residenceRoom1Texture, residenceRoom2Texture, messHallRoomTexture, securityRoomTexture, medBayRoomTexture, engineRoomTexture, reactorRoomTexture, logRoomTexture, commRoomTexture, elevatorRoomTexture, captainsQuartersRoomTexture, bridgeTexture, titleScreen, hidingTexture;
        Texture2D rectangleButtonTexture, circleIconTexture, storyPanel1, storyPanel2, closeButtonTexture, xunariMapIconTexture, miniMapTexture, miniMapCurrentRoomTexture, moveCursorLeft, moveCursorRight, moveCursorUp, moveCursorDown, circleCursor, BoxFrameTexture, logPanel1Texture, logPanel2Texture, escapePodPanel1Texture, escapePodPanel2Texture, escapePodPanel3Texture, escapePodPanel4Texture, blueKeyCard1Texture, redKeyCard2Texture, yellowKeyCard3Texture, orangeKeyCard4Texture, purpleKeyCard5Texture, cardTrackerTexture, bridgeStoryPanel1Texture, bridgeStoryPanel2Texture, panelButtonTexture, theManTexture;
        Texture2D DBmove1, DBmove2, DBmove3;
        Random generator = new Random();
        Monster monster1;
        Monster monster2;

        Point objectiveCardLocation;
        Rectangle backgroundRect, startButtonRect, storyTextBox, mapButtonRect, closeButtonRect, escapePodMapIconRect, xunariMapRect, miniMapRect, miniMapCurrentRoomRect, moveRoomRect1, moveRoomRect2, moveRoomRect3, moveRoomRect4, locationBoxRect, textBoxRect, storyButtonRect, keyCard1Rect, keyCard2Rect, keyCard3Rect, keyCard4Rect, keyCard5Rect, cardTrackerRect, trackerBlinkerRect, endStoryYesButtonRect, endStoryNoButtonRect, theManRect;
        Rectangle dockBayMapRect, podBayMapRect, res1MapRect, res2MapRect, messMapRect, secMapRect, cargoMapRect, hallAMapRect, hallBMapRect, hallCMapRect, hallDMapRect, hallEMapRect, medMapRect, engineMapRect, reactorMapRect, logMapRect, commMapRect, elevatorBlinkerRect;

        int storyPanelCount = 0, logPanelCount = 0, altEscapePodPanelCount = 0, iconBlinkCounter, cardTrackerBlinker, keyCardsHeld = 0, keyCard2Location, keyCard3Location, keyCard4Location, playerRoomNum, endStoryPanelCount = 0, endTextFlasherCount = 0, monsterTeleportTo, newHideI, oldHideI, quickTimeSuccessCounter;
        float timeStamp, elapsedTimeSec, monsterMoveTimeStamp, monsterMoveSec, timeToDieTimeStamp, timeToDieSec;
        double cardDistanceX, cardDistanceY, cardDistanceVector, monster1DistanceX, monster1DistanceY, monster1DistanceVector, monster2DistanceX, monster2DistanceY, monster2DistanceVector;
        bool travelToXunariPrompt = false, onXunari = false, readingShipLogs = false, inRoomWithKeyCard = false, endYesSelected = false, endNoSelected = false, quickTimeFirstRound = true, monsterHerePlaying = false, theXunariPlaying = false;

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1275;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            monster1 = new Monster();
            monster2 = new Monster();

            backgroundRect = new Rectangle(0, 0, 1275, 1000);
            theManRect = new Rectangle(200, 100, 875, 800);

            startButtonRect = new Rectangle(520, 515, 220, 110);
            mapButtonRect = new Rectangle(1115, 855, 150, 130);
            closeButtonRect = new Rectangle(1215, 10, 50, 50);

            storyTextBox = new Rectangle(250, 50, 800, 800);
            locationBoxRect = new Rectangle(10, 890, 250, 100);
            textBoxRect = new Rectangle(10, 10, 880, 60);
            storyButtonRect = new Rectangle(600, 700, 300, 100);

            moveRoomRect1 = new Rectangle();
            moveRoomRect2 = new Rectangle();
            moveRoomRect3 = new Rectangle();
            moveRoomRect4 = new Rectangle();

            keyCard1Rect = new Rectangle(975, 20, 50, 65);
            keyCard2Rect = new Rectangle(1035, 20, 50, 65);
            keyCard3Rect = new Rectangle(1095, 20, 50, 65);
            keyCard4Rect = new Rectangle(1155, 20, 50, 65);
            keyCard5Rect = new Rectangle(1215, 20, 50, 65);
            cardTrackerRect = new Rectangle(270, 890, 175, 80);
            trackerBlinkerRect = new Rectangle(cardTrackerRect.Center.X, cardTrackerRect.Center.Y, 20, 20);
            elevatorBlinkerRect = new Rectangle(1200, 150, 200, 500);

            escapePodMapIconRect = new Rectangle(700, 810, 20, 20);
            xunariMapRect = new Rectangle(575, 360, 150, 300);
            miniMapRect = new Rectangle(1000, 705, 275, 275);
            miniMapCurrentRoomRect = new Rectangle();

            storyPanelCount = 0;
            screen = Screen.Title;
            activeTask = Story.start;

            rooms.Add(Room.dockingBayRoom);
            rooms.Add(Room.escapePodBayRoom);
            rooms.Add(Room.residenceRoom1);
            rooms.Add(Room.residenceRoom2);
            rooms.Add(Room.messHallRoom);
            rooms.Add(Room.securityRoom);
            rooms.Add(Room.cargoBayRoom);
            rooms.Add(Room.hallwayARoom);
            rooms.Add(Room.hallwayBRoom);
            rooms.Add(Room.hallwayCRoom);
            rooms.Add(Room.hallwayDRoom);
            rooms.Add(Room.hallwayERoom);
            rooms.Add(Room.medBayRoom);
            rooms.Add(Room.engineRoom);
            rooms.Add(Room.reactorRoom);
            rooms.Add(Room.logRoom);
            rooms.Add(Room.commRoom);

            // key card location generation
            keyCard2Location = generator.Next(1, 18);
            keyCard3Location = generator.Next(1, 18);

            while (keyCard3Location == keyCard2Location)
                keyCard3Location = generator.Next(1, 18);

            keyCard4Location = generator.Next(1, 18);

            while (keyCard4Location == keyCard2Location || keyCard4Location == keyCard3Location)
                keyCard4Location = generator.Next(1, 18);

            LocateRoomMapRects();
            AddQuickTimeKeys();

            base.Initialize();


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // ROOM BACKGROUND TEXTURES
            yourEscapePodTexture = Content.Load<Texture2D>("yourEscapePodRoom");
            altEscapePodTexture = Content.Load<Texture2D>("escapePodRoom");
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
            hidingTexture = Content.Load<Texture2D>("insideLocker");

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
            BoxFrameTexture = Content.Load<Texture2D>("TextBorder");
            logPanel1Texture = Content.Load<Texture2D>("logPanel1");
            logPanel2Texture = Content.Load<Texture2D>("logPanel2");
            escapePodPanel1Texture = Content.Load<Texture2D>("altEscapePodPanel1");
            escapePodPanel2Texture = Content.Load<Texture2D>("altEscapePodPanel2");
            escapePodPanel3Texture = Content.Load<Texture2D>("altEscapePodPanel3");
            escapePodPanel4Texture = Content.Load<Texture2D>("altEscapePodPanel4");
            blueKeyCard1Texture = Content.Load<Texture2D>("blueKeyCard");
            redKeyCard2Texture = Content.Load<Texture2D>("redKeyCard");
            yellowKeyCard3Texture = Content.Load<Texture2D>("yellowKeyCard");
            orangeKeyCard4Texture = Content.Load<Texture2D>("orangeKeyCard");
            purpleKeyCard5Texture = Content.Load<Texture2D>("purpleKeyCard");
            cardTrackerTexture = Content.Load<Texture2D>("CornerLog");
            bridgeStoryPanel1Texture = Content.Load<Texture2D>("bridgePanel1");
            bridgeStoryPanel2Texture = Content.Load<Texture2D>("bridgePanel2");
            panelButtonTexture = Content.Load<Texture2D>("buttonTexture");
            theManTexture = Content.Load<Texture2D>("theMan");

            // FONTS
            titleReportFont = Content.Load<SpriteFont>("1942Font");
            generalTextFont = Content.Load<SpriteFont>("momTypewriter");

            // SOUND
            monsterHere = Content.Load<Song>("MonsterHere");
            theXunari = Content.Load<Song>("theXunari");
            heartbeatSlow = Content.Load<SoundEffect>("HeartbeatFar");
            heartbeatMedium = Content.Load<SoundEffect>("HeartbeatMedium");
            heartbeatFast = Content.Load<SoundEffect>("HeartbeatClose");

            heartbeatSlowInstance = heartbeatSlow.CreateInstance();
            heartbeatMediumInstance = heartbeatMedium.CreateInstance();
            heartbeatFastInstance = heartbeatFast.CreateInstance();


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
            newKeyboardState = Keyboard.GetState();

            iconBlinkCounter++;

            // CALCULATE CARD DISTANCE VECTOR
            if (activeTask == Story.findKeyCard2 || activeTask == Story.findKeyCard3 || activeTask == Story.findKeyCard4)
            {
                cardTrackerBlinker++;

                if (miniMapCurrentRoomRect.Center.X > objectiveCardLocation.X)
                    cardDistanceX = miniMapCurrentRoomRect.Center.X - objectiveCardLocation.X;

                else if (miniMapCurrentRoomRect.Center.X < objectiveCardLocation.X)
                    cardDistanceX = objectiveCardLocation.X - miniMapCurrentRoomRect.Center.X;


                if (miniMapCurrentRoomRect.Center.Y > objectiveCardLocation.Y)
                    cardDistanceY = miniMapCurrentRoomRect.Center.Y - objectiveCardLocation.Y;

                else if (miniMapCurrentRoomRect.Center.Y < objectiveCardLocation.Y)
                    cardDistanceY = objectiveCardLocation.Y - miniMapCurrentRoomRect.Center.Y;

                cardDistanceVector = Math.Sqrt(Math.Pow(cardDistanceX, 2) + Math.Pow(cardDistanceY, 2));
            }
            
            // Calculate Monster Distance Vectors
            if (onXunari)
            {
                // monster 1
                if (miniMapCurrentRoomRect.Center.X > monster1.MonsterMapLocation.X)
                    monster1DistanceX = miniMapCurrentRoomRect.Center.X - monster1.MonsterMapLocation.X;

                else if (miniMapCurrentRoomRect.Center.X < monster1.MonsterMapLocation.X)
                    monster1DistanceX = monster1.MonsterMapLocation.X - miniMapCurrentRoomRect.Center.X;

                if (miniMapCurrentRoomRect.Center.Y > monster1.MonsterMapLocation.Y)
                    monster1DistanceY = miniMapCurrentRoomRect.Center.Y - monster1.MonsterMapLocation.Y;

                else if (miniMapCurrentRoomRect.Center.Y < monster1.MonsterMapLocation.Y)
                    monster2DistanceY = monster1.MonsterMapLocation.Y - miniMapCurrentRoomRect.Center.Y;

                monster1DistanceVector = Math.Sqrt(Math.Pow(monster1DistanceX, 2) + Math.Pow(monster1DistanceY, 2));

                // monster2
                if (miniMapCurrentRoomRect.Center.X > monster2.MonsterMapLocation.X)
                    monster2DistanceX = miniMapCurrentRoomRect.Center.X - monster2.MonsterMapLocation.X;

                else if (miniMapCurrentRoomRect.Center.X < monster2.MonsterMapLocation.X)
                    monster2DistanceX = monster2.MonsterMapLocation.X - miniMapCurrentRoomRect.Center.X;

                if (miniMapCurrentRoomRect.Center.Y > monster2.MonsterMapLocation.Y)
                    monster2DistanceY = miniMapCurrentRoomRect.Center.Y - monster2.MonsterMapLocation.Y;

                else if (miniMapCurrentRoomRect.Center.Y < monster2.MonsterMapLocation.Y)
                    monster2DistanceY = monster2.MonsterMapLocation.Y - miniMapCurrentRoomRect.Center.Y;

                monster2DistanceVector = Math.Sqrt(Math.Pow(monster2DistanceX, 2) + Math.Pow(monster2DistanceY, 2));
            }

            ////////////////////////////////////////
            // SCREENS
            if (screen == Screen.Title)
            {
                if (!theXunariPlaying)
                {
                    MediaPlayer.Play(theXunari);
                    theXunariPlaying = true;
                }

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
                if (onXunari)
                {
                    if (monster1.RoomLocationEnum == currentRoom || monster2.RoomLocationEnum == currentRoom)
                    {
                        if (!monsterHerePlaying)
                        {
                            timeToDieTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                            MediaPlayer.Play(monsterHere);
                            monsterHerePlaying = true;
                        }
                        timeToDieSec = (float)gameTime.TotalGameTime.TotalSeconds - timeToDieTimeStamp;
                    }
                    if (monsterHerePlaying)
                    {
                        monster1.RoomLocationEnum = currentRoom;
                        monster2.RoomLocationEnum = currentRoom;
                    }
                    if (timeToDieSec >= 5)
                    {
                        timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        screen = Screen.dead;
                    }

                    if (newKeyboardState.IsKeyDown(Keys.H) && newKeyboardState != oldKeyboardState)
                    {
                        screen = Screen.Hiding;
                        if (currentRoom == monster1.RoomLocationEnum || monster2.RoomLocationEnum == currentRoom)
                        {
                            quickTimeLetter = quicktimeLetters[generator.Next(0, 26)];
                            timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        }
                    }

                    if (monsterMoveSec >= 3)
                    {
                        monsterMoveTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;

                        monster1.UpdateMovement();
                        monster2.UpdateMovement();
                    }
                    monsterMoveSec = (float)gameTime.TotalGameTime.TotalSeconds - monsterMoveTimeStamp;

                }

                if (currentRoom == Room.yourEscapePodRoom)
                {
                    if (newMouseState.LeftButton == ButtonState.Pressed && mapButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                    {
                        screen = Screen.EscapePodMap;
                    }
                }

                //////////////////////////////////////////////
                // ROOMS
                else if (currentRoom == Room.dockingBayRoom)
                    UpdateDockingBay();

                else if (currentRoom == Room.escapePodBayRoom)
                {
                    UpdateEscapePodBay();

                    if (activeTask == Story.searchAltEscapePod)
                    {
                        if (newMouseState.LeftButton == ButtonState.Pressed && storyButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                        {
                            currentRoom = Room.travelling;
                            timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        }

                    }
                }

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
                {
                    UpdateLogRoom();
                    if (activeTask == Story.goToLogRoom)
                    {
                        if (newMouseState.LeftButton == ButtonState.Pressed && storyButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                        {
                            activeTask = Story.searchAltEscapePod;
                            readingShipLogs = true;
                        }
                    }
                    else if (activeTask == Story.searchAltEscapePod)
                    {
                        if (newMouseState.LeftButton == ButtonState.Pressed && newMouseState != oldMouseState)
                            logPanelCount++;
                    }
                }

                else if (currentRoom == Room.hallwayDRoom)
                    UpdateHallwayD();

                else if (currentRoom == Room.hallwayERoom)
                {
                    UpdateHallwayE();

                    if (activeTask == Story.openBridge)
                    {
                        if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                        {
                            currentRoom = Room.bridgeRoom;
                        }
                    }

                }

                else if (currentRoom == Room.medBayRoom)
                    UpdateMedBay();

                else if (currentRoom == Room.elevatorRoom)
                    UpdateElevator();

                else if (currentRoom == Room.captainsQuartersRoom)
                {
                    UpdateCaptainsQuarters();

                    if (newMouseState.LeftButton == ButtonState.Pressed && storyButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                    {
                        activeTask = Story.openBridge;
                        keyCardsHeld++;
                    }
                }

                else if (currentRoom == Room.bridgeRoom)
                {
                    UpdateBridge();

                    if (newMouseState.LeftButton == ButtonState.Pressed && storyButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                    {
                        activeTask = Story.end;
                        screen = Screen.end;
                    }
                }

                else if (currentRoom == Room.altEscapePodRoom)
                {
                    moveRoomRect1 = new Rectangle();
                    moveRoomRect2 = new Rectangle();
                    moveRoomRect3 = new Rectangle();
                    moveRoomRect4 = new Rectangle();

                    if (newMouseState.LeftButton == ButtonState.Pressed && storyButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                    {
                        if (activeTask == Story.searchAltEscapePod)
                        {
                            keyCardsHeld++;
                            inRoomWithKeyCard = false;
                        }

                        activeTask = Story.findKeyCard1;
                    }
                    if (activeTask == Story.findKeyCard1 && newMouseState.LeftButton == ButtonState.Pressed && newMouseState != oldMouseState)
                    {
                        altEscapePodPanelCount++;
                    }
                    if (altEscapePodPanelCount >= 4)
                    {
                        storyButtonRect = new Rectangle(955, 880, 300, 100);
                    }
                    if (activeTask == Story.findKeyCard1 && altEscapePodPanelCount >= 4 && newMouseState.LeftButton == ButtonState.Pressed && storyButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                    {
                        activeTask = Story.findKeyCard2;
                        timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        currentRoom = Room.travelling;

                        MarkCardMapLocation();
                    }
                }

                else if (currentRoom == Room.travelling)
                {
                    elapsedTimeSec = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;

                    if (elapsedTimeSec >= 4)
                    {
                        if (activeTask == Story.goToLogRoom)
                        {
                            currentRoom = Room.dockingBayRoom;
                            onXunari = true;
                        }
                        else if (activeTask == Story.searchAltEscapePod)
                        {
                            currentRoom = Room.altEscapePodRoom;
                            onXunari = false;
                        }
                        else if (activeTask == Story.findKeyCard2)
                        {
                            currentRoom = Room.dockingBayRoom;
                            onXunari = true;
                        }
                    }
                }

            }

            else if (screen == Screen.EscapePodMap)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && xunariMapRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    travelToXunariPrompt = true;
                }
                if (travelToXunariPrompt && newKeyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Game;
                    currentRoom = Room.travelling; 
                    activeTask = Story.goToLogRoom;

                    monster1.InitializeLocation();
                    monster2.InitializeLocation();

                    timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                    travelToXunariPrompt = false;
                }
            }

            else if (screen == Screen.end)
            {
                endTextFlasherCount++;

                if (newMouseState.LeftButton == ButtonState.Pressed && newMouseState != oldMouseState)
                    endStoryPanelCount++;

                endStoryYesButtonRect = new Rectangle(400, 500, 100, 100);

                if (newMouseState.LeftButton == ButtonState.Pressed && endStoryYesButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    timeStamp = (float)gameTime.TotalGameTime.TotalSeconds; // use these time stamps to wait for the game to return to the title screen after SFXs are done playing
                    screen = Screen.outro;
                }

                if (endStoryNoButtonRect.Contains(newMouseState.X, newMouseState.Y))
                    endStoryNoButtonRect = new Rectangle();

                else
                    endStoryNoButtonRect = new Rectangle(830, 500, 100, 100);
            }

            else if (screen == Screen.outro)
            {
                elapsedTimeSec = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;
                if (elapsedTimeSec >= 8)
                {
                    Reset();
                    Initialize();
                    screen = Screen.Title;
                }
            }

            else if (screen == Screen.Hiding)
            {
                timeToDieSec = 0;

                if (newMouseState.LeftButton == ButtonState.Pressed && closeButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
                {
                    screen = Screen.Game;
                }

                if (monster1.RoomLocationEnum == currentRoom || monster2.RoomLocationEnum == currentRoom)
                {
                    elapsedTimeSec = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;

                    if (quickTimeSuccessCounter < 6)
                    {
                        if (newKeyboardState.IsKeyDown(quickTimeLetter) && elapsedTimeSec <= 1.5 && newKeyboardState != oldKeyboardState)
                        {
                            quickTimeSuccessCounter++;
                            quickTimeLetter = quicktimeLetters[generator.Next(0, 26)];
                            timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        }

                        else if (newKeyboardState.GetPressedKeyCount() != 0 && !newKeyboardState.IsKeyDown(quickTimeLetter) && newKeyboardState != oldKeyboardState)
                        {
                            if (!newKeyboardState.IsKeyDown(Keys.H) && quickTimeSuccessCounter == 0)
                                screen = Screen.dead;


                            else if (newKeyboardState.IsKeyDown(Keys.H) && quickTimeSuccessCounter != 0)
                                screen = Screen.dead;

                            if (!newKeyboardState.IsKeyDown(Keys.H) && quickTimeSuccessCounter != 0)
                                screen = Screen.dead;
                        }

                        else if (elapsedTimeSec > 1.5)
                            screen = Screen.dead;
                    }
                    else if (quickTimeSuccessCounter == 6)
                    {
                        screen = Screen.Game;
                        monster1.InitializeLocation();
                        monster2.InitializeLocation();
                        monsterHerePlaying = false;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(theXunari);

                        quickTimeSuccessCounter = 0;
                    }
                }
            }

            else if (screen == Screen.dead)
            {
                elapsedTimeSec = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;
                if (elapsedTimeSec >= 10)
                {
                    Reset();
                    Initialize();
                }
            }

            // change target keycard
            if (inRoomWithKeyCard && newMouseState.LeftButton == ButtonState.Pressed && storyButtonRect.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                inRoomWithKeyCard = false;

                keyCardsHeld++;
                if (activeTask == Story.findKeyCard2)
                {
                    activeTask = Story.findKeyCard3;
                }
                else if (activeTask == Story.findKeyCard3)
                {
                    activeTask = Story.findKeyCard4;
                }
                else if (activeTask == Story.findKeyCard4)
                {
                    objectiveCardLocation = new Point(1275, 806);
                    activeTask = Story.findKeyCard5;
                }
            }

            MonsterMapTracker();

            oldMouseState = newMouseState;
            oldKeyboardState = newKeyboardState;
            base.Update(gameTime);
        }


        // ////////////////////////////////////////
        // //////////////////////////////////////// DRAW
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            ///////////////////////////////////////////////////
            // SCREENS
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

                _spriteBatch.Draw(closeButtonTexture, closeButtonRect, Color.White);
            }

            else if (screen == Screen.end)
            {
                if (endStoryPanelCount == 0)
                    _spriteBatch.Draw(bridgeStoryPanel1Texture, backgroundRect, Color.White);

                else if (endStoryPanelCount >= 1)
                {
                    _spriteBatch.Draw(bridgeStoryPanel2Texture, backgroundRect, Color.White);
                    _spriteBatch.Draw(panelButtonTexture, endStoryYesButtonRect, Color.DarkSlateBlue);

                    if (!endStoryNoButtonRect.Contains(newMouseState.X, newMouseState.Y))
                        _spriteBatch.Draw(panelButtonTexture, endStoryNoButtonRect, Color.DarkSlateBlue);

                    else if (endStoryNoButtonRect.Contains(newMouseState.X, newMouseState.Y))
                    {
                        if (endTextFlasherCount <= 5)
                        {
                            _spriteBatch.DrawString(generalTextFont, "YOU CANNOT DELAY THE INEVITABLE.", new Vector2(400, 700), Color.DarkRed);
                        }
                        else if (endTextFlasherCount > 5 && iconBlinkCounter <= 10)
                        {
                            _spriteBatch.DrawString(generalTextFont, "YOU CANNOT DELAY THE INEVITABLE.", new Vector2(400, 700), Color.Black);
                        }
                        else if (endTextFlasherCount >= 11)
                            endTextFlasherCount = 0;
                    }

                }
            }

            else if (screen == Screen.Game)
            {
                // //////////////////////////////////////////
                // ROOMS
                if (currentRoom == Room.yourEscapePodRoom)
                {
                    _spriteBatch.Draw(yourEscapePodTexture, backgroundRect, Color.White);

                    _spriteBatch.Draw(rectangleButtonTexture, mapButtonRect, Color.Black);
                    _spriteBatch.DrawString(generalTextFont, "MAP", new Vector2(1155, 885), Color.Olive);
                }

                else if (currentRoom == Room.dockingBayRoom)
                {
                    if (activeTask == Story.findKeyCard2)
                    {

                    }
                    _spriteBatch.Draw(dockingBayTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Docking Bay", new Vector2(33, 920), Color.Olive);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    if (activeTask == Story.goToLogRoom)
                    {
                        _spriteBatch.Draw(BoxFrameTexture, textBoxRect, Color.White);
                        _spriteBatch.Draw(rectangleButtonTexture, textBoxRect, Color.Black);
                        _spriteBatch.DrawString(generalTextFont, "The log room would be the best place for me to start.", new Vector2(20, 20), Color.Olive);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.escapePodBayRoom)
                {
                    _spriteBatch.Draw(escapePodBayTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Pod Bay", new Vector2(57, 920), Color.Olive);

                    if (activeTask == Story.searchAltEscapePod)
                    {
                        _spriteBatch.Draw(rectangleButtonTexture, storyButtonRect, Color.Black);
                        _spriteBatch.Draw(BoxFrameTexture, storyButtonRect, Color.White);
                        _spriteBatch.DrawString(generalTextFont, "Exit to Pod #17", new Vector2(610, 730), Color.Olive);
                    }

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
                    _spriteBatch.DrawString(generalTextFont, "Hall A", new Vector2(57, 920), Color.Olive);


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
                    _spriteBatch.DrawString(generalTextFont, "Hall B", new Vector2(57, 920), Color.Olive);

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
                    _spriteBatch.DrawString(generalTextFont, "Residence A", new Vector2(35, 920), Color.Olive);


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
                    _spriteBatch.DrawString(generalTextFont, "Cargo Bay", new Vector2(51, 920), Color.Olive);


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
                    _spriteBatch.DrawString(generalTextFont, "Mess Hall", new Vector2(49, 920), Color.Olive);


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
                    _spriteBatch.DrawString(generalTextFont, "Residence B", new Vector2(35, 920), Color.Olive);

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
                    _spriteBatch.DrawString(generalTextFont, "Engine Room", new Vector2(40, 920), Color.Olive);


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
                    _spriteBatch.DrawString(generalTextFont, "Security Office", new Vector2(35, 920), Color.Olive);

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
                    else if (moveRoomRect4.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    RunMapBlinker();
                }

                else if (currentRoom == Room.reactorRoom)
                {
                    _spriteBatch.Draw(reactorRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Reactor", new Vector2(49, 920), Color.Olive);

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
                    _spriteBatch.DrawString(generalTextFont, "Hall D", new Vector2(57, 920), Color.Olive);

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

                else if (currentRoom == Room.hallwayERoom)
                {
                    _spriteBatch.Draw(hallway1Texture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Hall E", new Vector2(57, 920), Color.Olive);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    RunMapBlinker();
                }

                else if (currentRoom == Room.commRoom)
                {
                    _spriteBatch.Draw(commRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Comm Room", new Vector2(57, 920), Color.Olive);

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
                    _spriteBatch.DrawString(generalTextFont, "Hall C", new Vector2(57, 920), Color.Olive);

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
                    _spriteBatch.DrawString(generalTextFont, "Log Room", new Vector2(57, 920), Color.Olive);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorRight, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorUp, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    if (activeTask == Story.goToLogRoom)
                    {
                        _spriteBatch.Draw(rectangleButtonTexture, storyButtonRect, Color.Black);
                        _spriteBatch.Draw(BoxFrameTexture, storyButtonRect, Color.White);
                        _spriteBatch.DrawString(generalTextFont, "Examine Ship Logs", new Vector2(610, 730), Color.Olive);

                    }
                    if (activeTask == Story.searchAltEscapePod)
                    {
                        if (logPanelCount == 0)
                        {
                            _spriteBatch.Draw(logPanel1Texture, backgroundRect, Color.White);
                        }
                        else if (logPanelCount == 1)
                        {
                            _spriteBatch.Draw(logPanel2Texture, backgroundRect, Color.White);
                        }
                        else if (logPanelCount >= 2)
                        {
                            readingShipLogs = false;
                            _spriteBatch.Draw(BoxFrameTexture, textBoxRect, Color.White);
                            _spriteBatch.Draw(rectangleButtonTexture, textBoxRect, Color.Black);
                            _spriteBatch.DrawString(generalTextFont, "I need to go to the pod bay and get to pod 17.", new Vector2(20, 20), Color.Olive);
                        }
                    }
                    if (!readingShipLogs)
                        RunMapBlinker();
                }

                else if (currentRoom == Room.medBayRoom)
                {
                    _spriteBatch.Draw(medBayRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.Draw(miniMapTexture, miniMapRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Med Bay", new Vector2(57, 920), Color.Olive);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorLeft, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect3.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorRight, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    if (activeTask == Story.findKeyCard5)
                    {
                        if (iconBlinkCounter < 15)
                        {
                            _spriteBatch.Draw(rectangleButtonTexture, elevatorBlinkerRect, Color.Black);
                        }
                        else if (iconBlinkCounter > 15 && iconBlinkCounter < 30)
                        {
                            _spriteBatch.Draw(rectangleButtonTexture, elevatorBlinkerRect, Color.DarkRed);
                        }
                        else if (iconBlinkCounter >= 30)
                        {
                            iconBlinkCounter = 0;
                            _spriteBatch.Draw(rectangleButtonTexture, elevatorBlinkerRect, Color.Black);
                        }
                    }
                    
                    RunMapBlinker();
                }

                else if (currentRoom == Room.elevatorRoom)
                {
                    _spriteBatch.Draw(elevatorRoomTexture, backgroundRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Elevator", new Vector2(57, 920), Color.Olive);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                    else if (moveRoomRect2.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }
                }

                else if (currentRoom == Room.captainsQuartersRoom)
                {
                    _spriteBatch.Draw(residenceRoom2Texture, backgroundRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Captain's Quarters", new Vector2(35, 920), Color.Olive);

                    if (moveRoomRect1.Contains(newMouseState.X, newMouseState.Y))
                    {
                        _spriteBatch.Draw(moveCursorDown, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                    }

                    if (activeTask == Story.findKeyCard5)
                    {
                        _spriteBatch.Draw(rectangleButtonTexture, storyButtonRect, Color.Black);
                        _spriteBatch.DrawString(generalTextFont, "Take Key Card", new Vector2(610, 730), Color.Olive);
                    }
                    else if (activeTask == Story.openBridge)
                    {
                        _spriteBatch.Draw(BoxFrameTexture, textBoxRect, Color.White);
                        _spriteBatch.Draw(rectangleButtonTexture, textBoxRect, Color.Black);
                        _spriteBatch.DrawString(generalTextFont, "Now I can open the bridge doors.", new Vector2(20, 20), Color.Olive);
                    }
                }

                else if (currentRoom == Room.bridgeRoom)
                {
                    _spriteBatch.Draw(bridgeTexture, backgroundRect, Color.White);

                    _spriteBatch.Draw(rectangleButtonTexture, storyButtonRect, Color.Black);
                    _spriteBatch.Draw(BoxFrameTexture, storyButtonRect, Color.White);
                    _spriteBatch.DrawString(generalTextFont, "Send Distress Call", new Vector2(610, 730), Color.Olive);
                }

                else if (currentRoom == Room.altEscapePodRoom)
                {
                    _spriteBatch.Draw(altEscapePodTexture, backgroundRect, Color.White);

                    _spriteBatch.Draw(rectangleButtonTexture, storyButtonRect, Color.Black);
                    _spriteBatch.Draw(BoxFrameTexture, storyButtonRect, Color.White);
                    if (altEscapePodPanelCount < 4)
                        _spriteBatch.DrawString(generalTextFont, "Search Escape Pod", new Vector2(610, 730), Color.Olive);

                    if (activeTask == Story.findKeyCard1)
                    {
                        if (altEscapePodPanelCount == 0)
                        {
                            _spriteBatch.Draw(escapePodPanel1Texture, backgroundRect, Color.White);
                        }
                        else if (altEscapePodPanelCount == 1)
                        {
                            _spriteBatch.Draw(escapePodPanel2Texture, backgroundRect, Color.White);
                        }
                        else if (altEscapePodPanelCount == 2)
                        {
                            _spriteBatch.Draw(escapePodPanel3Texture, backgroundRect, Color.White);
                        }
                        else if (altEscapePodPanelCount == 3)
                        {
                            _spriteBatch.Draw(escapePodPanel4Texture, backgroundRect, Color.White);
                        }
                        else if (altEscapePodPanelCount >= 4)
                        {
                            _spriteBatch.Draw(rectangleButtonTexture, textBoxRect, Color.Black);
                            _spriteBatch.DrawString(generalTextFont, "I should try to find the remaining keycards.", new Vector2(20, 20), Color.Olive);
                            _spriteBatch.Draw(rectangleButtonTexture, storyButtonRect, Color.Black);
                            _spriteBatch.DrawString(generalTextFont, "Travel to Xunari", new Vector2(965, 900), Color.Olive);
                        }
                    }
                }
            } // Rooms


            else if (screen == Screen.Hiding)
            {
                if (monster1.RoomLocationEnum == currentRoom || monster2.RoomLocationEnum == currentRoom)
                {
                    _spriteBatch.Draw(theManTexture, theManRect, Color.White);
                }
                _spriteBatch.Draw(hidingTexture, backgroundRect, Color.White);

                if (monster1.RoomLocationEnum != currentRoom && monster2.RoomLocationEnum != currentRoom)
                    _spriteBatch.Draw(closeButtonTexture, closeButtonRect, Color.White);

                if (monster1.RoomLocationEnum == currentRoom || monster2.RoomLocationEnum == currentRoom)
                {
                    elapsedTimeSec = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;

                    if (quickTimeSuccessCounter < 6)
                    {
                        _spriteBatch.DrawString(titleReportFont, quickTimeLetter.ToString(), new Vector2(650, 500), Color.DarkRed);
                    }
                }
            }

            else if (screen == Screen.dead)
            {
                _spriteBatch.Draw(theManTexture, theManRect, Color.White);
            }

            /////////////////////////////////////////////////////////
            // OTHER
            if (!moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && !moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && !moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && !moveRoomRect4.Contains(newMouseState.X, newMouseState.Y))
            {
                _spriteBatch.Draw(circleCursor, new Vector2(newMouseState.X, newMouseState.Y), Color.White);
            }

            if (onXunari && currentRoom != Room.travelling && activeTask != Story.end && screen != Screen.dead)
            {
                _spriteBatch.Draw(BoxFrameTexture, locationBoxRect, Color.White);
            }

            if (inRoomWithKeyCard)
            {
                _spriteBatch.Draw(rectangleButtonTexture, storyButtonRect, Color.Black);
                _spriteBatch.DrawString(generalTextFont, "Take Key Card", new Vector2(610, 730), Color.Olive);
            }

            if (currentRoom != Room.bridgeRoom)
            {
                TrackerIndicator();
                DrawKeyCards();
            }
            
            if (currentRoom == Room.travelling && activeTask == Story.goToLogRoom)
            {
                _spriteBatch.DrawString(generalTextFont, "A heartbeat will indicate how close a monster is to you.", new Vector2(200, 500), Color.DarkRed);
                _spriteBatch.DrawString(generalTextFont, "Pay attention and press H to hide when they find you.", new Vector2(220, 600), Color.DarkRed);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        // ///////////////////////////////////////
        // ///////////////////////////////////////
        // ///////////////////////////////////////
        // ALT METHODS

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
            else if (activeTask == Story.findKeyCard5 && newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect3.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.elevatorRoom;
            }
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

        public void UpdateHallwayE()
        {
            miniMapCurrentRoomRect = new Rectangle(1134, 749, 14, 49);
            moveRoomRect1 = new Rectangle(720, 150, 300, 500);
            moveRoomRect2 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect3 = new Rectangle();
            moveRoomRect4 = new Rectangle();

            if (activeTask == Story.findKeyCard5 && newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                currentRoom = Room.bridgeRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                currentRoom = Room.securityRoom;
            }
        }

        public void UpdateSecurityRoom()
        {
            miniMapCurrentRoomRect = new Rectangle(1126, 807, 30, 30);
            moveRoomRect1 = new Rectangle(950, 190, 300, 500); // right side long
            moveRoomRect2 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect3 = new Rectangle(30, 180, 300, 500); // left side long
            moveRoomRect4 = new Rectangle(375, 100, 500, 215); // top wide

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
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect4.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.hallwayERoom;
            }
        }

        public void UpdateElevator()
        {
            moveRoomRect1 = new Rectangle(620, 150, 300, 500);
            moveRoomRect2 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect3 = new Rectangle();
            moveRoomRect4 = new Rectangle();

            if (activeTask == Story.findKeyCard5 && newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                currentRoom = Room.captainsQuartersRoom;
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect2.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                currentRoom = Room.medBayRoom;
            }
        }

        public void UpdateCaptainsQuarters()
        {
            moveRoomRect1 = new Rectangle(375, 775, 500, 215); // bottom wide
            moveRoomRect2 = new Rectangle();
            moveRoomRect3 = new Rectangle();
            moveRoomRect4 = new Rectangle();

            if (newMouseState.LeftButton == ButtonState.Pressed && moveRoomRect1.Contains(newMouseState.X, newMouseState.Y) && newMouseState != oldMouseState)
            {
                prevRoom = currentRoom;
                currentRoom = Room.elevatorRoom;
            }
        }

        public void UpdateBridge()
        {
            moveRoomRect1 = new Rectangle();
            moveRoomRect2 = new Rectangle();
            moveRoomRect3 = new Rectangle();
            moveRoomRect4 = new Rectangle();
        }

        // ////////////////////////////////

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
            else if (iconBlinkCounter >= 120)
            {
                iconBlinkCounter = 0;
                _spriteBatch.Draw(miniMapCurrentRoomTexture, miniMapCurrentRoomRect, Color.Black);
            }
        }

        public void DrawKeyCards()
        {
            if (currentRoom != Room.travelling && screen != Screen.dead)
            {
                if (keyCardsHeld == 1)
                {
                    _spriteBatch.Draw(blueKeyCard1Texture, keyCard1Rect, Color.White);
                }
                else if (keyCardsHeld == 2)
                {
                    _spriteBatch.Draw(blueKeyCard1Texture, keyCard1Rect, Color.White);
                    _spriteBatch.Draw(redKeyCard2Texture, keyCard2Rect, Color.White);
                }
                else if (keyCardsHeld == 3)
                {
                    _spriteBatch.Draw(blueKeyCard1Texture, keyCard1Rect, Color.White);
                    _spriteBatch.Draw(redKeyCard2Texture, keyCard2Rect, Color.White);
                    _spriteBatch.Draw(yellowKeyCard3Texture, keyCard3Rect, Color.White);
                }
                else if (keyCardsHeld == 4)
                {
                    _spriteBatch.Draw(blueKeyCard1Texture, keyCard1Rect, Color.White);
                    _spriteBatch.Draw(redKeyCard2Texture, keyCard2Rect, Color.White);
                    _spriteBatch.Draw(yellowKeyCard3Texture, keyCard3Rect, Color.White);
                    _spriteBatch.Draw(orangeKeyCard4Texture, keyCard4Rect, Color.White);

                }
                else if (keyCardsHeld == 5)
                {
                    _spriteBatch.Draw(blueKeyCard1Texture, keyCard1Rect, Color.White);
                    _spriteBatch.Draw(redKeyCard2Texture, keyCard2Rect, Color.White);
                    _spriteBatch.Draw(yellowKeyCard3Texture, keyCard3Rect, Color.White);
                    _spriteBatch.Draw(orangeKeyCard4Texture, keyCard4Rect, Color.White);
                    _spriteBatch.Draw(purpleKeyCard5Texture, keyCard5Rect, Color.White);
                }
            }
        }

        public void MarkCardMapLocation()
        {
            if (activeTask == Story.findKeyCard2)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (keyCard2Location == i)
                    {
                        objectiveCardLocation = roomRects[i].Center;
                    }
                }
            }
            else if (activeTask == Story.findKeyCard3)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (keyCard3Location == i)
                    {
                        objectiveCardLocation = roomRects[i].Center;
                    }
                }
            }
            else if (activeTask == Story.findKeyCard4)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (keyCard4Location == i)
                    {
                        objectiveCardLocation = roomRects[i].Center;
                    }
                }
            }
        }

        public void LocateRoomMapRects()
        {
            roomRects.Add(dockBayMapRect = new Rectangle(1024, 942, 234, 31)); 
            roomRects.Add(podBayMapRect = new Rectangle(1032, 895, 30, 30));
            roomRects.Add(res1MapRect = new Rectangle(1032, 807, 30, 30));
            roomRects.Add(res2MapRect = new Rectangle(1079, 767, 30, 30));
            roomRects.Add(messMapRect = new Rectangle(1069, 807, 50, 84));
            roomRects.Add(secMapRect = new Rectangle(1126, 807, 30, 30));
            roomRects.Add(cargoMapRect = new Rectangle(1126, 895, 30, 30));
            roomRects.Add(hallAMapRect = new Rectangle(1040, 840, 14, 49));
            roomRects.Add(hallBMapRect = new Rectangle(1069, 903, 49, 14));
            roomRects.Add(hallCMapRect = new Rectangle(1161, 858, 49, 14));
            roomRects.Add(hallDMapRect = new Rectangle(1162, 815, 49, 14));
            roomRects.Add(hallEMapRect = new Rectangle(1134, 749, 14, 49));
            roomRects.Add(medMapRect = new Rectangle(1216, 806, 30, 30));
            roomRects.Add(engineMapRect = new Rectangle(1170, 895, 30, 30));
            roomRects.Add(reactorMapRect = new Rectangle(1215, 895, 30, 30));
            roomRects.Add(logMapRect = new Rectangle(1126, 850, 30, 30));
            roomRects.Add(commMapRect = new Rectangle(1217, 849, 30, 30));
            
        }

        public void AddQuickTimeKeys()
        {
            quicktimeLetters.Add(Keys.A);
            quicktimeLetters.Add(Keys.B);
            quicktimeLetters.Add(Keys.C);
            quicktimeLetters.Add(Keys.D);
            quicktimeLetters.Add(Keys.E);
            quicktimeLetters.Add(Keys.F);
            quicktimeLetters.Add(Keys.G);
            quicktimeLetters.Add(Keys.H);
            quicktimeLetters.Add(Keys.I);
            quicktimeLetters.Add(Keys.J);
            quicktimeLetters.Add(Keys.K);
            quicktimeLetters.Add(Keys.L);
            quicktimeLetters.Add(Keys.M);
            quicktimeLetters.Add(Keys.N);
            quicktimeLetters.Add(Keys.O);
            quicktimeLetters.Add(Keys.P);
            quicktimeLetters.Add(Keys.Q);
            quicktimeLetters.Add(Keys.R);
            quicktimeLetters.Add(Keys.S);
            quicktimeLetters.Add(Keys.T);
            quicktimeLetters.Add(Keys.U);
            quicktimeLetters.Add(Keys.V);
            quicktimeLetters.Add(Keys.W);
            quicktimeLetters.Add(Keys.X);
            quicktimeLetters.Add(Keys.Y);
            quicktimeLetters.Add(Keys.Z);

        }

        public void TrackerIndicator()
        {
            if (screen != Screen.dead)
            {
                if (activeTask == Story.findKeyCard2 || activeTask == Story.findKeyCard3 || activeTask == Story.findKeyCard4 && currentRoom != Room.travelling && !inRoomWithKeyCard)
                {
                    _spriteBatch.Draw(cardTrackerTexture, cardTrackerRect, Color.White);

                    if (cardDistanceVector >= 175 && !inRoomWithKeyCard)
                    {
                        if (cardTrackerBlinker < 60)
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.Black);
                        }
                        else if (cardTrackerBlinker > 60 && cardTrackerBlinker < 120)
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.DarkRed);
                        }
                        else if (cardTrackerBlinker >= 120)
                        {
                            cardTrackerBlinker = 0;
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.Black);
                        }
                    }

                    else if (cardDistanceVector < 150 && cardDistanceVector > 100 && !inRoomWithKeyCard)
                    {
                        if (cardTrackerBlinker < 30)
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.Black);
                        }
                        else if (cardTrackerBlinker > 30 && cardTrackerBlinker < 60)
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.DarkRed);
                        }
                        else if (cardTrackerBlinker >= 60)
                        {
                            cardTrackerBlinker = 0;
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.Black);
                        }
                    }

                    else if (cardDistanceVector <= 100 && cardDistanceVector > 1 && !inRoomWithKeyCard)
                    {
                        if (cardTrackerBlinker < 15)
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.Black);
                        }
                        else if (cardTrackerBlinker > 15 && cardTrackerBlinker < 30)
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.DarkRed);
                        }
                        else if (cardTrackerBlinker >= 30)
                        {
                            cardTrackerBlinker = 0;
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.Black);
                        }
                    }

                }
                if (activeTask == Story.findKeyCard2)
                {
                    for (int i = 1; i < 17; i++)
                    {
                        if (keyCard2Location == i && currentRoom == rooms[i])
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.DarkRed);
                            storyButtonRect = new Rectangle(600, 700, 300, 100);
                            inRoomWithKeyCard = true;

                        }
                    }
                }
                else if (activeTask == Story.findKeyCard3)
                {
                    for (int i = 1; i < 17; i++)
                    {
                        if (keyCard3Location == i && currentRoom == rooms[i])
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.DarkRed);
                            inRoomWithKeyCard = true;
                        }
                    }
                }
                else if (activeTask == Story.findKeyCard4)
                {
                    for (int i = 1; i < 17; i++)
                    {
                        if (keyCard4Location == i && currentRoom == rooms[i])
                        {
                            _spriteBatch.Draw(circleIconTexture, trackerBlinkerRect, Color.DarkRed);
                            inRoomWithKeyCard = true;
                        }
                    }
                }
            }
            
        }

        public void Reset()
        {
            storyPanelCount = 0;
            logPanelCount = 0;
            altEscapePodPanelCount = 0;
            keyCardsHeld = 0;
            endStoryPanelCount = 0;
            endTextFlasherCount = 0;
            quickTimeSuccessCounter = 0;
            travelToXunariPrompt = false;
            onXunari = false;
            readingShipLogs = false;
            inRoomWithKeyCard = false;
            endYesSelected = false;
            endNoSelected = false;
            MediaPlayer.Stop();
            monsterHerePlaying = false;
            theXunariPlaying = false;
            monsterMoveSec = 0;
            monsterMoveTimeStamp = 0;
            timeToDieSec = 0;
            timeToDieTimeStamp = 0;

            rooms.Clear();
            roomRects.Clear();
        }

        public void MonsterMapTracker()
        {
            if (monster1DistanceVector >= 175 || monster2DistanceVector >= 175)
            {
                if (heartbeatSlowInstance.State == SoundState.Stopped)
                    heartbeatSlowInstance.Play();
            }
            else if (monster1DistanceVector < 150 && monster1DistanceVector > 100 || monster2DistanceVector < 150 && monster2DistanceVector > 100)
            {
                if (heartbeatMediumInstance.State == SoundState.Stopped)
                    heartbeatMediumInstance.Play();
            }
            else if (monster1DistanceVector < 100 && monster1DistanceVector > 1 || monster2DistanceVector < 100 && monster2DistanceVector > 1)
            {
                if (heartbeatFastInstance.State == SoundState.Stopped)
                    heartbeatFastInstance.Play();
            }
        }
    }
}