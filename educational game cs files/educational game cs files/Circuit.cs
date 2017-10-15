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
    public class Circuit : GameObject
    {
        public List<Wire> listOfWires;
        List<Vector2> listOfVectors;
        List<int> listOfInts;
        List<bool> listOfBools;
        List<string> listOfNames;
        Wire tempWire;
        PowerSource powerSource;
        public string circuitName, branchName;
        Wire connectedWire;
        float lastWirePower;
        public Circuit(List<Vector2> pos, PowerSource pwr, Game1 g, Texture2D t, List<int> dir, List<bool> hasSock, string n, List<string> s) 
            : base(t, g)

            //position generated from the text file, powerlevel requirement from the door
        {
            circuitName = n;
            listOfNames = s;
            listOfWires = new List<Wire>();
            listOfVectors = pos;
            powerSource = pwr;
            listOfInts = dir;
            listOfBools = hasSock;
            PopulateWireList();
            isCollidable = false;
            connectedWire = null;
        }
        public Circuit(List<Vector2> pos, PowerSource pwr, Game1 g, Texture2D t, List<int> dir, List<bool> hasSock, Wire cw, string n, List<string> s) 
            : base(t, g)

            //position generated from the text file, powerlevel requirement from the door
        {
            branchName = n;
            listOfNames = s;
            listOfWires = new List<Wire>();
            listOfVectors = pos;            
            listOfInts = dir;
            listOfBools = hasSock;
            isCollidable = false;
            powerSource = pwr;
            connectedWire = cw;
            PopulateBranchWireList();
        }

        public override void Update()
        {
            if (listOfWires != null)
            {
                foreach (Wire w in listOfWires)
                {
                    w.Update();
                }
                lastWirePower = listOfWires[listOfWires.Count - 1].PowerLevel;
            }

            base.Update();
        }

        public override void Render(SpriteBatch sb)
        {
            if (listOfWires != null)
            {
                foreach (Wire w in listOfWires)
                {
                    w.Render(sb);
                    if (w.hasSocket)
                    {
                        if (w.socketItem != null)
                        {
                            sb.Draw(w.socketItem.texture, w.position, new Rectangle(0, 0, w.socketItem.texture.Width, w.socketItem.texture.Height), color, rotation, Vector2.Zero, scale, SpriteEffects.None, .4f);
                        }
                    }
                }
            }
        }

        public void PopulateWireList() 
        {
            tempWire = new Wire(listOfVectors[0], game, texture, listOfInts[0], listOfBools[0], listOfNames[0]);
            tempWire.powerSource = this.powerSource;
            tempWire.powerLevel = powerSource.PowerSupplied;
            tempWire.parent = null; //starting node at power supply
            
            listOfWires.Add(tempWire);
            for (int i = 1; i <= listOfVectors.Count - 1; i++) 
            {
                tempWire = new Wire(listOfVectors[i], game, texture, listOfInts[i], listOfBools[i], listOfNames[i]);
                tempWire.parent = listOfWires[i - 1];
                if (!listOfBools[i])
                {
                    tempWire.powerLevel = tempWire.parent.powerLevel;
                }
                tempWire.powerSource = this.powerSource;
                listOfWires[i - 1].child = tempWire;
                listOfWires.Add(tempWire);
            }
        }
        public void PopulateBranchWireList()
        {
            tempWire = new Wire(listOfVectors[0], game, texture, listOfInts[0], listOfBools[0], listOfNames[0]);
            tempWire.powerSource = this.powerSource;
            tempWire.powerLevel = connectedWire.powerLevel;
            tempWire.parent = connectedWire; //starting node at branch circuit
            connectedWire.secondChild = tempWire;
            listOfWires.Add(tempWire);
            for (int i = 1; i <= listOfVectors.Count - 1; i++) 
            {
                tempWire = new Wire(listOfVectors[i], game, texture, listOfInts[i], listOfBools[i], listOfNames[i]);
                tempWire.parent = listOfWires[i - 1];
                tempWire.powerSource = this.powerSource;
                if (!listOfBools[i])
                {
                    tempWire.powerLevel = tempWire.parent.powerLevel;
                }
                listOfWires[i - 1].child = tempWire;
                listOfWires.Add(tempWire);
            }
        }

        public float LastWirePower
        {
            get { return lastWirePower; }
        }
    }
}
