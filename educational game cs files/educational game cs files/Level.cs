using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Security;
using Microsoft.Win32;
using System.Security.Permissions;

//Level will probably be an inheritance polymorphism structure.  Level then the sub levels will inherit from it.
//Keep this class as generic as possible
//There will be an update and a draw that handle all of the levels game logic, initialize in the specific sub levels
//We can use an enum case switch again in main to handle the current level but lets keep it to a simple update() and draw() and nothing more in main.  Keep the bulk in these classes.

namespace CGDD4303_Silverlight
{
    public class Level
    {
        //attributes of the level
        private List<LevelPiece> levelPieceList;
        public List<BarrierComponent> barrierList;
        private List<Door> doorList;
        private List<Circuit> circuitList;
        private List<Elevator> elevatorList;
        public List<PickupableItem> itemInLevelList;
        public List<PowerSource> pwrSrcList;
        private List<Entrance> entranceList;
        private List<Exit> exitList;
        private List<Event> eventList;
        public List<int> tempIntList;
        public List<float> tempFloatList;
        public List<bool> tempBoolList;
        public List<Vector2> tempVector2List;
        public List<string> tempListOfNames;
        public List<ConveyorBelt> conveyorList;
        private StreamReader streamIn;
        private string textFileName;
        private Game1 game;
        public string levelName; //tag to switch between levels
        private short teleportCount; //limits time inbetween teleporting
        private Background background;
        private ScreenTransition transition;
        public Inventory inventory;
      
        //more stuff internet stream reader stuff
        public WebClient wc;
        public Stream stream;
        public StreamReader reader;
        public string content;
        public int circuitCount, attemptsCount;
        bool runOnce,firstTimeExiting;
        public bool showInv;
        //Example background texture, the i/o level information from txt document, lists?,
        public Level(string t, Game1 g, string l)
        {
            
            levelName = l;
            textFileName = t;
            game = g;
            //instantiate lists
            levelPieceList = new List<LevelPiece>();
            barrierList = new List<BarrierComponent>();
            doorList = new List<Door>();
            elevatorList = new List<Elevator>();
            inventory = new Inventory(new Vector2((950/2)-(game.inventoryTexture.Width/2), 550), game, game.inventoryTexture);
            itemInLevelList = new List<PickupableItem>();
            pwrSrcList = new List<PowerSource>();
            conveyorList = new List<ConveyorBelt>();
            teleportCount = 30;
            InitializeTransition();
            circuitCount = 0;
            doorList = new List<Door>();
            circuitList = new List<Circuit>();
            elevatorList = new List<Elevator>();
            entranceList = new List<Entrance>();
            exitList = new List<Exit>();
            eventList = new List<Event>();
            tempIntList = new List<int>();
            tempBoolList = new List<bool>();
            tempFloatList = new List<float>();
            tempVector2List = new List<Vector2>();
            tempListOfNames = new List<string>();
            showInv = false;
            firstTimeExiting = true;
            attemptsCount = 0;
            //load level objects
            Initialize();
        }

        public virtual void Initialize()
        {

            //StreamLevelInfo();
            if (textFileName != "no_text_file")
            {
                internetStreamReader(textFileName);
            }

        }

        public void InitializeTransition()
        {
            transition = new ScreenTransition(game);
        }

