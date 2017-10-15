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

namespace CGDD4303_Silverlight
{
    public class Elevator : GameObject
    {
        public List<Vector2> elevatorFloors;
        Texture2D platform, track, button;
        Vector2 platformPos, nextPlatformPos;
        //additional attr for doors
        bool isPowered,goingDown, isButtonPressed, playerEleCheck,itemEleCheck,runOnceItem,runOncePlayer;
        public List<float> powerRequired;
        List<Rectangle> buttonRectList;
        AnimationComponent elevatorBoxAnimation;
        BarrierComponent barrier;
        int currentFloor;
        public Wire connectedWire;
        //Buttom elevBut put this into a list and when the player is intersecting a button and uses it, have the buttons host aka this elevator set its  isbuttonpressed bool to true? could also just be done with elevators
        private Level level;
        public Rectangle collisionRect;
        public string elevName;

        public Elevator(Vector2 p, Game1 g, Texture2D t, List<Vector2> eF, List<float> pR, Wire cW, Level l, string n)
            : base(t, g)
        {
            elevName = n;
            level = l;
            position = p;
            width = 100;
            height = 60;
            elevatorFloors = eF;
            platform = game.elevatorPlatform;
            track = game.elevatorTrack;
            button = game.eleButton;
            platformPos = eF[0];
            nextPlatformPos = eF[1];
            powerRequired = pR;
            connectedWire = cW;
            currentFloor = 0;
            goingDown = false;
            isButtonPressed = false;
            playerEleCheck = false;
            itemEleCheck = false;
            runOnceItem = false;
            runOncePlayer = false;
            elevatorBoxAnimation = new AnimationComponent(this);
            buttonRectList = new List<Rectangle>();
            instantiateButtonRects();
            barrier = new BarrierComponent(this.texture, platformPos, game, platform.Width, platform.Height);
            level.barrierList.Add(barrier);
            position.Y -= 60;
            collisionRect = new Rectangle((int)platformPos.X, (int)platformPos.Y-60, platform.Width, platform.Height+60);
            isCollidable = false;
            depth = .3f; //is this working for elevators
        }
        public override void Update()
        {
            DetermineIsPowered();
            elevatorButtonCall();
            CycleFloor();
            elevatorBoxAnimation.UpdateAnimation();
            barrier.position = platformPos;
            barrier.Update();
            collisionRect.X = (int)platformPos.X;
            collisionRect.Y = (int)platformPos.Y-60;
            base.Update();
        }

