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

namespace CGDD4303_Silverlight
{
    public class Scanner
    {
        public enum ScannerState { Elevator, Conveyor, Door, PowerSource, Items };
        public ScannerState state;
        Game1 game;
        Player host;
        public Level level;
        public bool isOn, runOnce, counterBool;
        int stateIndex, count;
        int listIndexCap;
        List<float> currentPowerRequiredElevatorFloors;
        float currentPower, currentPowerModifier;
        float currentPowerRequired;
        string currentName;
        private Texture2D voltMeterTex;
        private Vector2 voltMeterPos;
        private ConductivityMeter copperMeter, ironMeter, aluminumMeter, silverMeter;
        private PowerMeter pwrMeter1, pwrMeter2, pwrMeter3;

        private Vector2 conductPos, pwrMeter1Pos, pwrMeter2Pos, pwrMeter3Pos;

        public Scanner(Game1 g, Player p, Level l)
        {
            conductPos = new Vector2(790, 185);
            pwrMeter1Pos = new Vector2(790, 120);
            pwrMeter2Pos = new Vector2(790, 160);
            pwrMeter3Pos = new Vector2(790, 220);
            game = g;
            host = p;
            level = l;
            isOn = false;
            runOnce = false;
            counterBool = true;
            count = 0;
            stateIndex = 0;
            listIndexCap = level.ElevatorList.Count - 1;
            state = ScannerState.Elevator;
            currentName = "not assigned yet";
            currentPower = 0;
            currentPowerModifier = 0;
            currentPowerRequired = 0;
            currentPowerRequiredElevatorFloors = new List<float>();
            voltMeterTex = game.voltMeterTex;
            voltMeterPos = new Vector2(750, 0);

        }
        //MAIN UPDATE
        public void Update()
        {

            HandleCounter();
            HandleColor();
            level = game.currentLevel;
            HandleScannerLogic();
            UpdateMeters();
        }

        private void UpdateMeters()
        {
            if (copperMeter != null)
            {
                copperMeter.UpdateMeter();
            }
            if (aluminumMeter != null)
            {
                aluminumMeter.UpdateMeter();
            }
            if (ironMeter != null)
            {
                ironMeter.UpdateMeter();
            }
            if (silverMeter != null)
            {
                silverMeter.UpdateMeter();
            }

            if (pwrMeter1 != null)
            {
                pwrMeter1.UpdateMeter();
            }
            if (pwrMeter2 != null)
            {
                pwrMeter2.UpdateMeter();
            }
            if (pwrMeter3 != null)
            {
                pwrMeter3.UpdateMeter();
            }
        }

        //MAIN DRAW
        public void Render(SpriteBatch sb)
        {
            if (isOn)
            {
                DrawVoltMeter(sb);
            }
        }

