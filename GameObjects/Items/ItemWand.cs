﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects.MapObjects;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObjects.Items
{
    public class ItemWand : Item
    {
        public int Component1 { get; set; }
        public int Component2 { get; set; }
        public int Component3 { get; set; }
        public Color Component1Color { get; set; }
        public Color Component2Color { get; set; }
        public Color Component3Color { get; set; }
        public Color[] WandColours = new Color[]
        {
            Color.Red,Color.Blue,Color.Beige,Color.CornflowerBlue,Color.Gold,Color.Silver,Color.Lime,Color.Green,Color.Magenta
        };
        string[] WandShapeNames = new string[]
        {
            "thick", "plain", "forked", "crooked"
        };
        string[] WandColourNames = new string[]
        {
            "red", "blue", "ivory", "sky blue", "golden", "silver", "lime", "green","purple"
        };
        public ItemWand(System.Random RNG)
        {
            int colourcount = WandColours.Count();
            int c1 = RNG.Next(0, 4);
            this.Component1 = c1;
            this.Component2 = RNG.Next(0, 4);
            this.Component3 = RNG.Next(0, 4);
            int cc1 = RNG.Next(0, colourcount);
            this.Component1Color = WandColours[cc1];
            this.Component2Color = WandColours[RNG.Next(0, colourcount)];
            this.Component3Color = WandColours[RNG.Next(0, colourcount)];
            this.Name = "Unidentified " + WandShapeNames[c1] + " " + WandColourNames[cc1] + " wand";
        }
        public override void Render(int X, int Y, GraphicsDevice device, Renderer Renderer,float Scale=1.0f)
        {
            Renderer.SetColour(this.Component1Color);
            Renderer.RenderIconEx(device, X, Y, this.Component1 + 64,Scale);
            Renderer.SetColour(this.Component2Color);
            Renderer.RenderIconEx(device, X, Y, this.Component2 + 80,Scale);
            Renderer.SetColour(this.Component3Color);
            Renderer.RenderIconEx(device, X, Y, this.Component3 + 96,Scale);
            Renderer.SetColour(Color.Gray);
        }
        public override bool Apply(Actor Source,Actor Target)
        {
            if (Source is Player p)
                p.AimedAction = Zap;
            return base.Apply(Source, Target);
        }
        void Zap(Actor Source,Point Target)
        {
            if (Source.ParentMap.ItemAt(Target.X, Target.Y) is Actor a)
                a.TakeDamage(13);
            GameObjects.MapObjects.Particles.ParticleBeam beam = new MapObjects.Particles.ParticleBeam();
            beam.X = Source.X;
            beam.Y = Source.Y;
            beam.TargetX = Target.X;
            beam.TargetY = Target.Y;
            beam.TimeLeft = 0.5f;
            beam.Colour = this.Component1Color;
            beam.Width = 0.5f;
            Source.ParentMap.Particles.Add(beam);
        }
    }
}
