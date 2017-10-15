using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Controls;

namespace CGDD4303_Silverlight
{
    /// <summary>
    /// <notes>
    /// 
    /// screen effects class: fade in/out  slide transition  spotlight
    /// 
    /// </notes>
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //public List<BarrierComponent> barrierList;
        //public List<LevelPiece> levelPieceList;
        //public List<Door> doorList;
        //public List<Wire> wireList;
        //public List<Elevator> elevatorList;
        //public List<PickupableItem> itemList;
        public List<Level> levelList;
        public Texture2D playerText, platformTex, rightWallTex, leftWallTex,
            topWallTex, doorSpriteSheet, elevatorTrack, elevatorPlatform,
            elevatorBox, barrierDebug, smallExitTex, conveyorBeltTexture,
            powerBoxOn, powerBoxOff, inventoryTexture, invCursorText, eleButton;
        public Texture2D horizontal, leftUp, leftDown, rightUp, rightDown, vertical, socketed;

        public List<TutorialDatabase> tutorialDataList;

        //splash screen textures
        public Texture2D oogieWareTex, spsuTex, blackBackgroundTex;

        //credits textures
        public Texture2D developerCredits;

        //item textures
        public Texture2D copperTex, ironTex, aluminumTex, silverTex;

        //main menu variables
        public Texture2D computerBackgroundTex, playButtonTex, creditsButtonTex, mouseTex;
        private MainMenu mainMenu;
        public bool playingGame;

        public Level currentLevel;
        //currentlevel = new specific level; currentlevel.loadcontent()/update/draw
        public Level introLevel, hubWorld, tutorialLevel, zone1Level1, zone1Level2, zone1Level3, zone2Level1, zone2Level2, zone2Level3, zone3Level1, zone3Level2, zone3Level3,
            zone4Level1, zone4Level2, zone4Level3, zone5Level1, zone5Level2, zone5Level3;
        public Player playerRobot;
        //public LevelPiece box, horizontal, vertical, box2, box3, box4;
        public SpriteFont sf;
        public Vector2 playerRobotOrigin, barrierOrigin, fromAbove, fromBelow, fromLeft, fromRight;
        public Texture2D mainBackground, basement, foundary, upperFactory;
        public Wire testWire;
        private SplashScreen splashScreen;
        private CreditsScreen creditsScreen;
        public SpriteFont quartzFont;
        private bool enableDebug;
        //meter textures
        public Texture2D healthBorder, healthBar;
        public Texture2D conductBorder, conductBar;
        public Texture2D powerBorder, powerBar;

        //tutorial variables
        public bool tutorialPause;
        public Texture2D tutorialBackground;

        //tutorial database variables 
        public Texture2D movementTex, powerTex, pickupTex, placeTex, cycleTex,
          voltTex, exitTex;

        //voltmeter stuff
        public Texture2D voltMeterTex, elevInfo2Flr, elevInfo3Flr, conveyInfo, doorInfo,
            pwrSrcInfo, copper, alum, iron, silver;

        //stat tracking variables
        public Texture2D statScreen, incomplete, statBorder;
        public bool drawTracking;
        public ProgressTracker statTracker;
        public int statOpenCount;

        Song songOne, songTwo;
        int songOneDur, songTwoDur, count, scoreCounter;
        bool songOneBool, scoreBool;
        bool exitsHandled;

        public Texture2D exitClosedTex;

        public List<int> theoreticalAttempts, actualAttempts, scoreList;

