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
    public class GameObject //What if gameobjects did not include the player and enemy?
        //Gameobjects in that sense would be doors, conveyorbelts, items(wires,rubber,ect),Power source 
                    //all of which would be defined in their special classes
    {
        //attributes all gameobjects would have
        public Texture2D texture;
        public Vector2 position, origin;
        //public Vector4 influence; //X positive x axis(right), Y is positive Y axis(Down), Z is negative X Axis(Left), W is negative Y axis (Up)
        public Rectangle rect;
        protected Game1 game;
        //booleans to check from which 
        //direction a collision is about to happen
        public bool fromTheRight, fromTheLeft, fromAbove, fromBelow;
        public bool fromTopRight, fromTopLeft, fromBottomRight, fromBottomLeft;
        
        //bool collidable?
        //Velocity seperated into 4 floats, one for each direction
        public float positiveXSpd, negativeXSpd, positiveYSpd,
            negativeYSpd;
        public float rotation, scale, depth;
        public int width, height;
        //Maximum speed allowed for an object
        public short velCap, animationRow;
        //friction works similar to gravity, but in all directions
        public float inertia, friction;
        public bool isRightFacing;
        public bool isCollidable; // may need to be manipulated in a certain situation
        public Color color;

        //health for player, enemies etc
        public int health;

        public float powerModifier;

        public GameObject(Texture2D t, Game1 g)
        {
            
            health = 100;
            texture = t;
            position = Vector2.Zero;
            animationRow = 0;
            fromTheRight = false;
            fromTheLeft = false;
            fromAbove = false;
            fromBelow = false;
            game = g;
            rotation = 0f;
            scale = 1f;
            velCap = 3;
            depth = .5f;
            isRightFacing = true; //make a method for ai objects to determine if front facing
            color = Color.White;
            if (texture != null)
            {
                rect = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);
                origin = new Vector2(position.X + ((int)texture.Width / 2), position.Y + ((int)texture.Height / 2));
            }
            isCollidable = true; //for the most part, we want objects to be able to be stopped(collidable) 
            //with other objects
        }

        public virtual void Update()
        {    
            //NotFromAnyDirection();
            //if the object is meant to collide with other objects, figure out approaches
            if (isCollidable)
            {
                ApproachingFrom();
            }

            Gravity();
            //ModifySpeedByInfluence();
           
        }

        //method to calculate which direction the moving object
        //is about to collide from
        protected void ApproachingFrom()
        {
            fromTheRight = false;
            fromTheLeft = false;
            fromAbove = false;
            fromBelow = false;

            //checks from the appropriate sides of both objects to see 
            //if they are about to collide. If there is atleast one
            //object that the moving object is about to collide with, 
            //the moving object's position in that direction is not 
            //updated (or allowed)
            foreach (BarrierComponent b in game.currentLevel.barrierList)
            {
                if ((this.origin.X - (this.width / 2) > b.origin.X + (b.width / 2)))
                {
                    if ((((this.origin.X - this.width / 2) - (b.origin.X + b.width / 2)) < 7) &&
                        PerpendicularColl(this.origin.Y, this.height / 2, b.origin.Y, b.height / 2))
                    {
                        if (!fromTheRight)
                        {
                            fromTheRight = true;
                        }
                    }
                }
                else if ((this.origin.X + this.width / 2) < b.origin.X - (b.width / 2))
                {
                    if ((((b.origin.X - b.width / 2) - (this.origin.X + this.width / 2)) < 7) &&
                        PerpendicularColl(this.origin.Y, this.height / 2, b.origin.Y, b.height / 2))
                    {
                        if (!fromTheLeft)
                        {
                            fromTheLeft = true;
                        }
                    }
                }
                else if (((this.origin.Y + this.height / 2) < b.origin.Y - b.height / 2))
                {
                    if ((((b.origin.Y - b.height / 2) - (this.origin.Y + this.height / 2)) < 7) &&
                        PerpendicularColl(this.origin.X, this.width / 2, b.origin.X, b.width / 2))
                    {
                        if (!fromAbove)
                        {
                            fromAbove = true;
                        }
                    }
                }
                else if (((this.origin.Y - this.height / 2) > b.origin.Y + b.height / 2))
                {
                    if ((((this.origin.Y - this.height / 2) - (b.origin.Y + b.height / 2)) < 7) &&
                        PerpendicularColl(this.origin.X, this.width / 2, b.origin.X, b.width / 2))
                    {
                        if (!fromBelow)
                        {
                            fromBelow = true;
                        }
                    }
                }
               
               
            }
            
            PreventDiagonal();
           
        }

        //while checking collision on Y axis, calls in x radii for both objects, 
        //returns true if abs val of distance between X axis of objects is less than 
        //sum of the X radii.
        //vice versa for checking collision on X axis

        // don't update pos in certain directions if broken through and 
        //inside object??

        private bool PerpendicularColl(float characterAxis, float characterRadius,
            float barrierAxis, float barrierRadius)
        {
            //uncommon, but bug where you clip through diagonally
            //if this happens, increase for loop, add percentage in range, 
            //increase tolerance number in approaching from method

            //new bug with going through the corners. possible solution
            //is to check different imaginary boundaries inside the boundary
            //then set the player's position back up to where it's supposed
            //to be

            bool colliding = false;
            for (int i = 0; i < 5; i++)
            {
                //using the radius of each object, checks to see if collision exists on the opposite axis
                //. otherwise, the program creates an unpassible barrier straight through the middle 
                //of the screen
                if ((Math.Abs(characterAxis - barrierAxis)) < ((characterRadius + barrierRadius) + (.0115f * (characterRadius + barrierRadius))))
                {
                    if (!colliding)
                    {
                        colliding = true;
                    }
                }
            }
            return colliding;
        }

        //function to get an objects specific corner
        //1st parameter-the gameobject, 2nd parameter-
        //string to specify direction
        //change to enums
        string topLeft = "top left";
        string topRight = "top right";
        string bottomLeft = "bottom left";
        string bottomRight = "bottom right";
        public Vector2 FindCorner(GameObject g, string corner)
        {
            string chosenCorner = corner;
            Vector2 cornerPos = new Vector2(0, 0);
            if (corner == topLeft)
            {
                cornerPos.Y = (g.origin.Y - (g.height / 2));
                cornerPos.X = (g.origin.X - (g.width / 2));
            }
            else if (corner == topRight)
            {
                cornerPos.Y = (g.origin.Y - (g.height / 2));
                cornerPos.X = (g.origin.X + (g.width / 2));
            }
            else if (corner == bottomLeft)
            {
                cornerPos.Y = (g.origin.Y + (g.height / 2));
                cornerPos.X = (g.origin.X - (g.width / 2));
            }
            else if (corner == bottomRight)
            {
                cornerPos.Y = (g.origin.Y + (g.height / 2));
                cornerPos.X = (g.origin.X + (g.width / 2));
            }
            return cornerPos;
        }
        public void ApplyPosXFriction()
        {
            if (positiveXSpd > 0)
            {
                positiveXSpd -= friction;
                HaultXInertia();
            }
        }
        //slows neg x inertia
        public void ApplyNegXFriction()
        {
            if (negativeXSpd < 0)
            {
                negativeXSpd += friction;
                HaultXInertia();
            }
        }
        public void HaultXInertia()
        {
            if (positiveXSpd < inertia && positiveXSpd > -inertia)
            {
                positiveXSpd = 0;
            }
            if (negativeXSpd < inertia && negativeXSpd > -inertia)
            {
                negativeXSpd = 0;
            }
        }
        public void PreventDiagonal()
        {
            
            fromTopLeft = false;
            fromTopRight = false;
            fromBottomLeft = false;
            fromBottomRight = false;
            foreach (BarrierComponent b in game.currentLevel.barrierList)
            {
                FindDiagonalColl(b);
                if (fromTopLeft)
                {
                    if (FindCorner(this, bottomRight).Y > FindCorner(b, topLeft).Y)
                    {
                        fromTheLeft = true;
                        position.X -= 10;
                        
                    }
                    else if (FindCorner(this, bottomRight).Y < FindCorner(b, topLeft).Y)
                    {
                       fromAbove = true;
                       position.Y -= 10;
                    }
                    else
                    {
                        fromTheLeft = true;
                        position.X -= 10;
                    }
                }
                else if (fromTopRight)
                {
                    if (FindCorner(this, bottomLeft).Y > FindCorner(b, topRight).Y)
                    {
                        fromTheRight = true;
                        position.X += 10;
                    }
                    else if (FindCorner(this, bottomLeft).Y < FindCorner(b, topRight).Y)
                    {
                        fromAbove = true;
                        position.Y -= 10;
                    }
                    else
                    {
                        fromTheRight = true;
                        position.X += 10;
                    }
                }
                else if (fromBottomLeft)
                {
                    if (FindCorner(this, topRight).Y < FindCorner(b, bottomLeft).Y)
                    {
                        fromTheLeft = true;
                        position.X -= 10;
                    }
                    else if (FindCorner(this, topRight).Y > FindCorner(b, bottomLeft).Y)
                    {
                        fromBelow = true;
                        position.Y += 10;
                    }
                    else
                    {
                        fromTheLeft = true;
                        position.X -= 10;
                    }
                }
                else if (fromBottomRight)
                {
                    if (FindCorner(this, topLeft).Y < FindCorner(b, bottomRight).Y)
                    {
                        fromTheRight = true;
                        position.X += 10;
                    }
                    else if (FindCorner(this, topLeft).Y > FindCorner(b, bottomRight).Y)
                    {
                        fromBelow = true;
                        position.Y += 10;
                    }
                    else
                    {
                       fromTheRight = true;
                       position.X += 10;
                    }
                }
            }
            
        }

        public void FindDiagonalColl(GameObject g)
        {
            Vector2 playerTopLeft, playerTopRight, playerBottomLeft,
                playerBottomRight;
            playerTopLeft = FindCorner(this, topLeft);
            playerTopRight = FindCorner(this, topRight);
            playerBottomLeft = FindCorner(this, bottomLeft);
            playerBottomRight = FindCorner(this, bottomRight);

            
            
                Vector2 barrierTopLeft, barrierTopRight, barrierBottomLeft,
                    barrierBottomRight;
                barrierTopLeft = FindCorner(g, topLeft);
                barrierTopRight = FindCorner(g, topRight);
                barrierBottomLeft = FindCorner(g, bottomLeft);
                barrierBottomRight = FindCorner(g, bottomRight);
                if (Distance(playerBottomRight,barrierTopLeft) < 20)
                {
                    if (!fromTopLeft && playerBottomRight.X >= barrierTopLeft.X && playerBottomRight.Y >= barrierTopLeft.Y)
                    {
                        fromTopLeft = true;
                    }
                }
                else if (Distance(playerBottomLeft, barrierTopRight) < 20)
                {
                    if (!fromTopLeft && playerBottomLeft.X <= barrierTopRight.X && playerBottomLeft.Y >= barrierTopLeft.Y)
                    {
                        fromTopRight = true;
                    }
                }
                else if (Distance(playerTopRight, barrierBottomLeft) < 20)
                {
                    if (!fromBottomLeft && playerTopRight.X >= barrierBottomLeft.X && playerTopRight.Y <= barrierBottomLeft.Y)
                    {
                        fromBottomLeft = true;
                    }
                }
                else if (Distance(playerTopLeft, barrierBottomRight) < 20)
                {
                    if (!fromBottomRight && playerTopLeft.X <= barrierBottomRight.X && playerTopLeft.Y <= barrierBottomRight.Y)
                    {
                        fromBottomRight = true;
                    }
                }
            
        }

        //public float HypotenuseDistance(Texture2D characterSprite, Texture2D barrierSprite)
        //{
        //    float characterHypotenuse, barrierHypotenuse;

        //    characterHypotenuse = (float)Math.Sqrt((characterSprite.Width * characterSprite.Width) + (characterSprite.Height * characterSprite.Height));
        //    barrierHypotenuse = (float)Math.Sqrt((barrierSprite.Width * barrierSprite.Width) + (barrierSprite.Height * barrierSprite.Height));



        //    return characterHypotenuse+barrierHypotenuse;
        //}
        public float Distance(Vector2 p1, Vector2 p2)
        {
            Vector2 a, b;
            float x, y;
            a = p1;
            b = p2;
            x = a.X - b.X;
            y = a.Y - b.Y;
            return (float)Math.Sqrt(x * x + y * y);
        }
        public void Gravity()
        {
            if (!fromAbove)
            {
                this.positiveYSpd = 2;
            }
        }
        public void HandleYMovementDynamicObj()
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

        }
       
        //Main draw method to be called in Game1
        public virtual void Render(SpriteBatch sb)
        {
            sb.Draw(texture, position, new Rectangle(0,0,texture.Width,texture.Height),color, rotation, Vector2.Zero, scale, SpriteEffects.None, depth);
        }

        #region Properties
        //public bool FromTheRight
        //{
        //    set { this.fromTheRight = value; }
        //    get { return this.fromTheRight; }
        //}
        //public bool FromTheLeft
        //{
        //    set { this.fromTheLeft = value; }
        //    get { return this.fromTheLeft; }
        //}
        //public bool FromAbove
        //{
        //    set { this.fromAbove = value; }
        //    get { return this.fromAbove; }
        //}
        //public bool FromBelow
        //{
        //    set { this.fromBelow = value; }
        //    get { return this.fromBelow; }
        //}
       
        public float PositiveXSpeed
        {
            get {return positiveXSpd;}
            set {positiveXSpd = value;}
        }
        public float PositiveYSpeed
        {
            get {return positiveYSpd;}
            set {positiveYSpd = value;}
        }
        public float NegativeXSpeed
        {
            get {return negativeXSpd;}
            set {negativeXSpd = value;}
        }
        public float NegativeYSpeed
        {
            get {return negativeYSpd;}
            set {negativeYSpd = value;}
        }
        public short AnimationRow
        {
            get { return animationRow; }
            set { animationRow = value; }
        }

        #endregion

    }
}