        private void DrawVoltMeter(SpriteBatch sb)
        {
            sb.Draw(voltMeterTex, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .001f);


            switch (state)
            {
                case ScannerState.Elevator:
                    pwrMeter1 = new PowerMeter(pwrMeter1Pos, 2, game.powerBorder, game.powerBar,
                        game, null, currentPower);
                    pwrMeter1.DrawMeter(sb);

                    if (level.ElevatorList[host.InvCursorCount].elevatorFloors.Count > 2)
                    {
                        sb.Draw(game.elevInfo3Flr, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);

                        pwrMeter2 = new PowerMeter(pwrMeter2Pos, 2, game.powerBorder, game.powerBar,
                        game, null, currentPowerRequiredElevatorFloors[1]);

                        pwrMeter3 = new PowerMeter(new Vector2(pwrMeter3Pos.X, pwrMeter3Pos.Y - 20), 2, game.powerBorder, game.powerBar,
                        game, null, currentPowerRequiredElevatorFloors[2]);

                        pwrMeter2.DrawMeter(sb);
                        pwrMeter3.DrawMeter(sb);
                    }
                    else
                    {
                        sb.Draw(game.elevInfo2Flr, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);

                        pwrMeter2 = new PowerMeter(pwrMeter2Pos, 2, game.powerBorder, game.powerBar,
                        game, null, currentPowerRequiredElevatorFloors[1]);

                        pwrMeter2.DrawMeter(sb);
                    }
                    break;

                case ScannerState.Door:
                    sb.Draw(game.conveyInfo, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);

                    pwrMeter2 = new PowerMeter(new Vector2(pwrMeter2Pos.X, pwrMeter2Pos.Y - 25), 2, game.powerBorder, game.powerBar,
                    game, null, currentPower);

                    pwrMeter3 = new PowerMeter(new Vector2(pwrMeter3Pos.X, pwrMeter3Pos.Y - 45), 2, game.powerBorder, game.powerBar,
                    game, null, currentPowerRequired);

                    pwrMeter2.DrawMeter(sb);
                    pwrMeter3.DrawMeter(sb);

                    break;

                case ScannerState.Conveyor:
                    sb.Draw(game.conveyInfo, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);

                    pwrMeter2 = new PowerMeter(new Vector2(pwrMeter2Pos.X, pwrMeter2Pos.Y - 25), 2, game.powerBorder, game.powerBar,
                    game, null, currentPower);

                    pwrMeter3 = new PowerMeter(new Vector2(pwrMeter3Pos.X, pwrMeter3Pos.Y - 45), 2, game.powerBorder, game.powerBar,
                    game, null, currentPowerRequired);

                    pwrMeter2.DrawMeter(sb);
                    pwrMeter3.DrawMeter(sb);

                    break;

                case ScannerState.PowerSource:
                    sb.Draw(game.pwrSrcInfo, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);


                    pwrMeter3 = new PowerMeter(new Vector2(pwrMeter3Pos.X, pwrMeter3Pos.Y - 45), 2, game.powerBorder, game.powerBar,
                    game, null, currentPower);


                    pwrMeter3.DrawMeter(sb);

                    break;

                case ScannerState.Items:
                    if (level.Inventory.inventoryList.Count >= 1 &&
                        level.Inventory.inventoryList[host.InvCursorCount].name == "copper wire")
                    {
                        sb.Draw(game.copper, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);


                        copperMeter = new ConductivityMeter(conductPos, 2, game.conductBorder, game.conductBar, game,
                            null, level.Inventory.inventoryList[host.InvCursorCount].powerModifier);

                        if (copperMeter != null)
                        {
                            copperMeter.DrawMeter(sb);
                        }
                    }
                    else if (level.Inventory.inventoryList.Count >= 1 &&
                        level.Inventory.inventoryList[host.InvCursorCount].name == "iron wire")
                    {
                        sb.Draw(game.iron, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);


                        ironMeter = new ConductivityMeter(conductPos, 2, game.conductBorder, game.conductBar, game,
                            null, level.Inventory.inventoryList[host.InvCursorCount].powerModifier);

                        if (ironMeter != null)
                        {
                            ironMeter.DrawMeter(sb);
                        }

                    }
                    else if (level.Inventory.inventoryList.Count >= 1 &&
                       level.Inventory.inventoryList[host.InvCursorCount].name == "aluminum wire")
                    {
                        sb.Draw(game.alum, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);


                        aluminumMeter = new ConductivityMeter(conductPos, 2, game.conductBorder, game.conductBar, game,
                            null, level.Inventory.inventoryList[host.InvCursorCount].powerModifier);

                        if (aluminumMeter != null)
                        {
                            aluminumMeter.DrawMeter(sb);
                        }

                    }
                    else if (level.Inventory.inventoryList.Count >= 1 &&
                       level.Inventory.inventoryList[host.InvCursorCount].name == "silver wire")
                    {
                        sb.Draw(game.silver, voltMeterPos, new Rectangle(0, 0, voltMeterTex.Width, voltMeterTex.Height),
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .0001f);


                        silverMeter = new ConductivityMeter(conductPos, 2, game.conductBorder, game.conductBar, game,
                            null, level.Inventory.inventoryList[host.InvCursorCount].powerModifier);


                        if (silverMeter != null)
                        {
                            silverMeter.DrawMeter(sb);
                        }
                    }
                    break;
            }
        }