        public bool gameBeat;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferHeight = 650;
            this.graphics.PreferredBackBufferWidth = 950;
            this.graphics.IsFullScreen = false;
        }

        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferHeight = 650;
            this.graphics.PreferredBackBufferWidth = 950;
            this.graphics.IsFullScreen = false;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            gameBeat = false;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            enableDebug = false;
            songOne = Content.Load<Song>("Automaton");
            songTwo = Content.Load<Song>("Terminus");
            songOneDur = 15000;
            songTwoDur = 22000;
            count = 0;
            MediaPlayer.Play(songOne);
            scoreBool = true;
            scoreCounter = 0;
            theoreticalAttempts = new List<int>();
            #region intlist
            theoreticalAttempts.Add(5);
            theoreticalAttempts.Add(5);
            theoreticalAttempts.Add(12);
            theoreticalAttempts.Add(3);
            theoreticalAttempts.Add(4);
            theoreticalAttempts.Add(8);
            theoreticalAttempts.Add(4);
            theoreticalAttempts.Add(8);
            theoreticalAttempts.Add(6);
            theoreticalAttempts.Add(2);
            theoreticalAttempts.Add(6);
            theoreticalAttempts.Add(4);
            theoreticalAttempts.Add(10);


            #endregion
            actualAttempts = new List<int>();
            platformTex = Content.Load<Texture2D>("horizontal_level_piece_w_wires");
            rightWallTex = Content.Load<Texture2D>("vertical_level_piece_right");
            leftWallTex = Content.Load<Texture2D>("vertical_level_piece_left");
            topWallTex = Content.Load<Texture2D>("vertical_level_piece_top");
            //sf = Content.Load<SpriteFont>("SpriteFont1");
            playerText = Content.Load<Texture2D>("robot_sprite_sheet");
            doorSpriteSheet = Content.Load<Texture2D>("doorAnimated");
            elevatorBox = Content.Load<Texture2D>("elevator_box");
            elevatorPlatform = Content.Load<Texture2D>("elevator_platform");
            elevatorTrack = Content.Load<Texture2D>("elevator_track");
            barrierDebug = Content.Load<Texture2D>("tempElePlatform");
            smallExitTex = Content.Load<Texture2D>("small_exit");
            horizontal = Content.Load<Texture2D>("horizontal_wire");
            leftDown = Content.Load<Texture2D>("left_down_wire");
            leftUp = Content.Load<Texture2D>("left_up_wire");
            rightDown = Content.Load<Texture2D>("right_down_wire");
            rightUp = Content.Load<Texture2D>("right_up_wire");
            vertical = Content.Load<Texture2D>("vertical_wire");
            socketed = Content.Load<Texture2D>("socket");
            conveyorBeltTexture = Content.Load<Texture2D>("conveyor_belt");
            mainBackground = Content.Load<Texture2D>("main_background");
            basement = Content.Load<Texture2D>("basement");
            foundary = Content.Load<Texture2D>("foundary_background");
            upperFactory = Content.Load<Texture2D>("upper_factory");
            powerBoxOff = Content.Load<Texture2D>("power_box_off");
            powerBoxOn = Content.Load<Texture2D>("power_box_on");
            inventoryTexture = Content.Load<Texture2D>("inventory_with_boxes");
            invCursorText = Content.Load<Texture2D>("invCursor");
            eleButton = Content.Load<Texture2D>("elevator_button");
            //splash screen variables
            oogieWareTex = Content.Load<Texture2D>("oogie_ware");
            spsuTex = Content.Load<Texture2D>("spsu_games");
            blackBackgroundTex = Content.Load<Texture2D>("black_background");
            splashScreen = new SplashScreen(this);

            //item textures
            copperTex = Content.Load<Texture2D>("copper_wire");
            ironTex = Content.Load<Texture2D>("iron_wire");
            aluminumTex = Content.Load<Texture2D>("aluminum_wire");
            silverTex = Content.Load<Texture2D>("silver_wire");

            //credit screen variables
            developerCredits = Content.Load<Texture2D>("design_credits");


            //main menu variables
            playButtonTex = Content.Load<Texture2D>("play_button");
            creditsButtonTex = Content.Load<Texture2D>("credits_button");
            computerBackgroundTex = Content.Load<Texture2D>("menu_screen_with_logo");
            mouseTex = Content.Load<Texture2D>("mouse_tex");
            mainMenu = new MainMenu("no_text_file", this, "main menu");
            playingGame = false;
            tutorialPause = false;


            //meter variables
            healthBar = Content.Load<Texture2D>("Health");
            healthBorder = Content.Load<Texture2D>("Border");
            conductBar = Content.Load<Texture2D>("conductivity");
            conductBorder = Content.Load<Texture2D>("conductivity_meter");
            powerBorder = Content.Load<Texture2D>("power_meter");
            powerBar = Content.Load<Texture2D>("power");

            //tutorial textures
            tutorialBackground = Content.Load<Texture2D>("tutorial_background2");
            movementTex = Content.Load<Texture2D>("movement_tutorial");
            powerTex = Content.Load<Texture2D>("power_circuit_tutorial");
            pickupTex = Content.Load<Texture2D>("pickup_tutorial");
            placeTex = Content.Load<Texture2D>("place_object_tutorial");
            cycleTex = Content.Load<Texture2D>("cycle_inventory_tutorial");
            voltTex = Content.Load<Texture2D>("voltmeter_tutorial");
            exitTex = Content.Load<Texture2D>("exit_tutorial");

            //tutorial data instantiation
            TutorialDatabase movementTutorial = new TutorialDatabase("movement_tutorial", movementTex);
            TutorialDatabase powerTutorial = new TutorialDatabase("power_circuit_tutorial", powerTex);
            TutorialDatabase pickupTutorial = new TutorialDatabase("pickup_tutorial", pickupTex);
            TutorialDatabase placeTutorial = new TutorialDatabase("place_object_tutorial", placeTex);
            TutorialDatabase cycleTutorial = new TutorialDatabase("cycle_inventory_tutorial", cycleTex);
            TutorialDatabase voltmeterTutorial = new TutorialDatabase("voltmeter_tutorial", voltTex);
            TutorialDatabase exitTutorial = new TutorialDatabase("exit_tutorial", exitTex);

            //add to tutorial data list
            tutorialDataList = new List<TutorialDatabase>();
            tutorialDataList.Add(movementTutorial);
            tutorialDataList.Add(powerTutorial);
            tutorialDataList.Add(pickupTutorial);
            tutorialDataList.Add(placeTutorial);
            tutorialDataList.Add(cycleTutorial);
            tutorialDataList.Add(voltmeterTutorial);
            tutorialDataList.Add(exitTutorial);


            //voltmeter stuff
            voltMeterTex = Content.Load<Texture2D>("volt_meter");

            elevInfo2Flr = Content.Load<Texture2D>("elev_2floors");
            elevInfo3Flr = Content.Load<Texture2D>("elev_3floors");
            conveyInfo = Content.Load<Texture2D>("convey");
            doorInfo = Content.Load<Texture2D>("door");
            pwrSrcInfo = Content.Load<Texture2D>("pwr_src");
            copper = Content.Load<Texture2D>("copper");
            iron = Content.Load<Texture2D>("iron");
            alum = Content.Load<Texture2D>("aluminum");
            silver = Content.Load<Texture2D>("silver");

            //stat screen stuff
            statScreen = Content.Load<Texture2D>("stat_screen");
            incomplete = Content.Load<Texture2D>("incomplete");
            statBorder = Content.Load<Texture2D>("grading_meter");
            drawTracking = false;
            statOpenCount = 160;

            exitClosedTex = Content.Load<Texture2D>("exit_closed");


            levelList = new List<Level>();


            //todo
            
            
            //stat loging
            //hub world closed doors
            
            hubWorld = new Level("http://dullselmser.com/Levels/hub_World.txt", this, "hub world");
            tutorialLevel = new Level("http://dullselmser.com/Levels/tutorial_level.txt", this, "tutorial level");
            zone1Level1 = new Level("http://dullselmser.com/Levels/zone1_level1.txt", this, "zone 1, level 1");
            zone1Level2 = new Level("http://dullselmser.com/Levels/zone1_level2.txt", this, "zone 1, level 2");
            zone1Level3 = new Level("http://dullselmser.com/Levels/zone1_level3.txt", this, "zone 1, level 3");
            //
            zone2Level1 = new Level("http://dullselmser.com/Levels/zone2_level1.txt", this, "zone 2, level 1");
            zone2Level2 = new Level("http://dullselmser.com/Levels/zone2_level2.txt", this, "zone 2, level 2");
            zone2Level3 = new Level("http://dullselmser.com/Levels/zone2_level3.txt", this, "zone 2, level 3");
            //
            //zone3Level1 = new Level("http://dullselmser.com/Levels/zone3_level1.txt", this, "zone 3, level 1"); //crap level dereete
            //zone3Level2 = new Level("http://dullselmser.com/Levels/zone3_level2.txt", this, "zone 3, level 2");
            //zone3Level3 = new Level("http://dullselmser.com/Levels/zone3_level3.txt", this, "zone 3, level 3");
            //
            zone4Level1 = new Level("http://dullselmser.com/Levels/zone4_level1.txt", this, "zone 4, level 1");
            zone4Level2 = new Level("http://dullselmser.com/Levels/zone4_level2.txt", this, "zone 4, level 2");
            zone4Level3 = new Level("http://dullselmser.com/Levels/zone4_level3.txt", this, "zone 4, level 3");
            //
            zone5Level1 = new Level("http://dullselmser.com/Levels/zone5_level1.txt", this, "zone 5, level 1");
            zone5Level2 = new Level("http://dullselmser.com/Levels/zone5_level2.txt", this, "zone 5, level 2");
            zone5Level3 = new Level("http://dullselmser.com/Levels/zone5_level3.txt", this, "zone 5, level 3");
            //

            
            levelList.Add(hubWorld);
            levelList.Add(tutorialLevel);
            levelList.Add(zone1Level1);
            levelList.Add(zone1Level2);
            levelList.Add(zone1Level3);
            levelList.Add(zone2Level1);
            levelList.Add(zone2Level2);
            levelList.Add(zone2Level3);
            //levelList.Add(zone3Level1);
            //levelList.Add(zone3Level2);
            //levelList.Add(zone3Level3);
            levelList.Add(zone4Level1);
            levelList.Add(zone4Level2);
            levelList.Add(zone4Level3);
            levelList.Add(zone5Level1);
            levelList.Add(zone5Level2);
            levelList.Add(zone5Level3);
            currentLevel = mainMenu;
            playerRobot = new Player(playerText, this, currentLevel);

            
        }

        private void HandleClosedExits()
        {
            if (zone1Level3.ExitList != null)
            {
                hubWorld.ExitList[1].isOpen = false;
            }
            if (zone2Level3.ExitList != null)
            {
                hubWorld.ExitList[2].isOpen = false;
            }
            if (zone4Level3.ExitList != null)
            {
                hubWorld.ExitList[3].isOpen = false;
            }
            exitsHandled = true;
        }

        //creates a new credits screen
        public void InitializeCredits()
        {
            creditsScreen = new CreditsScreen(this);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            count++;
            if (count >= songOneDur && songOneBool)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(songTwo);
                count = 0;
                songOneBool = false;
            }
            if (count >= songTwoDur && !songOneBool)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(songOne);
                count = 0;
                songOneBool = true;
            }
            if (playingGame && Keyboard.GetState().IsKeyDown(Keys.P) && !scoreBool)
            {
                scoreBool = true;
                CalcScore();
                //doostuff
            }
            if (scoreCounter < 20)
            {
                scoreCounter++;
            }
            if (scoreCounter >= 20 && scoreBool)
            {
                scoreCounter = 0;
                scoreBool = false;
            }
            if (currentLevel != null && playerRobot != null && playerRobot.Distance(playerRobot.position, new Vector2(475, 375)) > 1200)
            {
                playerRobot.position = currentLevel.EntranceList[0].position;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                currentLevel = mainMenu;
            
            if (Keyboard.GetState().IsKeyDown(Keys.F12))
            {
                if (enableDebug == true)
                {
                    enableDebug = false;
                }
                else if (enableDebug == false)
                {
                    enableDebug = true;
                }
            }
          
            if (!splashScreen.ScreensComplete)
            {
                splashScreen.UpdateSplashScreen();
            }

            if (splashScreen.ScreensComplete)
            {
               
                if (playingGame && currentLevel != mainMenu && !tutorialPause)
                {
                    playerRobot.level = currentLevel;
                    playerRobot.Update();
                   
                }
                //if (creditsScreen != null && !creditsScreen.CreditsComplete)
                //{
                    currentLevel.Update();
                //}
            }

            if (creditsScreen != null && !creditsScreen.CreditsComplete)
            {
                creditsScreen.UpdateCredits();
            }


            if (playingGame && !exitsHandled)
            {
                HandleClosedExits();
            }

            if (playingGame)
            {
                CheckOpenExits();
                CheckOpenTracking();
                if (statTracker != null && drawTracking)
                {
                    statTracker.UpdateProgress();
                }
            }

            base.Update(gameTime);
        }

        private void CheckOpenExits()
        {
            if (zone1Level3.ExitList != null && zone1Level3.ExitList.Count > 0)
            {
                if (zone1Level3.ExitList[0].isTriggered)
                {
                    hubWorld.ExitList[1].isOpen = true;
                }
            }
            if (zone2Level3.ExitList != null && zone2Level3.ExitList.Count > 0)
            {
                if (zone2Level3.ExitList[0].isTriggered)
                {
                    hubWorld.ExitList[2].isOpen = true;
                }
            }
            if (zone4Level3.ExitList != null && zone4Level3.ExitList.Count > 0)
            {
                if (zone4Level3.ExitList[0].isTriggered)
                {
                    hubWorld.ExitList[3].isOpen = true;
                }
            }
            if (zone5Level3.ExitList != null && zone5Level3.ExitList.Count > 0)
            {
                if (zone5Level3.ExitList[0].isTriggered)
                {
                    hubWorld.ExitList[4].isOpen = true;
                }
            }
        }
        public void CalcScore()
        {
            scoreList = new List<int>();
            if (actualAttempts != null && actualAttempts.Count > 0 && theoreticalAttempts != null && theoreticalAttempts.Count > 0)
            {
                for (int i = 0; i <= actualAttempts.Count - 1; i++)
                {
                    int tmp;
                    tmp = (100 - (3 * (actualAttempts[i] - theoreticalAttempts[i])));
                    if (tmp > 100)
                    {
                        tmp = 100;
                    }
                    scoreList.Add(tmp);
                }
                while (scoreList.Count < theoreticalAttempts.Count)
                {
                    scoreList.Add(0);
                }
            }
            else if(scoreList != null && scoreList.Count <= 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    scoreList.Add(0);
                }
            }
        }

        public void CheckOpenTracking()
        {
            if (statOpenCount > 0)
            {
                statOpenCount--;
            }
            if (playingGame && Keyboard.GetState().IsKeyDown(Keys.P) && statTracker == null
                && statOpenCount == 0)
            {
                drawTracking = true;
                statTracker = new ProgressTracker(this);
            }

            if (drawTracking == false && !gameBeat)
            {
                statTracker = null;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.BackToFront, SaveStateMode.None);

            if (!splashScreen.ScreensComplete)
            {
                splashScreen.DrawSplashScreen(spriteBatch);
            }

            if (creditsScreen != null && !creditsScreen.CreditsComplete)
            {
                creditsScreen.DrawCredits(spriteBatch);
            }

            if (splashScreen.ScreensComplete)
            {
                currentLevel.Draw(spriteBatch);
                if (enableDebug)
                {
                    foreach (BarrierComponent b in currentLevel.barrierList)
                    {
                        b.Render(spriteBatch);
                    }
                    spriteBatch.Draw(barrierDebug, playerRobot.actualRect, new Rectangle(0, 0, playerRobot.actualRect.Width, playerRobot.actualRect.Height), Color.White, playerRobot.rotation, Vector2.Zero, SpriteEffects.None, .1f);
                }
                if (playingGame && currentLevel != mainMenu)
                {
                    playerRobot.Render(spriteBatch);
                }

                if (statTracker != null && drawTracking)
                {
                    statTracker.DrawProgress(spriteBatch);
                }
                
            }
            spriteBatch.End(); //

            base.Draw(gameTime);
        }
    }
}