        private void StreamLevelInfo()
        {
            string category;
            while ((category = streamIn.ReadLine()) != null)
            {
                if (category == "BACKGROUND")
                {
                    category = StreamBackground(category);
                }
                else if (category == "LEVEL PIECES")
                {
                    category = StreamLevelPieces(category);
                }
                else if (category == "ENTRANCES")
                {
                    category = StreamEntrances(category);
                }
                else if (category == "EXITS")
                {
                    category = StreamExits(category);
                }
                else if (category == "ITEMS")
                {
                    category = StreamItems(category);
                }
                else if (category == "POWER SOURCES")
                {
                    category = StreamPowerSources(category);
                }
                else if (category == "CIRCUITS")
                {
                    category = StreamCircuits(category);
                }
                else if (category == "BRANCHES")
                {
                    category = StreamBranches(category);
                }
                else if (category == "DOORS")
                {
                    category = StreamDoors(category);
                }
                else if (category == "CONVEYOR BELTS")
                {
                    category = StreamConveyors(category);
                }
                else if (category == "ELEVATORS")
                {
                    category = StreamElevators(category);
                }
                else if (category == "EVENTS")
                {
                    category = StreamEvents(category);
                }
            }

        }
        public void internetStreamReader(string address)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(address);
            client.OpenReadAsync(uri);
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(internetStreamReaderCallBack);
            

        }
        public void internetStreamReaderCallBack(Object sender, OpenReadCompletedEventArgs e)
        {
            Stream reply = null;
            streamIn = null;
            try
            {
                reply = (Stream)e.Result;
                streamIn = new StreamReader(reply);
                StreamLevelInfo();
            }
            finally
            {
                if (streamIn != null)
                {
                    streamIn.Close();
                }

                if (reply != null)
                {
                    reply.Close();
                }
            }
        }
        //parsing for level pieces
        private string StreamLevelPieces(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "LEVEL PIECES DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string texType = textLine;
                int width = Int32.Parse(streamIn.ReadLine());
                int height = Int32.Parse(streamIn.ReadLine());

                LevelPiece temp = new LevelPiece(tempPos, null, textLine, game, width, height, this);
                levelPieceList.Add(temp);

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for background
        private string StreamBackground(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "BACKGROUND DONE" && correctlyRead)
            {
                
                string texType = textLine;

                background = new Background(game.mainBackground, game, texType);

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for entrances
        private string StreamEntrances(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "ENTRANCES DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string entranceName = textLine;
                int width = Int32.Parse(streamIn.ReadLine());
                int height = Int32.Parse(streamIn.ReadLine());
                string lvlDestination = streamIn.ReadLine();
                string doorDestination = streamIn.ReadLine();

                Entrance temp = new Entrance(tempPos, game.smallExitTex, entranceName,
                    game, game.playerRobot);
                temp.destinationLevel = lvlDestination;
                temp.destinationDoor = doorDestination;
                entranceList.Add(temp);

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for exits
        private string StreamExits(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "EXITS DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string exitName = textLine;
                int width = Int32.Parse(streamIn.ReadLine());
                int height = Int32.Parse(streamIn.ReadLine());
                string lvlDestination = streamIn.ReadLine();
                string doorDestination = streamIn.ReadLine();

                Exit temp = new Exit(tempPos, game.smallExitTex, exitName,
                    game, game.playerRobot);
                temp.destinationLevel = lvlDestination;
                temp.destinationDoor = doorDestination;
                exitList.Add(temp);

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for events
        private string StreamEvents(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "EVENTS DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string eventName = textLine;
                int width = Int32.Parse(streamIn.ReadLine());
                int height = Int32.Parse(streamIn.ReadLine());
                string eventTag = streamIn.ReadLine();
                string eventType = streamIn.ReadLine();

                if(eventType == "tutorial"){
                    Event temp = new Tutorial(tempPos, game.smallExitTex, eventName, 
                        eventTag, eventType, game, game.playerRobot);
                    eventList.Add(temp);
                }

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for pickupable items
        private string StreamItems(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "ITEMS DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string wireName = textLine;
                int width = Int32.Parse(streamIn.ReadLine());
                int height = Int32.Parse(streamIn.ReadLine());
                float powerMod = float.Parse(streamIn.ReadLine());

                PickupableItem temp = new PickupableItem(tempPos, game, game.copperTex, powerMod, this);
                itemInLevelList.Add(temp);

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for power sources
        private string StreamPowerSources(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "POWER SOURCES DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string pwrSrcName = textLine;
                int width = Int32.Parse(streamIn.ReadLine());
                int height = Int32.Parse(streamIn.ReadLine());
                float fullPower = float.Parse(streamIn.ReadLine());

                PowerSource tmpPwrSrc = new PowerSource(tempPos, game, game.powerBoxOff,
                    fullPower, pwrSrcName);
                pwrSrcList.Add(tmpPwrSrc);

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for circuits
        private string StreamCircuits(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "CIRCUITS DONE" && correctlyRead)
            {
                string circuitName = textLine;
                float fullPower = float.Parse(streamIn.ReadLine());

                while (textLine != "WIRES")
                {
                    textLine = streamIn.ReadLine();
                }

                if (textLine == "WIRES")
                {
                    StreamWires(textLine);
                    circuitList.Add(new Circuit(tempVector2List, pwrSrcList[circuitCount], game, 
                        game.horizontal, tempIntList, tempBoolList, circuitName, tempListOfNames));
                    circuitCount++;
                }

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for branches
        private string StreamBranches(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "BRANCHES DONE" && correctlyRead)
            {
                string branchName = textLine;
                string wireToLookFor = streamIn.ReadLine();                
                Wire tempWire = ReturnConnectedWire(wireToLookFor);
                while (textLine != "WIRES")
                {
                    textLine = streamIn.ReadLine();
                }

                if (textLine == "WIRES")
                {
                    StreamWires(textLine);
                    circuitList.Add(new Circuit(tempVector2List, tempWire.powerSource,game,
                        game.horizontal, tempIntList, tempBoolList, tempWire, branchName, tempListOfNames));
                    circuitCount++;
                }

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }
        
        //parsing wires for circuits and branches
        private string StreamWires(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            tempBoolList = new List<bool>();
            tempIntList = new List<int>();
            tempVector2List = new List<Vector2>();
            tempListOfNames = new List<string>();

            while (textLine != "WIRES DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string wireName = textLine;
                int dir = Int32.Parse(streamIn.ReadLine());
                bool socketed = bool.Parse(streamIn.ReadLine());

                tempListOfNames.Add(wireName);
                tempIntList.Add(dir);
                tempBoolList.Add(socketed);
                tempVector2List.Add(tempPos);

                

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }
        //parsing for The Doors.
        private string StreamDoors(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "DOORS DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string doorName = textLine;
                float reqPwr = float.Parse(streamIn.ReadLine());
                string wire = StreamIn.ReadLine();
                Wire tempWire = ReturnConnectedWire(wire);

                Door tempDoor = new Door(tempPos, game, game.doorSpriteSheet, (int)reqPwr, tempWire, this, doorName);
                doorList.Add(tempDoor);

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for The Doors.
        private string StreamConveyors(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "CONVEYOR BELTS DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);

                textLine = streamIn.ReadLine();
                string conveyorName = textLine;
                float reqPwr = float.Parse(streamIn.ReadLine());
                bool isClockwise = bool.Parse(streamIn.ReadLine());
                string wire = StreamIn.ReadLine();
                Wire tempWire = ReturnConnectedWire(wire);

                ConveyorBelt tempCB = new ConveyorBelt(tempPos, game.conveyorBeltTexture, 
                    game, this, tempWire, (int)reqPwr, tempWire.powerSource.PowerSupplied, 
                    tempWire.powerSource.PowerSupplied / 1000, isClockwise, conveyorName);
                //possibly change powersouce powersupplied/1000 later to some var read
                //in from the level editor but not needed really

                conveyorList.Add(tempCB);
                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //parsing for elevators
        private string StreamElevators(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            while (textLine != "ELEVATORS DONE" && correctlyRead)
            {
                string elevatorName = textLine;
                string wire = streamIn.ReadLine();
                Wire tempWire = ReturnConnectedWire(wire);
                textLine = streamIn.ReadLine();
                while (textLine != "ELEVATOR POSITIONS")
                {
                    textLine = streamIn.ReadLine();
                }
                if (textLine == "ELEVATOR POSITIONS")
                {
                    StreamElePositions(textLine);
                }
                while (textLine != "ELEVATOR PWR REQS")
                {
                    textLine = streamIn.ReadLine();
                }
                if (textLine == "ELEVATOR PWR REQS")
                {
                    StreamElePower(textLine);
                    elevatorList.Add(new Elevator(tempVector2List[0], game, game.elevatorBox, 
                        tempVector2List, tempFloatList, tempWire, this, elevatorName));
                }

                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }
        //parsing positions for elevators
        private string StreamElePositions(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();            
            tempVector2List = new List<Vector2>();

            while (textLine != "ELEVATOR POSITIONS DONE" && correctlyRead)
            {
                string[] splitVect = textLine.Split(',');
                int xValue = Int32.Parse(splitVect[0]);
                int yValue = Int32.Parse(splitVect[1]);
                Vector2 tempPos = new Vector2(xValue, yValue);
                tempVector2List.Add(tempPos);



                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }
        //parsing positions for elevators
        private string StreamElePower(string category)
        {
            bool correctlyRead = true;
            string textLine = category;
            textLine = streamIn.ReadLine();
            tempFloatList = new List<float>();
            tempFloatList.Add(1f);
            
            while (textLine != "ELEVATOR PWR REQS DONE" && correctlyRead)
            {
                float pwrReq = float.Parse(textLine);
                tempFloatList.Add(pwrReq);



                textLine = streamIn.ReadLine();
                if (textLine == "--")
                {
                    correctlyRead = true;
                    textLine = streamIn.ReadLine();
                }
                else
                {
                    correctlyRead = false;
                }
            }
            return textLine;
        }

        //MAIN UPDATE
        public virtual void Update()
        {
             //temp?
            //Update obj/characters/level specifics
            if (transition.TransitionDone)
            {
                UpdateBarriers();
                UpdateDoors();
                UpdateElevators();
                UpdateEntrances();
                UpdateExits();
                UpdateEvents();
                UpdatePwrSources();
                UpdateConveyors();
                UpdateCircuits();
                UpdateInventory();
                UpdateItemsInLevel();
                //handles teleporting
                if (teleportCount > 0)
                {
                    teleportCount--;
                }
                GoThroughEntrance();
                GoThroughExit();
            }
            CheckUpdateTransition();
            //background.Update();
        }


        //go through entrance doors
        private void GoThroughEntrance()
        {
            if (teleportCount == 0)
            {
                foreach (Entrance e in entranceList)
                {
                    if (e.Entering)
                    {
                        FindSpawnLocation(e);
                    }
                }
            }
        }

        //go through exit doors
        private void GoThroughExit()
        {
            if (teleportCount == 0)
            {
                foreach (Exit e in exitList)
                {
                    if (e.Exiting && levelName != "zone 5, level 3")
                    {
                        FindSpawnLocation(e);
                        game.playerRobot.InvCursorCount = 0;
                        game.playerRobot.scanner.isOn = false;
                        showInv = false;
                        if (firstTimeExiting && e == this.exitList[0] && levelName != "hub world")
                        {
                            firstTimeExiting = false;
                            game.actualAttempts.Add(attemptsCount);
                        }

                    }
                    else if (e.Exiting && levelName == "zone 5, level 3")
                    {
                        if (e.Name == "exit1")
                        {
                            //trigger credits
                            game.gameBeat = true;
                            game.InitializeCredits();
                        }
                        else if (e.Name != "exit1")
                        {
                            FindSpawnLocation(e);
                            game.playerRobot.InvCursorCount = 0;
                            game.playerRobot.scanner.isOn = false;
                            showInv = false;
                        }
                    }
                }
            }
        }

        //finds a match for the spawn location, teleports you(FOR ENTRANCES)
        private void FindSpawnLocation(Entrance e)
        {
            Level temp = null;
            Vector2 spawnPos = new Vector2(0, 0);
            bool spawnPointFound = false;
            bool levelFound = false;
            foreach (Level l in game.levelList)
            {
                if (e.destinationLevel == l.levelName)
                {
                    temp = l;
                    levelFound = true;
                    foreach (Entrance otherEnt in temp.entranceList)
                    {
                        if (otherEnt.Name == e.destinationDoor)
                        {
                            spawnPos = otherEnt.position;
                            spawnPointFound = true;
                        }
                    }
                    if (!spawnPointFound)
                    {
                        foreach (Exit otherEx in temp.exitList)
                        {
                            if (otherEx.Name == e.destinationDoor)
                            {
                                spawnPos = otherEx.position;
                                spawnPointFound = true;
                            }
                        }
                    }
                }  
            }
            if (spawnPointFound && levelFound && temp!= null)
            {
                game.playerRobot.position = spawnPos;
                temp.InitializeTransition();
                game.currentLevel = temp;
                teleportCount = 30;
            }
        }

        //finds a match for the spawn location, teleports you(FOR EXITS)
        private void FindSpawnLocation(Exit e)
        {
            Level temp = null;
            Vector2 spawnPos = new Vector2(0, 0);
            bool spawnPointFound = false;
            bool levelFound = false;
            foreach (Level l in game.levelList)
            {
                if (e.destinationLevel == l.levelName)
                {
                    temp = l;
                    levelFound = true;
                    foreach (Entrance otherEnt in temp.entranceList)
                    {
                        if (otherEnt.Name == e.destinationDoor)
                        {
                            spawnPos = otherEnt.position;
                            spawnPointFound = true;
                        }
                    }
                    if (!spawnPointFound)
                    {
                        foreach (Exit otherEx in temp.exitList)
                        {
                            if (otherEx.Name == e.destinationDoor)
                            {
                                spawnPos = otherEx.position;
                                spawnPointFound = true;
                            }
                        }
                    }
                }
            }
            if (spawnPointFound && levelFound && temp != null)
            {
                game.playerRobot.position = spawnPos;
                temp.InitializeTransition();
                game.currentLevel = temp;
                teleportCount = 30;
            }
        }

        //checks to see if there is a current transition going on
        private void CheckUpdateTransition()
        {
            if (!transition.TransitionDone)
            {
                transition.UpdateTransition();
            }
        }

        //update the barriers of the levelPieces 
        private void UpdateBarriers()
        {
            if (barrierList != null)
            {
                foreach (BarrierComponent b in barrierList)
                {
                    b.Update();
                }
            }
        }
        private void UpdateInventory()
        {
            if (inventory != null)
            {
                foreach (PickupableItem i in inventory.inventoryList)
                {
                    i.Update();
                }
            }
        }
        private void UpdateItemsInLevel()
        {
            if (itemInLevelList != null)
            {
                foreach (PickupableItem i in itemInLevelList)
                {
                    i.Update();
                }
                for (int i = itemInLevelList.Count - 1; i >= 0; i--)
                {
                    if (itemInLevelList[i].delete)
                    {
                        itemInLevelList.Remove(itemInLevelList[i]);
                    }
                }
            }
        }
        //update doors
        private void UpdateDoors()
        {
            if (doorList != null)
            {
                foreach (Door d in doorList)
                {
                    d.Update();
                }
            }
        }

        //update elevators
        private void UpdateElevators()
        {
            if (elevatorList != null)
            {
                foreach (Elevator e in elevatorList)
                {
                    e.Update();
                }
            }
        }

        //update entrances
        private void UpdateEntrances()
        {
            if (entranceList != null)
            {
                foreach (Entrance e in entranceList)
                {
                    e.Update();
                }
            }
        }

        //update exits
        private void UpdateExits()
        {
            if (exitList != null)
            {
                foreach (Exit e in exitList)
                {
                    e.Update();
                }
            }
        }

        //update events
        private void UpdateEvents()
        {
            if (eventList != null)
            {
                foreach (Event e in eventList)
                {
                    e.Update();
                }
            }
        }

        //update power sources
        private void UpdatePwrSources()
        {
            if (pwrSrcList != null)
            {
                foreach (PowerSource p in pwrSrcList)
                {
                    p.Update();
                }
            }
        }

        //update circuits
        private void UpdateCircuits()
        {
            if (circuitList != null)
            {
                foreach (Circuit c in circuitList)
                {
                    c.Update();
                }
            }
        }

        //update conveyor belts
        private void UpdateConveyors()
        {
            if (conveyorList != null)
            {
                foreach (ConveyorBelt c in conveyorList)
                {
                    c.Update();
                }
            }
        }


        //MAIN DRAW
        public virtual void Draw(SpriteBatch sb)
        {
            
            //draw the background, objects, enemies of each level
            if (background != null)
            {
                background.Render(sb);
            }
            DrawEntrances(sb);
            DrawExits(sb);
            DrawEvents(sb);
            DrawConveyors(sb);
            DrawPwrSrcs(sb);
            DrawCircuits(sb);
            DrawDoors(sb);
            DrawElevators(sb);
            DrawLevelPieces(sb);
            DrawInventory(sb);
            DrawItemsInLevel(sb);
            CheckDrawTransition(sb);
        }

        //check to see if there is a transition that needs to be drawn
        private void CheckDrawTransition(SpriteBatch sb)
        {
            if (!transition.TransitionDone)
            {
                transition.DrawTransition(sb);
            }
        }
        //draw items
        private void DrawInventory(SpriteBatch sb)
        {
            if (inventory != null && showInv)
            {
                foreach (PickupableItem i in inventory.inventoryList)
                {
                    //i.Render(sb);
                }
                inventory.Render(sb);
            }
        }

        private void DrawItemsInLevel(SpriteBatch sb)
        {
            if (itemInLevelList != null)
            {
                foreach (PickupableItem i in itemInLevelList)
                {
                    i.Render(sb);
                }
            }
        }
        //Draw power sources
        private void DrawPwrSrcs(SpriteBatch sb)
        {
            if (pwrSrcList != null)
            {
                foreach (PowerSource p in pwrSrcList)
                {
                    p.Render(sb);
                }
            }
        }

        //Draw circuits
        private void DrawCircuits(SpriteBatch sb)
        {
            if (circuitList != null)
            {
                foreach (Circuit c in circuitList)
                {
                    c.Render(sb);
                }
            }
        }

        //Draw conveyor belts
        private void DrawConveyors(SpriteBatch sb)
        {
            if (conveyorList != null)
            {
                foreach (ConveyorBelt c in conveyorList)
                {
                    c.Render(sb);
                }
            }
        }

        //Draw level pieces
        private void DrawLevelPieces(SpriteBatch sb)
        {
            if (levelPieceList != null)
            {
                foreach (LevelPiece l in levelPieceList)
                {
                    l.Render(sb);
                }
            }
        }

        //Draw doors
        private void DrawDoors(SpriteBatch sb)
        {
            if (doorList != null)
            {
                foreach (Door d in doorList)
                {
                    d.Render(sb);
                }
            }
        }

        //Draw elevators
        private void DrawElevators(SpriteBatch sb)
        {
            if (elevatorList != null)
            {
                foreach (Elevator e in elevatorList)
                {
                    e.Render(sb);
                }
            }
        }

        //Draw entrances
        private void DrawEntrances(SpriteBatch sb)
        {
            if (entranceList != null)
            {
                foreach (Entrance e in entranceList)
                {
                    e.Render(sb);
                }
            }
        }

        //Draw exits
        private void DrawExits(SpriteBatch sb)
        {
            if (exitList != null)
            {
                foreach (Exit e in exitList)
                {
                    e.Render(sb);
                }
            }
        }

        //Draw Events
        private void DrawEvents(SpriteBatch sb)
        {
            if (eventList != null)
            {
                foreach (Event e in eventList)
                {
                    e.Render(sb);
                }
            }
        }

        public Wire ReturnCollidingSocketWire()
        {
            if (circuitList != null)
            {
                foreach (Circuit c in circuitList)
                {
                    foreach (Wire w in c.listOfWires)
                    {
                        if (w.colRect.Intersects(game.playerRobot.actualRect) && w.hasSocket == true)
                        {
                            return w;
                        }
                    }
                }
                return null;
            } return null;
        }
        private Wire ReturnConnectedWire(string s)
        {
            foreach (Circuit c in circuitList)
            {
                foreach (Wire w in c.listOfWires)
                {
                    if (w.name == s)
                    {
                        return w;
                    }
                }
            }
            return null;
        }
        public virtual void SpawnObjects()
        {
            //and put pickupable items in the list
        }

        public virtual void SpawnEnemies()
        {

        }


        ///PROPERTIES
        ///
        #region Properties

        public ScreenTransition Transition
        {
            set { transition = value; }
            get { return transition; }
        }

        public List<LevelPiece> LevelPieceList
        {
            set { levelPieceList = value; }
            get { return levelPieceList; }
        }

        public List<Door> DoorList
        {
            set { doorList = value; }
            get { return doorList; }
        }

        public List<Circuit> CircuitList
        {
            set { circuitList = value; }
            get { return circuitList; }
        }

        public List<Elevator> ElevatorList
        {
            set { elevatorList = value; }
            get { return elevatorList; }
        }

        public List<PickupableItem> ItemInLevelList
        {
            set { itemInLevelList = value; }
            get { return itemInLevelList; }
        }
        public Inventory Inventory
        {
            set { inventory = value; }
            get { return inventory; }
        }

        public List<Entrance> EntranceList
        {
            set { entranceList = value; }
            get { return entranceList; }
        }

        public List<Exit> ExitList
        {
            set { exitList = value; }
            get { return exitList; }
        }

        public StreamReader StreamIn
        {
            set { streamIn = value; }
            get { return streamIn; }
        }

        public string TextFileName
        {
            set { textFileName = value; }
            get { return textFileName; }
        }

        public Game1 Game
        {
            set { game = value; }
            get { return game; }
        }

        public short TeleportCount
        {
            set { teleportCount = value; }
            get { return teleportCount; }
        }

        #endregion
    }
}