        public void HandleCounter()
        {
            if (count < 0)
            {
                count = 0;
            }
            else if (count > 0)
            {
                count--;
            }
            else if (count == 0 && counterBool == false)
            {
                counterBool = true;
            }
        }
        public void ActivateScanner()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.O) && counterBool)
            {
                level.showInv = !level.showInv;
                isOn = !isOn;
                counterBool = false;
                count = 20;
            }
        }
        public void HandleScannerControls()
        {
            ActivateScanner();
            if (isOn)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Up) && host.cursorBool && stateIndex < 4)
                {
                    stateIndex++;
                    runOnce = false;
                    host.cursorBool = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && host.cursorBool && stateIndex > 0)
                {
                    stateIndex--;
                    runOnce = false;
                    host.cursorBool = false;
                }
                if (host.InvCursorCount < 0)
                {
                    host.InvCursorCount = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && host.cursorBool && host.InvCursorCount > 0)
                {
                    host.InvCursorCount--;
                    host.cursorBool = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && host.cursorBool && host.InvCursorCount < listIndexCap)
                {
                    host.InvCursorCount++;
                    host.cursorBool = false;
                }

            }
        }
        public void DetermineState()
        {

            HandleScannerControls();
            state = (ScannerState)(stateIndex);
        }
        public void HandleScannerLogic()
        {
            DetermineState();
            if (!runOnce)
            {
                host.InvCursorCount = 0;
                listIndexCap = 0;
                runOnce = true;
            }
            if (isOn)
            {
                switch (state)
                {
                    case ScannerState.Elevator:
                        if (level.ElevatorList != null)
                        {
                            if (level.ElevatorList.Count > 0)
                            {
                                listIndexCap = level.ElevatorList.Count - 1;

                                currentPowerRequired = 0;
                                currentPowerModifier = 0;
                                currentPowerRequiredElevatorFloors = level.ElevatorList[host.InvCursorCount].powerRequired;
                                currentPower = level.ElevatorList[host.InvCursorCount].connectedWire.powerLevel;
                                currentName = level.ElevatorList[host.InvCursorCount].elevName;
                            }
                        }
                        break;
                    case ScannerState.Conveyor:
                        if (level.conveyorList != null)
                        {
                            if (level.conveyorList.Count > 0)
                            {
                                listIndexCap = level.conveyorList.Count - 1;

                                currentPower = level.conveyorList[host.InvCursorCount].connectedWire.powerLevel;
                                currentPowerModifier = 0;
                                currentPowerRequiredElevatorFloors = new List<float>();
                                currentPowerRequired = level.conveyorList[host.InvCursorCount].powerRequired;
                                currentName = level.conveyorList[host.InvCursorCount].conveyorName;
                            }
                        }
                        break;
                    case ScannerState.Door:
                        if (level.DoorList != null)
                        {
                            if (level.DoorList.Count > 0)
                            {
                                listIndexCap = level.DoorList.Count - 1;

                                currentPower = level.DoorList[host.InvCursorCount].ConnectedWire.powerLevel;
                                currentPowerModifier = 0;
                                currentPowerRequiredElevatorFloors = new List<float>();
                                currentPowerRequired = (float)level.DoorList[host.InvCursorCount].powerRequired;
                                currentName = level.DoorList[host.InvCursorCount].doorName;
                            }
                        }
                        break;
                    case ScannerState.PowerSource:
                        if (level.pwrSrcList != null)
                        {
                            if (level.pwrSrcList.Count > 0)
                            {
                                listIndexCap = level.pwrSrcList.Count - 1;
                                currentPower = level.pwrSrcList[host.InvCursorCount].PowerSupplied;
                                currentPowerModifier = 0;
                                currentPowerRequiredElevatorFloors = new List<float>();
                                currentPowerRequired = 0;
                                currentName = level.pwrSrcList[host.InvCursorCount].pwrSrcName;
                            }
                        }

                        break;
                    case ScannerState.Items:
                        if (level.Inventory != null)
                        {
                            if (level.Inventory.inventoryList.Count > 0)
                            {
                                listIndexCap = level.Inventory.inventoryList.Count - 1;
                                currentPower = 0;
                                currentPowerModifier = level.Inventory.inventoryList[host.InvCursorCount].PowerModifier;
                                currentPowerRequiredElevatorFloors = new List<float>();
                                currentPower = 0;
                                currentName = level.Inventory.inventoryList[host.InvCursorCount].name;
                            }
                        }
                        break;
                }
            }
        }

        public void HandleColor()
        {
            foreach (Elevator e in level.ElevatorList)
            {
                e.color = Color.White;
            }
            foreach (ConveyorBelt c in level.conveyorList)
            {
                c.color = Color.White;
            }
            foreach (Door d in level.DoorList)
            {
                d.color = Color.White;
            }
            foreach (PowerSource p in level.pwrSrcList)
            {
                p.color = Color.White;
            }
            foreach (PickupableItem p in level.Inventory.inventoryList)
            {
                p.color = Color.White;
            }
            if (isOn)
            {
                switch (state)
                {
                    case ScannerState.Elevator:
                        if (level.ElevatorList != null)
                        {
                            foreach (Elevator e in level.ElevatorList)
                            {
                                if (level.ElevatorList.Count > 0)
                                {
                                    if (e == level.ElevatorList[host.InvCursorCount])
                                    {
                                        e.color = Color.Red;
                                    }
                                }
                            }
                        }
                        break;
                    case ScannerState.Conveyor:
                        if (level.conveyorList != null)
                        {
                            foreach (ConveyorBelt c in level.conveyorList)
                            {
                                if (level.conveyorList.Count > 0)
                                {
                                    if (c == level.conveyorList[host.InvCursorCount])
                                    {
                                        c.color = Color.Red;
                                    }
                                }
                            }
                        }
                        break;
                    case ScannerState.Door:
                        if (level.DoorList != null)
                        {
                            foreach (Door d in level.DoorList)
                            {
                                if (level.DoorList.Count > 0)
                                {
                                    if (d == level.DoorList[host.InvCursorCount])
                                    {
                                        d.color = Color.Red;
                                    }
                                }
                            }
                        }
                        break;
                    case ScannerState.PowerSource:
                        if (level.pwrSrcList != null)
                        {
                            foreach (PowerSource p in level.pwrSrcList)
                            {
                                if (level.pwrSrcList.Count > 0)
                                {
                                    if (p == level.pwrSrcList[host.InvCursorCount])
                                    {
                                        p.color = Color.Red;
                                    }
                                }
                            }
                        }
                        break;
                    case ScannerState.Items:
                        if (level.Inventory.inventoryList != null)
                        {
                            foreach (PickupableItem p in level.Inventory.inventoryList)
                            {
                                if (level.Inventory.inventoryList.Count > 0)
                                {
                                    if (p == level.Inventory.inventoryList[host.InvCursorCount])
                                    {
                                        p.color = Color.Red;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}