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
    public class Player: GameObject
    {
        //public Texture2D texture;
        
        public bool checkDiagBool, isButtonPressed;
        AnimationComponent animation;
        public Level level;
        public Rectangle actualRect;
        int invCursorCount;
        int count = 10;
        int countCursor = 10;
        public bool countBool,cursorBool;
        private HealthMeter hMeter;
        public Scanner scanner;
        public bool isOnEle;
        public Player(Texture2D t, Game1 g, Level l)
            : base(t, g)
        {
            scanner = new Scanner(g, this, l);
            health = 100;
            hMeter = new HealthMeter(new Vector2(75, 50), 2, game.healthBorder, game.healthBar,
                game, this);
            level = l;
            texture = t;
            position = new Vector2(150, 400);
            width = 50;
            height = 50;
            actualRect = new Rectangle((int)position.X, (int)position.Y, width, height);
            velCap = 3;
            inertia = 0.3f;
            friction = 0.1f;
            scale = .5f;
            animation = new AnimationComponent(this, 10, animationRow, 100, 100);
            animation.FrameSpeed = 1f;
            depth = .2f;
            invCursorCount = 0;
            //isCollidable = false;
            countBool = false;
            cursorBool = false;
            isOnEle = false;
        }

        public override void Update()
        {
            //Gravity();
            scanner.Update();
            HandleCounters();
            origin.X = position.X + (width / 2);
            origin.Y = position.Y + (height / 2);
            actualRect.X = (int)position.X;
            actualRect.Y = (int)position.Y;
            
            if (Keyboard.GetState().IsKeyDown(Keys.E) && countBool) //update to work with multiple ele or other button objs
            {
                isButtonPressed = true;  
                PickUpItemFromLevel();
                HandleSocketItems();
                countBool = false;
            }
            else isButtonPressed = false;
            HandleInvCursorCount();
            if (!isOnEle)
            {
                HandleXMovement();
                HandleYMovement();
                ApproachingFrom(); //moved from game obj to here for player, stopped calling base, add isonele condition so you cant move or collide if the elevator is moving, 
                                    //hardcoded in elev to set true when the cases would be true and set false elsewhere
            }
            animation.UpdateAnimation();

            //update health meter
            hMeter.UpdateMeter();
            

            Gravity();
            //base.Update();
        }

        //handles movement for the positive y direction
        //and the negative y direction. has them split
        //into 2 seperate floats
        private void HandleYMovement()
        {
            rect.Y = (int)position.Y;
            //if not about to collide from above, 
            //update the position
            if (!fromAbove)
            {
                position.Y += positiveYSpd;
            }
            //if not about to collide from below, 
            //update the position
            if (!fromBelow)
            {
                position.Y += negativeYSpd;
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.W) && !fromBelow)
            //{
                
            //    if (negativeYSpd > -velCap)
            //    {
            //        negativeYSpd -= inertia;
            //        if (positiveYSpd > 0)
            //        {
            //            positiveYSpd -= inertia;
            //        }
            //    }
            //}
            //else
            //{
                
            //    //otherwise apply friction direction not 
            //    //being pressed by the relative key
            //    ApplyNegYFriction();
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.S) && !fromAbove)
            {
                animationRow = 1;
                if (positiveYSpd < velCap)
                {
                    positiveYSpd += inertia;
                    if (negativeYSpd < 0)
                    {
                        negativeYSpd += inertia;
                    }
                }
            }
            else
            {
                
                //otherwise apply friction to direction not 
                //being pressed by the relative key
                ApplyPosYFriction();
                
            }
        }

        //slows pos y inertia
        private void HandleCounters()
        {
            if (count > 0 && countBool == false)
            {
                count--;
                countBool = false;
            }
            if (count == 0)
            {
                count = 20;
                countBool = true;
            }
            if (countCursor > 0 && cursorBool == false)
            {
                countCursor--;
                cursorBool = false;
            }
            if (countCursor == 0)
            {
                countCursor = 10;
                cursorBool = true;
            }
        }
        private void ApplyPosYFriction()
        {
            if (positiveYSpd > 0)
            {
                positiveYSpd -= friction;
                HaultYInertia();
            }
        }
        //slows neg y inertia
        private void ApplyNegYFriction()
        {
            if (negativeYSpd < 0)
            {
                negativeYSpd += friction;
                HaultYInertia();
            }
        }

        //method to make a character come to a 
        //complete stop when they are barely 
        //moving on their own when they shouldn't be
        private void HaultYInertia()
        {
            if (positiveYSpd < inertia && positiveYSpd > -inertia)
            {
                positiveYSpd = 0;
            }
            if (negativeYSpd < inertia && negativeYSpd > -inertia)
            {
                negativeYSpd = 0;
            }
        }

        private void HandleXMovement(){
            //if not about to collide from left, 
            //update the position
            if (!fromTheLeft)
            {
                position.X += positiveXSpd;
            }
            //if not about to collide from right, 
            //update the position
            if (!fromTheRight)
            {
                position.X += negativeXSpd;
            }
            if (!(Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.S)))
            {
                animationRow = 0;
            }
            rect.X = (int)position.X;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !fromTheRight)
            {
                animationRow = 1;
                isRightFacing = false;
                if (negativeXSpd > -velCap)
                {
                    negativeXSpd -= inertia;
                    if (positiveXSpd > 0)
                    {
                        positiveXSpd -= inertia;
                    }
                }
            }
            else
            {
                //otherwise apply friction to direction not 
                //being pressed by the relative key
                ApplyNegXFriction();

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !fromTheLeft)
            {
                animationRow = 1;
                isRightFacing = true;
                if (positiveXSpd < velCap)
                {
                    positiveXSpd += inertia;
                    if (negativeXSpd < 0)
                    {
                        negativeXSpd += inertia;
                    }

                }
            }
            else
            {
                //otherwise apply friction to direction not 
                //being pressed by the relative key
                ApplyPosXFriction();
            }
        }

        //slows pos x inertia
       


        //method to make a character come to a 
        //complete stop when they are barely 
        //moving on their own when they shouldn't be
        

        //public bool CheckDiagnol()
        //{
        //    checkDiagBool = false;
        //    foreach (BarrierComponent b in game.barrierList)
        //    {
        //        if (Math.Abs((Distance(b.origin, this.origin) - HypotenuseDistance(this.texture, b.sprite))) < 100f)
        //        {
        //            checkDiagBool = true;
        //        }
                
        //    }
        //    return checkDiagBool;
        //}

        public void HandleSocketItems() //needs testing
        {
            if (level != null)
            {
                if (level.inventory != null)
                {
                    if (level.inventory.inventoryList != null)
                    {
                        if (level.ReturnCollidingSocketWire() != null)
                        {
                            if (level.ReturnCollidingSocketWire().socketItem == null && level.inventory.inventoryList.Count > 0 && invCursorCount >= 0 && invCursorCount < level.Inventory.inventoryList.Count)
                            {
                                level.ReturnCollidingSocketWire().socketItem = level.inventory.inventoryList[this.invCursorCount];
                                level.ReturnCollidingSocketWire().isBroken = false;
                                level.inventory.inventoryList.Remove(level.inventory.inventoryList[this.invCursorCount]);
                            }
                            else if (level.ReturnCollidingSocketWire().socketItem != null)
                            {
                                level.inventory.inventoryList.Add(level.ReturnCollidingSocketWire().socketItem);
                                level.ReturnCollidingSocketWire().socketItem = null;
                                level.ReturnCollidingSocketWire().isBroken = true;
                            }
                            level.attemptsCount++;
                        }
                    }
                }
            }
        }
        
        public void PickUpItemFromLevel() //needs testing
        {
            if (level != null)
            {
                if (level.itemInLevelList != null)
                {
                    foreach (PickupableItem p in level.itemInLevelList)
                    {
                        if (p.colRect.Intersects(game.playerRobot.actualRect) && isButtonPressed)
                        {
                            level.inventory.inventoryList.Add(p);
                            p.delete = true;
                            p.isButtonPressed = false;
                        }
                    }
                }
            }
        }
        public void HandleInvCursorCount() //draw with this info
        {
            if (level != null)
            {
                if (level.inventory != null)
                {
                    if (invCursorCount < 0)
                    {
                        invCursorCount = 0;
                    }
                    if (scanner.state == Scanner.ScannerState.Items)
                    {
                        if (invCursorCount > level.inventory.inventoryList.Count - 1 && level.Inventory.inventoryList.Count > 0)
                        {
                            invCursorCount = level.inventory.inventoryList.Count - 1;
                        }
                    }
                    
                }
            }
        }
        public override void Render(SpriteBatch sb)
        {            
            animation.RenderAnimation(sb);
            //render health bar
            //hMeter.DrawMeter(sb);
            scanner.Render(sb);
        }

        #region Properties
        //Properties

        public float Inertia
        {
            set { this.inertia = value; }
            get { return this.inertia; }
        }
        public float Friction
        {
            set { this.friction = value; }
            get { return this.friction; }
        }
        public int InvCursorCount
        {
            get { return invCursorCount; }
            set { invCursorCount = value; }
        }
        #endregion
    }
}