        public void CycleFloor()
        {
            if (isPowered && isButtonPressed)
            {
                if (((currentFloor + 1) <= elevatorFloors.Count - 1) && !goingDown && (connectedWire.PowerLevel >= powerRequired[currentFloor + 1]))
                {
                    if (platformPos.Y > elevatorFloors[currentFloor + 1].Y)
                    {
                        runOnceEleCheck();
                        platformPos.Y--;
                        //foreach obj if intersecting move pos at same rate? check distance
                        isOnElevatorPlatform(true);

                    }
                    else if (platformPos.Y < elevatorFloors[currentFloor + 1].Y)
                    {
                        platformPos.Y = elevatorFloors[currentFloor + 1].Y;
                        game.playerRobot.isOnEle = false;
                        playerEleCheck = false;
                        itemEleCheck = false;
                        runOnceItem = false;
                        runOncePlayer = false;
                    }
                    else if (platformPos.Y == elevatorFloors[currentFloor + 1].Y)
                    {
                        currentFloor += 1;
                        isButtonPressed = false;
                        if (currentFloor >= elevatorFloors.Count - 1)
                        {
                            goingDown = true;
                        }
                        else if (currentFloor < elevatorFloors.Count - 1)
                        {
                            goingDown = false;
                        }
                        game.playerRobot.isOnEle = false;
                        playerEleCheck = false;
                        itemEleCheck = false;
                        runOnceItem = false;
                        runOncePlayer = false;
                    }
                }
                else if (((currentFloor + 1) <= elevatorFloors.Count - 1) && !goingDown && (connectedWire.PowerLevel < powerRequired[currentFloor + 1]))
                {
                    goingDown = true;
                    game.playerRobot.isOnEle = false;
                    playerEleCheck = false;
                    itemEleCheck = false;
                    runOnceItem = false;
                    runOncePlayer = false;
                }
                else if (((currentFloor + 1) <= elevatorFloors.Count - 1) && !goingDown && (connectedWire.PowerLevel < powerRequired[currentFloor + 1]))
                {
                    //draw something on screen to tell the player
                    game.playerRobot.isOnEle = false;
                    playerEleCheck = false;
                    itemEleCheck = false;
                    runOnceItem = false;
                    runOncePlayer = false;
                }
                else if (((currentFloor - 1) >= 0) && goingDown && (connectedWire.PowerLevel >= powerRequired[currentFloor - 1]))
                {
                    if (platformPos.Y < elevatorFloors[currentFloor - 1].Y)
                    {
                        runOnceEleCheck();
                        platformPos.Y++;
                        isOnElevatorPlatform(false);
                    }
                    else if (platformPos.Y > elevatorFloors[currentFloor - 1].Y)
                    {
                        platformPos.Y = elevatorFloors[currentFloor - 1].Y;
                        game.playerRobot.isOnEle = false;
                        playerEleCheck = false;
                        itemEleCheck = false;
                        runOnceItem = false;
                        runOncePlayer = false;
                    }
                    else if (platformPos.Y == elevatorFloors[currentFloor - 1].Y)
                    {
                        currentFloor -= 1;
                        isButtonPressed = false;
                        if (currentFloor > 0)
                        {
                            goingDown = true;
                        }
                        else if (currentFloor <= 0)
                        {
                            goingDown = false;
                        }
                        game.playerRobot.isOnEle = false;
                        playerEleCheck = false;
                        itemEleCheck = false;
                        runOnceItem = false;
                        runOncePlayer = false;
                    }
                }
                else goingDown = !goingDown;//test
            }
            else if ((connectedWire.PowerLevel < powerRequired[currentFloor] || !isPowered) && currentFloor > 0)
            {
                if (platformPos.Y > elevatorFloors[currentFloor - 1].Y)
                {
                    runOnceEleCheck();
                    isOnElevatorPlatform(false);
                    platformPos.Y++;
                }
                else if (platformPos.Y < elevatorFloors[currentFloor - 1].Y)
                {
                    platformPos.Y = elevatorFloors[currentFloor - 1].Y;
                    game.playerRobot.isOnEle = false;
                    playerEleCheck = false;
                    itemEleCheck = false;
                    runOnceItem = false;
                    runOncePlayer = false;
                }
                else if (platformPos.Y == elevatorFloors[currentFloor - 1].Y)
                {
                    currentFloor -= 1;
                    isButtonPressed = false;
                    if (currentFloor > 0)
                    {
                        goingDown = true;
                    }
                    else if (currentFloor <= 0)
                    {
                        goingDown = false;
                    }
                    game.playerRobot.isOnEle = false;
                    playerEleCheck = false;
                    itemEleCheck = false;
                    runOnceItem = false;
                    runOncePlayer = false;
                }
            }
        }
        public void DetermineIsPowered()
        {
            if (connectedWire.IsPowered)
            {
                this.isPowered = true;
            }
            else this.isPowered = false;
        }
        public void ModifyObjectPostionVertical(GameObject g, bool b)
        {
            if (b)
            {
                g.position.Y--;
            }
            else if (!b)
            {
                g.position.Y++;
            }            
        }
        public void isOnElevatorPlatform(bool b)
        {
            //check player
            if (playerEleCheck)
            {
                if (game.playerRobot.actualRect.Intersects(this.collisionRect))
                {
                    game.playerRobot.isOnEle = true;
                    ModifyObjectPostionVertical(game.playerRobot, b);
                }
            }
            //check enemies
            //check items
            if (itemEleCheck)
            {
                foreach (PickupableItem i in level.itemInLevelList)
                {
                    if (this.collisionRect.Intersects(i.colRect))
                    {
                        ModifyObjectPostionVertical(i, b);
                    }
                }
            }
        }
        public void runOnceEleCheck()
        {
            if (!runOncePlayer)
            {
                runOncePlayer = true;
                if (game.playerRobot.actualRect.Intersects(collisionRect))
                {
                    playerEleCheck = true;
                }
            }
            if (!runOnceItem)
            {
                runOnceItem = true;
                itemEleCheck = false;
                foreach (PickupableItem i in level.itemInLevelList)
                {
                    if (this.collisionRect.Intersects(i.colRect))
                    {
                        itemEleCheck = true;
                    }
                }
            }
        }
        public void instantiateButtonRects()
        {
            foreach (Vector2 p in elevatorFloors)
            {
                Rectangle temp = new Rectangle((int)p.X, (int)p.Y - 60, button.Width, button.Height);
                buttonRectList.Add(temp);
            }
        }
        public void elevatorButtonCall()
        {
            foreach (Rectangle r in buttonRectList)
            {
                if (this.isButtonPressed == false && r.Intersects(game.playerRobot.actualRect) && connectedWire.isPowered)
                {
                    if (game.playerRobot.isButtonPressed)
                    {
                        this.isButtonPressed = true; //need to test multiple elevators
                    }
                }
            }
        }
        public override void Render(SpriteBatch sb)
        {
            //sb.Draw(game.barrierDebug, collisionRect, new Rectangle(0,0,collisionRect.Width,collisionRect.Height), Color.White, rotation, Vector2.Zero, SpriteEffects.None, .1f);
            sb.Draw(platform, platformPos, null, Color.White, rotation, Vector2.Zero, scale, SpriteEffects.None, depth-.01f);
            //sb.Draw(track, new Vector2(this.position.X + 60, this.position.Y + 60-track.Height), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.6f);     //rect rather then vect       
            elevatorBoxAnimation.RenderAnimation(sb);
            foreach (Rectangle r in buttonRectList)
            {
                sb.Draw(button, r, new Rectangle(0, 0, button.Width, button.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            }
        }
        public bool IsButtonPressed
        {
            get { return isButtonPressed; }
            set { isButtonPressed = value; }
        }
    }
}
